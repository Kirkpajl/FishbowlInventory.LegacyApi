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
    class QuickShipSalesOrderRequest : IFishbowlRequest
    {
        [JsonIgnore]
        public string ElementName => "QuickShipRq";

        [JsonPropertyName("SONumber")]
        public string SalesOrderNumber { get; set; }

        [JsonPropertyName("FulfillServiceItems")]
        public bool FulfillServiceItems { get; set; }

        [JsonPropertyName("LocationGroup")]
        public string LocationGroup { get; set; }

        [JsonPropertyName("ErrorIfNotFulfilled")]
        public bool ErrorIfNotFulfilled { get; set; }

        [JsonPropertyName("ShipDate")]
        public DateTime? ShipDate { get; set; }
    }
}

/*
{
    "FbiJson": {
        "Ticket": {
            "Key": "5ef615e9-5ae0-4ab6-9c44-323254cedf5e"
        },
        "FbiMsgsRq": {
            "QuickShipRq": {
                "SONumber": "50052"
            }
        }
    }
}
*/