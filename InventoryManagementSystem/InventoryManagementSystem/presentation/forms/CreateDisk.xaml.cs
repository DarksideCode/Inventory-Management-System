using System;
using System.Collections.Generic;
using System.Windows;
using InventoryManagementSystem.dataAccess;
using InventoryManagementSystem.components;
using InventoryManagementSystem.validation;
using InventoryManagementSystem.control;

namespace InventoryManagementSystem.presentation.forms
{
    /// <summary>
    /// Interaction logic for CreateDisk.xaml
    /// </summary>
    public partial class CreateDisk : Window
    {
        private Disk entity;
        private bool isAvailable;

        /// <summary>
        /// Konstruktor: Setzt die Wert für die Initialisierung des Dialoges
        /// </summary>
        /// <param name="entity">Objekt einer Festplatte</param>
        public CreateDisk(Disk entity = null)
        {
            InitializeComponent();
            this.DiskCapacityUnit.SelectedIndex = 0;
            this.GetProducers();
            this.entity = entity;
            
            if (entity != null)
            {
                this.SetAllFields();
                this.entity = entity;
                this.isAvailable = true;
            }
            else
            {
                this.entity = new Disk();
                this.isAvailable = false;
            }
        }

        /// <summary>
        /// Setzt die Schnittstellen der aktuellen Entität
        /// Wird von EditPhysicalInterfaces aufgerufen
        /// </summary>
        /// <param name="physicalInterfaces">Liste von PhysicalInterfaceWithCount</param>
        public void SetPhysicalInterfaces(List<PhysicalInterfaceWithCount> physicalInterfaces)
        {
            this.entity.PhysicalInterfaces = physicalInterfaces;
        }

        /// <summary>
        /// Setzt die Werte der UI-Elemente, wenn eine Entität bearbeitet wird
        /// </summary>
        private void SetAllFields()
        {
            this.DiskDescription.Text = this.entity.Description;
            this.DiskCapacity.Text = this.entity.Capacity.ToString();
            this.DiskSize.Text = this.entity.Inch.ToString();
            this.DiskType.IsChecked = this.entity.Ssd;
            this.DiskProducer.SelectedItem = this.entity.Producer.CompanyName;
        }

        /// <summary>
        /// Schließt das aktuelle Fenster
        /// </summary>
        private void DiskCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Fügt alle Hersteller aus der Datenbank dem Drop-Down-Menü hinzu
        /// </summary>
        private void GetProducers()
        {
            ProducerDataAccess dataProducers = new ProducerDataAccess();
            List<Producer> producers = dataProducers.GetAllEntities<Producer>();

            foreach (Producer element in producers)
            {
                DiskProducer.Items.Add(element.CompanyName.ToString());
            }
        }

        /// <summary>
        /// Öffnet eine MessageBox mit der übergebenen Fehlermeldung.
        /// </summary>
        /// <param name="exception">Die Exception, welche ausgelöst wurde</param>
        /// <param name="message">Die Fehlermeldung, welche angezeigt wird</param>
        private void showErrorMessage(Exception exception, string message)
        {
            MessageBox.Show(message, exception.GetType().Name, MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        /// <summary>
        /// Ruft die Informationen aus dem Formular ab und speichert sie in die Datenbank.
        /// Führt eine Umrechnung in MB aus und wirft eine Fehlermeldung, wenn die Validierung
        /// fehlschlägt.
        /// </summary>
        private void DiskSave_Click(object sender, RoutedEventArgs e)
        {
            ProducerDataAccess dataProducer = new ProducerDataAccess();
            DiskDataAccess diskDataAccess = new DiskDataAccess();
            DiskValidator validator = new DiskValidator();

            try {
                this.entity.Description = this.DiskDescription.Text;
                if (this.DiskCapacityUnit.Text == "MB")
                {
                    this.entity.Capacity = Convert.ToUInt32(this.DiskCapacity.Text);
                }
                else if (this.DiskCapacityUnit.Text == "GB")
                {
                    this.entity.Capacity = UnitConverter.KiloByteToByte(Convert.ToUInt32(this.DiskCapacity.Text));
                }

                this.entity.Inch = Convert.ToDouble(this.DiskSize.Text.Replace('.',','));
                this.entity.Ssd = Convert.ToBoolean(this.DiskType.IsChecked);
                this.entity.Producer = dataProducer.GetEntityByName<Producer>("Firma", this.DiskProducer.Text.ToString());
                
                if (!validator.CheckConsistency(this.entity))
                {
                    throw new FormatException();
                }
                else
                {
                    if (this.isAvailable)
                        diskDataAccess.Update(this.entity);
                    else
                        diskDataAccess.Save(this.entity);
                }
            } catch (FormatException exception) {
                this.showErrorMessage(exception, "Die eingegebenen Daten sind inkonsistent. Bitte überprüfen Sie Ihre Eingaben!");
            }
            this.Close();
        }

        /// <summary>
        /// Öffnet das Fenster für die Verwaltung der Schnittstellen.
        /// </summary>
        private void DiskInterface_Click(object sender, RoutedEventArgs e)
        {
            EditPhysicalInterfaces interfaceWindow;

            if (this.entity != null)
            {
                interfaceWindow = new EditPhysicalInterfaces(this.entity.PhysicalInterfaces);
                interfaceWindow.ShowDialog();
                entity.PhysicalInterfaces = interfaceWindow.list;
            }
            else
            {
                interfaceWindow = new EditPhysicalInterfaces(new List<PhysicalInterfaceWithCount>());
                interfaceWindow.ShowDialog();
                entity.PhysicalInterfaces = interfaceWindow.list;
            }
        
        }
    }
}