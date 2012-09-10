using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace Test1.QC
{
    public class Resource
    {
        private const string defaultServerName = "server";
        
        private static Dictionary<string, QCServer> qcServers = new Dictionary<string,QCServer> ();
        private static Dictionary<string, QCDataserver> qcDataServers = new Dictionary<string, QCDataserver>();
        private static List<Project> projects = new List<Project>();
        private static List<string> domains = new List<string>(); 

        internal static void load()
        {   
            //get qc servers            
            string serverName = defaultServerName;
            string url = "http://192.168.34.15:8080/qcbin";
            string username = "sa";
            string password = "";  
            QCServer qcServer = new QCServer(url, username, password);
            qcServers.Add(serverName, qcServer);
            
            //get db servers                        
            string dbServer = "192.168.34.15\\QA";
            string admin = "sa";
            string pwd = "!qaz2wsx";
            QCDataserver qcDataserver1 = new QCDataserver(dbServer, admin, pwd);
            qcServer.addDBServer(qcDataserver1);
            qcDataServers.Add(dbServer,qcDataserver1);

            
            //int type = 3;
            //dbServer = "apps002";
            //admin = "system";
            //pwd = "!qaz2wsx";
            //string space = "ALMTABLESPACE1";
            //string tempSpace = "TEMP";
            //QCDataserver qcDataserver2 = new QCDataserver(type, dbServer, admin, pwd, space, tempSpace);
            //qcServer.addDBServer(qcDataserver2);
            //qcDataServers.Add(dbServer, qcDataserver2);

            //SA login
            foreach (QCServer server in qcServers.Values)
            {
                server.connect();
            }
        }

        //Todo
        internal static Boolean isResourceCreated()
        {
            if (projects.Count == 0 && domains.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        internal static void finishLoad()
        {
            //SA login
            foreach (QCServer server in qcServers.Values)
            {
                server.disconnect();
            }
        }

        internal static void release()
        { 
            //release all resources here, delete all projects, domains, dbs
            //release projects
            foreach (QCServer server in qcServers.Values)
            {
                server.connect();
                try
                {
                    foreach (Project p in projects)
                    {
                        server.deleteProject(p);
                    }

                    foreach (string domain in domains)
                    {
                        server.deleteDomain(domain);
                    }
                    server.disconnect();
                }
                catch (Exception e)
                {

                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.InnerException);
                }
                finally
                {
                    server.disconnect();
                }
            }
        }
        

        public static QCServer getServer(string serverName)
        {
            return Resource.qcServers[serverName];            
        }

        public static QCDataserver getDBServer(string dataServerName)
        {
            return qcDataServers[dataServerName];
        }

        #region createDomain
        public static string createDomain()
        {
            string domain = qcServers.First().Value.createDomain();
            domains.Add(domain);
            return domain;
        }

        public static string createDomain(QCServer qcServer, string domainName)
        {
            string domain = qcServer.createDomain(domainName);
            domains.Add(domain);
            return domain;
        }

        public static string createDomain(string domainName)
        {
            return createDomain(qcServers.First().Value, domainName);
        }

        #endregion createDomain


        #region createProject
        public static Project createProject(QCServer qcServer, QCDataserver dbServer, string domainName, string projectName)
        {
            Project p = qcServer.createProject(dbServer, domainName, projectName);
            projects.Add(p);
            return p;
        }

        public static Project createProject(QCServer qcServer, int dbType, string domainName, string projectName)
        {
            QCDataserver dbServer = qcServer.getDBServer(dbType);
            return createProject(qcServer, dbServer, domainName,projectName);
        }

        public static Project createProject(string domainName)
        {
            return createProject(qcServers.First().Value, QCDataserver.ANY_DB, domainName, "");
        }

        public static Project createProject(QCServer qcServer, int dbType)
        {
            QCDataserver dbServer = qcServer.getDBServer(dbType);
            return createProject(qcServer, dbServer, "", "");
        }

        public static Project createProject(QCServer qcServer)
        {               
            return createProject(qcServer,QCDataserver.ANY_DB);
        }

        public static Project createProject()
        {
            return createProject(qcServers.First().Value);
        }

        #endregion createProject
    }
}
