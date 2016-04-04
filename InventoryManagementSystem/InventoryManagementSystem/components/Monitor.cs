using System.Collections.Generic;

namespace InventoryManagementSystem.components
{
    /*
     *  Klasse für die Entität Monitor
     */
    public class Monitor
    {
        private List<PhysicalInterfaceWithCount> physicalInterfaces;

        public Monitor() 
        {
            this.physicalInterfaces = new List<PhysicalInterfaceWithCount>();
        }

        public int Id { get; set; }

        public string Description { get; set; }

        public int Resolution { get; set; }

        public double Inch { get; set; }

        public int AspectRatio { get; set; }

        public Producer Producer { get; set; }

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