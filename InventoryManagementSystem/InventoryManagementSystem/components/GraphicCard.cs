using System.Collections.Generic;

namespace InventoryManagementSystem.components
{
    /*
     *  Klasse für die Entität Grafikkarte
     */
    public class GraphicCard
    {
        private List<PhysicalInterfaceWithCount> physicalInterfaces;

        public GraphicCard()
        {
            this.physicalInterfaces = new List<PhysicalInterfaceWithCount>();
        }

        public int Id { get; set; }

        public string Description { get; set; }

        public double ClockRate { get; set; }

        public string Model { get; set; }

        public int Memory { get; set; }

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