using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FishbowlInventory.Serialization;
using System.Threading.Tasks;

namespace FishbowlInventory.Models
{
    /// <summary>
    /// Contains receipt information.
    /// </summary>
    public class ReceiveItem
    {
        [CsvPropertyName("id")]
        public int Id { get; set; }

        [CsvPropertyName("itemNum")]
        public string Number { get; set; }

        [CsvPropertyName("description")]
        public string Description { get; set; }

        [CsvPropertyName("lineNum")]
        public int LineNumber { get; set; }

        [CsvPropertyName("itemStatus")]
        public int Status { get; set; }

        [CsvPropertyName("itemType")]
        public int Type { get; set; }

        [CsvPropertyName("dateLastModified")]
        public DateTime? DateLastModified { get; set; }

        [CsvPropertyName("orderNum")]
        public string OrderNumber { get; set; }

        [CsvPropertyName("orderType")]
        public int OrderType { get; set; }

        [CsvPropertyName("soItemId")]
        public int SalesOrderItemId { get; set; }

        [CsvPropertyName("poItemId")]
        public int PurchaseOrderItemId { get; set; }

        [CsvPropertyName("xoItemId")]
        public int TransferOrderItemId { get; set; }

        [CsvPropertyName("orderItemType")]
        public int OrderItemType { get; set; }

        [CsvPropertyName("receiptID")]
        public int ReceiptId { get; set; }

        [CsvPropertyName("quantity")]
        public int Quantity { get; set; }

        [CsvPropertyName("uomName")]
        public string UnitOfMeasureName { get; set; }

        [CsvPropertyName("uomID")]
        public int UnitOfMeasureId { get; set; }

        [CsvPropertyName("suggestedLocationID")]
        public int SuggestedLocationId { get; set; }

        [CsvPropertyName("originalUnitCost")]
        public int OriginalUnitCost { get; set; }

        [CsvPropertyName("billedUnitCost")]
        public int BilledUnitCost { get; set; }

        [CsvPropertyName("landedUnitCost")]
        public int LandedUnitCost { get; set; }

        [CsvPropertyName("deliverTo ")]
        public string DeliverTo { get; set; }

        [CsvPropertyName("carrierID")]
        public int CarrierId { get; set; }

        [CsvPropertyName("partTypeID")]
        public int PartTypeId { get; set; }

        [CsvPropertyName("trackingNum")]
        public string TrackingNumber { get; set; }

        [CsvPropertyName("packageCount")]
        public int PackageCount { get; set; }

        [CsvPropertyName("dateScheduled")]
        public DateTime? DateScheduled { get; set; }

        [CsvPropertyName("receivedReceipts")]
        public ReceivedReceipt[] ReceivedReceipts { get; set; }

        [CsvPropertyName("linkedOrders")]
        public string LinkedOrders { get; set; }

        [CsvPropertyName("part")]
        public Part Part { get; set; }
    }
}
