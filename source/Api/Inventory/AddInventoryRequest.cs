using FishbowlInventory.Models;
using FishbowlInventory.Serialization;
using System.Text.Json.Serialization;

namespace FishbowlInventory.Api.Inventory
{
	/// <summary>
	/// Adds initial inventory of a part.
	/// </summary>
	class AddInventoryRequest : IFishbowlRequest
	{
        [JsonIgnore]
		public string ElementName => "AddInventoryRq";

        [JsonPropertyName("PartNum")]
		public string PartNumber { get; set; }

		[JsonPropertyName("Quantity")]
		public int Quantity { get; set; }

		[JsonPropertyName("UOMID")]
		public int UnitOfMeasureId { get; set; }

		[JsonPropertyName("Cost")]
		public double Cost { get; set; }

		[JsonPropertyName("Note")]
		public string Note { get; set; }

		[JsonPropertyName("Tracking")]
		public Tracking Tracking { get; set; }

		[JsonPropertyName("LocationTagNum")]
		public int LocationTagNumber { get; set; }

		[JsonPropertyName("TagNum")]
		public int TagNumber { get; set; }


		/*
		/// <summary>
		/// The location for the inventory.
		/// </summary>
		[JsonPropertyName("location")]
		public Location Location { get; set; }

		/// <summary>
		/// The quantity of the part to add to inventory.
		/// </summary>
		[JsonPropertyName("quantity")]
		public int Quantity { get; set; }

		/// <summary>
		/// The part unit of measure.
		/// </summary>
		[JsonPropertyName("uom")]
		public UnitOfMeasure UnitOfMeasure { get; set; }

		/// <summary>
		/// The unit cost for the inventory to be added. If blank, the current part cost will be used.
		/// </summary>
		[JsonPropertyName("unitCost")]
		public double UnitCost { get; set; }

		/// <summary>
		/// A note about the inventory adjustment.
		/// </summary>
		[JsonPropertyName("note")]
		public string Note { get; set; }

		/// <summary>
		/// The customer associated with the inventory adjustment.
		/// </summary>
		[JsonPropertyName("customer")]
		public Customer Customer { get; set; }

        /// <summary>
        /// The class category for the inventory adjustment.
        /// </summary>
        [JsonPropertyName("class")]
        public Category Category { get; set; }

        /// <summary>
        /// The date for the inventory adjustment. If blank, the current date will be used.
        /// </summary>
        [JsonPropertyName("date")]
		[JsonConverter(typeof(DateOnlyConverter))]
		public DateTime? Date { get; set; }

		/// <summary>
		/// A list of the tracking items for the inventory. Only used if the part tracks inventory.
		/// </summary>
		[JsonPropertyName("trackingItems")]
		public List<TrackingItem> TrackingItems { get; set; }
		*/
	}
}
