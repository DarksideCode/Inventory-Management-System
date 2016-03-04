using System.Windows;
using System.Windows.Controls;
using InventoryManagementSystem.utilitys;

namespace InventoryManagementSystem
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ConfigProzesser config = new ConfigProzesser();
            textBox.Text = config.getDBName();
        }
    }
}
