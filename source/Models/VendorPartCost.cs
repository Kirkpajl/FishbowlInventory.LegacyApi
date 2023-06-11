using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FishbowlInventory.Serialization;
using System.Threading.Tasks;

namespace FishbowlInventory.Models
{
    public class VendorPartCost
    {
        [CsvPropertyName("totalCost")]
        public double TotalCost { get; set; }

        [CsvPropertyName("unitCost")]
        public double UnitCost { get; set; }

        /// <summary>
        /// The part quantity associated with the cost.
        /// </summary>
        [CsvPropertyName("quantity")]
        public double Quantity { get; set; }

        /// <summary>
        /// The part quantity associated with the cost.
        /// </summary>
        [CsvPropertyName("minimumQuantity")]
        public double MinimumQuantity { get; set; }

        [CsvPropertyName("uom")]
        public UnitOfMeasure UnitOfMeasure { get; set; }

        [CsvPropertyName("vendorPartNum")]
        public string VendorPartNumber { get; set; }
    }
}
