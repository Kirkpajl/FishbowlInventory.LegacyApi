using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FishbowlInventory.Serialization;
using System.Threading.Tasks;

namespace FishbowlInventory.Models
{
    /// <summary>
    /// Stores Custom Items.
    /// </summary>
    public class CustomListItem
    {
        [CsvPropertyName("id")]
        public int Id { get; set; }

        [CsvPropertyName("name")]
        public string Name { get; set; }

        [CsvPropertyName("description")]
        public string Description { get; set; }
    }
}
