using System.Windows;
using System.Windows.Controls;
using InventoryManagementSystem.utilitys;
using InventoryManagementSystem.components;
using System.Collections.Generic;
using InventoryManagementSystem.dataAccess;
using InventoryManagementSystem.control;

namespace InventoryManagementSystem
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GraphicalObjectMapper mapper;

        public MainWindow()
        {
            InitializeComponent();
            RandomAccessMemoryDataAccess dataAccess = new RandomAccessMemoryDataAccess();
            mapper = new GraphicalObjectMapper();

            this.AddToTable(mapper.MapToGraphicalObject(dataAccess.GetAllEntities<RandomAccessMemory>()));
        }

        private void AddToTable<T> (List<T> list)
        {
            this.dataGrid.ItemsSource = list;
            this.dataGrid.IsReadOnly = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.dataGrid.Columns[0].Visibility = Visibility.Hidden;
        }
        

        private void RAM_Selected(object sender, RoutedEventArgs e)
        {
            RandomAccessMemoryDataAccess dataAccess = new RandomAccessMemoryDataAccess();
            this.AddToTable(mapper.MapToGraphicalObject(dataAccess.GetAllEntities<RandomAccessMemory>()));
            this.dataGrid.Columns[0].Visibility = Visibility.Hidden;
        }

        private void Disk_Selected(object sender, RoutedEventArgs e)
        {
            DiskDataAccess dataAccess = new DiskDataAccess();
            this.AddToTable(mapper.MapToGraphicalObject(dataAccess.GetAllEntities<Disk>()));
            this.dataGrid.Columns[0].Visibility = Visibility.Hidden;
        }

        private void GraphicCard_Selected(object sender, RoutedEventArgs e)
        {
            GraphicCardDataAccess dataAccess = new GraphicCardDataAccess();
            this.AddToTable(mapper.MapToGraphicalObject(dataAccess.GetAllEntities<GraphicCard>()));
            this.dataGrid.Columns[0].Visibility = Visibility.Hidden;
        }

        private void Motherboard_Selected(object sender, RoutedEventArgs e)
        {
            MotherboardDataAccess dataAccess = new MotherboardDataAccess();
            this.AddToTable(mapper.MapToGraphicalObject(dataAccess.GetAllEntities<Motherboard>()));
            this.dataGrid.Columns[0].Visibility = Visibility.Hidden;
        }

        private void Monitor_Selected(object sender, RoutedEventArgs e)
        {
            MonitorDataAccess dataAccess = new MonitorDataAccess();
            this.AddToTable(mapper.MapToGraphicalObject(dataAccess.GetAllEntities<Monitor>()));
            this.dataGrid.Columns[0].Visibility = Visibility.Hidden;
        }

        private void Processor_Selected(object sender, RoutedEventArgs e)
        {
            ProcessorDataAccess dataAccess = new ProcessorDataAccess();
            this.AddToTable(mapper.MapToGraphicalObject(dataAccess.GetAllEntities<Processor>()));
            this.dataGrid.Columns[0].Visibility = Visibility.Hidden;
        }

        
        private void Producer_Selected(object sender, RoutedEventArgs e)
        {
            ProducerDataAccess dataAccess = new ProducerDataAccess();
            this.AddToTable(mapper.MapToGraphicalObject(dataAccess.GetAllEntities<Producer>()));
            this.dataGrid.Columns[0].Visibility = Visibility.Hidden;
        }
        
        private void Interface_Selected(object sender, RoutedEventArgs e)
        {
            PhysicalInterfaceDataAccess dataAccess = new PhysicalInterfaceDataAccess();
            this.AddToTable(mapper.MapToGraphicalObject(dataAccess.GetAllEntities<PhysicalInterface>()));
            this.dataGrid.Columns[0].Visibility = Visibility.Hidden;
        }

    }
}