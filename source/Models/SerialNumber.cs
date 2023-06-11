using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FishbowlInventory.Serialization;
using System.Threading.Tasks;

namespace FishbowlInventory.Models
{
    /// <summary>
    /// Serial number value.
    /// </summary>
    public class SerialNumber
    {
        /// <summary>
        /// The type of tracking being set.
        /// </summary>
        [CsvPropertyName("partTracking")]
        public PartTracking PartTracking { get; set; }

        /// <summary>
        /// The serial number value.
        /// </summary>
        [CsvPropertyName("value")]
        public string Value { get; set; }
    }
}
