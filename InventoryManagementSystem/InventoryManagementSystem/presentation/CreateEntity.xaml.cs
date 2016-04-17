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
using InventoryManagementSystem.components;
using InventoryManagementSystem.dataAccess;
using InventoryManagementSystem.validation;
using InventoryManagementSystem.presentation.forms;

namespace InventoryManagementSystem.presentation
{
    /// <summary>
    /// Interaction logic for CreateEntity.xaml
    /// </summary>
    public partial class CreateEntity : Window
    {
        public CreateEntity(string Entity = null)
        {
            InitializeComponent();
            this.LoadCreatePage(Entity);

        }

        private void LoadCreatePage(string Entity)
        {
            switch (Entity)
            {
                case "Disk":
                    CreateFrame.Navigate(new CreateDisk());
                    break;
            }
        }

        private void DiskCancel_Click(object sender, RoutedEventArgs e)
        {
            // Close this window
            this.Close();
        }
    }
}
