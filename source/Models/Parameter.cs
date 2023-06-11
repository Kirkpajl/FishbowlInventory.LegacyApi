using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FishbowlInventory.Serialization;
using System.Threading.Tasks;

namespace FishbowlInventory.Models
{
    /// <summary>
    /// Contains name and value module parameters.
    /// </summary>
    public class Parameter
    {
        [CsvPropertyName("name")]
        public string Name { get; set; }

        [CsvPropertyName("value")]
        public string Value { get; set; }
    }
}
