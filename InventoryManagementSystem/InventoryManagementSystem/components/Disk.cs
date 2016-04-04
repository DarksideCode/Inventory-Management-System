using System.Collections.Generic;

namespace InventoryManagementSystem.components
{
    /// <summary>
    /// Klasse für die Entität 'Festplatte'
    /// </summary>
    public class Disk
    {
        private List<PhysicalInterfaceWithCount> physicalInterfaces;

        public Disk() 
        {
            this.physicalInterfaces = new List<PhysicalInterfaceWithCount>();
        }

        public int Id { get; set; }

        public string Description { get; set; }

        public int Capacity { get; set; }

        public bool Ssd { get; set; }

        public double Inch { get; set; }

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
    /// Ein grafisches Objekt der Entität 'Festplatte' zur Anzeige der Informationen auf
    /// der Benutzeroberfläche.
    /// </summary>
    public class DiskGraphicalObject
    {
        public string Description { get; set; }
        public int Capacity { get; set; }
        public string Ssd { get; set; }
        public double Inch { get; set; }
        public string Producer { get; set; }

        public void MapFromEntity(Disk entity)
        {
            this.Description = entity.Description;
            this.Capacity = entity.Capacity;
            this.Ssd = entity.Ssd.ToString();
            this.Inch = entity.Inch;
            this.Producer = entity.Producer.CompanyName;
        }
    }
}