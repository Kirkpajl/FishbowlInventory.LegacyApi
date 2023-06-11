using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FishbowlInventory.Serialization;
using System.Threading.Tasks;

namespace FishbowlInventory.Models
{
    /// <summary>
    /// Stores tax rate information.
    /// </summary>
    public class TaxRate
    {
        /// <summary>
        /// The unique identification number
        /// </summary>
        [CsvPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// The name of the object.
        /// </summary>
        [CsvPropertyName("name")]
        public string Name { get; set; }

        [CsvPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// The tax rate.
        /// </summary>
        [CsvPropertyName("rate")]
        public double Rate { get; set; }

        [CsvPropertyName("typeID")]
        public int TypeId { get; set; }

        [CsvPropertyName("vendorID")]
        public int VendorId { get; set; }

        [CsvPropertyName("defaultFlag")]
        public bool IsDefault { get; set; }

        [CsvPropertyName("activeFlag")]
        public bool IsActive { get; set; }
    }
}
