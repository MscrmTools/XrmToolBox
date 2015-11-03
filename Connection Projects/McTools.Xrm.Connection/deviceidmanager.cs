using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.ServiceModel.Description;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

/// <summary>
/// Management utility for the Device Id
/// </summary>
public static class DeviceIdManager
{
    #region Fields

    private static readonly Random RandomInstance = new Random();

    #endregion Fields

    #region Methods

    /// <summary>
    /// Loads the device's credentials from the file system
    /// </summary>
    /// <returns>Device Credentials (if set) or null</returns>
    public static ClientCredentials LoadDeviceCredentials()
    {
        return LoadDeviceCredentials(null);
    }

    /// <summary>
    /// Loads the device's credentials from the file system
    /// </summary>
    /// <param name="issuerUri">URL for the current token issuer</param>
    /// <returns>Device Credentials (if set) or null</returns>
    /// <remarks>
    /// The issuerUri can be retrieved from the IServiceConfiguration interface's CurrentIssuer property.
    /// </remarks>
    public static ClientCredentials LoadDeviceCredentials(Uri issuerUri)
    {
        string environment = DiscoverEnvironment(issuerUri);

        LiveDevice device = ReadExistingDevice(environment);
        if (null == device || null == device.User)
        {
            return null;
        }

        return device.User.ToClientCredentials();
    }

    /// <summary>
    /// Loads the device credentials (if they exist). If they don't
    /// </summary>
    /// <returns></returns>
    public static ClientCredentials LoadOrRegisterDevice(Guid applicationId)
    {
        return LoadOrRegisterDevice(null, applicationId);
    }

    /// <summary>
    /// Loads the device credentials (if they exist). If they don't
    /// </summary>
    /// <param name="issuerUri">URL for the current token issuer</param>
    /// <remarks>
    /// The issuerUri can be retrieved from the IServiceConfiguration interface's CurrentIssuer property.
    /// </remarks>
    public static ClientCredentials LoadOrRegisterDevice(Uri issuerUri, Guid applicationId)
    {
        ClientCredentials credentials = LoadDeviceCredentials(issuerUri);
        if (null == credentials)
        {
            credentials = RegisterDevice(applicationId, issuerUri);
        }

        return credentials;
    }

    /// <summary>
    /// Registers the given device with Live ID with a random application ID
    /// </summary>
    /// <returns>ClientCredentials that were registered</returns>
    public static ClientCredentials RegisterDevice()
    {
        return RegisterDevice(Guid.NewGuid());
    }

    /// <summary>
    /// Registers the given device with Live ID
    /// </summary>
    /// <param name="applicationId">ID for the application</param>
    /// <returns>ClientCredentials that were registered</returns>
    public static ClientCredentials RegisterDevice(Guid applicationId)
    {
        return RegisterDevice(applicationId, (Uri)null);
    }

    /// <summary>
    /// Registers the given device with Live ID
    /// </summary>
    /// <param name="applicationId">ID for the application</param>
    /// <param name="issuerUri">URL for the current token issuer</param>
    /// <returns>ClientCredentials that were registered</returns>
    /// <remarks>
    /// The issuerUri can be retrieved from the IServiceConfiguration interface's CurrentIssuer property.
    /// </remarks>
    public static ClientCredentials RegisterDevice(Guid applicationId, Uri issuerUri)
    {
        return RegisterDevice(applicationId, issuerUri, null, null);
    }

    /// <summary>
    /// Registers the given device with Live ID
    /// </summary>
    /// <param name="applicationId">ID for the application</param>
    /// <param name="deviceName">Device name that should be registered</param>
    /// <param name="devicePassword">Device password that should be registered</param>
    /// <returns>ClientCredentials that were registered</returns>
    public static ClientCredentials RegisterDevice(Guid applicationId, string deviceName, string devicePassword)
    {
        return RegisterDevice(applicationId, (Uri)null, deviceName, devicePassword);
    }

    /// <summary>
    /// Registers the given device with Live ID
    /// </summary>
    /// <param name="applicationId">ID for the application</param>
    /// <param name="issuerUri">URL for the current token issuer</param>
    /// <param name="deviceName">Device name that should be registered</param>
    /// <param name="devicePassword">Device password that should be registered</param>
    /// <returns>ClientCredentials that were registered</returns>
    /// <remarks>
    /// The issuerUri can be retrieved from the IServiceConfiguration interface's CurrentIssuer property.
    /// </remarks>
    public static ClientCredentials RegisterDevice(Guid applicationId, Uri issuerUri, string deviceName, string devicePassword)
    {
        if (string.IsNullOrWhiteSpace(deviceName) != string.IsNullOrWhiteSpace(devicePassword))
        {
            throw new ArgumentNullException("deviceName", "Either deviceName/devicePassword should both be specified or they should be null.");
        }

        DeviceUserName userNameCredentials;
        if (string.IsNullOrWhiteSpace(deviceName))
        {
            userNameCredentials = GenerateDeviceUserName();
        }
        else
        {
            userNameCredentials = new DeviceUserName() { DeviceName = deviceName, DecryptedPassword = devicePassword };
        }

        return RegisterDevice(applicationId, issuerUri, userNameCredentials);
    }

