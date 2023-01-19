using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace XrmToolBox.ToolLibrary.AppCode
{
    public class SerializerHelper
    {
        public static T ReadObject<T>(Stream content)
        {
            var serializer = new DataContractJsonSerializer(typeof(T), new DataContractJsonSerializerSettings
            {
                UseSimpleDictionaryFormat = true
            });

            return (T)serializer.ReadObject(content);
        }

        public static T ReadObject<T>(Stream content, DateTimeFormat dtf)
        {
            var settings = new DataContractJsonSerializerSettings
            {
                UseSimpleDictionaryFormat = true,
            };

            if (dtf != null)
            {
                settings.DateTimeFormat = dtf;
            }

            var serializer = new DataContractJsonSerializer(typeof(T), settings);

            return (T)serializer.ReadObject(content);
        }

        public static T ReadObject<T>(string content, DateTimeFormat dtf = null)
        {
            var settings = new DataContractJsonSerializerSettings
            {
                UseSimpleDictionaryFormat = true,
            };

            if (dtf != null)
            {
                settings.DateTimeFormat = dtf;
            }

            var serializer = new DataContractJsonSerializer(typeof(T), settings);

            return (T)serializer.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(content)));
        }

        public static string WriteObject<T>(T content, Encoding encoding, DateTimeFormat dtf = null)
        {
            var settings = new DataContractJsonSerializerSettings
            {
                UseSimpleDictionaryFormat = true,
            };

            if (dtf != null)
            {
                settings.DateTimeFormat = dtf;
            }

            var serializer = new DataContractJsonSerializer(typeof(T), settings);

            MemoryStream stream = new MemoryStream();
            serializer.WriteObject(stream, content);

            return encoding.GetString(stream.ToArray());
        }
    }
}