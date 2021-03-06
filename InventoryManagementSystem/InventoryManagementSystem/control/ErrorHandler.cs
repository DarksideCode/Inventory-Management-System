﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InventoryManagementSystem.control
{
    /// <summary>
    /// Stellt Methoden zur Anzeige von Fehlermeldungen zur Verfügung und bietet eine Auswahl von passenden
    /// Fehlermeldungen für verschiedene Situationen
    /// </summary>
    public class ErrorHandler
    {
        public static readonly string VALIDATION_FAILED = "Die eingegebenen Daten sind inkonsistent. Bitte überprüfen Sie Ihre Eingaben!";
        public static readonly string SAVE_WENT_WRONG = "Die eingegebenen Daten sind inkonsistent. Bitte überprüfen Sie Ihre Eingaben!";
        public static readonly string WRONG_FORMAT = "Es wurde ein ungültiges Format eingegeben oder ein Feld wurde leer gelassen. Bitte überprüfen Sie Ihre Eingaben!";
        public static readonly string DATA_TOO_LONG = "Die eingegebenen Daten sind zu lang.";
        public static readonly string ENTITY_STILL_REFERENCED = "Das ausgewählte Element kann nicht gelöscht werden, da noch eine Referenz darauf besteht.";
        public static readonly string NO_DATABASE_CONNECTION = "Es konnte keine Verbindung zur Datenbank hergestellt werden. Bitte überprüfen Sie Ihre Einstellungen!";

        /// <summary>
        /// Öffnet eine MessageBox mit der übergebenen Fehlermeldung.
        /// </summary>
        /// <param name="exception">Die Exception, welche ausgelöst wurde</param>
        /// <param name="message">Die Fehlermeldung, welche angezeigt wird</param>
        public static void ShowErrorMessage(Exception exception, string message)
        {
            MessageBox.Show(message, exception.GetType().Name, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// Öffnet eine MessageBox mit dem übergebenen Titel und der Fehlermeldung
        /// </summary>
        /// <param name="title">Der Titel des Fehlers, welcher der Titel der Fehlermeldung ist</param>
        /// <param name="message">Die Fehlermeldung, welche angezeigt wird</param>
        public static void ShowErrorMessage(string title, string message)
        {
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
