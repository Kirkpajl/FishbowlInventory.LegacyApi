using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FishbowlInventory.Serialization;
using System.Threading.Tasks;

namespace FishbowlInventory.Models
{
	/// <summary>
	/// This object contains the information being tracked.
	/// </summary>
	public class TrackingItem
    {
        /// <summary>
        /// The type of tracking being set.
        /// </summary>
        [CsvPropertyName("partTracking")]
		public PartTracking PartTracking { get; set; }

        /// <summary>
        /// The tracking value.
        /// </summary>
        [CsvPropertyName("value")]
		public string Value { get; set; }

        /// <summary>
        /// A list of serial numbers.
        /// </summary>
        [CsvPropertyName("serialNumbers")]
        public SerialNumberList SerialNumbers { get; set; }
	}
}
