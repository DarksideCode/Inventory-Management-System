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
    /// Ermöglicht die Verwaltung von Schnittstellen einer beliebigen Entität. 
    /// </summary>
    public partial class EditPhysicalInterfaces : Window
    {
        public List<PhysicalInterfaceWithCount> list;

        public EditPhysicalInterfaces(List<PhysicalInterfaceWithCount> usedEntities)
        {
            InitializeComponent();
            this.FillLists(usedEntities);
            list = usedEntities;
        }

        /// <summary>
        /// Füllt die Liste der verwendeten und nicht verwendeten Schnittstellen auf, damit diese auf
        /// der Oberfläche zu sehen sind.
        /// </summary>
        /// <param name="usedEntities"></param>
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

        /// <summary>
        /// Wird aufgerufen, wenn auf den Button mit dem '+' geklickt wird.
        /// Fügt das selektierte Item zur Liste der verwendeten Schnittstellen hinzu. 
        /// </summary>
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if(this.physicalInterfaceList.SelectedItems.Count > 0)
            {
                this.usedList.Items.Add(this.physicalInterfaceList.SelectedItems[0]);
                this.physicalInterfaceList.UnselectAll();
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn auf den Button mit dem '-' geklickt wird.
        /// Entfernt die selektierte Schnittstelle aus der Liste der verwendeten Schnittstellen.
        /// </summary>
        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            if(this.usedList.SelectedItems.Count > 0)
            {
                this.usedList.Items.Remove(this.usedList.SelectedItems[0]);
                this.usedList.UnselectAll();
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn auf den Speichern-Button geklickt wird.
        /// Speichert die aktuelle Liste und schließt das Fenster.
        /// </summary>
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            List<PhysicalInterfaceWithCount> list = this.BuildList();
            this.Close();
        }

        /// <summary>
        /// Generiert eine Liste von 'PhysicalInterfaceWithCount' aus der Tabelle der
        /// verwendeten Schnittstellen. Es werden die Schnittstellen und Anzahl der Vorkommen
        /// ermittelt und in die Liste geschrieben.
        /// </summary>
        /// <returns>Liste von PhysicalInterfaceWithCount</returns>
        private List<PhysicalInterfaceWithCount> BuildList()
        {
            PhysicalInterfaceDataAccess dataAccess = new PhysicalInterfaceDataAccess();
            this.list = new List<PhysicalInterfaceWithCount>();

            foreach (String interfaceName in this.usedList.Items)
            {
                bool foundEntity = false;
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].PhysicalInterface.Name.Equals(interfaceName))
                    {
                        list[i].Number = list[i].Number + 1;
                        foundEntity = true;
                    }
                }
                if (!foundEntity)
                {
                    PhysicalInterface physicalInterface = dataAccess.GetByName(interfaceName);
                    list.Add(new PhysicalInterfaceWithCount(physicalInterface, 1));
                }
            }

            return list;
        }

        /// <summary>
        /// Wird aufgerufen, wenn auf den Abbrechen-Button geklickt wird.
        /// Schließt den aktuellen Dialog, ohne die Schnittstellen zu speichern.
        /// </summary>
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
