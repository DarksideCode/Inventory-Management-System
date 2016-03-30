using System.Collections.Generic;

namespace InventoryManagementSystem.components
{
    /*
     *  Klasse für die Entität Monitor
     */
    public class Monitor
    {
        private int id;

        private string description;

        private int resolution;

        private double inch;

        private int aspectRatio;

        private Producer producer;

        private List<PhysicalInterfaceWithCount> physicalInterfaces;


        public Monitor() 
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

        public int Resolution
        {
            get { return this.resolution; }
            set { this.resolution = value; }
        }

        public double Inch
        {
            get { return this.inch; }
            set { this.inch = value; }
        }

        public int AspectRatio
        {
            get { return this.aspectRatio; }
            set { this.aspectRatio = value; }
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
