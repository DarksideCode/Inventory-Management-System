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
        private void AddToTable<T>(List<T> list)
        {
            this.dataGrid.ItemsSource = list;
        }

        private void AddToHeaderName(string currentHeader, string addition)
        {
            for (int i = 0; i < this.dataGrid.Columns.Count; i++)
            {
                if (this.dataGrid.Columns[i].Header.ToString().Equals(currentHeader))
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
                if (((MenuItem)this.menu.Items[i]).Name != "menu_stammdaten" && ((MenuItem)this.menu.Items[i]).Name != "menu_komponenten")
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

        private void Menu_Selected(object sender, int indicator, string[] HeaderName, string[] HeaderUnit, string[] Entity)
        {
            try
            {
                int counter = 0;
                switch (indicator)
                {
                    case (1):
                        RandomAccessMemoryDataAccess ramDataAccess = new RandomAccessMemoryDataAccess();
                        this.AddToTable(mapper.MapToGraphicalObject(ramDataAccess.GetAllEntities<RandomAccessMemory>()));
                        this.menu_ram.Background = this.defaultBrush;
                        break;
                    case (2):
                        DiskDataAccess diskDataAccess = new DiskDataAccess();
                        this.AddToTable(mapper.MapToGraphicalObject(diskDataAccess.GetAllEntities<Disk>()));
                        this.menu_disk.Background = this.defaultBrush;
                        break;
                    case (3):
                        GraphicCardDataAccess graphicCardDataAccess = new GraphicCardDataAccess();
                        this.AddToTable(mapper.MapToGraphicalObject(graphicCardDataAccess.GetAllEntities<GraphicCard>()));
                        this.menu_graphiccard.Background = this.defaultBrush;
                        break;
                    case (4):
                        MotherboardDataAccess MotherboardDataAccess = new MotherboardDataAccess();
                        this.AddToTable(mapper.MapToGraphicalObject(MotherboardDataAccess.GetAllEntities<Motherboard>()));
                        this.menu_motherboard.Background = this.defaultBrush;
                        break;
                    case (5):
                        MonitorDataAccess MonitorDataAccess = new MonitorDataAccess();
                        this.AddToTable(mapper.MapToGraphicalObject(MonitorDataAccess.GetAllEntities<Monitor>()));
                        this.menu_monitor.Background = this.defaultBrush;
                        break;
                    case (6):
                        ProcessorDataAccess ProcessorDataAccess = new ProcessorDataAccess();
                        this.AddToTable(mapper.MapToGraphicalObject(ProcessorDataAccess.GetAllEntities<Processor>()));
                        this.menu_processor.Background = this.defaultBrush;
                        break;
                    case (7):
                        ProducerDataAccess ProducerDataAccess = new ProducerDataAccess();
                        this.AddToTable(mapper.MapToGraphicalObject(ProducerDataAccess.GetAllEntities<Producer>()));
                        this.menu_producer.Background = this.defaultBrush;
                        break;
                    case (8):
                        PhysicalInterfaceDataAccess InterfaceDataAccess = new PhysicalInterfaceDataAccess();
                        this.AddToTable(mapper.MapToGraphicalObject(InterfaceDataAccess.GetAllEntities<PhysicalInterface>()));
                        this.menu_interface.Background = this.defaultBrush;
                        break;
                }
                this.dataGrid.Columns[0].Visibility = Visibility.Hidden;
                foreach (string Name in HeaderName)
                {
                    this.AddToHeaderName(Name, HeaderUnit[counter]);
                    counter++;
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException exception)
            {
                this.showErrorMessage(exception, this.noDatabaseConnection);
            }
            this.entityName.Content = Entity[0];
            this.selectedEntity = Entity[1];
            this.resetMenuBackground();
        }

        /// <summary>
        /// Wird aufgerufen, wenn der Menüpunkt 'Arbeitsspeicher' selektiert wurde.
        /// Lädt die Tabelle neu, passt den Titel an und ändert die Hintergrundfarbe des Menüpunktes.
        /// </summary>
        private void RAM_Selected(object sender, RoutedEventArgs e)
        {
            int indicatorDA = 1;
            string[] HeaderName = { "Speicher", "Taktrate" };
            string[] HeaderUnit = { " (MB)", " (MHz)" };
            string[] Entity = { "Arbeitsspeicher", "RandomAccessMemory" };
            Menu_Selected(this, indicatorDA, HeaderName, HeaderUnit, Entity);
        }

        /// <summary>
        /// Wird aufgerufen, wenn der Menüpunkt 'Festplatte' selektiert wurde.
        /// Lädt die Tabelle neu, passt den Titel an und ändert die Hintergrundfarbe des Menüpunktes.
        /// </summary>
        private void Disk_Selected(object sender, RoutedEventArgs e)
        {
            int indicatorDA = 2;
            string[] HeaderName = { "Kapazität" };
            string[] HeaderUnit = { " (GB)" };
            string[] Entity = { "Festplatte", "Disk" };
            Menu_Selected(this, indicatorDA, HeaderName, HeaderUnit, Entity);
        }

        /// <summary>
        /// Wird aufgerufen, wenn der Menüpunkt 'Grafikkarte' selektiert wurde.
        /// Lädt die Tabelle neu, passt den Titel an und ändert die Hintergrundfarbe des Menüpunktes.
        /// </summary>
        private void GraphicCard_Selected(object sender, RoutedEventArgs e)
        {
            int indicatorDA = 3;
            string[] HeaderName = { "Taktrate", "Speicher" };
            string[] HeaderUnit = { " (MHz)", " (MB)" };
            string[] Entity = { "Grafikkarte", "GraphicCard" };
            Menu_Selected(this, indicatorDA, HeaderName, HeaderUnit, Entity);
        }

        /// <summary>
        /// Wird aufgerufen, wenn der Menüpunkt 'Hauptplatine' selektiert wurde.
        /// Lädt die Tabelle neu, passt den Titel an und ändert die Hintergrundfarbe des Menüpunktes.
        /// </summary>
        private void Motherboard_Selected(object sender, RoutedEventArgs e)
        {
            int indicatorDA = 4;
            string[] HeaderName = { };
            string[] HeaderUnit = { };
            string[] Entity = { "Hauptplatine", "Motherboard" };
            Menu_Selected(this, indicatorDA, HeaderName, HeaderUnit, Entity);
        }

        /// <summary>
        /// Wird aufgerufen, wenn der Menüpunkt 'Monitor' selektiert wurde.
        /// Lädt die Tabelle neu, passt den Titel an und ändert die Hintergrundfarbe des Menüpunktes.
        /// </summary>
        private void Monitor_Selected(object sender, RoutedEventArgs e)
        {
            int indicatorDA = 5;
            string[] HeaderName = { };
            string[] HeaderUnit = { };
            string[] Entity = { "Monitor", "Monitor" };
            Menu_Selected(this, indicatorDA, HeaderName, HeaderUnit, Entity);
        }

        /// <summary>
        /// Wird aufgerufen, wenn der Menüpunkt 'Prozessor' selektiert wurde.
        /// Lädt die Tabelle neu, passt den Titel an und ändert die Hintergrundfarbe des Menüpunktes.
        /// </summary>
        private void Processor_Selected(object sender, RoutedEventArgs e)
        {
            int indicatorDA = 6;
            string[] HeaderName = { "Taktrate" };
            string[] HeaderUnit = { " (MHz)" };
            string[] Entity = { "Prozessor", "Processor" };
            Menu_Selected(this, indicatorDA, HeaderName, HeaderUnit, Entity);
        }

        /// <summary>
        /// Wird aufgerufen, wenn der Menüpunkt 'Hersteller' selektiert wurde.
        /// Lädt die Tabelle neu, passt den Titel an und ändert die Hintergrundfarbe des Menüpunktes.
        /// </summary>
        private void Producer_Selected(object sender, RoutedEventArgs e)
        {
            int indicatorDA = 7;
            string[] HeaderName = { };
            string[] HeaderUnit = { };
            string[] Entity = { "Hersteller", "Producer" };
            Menu_Selected(this, indicatorDA, HeaderName, HeaderUnit, Entity);
        }

        /// <summary>
        /// Wird aufgerufen, wenn der Menüpunkt 'Schnittstelle' selektiert wurde.
        /// Lädt die Tabelle neu, passt den Titel an und ändert die Hintergrundfarbe des Menüpunktes.
        /// </summary>
        private void Interface_Selected(object sender, RoutedEventArgs e)
        {
            int indicatorDA = 8;
            string[] HeaderName = { "Transferrate" };
            string[] HeaderUnit = { " (MB/s)" };
            string[] Entity = { "Schnittstelle", "PhysicalInterface" };
            Menu_Selected(this, indicatorDA, HeaderName, HeaderUnit, Entity);
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

                        try
                        {
                            dataAccess.Delete(id);
                        }
                        catch (MySql.Data.MySqlClient.MySqlException exception)
                        {
                            this.showErrorMessage(exception, this.elementStillReferenced);
                        }

                        this.AddToTable(mapper.MapToGraphicalObject(dataAccess.GetAllEntities<Producer>()));
                        break;
                    case "PhysicalInterfaceGraphicalObject":
                        dataAccess = new PhysicalInterfaceDataAccess();
                        id = ((PhysicalInterfaceGraphicalObject)this.dataGrid.SelectedItems[0]).Id;

                        try
                        {
                            dataAccess.Delete(id);
                        }
                        catch (MySql.Data.MySqlClient.MySqlException exception)
                        {
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
            switch (this.selectedEntity)
            {
                case "Disk":
                    CreateDisk createDiskWindow = new CreateDisk();
                    createDiskWindow.ShowDialog();
                    this.Disk_Selected(null, null);
                    break;
                case "RandomAccessMemory":
                    CreateRAM createRandomAccessMemoryWindow = new CreateRAM();
                    createRandomAccessMemoryWindow.ShowDialog();
                    this.RAM_Selected(null, null);
                    break;
                case "GraphicCard":
                    CreateGraphicCard createGrapicCardWindow = new CreateGraphicCard();
                    createGrapicCardWindow.ShowDialog();
                    this.GraphicCard_Selected(null, null);
                    break;
                case "Motherboard":
                    CreateMotherboard createMotherboardWindow = new CreateMotherboard();
                    createMotherboardWindow.ShowDialog();
                    this.Motherboard_Selected(null, null);
                    break;
                case "Processor":
                    CreateProcessor createProcessorWindow = new CreateProcessor();
                    createProcessorWindow.ShowDialog();
                    this.Processor_Selected(null, null);
                    break;
                case "Producer":
                    CreateProducer createProducerWindow = new CreateProducer();
                    createProducerWindow.ShowDialog();
                    this.Producer_Selected(null, null);
                    break;
            }
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Mapping zu Enität von GraphicalObjectMapper übernehmen lassen!
            if (this.dataGrid.SelectedItems.Count > 0)
            {
                switch (this.selectedEntity)
                {
                    case "RandomAccessMemory":
                        RandomAccessMemoryDataAccess ramDataAccess = new RandomAccessMemoryDataAccess();
                        RandomAccessMemoryGraphicalObject ram = (RandomAccessMemoryGraphicalObject)this.dataGrid.SelectedItems[0];
                        CreateRAM createRAMWindow = new CreateRAM(ramDataAccess.GetEntityById<RandomAccessMemory>(ram.Id));
                        createRAMWindow.ShowDialog();
                        break;
                    case "Disk":
                        DiskDataAccess diskDataAccess = new DiskDataAccess();
                        DiskGraphicalObject disk = (DiskGraphicalObject)this.dataGrid.SelectedItems[0];
                        CreateDisk createDiskWindow = new CreateDisk(diskDataAccess.GetEntityById<Disk>(disk.Id));
                        createDiskWindow.ShowDialog();
                        break;
                    case "GraphicCard":
                        GraphicCardDataAccess graphicCardDataAccess = new GraphicCardDataAccess();
                        GraphicCardGraphicalObject graphicCard = (GraphicCardGraphicalObject)this.dataGrid.SelectedItems[0];
                        CreateGraphicCard createGraphicCardWindow = new CreateGraphicCard(graphicCardDataAccess.GetEntityById<GraphicCard>(graphicCard.Id));
                        createGraphicCardWindow.ShowDialog();
                        break;
                    case "Motherboard":
                        MotherboardDataAccess motherboardDataAccess = new MotherboardDataAccess();
                        MotherboardGraphicalObject motherboard = (MotherboardGraphicalObject)this.dataGrid.SelectedItems[0];
                        CreateMotherboard createMotherboardWindow = new CreateMotherboard(motherboardDataAccess.GetEntityById<Motherboard>(motherboard.Id));
                        createMotherboardWindow.ShowDialog();
                        break;
                    case "Producer":
                        ProducerDataAccess producerDataAccess = new ProducerDataAccess();
                        ProducerGraphicalObject producer = (ProducerGraphicalObject)this.dataGrid.SelectedItems[0];
                        CreateProducer createProducerWindow = new CreateProducer(producerDataAccess.GetEntityById<Producer>(producer.Id));
                        createProducerWindow.ShowDialog();
                        break;
                }
            }
        }
    }
}