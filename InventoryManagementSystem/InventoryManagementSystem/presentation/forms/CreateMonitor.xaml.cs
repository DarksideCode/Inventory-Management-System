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
    /// Interaktionslogik für CreateMonitor.xaml
    /// </summary>
    public partial class CreateMonitor : Window
    {
        private Monitor entity;
        private bool isAvailable;

        /// <summary>
        /// Konstruktor: Setzt die Wert für die Initialisierung des Dialoges
        /// </summary>
        /// <param name="entity">Objekt eines Monitor</param>
        public CreateMonitor(Monitor entity = null)
        {
            InitializeComponent();
            this.GetProducers();
            if (entity != null)
            {
                this.entity = entity;
                this.isAvailable = true;
                this.SetAllFields();
            }
            else
            {
                this.entity = new Monitor();
                this.isAvailable = false;
            }
        }

        private void SetAllFields()
        {
            this.description.Text = entity.Description.ToString();
            this.resolution.Text  = entity.Resolution.ToString();
            this.inch.Text = entity.Inch.ToString();
            this.aspectRatio.Text = entity.AspectRatio.ToString();
            this.producer.SelectedItem = this.entity.Producer.CompanyName;
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
                this.producer.Items.Add(element.CompanyName.ToString());
            }
        }

        /// <summary>
        /// Schließt das aktuelle Fenster
        /// </summary>
        private void MonitorCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Ruft die Informationen aus dem Formular ab und speichert sie in die Datenbank.
        /// Wirft eine Fehlermeldung, wenn die Validierung fehlschlägt.
        /// </summary>
        private void MonitorSave_Click(object sender, RoutedEventArgs e)
        {
            ProducerDataAccess dataProducer = new ProducerDataAccess();
            MonitorDataAccess monitorDataAccess = new MonitorDataAccess();
            MonitorValidator validator = new MonitorValidator();

            try
            {
                this.setEntityWithFormData(dataProducer);

                if (!validator.CheckConsistency(this.entity))
                {
                    ErrorHandler.ShowErrorMessage("Validierung fehlgeschlagen", ErrorHandler.VALIDATION_FAILED);
                }
                else
                {
                    if (this.isAvailable)
                        monitorDataAccess.Update(this.entity);
                    else
                        monitorDataAccess.Save(this.entity);
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
            catch(System.OverflowException exception)
            {
                ErrorHandler.ShowErrorMessage(exception, ErrorHandler.DATA_TOO_LONG);
            }
        }

        /// <summary>
        /// Setzt die Werte des Formulares in der entity.
        /// </summary>
        /// <param name="dataProducer">Dataaccess Objekt eines Produzenten</param>
        private void setEntityWithFormData(ProducerDataAccess dataProducer)
        {
            this.entity.Description = this.description.Text;
            this.entity.Resolution = this.resolution.Text;
            this.entity.Inch = double.Parse(this.inch.Text.Replace('.',','));
            this.entity.AspectRatio = this.aspectRatio.Text;
            this.entity.Producer = dataProducer.GetEntityByName<Producer>("Firma", this.producer.Text.ToString());
        }

        /// <summary>
        /// Öffnet das Fenster für die Verwaltung der Schnittstellen.
        /// </summary>
        private void GraphicInterface_Click(object sender, RoutedEventArgs e)
        {
            EditPhysicalInterfaces interfaceWindow;

            if (this.entity != null)
            {
                interfaceWindow = new EditPhysicalInterfaces(this.entity.PhysicalInterfaces);
                interfaceWindow.ShowDialog();
                this.entity.PhysicalInterfaces = interfaceWindow.list;
            }
            else
            {
                interfaceWindow = new EditPhysicalInterfaces(new List<PhysicalInterfaceWithCount>());
                interfaceWindow.ShowDialog();
                this.entity.PhysicalInterfaces = interfaceWindow.list;
            }
        }
    }
}
