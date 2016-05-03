using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InventoryManagementSystem.control
{
    public class ErrorHandler
    {
        public static string VALIDATION_FAILED = "Die eingegebenen Daten sind inkonsistent. Bitte überprüfen Sie Ihre Eingaben!";
        public static string SAVE_WENT_WRONG = "Die eingegebenen Daten sind inkonsistent. Bitte überprüfen Sie Ihre Eingaben!";
        public static string WRONG_FORMAT = "Es wurde ein ungültiges Format eingegeben oder ein Feld wurde leer gelassen. Bitte überprüfen Sie Ihre Eingaben!";
        public static string DATA_TOO_LONG = "Die eingegebenen Daten sind zu lang.";

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
