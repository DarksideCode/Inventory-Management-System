using InventoryManagementSystem.components;
using System.Text.RegularExpressions;

namespace InventoryManagementSystem.validation
{
    /// <summary>
    /// Validator-Klasse der Entität 'Monitor'.
    /// Legt die Regeln für die Validierung fest und überprüft mittels regulärer Ausdrückes, ob diese
    /// eingehalten werden.
    /// </summary>
    public class MonitorValidator
    {
        private string resolutionPattern = "^[0-9 \\*x]*$";
        private string aspectRatioPattern = "^[0-9 \\:]*$";

        /// <summary>
        /// Prüft die Konsistenz der Attribute der Entität 'Monitor'
        /// </summary>
        /// <param name="entity">Das Objekt, welches geprüft wird</param>
        /// <returns>true: Objekt Konsistent, false: Objekt fehlerhaft</returns>
        public bool CheckConsistency(Monitor entity)
        {
            bool result = true;
            Regex resolutionReg = new Regex(this.resolutionPattern);
            Regex aspectRatioReg = new Regex(this.aspectRatioPattern);
            
            if(!resolutionReg.Match(entity.Resolution).Success)
            {
                result = false;
            }

            if(entity.Inch <= 0)
            {
                result = false;
            }

            if(!aspectRatioReg.Match(entity.AspectRatio).Success)
            {
                result = false;
            }

            return result;
        }
    }
}
