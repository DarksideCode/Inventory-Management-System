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
        public string Name { get; set; }
        public string Description { get; set; }
        public string Serial { get; set; }
        public int TransferRate { get; set; }

        public void MapFromEntity(PhysicalInterface entity)
        {
            this.Name = entity.Name;
            this.Description = entity.Description;
            this.Serial = entity.Serial.ToString();
            this.TransferRate = entity.TransferRate;
        }
    }
}
