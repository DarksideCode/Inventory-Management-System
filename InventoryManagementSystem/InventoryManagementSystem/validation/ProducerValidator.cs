using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagementSystem.components;
using System.Text.RegularExpressions;

namespace InventoryManagementSystem.validation
{
    /*
     *  Validator-Klasse der Entität 'Hersteller'
     *  Legt die Regeln für die Validierung fest und überprüft diese mittels regulärer Ausdrücke
     */
    public class ProducerValidator
    {
        private string emailPattern = "^[a-zA-Z]{1,}.*\\@[a-zA-Z]*\\.(de|com|net|org){1,1}$";
        private string websitePattern = "^(http:\\/\\/){0,1}www\\..*\\.[a-z]{1,4}$";
        private string placePattern = "^[A-Z]{1,}[A-Za-zöäü ]*$";

        /*
         *  Prüft die Konsistenz der Attribute der Entität 'Hersteller'
         */
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

            if(entity.PhoneNumber < 0)
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

            if (entity.HouseNumber < 1)
            {
                result = false;
            }

            return result;
        }
    }
}
