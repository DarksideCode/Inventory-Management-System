using System.Collections.Generic;

namespace InventoryManagementSystem.components
{
    /*
     *  Klasse für die Entität Festplatte
     */
    public class Disk
    {
        private int id;

        private string description;

        private int capacity;

        private bool ssd;

        private double inch;

        private Producer producer;

        private List<PhysicalInterfaceWithCount> physicalInterfaces;

        public Disk() 
        {
            this.physicalInterfaces = new List<PhysicalInterfaceWithCount>();
        }

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

        public int Capacity
        {
            get { return this.capacity; }
            set { this.capacity = value; }
        }

        public bool Ssd
        {
            get { return this.ssd; }
            set { this.ssd = value; }
        }

        public double Inch
        {
            get { return this.inch; }
            set { this.inch = value; }
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
