using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FishbowlInventory.Serialization;
using System.Threading.Tasks;

namespace FishbowlInventory.Models
{
    /// <summary>
    /// Stores information about items that are involved in a pick.
    /// </summary>
    public class PickItem
    {
        [CsvPropertyName("pickItemID")]
        public int Id { get; set; }

        [CsvPropertyName("status")]
        public int Status { get; set; }

        [CsvPropertyName("part")]
        public Part Part { get; set; }

        [CsvPropertyName("tag")]
        public Tag Tag { get; set; }

        [CsvPropertyName("quantity")]
        public int Quantity { get; set; }

        [CsvPropertyName("uom")]
        public UnitOfMeasure UnitOfMeasure { get; set; }

        [CsvPropertyName("tracking")]
        public TrackingItem Tracking { get; set; }

        [CsvPropertyName("destinationTag")]
        public Tag DestinationTag { get; set; }

        [CsvPropertyName("orderType")]
        public string OrderType { get; set; }

        [CsvPropertyName("orderTypeID")]
        public int OrderTypeId { get; set; }

        [CsvPropertyName("orderNum")]
        public string OrderNumber { get; set; }

        [CsvPropertyName("orderID")]
        public int OrderId { get; set; }

        [CsvPropertyName("soItemId")]
        public int SoItemId { get; set; }

        [CsvPropertyName("poItemId")]
        public int PoItemId { get; set; }

        [CsvPropertyName("xoItemId")]
        public int XoItemId { get; set; }

        [CsvPropertyName("woItemId")]
        public int WoItemId { get; set; }

        [CsvPropertyName("slotNumber")]
        public int SlotNumber { get; set; }

        [CsvPropertyName("note")]
        public string Note { get; set; }

        [CsvPropertyName("location")]
        public Location Location { get; set; }

        [CsvPropertyName("pickItemType")]
        public int PickItemType { get; set; }
    }
}
