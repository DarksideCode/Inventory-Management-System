namespace InventoryManagementSystem.components
{
    /*
     *  Klasse für die Entität Schnittstelle
     */
    public class PhysicalInterface
    {
        public PhysicalInterface() { }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool Serial { get; set; }

        public int TransferRate { get; set; }
    }
}
