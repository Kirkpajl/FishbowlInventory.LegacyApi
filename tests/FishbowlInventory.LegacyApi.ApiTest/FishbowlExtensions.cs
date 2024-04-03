using FishbowlInventory.Enumerations;
using FishbowlInventory.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FishbowlInventory
{
    internal static class FishbowlExtensions
    {
        /// <summary>
        /// Return all active Part numbers in Fishbowl
        /// </summary>
        /// <returns></returns>
        public static async Task<string[]> GetActivePartNumbersAsync(this FishbowlInventoryApiClient client, PartType? partType = null, CancellationToken token = default)
        {
            // Get the total number of parts
            int totalRecordCount = await client.ExecuteScalarQueryAsync<int>($"SELECT COUNT(*) FROM part WHERE activeFlag = true {(partType.HasValue ? $"AND typeId = {(int)partType.Value}" : "")}", token);

            // Retrieve the part numbers in batches
            var numbers = new List<string>();

            int offset = 0;
            while (offset < totalRecordCount)
            {
                int batchSize = Math.Min(100, totalRecordCount - offset);
                var dt = await client.ExecuteQueryAsync(sqlQuery: $"SELECT num FROM part WHERE activeFlag = true {(partType.HasValue ? $"AND typeId = {(int)partType.Value}" : "")} ORDER BY num LIMIT {batchSize} OFFSET {offset}", cancellationToken: token);

                if (dt == null) break;

                string[] batchNumbers = dt.AsEnumerable().Select(r => r.Field<string>("num")).ToArray();
                numbers.AddRange(batchNumbers);
                offset += batchSize;
            }

            // Return the complete collection of numbers
            return numbers.OrderBy(s => s).ToArray();
        }



        /// <summary>
        /// Return all Purchase Order numbers in Fishbowl
        /// </summary>
        /// <returns></returns>
        public static async Task<string[]> GetPurchaseOrderNumbersAsync(this FishbowlInventoryApiClient client, CancellationToken token = default)
        {
            // Build the MySQL SELECT query
            string sqlQuery = "SELECT num FROM po ORDER BY num";

            // Execute the SELECT query
            var dt = await client.ExecuteQueryAsync(sqlQuery: sqlQuery, cancellationToken: token);
            if (dt == null) return Array.Empty<string>();

            // Extract the first column values from the datatable
            return dt.AsEnumerable().Select(r => r.Field<string>("num")).ToArray();
        }

        /// <summary>
        /// Return all open Purchase Orders in Fishbowl
        /// </summary>
        /// <returns></returns>
        public static async Task<PurchaseOrder[]> GetOpenPurchaseOrdersAsync(this FishbowlInventoryApiClient client, bool includeItems = false, CancellationToken token = default)
        {
            var bidPurchaseOrders = await client.SearchPurchaseOrdersAsync(status: PurchaseOrderStatus.BidRequest, includeItems: includeItems, cancellationToken: token);
            var issuedPurchaseOrders = await client.SearchPurchaseOrdersAsync(status: PurchaseOrderStatus.Issued, includeItems: includeItems, cancellationToken: token);
            var pickingPurchaseOrders = await client.SearchPurchaseOrdersAsync(status: PurchaseOrderStatus.Picking, includeItems: includeItems, cancellationToken: token);
            var partialPurchaseOrders = await client.SearchPurchaseOrdersAsync(status: PurchaseOrderStatus.Partial, includeItems: includeItems, cancellationToken: token);
            var pickedPurchaseOrders = await client.SearchPurchaseOrdersAsync(status: PurchaseOrderStatus.Picked, includeItems: includeItems, cancellationToken: token);

            return bidPurchaseOrders
                .Union(issuedPurchaseOrders)
                .Union(pickingPurchaseOrders)
                .Union(partialPurchaseOrders)
                .Union(pickedPurchaseOrders)
                .OrderBy(po => po.Number)
                .ToArray();
        }



        /// <summary>
        /// Return all open Work Orders in Fishbowl
        /// </summary>
        /// <returns></returns>
        public static async Task<WorkOrder[]> GetOpenWorkOrdersAsync(this FishbowlInventoryApiClient client, CancellationToken token = default)
        {
            var enteredWorkOrders = await client.SearchWorkOrdersAsync(status: WorkOrderStatus.Entered, cancellationToken: token);
            var startedWorkOrders = await client.SearchWorkOrdersAsync(status: WorkOrderStatus.Started, cancellationToken: token);

            return enteredWorkOrders
                .Union(startedWorkOrders)
                .OrderBy(wo => wo.Number)
                .ToArray();
        }
    }
}
