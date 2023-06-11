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
    /// This request allows you to import data. The example uses the import to edit/add a vendor. 
    /// Data columns can be blank, but each data column must be represented in the request. 
    /// It is best practice to always include the header rows when importing data.
    /// </summary>
    class ImportRequest : IFishbowlRequest
    {
        [JsonIgnore]
        public string ElementName => "ImportRq";

        [JsonPropertyName("Type")]
        public string Type { get; set; }

        [JsonPropertyName("Rows")]
        public RowsData Data { get; set; }
    }    
}

/*
{
    "FbiJson": {
        "Ticket": {
            "Key": "055f7a56-ffbe-46da-b5be-63b7f7be808d"
        },
        "FbiMsgsRq": {
            "ImportRq": {
                "Type": "ImportVendors",
                "Rows": {
                    "Row": [
                    "\"Name\",\"AddressName\",\"AddressContact\",\"AddressType\",\"IsDefault\",\"Address\",\"City\",\"State\",\"Zip\",\"Country\",\"Main\",\"Home\",\"Work\",\"Mobile\",\"Fax\",\"Email\",\"Pager\",\"Web\",\"Other\",\"DefaultTerms\",\"DefaultShippingTerms\",\"Status\",\"AlertNotes\",\"URL\",\"DefaultCarrier\",\"MinOrderAmount\",\"Active\",\"AccountNumber\",\"CurrencyName\",\"CurrencyRate\",\"CF-Custom1\",\"CF-Custom2\",\"CF-Custom3\",\"CF-Custom4\"",
                    "\"Monroe Bike Company\",\"Williams Bike Company - 210\",\"Williams Bike Company\",\"50\",\"true\",\"Wall st.\",\"New York\",\"NY\",\"21004\",\"UNITED STATES\",\"212-321-5643\",,,,,,,,,,,,,"
                    ]
                }
            }
        }
    }
}
*/