using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FishbowlInventory.Serialization;
using System.Threading.Tasks;

namespace FishbowlInventory.Models
{
    /// <summary>
    /// Stores a detail about a contact.There are no fields that are essential for the use of a Contact Information object. Use only those fields that you need.
    /// </summary>
    public class ContactInformation
    {
        [CsvPropertyName("contactName")]
        public string ContactName { get; set; }

        [CsvPropertyName("contactId")]
        public int ContactId { get; set; }

        [CsvPropertyName("type")]
        public int Type { get; set; }

        [CsvPropertyName("default")]
        public bool IsDefault { get; set; }
    }
}
