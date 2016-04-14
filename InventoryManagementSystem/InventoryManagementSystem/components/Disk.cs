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
        public int Id { get; set; }
        public string Beschreibung { get; set; }
        public int Kapazität { get; set; }
        public string SSD { get; set; }
        public double Zoll { get; set; }
        public string Hersteller { get; set; }
        public string Schnittstellen { get; set; }

        public void MapFromEntity(Disk entity)
        {
            this.Id = entity.Id;
            this.Beschreibung = entity.Description;
            this.Kapazität = entity.Capacity;
            this.SSD = entity.Ssd.ToString();
            this.Zoll = entity.Inch;
            this.Hersteller = entity.Producer.CompanyName;
            string schnittstellen = "";
            for (int i = 0; i < entity.PhysicalInterfaces.Count; i++ )
            {
                schnittstellen += entity.PhysicalInterfaces[i].PhysicalInterface.Name;
                schnittstellen += " (" + entity.PhysicalInterfaces[i].Number + ")";
                if(i > 0)
                    schnittstellen += ", ";
            }
            this.Schnittstellen = schnittstellen;
        }
    }
}