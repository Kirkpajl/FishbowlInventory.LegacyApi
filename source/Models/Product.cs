using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FishbowlInventory.Serialization;
using System.Threading.Tasks;

namespace FishbowlInventory.Models
{
    /// <summary>
    /// Contains information about a Product.
    /// </summary>
    public class Product
    {
        [CsvPropertyName("id")]
        public int Id { get; set; }

        [CsvPropertyName("partID")]
        public int PartId { get; set; }

        [CsvPropertyName("part")]
        public Part Part { get; set; }

        [CsvPropertyName("num")]
        public string Number { get; set; }

        [CsvPropertyName("description")]
        public string Description { get; set; }

        [CsvPropertyName("details")]
        public string Details { get; set; }

        [CsvPropertyName("upc")]
        public string UPC { get; set; }

        [CsvPropertyName("sku")]
        public string SKU { get; set; }

        [CsvPropertyName("price")]
        public double Price { get; set; }

        [CsvPropertyName("uom")]
        public UnitOfMeasure UnitOfMeasure { get; set; }

        /// <summary>
        /// See DB table SOITEMTYPE for options.
        /// </summary>
        [CsvPropertyName("defaultSOItemType")]
        public string DefaultSOItemType { get; set; }

        [CsvPropertyName("displayType")]
        public string DisplayType { get; set; }

        [CsvPropertyName("url")]
        public string URL { get; set; }

        [CsvPropertyName("weight")]
        public int Weight { get; set; }

        ///// <summary>
        ///// See UOM table
        ///// </summary>
        //[CsvPropertyName("weightUOMID")]
        //public int WeightUnitOfMeasureId { get; set; }

        [CsvPropertyName("weightUOM")]
        public UnitOfMeasure WeightUnitOfMeasure { get; set; }

        [CsvPropertyName("width")]
        public int Width { get; set; }

        [CsvPropertyName("height")]
        public int Height { get; set; }

        [CsvPropertyName("len")]
        public int Length { get; set; }

        ///// <summary>
        ///// See UOM table
        ///// </summary>
        //[CsvPropertyName("sizeUOMID")]
        //public int SizeUnitOfMeasureId { get; set; }

        [CsvPropertyName("sizeUOM")]
        public UnitOfMeasure SizeUnitOfMeasure { get; set; }

        [CsvPropertyName("accountingID")]
        public int AccountingId { get; set; }

        [CsvPropertyName("accountingHash")]
        public string AccountingHash { get; set; }

        [CsvPropertyName("sellableInOtherUOMFlag")]
        public bool IsSellableInOtherUnitOfMeasure { get; set; }

        [CsvPropertyName("activeFlag")]
        public bool IsActive { get; set; }

        [CsvPropertyName("taxableFlag")]
        public bool IsTaxable { get; set; }

        [CsvPropertyName("usePriceFlag")]
        public bool UsePrice { get; set; }

        [CsvPropertyName("kitFlag")]
        public bool IsKit { get; set; }

        [CsvPropertyName("showSOComboFlag")]
        public bool ShowSOCombo { get; set; }

        [CsvPropertyName("image")]
        public string Image { get; set; }
    }
}
