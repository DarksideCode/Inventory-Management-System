using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagementSystem.components;
using System.Text.RegularExpressions;

namespace InventoryManagementSystem.validation
{
    /*
     *  Validator-Klasse der Entität 'Grafikkarte'
     *  Legt die Regeln für die Validierung fest und überprüft diese mittels regulärer Ausdrücke
     */
    public class GraphicCardValidator
    {
        private string modelPattern = "^[A-Za-z0-9]*$";

        /*
         *  Prüft die Konsistenz der Attribute der Entität 'Grafikkarte'
         */
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

            if (entity.Memory <= 0)
            {
                result = false;
            }

            return result;
        }
    }
}
