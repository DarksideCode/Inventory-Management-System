using InventoryManagementSystem.components;

namespace InventoryManagementSystem.validation
{
    /*
     *  Validator-Klasse der Entität 'Arbeitsspeicher'
     *  Legt die Regeln für die Validierung fest und überprüft diese mittels regulärer Ausdrücke
     */
    public class RandomAccessMemoryValidator
    {
        /*
         *  Prüft die Konsistenz der Attribute der Entität 'Arbeitsspeicher'
         */
        public bool CheckConsistency(RandomAccessMemory entity)
        {
            bool result = true;

            if(entity.Memory <= 0)
            {
                result = false;
            }

            if (entity.ClockRate <= 0)
            {
                result = false;
            }

            return result;
        }
    }
}
