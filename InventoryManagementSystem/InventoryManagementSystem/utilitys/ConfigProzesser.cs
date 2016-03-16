﻿namespace InventoryManagementSystem.utilitys
{
    public class ConfigProzesser
    {
        /**
        * string contains DB Name
        **/
        private string DBName = "inventory";

        /**
        * string contains DB User
        **/
        private string DBUser = "root";

        /**
        * string contains DB Password
        **/
        private string DBPassword;

        /**
        * string contains DB Praefix
        **/
        private string DBPraefix = "ims_";

        /**
        * string contains DB Host
        **/
        private string DBHost = "localhost";

        public ConfigProzesser()
        {
            setConfigParams();
            //ReadAllSettings();
        }

        private void setConfigParams()
        {
            DBName = getDBName();
            DBUser = getDBUser();
            DBPassword = getDBPassword();
            DBPraefix = getDBPraefix();
            DBHost = getDBHost();
        }

        static void ReadAllSettings()
        {
            //SAVE Configs
            //Properties.Settings.Default.Save();
            //@todo do it
        }

        /**
        * Returns the DB Name from Config
        * return string
        **/
        public string getDBName()
        {
            return Properties.Settings.Default.DB_NAME;
        }

        /**
        * Saves the DB Name in Config
        * @sDBName = string
        **/
        public void saveDBName(string sDBName)
        {
            Properties.Settings.Default.DB_NAME = sDBName;

            Properties.Settings.Default.Save();
        }

        /**
        * Returns the DB User from Config
        * return string
        **/
        public string getDBUser()
        {
            return Properties.Settings.Default.DB_USER;
        }

        /**
        * Saves the DB User in Config
        * @sDBUser = string
        **/
        public void saveDBUser(string sDBUser)
        {
            Properties.Settings.Default.DB_USER = sDBUser;

            Properties.Settings.Default.Save();
        }

        /**
        * Returns the DB Password from Config
        * return string
        **/
        public string getDBPassword()
        {
            return Properties.Settings.Default.DB_PASSWORD;
        }

        /**
        * Saves the DB User in Config
        * @sDBPassword = string
        **/
        public void saveDBPassword(string sDBPassword)
        {
            Properties.Settings.Default.DB_PASSWORD = sDBPassword;

            Properties.Settings.Default.Save();
        }

        /**
        * Returns the DB Praefix from Config
        * return string
        **/
        public string getDBPraefix()
        {
            return Properties.Settings.Default.DB_PRAEFIX;
        }

        /**
        * Saves the DB User in Config
        * @sDBPraefix = string
        **/
        public void saveDBPraefix(string sDBPraefix)
        {
            Properties.Settings.Default.DB_PRAEFIX = sDBPraefix;

            Properties.Settings.Default.Save();
        }

        /**
        * Returns the DB Host from Config
        * return string
        **/
        public string getDBHost()
        {
            return Properties.Settings.Default.DB_HOST;
        }

        /**
        * Saves the DB Host in Config
        * @sDBHost = string
        **/
        public void saveDBHost(string sDBHost)
        {
            Properties.Settings.Default.DB_HOST = sDBHost;

            Properties.Settings.Default.Save();
        }
    }
}