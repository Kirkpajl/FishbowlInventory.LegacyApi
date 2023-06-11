using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FishbowlInventory.Serialization;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace FishbowlInventory.Api.Inventory
{
    class AddInventoryResponse : IFishbowlResponse<AddInventoryRequest>
    {
        [JsonIgnore]
        public string ElementName => "AddInventoryRs";

        [JsonPropertyName("statusCode")]
        public int StatusCode { get; set; }
    }
}
