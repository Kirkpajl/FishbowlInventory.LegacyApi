using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FishbowlInventory.Serialization;
using System.Threading.Tasks;

namespace FishbowlInventory.Models
{
    /// <summary>
    /// Stores Discount information
    /// </summary>
    public class Discount
    {
        [CsvPropertyName("id")]
        public int Id { get; set; }

        [CsvPropertyName("name")]
        public string Name { get; set; }

        [CsvPropertyName("description")]
        public string Description { get; set; }

        [CsvPropertyName("type")]
        public string Type { get; set; }

        [CsvPropertyName("typeID")]
        public int TypeId { get; set; }

        [CsvPropertyName("percentage")]
        public double Percentage { get; set; }

        [CsvPropertyName("amount")]
        public double Amount { get; set; }

        [CsvPropertyName("taxableFlag")]
        public bool IsTaxable { get; set; }
    }
}
