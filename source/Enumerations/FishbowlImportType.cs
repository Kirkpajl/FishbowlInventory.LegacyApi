using System;
using System.Collections.Generic;
using System.Text;

namespace FishbowlInventory.Enumerations
{
    internal enum FishbowlImportType
    {
        [FishbowlImport("ImportAddInventory", "Add Inventory")]
        AddInventory,
        [FishbowlImport("ImportAssocPricing", "Associated Pricing")]
        AssociatedPricing,
        [FishbowlImport("ImportAssocPricingType", "Associated Pricing Type")]
        AssociatedPricingType,
        [FishbowlImport("ImportBillOfMaterials", "Bill of Materials")]
        BillofMaterials,
        [FishbowlImport("ImportBillOfMaterialsDetails", "Bill of Materials Details")]
        BillofMaterialsDetails,
        [FishbowlImport("ImporBOMInstructions", "Bill of Materials Instructions")]
        BillofMaterialsInstructions,
        [FishbowlImport("ImportCarriers", "Carriers")]
        Carriers,
        [FishbowlImport("ImportCountryState", "Country And State")]
        CountryAndState,
        [FishbowlImport("ImportCurrency", "Currency")]
        Currency,
        [FishbowlImport("ImportCustomFieldData", "Custom Field Data")]
        CustomFieldData,
        [FishbowlImport("ImportCustomFieldLists", "Custom Field Lists")]
        CustomFieldLists,
        [FishbowlImport("ImportCustomFields", "Custom Fields")]
        CustomFields,
        [FishbowlImport("ImportCustomerGroupRelations", "Customer Group Relations")]
        CustomerGroupRelations,
        [FishbowlImport("ImportCustomerParts", "Customer Parts")]
        CustomerParts,
        [FishbowlImport("ImportCustomers", "Customers")]
        Customers,
        [FishbowlImport("ImportCycleCount", "Cycle Count Data")]
        CycleCountData,
        [FishbowlImport("ImportDefaultLocations", "Default Locations")]
        DefaultLocations,
        [FishbowlImport("ImportDiscounts", "Discounts")]
        Discounts,
        [FishbowlImport("ImportEDIShipping", "EDI Shipping")]
        EDIShipping,
        [FishbowlImport("ImportGenericSC", "Generic Shopping Cart")]
        GenericShoppingCart,
        [FishbowlImport("ImportInvMove", "Inventory Move")]
        InventoryMove,
        [FishbowlImport("ImportInvQtys", "Inventory Quantities")]
        InventoryQuantities,
        [FishbowlImport("ImportKitItems", "Kit Items")]
        KitItems,
        [FishbowlImport("ImportLocations", "Locations")]
        Locations,
        [FishbowlImport("ImportMemoData", "Memo Data")]
        MemoData,
        [FishbowlImport("ImportMIVA", "MIVA Shopping Cart")]
        MIVAShoppingCart,
        [FishbowlImport("ImportOSCommerce", "OS Commerce Shopping Cart")]
        OSCommerceShoppingCart,
        [FishbowlImport("ImportPacking", "Packing Data")]
        PackingData,
        [FishbowlImport("ImportPart", "Part")]
        Part,
        [FishbowlImport("ImportPartAndProductRenaming", "Part and Product Renaming")]
        PartandProductRenaming,
        [FishbowlImport("ImportPartAndQuantity", "Part and Quantity")]
        PartandQuantity,
        [FishbowlImport("ImportPartCost", "Part Cost")]
        PartCost,
        [FishbowlImport("ImportPartStdCost", "Part Standard Cost")]
        PartStandardCost,
        [FishbowlImport("ImportPartUOMs", "Part Units of Measure")]
        PartUnitsofMeasure,
        [FishbowlImport("ImportPayments", "Payment Data")]
        PaymentData,
        [FishbowlImport("ImportPaymentTerms", "Payment Terms")]
        PaymentTerms,
        [FishbowlImport("ImportPick", "Picking Data")]
        PickingData,
        [FishbowlImport("ImportPricingRules", "Pricing Rules")]
        PricingRules,
        [FishbowlImport("ImportProduct", "Product")]
        Product,
        [FishbowlImport("ImportProductPricing", "Product Pricing")]
        ProductPricing,
        [FishbowlImport("ImportProductTree", "Product Tree")]
        ProductTree,
        [FishbowlImport("ImportProductTreeCategories", "Product Tree Categories")]
        ProductTreeCategories,
        [FishbowlImport("ImportPurchaseOrder", "Purchase Order")]
        PurchaseOrder,
        //[FishbowlImport("ImportPurchaseOrderAsSalesOrder", "Purchase Order As Sales Order")]
        //PurchaseOrderAsSalesOrder,
        [FishbowlImport("ImportPurchaseOrderLineItem", "Purchase Order Line Items")]
        PurchaseOrderLineItems,
        [FishbowlImport("ImportQBClass", "QuickBooks Class")]
        QuickBooksClass,
        [FishbowlImport("ImportReceiving", "Receiving Data")]
        ReceivingData,
        [FishbowlImport("ImportReorderLevels", "Reorder Levels")]
        ReorderLevels,
        [FishbowlImport("ImportSalesOrder", "Sales Order")]
        SalesOrder,
        [FishbowlImport("ImportSalesOrderDetails", "Sales Order Details")]
        SalesOrderDetails,
        [FishbowlImport("ImportScrapData", "Scrap Data")]
        ScrapData,
        [FishbowlImport("ImportShipCarton", "Ship Carton")]
        ShipCarton,
        [FishbowlImport("ImportShipCartonTracking", "Ship Carton Tracking")]
        ShipCartonTracking,
        [FishbowlImport("ImportShip", "Shipping Data")]
        ShippingData,
        [FishbowlImport("ImportTagData", "Tag Information")]
        TagInformation,
        [FishbowlImport("ImportTaxRates", "Tax Rates")]
        TaxRates,
        [FishbowlImport("ImportTransferOrder", "Transfer Order")]
        TransferOrder,
        [FishbowlImport("ImportUOMConv", "Unit of Measure Conversions")]
        UnitofMeasureConversions,
        [FishbowlImport("ImportUOMs", "Units of Measure")]
        UnitsofMeasure,
        [FishbowlImport("ImportUsers", "Users")]
        Users,
        [FishbowlImport("ImportVendorCostRules", "Vendor Cost Rules")]
        VendorCostRules,
        [FishbowlImport("ImportVendorParts", "Vendor Parts")]
        VendorParts,
        [FishbowlImport("PPVendorPricing", "Vendor Pricing")]
        VendorPricing,
        [FishbowlImport("ImportVendors", "Vendors")]
        Vendors,
        [FishbowlImport("ImportYahoo", "Yahoo Shopping Cart")]
        YahooShoppingCart,
        [FishbowlImport("ImportWoSteps", "Work Order Steps")]
        WorkOrderSteps
    }



