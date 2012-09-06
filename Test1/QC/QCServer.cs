using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SACLIENTLib;

namespace Test1.QC
{
    public class QCServer
    {
        private string url;
        private string admin;
        private string adminPassword;
        private SAapi saConnection;
        private Dictionary<string, QCDataserver> dataServers = new Dictionary<string, QCDataserver>();


        private int projectCount = 0;
        private int domainCount = 0;
        private const string QCPROJECT_PREFIX = "AutoGenerate_Project";
        private const string DOMAIN_PREFIX = "AUTODOMAIN_";
        private const string defaultDomain = "DEFAULT";

        public string Url
        {
            get { return url; }
        }
        

        #region construct
        internal QCServer(string serverUrl, string admin, string adminPassword) 
        {
            this.url = serverUrl;
            this.admin = admin;
            this.adminPassword = adminPassword;
        }
        #endregion construct

        private string getNewProjectName()
        {
            projectCount++;
            return QCPROJECT_PREFIX + projectCount;
        }

        private string getNewDomainName()
        {
            domainCount++;
            return DOMAIN_PREFIX + domainCount;
        }

        public void connect()
        {
            disconnect();           
            saConnection = new SAapi();
            saConnection.Login(url, admin, adminPassword);           
        }

        public void disconnect()
        {
            if (saConnection != null)
            {
                saConnection.Logout();
            }
        }



        internal void addDBServer(QCDataserver dataServer)
        {
            dataServers.Add(dataServer.DbServerName,dataServer);
        }

        //get one dbserver with specified db type
        //if dbType == any db, return the first dataserver
        internal QCDataserver getDBServer(int dbType)
        {
            if (dbType == QCDataserver.ANY_DB)
            {
                return dataServers.First().Value;
            }

            foreach (QCDataserver dataServer in dataServers.Values)
            {
                if (dataServer.DbType == dbType)
                {
                    return dataServer;
                }
            }
            return null;
        }

        //todo 
        //get all db servers internal 

        #region createProject
        //full parameter
        internal Project createProject(QCDataserver dataServer,string domainName,string projectName)
        {
            if (dataServer == null) dataServer = getDBServer(QCDataserver.ANY_DB);
            if (domainName == "") domainName = defaultDomain;
            if (projectName == "") projectName = getNewProjectName();
            
            int spaceSize = 0;
            int tempSize = 0;
            int creationOption = 1;
            try
            {
                saConnection.CreateProject(domainName, projectName, dataServer.DbType, dataServer.DbServerName, dataServer.AdminUser,
                                                                  dataServer.AdminPassword, dataServer.TableSpace, dataServer.TempSpace, spaceSize,
                                                                  tempSize, creationOption);
                return new Project(this.Url, projectName, domainName, dataServer.AdminUser, dataServer.AdminPassword);
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return null;
            }
        }

        //create project according specific db type
        internal Project createProject(int dbType,string domainName, string projectName)
        {           
            QCDataserver dataServer = getDBServer(dbType);
            if (dataServer == null) return null;
            return createProject(dataServer, domainName, projectName);
            
        }

        internal Project createProject(int dbType)
        {
            return createProject(dbType, defaultDomain, getNewProjectName());
        }

        internal Project createProject()
        {            
            return createProject(QCDataserver.ANY_DB);
        }

        #endregion createProject

        internal bool deleteProject(Project p)
        {
            string result = saConnection.DeleteProject(p.Domain, p.Name, p.DbUser, p.DbPassword);
            if (int.Parse(result) == 1) return true;
            return false;
        }

        #region create domain
        internal string createDomain(string domainName)
        {
            saConnection.CreateDomain(domainName, "", "", -1);
            return domainName;
        }

        internal string createDomain()
        {
            string domain = getNewDomainName();
            return createDomain(domain);
        }
        #endregion create domain

        internal bool deleteDomain(string domainName)
        {
           string result = saConnection.DeleteDomain(domainName);
            if (int.Parse(result) == 1) return true;
            return false;

        }
    }
}
