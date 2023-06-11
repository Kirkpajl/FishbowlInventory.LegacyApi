using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace FishbowlInventory.Api
{
    class RowsData
    {
        [JsonPropertyName("Row")]
        public string[] Rows { get; set; }

        //"Row":["\"Id\",\"CustomerId\",\"LastFulfillmentDate\",\"FulfillmentDate\",\"Description\",\"McTotalCost\",\"Note\",\"PartNumber\",\"LineItem\",\"FulfilledQuantity\",\"PickedQuantity\",\"PartQuantity\",\"Repair\",\"RevisionLevel\",\"StatusId\",\"TaxRate\",\"CostToBeDetermined\",\"TotalCost\",\"TypeId\",\"PartPrice\",\"VendorPartNumber\",\"OutsourcedPartNumber\",\"OutsourcedPartDescription\",\"CustomerName\",\"QuickBooksClassName\",\"StatusName\",\"TaxRateName\",\"TypeName\",\"UOM\"","\"675\",,\"2021-09-15 00:00:00.0\",\"2021-09-15 00:00:00.0\",\"Bolt, Tee, 3/8\"\"-16 UNC x 3-1/2\"\" LG, J429\",\"830.000000000\",\"100191\",\"BOLT-TEE-0375UNCX03500-J429\",\"1\",\"100.000000000\",\"0E-9\",\"100.000000000\",\"false\",,\"50\",\"0.0\",\"false\",\"830.000000000\",\"10\",\"8.300000000\",\"BOLT-TEE-0375UNCX03500-J429\",,,,\"None\",\"Fulfilled\",\"None\",\"Purchase\",\"ea\"","\"676\",,\"2021-09-15 00:00:00.0\",\"2021-09-15 00:00:00.0\",\"UPS Ground\",\"17.990000000\",,\"SHIPPING-UPS-GROUND\",\"2\",\"1.000000000\",\"0E-9\",\"1.000000000\",\"false\",,\"50\",\"0.0\",\"false\",\"17.990000000\",\"40\",\"17.990000000\",\"SHIPPING-UPS-GROUND\",,,,\"None\",\"Fulfilled\",\"None\",\"Shipping\",\"ea\""]
        //"Row":"\"Id\",\"CustomerId\",\"LastFulfillmentDate\",\"FulfillmentDate\",\"Description\",\"McTotalCost\",\"Note\",\"PartNumber\",\"LineItem\",\"FulfilledQuantity\",\"PickedQuantity\",\"PartQuantity\",\"Repair\",\"RevisionLevel\",\"StatusId\",\"TaxRate\",\"CostToBeDetermined\",\"TotalCost\",\"TypeId\",\"PartPrice\",\"VendorPartNumber\",\"OutsourcedPartNumber\",\"OutsourcedPartDescription\",\"CustomerName\",\"QuickBooksClassName\",\"StatusName\",\"TaxRateName\",\"TypeName\",\"UOM\""



        /// <summary>
        /// Parse the CSV lines into a DataTable
        /// </summary>
        /// <returns></returns>
        public DataTable GetTable()
        {
            // Ensure that CSV data was provided
            if (Rows == null || Rows.Length < 2) return null;

            // Initialize the DataTable
            var dt = new DataTable();

            // Parse the first Header Line into columns (removing double-quotes)
            string[] columnNames = ParseCsvLine(Rows[0]);

            dt.Columns.AddRange(columnNames.Select(n => new DataColumn(n)).ToArray());

            // Parse the remaining lines into rows (removing double-quotes)
            for (int i = 1; i < Rows.Length; i++)
            {
                string line = Rows[i];
                string[] values = ParseCsvLine(Rows[i]);

                if (values.Length != dt.Columns.Count) continue;

                dt.Rows.Add(values);
            }

            // Return the populated DataTable
            return dt;
        }

        /// <summary>
        /// Parse the CSV line(s) into a 
        /// </summary>
        /// <returns></returns>
        public string[] GetValues()
        {
            // Ensure that CSV data was provided
            if (Rows == null) return null;

            // Parse the CSV line into a set of header names (removing double-quotes)
            if (Rows.Length == 1)
            {
                return ParseCsvLine(Rows[0]);
            }
            // Multiple header rows were returned
            else
            {
                var lines = new List<string>();

                foreach (string row in Rows)
                {
                    string[] values = ParseCsvLine(row);
                    lines.Add(string.Join(",", values));
                }

                return lines.ToArray();
            }



            // Split the CSV row and remove the double-quotation characters
            //return Rows
            //    .Split(',')
            //    .Select(n => n.Replace("\"", ""))
            //    .ToArray();
        }


        private readonly Regex _csvParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
        private string[] ParseCsvLine(string line) => _csvParser.Split(line)
            .Select(s => s.TrimStart('\"').TrimEnd('\"').Replace("\"\"", "\""))  // Remove leading/trailing/escaped double-quotes
            .ToArray();
    }
}