    // Name	                            Description
    // Add Inventory                    Import toadd inventory.The quantity in the import will increase(not adjust) existing inventory.
    // Associated Pricing               Import/Export associated pricing.
    // Associated Pricing Type          Import/Export associated pricing types.
    // Bill of Materials                Import/Export abill of materials.
    // Bill of Materials Details        Import/Export details for a bill of materials.
    // Bill of Materials Instructions   Import/Export instruction steps for a bill of materials.
    // Carriers                         Import/Export carriers.
    // Country And State                Import/Export Country and State information.
    // Currency                         Import/Export currencyconversion information.
    // Custom Field Data                Import custom fielddata.
    // Custom Field Lists               Import/Export custom field lists.
    // Custom Fields                    Import/Export custom fields.
    // Customer Group Relations         Import/Export customer group relations.
    // Customer Parts                   Import/Export customer part numbers.
    // Customers                        Import/Export customers.
    // Cycle Count Data                 Import/Export to perform acycle count.
    // Default Locations                Import/Export part default locations.
    // Discounts                        Import/Export discounts.
    // EDI Shipping                     Export an EDI Shipping 856SOPI file.
    // Generic Shopping Cart            Import information from a generic shopping cart.
    // Inventory Move                   Import to perform aninventory move.
    // Inventory Quantities             Export current inventory quantities.
    // Kit Items                        Import/Export kit items.
    // Locations                        Import/Export locationsand Location Groups.
    // Memo Data                        Import memo data information.
    // MIVA Shopping Cart               Import information from a MIVA shopping cart.
    // OS Commerce Shopping Cart        Import information from anOS Commerce shopping cart.
    // Packing Data                     Import orders that have been packed.
    // Part                             Import/Export parts.
    // Part and Product Renaming        Import/Export partand productnames.
    // Part and Quantity                Import basic partinformation and quantity.
    // Part Cost                        Import/Export partaverage costs.
    // Part, Product, Vendor Pricing    Import/Export part, product and vendor pricing.
    // Part Standard Cost               Import/Export partstandard costs.
    // Part Units of Measure            Import/Export partunits of measure.
    // Payment Data                     Import/Export payments.
    // Payment Terms                    Import/Export payment terms.
    // Picking Data                     Import orders that have been picked.
    // Pricing Rules                    Import/Export pricing rules.
    // Product                          Import/Export products.
    // Product Pricing                  Import/Export prices for products.
    // Product Tree                     Import/Export products and the corresponding product tree categories.
    // Product Tree Categories          Import/Export product tree categories.
    // Purchase Order                   Import/Export purchase orders.
    // Purchase Order As Sales Order    Export a purchase order that can beimportedas a sales order.
    // Purchase Order Line Items        Export purchase order line items.
    // QuickBooks Class                 Import/Export classes.
    // Receiving Data                   Import/Export a record of what needs to bereceived from outstanding purchase orders.
    // Reorder Levels                   Import/Export reorder levels.
    // Sales Order                      Import/Export sales orders.
    // Sales Order Details              Import/Export changes to sales order details.
    // Scrap Data                       Import details of inventory that has been scrapped.
    // Ship Carton                      Export ship carton information.
    // Ship Carton Tracking             Import/Export ship carton tracking information.
    // Shipping Data                    Import orders that have been shipped.
    // Tag Information                  Export inventory tags. A tag is an internal number used to group similar items together.
    // Tax Rates                        Import/Export tax rates.
    // Transfer Order                   Import/Export transfer orders.
    // Unit of Measure Conversions      Import/Export unit of measure conversions.
    // Units of Measure                 Import/Export units of measure.
    // Users                            Import/Export users.
    // Vendor Cost Rules                Import/Export vendor cost rules.
    // Vendor Parts                     Import/Export vendor part information.
    // Vendors                          Import/Export vendors.
    // Yahoo Shopping Cart              Import information from a Yahoo Shopping Cart.
    // Work Order Steps                 Import/Export work order instruction steps.


}
