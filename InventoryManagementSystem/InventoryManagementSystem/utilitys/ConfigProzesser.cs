using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace InventoryManagementSystem.utilitys
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
        * string contains DB Präfix
        **/
        private string DBPräfix = "ims_";



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
            DBPräfix = getDBPräfix();
        }

        static void ReadAllSettings()
        {
            //SAVE Configs
            Properties.Settings.Default.Save();
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
        * Returns the DB User from Config
        * return string
        **/
        public string getDBUser()
        {
            return Properties.Settings.Default.DB_USER;
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
        * Returns the DB Präfix from Config
        * return string
        **/
        public string getDBPräfix()
        {
            return Properties.Settings.Default.DB_PRÄFIX;
        }
    }
}