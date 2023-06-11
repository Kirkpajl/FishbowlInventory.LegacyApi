using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using FishbowlInventory.Serialization;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace FishbowlInventory.Api.SaleOrders
{
    /// <summary>
    /// Will do an automatic pick, pack, and ship on a Sales Order that has already been issued (Work Orders tied to the SO must be fulfilled also). 
    /// Fishbowl automatically picks the best tracking available. FulfillServiceItems indicates if the service items on the SO should be fulfilled during ship.
    /// </summary>
    class QuickShipSalesOrderResponse : IFishbowlResponse<QuickShipSalesOrderRequest>
    {
        [JsonIgnore]
        public string ElementName => "QuickShipRs";

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
//            "QuickShipRs": {
//                "statusCode": 1000
//                }
//            }
//        }
//    }
//}
*/