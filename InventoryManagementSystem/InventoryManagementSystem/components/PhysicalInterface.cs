namespace InventoryManagementSystem.components
{
    /// <summary>
    /// Klasse für die Entität 'Schnittstelle'
    /// </summary>
    public class PhysicalInterface
    {
        public PhysicalInterface() { }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool Serial { get; set; }

        public int TransferRate { get; set; }
    }

    /// <summary>
    /// Ein grafisches Objekt der Entität 'Schnittstelle' zur Anzeige der Informationen auf
    /// der Benutzeroberfläche.
    /// </summary>
    public class PhysicalInterfaceGraphicalObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Beschreibung { get; set; }
        public string Seriell { get; set; }
        public int Transferrate { get; set; }

        public void MapFromEntity(PhysicalInterface entity)
        {
            this.Id = entity.Id;
            this.Name = entity.Name;
            this.Beschreibung = entity.Description;
            this.Seriell = entity.Serial.ToString();
            this.Transferrate = entity.TransferRate;
        }
    }
}
