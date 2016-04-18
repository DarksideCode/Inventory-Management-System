using InventoryManagementSystem.components;
using System.Text.RegularExpressions;

namespace InventoryManagementSystem.validation
{
    /// <summary>
    /// Validator-Klasse der Entität 'Grafikkarte'.
    /// Legt die Regeln für die Validierung fest und überprüft mittels regulärer Ausdrückes, ob diese
    /// eingehalten werden.
    /// </summary>
    public class GraphicCardValidator
    {
        private string modelPattern = "^[A-Za-z0-9]*$";

        /// <summary>
        /// Prüft die Konsistenz der Attribute der Entität 'Grafikkarte'
        /// </summary>
        /// <param name="entity">Das Objekt, welches geprüft wird</param>
        /// <returns>true: Objekt Konsistent, false: Objekt fehlerhaft</returns>
        public bool CheckConsistency(GraphicCard entity)
        {
            bool result = true;
            Regex modelReg = new Regex(this.modelPattern);

            if (entity.ClockRate <= 0)
            {
                result = false;
            }

            if (!modelReg.Match(entity.Model).Success)
            {
                result = false;
            }

            if (entity.Memory == 0)
            {
                result = false;
            }

            return result;
        }
    }
}
