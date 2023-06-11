using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FishbowlInventory.Serialization;
using System.Threading.Tasks;
using FishbowlInventory.Enumerations;

namespace FishbowlInventory.Models
{
    /// <summary>
    /// An object containing information about an address. There are no fields that are essential for the use of an address object. Use only those fields that you need.
    /// </summary>
    public class Address
    {
        [CsvPropertyName("id")]
        public int Id { get; set; }

        [CsvPropertyName("temp-Account")]
        public TempAccount TempAccount { get; set; }

        /// <summary>
        /// The name on the address.
        /// </summary>
        [CsvPropertyName("name")]
        public string Name { get; set; }

        [CsvPropertyName("attn")]
        public string Attention { get; set; }

        /// <summary>
        /// The street line.
        /// </summary>
        [CsvPropertyName("street")]
        public string StreetAddress { get; set; }

        /// <summary>
        /// The city.
        /// </summary>
        [CsvPropertyName("city")]
        public string City { get; set; }

        /// <summary>
        /// The state's abbreviation.
        /// </summary>
        [CsvPropertyName("state")]
        public string State { get; set; }

        /// <summary>
        /// The postal code.
        /// </summary>
        [CsvPropertyName("postalCode")]
        public string PostalCode { get; set; }

        /// <summary>
        /// The country's abbreviation.
        /// </summary>
        [CsvPropertyName("country")]
        public string Country { get; set; }

        [CsvPropertyName("locationGroupId")]
        public int LocationGroupId { get; set; }

        [CsvPropertyName("default")]
        public bool IsDefault { get; set; }

        [CsvPropertyName("residential")]
        public bool IsResidential { get; set; }

        [CsvPropertyName("type")]
        public AddressType Type { get; set; }

        [CsvPropertyName("addressInformationList")]
        public ICollection<Contact> Contacts { get; } = new HashSet<Contact>();
    }
}
