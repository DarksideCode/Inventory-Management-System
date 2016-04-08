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
        public int Id { get; set; }
        public string Beschreibung { get; set; }
        public string Modell { get; set; }
        public int Kerne { get; set; }
        public string Befehlssatz { get; set; }
        public int Architektur { get; set; }
        public double Taktrate { get; set; }
        public string Hersteller { get; set; }

        public void MapFromEntity (Processor entity)
        {
            this.Id = entity.Id;
            this.Beschreibung = entity.Description;
            this.Modell = entity.Model;
            this.Kerne = entity.Core;
            this.Befehlssatz = entity.CommandSet;
            this.Architektur = entity.Architecture;
            this.Taktrate = entity.ClockRate;
            this.Hersteller = entity.Producer.CompanyName;
        }
    }
}
