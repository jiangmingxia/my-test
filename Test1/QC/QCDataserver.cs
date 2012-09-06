using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test1.QC
{
    public class QCDataserver
    {
        public const int ORACLE_DB = 3;
        public const int SQLSERVER_DB = 2;
        public const int ANY_DB = -1;

        private int dbType;
        private string dbServerName;
        private string adminUser;
        private string adminPassword;
        private string[] tableSpaces;
        private string[] tempSpaces;

        #region construct
        internal QCDataserver(int databaseType, string databaseServerName, string userName, string password,string[] tbSpaces,string[] tpSpaces) 
        {
            this.dbType = databaseType;
            this.dbServerName = databaseServerName;
            this.adminUser = userName;
            this.adminPassword = password;

            if (databaseType == ORACLE_DB)
            {
                this.tableSpaces = tbSpaces;
                this.tempSpaces = tpSpaces;
            }

            if (databaseType == SQLSERVER_DB)
            {
                this.tableSpaces = new string[1];
                tableSpaces[0] = "";
                this.tempSpaces = new string[1];
                tempSpaces[0] = "";
            }
        }

        internal QCDataserver(int databaseType, string databaseServerName, string userName, string password, string tbSpace, string tpSpace)
        {
            this.dbType = databaseType;
            this.dbServerName = databaseServerName;
            this.adminUser = userName;
            this.adminPassword = password;
            this.tableSpaces = new string[1];
            tableSpaces[0] = "";
            this.tempSpaces = new string[1];
            tempSpaces[0] = "";

            if (databaseType == ORACLE_DB)
            {
                this.tableSpaces[0] = tbSpace;
                this.tempSpaces[0] = tpSpace;
            }
        }

        internal QCDataserver(string databaseServerName, string userName, string password)
        {
            this.dbType = SQLSERVER_DB;
            this.dbServerName = databaseServerName;
            this.adminUser = userName;
            this.adminPassword = password;
            this.tableSpaces = new string[1];
            tableSpaces[0] = "";
            this.tempSpaces = new string[1];
            tempSpaces[0] = "";
        }
        #endregion construct

        #region internal properties

        internal int DbType
        {
            get { return dbType; }
        }

        internal string DbServerName
        {
            get { return dbServerName; }
        }

        internal string AdminUser
        {
            get { return adminUser; }
        }

        internal string AdminPassword
        {
            get { return adminPassword; }
        }

        internal string TableSpace
        {
            get { return tableSpaces[0]; }
        }

        internal string TempSpace
        {
            get { return tempSpaces[0]; }
        }

        #endregion internal properties

    }
}
