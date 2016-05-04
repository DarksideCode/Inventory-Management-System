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
                //this.SetAllFields();
            }
            else
            {
                this.entity = new Monitor();
                this.isAvailable = false;
            }
        }

        private void SetAllFields()
        {

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

        private void MonitorCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
