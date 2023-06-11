using FishbowlInventory.Enumerations;
using FishbowlInventory.Serialization;
using System;
using System.Collections.Generic;

namespace FishbowlInventory.Models
{
    /// <summary>
    /// Manufacture orders are used to organize work orders and allows for items to be manufactured, disassembled, and repaired.
    /// </summary>
    public class ManufactureOrder
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
        /// The order status.
        /// </summary>
        [CsvPropertyName("status")]
        public ManufactureOrderStatus Status { get; set; }

        /// <summary>
        /// The revision number.
        /// </summary>
        [CsvPropertyName("revisionNumber")]
        public int RevisionNumber { get; set; }

        /// <summary>
        /// The order's note field.
        /// </summary>
        [CsvPropertyName("note")]
        public string Note { get; set; }

        /// <summary>
        /// The location group the order belongs to.
        /// </summary>
        [CsvPropertyName("locationGroup")]
        public LocationGroup LocationGroup { get; set; }

        /// <summary>
        /// The associated sales order number.
        /// </summary>
        [CsvPropertyName("salesOrder")]
        public SalesOrder SalesOrder { get; set; }

        /// <summary>
        /// Timestamp of when the order was created.
        /// </summary>
        [CsvPropertyName("dateCreated")]  //'yyyy-MM-dd'
        public DateTime? DateCreated { get; set; }

        /// <summary>
        /// Timestamp of when the order was last modified and the user that made the modifications.
        /// </summary>
        [CsvPropertyName("lastModified")]  //'yyyy-MM-dd'
        public DateTime? LastModified { get; set; }

        /// <summary>
        /// Timestamp of when the order was issued.
        /// </summary>
        [CsvPropertyName("dateIssued")]  //'yyyy-MM-dd'
        public DateTime? DateIssued { get; set; }

        /// <summary>
        /// Timestamp of when the order was scheduled.
        /// </summary>
        [CsvPropertyName("dateScheduled")]  //'yyyy-MM-dd'
        public DateTime? DateScheduled { get; set; }

        /// <summary>
        /// Timestamp of when the order was completed.
        /// </summary>
        [CsvPropertyName("dateCompleted")]  //'yyyy-MM-dd'
        public DateTime? DateCompleted { get; set; }

        /// <summary>
        /// The url link on the order.
        /// </summary>
        [CsvPropertyName("url")]
        public string Url { get; set; }

        /// <summary>
        /// The percent of the order that is complete.
        /// </summary>
        [CsvPropertyName("percentComplete")]
        public string PercentComplete { get; set; }

        /// <summary>
        /// The class category.
        /// </summary>
        [CsvPropertyName("class")]
        public Category Class { get; set; }

        /// <summary>
        /// A list of the manufacture order configurations.
        /// </summary>
        [CsvPropertyName("configurations")]
        public List<ManufactureOrderConfiguration> Configurations { get; set; }

        /// <summary>
        /// A list of custom fields associated with the order.
        /// </summary>
        [CsvPropertyName("customFields")]
        public List<CustomField> CustomFields { get; set; }
    }
}
