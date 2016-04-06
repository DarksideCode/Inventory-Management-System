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
        public MainWindow()
        {
            InitializeComponent();
            RandomAccessMemoryDataAccess dataAccess = new RandomAccessMemoryDataAccess();
            GraphicalObjectMapper mapper = new GraphicalObjectMapper();

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
    }
}
