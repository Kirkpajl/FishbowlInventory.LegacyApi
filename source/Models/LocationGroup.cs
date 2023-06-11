using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using FishbowlInventory.Serialization;
using System.Threading.Tasks;

namespace FishbowlInventory.Models
{
    /// <summary>
    /// This is an object representing a Fishbowl location group.
    /// </summary>
    public class LocationGroup
    {
        /// <summary>
        /// The location group's unique identification number.
        /// </summary>
        [CsvPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// The location group's name.
        /// </summary>
        [CsvPropertyName("name"), Required]
        public string Name { get; set; }

        /// <summary>
        /// The class category.
        /// </summary>
        [CsvPropertyName("class")]
        public Category Class { get; set; }

        /// <summary>
        /// The location group's active status.
        /// </summary>
        [CsvPropertyName("active")]
        public bool Active { get; set; }
    }
}
