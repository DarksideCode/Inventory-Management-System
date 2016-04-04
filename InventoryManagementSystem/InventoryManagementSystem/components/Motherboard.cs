using System.Collections.Generic;

namespace InventoryManagementSystem.components
{
    /// <summary>
    /// Klasse für die Entität 'Hauptplatine'
    /// </summary>
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

    /// <summary>
    /// Ein grafisches Objekt der Entität 'Hauptplatine' zur Anzeige der Informationen auf
    /// der Benutzeroberfläche.
    /// </summary>
    public class MotherboardGraphicalObject
    {
        public string Description { get; set; }
        public double Inch { get; set; }
        public string Socket { get; set; }
        public string Producer { get; set; }

        public void MapFromEntity (Motherboard entity)
        {
            this.Description = entity.Description;
            this.Inch = entity.Inch;
            this.Socket = entity.Socket;
            this.Producer = entity.Producer.CompanyName;
        }
    }
}