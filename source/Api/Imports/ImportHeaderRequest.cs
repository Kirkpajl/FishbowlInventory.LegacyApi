using FishbowlInventory.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using FishbowlInventory.Serialization;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace FishbowlInventory.Api.Imports
{
    /// <summary>
    /// This request allows you to get the headers of an import.
    /// </summary>
    class ImportHeaderRequest : IFishbowlRequest
    {
        [JsonIgnore]
        public string ElementName => "ImportHeaderRq";

        [JsonPropertyName("Type")]
        public string Type { get; set; }
    }
}

/*
{
    "FbiJson": {
        "Ticket": {
            "Key": "055f7a56-ffbe-46da-b5be-63b7f7be808d"
        },
        "FbiMsgsRq": {
            "ImportHeaderRq": {
                "Type": "ImportVendors"
            }
        }
    }
}
*/