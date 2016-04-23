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
    /// Interaction logic for CreateMotherboard.xaml
    /// </summary>
    public partial class CreateMotherboard : Window
    {
        private Motherboard entity;
        private bool isAvailable;

        /// <summary>
        /// Konstruktor: Setzt die Wert für die Initialisierung des Dialoges
        /// </summary>
        /// <param name="entity">Objekt eines Mainboards</param>
        public CreateMotherboard(Motherboard entity = null)
        {
            InitializeComponent();
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
                this.entity = new Motherboard();
                this.isAvailable = false;
            }
        }

        /// <summary>
        /// Setzt die Werte der UI-Elemente, wenn eine Entität bearbeitet wird
        /// </summary>
        private void SetAllFields()
        {
            this.description.Text = this.entity.Description;
            this.size.Text = this.entity.Inch.ToString();
            this.producer.SelectedItem = this.entity.Producer.CompanyName;
            this.socket.Text = this.entity.Socket;
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
        /// Öffnet eine MessageBox mit der übergebenen Fehlermeldung.
        /// </summary>
        /// <param name="exception">Die Exception, welche ausgelöst wurde</param>
        /// <param name="message">Die Fehlermeldung, welche angezeigt wird</param>
        private void showErrorMessage(Exception exception, string message)
        {
            MessageBox.Show(message, exception.GetType().Name, MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void MainboardSave_Click(object sender, RoutedEventArgs e)
        {
            ProducerDataAccess dataProducer = new ProducerDataAccess();
            MotherboardDataAccess diskDataAccess = new MotherboardDataAccess();
            MotherboardValidator validator = new MotherboardValidator();

            try
            {
                this.entity.Description = this.description.Text;
                this.entity.Inch = Convert.ToDouble(this.size.Text.Replace('.', ','));
                this.entity.Producer = dataProducer.GetEntityByName<Producer>("Firma", this.producer.Text.ToString());
                this.entity.Socket = this.socket.Text;

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
            }
            catch (FormatException exception)
            {
                this.showErrorMessage(exception, "Die eingegebenen Daten sind inkonsistent. Bitte überprüfen Sie Ihre Eingaben!");
            }

            this.Close();
        }

        private void MainboardCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
