using InventoryManagementSystem.components;
using System.Text.RegularExpressions;

namespace InventoryManagementSystem.validation
{
    /// <summary>
    /// Validator-Klasse der Entität 'Hersteller'.
    /// Legt die Regeln für die Validierung fest und überprüft mittels regulärer Ausdrückes, ob diese
    /// eingehalten werden.
    /// </summary>
    public class ProducerValidator
    {
        private string emailPattern = "^[a-zA-Z]{1,}.*\\@[a-zA-Z]*\\.(de|com|net|org){1,1}$";
        private string websitePattern = "^(http:\\/\\/|https:\\/\\/){0,1}www\\..{1,}\\.[a-z]{1,4}(\\/){0,1}$";
        private string placePattern = "^[A-Z]{1,}[A-Za-zöäü ]*$";

        /// <summary>
        /// Prüft die Konsistenz der Attribute der Entität 'Hersteller'
        /// </summary>
        /// <param name="entity">Das Objekt, welches geprüft wird</param>
        /// <returns>true: Objekt Konsistent, false: Objekt fehlerhaft</returns>
        public bool CheckConsistency(Producer entity)
        {
            bool result = true;
            Regex emailReg = new Regex(this.emailPattern, RegexOptions.IgnoreCase);
            Regex websiteReg = new Regex(this.websitePattern, RegexOptions.IgnoreCase);
            Regex placeReg = new Regex(this.placePattern);

            if(entity.CompanyName.Length == 0)
            {
                result = false;
            }

            if(entity.PhoneNumber == 0)
            {
                result = false;
            }

            if(!emailReg.Match(entity.Email).Success)
            {
                result = false;
            }

            if(!websiteReg.Match(entity.Website).Success)
            {
                result = false;
            }

            if(entity.PostalCode > 99999999 || entity.PostalCode <= 999)
            {
                result = false;
            }

            if(!placeReg.Match(entity.Place).Success)
            {
                result = false;
            }

            if(entity.Street.Length == 0)
            {
                result = false;
            }

            if (entity.HouseNumber == 0)
            {
                result = false;
            }

            return result;
        }
    }
}
