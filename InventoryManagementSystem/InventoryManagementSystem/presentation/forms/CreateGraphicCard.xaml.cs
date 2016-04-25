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
using InventoryManagementSystem.validation;
using InventoryManagementSystem.control;

namespace InventoryManagementSystem.presentation.forms
{
    /// <summary>
    /// Interaktionslogik für CreateGraphicCard.xaml
    /// </summary>
    public partial class CreateGraphicCard : Window
    {
        private GraphicCard entity;
        private bool isAvailable;

        /// <summary>
        /// Konstruktor: Setzt die Wert für die Initialisierung des Dialoges
        /// </summary>
        /// <param name="entity">Objekt einer Grafikkarte</param>
        public CreateGraphicCard(GraphicCard entity = null)
        {
            InitializeComponent();
            this.GraphicStorageUnit.SelectedIndex = 0;
            this.GetProducers();
            if (entity != null)
            {
                this.entity = entity;
                this.isAvailable = true;
                this.SetAllFields();
            }
            else
            {
                this.entity = new GraphicCard();
                this.isAvailable = false;
            }
        }

        /// <summary>
        /// Setzt die Werte der UI-Elemente, wenn eine Entität bearbeitet wird
        /// </summary>
        private void SetAllFields()
        {
            this.GraphicDescription.Text = this.entity.Description;
            this.GraphicClockRate.Text = this.entity.ClockRate.ToString();
            this.GraphicModel.Text = this.entity.Model;
            this.GraphicStorage.Text = this.entity.Memory.ToString();
            this.GraphicProducer.SelectedItem = this.entity.Producer.CompanyName;
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
                this.GraphicProducer.Items.Add(element.CompanyName.ToString());
            }
        }

        /// <summary>
        /// Schließt das aktuelle Fenster
        /// </summary>
        private void GraphicCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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
        private void GraphicSave_Click(object sender, RoutedEventArgs e)
        {
            GraphicCardDataAccess dataGraphicModel = new GraphicCardDataAccess();
            ProducerDataAccess dataProducer = new ProducerDataAccess();
            GraphicCardValidator validator = new GraphicCardValidator();

            try
            {
                this.entity.Description = this.GraphicDescription.Text.ToString();
                this.entity.ClockRate = double.Parse(this.GraphicClockRate.Text.Replace('.', ','));
                this.entity.Model = this.GraphicModel.Text.ToString();

                if (this.GraphicStorageUnit.Text == "MB")
                {
                    this.entity.Memory = ulong.Parse(this.GraphicStorage.Text);
                }
                else if (this.GraphicStorageUnit.Text == "GB")
                {
                    this.entity.Memory = UnitConverter.KiloByteToByte(Convert.ToDouble(this.GraphicStorage.Text));
                }

                this.entity.Producer = dataProducer.GetEntityByName<Producer>("Firma", this.GraphicProducer.Text.ToString());

                if (!validator.CheckConsistency(this.entity))
                {
                    throw new FormatException();
                }
                else
                {
                    if (isAvailable)
                        dataGraphicModel.Update(this.entity);
                    else
                        dataGraphicModel.Save(this.entity);
                }
                this.Close();
            }
            catch (FormatException exception)
            {
                this.showErrorMessage(exception, "Die eingegebenen Daten sind inkonsistent. Bitte überprüfen Sie Ihre Eingaben!");
            }
            catch (MySql.Data.MySqlClient.MySqlException exception)
            {
                this.showErrorMessage(exception, "Die eingegebenen Daten sind inkonsistent. Bitte überprüfen Sie Ihre Eingaben!");
            }
        }
    }
}