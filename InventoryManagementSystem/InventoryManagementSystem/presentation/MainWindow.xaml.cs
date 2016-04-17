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
using System.Windows.Media;
using InventoryManagementSystem.presentation;

namespace InventoryManagementSystem
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GraphicalObjectMapper mapper;
        private string selectedEntity = "RandomAccessMemory";
        private SolidColorBrush defaultBrush = new SolidColorBrush();

        private readonly string noDatabaseConnection = "Es konnte keine Verbindung zur Datenbank hergestellt werden. Bitte überprüfen Sie Ihre Einstellungen!";
        private readonly string elementStillReferenced = "Das ausgewählte Element kann nicht gelöscht werden, da noch eine Referenz darauf besteht.";

        /// <summary>
        /// Initialisiert die UI-Elemente und selektiert den ersten Menüeintrag (Arbeitsspeicher)
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            RandomAccessMemoryDataAccess dataAccess = new RandomAccessMemoryDataAccess();
            mapper = new GraphicalObjectMapper();
            defaultBrush.Color = Color.FromRgb(196, 255, 194);
            this.menu_ram.Background = defaultBrush;

            try
            {
                this.AddToTable(mapper.MapToGraphicalObject(dataAccess.GetAllEntities<RandomAccessMemory>()));
            } catch (MySql.Data.MySqlClient.MySqlException exception) {
                this.showErrorMessage(exception, this.noDatabaseConnection);
            }
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
        
        private void resetMenuBackground()
        {
            for (int i = 0; i < this.menu.Items.Count; i++)
            {
                if(((MenuItem)this.menu.Items[i]).Name != "menu_stammdaten" && ((MenuItem)this.menu.Items[i]).Name != "menu_komponenten")
                    ((MenuItem)this.menu.Items[i]).Background = Brushes.White;
            }
        }

        private void showErrorMessage(Exception exception, string message)
        {
            MessageBoxResult result = MessageBox.Show(message, exception.GetType().Name, MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void RAM_Selected(object sender, RoutedEventArgs e)
        {
            try {
                RandomAccessMemoryDataAccess dataAccess = new RandomAccessMemoryDataAccess();
                this.AddToTable(mapper.MapToGraphicalObject(dataAccess.GetAllEntities<RandomAccessMemory>()));
                this.dataGrid.Columns[0].Visibility = Visibility.Hidden;
            } catch (MySql.Data.MySqlClient.MySqlException exception) {
                this.showErrorMessage(exception, this.noDatabaseConnection);
            }
            this.entityName.Content = "Arbeitsspeicher";
            this.selectedEntity = "RandomAccessMemory";
            this.resetMenuBackground();
            this.menu_ram.Background = this.defaultBrush;
        }

        private void Disk_Selected(object sender, RoutedEventArgs e)
        {
            try {
                DiskDataAccess dataAccess = new DiskDataAccess();
                this.AddToTable(mapper.MapToGraphicalObject(dataAccess.GetAllEntities<Disk>()));
                this.dataGrid.Columns[0].Visibility = Visibility.Hidden;
            } catch (MySql.Data.MySqlClient.MySqlException exception) {
                this.showErrorMessage(exception, this.noDatabaseConnection);
            }
            this.entityName.Content = "Festplatte";
            this.selectedEntity = "Disk";
            this.resetMenuBackground();
            this.menu_disk.Background = this.defaultBrush;
        }

        private void GraphicCard_Selected(object sender, RoutedEventArgs e)
        {
            try {
                GraphicCardDataAccess dataAccess = new GraphicCardDataAccess();
                this.AddToTable(mapper.MapToGraphicalObject(dataAccess.GetAllEntities<GraphicCard>()));
                this.dataGrid.Columns[0].Visibility = Visibility.Hidden;
            } catch (MySql.Data.MySqlClient.MySqlException exception) {
                this.showErrorMessage(exception, this.noDatabaseConnection);
            }
            this.entityName.Content = "Grafikkarte";
            this.selectedEntity = "GraphicCard";
            this.resetMenuBackground();
            this.menu_graphiccard.Background = this.defaultBrush;
        }

        private void Motherboard_Selected(object sender, RoutedEventArgs e)
        {
            try {
                MotherboardDataAccess dataAccess = new MotherboardDataAccess();
                this.AddToTable(mapper.MapToGraphicalObject(dataAccess.GetAllEntities<Motherboard>()));
                this.dataGrid.Columns[0].Visibility = Visibility.Hidden;
            } catch (MySql.Data.MySqlClient.MySqlException exception) {
                this.showErrorMessage(exception, this.noDatabaseConnection);
            }
            this.entityName.Content = "Hauptplatine";
            this.selectedEntity = "Motherboard";
            this.resetMenuBackground();
            this.menu_motherboard.Background = this.defaultBrush;
        }

        private void Monitor_Selected(object sender, RoutedEventArgs e)
        {
            try {
                MonitorDataAccess dataAccess = new MonitorDataAccess();
                this.AddToTable(mapper.MapToGraphicalObject(dataAccess.GetAllEntities<Monitor>()));
                this.dataGrid.Columns[0].Visibility = Visibility.Hidden;
            } catch (MySql.Data.MySqlClient.MySqlException exception) {
                this.showErrorMessage(exception, this.noDatabaseConnection);
            }
            this.entityName.Content = "Monitor";
            this.selectedEntity = "Monitor";
            this.resetMenuBackground();
            this.menu_monitor.Background = this.defaultBrush;
        }

        private void Processor_Selected(object sender, RoutedEventArgs e)
        {
            try {
                ProcessorDataAccess dataAccess = new ProcessorDataAccess();
                this.AddToTable(mapper.MapToGraphicalObject(dataAccess.GetAllEntities<Processor>()));
                this.dataGrid.Columns[0].Visibility = Visibility.Hidden;
            } catch (MySql.Data.MySqlClient.MySqlException exception) {
                this.showErrorMessage(exception, this.noDatabaseConnection);
            }
            this.entityName.Content = "Prozessor";
            this.selectedEntity = "Processor";
            this.resetMenuBackground();
            this.menu_processor.Background = this.defaultBrush;
        }

        
        private void Producer_Selected(object sender, RoutedEventArgs e)
        {
            try {
                ProducerDataAccess dataAccess = new ProducerDataAccess();
                this.AddToTable(mapper.MapToGraphicalObject(dataAccess.GetAllEntities<Producer>()));
                this.dataGrid.Columns[0].Visibility = Visibility.Hidden;
            } catch (MySql.Data.MySqlClient.MySqlException exception) {
                this.showErrorMessage(exception, this.noDatabaseConnection);
            }
            this.entityName.Content = "Hersteller";
            this.selectedEntity = "Producer";
            this.resetMenuBackground();
            this.menu_producer.Background = this.defaultBrush;
        }
        
        private void Interface_Selected(object sender, RoutedEventArgs e)
        {
            try {
                PhysicalInterfaceDataAccess dataAccess = new PhysicalInterfaceDataAccess();
                this.AddToTable(mapper.MapToGraphicalObject(dataAccess.GetAllEntities<PhysicalInterface>()));
                this.dataGrid.Columns[0].Visibility = Visibility.Hidden;
            } catch (MySql.Data.MySqlClient.MySqlException exception) {
                this.showErrorMessage(exception, this.noDatabaseConnection);
            }
            this.entityName.Content = "Schnittstelle";
            this.selectedEntity = "PhysicalInterface";
            this.resetMenuBackground();
            this.menu_interface.Background = this.defaultBrush;
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
                            this.showErrorMessage(exception, this.elementStillReferenced);    
                        }

                        this.AddToTable(mapper.MapToGraphicalObject(dataAccess.GetAllEntities<Producer>()));
                        break;
                    case "PhysicalInterfaceGraphicalObject":
                        dataAccess = new PhysicalInterfaceDataAccess();
                        id = ((PhysicalInterfaceGraphicalObject)this.dataGrid.SelectedItems[0]).Id;
                        
                        try {
                            dataAccess.Delete(id);
                        } catch (MySql.Data.MySqlClient.MySqlException exception) {
                            this.showErrorMessage(exception, this.elementStillReferenced);
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

        private void NewBtn_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(this.selectedEntity);
            switch(this.selectedEntity)
            {
                case "Disk":
                    CreateEntity Create = new CreateEntity(this.selectedEntity);
                    Create.Show();
                    break;
            }
        }

    }
}