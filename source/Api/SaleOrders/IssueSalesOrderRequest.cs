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
    /// Will issue a SalesOrder.
    /// </summary>
    class IssueSalesOrderRequest : IFishbowlRequest
    {
        [JsonIgnore]
        public string ElementName => "IssueSORq";

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
            "IssueSORq": {
                "SONumber": "50054"
            }
        }
    }
}
*/