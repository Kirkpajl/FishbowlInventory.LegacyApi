using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using FishbowlInventory.Serialization;
using System.Threading.Tasks;
using FishbowlInventory.Models;
using System.Text.Json.Serialization;

namespace FishbowlInventory.Api.SaleOrders
{
    /// <summary>
    /// Adds an item to a Sales Order.
    /// </summary>
    class AddSalesOrderItemRequest : IFishbowlRequest
    {
        [JsonIgnore]
        public string ElementName => "AddSOItemRq";

        [JsonPropertyName("OrderNum")]
        public string SalesOrderNumber { get; set; }

        [JsonPropertyName("SalesOrderItem")]
        public SalesOrderItem Item { get; set; }
    }
}

/*
{
    "FbiJson": {
        "Ticket": {
            "Key": "5ef615e9-5ae0-4ab6-9c44-323254cedf5e"
        },
        "FbiMsgsRq": {
            "AddSOItemRq": {
                "OrderNum": "50050",
                "SalesOrderItem": {
                    "ID": -1,
                    "ProductNumber": "BTY100-Core",
                    "SOID": 94,
                    "Description": "Battery Pack",
                    "Taxable": true,
                    "Quantity": 1,
                    "ProductPrice": -95.00,
                    "TotalPrice": -95.00,
                    "UOMCode": "ea",
                    "ItemType": 20,
                    "Status": 10,
                    "QuickBooksClassName": "Salt Lake City"
                }
            }
        }
    }
}
*/