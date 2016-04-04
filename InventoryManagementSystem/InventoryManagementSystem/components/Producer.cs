namespace InventoryManagementSystem.components
{
    /*
     *  Klasse für die Entität Hersteller
     */
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
}