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
    class ImportResponse : IFishbowlResponse<ImportRequest>
    {
        [JsonIgnore]
        public string ElementName => "ImportRs";

        [JsonPropertyName("statusCode")]
        public int StatusCode { get; set; }
    }
}

/*{
    "FbiJson": {
        "Ticket": {
            "UserID": 1,
            "Key": "055f7a56-ffbe-46da-b5be-63b7f7be808d"
        },
        "FbiMsgsRs": {
            "statusCode": 1000,
            "ImportRs": {
                "statusCode": 1000
            }
        }
    }
}*/

/*{
   "FbiJson":{
      "Ticket":{
         "UserID":16,
         "Key":"6c3faec4-4560-486a-b404-e2cb6a9a27ec"
      },
      "FbiMsgsRs":{
         "statusCode":1000,
         "ImportRs":{
            "statusCode":1500,
            "statusMessage":"The following lines of the CSV import do not have the correct format or contain incompatible data. Due to these errors, only some information was imported. Please make the appropriate changes to these lines and re-import the file.\n\nExpected flag 'PO', but found 'Flag,PONum,Status,VendorName,VendorContact,RemitToName,RemitToAddress,RemitToCity,RemitToState,RemitToZip,RemitToCountry,ShipToName,DeliverToName,ShipToAddress,ShipToCity,ShipToState,ShipToZip,ShipToCountry,CarrierName,CarrierService,VendorSONum,CustomerSONum,CreatedDate,CompletedDate,ConfirmedDate,FulfillmentDate,IssuedDate,Buyer,ShippingTerms,PaymentTerms,FOB,Note,QuickBooksClassName,LocationGroupName,URL,Phone,Email\"'\nLine Number: 1"
         }
      }
   }
}*/