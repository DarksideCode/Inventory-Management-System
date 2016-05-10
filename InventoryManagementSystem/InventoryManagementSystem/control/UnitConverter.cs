using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.control
{
    /// <summary>
    /// Klasse zur Umrechnung von Maßeinheiten
    /// </summary>
    public class UnitConverter
    {
        /// <summary>
        /// Rechnet einen Wert in die nächst höhere Einheit um
        /// </summary>
        /// <param name="value">Der aktuelle Wert</param>
        /// <returns>Berechneter Wert der nächst höheren Einheit</returns>
        static public ulong ConvertToHigher(double value)
        {
            return (ulong) value / 1000;
        }

        /// <summary>
        /// Rechnet einen Wert in die nächst niedrigere Einheit um
        /// </summary>
        /// <param name="value">Der aktuelle Wert</param>
        /// <returns>Berechneter Wert der nächst niedrigeren Einheit</returns>
        static public ulong ConvertToLower(double value)
        {
            return (ulong) value * 1000;
        }
    }
}
