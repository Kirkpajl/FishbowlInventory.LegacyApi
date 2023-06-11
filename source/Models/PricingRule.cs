using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FishbowlInventory.Serialization;
using System.Threading.Tasks;

namespace FishbowlInventory.Models
{
    public class PricingRule
    {
        [CsvPropertyName("id")]
        public int Id { get; set; }

        [CsvPropertyName("name")]
        public string Name { get; set; }

        [CsvPropertyName("description")]
        public string Description { get; set; }

        [CsvPropertyName("unitPrice")]
        public decimal UnitPrice { get; set; }

        [CsvPropertyName("valid")]
        public bool Valid { get; set; }
    }
}
