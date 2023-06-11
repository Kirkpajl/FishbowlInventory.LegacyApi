using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FishbowlInventory.Serialization;
using System.Threading.Tasks;

namespace FishbowlInventory.Models
{
    /// <summary>
    /// This object just contains the carrier's name
    /// </summary>
    public class Carrier
    {
        [CsvPropertyName("name")]
        public string Name { get; set; }
    }
}
