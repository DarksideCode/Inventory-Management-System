using InventoryManagementSystem.components;
using System.Text.RegularExpressions;

namespace InventoryManagementSystem.validation
{
    /*
     *  Validator-Klasse der Entität 'Prozessor'
     *  Legt die Regeln für die Validierung fest und überprüft diese mittels regulärer Ausdrücke
     */
    public class ProcessorValidator
    {
        private string modelPattern = "^[A-Za-z0-9 ]*$";

        private string commandSetPattern = "^[A-Za-z0-9 \\-]*$";


        /*
         *  Prüft die Konsistenz der Attribute der Entität 'Prozessor'
         */
        public bool CheckConsistency(Processor entity)
        {
            bool result = true;
            Regex modelReg = new Regex(this.modelPattern);
            Regex commandSetReg = new Regex(this.commandSetPattern);

            if(!modelReg.Match(entity.Model).Success)
            {
                result = false;
            }

            if (entity.Core <= 0)
            {
                result = false;
            }

            if(!commandSetReg.Match(entity.CommandSet).Success)
            {
                result = false;
            }

            if (entity.Architecture <= 0)
            {
                result = false;
            }

            if(entity.ClockRate <= 0)
            {
                result = false;
            }

            return result;
        }
    }
}
