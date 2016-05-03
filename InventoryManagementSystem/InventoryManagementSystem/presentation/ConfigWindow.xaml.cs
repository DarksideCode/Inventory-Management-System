using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using InventoryManagementSystem.utilitys;

namespace InventoryManagementSystem.presentation
{
    /// <summary>
    /// Interaktionslogik für ConfigWindow.xaml
    /// Ermöglicht das Konfigurieren der Datenbankverbindung auf Client-Seite.
    /// </summary>
    public partial class ConfigWindow : Window
    {
        private ConfigProzesser config;

        /// <summary>
        /// Konstruktor: Initialisiert die Elemente.
        /// </summary>
        public ConfigWindow()
        {
            InitializeComponent();
            config = new ConfigProzesser();
            this.FillTextFields();
        }

        /// <summary>
        /// Setzt die Werte der Text-Felder mit den Informationen aus
        /// der Konfiguration
        /// </summary>
        private void FillTextFields()
        {
            this.TBName.Text = config.DBName;
            this.TBPraefix.Text = config.DBPraefix;
            this.TBHost.Text = config.DBHost;
            this.TBUser.Text = config.DBUser;
            this.TBPassword.Password = config.DBPassword;
        }

        /// <summary>
        /// Wird aufgerufen, wenn der Speichern-Button geklickt wurde.
        /// Setzt die Informationen aus der Konfiguration auf die eingegebenen
        /// Werte und schließt das Fenster.
        /// </summary>
        protected void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            config.SaveDBName(this.TBName.Text);
            config.SaveDBPraefix(this.TBPraefix.Text);
            config.SaveDBHost(this.TBHost.Text);
            config.SaveDBUser(this.TBUser.Text);
            config.SaveDBPassword(this.TBPassword.Password);

            this.Close();
        }
    }
}
