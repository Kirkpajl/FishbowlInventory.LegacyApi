using FishbowlInventory.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FishbowlInventory.Serialization;
using System.Threading.Tasks;

namespace FishbowlInventory.Models
{
    public class ManufactureOrderConfigurationItem
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
        /// Indicates if the item is a stage.
        /// </summary>
        [CsvPropertyName("stage")]
        public bool Stage { get; set; }

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
        /// The part associated with the item.
        /// </summary>
        [CsvPropertyName("part")]
        public Part Part { get; set; }

        /// <summary>
        /// The configuration item type.
        /// </summary>
        /// <remarks>
        /// 'Finished Good' | 'Raw Good' | 'Repair Raw Good' | 'Note' | 'Bill of Materials'
        /// </remarks>
        [CsvPropertyName("type")]
        public ConfigurationItemType Type { get; set; }

        /// <summary>
        /// The configuration item's unit of measure.
        /// </summary>
        [CsvPropertyName("uom")]
        public UnitOfMeasure UOM { get; set; }

        /// <summary>
        /// Indicates if the item is a one time item.
        /// </summary>
        [CsvPropertyName("oneTimeItem")]
        public bool OneTimeItem { get; set; }

        /// <summary>
        /// An optional nested configuration object
        /// </summary>
        [CsvPropertyName("configuration")]
        public ManufactureOrderConfiguration Configuration { get; set; }
    }
}
