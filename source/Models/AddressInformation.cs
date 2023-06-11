using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FishbowlInventory.Serialization;
using System.Threading.Tasks;

namespace FishbowlInventory.Models
{
    public class AddressInformation
    {
        [CsvPropertyName("id")]
        public int Id { get; set; }

        [CsvPropertyName("name")]
        public string Name { get; set; }

        [CsvPropertyName("default")]
        public bool IsDefault { get; set; }

        [CsvPropertyName("type")]
        public string Type { get; set; }
    }
}
