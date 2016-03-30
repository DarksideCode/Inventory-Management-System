using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagementSystem.components;

namespace InventoryManagementSystem.validation
{
    /*
     *  Validator-Klasse der Entität 'Festplatte'
     *  Legt die Regeln für die Validierung fest und überprüft diese mittels regulärer Ausdrücke
     */
    public class DiskValidator
    {
        /*
         *  Prüft die Konsistenz der Attribute der Entität 'Festplatte'
         */
        public bool CheckConsistency(Disk entity)
        {
            bool result = true;
            
            if(entity.Capacity <= 0)
            {
                result = false;
            }

            if (entity.Ssd == null)
            {
                result = false;
            }

            if(entity.Inch <= 0)
            {
                result = false;
            }

            return result;
        }
    }
}
