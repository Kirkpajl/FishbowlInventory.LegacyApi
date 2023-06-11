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
    /// This request is used to void shipments that are in a packed status or shipped status.
    /// </summary>
    class VoidSalesOrderRequest : IFishbowlRequest
    {
        [JsonIgnore]
        public string ElementName => "VoidSORq";

        [JsonPropertyName("SONumber")]
        public string SalesOrderNumber { get; set; }
    }
}

/*
{
    "FbiJson": {
        "Ticket": {
            "Key": "5ef615e9-5ae0-4ab6-9c44-323254cedf5e"
        },
        "FbiMsgsRq": {
            "VoidSORq": {
                "SONumber": "50052"
            }
        }
    }
}
*/