namespace InventoryManagementSystem.components
{
    /// <summary>
    /// Klasse für die Entität 'Hersteller'
    /// </summary>
    public class Producer
    {
        public Producer() { }

        public int Id { get; set; }

        public string CompanyName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Website { get; set; }

        public uint PostalCode { get; set; }

        public string Place { get; set; }

        public string Street { get; set; }

        public uint HouseNumber { get; set; }
    }

    /// <summary>
    /// Ein grafisches Objekt der Entität 'Hersteller' zur Anzeige der Informationen auf
    /// der Benutzeroberfläche.
    /// </summary>
    public class ProducerGraphicalObject
    {
        public int Id { get; set; }
        public string Firma { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public string Webseite { get; set; }
        public uint PLZ { get; set; }
        public string Ort { get; set; }
        public string Straße { get; set; }
        public uint Hausnummer { get; set; }

        public void MapFromEntity (Producer entity)
        {
            this.Id = entity.Id;
            this.Firma = entity.CompanyName;
            this.Telefon = entity.PhoneNumber;
            this.Email = entity.Email;
            this.Webseite = entity.Website;
            this.PLZ = entity.PostalCode;
            this.Ort = entity.Place;
            this.Straße = entity.Street;
            this.Hausnummer = entity.HouseNumber;
        }
    }
}