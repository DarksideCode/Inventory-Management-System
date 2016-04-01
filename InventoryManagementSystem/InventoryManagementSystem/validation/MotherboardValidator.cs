using InventoryManagementSystem.components;
using System.Text.RegularExpressions;

namespace InventoryManagementSystem.validation
{
   /*
    *  Validator-Klasse der Entität 'Hauptplatine'
    *  Legt die Regeln für die Validierung fest und überprüft diese mittels regulärer Ausdrücke
    */
    public class MotherboardValidator
    {
        private string socketPattern = "^[A-Za-z0-9\\-]*$";

        /*
         *  Prüft die Konsistenz der Attribute der Entität 'Hauptplatine'
         */
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
