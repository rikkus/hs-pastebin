namespace HS.SqlServer
{
    public class ConnectionInfo
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ServerName { get; set; }
        public string InstanceName { get; set; }
        public string DatabaseName { get; set; }
        public bool UseWindowsAuth { get; set; }

        public ConnectionInfo(string serverName, string instanceName)
        {
            ServerName = serverName;
            InstanceName = instanceName;
        }

        public ConnectionInfo(string serverName, string instanceName, string databaseName)
        {
            ServerName = serverName;
            InstanceName = instanceName;
            DatabaseName = databaseName;
            UseWindowsAuth = true;
        }

        public ConnectionInfo
            (
            string serverName,
            string instanceName,
            string userName,
            string password,
            string databaseName
            )
        {
            ServerName = serverName;
            InstanceName = instanceName;
            UserName = userName;
            Password = password;
            DatabaseName = databaseName;
        }

        public string DatabaseConnectionString
        {
            get
            {
                return string.Format
                    (
                    "{0};{1};{2};",
                    DataSourceString,
                    DatabaseNameString,
                    InstanceAuthString
                    );
            }
        }

        public string InstanceConnectionString
        {
            get
            {
                return string.Format
                    (
                    "{0};{1};",
                    DataSourceString,
                    InstanceAuthString
                    );
            }
        }

        private string DatabaseNameString
        {
            get { return "Database=" + DatabaseName; }
        }

        private string DataSourceString
        {
            get
            {
                return "Server=" + (string.IsNullOrEmpty(InstanceName)
                                        ? ServerName
                                        : ServerName + '\\' + InstanceName);
            }
        }

        private string InstanceAuthString
        {
            get
            {
                return UseWindowsAuth
                           ? "Trusted_Connection=True"
                           : string.Format("User ID={0};Password={1}", UserName, Password);
            }
        }
    }
}