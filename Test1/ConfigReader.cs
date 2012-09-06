using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Test1
{
    class ConfigReader
    {
        private static Dictionary<string, List<Dictionary<string ,string>>> configs;
        private const string QC_SERVER="qc_server";
        private const string DB_SERVER="db_server";
        private const string DETAILS = "details";

        private const string QC_SERVER_NAME = "tag";
        private const string QC_SERVER_URL = "server";
        private const string QC_SERVER_ADMIN = "server.admin";
        private const string QC_SERVER_ADMINPASSWORD = "server.admin.password";

        private const string DB_SERVER_NAME = "dbserver";
        private const string DB_SERVER_ADMIN = "dbserver.admin";
        private const string DB_SERVER_ADMINPASSWORD = "dbserver.admin.password";
        private const string DB_SERVER_SPACE = "dbserver.space";
        private const string DB_SERVER_TEMPSPACE = "dbserver.tempspace";
        private const string DB_SERVER_HOST_QC = "tag";

        //the string is used in config file as tag
        private const string TAG = " -tag ";
       
        private static void load(string config_file)
        {
            configs = new Dictionary<string, List<Dictionary<string, string>>>();
            List<Dictionary<string, string>> servers = new List<Dictionary<string, string>>();
            List<Dictionary<string, string>> dbServers = new List<Dictionary<string, string>>();
            try
            {
                using (StreamReader sr = new StreamReader(config_file))
                {
                    String line;
                    Dictionary<string, string> server = new Dictionary<string, string>(); 
                    Dictionary<string, string> dbServer = new Dictionary<string, string>(); 
                    string server_tag = "";

                    // Read all lines from the file until the end of 
                    // the file is reached.                    
                    while ((line = sr.ReadLine()) != null)
                    {
                        //skip comment line and line without "="
                        line = line.Trim();                        
                        if (line.StartsWith("#")) continue;                        
                        if (!line.Contains("=")) continue;

                        int index = line.IndexOf("=");
                        string key = line.Substring(0, index).Trim();
                        string value = line.Substring(index+1,line.Length-index-1).Trim();

                        //url
                        if (key.Equals(QC_SERVER_URL))
                        {
                            //add info to servers/dbservers
                            if (server.Count != 0)
                            {
                                servers.Add(server);
                                server = new Dictionary<string, string>();
                            }

                            if (dbServer.Count != 0)
                            {
                                dbServers.Add(dbServer);
                                dbServer = new Dictionary<string, string>();
                            }

                            //tag
                            server_tag = "";
                            int tagIndex = value.IndexOf(TAG);
                            if (tagIndex > 0)
                            {
                                server_tag = value.Substring(tagIndex + TAG.Length);
                                value = value.Substring(0,tagIndex).Trim();
                            }
                            server.Add(QC_SERVER_NAME, server_tag);

                            //url
                            server.Add(QC_SERVER_URL, value);
                            continue;
                        }

                        //qc admin
                        if (key.Equals(QC_SERVER_ADMIN))
                        {
                            server.Add(QC_SERVER_ADMIN, value);
                            continue;
                        }

                        //qc admin pwd
                        if (key.Equals(QC_SERVER_ADMINPASSWORD))
                        {
                            server.Add(QC_SERVER_ADMINPASSWORD, value);
                            continue;
                        }

                        //db name
                        if (key.Equals(DB_SERVER_NAME))
                        {
                            //add info to servers/dbservers
                            if (server.Count != 0)
                            {
                                servers.Add(server);
                                server = new Dictionary<string, string>();
                            }

                            if (dbServer.Count != 0)
                            {
                                dbServers.Add(dbServer);
                                dbServer = new Dictionary<string, string>();
                            }
                            
                            dbServer.Add(DB_SERVER_NAME, value);
                            dbServer.Add(DB_SERVER_HOST_QC,server_tag);
                            continue;
                        }

                        //db admin
                        if (key.Equals(DB_SERVER_ADMIN))
                        {
                            dbServer.Add(DB_SERVER_ADMIN, value);
                            continue;
                        }

                        //db admin pwd
                        if (key.Equals(DB_SERVER_ADMINPASSWORD))
                        {
                            dbServer.Add(DB_SERVER_ADMINPASSWORD, value);
                            continue;
                        }

                        //db tablespace
                        if (key.Equals(DB_SERVER_SPACE))
                        {
                            dbServer.Add(DB_SERVER_SPACE, value);
                            continue;
                        }

                        //db tempspace
                        if (key.Equals(DB_SERVER_TEMPSPACE))
                        {
                            dbServer.Add(DB_SERVER_TEMPSPACE, value);
                            continue;
                        }
                    }
                }
                configs.Add(QC_SERVER, servers);
                configs.Add(DB_SERVER, dbServers);                
            }
            catch (Exception e)
            {
                Console.WriteLine("File does not exist:" + config_file);
                Console.WriteLine(e.StackTrace);
                return;
            }

            readDetails(config_file);
        }

        //Read running details from config file
        private static void readDetails(string config_file)
        {   
            //only one element in this list
            List<Dictionary<string, string>> details_l = new List<Dictionary<string, string>>();
            Dictionary<string, string> details = new Dictionary<string, string>();
            
            try
            {
                using (StreamReader sr = new StreamReader(config_file))
                {
                    String line;

                    // Read all lines from the file until the end of 
                    // the file is reached.                    
                    while ((line = sr.ReadLine()) != null)
                    {
                        //skip comment line and line without "="
                        line = line.Trim();
                        if (line.StartsWith("#")) continue;
                        if (!line.Contains("=")) continue;
                        if (line.StartsWith(QC_SERVER_URL) || line.StartsWith(DB_SERVER_NAME)) continue;

                        int index = line.IndexOf("=");
                        string key = line.Substring(0, index).Trim();
                        string value = line.Substring(index + 1, line.Length - index - 1).Trim();

                        details.Add(key, value);
                    }
                }
                details_l.Add(details);
                configs.Add(DETAILS,details_l);
            }
            catch (Exception e)
            {
                Console.WriteLine("File does not exist:" + config_file);
                Console.WriteLine(e.StackTrace);
                return;
            }
            
        }
        
        
        private static Dictionary<string, List<Dictionary<string, string>>> get_config()
        {
            if (configs == null)
            {
                //load();
            }
            return configs;
        }
        
    }
}
