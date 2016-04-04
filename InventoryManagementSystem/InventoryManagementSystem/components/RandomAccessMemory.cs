namespace InventoryManagementSystem.components
{
    /*
     *  Klasse für die Entität Arbeitsspeicher
     */
    public class RandomAccessMemory
    {
        public RandomAccessMemory() { }

        public int Id { get; set; }

        public string Description { get; set; }

        public int Memory { get; set; }

        public double ClockRate { get; set; }

        public Producer Producer { get; set; }
    }
}