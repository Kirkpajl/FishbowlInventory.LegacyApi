using FishbowlInventory.Enumerations;
using FishbowlInventory.Serialization;
using System;

namespace FishbowlInventory.Models
{
    public class PurchaseOrderItem
    {
        /// <summary>
        /// The unique identifier for the order item.
        /// </summary>
        [CsvPropertyName("Id")]
        public int Id { get; set; }

        /// <summary>
        /// The sort order number for the order item.
        /// </summary>
        [CsvPropertyName("LineItem")]
        public int LineNumber { get; set; }

        /// <summary>
        /// The description of the order item.
        /// </summary>
        [CsvPropertyName("Description")]
        public string Description { get; set; }

        /// <summary>
        /// The part on the order item.
        /// </summary>
        [CsvPropertyName("PartNumber")]
        public string PartNumber { get; set; }

        /// <summary>
        /// The description of the part on the order item.
        /// </summary>
        [CsvPropertyName("PartDescription")]
        public string PartDescription { get; set; }

        /// <summary>
        /// The outsourced part on the order item.
        /// </summary>
        [CsvPropertyName("OutsourcedPartNumber")]
        public string OutsourcedPartNumber { get; set; }

        /// <summary>
        /// The outsourced part on the order item.
        /// </summary>
        [CsvPropertyName("OutsourcedPartDescription")]
        public string OutsourcedPartDescription { get; set; }

        /// <summary>
        /// The order item's type.
        /// </summary>
        [CsvPropertyName("TypeId")]
        public PurchaseOrderItemType Type { get; set; }

        /// <summary>
        /// The status of the order item.
        /// </summary>
        [CsvPropertyName("StatusId")]
        public PurchaseOrderItemStatus Status { get; set; }

        /// <summary>
        /// The part unit of measure.
        /// </summary>
        [CsvPropertyName("UOM")]
        public string UnitOfMeasure { get; set; }

        /// <summary>
        /// The vendor's number for the part.
        /// </summary>
        [CsvPropertyName("VendorPartNumber")]
        public string VendorPartNumber { get; set; }

        /// <summary>
        /// The quantity to be fulfilled.
        /// </summary>
        [CsvPropertyName("PartQuantity")]
        public decimal QuantityToFulfill { get; set; }

        /// <summary>
        /// The quantity already fulfilled.
        /// </summary>
        [CsvPropertyName("FulfilledQuantity")]
        public decimal QuantityFulfilled { get; set; }

        /// <summary>
        /// The quantity already picked.
        /// </summary>
        [CsvPropertyName("PickedQuantity")]
        public decimal QuantityPicked { get; set; }

        /// <summary>
        /// The unit cost of the part.
        /// </summary>
        [CsvPropertyName("PartPrice")]
        public decimal UnitCost { get; set; }

        /// <summary>
        /// The total cost of the part.
        /// </summary>
        [CsvPropertyName("totalCost")]
        public decimal TotalCost { get; set; }

        /// <summary>
        /// The tax rate for the order item. This object is ignored for companies in the United States.
        /// </summary>
        [CsvPropertyName("TaxRateName")]
        public string TaxRateName { get; set; }

        /// <summary>
        /// Timestamp of when the order is scheduled to be fulfilled.
        /// </summary>
        [CsvPropertyName("FulfillmentDate")]
        public DateTime? DateScheduled { get; set; }

        /// <summary>
        /// Timestamp of when the part/product was last fulfilled.
        /// </summary>
        [CsvPropertyName("LastFulfillmentDate")]
        public DateTime? DateLastFulfilled { get; set; }

        /// <summary>
        /// The revision number for the order item.
        /// </summary>
        [CsvPropertyName("RevisionLevel")]
        public string Revision { get; set; }

        /// <summary>
        /// The class category for the order item.
        /// </summary>
        [CsvPropertyName("QuickBooksClassName")]
        public string QuickBooksClassName { get; set; }

        /// <summary>
        /// The note on the order item
        /// </summary>
        [CsvPropertyName("Note")]
        public string Note { get; set; }

        /// <summary>
        /// The customer on the order item.
        /// </summary>
        [CsvPropertyName("CustomerName")]
        public string CustomerName { get; set; }
    }
}
