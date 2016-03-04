namespace InventoryManagementSystem.components
{
    /*
     *  Klasse für die Entität Prozessor
     */
    public class Processor
    {
        private int id;

        private string description;

        private string model;

        private int core;

        private string commandSet;

        private int architecture;

        private double clockRate;

        private Producer producer;

        public Processor() { }

        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }

        public string Model 
        {
            get { return this.model; }
            set { this.model = value; }
        }

        public int Core
        {
            get { return this.core; }
            set { this.core = value; }
        }

        public string CommandSet
        {
            get { return this.commandSet; }
            set { this.commandSet = value; }
        }

        public int Architecture
        {
            get { return this.architecture; }
            set { this.architecture = value; }
        }

        public double ClockRate
        {
            get { return this.clockRate; }
            set { this.clockRate = value; }
        }

        public Producer Producer
        {
            get { return this.producer; }
            set { this.producer = value; }
        }
    }
}
