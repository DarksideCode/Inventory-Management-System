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
        public int Id { get; set; }
        public string Beschreibung { get; set; }
        public int Auflösung { get; set; }
        public double Zoll { get; set; }
        public int Seitenverhältnis { get; set; }
        public string Hersteller { get; set; }

        public void MapFromEntity(Monitor entity)
        {
            this.Id = entity.Id;
            this.Beschreibung = entity.Description;
            this.Auflösung = entity.Resolution;
            this.Zoll = entity.Inch;
            this.Seitenverhältnis = entity.AspectRatio;
            this.Hersteller = entity.Producer.CompanyName;
        }
    }
}