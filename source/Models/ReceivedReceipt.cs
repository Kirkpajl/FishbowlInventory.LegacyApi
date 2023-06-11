using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FishbowlInventory.Serialization;
using System.Threading.Tasks;

namespace FishbowlInventory.Models
{
    public class ReceivedReceipt
    {
        [CsvPropertyName("itemType")]
        public int ItemType { get; set; }

        [CsvPropertyName("quantity")]
        public int Quantity { get; set; }

        [CsvPropertyName("reason")]
        public string Reason { get; set; }

        [CsvPropertyName("locationID")]
        public int LocationId { get; set; }

        [CsvPropertyName("tracking")]
        public string Tracking { get; set; }
    }
}
