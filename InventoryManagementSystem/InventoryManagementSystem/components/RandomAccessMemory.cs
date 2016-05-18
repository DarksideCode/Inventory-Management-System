namespace InventoryManagementSystem.components
{
    /// <summary>
    /// Klasse für die Entität 'Arbeitsspeicher'
    /// </summary>
    public class RandomAccessMemory
    {
        public RandomAccessMemory() { }

        public int Id { get; set; }

        public string Description { get; set; }

        public ulong Memory { get; set; }

        public double ClockRate { get; set; }

        public Producer Producer { get; set; }
    }

    /// <summary>
    /// Ein grafisches Objekt der Entität 'Arbeitsspeicher' zur Anzeige der Informationen auf
    /// der Benutzeroberfläche.
    /// </summary>
    public class RandomAccessMemoryGraphicalObject
    {
        public int Id { get; set; }
        public string Beschreibung { get; set; }
        public ulong Speicher { get; set; }
        public double Taktrate { get; set; }
        public string Hersteller { get; set; }

        /// <summary>
        /// Wandelt ein Objekt der Entität 'Arbeitsspeicher' in eine grafisches Objekt.
        /// </summary>
        /// <param name="entity">Objekt vom Typ 'Arbeitsspeicher'</param>
        public void MapFromEntity(RandomAccessMemory entity)
        {
            this.Id = entity.Id;
            this.Beschreibung = entity.Description;
            this.Speicher = entity.Memory;
            this.Taktrate = entity.ClockRate;
            this.Hersteller = entity.Producer.CompanyName;
        }
    }
}