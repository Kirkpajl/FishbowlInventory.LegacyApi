using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FishbowlInventory.Serialization;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace FishbowlInventory.Api.Memos
{
    class AddMemoResponse : IFishbowlResponse<AddMemoRequest>
    {
        [JsonIgnore]
        public string ElementName => "AddMemoRs";

        [JsonPropertyName("statusCode")]
        public int StatusCode { get; set; }
    }
}
