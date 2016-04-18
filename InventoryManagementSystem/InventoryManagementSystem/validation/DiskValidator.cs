using InventoryManagementSystem.components;

namespace InventoryManagementSystem.validation
{
    /// <summary>
    /// Validator-Klasse der Entität 'Festplatte'.
    /// Legt die Regeln für die Validierung fest und überprüft mittels regulärer Ausdrückes, ob diese
    /// eingehalten werden.
    /// </summary>
    public class DiskValidator
    {
        /// <summary>
        /// Prüft die Konsistenz der Attribute der Entität 'Festplatte'
        /// </summary>
        /// <param name="entity">Das Objekt, welches geprüft wird</param>
        /// <returns>true: Objekt Konsistent, false: Objekt fehlerhaft</returns>
        public bool CheckConsistency(Disk entity)
        {
            bool result = true;

            if (entity.Capacity == 0)
            {
                result = false;
            }

            if(entity.Inch <= 0)
            {
                result = false;
            }

            return result;
        }
    }
}
