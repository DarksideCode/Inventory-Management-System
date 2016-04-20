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
using System.Windows.Navigation;
using System.Windows.Shapes;
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

        public CreateDisk(Disk entity = null)
        {
            InitializeComponent();
            this.SetValuesCapacityUnit();
            this.GetProducers();
            this.entity = entity;
            if (entity != null)
            {
                this.SetAllFields(entity);
                this.entity = entity;
            }
            else
            {
                this.entity = new Disk();
            }
        }

        public void SetPhysicalInterfaces(List<PhysicalInterfaceWithCount> physicalInterfaces)
        {
            this.entity.PhysicalInterfaces = physicalInterfaces;
        }

        private void SetValuesCapacityUnit()
        {
            DiskCapacityUnit.Items.Add("MB");
            DiskCapacityUnit.Items.Add("GB");
            DiskCapacityUnit.Items.Add("TB");

            DiskCapacityUnit.SelectedIndex = 1;
        }

        private void SetAllFields(Disk entity)
        {
            this.DiskDescription.Text = entity.Description;
            this.DiskCapacity.Text = entity.Capacity.ToString();
            this.DiskSize.Text = entity.Inch.ToString();
            this.DiskType.IsChecked = entity.Ssd;
            this.DiskProducer.SelectedItem = entity.Producer.CompanyName;
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

        /// <summary>
        /// Öffnet eine MessageBox mit der übergebenen Fehlermeldung.
        /// </summary>
        /// <param name="exception">Die Exception, welche ausgelöst wurde</param>
        /// <param name="message">Die Fehlermeldung, welche angezeigt wird</param>
        private void showErrorMessage(Exception exception, string message)
        {
            MessageBox.Show(message, exception.GetType().Name, MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void DiskSave_Click(object sender, RoutedEventArgs e)
        {
            Disk dataDisk = this.entity;
            ProducerDataAccess dataProducer = new ProducerDataAccess();
            DiskDataAccess diskDataAccess = new DiskDataAccess();
            DiskValidator validator = new DiskValidator();

            try {
                dataDisk.Description = this.DiskDescription.Text;
                if (this.DiskCapacityUnit.Text == "MB")
                {
                    dataDisk.Capacity = UnitConverter.MegaByteToByte(Convert.ToUInt32(this.DiskCapacity.Text));
                }
                else if (this.DiskCapacityUnit.Text == "GB")
                {
                    dataDisk.Capacity = UnitConverter.GigaByteToByte(Convert.ToUInt32(this.DiskCapacity.Text));
                }

                dataDisk.Inch = Convert.ToDouble(this.DiskSize.Text);
                dataDisk.Ssd = Convert.ToBoolean(this.DiskType.IsChecked);
                dataDisk.Producer = dataProducer.GetEntityByName<Producer>("Firma", this.DiskProducer.Text.ToString());
                if (!validator.CheckConsistency(dataDisk))
                {
                    throw new FormatException();
                }
            } catch (FormatException exception) {
                this.showErrorMessage(exception, "Die eingegebenen Daten sind inkonsistent. Bitte überprüfen Sie Ihre Eingaben!");
            }

            
            //diskDataAccess.Save(dataDisk);

            this.Close();
        }

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
    }
}
