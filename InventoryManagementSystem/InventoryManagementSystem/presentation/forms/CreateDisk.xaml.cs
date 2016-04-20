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
using System.Windows.Navigation;
using System.Windows.Shapes;
using InventoryManagementSystem.dataAccess;
using InventoryManagementSystem.components;
using InventoryManagementSystem.validation;

namespace InventoryManagementSystem.presentation.forms
{
    /// <summary>
    /// Interaction logic for CreateDisk.xaml
    /// </summary>
    public partial class CreateDisk : Window
    {

        public CreateDisk()
        {
            InitializeComponent();
            this.SetValuesCapacityUnit();
            this.GetProducers();
        }

        private void SetValuesCapacityUnit()
        {
            DiskCapacityUnit.Items.Add("MB");
            DiskCapacityUnit.Items.Add("GB");
            DiskCapacityUnit.Items.Add("TB");

            DiskCapacityUnit.SelectedIndex = 1;
        }

        private void SetValuesProducerBox()
        {
            Producer producer = new Producer();

            DiskProducer.Items.Add(producer.CompanyName);
            //DiskProducer.SelectedIndex = 1;
        }

        private void DiskCancel_Click(object sender, RoutedEventArgs e)
        {
            // Close this window
            this.Close();
        }

        private void GetProducers()
        {
            ProducerDataAccess dataProducers = new ProducerDataAccess();

            List<Producer> producers = dataProducers.GetAllEntities<Producer>();

            foreach (Producer element in producers)
            {
                DiskProducer.Items.Add(element.CompanyName.ToString());
            }
        }
    }
}
