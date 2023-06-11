using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FishbowlInventory.Serialization;
using System.Threading.Tasks;

namespace FishbowlInventory.Models
{
    public class State
    {
        [CsvPropertyName("id")]
        public int Id { get; set; }

        [CsvPropertyName("code")]
        public string Code { get; set; }

        [CsvPropertyName("name")]
        public string Name { get; set; }

        [CsvPropertyName("countryId")]
        public int CountryId { get; set; }
    }
}
