using InventoryManagementSystem.components;

namespace InventoryManagementSystem.validation
{
    /// <summary>
    /// Validator-Klasse der Entität 'Monitor'.
    /// Legt die Regeln für die Validierung fest und überprüft mittels regulärer Ausdrückes, ob diese
    /// eingehalten werden.
    /// </summary>
    public class MonitorValidator
    {
        /// <summary>
        /// Prüft die Konsistenz der Attribute der Entität 'Monitor'
        /// </summary>
        /// <param name="entity">Das Objekt, welches geprüft wird</param>
        /// <returns>true: Objekt Konsistent, false: Objekt fehlerhaft</returns>
        public bool CheckConsistency(Monitor entity)
        {
            bool result = true;
            
            if(entity.Resolution == 0)
            {
                result = false;
            }

            if(entity.Inch <= 0)
            {
                result = false;
            }

            if(entity.AspectRatio == 0)
            {
                result = false;
            }

            return result;
        }
    }
}
