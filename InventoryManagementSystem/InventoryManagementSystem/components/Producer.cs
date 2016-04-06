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

        public int PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Website { get; set; }

        public int PostalCode { get; set; }

        public string Place { get; set; }

        public string Street { get; set; }

        public int HouseNumber { get; set; }
    }

    /// <summary>
    /// Ein grafisches Objekt der Entität 'Hersteller' zur Anzeige der Informationen auf
    /// der Benutzeroberfläche.
    /// </summary>
    public class ProducerGraphicalObject
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public int PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public int PostalCode { get; set; }
        public string Place { get; set; }
        public string Street { get; set; }
        public int HouseNumber { get; set; }

        public void MapFromEntity (Producer entity)
        {
            this.Id = entity.Id;
            this.CompanyName = entity.CompanyName;
            this.PhoneNumber = entity.PhoneNumber;
            this.Email = entity.Email;
            this.Website = entity.Website;
            this.PostalCode = entity.PostalCode;
            this.Place = entity.Place;
            this.Street = entity.Street;
            this.HouseNumber = entity.HouseNumber;
        }
    }
}