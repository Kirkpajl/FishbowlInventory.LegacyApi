using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FishbowlInventory.Serialization;
using System.Threading.Tasks;

namespace FishbowlInventory.Models
{
    /// <summary>
    /// Stores credit card information to be used in a credit card payment.
    /// </summary>
    public class CreditCard
    {
        [CsvPropertyName("cardNumber")]
        public string Number { get; set; }
        
        [CsvPropertyName("cardExpMonth")]
        public int ExpMonth { get; set; }

        [CsvPropertyName("cardExpYear")]
        public int ExpYear { get; set; }

        [CsvPropertyName("securityCode")]
        public string SecurityCode { get; set; }

        [CsvPropertyName("nameOnCard")]
        public string NameOnCard { get; set; }

        [CsvPropertyName("cardAddress")]
        public string Address { get; set; }

        [CsvPropertyName("cardZipCode")]
        public string ZipCode { get; set; }

        [CsvPropertyName("cardCountryCode")]
        public string CountryCode { get; set; }
    }
}
