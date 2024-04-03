using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using FishbowlInventory.Serialization;
using System.Threading.Tasks;
using FishbowlInventory.Models;
using System.Text.Json.Serialization;

namespace FishbowlInventory.Api.WorkOrders
{
    /// <summary>
    /// Add an item to an existing open WO.
    /// </summary>
    class AddWorkOrderItemRequest : IFishbowlRequest
    {
        [JsonIgnore]
        public string ElementName => "AddWorkOrderItemRq";

        [JsonPropertyName("OrderNum")]
        public string OrderNumber { get; set; }

        [JsonPropertyName("TypeId")]
        public int TypeId { get; set; }

        [JsonPropertyName("Description")]
        public string Description { get; set; }

        [JsonPropertyName("PartNum")]
        public string PartNumber { get; set; }

        [JsonPropertyName("Quantity")]
        public decimal Quantity { get; set; }

        [JsonPropertyName("UOMCode")]
        public string UnitOfMeasureCode { get; set; }

        [JsonPropertyName("Cost")]
        public decimal Cost { get; set; }
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