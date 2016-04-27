using System;
using System.Collections.Generic;
using System.Windows;
using InventoryManagementSystem.dataAccess;
using InventoryManagementSystem.components;
using InventoryManagementSystem.validation;
using InventoryManagementSystem.control;

namespace InventoryManagementSystem.presentation.forms
{
    /// <summary>
    /// Interaktionslogik für CreateMonitor.xaml
    /// </summary>
    public partial class CreateMonitor : Window
    {
        public CreateMonitor(Monitor entity = null)
        {
            InitializeComponent();
        }
    }
}
