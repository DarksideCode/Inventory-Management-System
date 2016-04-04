using System.Collections.Generic;

namespace InventoryManagementSystem.components
{
    /// <summary>
    /// Klasse für die Entität 'Grafikkarte'
    /// </summary>
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

    /// <summary>
    /// Ein grafisches Objekt der Entität 'Grafikkarte' zur Anzeige der Informationen auf
    /// der Benutzeroberfläche.
    /// </summary>
    public class GraphicCardGraphicalObject
    {
        public string Description { get; set; }
        public double ClockRate { get; set; }
        public string Model { get; set; }
        public int Memory { get; set; }
        public string Producer { get; set; }

        public void MapFromEntity(GraphicCard entity)
        {
            this.Description = entity.Description;
            this.ClockRate = entity.ClockRate;
            this.Model = entity.Model;
            this.Memory = entity.Memory;
            this.Producer = entity.Producer.CompanyName;
        }
    }
}