using FishbowlInventory.Serialization;

namespace FishbowlInventory.Models
{
    /// <summary>
    /// Stores the details about a Fishbowl custom field.
    /// </summary>
    public class CustomField
    {
        /// <summary>
        /// The custom field's unique identifier.
        /// </summary>
        [CsvPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// The name of the custom field.
        /// </summary>
        [CsvPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// The custom field's value.
        /// </summary>
        [CsvPropertyName("value")]
        public string Value { get; set; }

        /// <summary>
        /// The category of the custom field's value.
        /// </summary>
        [CsvPropertyName("type")]
        public string Type { get; set; }

        //[CsvPropertyName("description")]
        //public string Description { get; set; }

        //[CsvPropertyName("sortOrder")]
        //public int SortOrder { get; set; }

        //[CsvPropertyName("info")]
        //public string Info { get; set; }

        //[CsvPropertyName("requiredFlag")]
        //public bool IsRequired { get; set; }

        //[CsvPropertyName("activeFlag")]
        //public bool IsActive { get; set; }
        
        ///// <summary>
        ///// Custom list objects
        ///// </summary>
        //[CsvPropertyName("customList")]
        //public CustomList CustomList { get; set; }
    }
}
