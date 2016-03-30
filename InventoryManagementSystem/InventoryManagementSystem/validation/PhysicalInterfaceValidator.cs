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
     *  Validator-Klasse der Entität 'Schnittstelle'
     *  Legt die Regeln für die Validierung fest und überprüft diese mittels regulärer Ausdrücke
     */
    public class PhysicalInterfaceValidator
    {
        private string namePattern = "^[A-Za-z0-9 \\-\\.]*$";

        /*
         *  Prüft die Konsistenz der Attribute der Entität 'Schnittstelle'
         */
        public bool CheckConsistency(PhysicalInterface entity)
        {
            bool result = true;
            Regex nameReg = new Regex(this.namePattern);

            if(!nameReg.Match(entity.Name).Success)
            {
                result = false;
            }

            if(entity.Serial == null)
            {
                result = false;
            }

            if(entity.TransferRate <= 0)
            {
                result = false;
            }

            return result;
        }
    }
}
