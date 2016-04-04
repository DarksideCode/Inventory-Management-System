namespace InventoryManagementSystem.components
{
    /// <summary>
    /// Klasse für die Entität 'Prozessor'
    /// </summary>
    public class Processor
    {
        public Processor() { }

        public int Id { get; set; }

        public string Description { get; set; }

        public string Model { get; set; }

        public int Core { get; set; }

        public string CommandSet { get; set; }

        public int Architecture { get; set; }

        public double ClockRate { get; set; }

        public Producer Producer { get; set; }
    }

    /// <summary>
    /// Ein grafisches Objekt der Entität 'Prozessor' zur Anzeige der Informationen auf
    /// der Benutzeroberfläche.
    /// </summary>
    public class ProcessorGraphicalObject
    {
        public string Description { get; set; }
        public string Model { get; set; }
        public int Core { get; set; }
        public string CommandSet { get; set; }
        public int Architecture { get; set; }
        public double ClockRate { get; set; }
        public string Producer { get; set; }

        public void MapFromEntity (Processor entity)
        {
            this.Description = entity.Description;
            this.Model = entity.Model;
            this.Core = entity.Core;
            this.CommandSet = entity.CommandSet;
            this.Architecture = entity.Architecture;
            this.ClockRate = entity.ClockRate;
            this.Producer = entity.Producer.CompanyName;
        }
    }
}
