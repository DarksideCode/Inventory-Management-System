using InventoryManagementSystem.components;
using System.Text.RegularExpressions;

namespace InventoryManagementSystem.validation
{
    /// <summary>
    /// Validator-Klasse der Entität 'Hauptplatine'.
    /// Legt die Regeln für die Validierung fest und überprüft mittels regulärer Ausdrückes, ob diese
    /// eingehalten werden.
    /// </summary>
    public class MotherboardValidator
    {
        private string socketPattern = "^[A-Za-z0-9\\-]*$";

        /// <summary>
        /// Prüft die Konsistenz der Attribute der Entität 'Hauptplatine'
        /// </summary>
        /// <param name="entity">Das Objekt, welches geprüft wird</param>
        /// <returns>true: Objekt Konsistent, false: Objekt fehlerhaft</returns>
        public bool CheckConsistency(Motherboard entity)
        {
            bool result = true;
            Regex socketReg = new Regex(this.socketPattern);

            if (!socketReg.Match(entity.Socket).Success)
            {
                result = false;
            }

            if (entity.Inch <= 0)
            {
                result = false;
            }

            return result;
        }
    }
}
