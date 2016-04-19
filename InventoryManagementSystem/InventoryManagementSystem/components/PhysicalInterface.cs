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

        public uint TransferRate { get; set; }
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
        public uint Transferrate { get; set; }

        public void MapFromEntity(PhysicalInterface entity)
        {
            this.Id = entity.Id;
            this.Name = entity.Name;
            this.Beschreibung = entity.Description;
            if (entity.Serial)
                this.Seriell = "Ja";
            else
                this.Seriell = "Nein";
            this.Transferrate = entity.TransferRate;
        }
    }
}
