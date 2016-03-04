namespace InventoryManagementSystem.components
{
    /*
     *  Klasse für die Entität Hersteller
     */
    public class Producer
    {
        private int id;

        private string companyName;

        private int phoneNumber;

        private string email;

        private string website;

        private int postalCode;

        private string place;

        private string street;

        private int houseNumber;

        public Producer() { }

        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public string CompanyName
        {
            get { return this.companyName; }
            set { this.companyName = value; }
        }

        public int PhoneNumber
        {
            get { return this.phoneNumber; }
            set { this.phoneNumber = value; }
        }

        public string Email
        {
            get { return this.email; }
            set { this.email = value; }
        }

        public string Website
        {
            get { return this.website; }
            set { this.website = value; }
        }

        public int PostalCode
        {
            get { return this.postalCode; }
            set { this.postalCode = value; }
        }

        public string Place
        {
            get { return this.place; }
            set { this.place = value; }
        }

        public string Street
        {
            get { return this.street; }
            set { this.street = value; }
        }

        public int HouseNumber
        {
            get { return this.houseNumber; }
            set { this.houseNumber = value; }
        }

    }
}
