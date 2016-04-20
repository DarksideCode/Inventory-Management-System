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
        public CreateGraphicCard(GraphicCard entity = null)
        {
            InitializeComponent();
            this.SetValuesCapacityUnit();
            this.GetProducers();
        }

        private void SetValuesCapacityUnit()
        {
            this.GraphicStorageUnit.Items.Add("MB");
            this.GraphicStorageUnit.Items.Add("GB");
            this.GraphicStorageUnit.Items.Add("TB");

            this.GraphicStorageUnit.SelectedIndex = 1;
        }

        private void GetProducers()
        {
            ProducerDataAccess dataProducers = new ProducerDataAccess();

            List<Producer> producers = dataProducers.GetAllEntities<Producer>();

            foreach (Producer element in producers)
            {
                this.GraphicProducer.Items.Add(element.CompanyName.ToString());
            }
        }

        private void GraphicCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void GraphicSave_Click(object sender, RoutedEventArgs e)
        {
            GraphicCard dataGraphic = new GraphicCard();
            GraphicCardDataAccess dataGraphicModel = new GraphicCardDataAccess();
            ProducerDataAccess dataProducer = new ProducerDataAccess();

            dataGraphic.Description = this.GraphicDescription.Text.ToString();
            dataGraphic.ClockRate = double.Parse(this.GraphicClockRate.Text);
            dataGraphic.Model = this.GraphicModel.Text.ToString();

            if (this.GraphicStorageUnit.Text == "MB")
            {
                dataGraphic.Memory = UnitConverter.MegaByteToByte(Convert.ToUInt32(this.GraphicStorage.Text));
            }
            else if (this.GraphicStorageUnit.Text == "GB")
            {
                dataGraphic.Memory = UnitConverter.GigaByteToByte(Convert.ToUInt32(this.GraphicStorage.Text));
            }

            dataGraphic.Producer = dataProducer.GetEntityByName<Producer>("Firma", this.GraphicProducer.Text.ToString());

            dataGraphicModel.Save(dataGraphic);
            this.Close();
        }
    }
}