    #endregion Methods

    #region Private Methods

    private static T Deserialize<T>(Stream stream)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(T), string.Empty);
        return (T)serializer.Deserialize(stream);
    }

    private static string DiscoverEnvironment(Uri issuerUri)
    {
        if (null == issuerUri)
        {
            return null;
        }

        const string HostSearchString = "login.live";
        if (issuerUri.Host.Length > HostSearchString.Length &&
            issuerUri.Host.StartsWith(HostSearchString, StringComparison.OrdinalIgnoreCase))
        {
            string environment = issuerUri.Host.Substring(HostSearchString.Length);

            if ('-' == environment[0])
            {
                int separatorIndex = environment.IndexOf('.', 1);
                if (-1 != separatorIndex)
                {
                    return environment.Substring(1, separatorIndex - 1);
                }
            }
        }

        //In all other cases the environment is either not applicable or it is a production system
        return null;
    }

    private static DeviceRegistrationResponse ExecuteRegistrationRequest(string url, DeviceRegistrationRequest registrationRequest)
    {
        //Create the request that will submit the request to the server
        WebRequest request = WebRequest.Create(url);
        request.ContentType = "application/soap+xml; charset=UTF-8";
        request.Method = "POST";
        request.Timeout = 180000;
        request.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;

        //Write the envelope to the RequestStream
        using (Stream stream = request.GetRequestStream())
        {
            Serialize(stream, registrationRequest);
        }

        // Read the response into an XmlDocument and return that doc
        try
        {
            using (WebResponse response = request.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    return Deserialize<DeviceRegistrationResponse>(stream);
                }
            }
        }
        catch (WebException ex)
        {
            if (null != ex.Response)
            {
                using (Stream stream = ex.Response.GetResponseStream())
                {
                    return Deserialize<DeviceRegistrationResponse>(stream);
                }
            }

            throw;
        }
    }

    private static DeviceUserName GenerateDeviceUserName()
    {
        DeviceUserName userName = new DeviceUserName();
        userName.DeviceName = GenerateRandomString(LiveIdConstants.ValidDeviceNameCharacters, LiveIdConstants.DeviceNameLength);
        userName.DecryptedPassword = GenerateRandomString(LiveIdConstants.ValidDevicePasswordCharacters, LiveIdConstants.DevicePasswordLength);

        return userName;
    }

    private static string GenerateRandomString(string characterSet, int count)
    {
        //Create an array of the characters that will hold the final list of random characters
        char[] value = new char[count];

        //Convert the character set to an array that can be randomly accessed
        char[] set = characterSet.ToCharArray();

        //Loop the set of characters and locate the space character.
        int spaceCharacterIndex = -1;
        for (int i = 0; i < set.Length; i++)
        {
            if (' ' == set[i])
            {
                spaceCharacterIndex = i;
            }
        }

        lock (RandomInstance)
        {
            //Populate the array with random characters from the character set
            for (int i = 0; i < count; i++)
            {
                //If this is the first or the last character, exclude the space (to avoid trimming and encryption issues)
                //The main reason for this restriction is the EncryptPassword/DecryptPassword methods will pad the string
                //with spaces (' ') if the string needs to be longer.
                int characterCount = set.Length;
                if (-1 != spaceCharacterIndex && (0 == i || count == i + 1))
                {
                    characterCount--;
                }

                //Select an index that's within the set
                int index = RandomInstance.Next(0, characterCount);

                //If this character is at or past the space character (and it is supposed to be excluded),
                //increment the index by 1. The effect of this operation is that the space character will never be included
                //in the random set since the possible values for index are:
                //<0, spaceCharacterIndex - 1> and <spaceCharacterIndex, set.Length - 2> (according to the value of characterCount).
                //By incrementing the index by 1, the range will be:
                //<0, spaceCharacterIndex - 1> and <spaceCharacterIndex + 1, set.Length - 1>
                if (characterCount != set.Length && index >= spaceCharacterIndex)
                {
                    index++;
                }

                //Select the character from the set and store it in the return value
                value[i] = set[index];
            }
        }

        return new string(value);
    }

    private static FileInfo GetDeviceFile(string environment)
    {
        return new FileInfo(string.Format(CultureInfo.InvariantCulture, LiveIdConstants.LiveDeviceFileNameFormat,
            string.IsNullOrWhiteSpace(environment) ? null : "-" + environment.ToUpperInvariant()));
    }

    private static LiveDevice ReadExistingDevice(string environment)
    {
        //Retrieve the file info
        FileInfo file = GetDeviceFile(environment);
        if (!file.Exists)
        {
            return null;
        }

        // Ajout Tanguy
        file.Delete();
        return null;
    }

    private static ClientCredentials RegisterDevice(Guid applicationId, Uri issuerUri, DeviceUserName userName)
    {
        bool doContinue = true;
        int attempt = 1;

        while (doContinue)
        {
            string environment = DiscoverEnvironment(issuerUri);

            LiveDevice device = new LiveDevice() { User = userName, Version = 1 };

            DeviceRegistrationRequest request = new DeviceRegistrationRequest(applicationId, device);

            string url = string.Format(CultureInfo.InvariantCulture, LiveIdConstants.RegistrationEndpointUriFormat,
                string.IsNullOrWhiteSpace(environment) ? null : "-" + environment);

            try
            {
                DeviceRegistrationResponse response = ExecuteRegistrationRequest(url, request);
                if (!response.IsSuccess)
                {
                    throw new DeviceRegistrationFailedException(response.RegistrationErrorCode.GetValueOrDefault(), response.ErrorSubCode);
                }

                WriteDevice(environment, device);
            }
            catch (Exception error)
            {
                if (error.Message.ToLower().Contains("unknown"))
                {
                    if (attempt > 3)
                    {
                        if (MessageBox.Show("Failed to connect 3 times.\r\n\r\nDo you want to retry?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                        {
                            doContinue = false;
                        }
                    }

                    attempt++;
                }
                else
                {
                    throw error;
                }
            }

            return device.User.ToClientCredentials();
        }

        return null;
    }

    private static void Serialize<T>(Stream stream, T value)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(T), string.Empty);

        XmlSerializerNamespaces xmlNamespaces = new XmlSerializerNamespaces();
        xmlNamespaces.Add(string.Empty, string.Empty);

        serializer.Serialize(stream, value, xmlNamespaces);
    }

    private static void WriteDevice(string environment, LiveDevice device)
    {
        FileInfo file = GetDeviceFile(environment);
        if (!file.Directory.Exists)
        {
            file.Directory.Create();
        }

        using (FileStream stream = file.Open(FileMode.Create, FileAccess.Write, FileShare.None))
        {
            Serialize(stream, device);
        }
    }

    #endregion Private Methods

    #region Private Classes

    private static class LiveIdConstants
    {
        public const int DeviceNameLength = 24;
        public const int DevicePasswordLength = 24;
        public const string DevicePrefix = "11";
        public const string RegistrationEndpointUriFormat = @"https://login.live{0}.com/ppsecure/DeviceAddCredential.srf";
        public const string ValidDeviceNameCharacters = "0123456789abcdefghijklmnopqrstuvqxyz";

        //Consists of the list of characters specified in the documentation
        public const string ValidDevicePasswordCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^*()-_=+; ,./?`~";

        public static readonly string LiveDeviceFileNameFormat = Path.Combine(Path.Combine(
            Environment.ExpandEnvironmentVariables("%USERPROFILE%"), "LiveDeviceID"), "LiveDevice{0}.xml");
    }

    #endregion Private Classes
}

#region Public Classes & Enums

/// <summary>
/// Indicates an error during registration
/// </summary>
public enum DeviceRegistrationErrorCode
{
    /// <summary>
    /// Unspecified or Unknown Error occurred
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// Interface Disabled
    /// </summary>
    InterfaceDisabled = 1,

    /// <summary>
    /// Invalid Request Format
    /// </summary>
    InvalidRequestFormat = 3,

    /// <summary>
    /// Unknown Client Version
    /// </summary>
    UnknownClientVersion = 4,

    /// <summary>
    /// Blank Password
    /// </summary>
    BlankPassword = 6,

    /// <summary>
    /// Missing Device User Name or Password
    /// </summary>
    MissingDeviceUserNameOrPassword = 7,

    /// <summary>
    /// Invalid Parameter Syntax
    /// </summary>
    InvalidParameterSyntax = 8,

    /// <summary>
    /// Internal Error
    /// </summary>
    InternalError = 11,

    /// <summary>
    /// Device Already Exists
    /// </summary>
    DeviceAlreadyExists = 13
}

/// <summary>
/// Indicates that Device Registration failed
/// </summary>
[Serializable]
public sealed class DeviceRegistrationFailedException : Exception
{
    /// <summary>
    /// Construct an instance of the DeviceRegistrationFailedException class
    /// </summary>
    public DeviceRegistrationFailedException()
        : base()
    {
    }

    /// <summary>
    /// Construct an instance of the DeviceRegistrationFailedException class
    /// </summary>
    /// <param name="message">Message to pass</param>
    public DeviceRegistrationFailedException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Construct an instance of the DeviceRegistrationFailedException class
    /// </summary>
    /// <param name="message">Message to pass</param>
    /// <param name="innerException">Exception to include</param>
    public DeviceRegistrationFailedException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    /// <summary>
    /// Construct an instance of the DeviceRegistrationFailedException class
    /// </summary>
    /// <param name="code">Error code that occurred</param>
    /// <param name="subCode">Subcode that occurred</param>
    public DeviceRegistrationFailedException(DeviceRegistrationErrorCode code, string subCode)
        : this(code, subCode, null)
    {
    }

    /// <summary>
    /// Construct an instance of the DeviceRegistrationFailedException class
    /// </summary>
    /// <param name="code">Error code that occurred</param>
    /// <param name="subCode">Subcode that occurred</param>
    /// <param name="innerException">Inner exception</param>
    public DeviceRegistrationFailedException(DeviceRegistrationErrorCode code, string subCode, Exception innerException)
        : base(string.Concat(code.ToString(), ": ", subCode), innerException)
    {
    }

    /// <summary>
    /// Construct an instance of the DeviceRegistrationFailedException class
    /// </summary>
    /// <param name="si"></param>
    /// <param name="sc"></param>
    private DeviceRegistrationFailedException(SerializationInfo si, StreamingContext sc)
        : base(si, sc)
    {
    }
}

#region Serialization Classes

#region DeviceRegistrationRequest Class

[EditorBrowsable(EditorBrowsableState.Never)]
[XmlRoot("DeviceAddRequest")]
public sealed class DeviceRegistrationRequest
{
    #region Constructors

    public DeviceRegistrationRequest()
    {
    }

    public DeviceRegistrationRequest(Guid applicationId, LiveDevice device)
        : this()
    {
        if (null == device)
        {
            throw new ArgumentNullException("device");
        }

        this.ClientInfo = new DeviceRegistrationClientInfo() { ApplicationId = applicationId, Version = "1.0" };
        this.Authentication = new DeviceRegistrationAuthentication()
        {
            MemberName = device.User.DeviceId,
            Password = device.User.DecryptedPassword
        };
    }

    #endregion Constructors

    #region Properties

    [XmlElement("Authentication")]
    public DeviceRegistrationAuthentication Authentication { get; set; }

    [XmlElement("ClientInfo")]
    public DeviceRegistrationClientInfo ClientInfo { get; set; }

    #endregion Properties
}

#endregion DeviceRegistrationRequest Class

#region DeviceRegistrationClientInfo Class

[EditorBrowsable(EditorBrowsableState.Never)]
[XmlRoot("ClientInfo")]
public sealed class DeviceRegistrationClientInfo
{
    #region Properties

    [XmlAttribute("name")]
    public Guid ApplicationId { get; set; }

    [XmlAttribute("version")]
    public string Version { get; set; }

    #endregion Properties
}

#endregion DeviceRegistrationClientInfo Class

#region DeviceRegistrationAuthentication Class

[EditorBrowsable(EditorBrowsableState.Never)]
[XmlRoot("Authentication")]
public sealed class DeviceRegistrationAuthentication
{
    #region Properties

    [XmlElement("Membername")]
    public string MemberName { get; set; }

    [XmlElement("Password")]
    public string Password { get; set; }

    #endregion Properties
}

#endregion DeviceRegistrationAuthentication Class

#region DeviceRegistrationResponse Class

[EditorBrowsable(EditorBrowsableState.Never)]
[XmlRoot("DeviceAddResponse")]
public sealed class DeviceRegistrationResponse
{
    private string _errorSubCode;

    #region Properties

    [XmlElement("Error Code")]
    public string ErrorCode { get; set; }

    [XmlElement("ErrorSubcode")]
    public string ErrorSubCode
    {
        get
        {
            return this._errorSubCode;
        }

        set
        {
            this._errorSubCode = value;

            //Parse the error code
            if (string.IsNullOrWhiteSpace(value))
            {
                this.RegistrationErrorCode = null;
            }
            else
            {
                this.RegistrationErrorCode = DeviceRegistrationErrorCode.Unknown;

                //Parse the error code
                if (value.StartsWith("dc", StringComparison.Ordinal))
                {
                    int code;
                    if (int.TryParse(value.Substring(2), NumberStyles.Integer,
                        CultureInfo.InvariantCulture, out code) &&
                        Enum.IsDefined(typeof(DeviceRegistrationErrorCode), code))
                    {
                        this.RegistrationErrorCode = (DeviceRegistrationErrorCode)Enum.ToObject(
                            typeof(DeviceRegistrationErrorCode), code);
                    }
                }
            }
        }
    }

    [XmlElement("success")]
    public bool IsSuccess { get; set; }

    [XmlElement("puid")]
    public string Puid { get; set; }

    [XmlIgnore]
    public DeviceRegistrationErrorCode? RegistrationErrorCode { get; private set; }

    #endregion Properties
}

#endregion DeviceRegistrationResponse Class

#region LiveDevice Class

[EditorBrowsable(EditorBrowsableState.Never)]
[XmlRoot("Data")]
public sealed class LiveDevice
{
    #region Properties

    [XmlElement("ClockSkew")]
    public string ClockSkew { get; set; }

    [XmlElement("Expiry")]
    public string Expiry { get; set; }

    [SuppressMessage("Microsoft.Design", "CA1059:MembersShouldNotExposeCertainConcreteTypes", MessageId = "System.Xml.XmlNode", Justification = "This is required for proper XML Serialization")]
    [XmlElement("Token")]
    public XmlNode Token { get; set; }

    [XmlElement("User")]
    public DeviceUserName User { get; set; }

    [XmlAttribute("version")]
    public int Version { get; set; }

    #endregion Properties
}

#endregion LiveDevice Class

#region DeviceUserName Class

[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class DeviceUserName
{
    #region Constants

    private const string UserNamePrefix = "11";

    #endregion Constants

    #region Constructors

    public DeviceUserName()
    {
        this.UserNameType = "Logical";
    }

    #endregion Constructors

    #region Properties

    [XmlIgnore]
    public string DecryptedPassword
    {
        get
        {
            if (string.IsNullOrWhiteSpace(this.EncryptedPassword))
            {
                return this.EncryptedPassword;
            }

            byte[] decryptedBytes = Convert.FromBase64String(this.EncryptedPassword);
            ProtectedMemory.Unprotect(decryptedBytes, MemoryProtectionScope.SameLogon);

            //The array will have been padded with null characters for the memory protection to work.
            //See the setter for this property for more details
            int count = decryptedBytes.Length;
            for (int i = count - 1; i >= 0; i--)
            {
                if ('\0' == decryptedBytes[i])
                {
                    count--;
                }
                else
                {
                    break;
                }
            }
            if (count <= 0)
            {
                return null;
            }

            return Encoding.UTF8.GetString(decryptedBytes, 0, count);
        }

        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                this.EncryptedPassword = value;
                return;
            }

            byte[] encryptedBytes = Encoding.UTF8.GetBytes(value);

            //The length of the bytes needs to be a multiple of 16, or a CryptographicException will be thrown.
            //For more information, see http://msdn.microsoft.com/en-us/library/system.security.cryptography.protectedmemory.protect.aspx
            int missingCharacterCount = 16 - (encryptedBytes.Length % 16);
            if (missingCharacterCount > 0)
            {
                Array.Resize(ref encryptedBytes, encryptedBytes.Length + missingCharacterCount);
            }

            ProtectedMemory.Protect(encryptedBytes, MemoryProtectionScope.SameLogon);
            this.EncryptedPassword = Convert.ToBase64String(encryptedBytes);
        }
    }

    public string DeviceId
    {
        get
        {
            return UserNamePrefix + DeviceName;
        }
    }

    [XmlAttribute("username")]
    public string DeviceName { get; set; }

    [XmlElement("Pwd")]
    public string EncryptedPassword { get; set; }

    [XmlAttribute("type")]
    public string UserNameType { get; set; }

    #endregion Properties

    #region Methods

    public ClientCredentials ToClientCredentials()
    {
        ClientCredentials credentials = new ClientCredentials();
        credentials.UserName.UserName = this.DeviceId;
        credentials.UserName.Password = this.DecryptedPassword;

        return credentials;
    }

    #endregion Methods
}

#endregion DeviceUserName Class

#endregion Serialization Classes

#endregion Public Classes & Enums