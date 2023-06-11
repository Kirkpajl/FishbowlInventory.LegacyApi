using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FishbowlInventory.Serialization;
using System.Threading.Tasks;

namespace FishbowlInventory.Models
{
    /// <summary>
    /// Represents a Location.
    /// </summary>
    public class Location
    {
        [CsvPropertyName("locationID")]
        public int LocationId { get; set; }

        [CsvPropertyName("typeID")]
        public int TypeId { get; set; }

        [CsvPropertyName("parentID")]
        public int ParentId { get; set; }

        [CsvPropertyName("name")]
        public string Name { get; set; }

        [CsvPropertyName("description")]
        public string Description { get; set; }

        [CsvPropertyName("countedAsAvailable")]
        public bool CountedAsAvailable { get; set; }

        [CsvPropertyName("default")]
        public bool IsDefault { get; set; }

        [CsvPropertyName("active")]
        public bool IsActive { get; set; }

        [CsvPropertyName("pickable")]
        public bool IsPickable { get; set; }

        [CsvPropertyName("receivable")]
        public bool IsReceivable { get; set; }

        [CsvPropertyName("locationGroupID")]
        public int LocationGroupId { get; set; }

        [CsvPropertyName("locationGroupName")]
        public string LocationGroupName { get; set; }

        [CsvPropertyName("enforceTracking")]
        public bool EnforceTracking { get; set; }

        [CsvPropertyName("sortOrder")]
        public int SortOrder { get; set; }
    }
}
