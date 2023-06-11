using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FishbowlInventory.Serialization;
using System.Threading.Tasks;

namespace FishbowlInventory.Models
{
    /// <summary>
    /// A number that helps describe a location.
    /// </summary>
    public class Tag
    {
        [CsvPropertyName("tagID")]
        public int Id { get; set; }

        [CsvPropertyName("num")]
        public string Number { get; set; }

        [CsvPropertyName("partNum")]
        public string PartNumber { get; set; }

        [CsvPropertyName("location")]
        public Location Location { get; set; }

        [CsvPropertyName("quantity")]
        public int Quantity { get; set; }

        [CsvPropertyName("quantityCommitted")]
        public int QuantityCommitted { get; set; }

        [CsvPropertyName("woNum")]
        public string WorkOrderNumber { get; set; }

        [CsvPropertyName("dateCreated")]
        public DateTime? DateCreated { get; set; }

        [CsvPropertyName("tracking")]
        public TrackingItem Tracking { get; set; }

        [CsvPropertyName("typeID")]
        public int TypeId { get; set; }

        [CsvPropertyName("accountID")]
        public int AccountId { get; set; }
    }
}
