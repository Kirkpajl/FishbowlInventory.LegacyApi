using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FishbowlInventory.Serialization;
using System.Threading.Tasks;

namespace FishbowlInventory.Models
{
    /// <summary>
    /// The order item's type.
    /// </summary>
    public class OrderItemType
    {
        /// <summary>
        /// The unique identification number.
        /// </summary>
        [CsvPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// The name of the object.
        /// </summary>
        [CsvPropertyName("name")]
        public string Name { get; set; }
    }
}
