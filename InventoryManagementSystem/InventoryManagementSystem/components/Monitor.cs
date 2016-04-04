using System.Collections.Generic;

namespace InventoryManagementSystem.components
{
    /// <summary>
    /// Klasse für die Entität 'Monitor'
    /// </summary>
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

    /// <summary>
    /// Ein grafisches Objekt der Entität 'Monitor' zur Anzeige der Informationen auf
    /// der Benutzeroberfläche.
    /// </summary>
    public class MonitorGraphicalObject
    {
        public string Description { get; set; }
        public int Resolution { get; set; }
        public double Inch { get; set; }
        public int AspectRatio { get; set; }
        public string Producer { get; set; }

        public void MapFromEntity(Monitor entity)
        {
            this.Description = entity.Description;
            this.Resolution = entity.Resolution;
            this.Inch = entity.Inch;
            this.AspectRatio = entity.AspectRatio;
            this.Producer = entity.Producer.CompanyName;
        }
    }
}