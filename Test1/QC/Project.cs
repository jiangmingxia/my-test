using System;

namespace Test1.QC
{
    public class Project
    {
        private string name;
        private string domain;
        private string serverUrl;

        private string dbUser;
        private string dbPassword;

        internal Project(string url, string projectName, string projectDomain, string dbUserName, string password) 
        {
            name = projectName;
            domain = projectDomain;
            serverUrl = url;
            dbUser = dbUserName;
            dbPassword = password;
        }

        #region properties

        internal string Name
        {
            get { return name; }
        }

        internal string Domain
        {
            get { return domain; }
        }

        internal string ServerUrl
        {
            get { return serverUrl; }
        }

        internal string DbUser
        {
            get { return dbUser; }
        }

        internal string DbPassword
        {
            get { return dbPassword; }
        }

        #endregion properties
    }
}
