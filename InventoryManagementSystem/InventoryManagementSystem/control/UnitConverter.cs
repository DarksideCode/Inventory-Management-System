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
        /// Rechnet einen Byte-Wert in Kilobyte (KB) um
        /// </summary>
        /// <param name="value">Bytewert</param>
        /// <returns>Parameter in Kilobyte</returns>
        static public ulong ByteToKiloByte(double value)
        {
            return (ulong) value / 1000;
        }

        /// <summary>
        /// Rechnet einen Byte-Wert in Megabyte (MB) um
        /// </summary>
        /// <param name="value">Bytewert</param>
        /// <returns>Parameter in Megabyte</returns>
        static public ulong ByteToMegaByte(double value)
        {
            return (ulong) value / (1000 * 1000);
        }

        /// <summary>
        /// Rechnet einen Byte-Wert in Gigabyte (GB) um
        /// </summary>
        /// <param name="value">Bytewert</param>
        /// <returns>Parameter in Gigabyte</returns>
        static public ulong ByteToGigaByte(double value)
        {
            return (ulong) value / (1000 * 1000 * 1000);
        }

        /// <summary>
        /// Rechnet einen Kilobyte-Wert (KB) in Byte um
        /// </summary>
        /// <param name="value">Kilobytewert</param>
        /// <returns>Parameter in Byte</returns>
        static public ulong KiloByteToByte(double value)
        {
            return (ulong) value * 1000;
        }

        /// <summary>
        /// Rechnet einen Megabyte-Wert (MB) in Byte um
        /// </summary>
        /// <param name="value">Megabytewert</param>
        /// <returns>Parameter in Byte</returns>
        static public ulong MegaByteToByte(double value)
        {
            return (ulong) value * 1000 * 1000;
        }

        /// <summary>
        /// Rechnet einen Gigabyte-Wert (GB) in Byte um
        /// </summary>
        /// <param name="value">Gigabyte-Wert</param>
        /// <returns>Parameter in Byte</returns>
        static public ulong GigaByteToByte(double value)
        {
            return (ulong) value * 1000 * 1000 * 1000;
        }
    }
}
