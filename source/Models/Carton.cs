using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FishbowlInventory.Serialization;
using System.Threading.Tasks;

namespace FishbowlInventory.Models
{
    /// <summary>
    /// Stores information about a carton.
    /// </summary>
    public class Carton
    {
        [CsvPropertyName("id")]
        public int Id { get; set; }

        [CsvPropertyName("shipId")]
        public int ShipId { get; set; }

        [CsvPropertyName("cartonNum")]
        public int Number { get; set; }

        [CsvPropertyName("trackingNum")]
        public string TrackingNumber { get; set; }

        [CsvPropertyName("freightWeight")]
        public int FreightWeight { get; set; }

        [CsvPropertyName("freightAmount")]
        public int FreightAmount { get; set; }

        [CsvPropertyName("shippingItems")]
        public ShippingItem[] ShippingItems { get; set; }
    }
}
