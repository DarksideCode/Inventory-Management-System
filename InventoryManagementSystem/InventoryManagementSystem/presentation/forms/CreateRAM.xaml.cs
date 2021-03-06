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
using InventoryManagementSystem.dataAccess;
using InventoryManagementSystem.components;
using InventoryManagementSystem.validation;
using InventoryManagementSystem.control;

namespace InventoryManagementSystem.presentation.forms
{
    /// <summary>
    /// Interaktionslogik für CreateRAM.xaml
    /// </summary>
    public partial class CreateRAM : Window
    {
        private RandomAccessMemory entity;
        private bool isAvailable;

        /// <summary>
        /// Konstruktor: Setzt die Wert für die Initialisierung des Dialoges
        /// </summary>
        /// <param name="entity">Objekt eines Arbeitsspeichers</param>
        public CreateRAM(RandomAccessMemory entity = null)
        {
            InitializeComponent();
            this.GetProducers();
            this.RamStorageUnit.SelectedIndex = 0;
            if (entity != null)
            {
                this.entity = entity;
                this.isAvailable = true;
                this.SetAllFields();
            }
            else
            {
                this.entity = new RandomAccessMemory();
                this.isAvailable = false;
            }
        }

        /// <summary>
        /// Setzt die Werte der UI-Elemente, wenn eine Entität bearbeitet wird
        /// </summary>
        private void SetAllFields()
        {
            this.RamDescription.Text = this.entity.Description.ToString();
            this.RamStorage.Text = this.entity.Memory.ToString();
            this.RamClockRate.Text = this.entity.ClockRate.ToString();
            this.RamProducer.SelectedItem = this.entity.Producer.CompanyName;
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
                this.RamProducer.Items.Add(element.CompanyName.ToString());
            }
        }

        /// <summary>
        /// Setzt die Werte des Formulares in der entity.
        /// </summary>
        /// <param name="dataProducer">Dataaccess Objekt eines Produzenten</param>
        private void setEntityWithFormData(ProducerDataAccess dataProducer)
        {
            this.entity.Description = this.RamDescription.Text.ToString();
            this.entity.ClockRate = double.Parse(this.RamClockRate.Text.Replace('.', ','));
            this.entity.Producer = dataProducer.GetEntityByName<Producer>("Firma", this.RamProducer.Text.ToString());

            if (this.RamStorageUnit.Text == "MB")
            {
                this.entity.Memory = ulong.Parse(this.RamStorage.Text);
            }
            else if (this.RamStorageUnit.Text == "GB")
            {
                this.entity.Memory = UnitConverter.ConvertToLower(Convert.ToDouble(this.RamStorage.Text));
            }
        }

        /// <summary>
        /// Ruft die Informationen aus dem Formular ab und speichert sie in die Datenbank.
        /// Führt eine Umrechnung in MB aus und wirft eine Fehlermeldung, wenn die Validierung
        /// fehlschlägt.
        /// </summary>
        private void RamSave_Click(object sender, RoutedEventArgs e)
        {
            RandomAccessMemoryDataAccess dataRandom = new RandomAccessMemoryDataAccess();
            ProducerDataAccess dataProducer = new ProducerDataAccess();
            RandomAccessMemoryValidator validator = new RandomAccessMemoryValidator();

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
                        dataRandom.Update(this.entity);
                    else
                        dataRandom.Save(this.entity);
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
        private void RamCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
