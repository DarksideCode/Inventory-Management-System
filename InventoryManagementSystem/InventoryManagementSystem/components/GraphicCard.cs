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
        public int Id { get; set; }
        public string Beschreibung { get; set; }
        public double Taktrate { get; set; }
        public string Modell { get; set; }
        public int Speicher { get; set; }
        public string Hersteller { get; set; }
        public string Schnittstellen { get; set; }

        public void MapFromEntity(GraphicCard entity)
        {
            this.Id = entity.Id;
            this.Beschreibung = entity.Description;
            this.Taktrate = entity.ClockRate;
            this.Modell = entity.Model;
            this.Speicher = entity.Memory;
            this.Hersteller = entity.Producer.CompanyName;
            string schnittstellen = "";
            for (int i = 0; i < entity.PhysicalInterfaces.Count; i++)
            {
                schnittstellen += entity.PhysicalInterfaces[i].PhysicalInterface.Name;
                schnittstellen += " (" + entity.PhysicalInterfaces[i].Number + ")";
                if (i > 0)
                    schnittstellen += ", ";
            }
            this.Schnittstellen = schnittstellen;
        }
    }
}