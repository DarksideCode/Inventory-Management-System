namespace InventoryManagementSystem.components
{
    /*
     *  Klasse für die Entität Prozessor
     */
    public class Processor
    {
        public Processor() { }

        public int Id { get; set; }

        public string Description { get; set; }

        public string Model { get; set; }

        public int Core { get; set; }

        public string CommandSet { get; set; }

        public int Architecture { get; set; }

        public double ClockRate { get; set; }

        public Producer Producer { get; set; }
    }
}
