using FishbowlInventory.Serialization;

namespace FishbowlInventory.Models
{
	public class PartTracking
	{
        /// <summary>
        /// The unique identification number.
        /// </summary>
        [CsvPropertyName("id")]
		public int Id { get; set; }

		[CsvPropertyName("name")]
		public string Name { get; set; }

		[CsvPropertyName("abbr")]
		public string Abbreviation { get; set; }

		[CsvPropertyName("description")]
		public string Description { get; set; }

		[CsvPropertyName("sortOrder")]
		public int SortOrder { get; set; }

		[CsvPropertyName("trackingTypeID")]
		public int TrackingTypeId { get; set; }

		[CsvPropertyName("active")]
		public bool IsActive { get; set; }
	}
}
