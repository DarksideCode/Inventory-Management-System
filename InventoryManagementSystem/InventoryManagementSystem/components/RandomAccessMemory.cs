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

        public int Memory { get; set; }

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
        public string Description { get; set; }
        public int Memory { get; set; }
        public double ClockRate { get; set; }
        public string Producer { get; set; }

        public void MapFromEntity(RandomAccessMemory entity)
        {
            this.Id = entity.Id;
            this.Description = entity.Description;
            this.Memory = entity.Memory;
            this.ClockRate = entity.ClockRate;
            this.Producer = entity.Producer.CompanyName;
        }
    }
}