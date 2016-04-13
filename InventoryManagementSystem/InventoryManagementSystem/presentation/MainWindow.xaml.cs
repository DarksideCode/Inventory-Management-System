using System.Windows;
using System.Windows.Controls;
using InventoryManagementSystem.utilitys;
using InventoryManagementSystem.components;
using System.Collections.Generic;
using InventoryManagementSystem.dataAccess;
using InventoryManagementSystem.control;
using InventoryManagementSystem.database.basic;
using System.Data;
using System;

namespace InventoryManagementSystem
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GraphicalObjectMapper mapper;
        private string selectedEntity;

        public MainWindow()
        {
            InitializeComponent();
            RandomAccessMemoryDataAccess dataAccess = new RandomAccessMemoryDataAccess();
            mapper = new GraphicalObjectMapper();
            selectedEntity = "RandomAccessMemory";

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
            this.entityName.Content = "Arbeitsspeicher";
        }

        private void Disk_Selected(object sender, RoutedEventArgs e)
        {
            DiskDataAccess dataAccess = new DiskDataAccess();
            this.AddToTable(mapper.MapToGraphicalObject(dataAccess.GetAllEntities<Disk>()));
            this.dataGrid.Columns[0].Visibility = Visibility.Hidden;
            this.entityName.Content = "Festplatte";
        }

        private void GraphicCard_Selected(object sender, RoutedEventArgs e)
        {
            GraphicCardDataAccess dataAccess = new GraphicCardDataAccess();
            this.AddToTable(mapper.MapToGraphicalObject(dataAccess.GetAllEntities<GraphicCard>()));
            this.dataGrid.Columns[0].Visibility = Visibility.Hidden;
            this.entityName.Content = "Grafikkarte";
        }

        private void Motherboard_Selected(object sender, RoutedEventArgs e)
        {
            MotherboardDataAccess dataAccess = new MotherboardDataAccess();
            this.AddToTable(mapper.MapToGraphicalObject(dataAccess.GetAllEntities<Motherboard>()));
            this.dataGrid.Columns[0].Visibility = Visibility.Hidden;
            this.entityName.Content = "Hauptplatine";
        }

        private void Monitor_Selected(object sender, RoutedEventArgs e)
        {
            MonitorDataAccess dataAccess = new MonitorDataAccess();
            this.AddToTable(mapper.MapToGraphicalObject(dataAccess.GetAllEntities<Monitor>()));
            this.dataGrid.Columns[0].Visibility = Visibility.Hidden;
            this.entityName.Content = "Monitor";
        }

        private void Processor_Selected(object sender, RoutedEventArgs e)
        {
            ProcessorDataAccess dataAccess = new ProcessorDataAccess();
            this.AddToTable(mapper.MapToGraphicalObject(dataAccess.GetAllEntities<Processor>()));
            this.dataGrid.Columns[0].Visibility = Visibility.Hidden;
            this.entityName.Content = "Prozessor";
        }

        
        private void Producer_Selected(object sender, RoutedEventArgs e)
        {
            ProducerDataAccess dataAccess = new ProducerDataAccess();
            this.AddToTable(mapper.MapToGraphicalObject(dataAccess.GetAllEntities<Producer>()));
            this.dataGrid.Columns[0].Visibility = Visibility.Hidden;
            this.entityName.Content = "Hersteller";
        }
        
        private void Interface_Selected(object sender, RoutedEventArgs e)
        {
            PhysicalInterfaceDataAccess dataAccess = new PhysicalInterfaceDataAccess();
            this.AddToTable(mapper.MapToGraphicalObject(dataAccess.GetAllEntities<PhysicalInterface>()));
            this.dataGrid.Columns[0].Visibility = Visibility.Hidden;
            this.entityName.Content = "Schnittstelle";
        }

        /// <summary>
        /// Initiiert das Löschen des ausgewählten Objektes aus der Datenbank und lädt die Tabelle
        /// erneut. Beim verletzen einer Datenbank-Regel wird das Löschen verhindert und eine entsprechende
        /// Fehlermeldung ausgegeben.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Delete_Entity(object sender, RoutedEventArgs e)
        {
            int id = 0;
            DatabaseBasic dataAccess = null;

            if (this.dataGrid.SelectedItems.Count > 0)
            {
                switch (this.dataGrid.SelectedItems[0].GetType().Name)
                {
                    case "RandomAccessMemoryGraphicalObject":
                        dataAccess = new RandomAccessMemoryDataAccess();
                        id = ((RandomAccessMemoryGraphicalObject)this.dataGrid.SelectedItems[0]).Id;
                        dataAccess.Delete(id);
                        this.AddToTable(mapper.MapToGraphicalObject(dataAccess.GetAllEntities<RandomAccessMemory>()));
                        break;
                    case "DiskGraphicalObject":
                        dataAccess = new DiskDataAccess();
                        id = ((DiskGraphicalObject)this.dataGrid.SelectedItems[0]).Id;
                        dataAccess.Delete(id);
                        this.AddToTable(mapper.MapToGraphicalObject(dataAccess.GetAllEntities<Disk>()));
                        break;
                    case "GraphicCardGraphicalObject":
                        dataAccess = new GraphicCardDataAccess();
                        id = ((GraphicCardGraphicalObject)this.dataGrid.SelectedItems[0]).Id;
                        dataAccess.Delete(id);
                        this.AddToTable(mapper.MapToGraphicalObject(dataAccess.GetAllEntities<GraphicCard>()));
                        break;
                    case "MotherboardGraphicalObject":
                        dataAccess = new MotherboardDataAccess();
                        id = ((MotherboardGraphicalObject)this.dataGrid.SelectedItems[0]).Id;
                        dataAccess.Delete(id);
                        this.AddToTable(mapper.MapToGraphicalObject(dataAccess.GetAllEntities<Motherboard>()));
                        break;
                    case "MonitorGraphicalObject":
                        dataAccess = new MonitorDataAccess();
                        id = ((MonitorGraphicalObject)this.dataGrid.SelectedItems[0]).Id;
                        dataAccess.Delete(id);
                        this.AddToTable(mapper.MapToGraphicalObject(dataAccess.GetAllEntities<Monitor>()));
                        break;
                    case "ProcessorGraphicalObject":
                        dataAccess = new ProcessorDataAccess();
                        id = ((ProcessorGraphicalObject)this.dataGrid.SelectedItems[0]).Id;
                        dataAccess.Delete(id);
                        this.AddToTable(mapper.MapToGraphicalObject(dataAccess.GetAllEntities<Processor>()));
                        break;
                    case "ProducerGraphicalObject":
                        dataAccess = new ProducerDataAccess();
                        id = ((ProducerGraphicalObject)this.dataGrid.SelectedItems[0]).Id;

                        try {
                            dataAccess.Delete(id);
                        } catch (MySql.Data.MySqlClient.MySqlException exception) {
                            MessageBoxResult result = MessageBox.Show("Das ausgewählte Element kann nicht gelöscht werden, da noch eine Referenz darauf besteht.", "Warnung", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }

                        this.AddToTable(mapper.MapToGraphicalObject(dataAccess.GetAllEntities<Producer>()));
                        break;
                    case "PhysicalInterfaceGraphicalObject":
                        dataAccess = new PhysicalInterfaceDataAccess();
                        id = ((PhysicalInterfaceGraphicalObject)this.dataGrid.SelectedItems[0]).Id;
                        
                        try {
                            dataAccess.Delete(id);
                        } catch (MySql.Data.MySqlClient.MySqlException exception) {
                            MessageBoxResult result = MessageBox.Show("Das ausgewählte Element kann nicht gelöscht werden, da noch eine Referenz darauf besteht.", "Warnung", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }

                        this.AddToTable(mapper.MapToGraphicalObject(dataAccess.GetAllEntities<PhysicalInterface>()));
                        break;
                }
                this.dataGrid.Columns[0].Visibility = Visibility.Hidden;
                this.dataGrid.Items.Refresh();
            }
        }

        private void ConfigBtn_Click(object sender, RoutedEventArgs e)
        {
            Window ConfigWindow = new InventoryManagementSystem.presentation.ConfigWindow();
            ConfigWindow.Show();
        }
    }
}