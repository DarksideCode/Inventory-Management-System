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
            this.TBHost.Text = config.getDBHost();
            this.TBName.Text = config.getDBName();
            this.TBPraefix.Text = config.getDBPraefix();
            this.TBUser.Text = config.getDBUser();
            this.TBPassword.Text = config.getDBPassword();

            /*private InventoryManagementSystem.utilitys.ConfigProzesser otherForm;
            private void test()
            {
                 string DBName = otherForm.
            }*/

        }
    }
}
