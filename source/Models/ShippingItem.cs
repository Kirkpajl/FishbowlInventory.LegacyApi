using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FishbowlInventory.Serialization;
using System.Threading.Tasks;

namespace FishbowlInventory.Models
{
    public class ShippingItem
    {
        [CsvPropertyName("shipItemId")]
        public int Id { get; set; }

        [CsvPropertyName("productNumber")]
        public string ProductNumber { get; set; }

        [CsvPropertyName("productDescription")]
        public string ProductDescription { get; set; }

        [CsvPropertyName("qtyShipped")]
        public int QuantityShipped { get; set; }

        [CsvPropertyName("uom")]
        public UnitOfMeasure UnitOfMeasure { get; set; }

        [CsvPropertyName("cost")]
        public int Cost { get; set; }

        [CsvPropertyName("sku")]
        public string SKU { get; set; }

        [CsvPropertyName("upc")]
        public string UPC { get; set; }

        [CsvPropertyName("orderItemId")]
        public int OrderItemId { get; set; }

        [CsvPropertyName("orderLineItem")]
        public int OrderLineItem { get; set; }

        [CsvPropertyName("cartonName")]
        public string Name { get; set; }

        [CsvPropertyName("tagNum")]
        public int TagNumber { get; set; }

        [CsvPropertyName("weight")]
        public decimal Weight { get; set; }

        [CsvPropertyName("weightUOM")]
        public UnitOfMeasure WeightUnitOfMeasure { get; set; }

        [CsvPropertyName("displayWeight")]
        public decimal DisplayWeight { get; set; }

        [CsvPropertyName("displayWeightUOM")]
        public UnitOfMeasure DisplayWeightUnitOfMeasure { get; set; }

        [CsvPropertyName("tracking")]
        public string Tracking { get; set; }
    }
}
