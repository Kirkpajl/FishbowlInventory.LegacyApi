using FishbowlInventory.Serialization;

namespace FishbowlInventory.Models
{
    /// <summary>
    /// Stores information about an item found in a work order.
    /// </summary>
    public class WorkOrderItem
    {
        [CsvPropertyName("Id")]
        public int Id { get; set; }

        [CsvPropertyName("ManufacturingOrderItemId")]
        public int ManufacturingOrderItemId { get; set; }

        [CsvPropertyName("TypeId")]
        public int TypeId { get; set; }

        [CsvPropertyName("PartNumber")]
        public string PartNumber { get; set; }

        [CsvPropertyName("Description")]
        public string Description { get; set; }

        [CsvPropertyName("Cost")]
        public decimal Cost { get; set; }

        [CsvPropertyName("QuantityScrapped")]
        public decimal QuantityScrapped { get; set; }

        [CsvPropertyName("QuantityTarget")]
        public decimal QuantityTarget { get; set; }

        [CsvPropertyName("QuantityUsed")]
        public decimal QuantityUsed { get; set; }

        [CsvPropertyName("UOM")]
        public UnitOfMeasure UnitOfMeasure { get; set; }

        [CsvPropertyName("SortId")]
        public int SortId { get; set; }
    }
}
