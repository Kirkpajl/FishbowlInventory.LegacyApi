using FishbowlInventory.Models;
using FishbowlInventory.Serialization;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FishbowlInventory.Api.Inventory
{
	/// <summary>
	/// Used to correct inventory amounts.
	/// </summary>
	class CycleCountRequest : IFishbowlRequest
	{
		[JsonIgnore]
		public string ElementName => "CycleCountRq";

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
		public DateTime? Date { get; set; }

		/// <summary>
		/// A list of the tracking items for the inventory. Only used if the part tracks inventory.
		/// </summary>
		[JsonPropertyName("trackingItems")]
		public List<TrackingItem> TrackingItems { get; set; }
    }
}
