using FishbowlInventory.Enumerations;
using FishbowlInventory.Serialization;
using System;
using System.Collections.Generic;

namespace FishbowlInventory.Models
{
    /// <summary>
    /// Object used to store data concerning a Purchase Order.
    /// </summary>
    public class PurchaseOrder
    {
        /// <summary>
        /// The purchase order's unique identification number.
        /// </summary>
        [CsvPropertyName("Id")]
        public int Id { get; set; }

        /// <summary>
        /// The purchase order number.
        /// </summary>
        [CsvPropertyName("PONum")]
        public string Number { get; set; }

        /// <summary>
        /// The order status.
        /// </summary>
        [CsvPropertyName("Status")]
        //[JsonConverter(typeof(EnumDescriptionConverter))]
        public PurchaseOrderStatus Status { get; set; } = PurchaseOrderStatus.BidRequest;

        /// <summary>
        /// The class category for the order item.
        /// </summary>
        [CsvPropertyName("QuickBooksClassName")]
        public string QuickBooksClassName { get; set; }

        /// <summary>
        /// The shipping carrier.
        /// </summary>
        [CsvPropertyName("CarrierName")]
        public string CarrierName { get; set; }

        /// <summary>
        /// The Carrier Service selected on the purchase order.
        /// </summary>
        [CsvPropertyName("CarrierService")]
        public string CarrierServiceName { get; set; }

        /// <summary>
        /// Indicates when ownership/liability of the order transfers to the purchaser.
        /// </summary>
        [CsvPropertyName("FobPointName")]
        public string FobPointName { get; set; }

        /// <summary>
        /// The terms of payment on the order.
        /// </summary>
        [CsvPropertyName("PaymentTermsName")]
        public string PaymentTermsName { get; set; }

        /// <summary>
        /// The shipping terms on the order.
        /// </summary>
        [CsvPropertyName("ShippingTermsId")]
        public ShippingTerms ShippingTerms { get; set; } = ShippingTerms.PrepaidBilled;

        /// <summary>
        /// The shipping terms on the order.
        /// </summary>
        [CsvPropertyName("ShippingTermsName")]
        public string ShippingTermsName { get; set; }

        /// <summary>
        /// Indicates whether the order is a standard or drop ship purchase order.
        /// </summary>
        [CsvPropertyName("TypeId")]
        public PurchaseOrderType Type { get; set; } = PurchaseOrderType.Standard;

        /// <summary>
        /// The name of the vendor.
        /// </summary>
        [CsvPropertyName("VendorName")]
        public string VendorName { get; set; }

        /// <summary>
        /// The vendor sales order number.
        /// </summary>
        [CsvPropertyName("VendorSONum")]
        public string VendorSalesOrderNumber { get; set; }

        /// <summary>
        /// The customer sales order number.
        /// </summary>
        [CsvPropertyName("CustomerSONum")]
        public string CustomerSalesOrderNumber { get; set; }

        /// <summary>
        /// The Fishbowl user that created the order.
        /// </summary>
        [CsvPropertyName("BuyerUserName")]
        public string BuyerUserName { get; set; }

        /// <summary>
        /// The intended recipient of the order.
        /// </summary>
        [CsvPropertyName("DeliverToName")]
        public string DeliverTo { get; set; }

        /// <summary>
        /// The revision number.
        /// </summary>
        [CsvPropertyName("RevisionNumber")]
        public int RevisionNumber { get; set; }

        /// <summary>
        /// Timestamp of when the order was last modified.
        /// </summary>
        [CsvPropertyName("LastModifiedDate")]
        public DateTime? DateLastModified { get; set; }

        /// <summary>
        /// Timestamp of when the order was issued.
        /// </summary>
        [CsvPropertyName("IssuedDate")]
        public DateTime? DateIssued { get; set; }

        /// <summary>
        /// Timestamp of when the order was created.
        /// </summary>
        [CsvPropertyName("CreatedDate")]
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Timestamp of when the order was confirmed by the vendor.
        /// </summary>
        [CsvPropertyName("ConfirmedDate")]
        public DateTime? DateConfirmed { get; set; }

        /// <summary>
        /// Timestamp of when the order was last revised.
        /// </summary>
        [CsvPropertyName("RevisionDate")]
        public DateTime? DateRevised { get; set; }

        /// <summary>
        /// Timestamp of when the order was scheduled to be fulfilled.
        /// </summary>
        [CsvPropertyName("FulfillmentDate")]
        public DateTime? DateScheduled { get; set; }

        /// <summary>
        /// Timestamp of when the order was completed.
        /// </summary>
        [CsvPropertyName("CompletedDate")]
        public DateTime? DateCompleted { get; set; }

        /// <summary>
        /// The tax rate on the order. This object is ignored for companies in the United States.
        /// </summary>
        [CsvPropertyName("TaxRateName")]
        public string TaxRateName { get; set; }

        /// <summary>
        /// The total amount of tax on the order.
        /// </summary>
        [CsvPropertyName("TotalTax")]
        public decimal TotalTax { get; set; }

        /// <summary>
        /// Indicates if the order total includes tax.
        /// </summary>
        [CsvPropertyName("TotalIncludesTax")]
        public bool TotalIncludesTax { get; set; }

        /// <summary>
        /// The location group the order belongs to.
        /// </summary>
        [CsvPropertyName("LocationGroupName")]
        public string LocationGroupName { get; set; }

        /// <summary>
        /// The order's note field.
        /// </summary>
        [CsvPropertyName("Note")]
        public string Note { get; set; }

        /// <summary>
        /// The url link on the order.
        /// </summary>
        [CsvPropertyName("URL")]
        public string Url { get; set; }

        /// <summary>
        /// The currency used on the order.
        /// </summary>
        [CsvPropertyName("CurrencyName")]
        public string CurrencyName { get; set; }

        /// <summary>
        /// The vendor's email address for the order.
        /// </summary>
        [CsvPropertyName("Email")]
        public string VendorEmail { get; set; }

        /// <summary>
        /// The vendor's phone number for the order.
        /// </summary>
        [CsvPropertyName("Phone")]
        public string VendorPhone { get; set; }

        /// <summary>
        /// The shipping address on the order.
        /// </summary>
        [CsvIgnore]  //[CsvProperty("shipToAddress")]
        public Address ShipToAddress { get; set; } = new Address();

        /// <summary>
        /// The remit to address on the order.
        /// </summary>
        [CsvIgnore]  //[CsvProperty("remitToAddress")]
        public Address RemitToAddress { get; set; } = new Address();

        /// <summary>
        /// A list of the purchase order items.
        /// </summary>
        [CsvIgnore]  //[CsvProperty("poItems")]
        public PurchaseOrderItem[] Items { get; set; } = Array.Empty<PurchaseOrderItem>();

        /// <summary>
        /// A list of custom fields associated with the order.
        /// </summary>
        [CsvIgnore]  //[CsvProperty("customFields")]
        public List<CustomField> CustomFields { get; set; } = new List<CustomField>();
    }
}
