namespace InventoryManagementSystem.utilitys
{
    /// <summary>
    /// Klasse zur Verwaltung der Konfigurationen
    /// </summary>
    public class ConfigProzesser
    {
        private string dbName = "inventory";
        private string dbUser = "root";
        private string dbPassword = "";
        private string dbPraefix = "ims_";
        private string dbHost = "localhost";

        /// <summary>
        /// Konstruktor: Ruft die Methode für das setzer der Variablen auf
        /// </summary>
        public ConfigProzesser()
        {
            this.SetConfigParams();
        }

        /// <summary>
        /// Getter/Setter für die Variable dbName
        /// </summary>
        public string DBName
        {
            get { return this.dbName; }
            set { this.dbName = value; }
        }

        /// <summary>
        /// Getter/Setter für die Variable dbUser
        /// </summary>
        public string DBUser
        {
            get { return this.dbUser; }
            set { this.dbUser = value; }
        }

        /// <summary>
        /// Getter/Setter für die Variable dbPassword
        /// </summary>
        public string DBPassword
        {
            get { return this.dbPassword; }
            set { this.dbPassword = value; }
        }

        /// <summary>
        /// Getter/Setter für die Variable dbPraefix
        /// </summary>
        public string DBPraefix
        {
            get { return this.dbPraefix; }
            set { this.dbPraefix = value; }
        }

        /// <summary>
        /// Getter/Setter für die Variable dbHost
        /// </summary>
        public string DBHost
        {
            get { return this.dbHost; }
            set { this.dbHost = value; }
        }

        /// <summary>
        /// Liest die Einstellungen aus der Konfiguration und setzt die Variablen
        /// </summary>
        private void SetConfigParams()
        {
            this.dbName = Properties.Settings.Default.DB_NAME;
            this.dbUser = Properties.Settings.Default.DB_USER;
            this.dbPassword = Properties.Settings.Default.DB_PASSWORD;
            this.dbPraefix = Properties.Settings.Default.DB_PRAEFIX;
            this.dbHost = Properties.Settings.Default.DB_HOST;
        }

        /// <summary>
        /// Speichert den Datenbank-Namen in den Konfigurationen
        /// </summary>
        /// <param name="dbName">Name der Datenbank</param>
        public void SaveDBName(string dbName)
        {
            Properties.Settings.Default.DB_NAME = dbName;
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Speichert den Datenbank-Benutzer in den Konfigurationen
        /// </summary>
        /// <param name="dbUser">Name der Benutzers</param>
        public void SaveDBUser(string dbUser)
        {
            Properties.Settings.Default.DB_USER = dbUser;
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Speichert das Passwort für den Datenbank-Benutzer in den Konfigurationen
        /// </summary>
        /// <param name="dbPassword">Passwort des Benutzers</param>
        public void SaveDBPassword(string dbPassword)
        {
            Properties.Settings.Default.DB_PASSWORD = dbPassword;
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Speichert den Tabellen-Präfix in den Konfigurationen
        /// </summary>
        /// <param name="dbPraefix">Präfix der Tabellen</param>
        public void SaveDBPraefix(string dbPraefix)
        {
            Properties.Settings.Default.DB_PRAEFIX = dbPraefix;
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Speichert den Datenbank-Host in den Konfigurationen
        /// </summary>
        /// <param name="dbHost">Host der Datenbank</param>
        public void SaveDBHost(string dbHost)
        {
            Properties.Settings.Default.DB_HOST = dbHost;
            Properties.Settings.Default.Save();
        }
    }
}