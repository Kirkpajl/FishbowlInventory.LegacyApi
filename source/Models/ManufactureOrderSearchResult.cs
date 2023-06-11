using FishbowlInventory.Enumerations;
using FishbowlInventory.Serialization;
using System;

namespace FishbowlInventory.Models
{
    /// <summary>
    /// Manufacture orders are used to organize work orders and allows for items to be manufactured, disassembled, and repaired.
    /// </summary>
    public class ManufactureOrderSearchResult
    {
        /// <summary>
        /// The manufacture order's unique identification number.
        /// </summary>
        [CsvPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// The manufacture order number.
        /// </summary>
        [CsvPropertyName("number")]
        public string Number { get; set; }

        /// <summary>
        /// The number of the associated Bill of Material.
        /// </summary>
        [CsvPropertyName("bomNumber")]
        public string BillOfMaterialNumber { get; set; }

        /// <summary>
        /// The description of the associated Bill of Material.
        /// </summary>
        [CsvPropertyName("bomDescription")]
        public string BillOfMaterialDescription { get; set; }

        /// <summary>
        /// The associated sales order number.
        /// </summary>
        [CsvPropertyName("soNumber")]
        public string SalesOrderNumber { get; set; }

        /// <summary>
        /// Timestamp of when the order was scheduled.
        /// </summary>
        [CsvPropertyName("dateScheduled")]  //'yyyy-MM-dd'
        public DateTime? DateScheduled { get; set; }

        /// <summary>
        /// The order status.
        /// </summary>
        [CsvPropertyName("status")]
        public ManufactureOrderStatus Status { get; set; }

        /// <summary>
        /// The location group the order belongs to.
        /// </summary>
        [CsvPropertyName("locationGroup")]
        public string LocationGroupName { get; set; }
    }
}
