using InventoryManagementSystem.components;
using System.Text.RegularExpressions;

namespace InventoryManagementSystem.validation
{
    /// <summary>
    /// Validator-Klasse der Entität 'Schnittstelle'.
    /// Legt die Regeln für die Validierung fest und überprüft mittels regulärer Ausdrückes, ob diese
    /// eingehalten werden.
    /// </summary>
    public class PhysicalInterfaceValidator
    {
        private string namePattern = "^[A-Za-z0-9 \\-\\.]*$";

        /// <summary>
        /// Prüft die Konsistenz der Attribute der Entität 'Schnittstelle'
        /// </summary>
        /// <param name="entity">Das Objekt, welches geprüft wird</param>
        /// <returns>true: Objekt Konsistent, false: Objekt fehlerhaft</returns>
        public bool CheckConsistency(PhysicalInterface entity)
        {
            bool result = true;
            Regex nameReg = new Regex(this.namePattern);

            if(!nameReg.Match(entity.Name).Success)
            {
                result = false;
            }

            if(entity.TransferRate == 0)
            {
                result = false;
            }

            return result;
        }
    }
}
