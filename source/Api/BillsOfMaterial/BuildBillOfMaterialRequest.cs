using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using FishbowlInventory.Serialization;
using System.Threading.Tasks;
using FishbowlInventory.Models;
using System.Text.Json.Serialization;

namespace FishbowlInventory.Api.BillsOfMaterial
{
    /// <summary>
    /// Adjust the inventory in a tag.
    /// </summary>
    class BuildBillOfMaterialRequest : IFishbowlRequest
    {
        [JsonIgnore]
        public string ElementName => "BuildBomRq";

        [JsonPropertyName("BomNumber")]
        public string Number { get; set; }

        [JsonPropertyName("Quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("DateScheduled")]
        public DateTime DateScheduled { get; set; }
    }
}

/*
{
    "FbiJson": {
        "Ticket": {
            "Key": "5ef615e9-5ae0-4ab6-9c44-323254cedf5e"
        },
        "FbiMsgsRq": {
            "AddWorkOrderItemRq": {
                "OrderNum": "502 001",
                "Description": "This will create a note item."
            }
        }
    }
}
*/