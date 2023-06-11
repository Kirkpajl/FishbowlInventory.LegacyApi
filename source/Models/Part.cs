using FishbowlInventory.Enumerations;
using FishbowlInventory.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FishbowlInventory.Models
{
    /// <summary>
    /// Stores information about a part.
    /// </summary>
    public class Part
    {
        [CsvPropertyName("Id")]
        public int Id { get; set; }

        /// <summary>
        /// The part number.
        /// </summary>
        [CsvPropertyName("PartNumber"), Required]
        public string Number { get; set; }

        /// <summary>
        /// The part description.
        /// </summary>
        [CsvPropertyName("PartDescription"), Required]
        public string Description { get; set; }

        /// <summary>
        /// The part details.
        /// </summary>
        [CsvPropertyName("PartDetails")]
        public string Details { get; set; }

        /// <summary>
        /// The standard cost of the part.
        /// </summary>
        [CsvPropertyName("StandardCost")]
        public decimal StandardCost { get; set; }

        /// <summary>
        /// The average cost of the part.
        /// </summary>
        [CsvPropertyName("AverageCost")]
        public decimal AverageCost { get; set; }

        /// <summary>
        /// The unit of measurement the part is stored in. It must match (including case) an existing UOM abbreviation (not the name) in Fishbowl.
        /// </summary>
        /// <remarks>
        /// This is used for creating new parts only; it cannot be updated through the import.
        /// </remarks>
        [CsvPropertyName("UOM"), Required]
        public UnitOfMeasure UnitOfMeasure { get; set; }

        /// <summary>
        /// The UPC code for the part.
        /// </summary>
        [CsvPropertyName("UPC")]
        public string UPC { get; set; }

        /// <summary>
        /// The basic type of the part.
        /// </summary>
        [CsvPropertyName("PartType"), Required]
        public PartType Type { get; set; }

        /// <summary>
        /// The active status of the UOM.
        /// </summary>
        [CsvPropertyName("Active")]
        public bool IsActive { get; set; }

        /// <summary>
        /// The ABC code for the part.
        /// </summary>
        [CsvPropertyName("ABCCode")]
        public string ABCCode { get; set; }

        /// <summary>
        /// The weight of the part.
        /// </summary>
        [CsvPropertyName("Weight")]
        public double Weight { get; set; }

        /// <summary>
        /// The weight UOM of the part.
        /// </summary>
        [CsvPropertyName("WeightUOM")]
        public UnitOfMeasure WeightUnitOfMeasure { get; set; }

        /// <summary>
        /// The width of the part.
        /// </summary>
        [CsvPropertyName("Width")]
        public double Width { get; set; }

        /// <summary>
        /// The height of the part.
        /// </summary>
        [CsvPropertyName("Height")]
        public double Height { get; set; }

        /// <summary>
        /// The length of the part.
        /// </summary>
        [CsvPropertyName("Length")]
        public double Length { get; set; }

        /// <summary>
        /// The size UOM of the part.
        /// </summary>
        [CsvPropertyName("SizeUOM")]
        public UnitOfMeasure SizeUnitOfMeasure { get; set; }

        /// <summary>
        /// The consumption rate of the part.
        /// </summary>
        [CsvPropertyName("ConsumptionRate")]
        public decimal ConsumptionRate { get; set; }

        /// <summary>
        /// The alert note of the part.
        /// </summary>
        [CsvPropertyName("AlertNote")]
        public string AlertNote { get; set; }

        /// <summary>
        /// Contains a URL reference to the picture to be uploaded to the part.
        /// </summary>
        /// <remarks>
        /// The URL reference must begin with "file://", followed by either "localhost" or the IP address of the computer where the picture is stored, followed by the file path.
        /// </remarks>
        /// <example>
        /// file://localhost/C:/Users/Pictures/Saved Pictures/image.jpg
        /// </example>
        [CsvPropertyName("PictureUrl")]
        public string ImageUrl { get; set; }

        /// <summary>
        /// The revision field of the part.
        /// </summary>
        [CsvPropertyName("Revision")]
        public string Revision { get; set; }

        /// <summary>
        /// The default PO Item Type.
        /// </summary>
        /// <remarks>
        /// Can be Purchase, Credit Return, or Out Sourced.
        /// </remarks>
        [CsvPropertyName("POItemType"), Required]
        public PurchaseOrderItemType PurchaseOrderItemType { get; set; }

        /// <summary>
        /// The default outsourced return item for a part with an out sourced po item type.
        /// </summary>
        [CsvPropertyName("DefaultOutsourcedReturnItem")]
        public string DefaultOutsourcedReturnItem { get; set; }

        /// <summary>
        /// The Primary Tracking type of the part. This corresponds to the name of a tracking type.
        /// </summary>
        [CsvPropertyName("PrimaryTracking")]
        public PartTrackingItem PrimaryTracking { get; set; }

        /// <summary>
        /// The types of tracking information that this part is tracking.
        /// </summary>
        public List<PartTrackingItem> Tracks { get; } = new List<PartTrackingItem>();

        /// <summary>
        /// The part's custom fields.
        /// </summary>
        public List<CustomField> CustomFields { get; } = new List<CustomField>();




        //[CsvPropertyName("hasTracking")]
        //public bool HasTracking { get; set; }

        //[CsvPropertyName("tracksSerialNumbers")]
        //public bool TracksSerialNumbers { get; set; }

        //[CsvPropertyName("trackingFlag")]
        //public bool IsTracking { get; set; }

        //[CsvPropertyName("partTrackingList")]
        //public PartTrackingItem[] PartTrackingItems { get; set; }

        //[CsvPropertyName("partClassID")]
        //public int ClassId { get; set; }

        //[CsvPropertyName("tagLabel")]
        //public string TagLabel { get; set; }

        //[CsvPropertyName("standardCost")]
        //public string StandardCost { get; set; }

        ///// <summary>
        ///// Indicates if the part has an associated bill of materials. True returns parts with an associated bill of materials, false does not filter the results.
        ///// </summary>
        //[CsvPropertyName("hasBom")]
        //public bool HasBillOfMaterial { get; set; }

        //[CsvPropertyName("configurable")]
        //public bool IsConfigurable { get; set; }

        //[CsvPropertyName("serializedFlag")]
        //public bool IsSerialized { get; set; }

        //[CsvPropertyName("usedFlag")]
        //public bool IsUsed { get; set; }

        ///// <summary>
        ///// The associated product number.
        ///// </summary>
        //[CsvPropertyName("productNumber")]
        //public string ProductNumber { get; set; }

        ///// <summary>
        ///// The associated product description.
        ///// </summary>
        //[CsvPropertyName("productDescription")]
        //public string ProductDescription { get; set; }

        ///// <summary>
        ///// The Vendor Part Number
        ///// </summary>
        //[CsvPropertyName("vendorPartNumber")]
        //public string VendorPartNumber { get; set; }

        ///// <summary>
        ///// The name of the associated vendor.
        ///// </summary>
        //[CsvPropertyName("vendorName")]
        //public string VendorName { get; set; }

        //[CsvPropertyName("manufacturer")]
        //public string Manufacturer { get; set; }
    }
}
