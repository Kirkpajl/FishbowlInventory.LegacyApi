using FishbowlInventory.Enumerations;
using FishbowlInventory.Models;
using FishbowlInventory.Serialization;
using System.Text.Json.Serialization;

namespace FishbowlInventory.Api.Memos
{
    /// <summary>
    /// Adds a memo to the specified object.
    /// </summary>
    class AddMemoRequest : IFishbowlRequest
    {
        [JsonIgnore]
        public string ElementName => "AddMemoRq";

        [JsonPropertyName("ItemType")]
        public FishbowlItemType ItemType { get; set; }

        [JsonPropertyName("PartNum")]
        public string PartNumber { get; set; }

        [JsonPropertyName("ProductNum")]
        public string ProductNumber { get; set; }

        [JsonPropertyName("OrderNum")]
        public string OrderNumber { get; set; }

        [JsonPropertyName("CustomerNum")]
        public string CustomerNumber { get; set; }

        [JsonPropertyName("VendorNum")]
        public string VendorNumber { get; set; }

        [JsonPropertyName("Memo")]
        public Memo Memo { get; set; }
    }
}
