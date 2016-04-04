using System.Collections.Generic;

namespace InventoryManagementSystem.components
{
    /*
     *  Klasse für die Entität Hauptplatine
     */
    public class Motherboard
    {
        private List<PhysicalInterfaceWithCount> physicalInterfaces;

        public Motherboard() 
        {
            this.physicalInterfaces = new List<PhysicalInterfaceWithCount>();
        }

        public int Id { get; set; }

        public string Description { get; set; }

        public double Inch { get; set; }

        public string Socket { get; set; }

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