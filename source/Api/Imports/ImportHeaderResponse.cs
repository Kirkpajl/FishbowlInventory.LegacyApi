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
    /// This request allows you to get the headers of an import.
    /// </summary>
    class ImportHeaderResponse : IFishbowlResponse<ImportHeaderRequest>
    {
        [JsonIgnore]
        public string ElementName => "ImportHeaderRs";

        [JsonPropertyName("statusCode")]
        public int StatusCode { get; set; }

        [JsonPropertyName("Header")]
        public RowsData Header { get; set; }
        //public HeaderInformation Header { get; set; }
    }

    /*class HeaderInformation
    {
        public string Row { get; set; }



        /// <summary>
        /// Split the CSV row (and remove the double-quotation characters)
        /// </summary>
        /// <returns></returns>
        public string[] GetValues()
        {
            // Ensure that a CSV row was provided
            if (Row == null) return null;

            // Split the CSV row and remove the double-quotation characters
            return Row
                .Split(',')
                .Select(n => n.Replace("\"", ""))
                .ToArray();
        }
    }*/
}

/*{
    "FbiJson": {
        "Ticket": {
            "UserID": 1,
            "Key": "055f7a56-ffbe-46da-b5be-63b7f7be808d"
        },
        "FbiMsgsRs": {
            "statusCode": 1000,
            "ImportHeaderRs": {
                "statusCode": 1000,
                "Header": {
                    "Row": "\"Name\",\"AddressName\",\"AddressContact\",\"AddressType\",\"IsDefault\",\"Address\",\"City\",\"State\",\"Zip\",\"Country\",\"Main\",\"Home\",\"Work\",\"Mobile\",\"Fax\",\"Email\",\"Pager\",\"Web\",\"Other\",\"DefaultTerms\",\"DefaultShippingTerms\",\"Status\",\"AlertNotes\",\"URL\",\"DefaultCarrier\",\"MinOrderAmount\",\"Active\",\"AccountNumber\""
                }
            }
        }
    }
}*/

/*{
   "FbiJson":{
      "Ticket":{
         "UserID":16,
         "Key":"b5848f46-ce54-4882-bac1-6b6932b3f658"
      },
      "FbiMsgsRs":{
         "statusCode":1000,
         "ImportHeaderRs":{
            "statusCode":1000,
            "Header":{
               "Row":[
                  "\"Flag\",\"SONum\",\"Status\",\"CustomerName\",\"CustomerContact\",\"BillToName\",\"BillToAddress\",\"BillToCity\",\"BillToState\",\"BillToZip\",\"BillToCountry\",\"ShipToName\",\"ShipToAddress\",\"ShipToCity\",\"ShipToState\",\"ShipToZip\",\"ShipToCountry\",\"ShipToResidential\",\"CarrierName\",\"TaxRateName\",\"PriorityId\",\"PONum\",\"VendorPONum\",\"Date\",\"Salesman\",\"ShippingTerms\",\"PaymentTerms\",\"FOB\",\"Note\",\"QuickBooksClassName\",\"LocationGroupName\",\"OrderDateScheduled\",\"URL\",\"CarrierService\",\"DateExpired\",\"Phone\",\"Email\",\"Category\"",
                  "\"Flag\",\"SOItemTypeID\",\"ProductNumber\",\"ProductDescription\",\"ProductQuantity\",\"UOM\",\"ProductPrice\",\"Taxable\",\"TaxCode\",\"Note\",\"ItemQuickBooksClassName\",\"ItemDateScheduled\",\"ShowItem\",\"KitItem\",\"RevisionLevel\",\"CustomerPartNumber\""
               ]
            }
         }
      }
   }
}*/