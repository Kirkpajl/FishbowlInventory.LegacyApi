using FishbowlInventory.Api;
using FishbowlInventory.Api.Queries;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FishbowlInventory.Serialization
{
    class RowsDataJsonConverter : JsonConverter<RowsData>
    {
        // {"Row":"\"Id\",\"CustomerId\",\"LastFulfillmentDate\",\"FulfillmentDate\",\"Description\",\"McTotalCost\",\"Note\",\"PartNumber\",\"LineItem\",\"FulfilledQuantity\",\"PickedQuantity\",\"PartQuantity\",\"Repair\",\"RevisionLevel\",\"StatusId\",\"TaxRate\",\"CostToBeDetermined\",\"TotalCost\",\"TypeId\",\"PartPrice\",\"VendorPartNumber\",\"OutsourcedPartNumber\",\"OutsourcedPartDescription\",\"CustomerName\",\"QuickBooksClassName\",\"StatusName\",\"TaxRateName\",\"TypeName\",\"UOM\",\"PartDescription\""}

        // {"Row":["\"name\"","\"ACE HARDWARE\"","\"AIRGAS\"","\"ALCO MACHINE\"","\"AMAZON\"","\"AMERICAN PIPE AND SUPPLY\"","\"ATLANTA CASTER\"","\"BARTON INTERNATIONAL\"","\"BEST BUY METALS\"","\"BOILER TUBE COMPANY OF AMERICA\"","\"CHATTANOOGA INDUSTRIAL MOTORS, INC.\"","\"CHATTANOOGA RUBBER & GASKETS\"","\"CLEMCO\"","\"COOSA STEEL CORPORATION\"","\"COPELANDS, INC\"","\"CULLIGAN WATER CONDITIONING\"","\"D & B POWDER COATING\"","\"DELONG EQUIPMENT\"","\"DELTA RIGGING & TOOLS\"","\"DF SUPPLY\"","\"DUNLAP FAMILY RV\"","\"EBBCO, INC\"","\"ESCO TOOL COMPANY\"","\"ESHEAVES.COM\"","\"ETRAILER\"","\"EXCEL GRAPHIC SERVICES\"","\"FERGUSON ENTERPRISES, INC\"","\"FISHER STEEL\"","\"FLOW INTERNATIONAL\"","\"GEORGIA DEPARTMENT OF REVENUE\"","\"GLOBAL ENVIRONMENTAL, INC.\"","\"GLOBAL INDUSTRIAL\"","\"GRAINGER\"","\"HAYWARD BOLT & SPECIALTY\"","\"HOLSTON GASES\"","\"HOME DEPOT\"","\"HOTFOIL-EHS\"","\"JEFF HOUSTON\"","\"JMS METAL SERVICE\"","\"LED LIGHTING WHOLESALE, INC\"","\"LOOKOUT VALLEY TOOL & MACHINE\"","\"MCMASTER-CARR SUPPLY CO.\"","\"MCNICHOLS CO\"","\"METALS DEPOT\"","\"METRO BOILER TUBE CO.\"","\"MSC INDUSTRIAL SUPPLY\"","\"MUNROE INC.\"","\"NORTH SHORE STEEL\"","\"NORTHERN TOOL & EQUIPMENT\"","\"NORTHWEST FASTENER AND SUPPLY\"","\"OVERSEAS DISTRIBUTION\"","\"PALATKA BOLT & SCREW\"","\"PENN TOOL\"","\"PHILLIPS DUSTLESS BLASTING, LLC\"","\"PIPING SUPPLY CO.\"","\"POLYMER SHAPES\"","\"RYERSON\"","\"SHARROCK MACHINE & WELDING\"","\"SHOOK & FLETCHER\"","\"SISKIN STEEL & SUPPLY CO.\"","\"SOUTHERN STAR\"","\"SOUTHERN TOOL STEEL INC.\"","\"STERLING INDUSTRIAL ALLOYS\"","\"STREETER SIGNS\"","\"STREETER SIGNS & GRAPHICS\"","\"STREETER SIGNS & GRAPHICS2\"","\"SUMMIT RACING EQUIPMENT\"","\"SUTTON PLUMBING\"","\"TENNESSEE GALVANIZING INC\"","\"TENNESSEE SLING CENTER\"","\"TIOGA PIPE\"","\"TRIANGLE TECH, LLC\"","\"TRUE-LINE CORING & CUTTING\"","\"ULINE\"","\"UNITED RENTALS\"","\"VALLEY MACHINE\"","\"WALTER A WOOD\"","\"WEX, INC.\"","\"XPO LOGISTICS\""]}}

        public override RowsData Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Parse the RowData JSON
            using (var jsonDoc = JsonDocument.ParseValue(ref reader))
            {
                if (jsonDoc.RootElement.TryGetProperty("Row", out var rowProperty))
                {
                    // Return a single-row array from the receive string value
                    if (rowProperty.ValueKind == JsonValueKind.String)
                    {
                        return new RowsData
                        {
                            Rows = new string[] { rowProperty.GetString() }
                        };
                    }

                    // Return the received string array value
                    if (rowProperty.ValueKind == JsonValueKind.Array)
                    {
                        return new RowsData
                        {
                            Rows = rowProperty.EnumerateArray().Select(i => i.GetString()).ToArray()
                        };
                    }

                    // Unable to correctly parse the provided JSON
                    throw new NotSupportedException($"{typeof(RowsDataJsonConverter).Name} does not support JSON {rowProperty.ValueKind} elements!");
                }
            }

            // The JSON provided is malformed
            throw new NotSupportedException($"Unable to parse JSON:  {reader.GetString()}");
        }



        /*
        "ImportRq": {
            "Type": "ImportVendors",
            "Rows": {
                "Row": [
                "\"Name\",\"AddressName\",\"AddressContact\",\"AddressType\",\"IsDefault\",\"Address\",\"City\",\"State\",\"Zip\",\"Country\",\"Main\",\"Home\",\"Work\",\"Mobile\",\"Fax\",\"Email\",\"Pager\",\"Web\",\"Other\",\"DefaultTerms\",\"DefaultShippingTerms\",\"Status\",\"AlertNotes\",\"URL\",\"DefaultCarrier\",\"MinOrderAmount\",\"Active\",\"AccountNumber\",\"CurrencyName\",\"CurrencyRate\",\"CF-Custom1\",\"CF-Custom2\",\"CF-Custom3\",\"CF-Custom4\"",
                "\"Monroe Bike Company\",\"Williams Bike Company - 210\",\"Williams Bike Company\",\"50\",\"true\",\"Wall st.\",\"New York\",\"NY\",\"21004\",\"UNITED STATES\",\"212-321-5643\",,,,,,,,,,,,,"
                ]
            }
        }
        */

        public override void Write(Utf8JsonWriter writer, RowsData value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteStartArray("Row");
            Array.ForEach(value.Rows, r => writer.WriteStringValue(r));
            //Array.ForEach(value.Rows, r => writer.WriteStringValue(EnsureStringHasQuotes(r)));
            writer.WriteEndArray();

            writer.WriteEndObject();
        }

        //private static string EnsureStringHasQuotes(string value) => value.StartsWith('\"') && value.EndsWith('\"') ? value : $"\"{value}\"";
    }
}
