using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using FishbowlInventory.Serialization;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace FishbowlInventory.Api.Queries
{
    /// <summary>
    /// Returns results of sql query in csv format. Two options are available. 
    /// A query that has been saved in the Fishbowl Client data module can be executed 
    /// using <Name> or a query can be placed directly in the call using <Query>.
    /// </summary>
    class ExecuteQueryRequest : IFishbowlRequest
    {
        [JsonIgnore]
        public string ElementName => "ExecuteQueryRq";

        [JsonPropertyName("Query")]
        public string Query { get; set; }
        
        [JsonPropertyName("Name")]
        public string Name { get; set; }
    }
}

/*
{
    "FbiJson": {
        "Ticket": {
            "Key": "5ef615e9-5ae0-4ab6-9c44-323254cedf5e"
        },
        "FbiMsgsRq": {
            "ExecuteQueryRq": {
                "Name": "PartList"
            }
        }
    }
}
*/

/*
{
    "FbiJson": {
        "Ticket": {
            "Key": "5ef615e9-5ae0-4ab6-9c44-323254cedf5e"
        },
        "FbiMsgsRq": {
            "ExecuteQueryRq": {
                "Query": "SELECT part.num AS partNumber, part.description AS partDescription FROM part"
            }
        }
    }
}
*/