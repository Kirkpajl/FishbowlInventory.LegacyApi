using FishbowlInventory.Serialization;

namespace FishbowlInventory.Models
{
    /// <summary>
    /// A list of Custom Items
    /// </summary>
    public class CustomList
    {
        [CsvPropertyName("id")]
        public int Id { get; set; }

        [CsvPropertyName("name")]
        public string Name { get; set; }

        [CsvPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// Custom List Item objects
        /// </summary>
        [CsvPropertyName("customListItems")]
        public CustomListItem[] CustomListItems { get; set; }
    }
}
