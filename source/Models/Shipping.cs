using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FishbowlInventory.Serialization;
using System.Threading.Tasks;

namespace FishbowlInventory.Models
{
    /// <summary>
    /// Contains the details about a shipment.
    /// </summary>
    public class Shipping
    {
        [CsvPropertyName("id")]
        public int Id { get; set; }

        [CsvPropertyName("orderNumber")]
        public int OrderNumber { get; set; }

        [CsvPropertyName("orderType")]
        public string OrderType { get; set; }

        [CsvPropertyName("createdDate")]
        public DateTime? CreatedDate { get; set; }

        [CsvPropertyName("dateLastModified")]
        public DateTime? DateLastModified { get; set; }

        [CsvPropertyName("carrier")]
        public string Carrier { get; set; }

        [CsvPropertyName("status")]
        public int Status { get; set; }

        /// <summary>
        /// Origin
        /// </summary>
        [CsvPropertyName("fob")]
        public string FOB { get; set; }

        [CsvPropertyName("note")]
        public string Note { get; set; }

        [CsvPropertyName("cartonCount")]
        public int CartonCount { get; set; }

        [CsvPropertyName("address")]
        public Address Address { get; set; }

        [CsvPropertyName("cartons")]
        public Carton[] Cartons { get; set; }
    }
}
