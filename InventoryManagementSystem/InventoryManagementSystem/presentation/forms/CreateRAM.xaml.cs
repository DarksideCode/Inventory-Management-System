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
    /// Interaktionslogik für CreateRAM.xaml
    /// </summary>
    public partial class CreateRAM : Window
    {
        public CreateRAM(RandomAccessMemory entity = null)
        {
            InitializeComponent();
            this.GetProducers();
            this.SetValuesCapacityUnit();
            if (entity != null)
            {
                this.SetAllFields(entity);
            }
        }

        private void SetAllFields(RandomAccessMemory entity)
        {
            this.RamDescription.Text = entity.Description.ToString();
            this.RamStorage.Text = entity.Memory.ToString();
            this.RamClockRate.Text = entity.ClockRate.ToString();
        }

        private void CancelRamBTN_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CreateRamBTN_Click(object sender, RoutedEventArgs e)
        {
            RandomAccessMemory dataRandomModel = new RandomAccessMemory();
            RandomAccessMemoryDataAccess dataRandom = new RandomAccessMemoryDataAccess();
            ProducerDataAccess dataProducer = new ProducerDataAccess();

            dataRandomModel.Description = this.RamDescription.Text.ToString();
            dataRandomModel.Memory = ulong.Parse(this.RamStorage.Text);
            dataRandomModel.ClockRate = double.Parse(this.RamClockRate.Text);
            dataRandomModel.Producer = dataProducer.GetEntityByName<Producer>("Firma", this.RamProducer.Text.ToString());

            if (this.RamStorageUnit.Text == "MB")
            {
                dataRandomModel.Memory = UnitConverter.MegaByteToByte(Convert.ToUInt32(this.RamStorage.Text));
            }
            else if (this.RamStorageUnit.Text == "GB")
            {
                dataRandomModel.Memory = UnitConverter.GigaByteToByte(Convert.ToUInt32(this.RamStorage.Text));
            }

                dataRandom.Save(dataRandomModel);

            this.Close();
        }

        private void GetProducers()
        {
            ProducerDataAccess dataProducers = new ProducerDataAccess();

            List<Producer> producers = dataProducers.GetAllEntities<Producer>();

            foreach (Producer element in producers)
            {
                this.RamProducer.Items.Add(element.CompanyName.ToString());
            }
        }

        private void SetValuesCapacityUnit()
        {
            this.RamStorageUnit.Items.Add("MB");
            this.RamStorageUnit.Items.Add("GB");

            this.RamStorageUnit.SelectedIndex = 1;
        }
    }
}
