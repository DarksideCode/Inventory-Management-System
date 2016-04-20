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
using InventoryManagementSystem.presentation.forms;

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
            mapper = new GraphicalObjectMapper();
            defaultBrush.Color = Color.FromRgb(196, 255, 194);
        }

        /// <summary>
        /// Lädt eine beliebige Liste in die Tabelle
        /// </summary>
        /// <param name="list">Eine beliebige Liste von Datensätzen</param>
        private void AddToTable<T> (List<T> list)
        {
            this.dataGrid.ItemsSource = list;
        }

        private void AddToHeaderName(string currentHeader, string addition)
        {
            for(int i = 0; i < this.dataGrid.Columns.Count; i++)
            {
                if(this.dataGrid.Columns[i].Header.ToString().Equals(currentHeader))
                {
                    this.dataGrid.Columns[i].Header = currentHeader + addition;
                }
            }
        }

        /// <summary>
        /// Wird beim ersten Laden des Fensters aufgerufen.
        /// Versteckt die erste Spalte (ID), da diese einen rein technischen Zweck erfüllt.
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //this.dataGrid.Columns[0].Visibility = Visibility.Hidden;
        }
        
        /// <summary>
        /// Setzt die Hintergrundfarbe aller Elemente zurück auf die Ursprungsfarbe (Weiß)
        /// </summary>
        private void resetMenuBackground()
        {
            for (int i = 0; i < this.menu.Items.Count; i++)
            {
                if(((MenuItem)this.menu.Items[i]).Name != "menu_stammdaten" && ((MenuItem)this.menu.Items[i]).Name != "menu_komponenten")
                    ((MenuItem)this.menu.Items[i]).Background = Brushes.White;
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

        /// <summary>
        /// Wird aufgerufen, wenn der Menüpunkt 'Arbeitsspeicher' selektiert wurde.
        /// Lädt die Tabelle neu, passt den Titel an und ändert die Hintergrundfarbe des Menüpunktes.
        /// </summary>
        private void RAM_Selected(object sender, RoutedEventArgs e)
        {
            try {
                RandomAccessMemoryDataAccess dataAccess = new RandomAccessMemoryDataAccess();
                this.AddToTable(mapper.MapToGraphicalObject(dataAccess.GetAllEntities<RandomAccessMemory>()));
                this.dataGrid.Columns[0].Visibility = Visibility.Hidden;
                this.AddToHeaderName("Speicher", " (MB)");
                this.AddToHeaderName("Taktrate", " (MHz)");
            } catch (MySql.Data.MySqlClient.MySqlException exception) {
                this.showErrorMessage(exception, this.noDatabaseConnection);
            }
            this.entityName.Content = "Arbeitsspeicher";
            this.selectedEntity = "RandomAccessMemory";
            this.resetMenuBackground();
            this.menu_ram.Background = this.defaultBrush;
        }

        /// <summary>
        /// Wird aufgerufen, wenn der Menüpunkt 'Festplatte' selektiert wurde.
        /// Lädt die Tabelle neu, passt den Titel an und ändert die Hintergrundfarbe des Menüpunktes.
        /// </summary>
        private void Disk_Selected(object sender, RoutedEventArgs e)
        {
            try {
                DiskDataAccess dataAccess = new DiskDataAccess();
                this.AddToTable(mapper.MapToGraphicalObject(dataAccess.GetAllEntities<Disk>()));
                this.dataGrid.Columns[0].Visibility = Visibility.Hidden;
                this.AddToHeaderName("Kapazität", " (GB)");
            } catch (MySql.Data.MySqlClient.MySqlException exception) {
                this.showErrorMessage(exception, this.noDatabaseConnection);
            }
            this.entityName.Content = "Festplatte";
            this.selectedEntity = "Disk";
            this.resetMenuBackground();
            this.menu_disk.Background = this.defaultBrush;
        }

        /// <summary>
        /// Wird aufgerufen, wenn der Menüpunkt 'Grafikkarte' selektiert wurde.
        /// Lädt die Tabelle neu, passt den Titel an und ändert die Hintergrundfarbe des Menüpunktes.
        /// </summary>
        private void GraphicCard_Selected(object sender, RoutedEventArgs e)
        {
            try {
                GraphicCardDataAccess dataAccess = new GraphicCardDataAccess();
                this.AddToTable(mapper.MapToGraphicalObject(dataAccess.GetAllEntities<GraphicCard>()));
                this.dataGrid.Columns[0].Visibility = Visibility.Hidden;
                this.AddToHeaderName("Taktrate", " (MHz)");
                this.AddToHeaderName("Speicher", " (MB)");
            } catch (MySql.Data.MySqlClient.MySqlException exception) {
                this.showErrorMessage(exception, this.noDatabaseConnection);
            }
            this.entityName.Content = "Grafikkarte";
            this.selectedEntity = "GraphicCard";
            this.resetMenuBackground();
            this.menu_graphiccard.Background = this.defaultBrush;
        }

        /// <summary>
        /// Wird aufgerufen, wenn der Menüpunkt 'Hauptplatine' selektiert wurde.
        /// Lädt die Tabelle neu, passt den Titel an und ändert die Hintergrundfarbe des Menüpunktes.
        /// </summary>
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

        /// <summary>
        /// Wird aufgerufen, wenn der Menüpunkt 'Monitor' selektiert wurde.
        /// Lädt die Tabelle neu, passt den Titel an und ändert die Hintergrundfarbe des Menüpunktes.
        /// </summary>
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

        /// <summary>
        /// Wird aufgerufen, wenn der Menüpunkt 'Prozessor' selektiert wurde.
        /// Lädt die Tabelle neu, passt den Titel an und ändert die Hintergrundfarbe des Menüpunktes.
        /// </summary>
        private void Processor_Selected(object sender, RoutedEventArgs e)
        {
            try {
                ProcessorDataAccess dataAccess = new ProcessorDataAccess();
                this.AddToTable(mapper.MapToGraphicalObject(dataAccess.GetAllEntities<Processor>()));
                this.dataGrid.Columns[0].Visibility = Visibility.Hidden;
                this.AddToHeaderName("Taktrate", " (MHz)");
            } catch (MySql.Data.MySqlClient.MySqlException exception) {
                this.showErrorMessage(exception, this.noDatabaseConnection);
            }
            this.entityName.Content = "Prozessor";
            this.selectedEntity = "Processor";
            this.resetMenuBackground();
            this.menu_processor.Background = this.defaultBrush;
        }

        /// <summary>
        /// Wird aufgerufen, wenn der Menüpunkt 'Hersteller' selektiert wurde.
        /// Lädt die Tabelle neu, passt den Titel an und ändert die Hintergrundfarbe des Menüpunktes.
        /// </summary>
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

        /// <summary>
        /// Wird aufgerufen, wenn der Menüpunkt 'Schnittstelle' selektiert wurde.
        /// Lädt die Tabelle neu, passt den Titel an und ändert die Hintergrundfarbe des Menüpunktes.
        /// </summary>
        private void Interface_Selected(object sender, RoutedEventArgs e)
        {
            try {
                PhysicalInterfaceDataAccess dataAccess = new PhysicalInterfaceDataAccess();
                this.AddToTable(mapper.MapToGraphicalObject(dataAccess.GetAllEntities<PhysicalInterface>()));
                this.dataGrid.Columns[0].Visibility = Visibility.Hidden;
                this.AddToHeaderName("Transferrate", " (MB/s)");
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
            Window configWindow = new InventoryManagementSystem.presentation.ConfigWindow();
            configWindow.ShowDialog();
        }

        private void NewBtn_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(this.selectedEntity);
            switch(this.selectedEntity)
            {
                case "Disk":
                    CreateDisk createWindow = new CreateDisk();
                    createWindow.ShowDialog();
                    break;
            }
        }

    }
}