

namespace UI
{
    public class DBActions
    {
        static string connString;
        IConfiguration config;

        public DBActions(IConfiguration config)
        {
            this.config = config;
        }
        public string GetConnectionString(string connStrNameInCnfig)
        {
            if (connString != null)
            {
                return connString;
            }
            string connStr = config.GetConnectionString(connStrNameInCnfig);
            connStr = ReplaceWithCurrentLocation(connStr);
            return connStr;
        }

        private string ReplaceWithCurrentLocation(string connStr)
        {
            string str = AppDomain.CurrentDomain.BaseDirectory;
            string directryAboveBin = str.Substring(0, str.IndexOf("\\bin"));
            string twoDirectoriesAboveBin = directryAboveBin.Substring(0, directryAboveBin.LastIndexOf("\\"));
            connStr = string.Format(connStr, twoDirectoriesAboveBin);
            return connStr;
        }
    }
}
