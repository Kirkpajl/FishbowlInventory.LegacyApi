using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using FishbowlInventory.Serialization;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace FishbowlInventory.Api.WorkOrders
{
    /// <summary>
    /// Add an item to an existing open WO.
    /// </summary>
    class AddWorkOrderItemResponse : IFishbowlResponse<AddWorkOrderItemRequest>
    {
        [JsonIgnore]
        public string ElementName => "AddWorkOrderItemRs";

        [JsonPropertyName("statusCode")]
        public int StatusCode { get; set; }
    }
}

/*
//{
//    "FbiJson": {
//        "Ticket": {
//            "UserID": 2,
//            "Key": "8ccad466-8688-4174-b843-f97c2e0b0334"
//        },
//        "FbiMsgsRs": {
//            "statusCode": 1000,
//            "IssueSORs": {
//                "statusCode": 1000
//                }
//            }
//        }
//    }
//}
*/