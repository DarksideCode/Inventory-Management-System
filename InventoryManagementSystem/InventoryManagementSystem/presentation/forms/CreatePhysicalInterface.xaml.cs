﻿using System;
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
using InventoryManagementSystem.control;

namespace InventoryManagementSystem.presentation.forms
{
    /// <summary>
    /// Interaktionslogik für CreatePhysicalInterface.xaml
    /// </summary>
    public partial class CreatePhysicalInterface : Window
    {
        private PhysicalInterface entity;
        private bool isAvailable;

        /// <summary>
        /// Konstruktor: Setzt die Wert für die Initialisierung des Dialoges
        /// </summary>
        /// <param name="entity">Objekt einer Schnittstelle</param>
        public CreatePhysicalInterface(PhysicalInterface entity = null)
        {
            InitializeComponent();
            this.entity = entity;

            if (entity != null)
            {
                this.SetAllFields();
                this.entity = entity;
                this.isAvailable = true;
            }
            else
            {
                this.entity = new PhysicalInterface();
                this.isAvailable = false;
            }
        }

        /// <summary>
        /// Setzt die Werte der UI-Elemente, wenn eine Entität bearbeitet wird
        /// </summary>
        private void SetAllFields()
        {
            this.interfaceName.Text = this.entity.Name;
            this.interfaceDescription.Text = this.entity.Description;
            this.interfaceSerial.IsChecked = this.entity.Serial;
            this.interfaceTransferRate.Text = this.entity.TransferRate.ToString();
        }

        /// <summary>
        /// Setzt die Werte des Formulares in der entity.
        /// </summary>
        /// <param name="dataProducer">Dataaccess Objekt eines Produzenten</param>
        private void setEntityWithFormData(ProducerDataAccess dataProducer = null)
        {
            this.entity.Name = this.interfaceName.Text;
            this.entity.Description = this.interfaceDescription.Text;
            this.entity.Serial = Convert.ToBoolean(this.interfaceSerial.IsChecked);
            this.entity.TransferRate = Convert.ToDouble(this.interfaceTransferRate.Text.Replace('.', ','));
        }

        /// <summary>
        /// Ruft die Informationen aus dem Formular ab und speichert sie in die Datenbank.
        /// Wirft eine Fehlermeldung, wenn die Validierung fehlschlägt.
        /// </summary>
        private void InterfaceSave_Click(object sender, RoutedEventArgs e)
        {
            PhysicalInterfaceDataAccess interfaceDataAccess = new PhysicalInterfaceDataAccess();
            PhysicalInterfaceValidator validator = new PhysicalInterfaceValidator();

            try
            {
                this.setEntityWithFormData();

                if (!validator.CheckConsistency(this.entity))
                {
                    ErrorHandler.ShowErrorMessage("Validierung fehlgeschlagen", ErrorHandler.VALIDATION_FAILED);
                    throw new FormatException();
                }
                else
                {
                    if (this.isAvailable)
                        interfaceDataAccess.Update(this.entity);
                    else
                        interfaceDataAccess.Save(this.entity);
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
        private void InterfaceCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
