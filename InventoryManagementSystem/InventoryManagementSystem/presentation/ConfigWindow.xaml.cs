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
    /// </summary>
    public partial class ConfigWindow : Window
    {
        public ConfigWindow()
        {
            InitializeComponent();
            ConfigProzesser config = new ConfigProzesser();

            string DBName = config.PDBName;
            string DBPraefix = config.PDBPraefix;
            string DBHost = config.PDBHost;
            string DBUser = config.PDBUser;
            string DBPassword = config.PDBPassword;

            this.TBName.Text = DBName;
            this.TBPraefix.Text = DBPraefix;
            this.TBHost.Text = DBHost;
            this.TBUser.Text = DBUser;
            this.TBPassword.Password = DBPassword;
        }

        protected void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            string DBName = TBName.Text;
            string DBPraefix = TBPraefix.Text;
            string DBHost = TBHost.Text;
            string DBUser = TBUser.Text;
            string DBPassword = TBPassword.Password;

            ConfigProzesser config = new ConfigProzesser();
            config.saveDBName(DBName);
            config.saveDBPraefix(DBPraefix);
            config.saveDBHost(DBHost);
            config.saveDBUser(DBUser);
            config.saveDBPassword(DBPassword);

            this.Close();
        }
    }
}
