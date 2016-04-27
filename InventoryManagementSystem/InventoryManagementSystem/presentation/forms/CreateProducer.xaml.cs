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

namespace InventoryManagementSystem.presentation.forms
{
    /// <summary>
    /// Interaktionslogik für CreateProducer.xaml
    /// </summary>
    public partial class CreateProducer : Window
    {
        private Producer entity;
        private bool isAvailable;

        /// <summary>
        /// Konstruktor: Setzt die Wert für die Initialisierung des Dialoges
        /// </summary>
        /// <param name="entity">Objekt eines Herstellers</param>
        public CreateProducer(Producer entity = null)
        {
            InitializeComponent();
            if (entity != null)
            {
                this.entity = entity;
                this.isAvailable = true;
                this.SetAllFields();
            }
            else
            {
                this.entity = new Producer();
                this.isAvailable = false;
            }
        }

        /// <summary>
        /// Setzt die Werte der UI-Elemente, wenn eine Entität bearbeitet wird
        /// </summary>
        private void SetAllFields()
        {
            this.ProducerCompanyName.Text = this.entity.CompanyName;
            this.ProducerEmail.Text = this.entity.Email;
            this.ProducerTelephone.Text = this.entity.PhoneNumber.ToString();
            this.ProducerWebsite.Text = this.entity.Website;
            this.ProducerPostalCode.Text = this.entity.PostalCode.ToString();
            this.ProducerPlace.Text = this.entity.Place;
            this.ProducerStreet.Text = this.entity.Street;
            this.ProducerHouseNumber.Text = this.entity.HouseNumber.ToString();
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
        /// Wirft eine Fehlermeldung, wenn die Validierung fehlschlägt.
        /// </summary>
        private void ProducerSave_Click(object sender, RoutedEventArgs e)
        {
            ProducerDataAccess dataAccess = new ProducerDataAccess();
            ProducerValidator validator = new ProducerValidator();

            try
            {
                this.entity.CompanyName = this.ProducerCompanyName.Text;
                this.entity.Email = this.ProducerEmail.Text;
                this.entity.PhoneNumber = this.ProducerTelephone.Text;
                this.entity.Website = this.ProducerWebsite.Text;
                this.entity.PostalCode = uint.Parse(this.ProducerPostalCode.Text);
                this.entity.Place = this.ProducerPlace.Text;
                this.entity.Street = this.ProducerStreet.Text;
                this.entity.HouseNumber = uint.Parse(this.ProducerHouseNumber.Text);

                if (validator.CheckConsistency(this.entity))
                {
                    if (this.isAvailable)
                        dataAccess.Update(this.entity);
                    else
                        dataAccess.Save(this.entity);
                }
                else
                {
                    throw new FormatException();
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
            catch (System.OverflowException exception)
            {
                this.showErrorMessage(exception, "Die eingegebenen Daten sind zu lang!");
            }
        }

        /// <summary>
        /// Schließt das aktuelle Fenster
        /// </summary>
        private void ProducerCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
