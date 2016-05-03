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

            try
            {
                this.entity.Description = this.DiskDescription.Text;
                if (this.DiskCapacityUnit.Text == "MB")
                {
                    this.entity.Capacity = Convert.ToUInt32(this.DiskCapacity.Text);
                }
                else if (this.DiskCapacityUnit.Text == "GB")
                {
                    this.entity.Capacity = UnitConverter.KiloByteToByte(Convert.ToUInt32(this.DiskCapacity.Text));
                }

                this.entity.Inch = Convert.ToDouble(this.DiskSize.Text.Replace('.', ','));
                this.entity.Ssd = Convert.ToBoolean(this.DiskType.IsChecked);
                this.entity.Producer = dataProducer.GetEntityByName<Producer>("Firma", this.DiskProducer.Text.ToString());

                if (!validator.CheckConsistency(this.entity))
                {
                    ErrorHandler.ShowErrorMessage("Validierung fehlgeschlagen", ErrorHandler.VALIDATION_FAILED);
                }
                else
                {
                    if (this.isAvailable)
                        diskDataAccess.Update(this.entity);
                    else
                        diskDataAccess.Save(this.entity);
                    this.Close();
                }
            }
            catch (FormatException exception)
            {
                ErrorHandler.ShowErrorMessage(exception, ErrorHandler.WRONG_FORMAT);
            }
            catch (MySql.Data.MySqlClient.MySqlException exception)
            {
                ErrorHandler.ShowErrorMessage(exception, ErrorHandler.SAVE_WENT_WRONG);
            }
            catch (System.OverflowException exception)
            {
                ErrorHandler.ShowErrorMessage(exception, ErrorHandler.DATA_TOO_LONG);
            }
        }

        /// <summary>
        /// Schließt das aktuelle Fenster
        /// </summary>
        private void DiskCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}