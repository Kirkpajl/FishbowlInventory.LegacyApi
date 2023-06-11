using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FishbowlInventory.Serialization;
using System.Threading.Tasks;

namespace FishbowlInventory.Models
{
    /// <summary>
    /// Manufacture order configuration
    /// </summary>
    public class ManufactureOrderConfiguration
    {
        /// <summary>
        /// The configuration's unique identification number.
        /// </summary>
        [CsvPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// The order status.
        /// </summary>
        [CsvPropertyName("status")]
        public string Status { get; set; }

        /// <summary>
        /// The description of the object.
        /// </summary>
        [CsvPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// The bill of materials associated with the configuration.
        /// </summary>
        [CsvPropertyName("bom")]
        public BillOfMaterial BOM { get; set; }

        /// <summary>
        /// The quantity to be fulfilled.
        /// </summary>
        [CsvPropertyName("quantity")]
        public int Quantity { get; set; }

        /// <summary>
        /// The quantity already fulfilled.
        /// </summary>
        [CsvPropertyName("quantityFulfilled")]
        public int QuantityFulfilled { get; set; }

        /// <summary>
        /// The amount to adjust the configuration price on the sales order.
        /// </summary>
        [CsvPropertyName("priceAdjustment")]
        public decimal PriceAdjustment { get; set; }

        /// <summary>
        /// The unit cost of the configuration.
        /// </summary>
        [CsvPropertyName("unitCost")]
        public decimal UnitCost { get; set; }

        /// <summary>
        /// Timestamp of when the configuration was scheduled.
        /// </summary>
        [CsvPropertyName("dateScheduled")]  //'yyyy-MM-dd'
        public DateTime? DateScheduled { get; set; }

        /// <summary>
        /// Timestamp of when the configuration was scheduled.
        /// </summary>
        [CsvPropertyName("dateScheduledToStart")]  //'yyyy-MM-dd'
        public DateTime? DateScheduledToStart { get; set; }

        /// <summary>
        /// The item's note field.
        /// </summary>
        [CsvPropertyName("note")]
        public string Note { get; set; }

        /// <summary>
        /// The position in the sort order.
        /// </summary>
        [CsvPropertyName("sortId")]
        public int SortId { get; set; }

        /// <summary>
        /// The depth of the stage.
        /// </summary>
        [CsvPropertyName("stageLevel")]
        public int StageLevel { get; set; }

        /// <summary>
        /// The class category.
        /// </summary>
        [CsvPropertyName("class")]
        public Category Class { get; set; }

        /// <summary>
        /// Indicates if the configuration has an associated sales order.
        /// </summary>
        [CsvPropertyName("hasSo")]
        public bool HasSalesOrder { get; set; }

        /// <summary>
        /// The name of the priority level.
        /// </summary>
        [CsvPropertyName("priority")]
        public string Priority { get; set; }

        /// <summary>
        /// The calendar category.
        /// </summary>
        [CsvPropertyName("category")]
        public Category Category { get; set; }

        /// <summary>
        /// The list of configuration items
        /// </summary>
        [CsvPropertyName("items")]
        public List<ManufactureOrderConfigurationItem> Items { get; set; }
    }
}
