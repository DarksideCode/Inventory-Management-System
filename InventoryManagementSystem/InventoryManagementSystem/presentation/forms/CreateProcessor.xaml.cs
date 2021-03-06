﻿using System;
using System.Collections.Generic;
using System.Windows;
using InventoryManagementSystem.dataAccess;
using InventoryManagementSystem.components;
using InventoryManagementSystem.validation;
using InventoryManagementSystem.control;

namespace InventoryManagementSystem.presentation.forms
{

    /// <summary>
    /// Interaction logic for CreateProcessor.xaml
    /// </summary>
    public partial class CreateProcessor : Window
    {
        private Processor entity;
        private bool isAvailable;

        /// <summary>
        /// Konstruktor: Setzt die Wert für die Initialisierung des Dialoges
        /// </summary>
        /// <param name="entity">Objekt eines Prozessors</param>
        public CreateProcessor(Processor entity = null)
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
                this.entity = new Processor();
                this.isAvailable = false;
            }
        }

        /// <summary>
        /// Setzt die Werte der UI-Elemente, wenn eine Entität bearbeitet wird
        /// </summary>
        private void SetAllFields()
        {
            this.description.Text = this.entity.Description.ToString();
            this.model.Text = this.entity.Model.ToString();
            this.producer.SelectedItem = this.entity.Producer.CompanyName;
            this.commandSet.Text = this.entity.CommandSet.ToString();
            this.architecture.Text = this.entity.Architecture.ToString();
            this.clockRate.Text = this.entity.ClockRate.ToString();
            this.cores.Text = this.entity.Core.ToString();
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
        /// Setzt die Werte des Formulares in der entity.
        /// </summary>
        /// <param name="dataProducer">Dataaccess Objekt eines Produzenten</param>
        private void setEntityWithFormData(ProducerDataAccess dataProducer)
        {
            this.entity.Description = this.description.Text;
            this.entity.Model = this.model.Text;
            this.entity.Producer = dataProducer.GetEntityByName<Producer>("Firma", this.producer.Text.ToString());
            this.entity.CommandSet = this.commandSet.Text;
            this.entity.Architecture = uint.Parse(this.architecture.Text);
            this.entity.ClockRate = double.Parse(this.clockRate.Text.Replace(".", ","));
            this.entity.Core = uint.Parse(this.cores.Text);
        }

        /// <summary>
        /// Ruft die Informationen aus dem Formular ab und speichert sie in die Datenbank.
        /// Wirft eine Fehlermeldung, wenn die Validierung fehlschlägt.
        /// </summary>
        private void ProcessorSave_Click(object sender, RoutedEventArgs e)
        {
            ProducerDataAccess dataProducer = new ProducerDataAccess();
            ProcessorDataAccess processorDataAccess = new ProcessorDataAccess();
            ProcessorValidator validator = new ProcessorValidator();

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
                        processorDataAccess.Update(this.entity);
                    else
                        processorDataAccess.Save(this.entity);
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
        /// Schließt das aktuelle Fenster
        /// </summary>
        private void ProcessorCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
