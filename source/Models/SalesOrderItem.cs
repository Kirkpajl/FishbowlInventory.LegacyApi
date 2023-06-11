using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FishbowlInventory.Serialization;
using System.Threading.Tasks;
using FishbowlInventory.Enumerations;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Security.Cryptography;

namespace FishbowlInventory.Models
{
    public class SalesOrderItem
    {
        [CsvPropertyName("ID")]
        public int Id { get; set; }

        /// <summary>
        /// The type of sales order item. 
        /// </summary>
        /// <remarks>
        /// If this column is left blank, the item will be imported with the Default SOItem Type of the product.
        /// 
        /// Items of type Subtotal, Discount Percentage or Discount Amount do not require any of the values that 
        /// other sales order items require.Subtotals do not require any other values to be specified except for 
        /// the word "Subtotal" in the 'Product Number' column.Discounts and Assoc.Prices require the name of the 
        /// discount/assoc.price to be specified in the "Product Number" column.  BOM Configuration Items are raw 
        /// goods for the line above it. Notes are specified in the "Product Number" column.
        /// </remarks>
        [CsvPropertyName("SOItemTypeID"), Required]
        public SalesOrderItemType ItemType { get; set; }

        /// <summary>
        /// The product number from Fishbowl (case insensitive).
        /// </summary>
        [CsvPropertyName("ProductNumber"), Required]
        public string ProductNumber { get; set; }

        /// <summary>
        /// The unique ID of the parent <see cref="SalesOrder"/> record.
        /// </summary>
        [CsvPropertyName("SOID")]
        public int SalesOrderId { get; set; }

        /// <summary>
        /// The sales order item description from Fishbowl.
        /// </summary>
        [CsvPropertyName("ProductDescription")]
        public string Description { get; set; }

        /// <summary>
        /// The quantity for this Product.
        /// </summary>
        [CsvPropertyName("ProductQuantity"), Required]
        public decimal Quantity { get; set; }

        /// <summary>
        /// The unit of measure (UOM) abbreviation the product is being bought in.
        /// </summary>
        [CsvPropertyName("UOM"), Required]
        public UnitOfMeasure UnitOfMeasure { get; set; }

        /// <summary>
        /// The specified price for this Product.
        /// </summary>
        /// <remarks>
        /// Leave blank for pricing rules to be applied to item.
        /// </remarks>
        [CsvPropertyName("ProductPrice")]
        public decimal ProductPrice { get; set; }

        /// <summary>
        /// The total price for this Product.
        /// </summary>
        [CsvPropertyName("TotalPrice")]
        public decimal TotalPrice => Quantity * ProductPrice;

        /// <summary>
        /// Whether this line item is taxable or not.
        /// </summary>
        [CsvPropertyName("Taxable")]
        public bool IsTaxable { get; set; }

        /// <summary>
        /// The tax code for the sales order item.
        /// </summary>
        [CsvPropertyName("TaxCode")]
        public string TaxCode { get; set; }

        /// <summary>
        /// SO Item Notes
        /// </summary>
        [CsvPropertyName("Note")]
        public string Note { get; set; }

        /// <summary>
        /// The name of the QuickBooks class for this line item (created if not found).
        /// </summary>
        [CsvPropertyName("ItemQuickBooksClassName")]
        public string QuickBooksClassName { get; set; }

        /// <summary>
        /// The date scheduled of this line item (I.E. M/d/yyyy or 6/2/2005)
        /// </summary>
        [CsvPropertyName("ItemDateScheduled")]
        public DateTime? DateScheduled { get; set; }

        /// <summary>
        /// Determines if the kit item should be shown.
        /// </summary>
        [CsvPropertyName("ShowItem")]
        public bool ShowItem { get; set; }

        /// <summary>
        /// Determines if the items is part of a kit.
        /// </summary>
        /// <remarks>
        /// When using kits, each kit item can be included in the CSV, or alternatively, the default kit items 
        /// will be added if only the kit header is included in the CSV. When using the kit header only, the price 
        /// for the entire kit will be determined by the ProductPrice field in the CSV, even if "Specify Kit Price" 
        /// is not enabled on the kit. In this scenario, a percentage discount or tax would need to be saved as the 
        /// first item in the kit so that the percentage is calculated using the total kit price.
        /// </remarks>
        [CsvPropertyName("KitItem")]
        public bool IsKitItem { get; set; }

        /// <summary>
        /// The revision level on the sales order item.
        /// </summary>
        [CsvPropertyName("RevisionLevel")]
        public int RevisionLevel { get; set; }

        /// <summary>
        /// The customer part number for the sales order item.
        /// </summary>
        [CsvPropertyName("CustomerPartNumber")]
        public string CustomerPartNumber { get; set; }

        /// <summary>
        /// The sales order item's custom fields.
        /// </summary>
        public List<CustomField> CustomFields { get; } = new List<CustomField>();
    }
}
