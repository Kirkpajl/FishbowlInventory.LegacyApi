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
    /// This request returns a list of your import options.
    /// </summary>
    class ImportListResponse : IFishbowlResponse<ImportListRequest>
    {
        [JsonIgnore]
        public string ElementName => "ImportListRs";

        [JsonPropertyName("statusCode")]
        public int StatusCode { get; set; }

        [JsonPropertyName("ImportNames")]
        public ImportNamesData Data { get; set; }
    }

    class ImportNamesData
    {
        [JsonPropertyName("ImportName")]
        public string[] Names { get; set; }
    }
}



/*
{
    "FbiJson": {
        "Ticket": {
            "UserID": 1,
            "Key": "055f7a56-ffbe-46da-b5be-63b7f7be808d"
        },
        "FbiMsgsRs": {
            "statusCode": 1000,
            "ImportListRs": {
                "statusCode": 1000,
                "ImportNames": {
                    "ImportName": [
                        "ImportAddInventory",
                        "ImportAssociatedPricing",
                        "ImportAssociatedPricingType",
                        "ImportBillOfMaterials",
                        "ImportBillOfMaterialsDetails",
                        "ImportBillOfMaterialsInstructions",
                        "ImportCarriers",
                        "ImportCountryAndState",
                        "ImportCurrency",
                        "ImportCustomFieldData",
                        "ImportCustomFieldLists",
                        "ImportCustomFields",
                        "ImportCustomerGroupRelations",
                        "ImportCustomerParts",
                        "ImportCustomers",
                        "ImportCycleCountData",
                        "ImportDefaultLocations",
                        "ImportDiscounts",
                        "ImportGenericShoppingCart",
                        "ImportInventoryMove",
                        "ImportKitItems",
                        "ImportLocations",
                        "ImportMemoData",
                        "ImportMivaShoppingCart",
                        "ImportOSCommerceShoppingCart",
                        "ImportPackingData",
                        "ImportPart",
                        "ImportPartAndProductRenaming",
                        "ImportPartAndQuantity",
                        "ImportPartCost",
                        "ImportPartProductAndVendorPricing",
                        "ImportPartStandardCost",
                        "ImportPartUnitsOfMeasure",
                        "ImportPaymentData",
                        "ImportPaymentTerms",
                        "ImportPickingData",
                        "ImportPricingRules",
                        "ImportProduct",
                        "ImportProductPricing",
                        "ImportProductTree",
                        "ImportProductTreeCategories",
                        "ImportPurchaseOrder",
                        "ImportQuickBooksClass",
                        "ImportReceivingData",
                        "ImportReorderLevels",
                        "ImportSalesOrder",
                        "ImportSalesOrderDetails",
                        "ImportScrapData",
                        "ImportShipCartonTracking",
                        "ImportShippingData",
                        "ImportTaxRates",
                        "ImportTransferOrder",
                        "ImportUnitOfMeasureConversions",
                        "ImportUnitsOfMeasure",
                        "ImportUsers",
                        "ImportVendorCostRules",
                        "ImportVendorParts",
                        "ImportVendors",
                        "ImportWorkOrderSteps",
                        "ImportYahooShoppingCart"
                    ]
                }
            }
        }
    }
}
*/