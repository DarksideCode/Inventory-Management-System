using System.Collections.Generic;

namespace InventoryManagementSystem.components
{
    /*
     *  Klasse für die Entität Hauptplatine
     */
    public class Motherboard
    {
        private int id;

        private string description;

        private double inch;

        private string socket;

        private Producer producer;

        private List<PhysicalInterfaceWithCount> physicalInterfaces = new List<PhysicalInterfaceWithCount>();


        public Motherboard() { }

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

        public double Inch
        {
            get { return this.inch; }
            set { this.inch = value; }
        }

        public string Socket
        {
            get { return this.socket; }
            set { this.socket = value; }
        }

        public Producer Producer
        {
            get { return this.producer; }
            set { this.producer = value; }
        }

        public List<PhysicalInterfaceWithCount> PhysicalInterfaces
        {
            get { return this.physicalInterfaces; }
            set { this.physicalInterfaces = value; }
        }

        public void AddPhysicalInterface(PhysicalInterfaceWithCount physicalInterface)
        {
            this.physicalInterfaces.Add(physicalInterface);
        }
    }
}
