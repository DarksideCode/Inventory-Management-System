using InventoryManagementSystem.components;

namespace InventoryManagementSystem.validation
{
    /// <summary>
    /// Validator-Klasse der Entität 'Arbeitsspeicher'.
    /// Legt die Regeln für die Validierung fest und überprüft mittels regulärer Ausdrückes, ob diese
    /// eingehalten werden.
    /// </summary>
    public class RandomAccessMemoryValidator
    {
        /// <summary>
        /// Prüft die Konsistenz der Attribute der Entität 'Arbeitsspeicher'
        /// </summary>
        /// <param name="entity">Das Objekt, welches geprüft wird</param>
        /// <returns>true: Objekt Konsistent, false: Objekt fehlerhaft</returns>
        public bool CheckConsistency(RandomAccessMemory entity)
        {
            bool result = true;

            if(entity.Memory <= 0)
            {
                result = false;
            }

            if (entity.ClockRate <= 0)
            {
                result = false;
            }

            return result;
        }
    }
}
