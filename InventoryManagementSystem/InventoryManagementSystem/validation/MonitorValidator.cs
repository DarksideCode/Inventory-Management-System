using InventoryManagementSystem.components;

namespace InventoryManagementSystem.validation
{
    /*
     *  Validator-Klasse der Entität 'Monitor'
     *  Legt die Regeln für die Validierung fest und überprüft diese mittels regulärer Ausdrücke
     */
    public class MonitorValidator
    {
        /*
         *  Prüft die Konsistenz der Attribute der Entität 'Monitor'
         */
        public bool CheckConsistency(Monitor entity)
        {
            bool result = true;
            
            if(entity.Resolution <= 0)
            {
                result = false;
            }

            if(entity.Inch <= 0)
            {
                result = false;
            }

            if(entity.AspectRatio <= 0)
            {
                result = false;
            }

            return result;
        }
    }
}
