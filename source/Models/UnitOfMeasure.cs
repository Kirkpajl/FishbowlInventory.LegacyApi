using FishbowlInventory.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FishbowlInventory.Serialization;
using System.Threading.Tasks;

namespace FishbowlInventory.Models
{
    /// <summary>
    /// Fishbowl unit of measure (UOM).
    /// </summary>
    public class UnitOfMeasure
    {
        /// <summary>
        /// The UOM's unique identification number.
        /// </summary>
        [CsvPropertyName("id")]  //UOMID
        public int Id { get; set; }

        /// <summary>
        /// The UOM name.
        /// </summary>
        [CsvPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// The UOM abbreviation.
        /// </summary>
        [CsvPropertyName("abbreviation")]
        public string Abbreviation { get; set; }

        /// <summary>
        /// The UOM description.
        /// </summary>
        [CsvPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// The basic type of the UOM.
        /// </summary>
        [CsvPropertyName("type")]
        public UnitOfMeasureType Type { get; set; }

        /// <summary>
        /// The active status of the UOM.
        /// </summary>
        [CsvPropertyName("active")]
        public bool IsActive { get; set; }

        /// <summary>
        /// Indicates if the quantity must be a whole number.
        /// </summary>
        [CsvPropertyName("integral")]
        public bool IsIntegral { get; set; }

        /// <summary>
        /// Indicates if the UOM is read-only.
        /// </summary>
        [CsvPropertyName("readOnly")]
        public bool ReadOnly { get; set; }

        /// <summary>
        /// A list of the UOM conversions.
        /// </summary>
        [CsvPropertyName("conversions")]
        public UnitOfMeasureConversion[] Conversions { get; set; }
    }
}
