namespace McTools.Xrm.Connection.Console
{
    internal class Program
    {
        private ConnectionManager connectionManager;

        public void Run()
        {
            var cd = new ConnectionDetail()
            {
                UseIfd = RequestInformation("Use IFD") == "y",
                UserDomain = RequestInformation("Domain"),
                UserName = RequestInformation("Username"),
                UseSsl = RequestInformation("Use SSL") == "y",
                OrganizationServiceUrl = RequestInformation("Organization service url")
            };
            cd.SetPassword(RequestInformation("Password"));

            connectionManager = ConnectionManager.Instance;
            connectionManager.ConnectionSucceed += new ConnectionManager.ConnectionSucceedEventHandler(connectionManager_ConnectionSucceed);
            connectionManager.ConnectionFailed += new ConnectionManager.ConnectionFailedEventHandler(connectionManager_ConnectionFailed);
            connectionManager.RequestPassword += new ConnectionManager.RequestPasswordEventHandler(connectionManager_RequestPassword);
            connectionManager.UseProxy += new ConnectionManager.UseProxyEventHandler(connectionManager_UseProxy);
            connectionManager.StepChanged += new ConnectionManager.StepChangedEventHandler(connectionManager_StepChanged);
            connectionManager.ConnectToServer(cd);

            System.Console.ReadLine();
        }

        private static void Main(string[] args)
        {
            new Program().Run();
        }

        private void connectionManager_ConnectionFailed(object sender, ConnectionFailedEventArgs e)
        {
            System.Console.WriteLine("Failed to connect: " + e.FailureReason);
        }

        private void connectionManager_ConnectionSucceed(object sender, ConnectionSucceedEventArgs e)
        {
            System.Console.WriteLine("Successfully connected");
        }

        private bool connectionManager_RequestPassword(object sender, RequestPasswordEventArgs e)
        {
            System.Console.WriteLine("Please enter your password: ");
            e.ConnectionDetail.SetPassword(System.Console.ReadLine());
            return false;
        }

        private void connectionManager_StepChanged(object sender, StepChangedEventArgs e)
        {
            System.Console.WriteLine(e.CurrentStep);
        }

        private void connectionManager_UseProxy(object sender, UseProxyEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private string RequestInformation(string name)
        {
            System.Console.WriteLine("Please provide your " + name + "?:");
            return System.Console.ReadLine();
        }
    }
}