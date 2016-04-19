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
using InventoryManagementSystem.dataAccess;
using InventoryManagementSystem.components;

namespace InventoryManagementSystem.presentation
{
    /// <summary>
    /// Interaktionslogik für EditPhysicalInterfaces.xaml
    /// </summary>
    public partial class EditPhysicalInterfaces : Window
    {
        public EditPhysicalInterfaces(List<PhysicalInterfaceWithCount> usedEntities)
        {
            InitializeComponent();
            this.FillLists(usedEntities);
        }

        private void FillLists(List<PhysicalInterfaceWithCount> usedEntities)
        {
            PhysicalInterfaceDataAccess dataAccess = new PhysicalInterfaceDataAccess();
            List<PhysicalInterface> list = dataAccess.GetAllEntities<PhysicalInterface>();

            for(int i = 0; i < list.Count; i++)
            {
                this.physicalInterfaceList.Items.Add(list[i].Name);
            }

            for(int i = 0; i < usedEntities.Count; i++)
            {
                for(int j = 0; j < usedEntities[i].Number; j++)
                {
                    this.usedList.Items.Add(usedEntities[i].PhysicalInterface.Name);
                }
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
