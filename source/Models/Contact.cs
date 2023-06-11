using FishbowlInventory.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FishbowlInventory.Serialization;
using System.Threading.Tasks;

namespace FishbowlInventory.Models
{
    /// <summary>
    /// Contains Contact Information.
    /// </summary>
    public class Contact
    {
        [CsvPropertyName("contactName")]
        public string Name { get; set; }

        [CsvPropertyName("contactId")]
        public int Id { get; set; }

        [CsvPropertyName("type")]
        public ContactType Type { get; set; }

        [CsvPropertyName("default")]
        public bool IsDefault { get; set; }

        [CsvPropertyName("datum")]
        public string Datum { get; set; }
    }
}
