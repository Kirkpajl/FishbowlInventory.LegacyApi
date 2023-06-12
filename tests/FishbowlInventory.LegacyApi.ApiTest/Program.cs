using FishbowlInventory.Enumerations;
using FishbowlInventory.Exceptions;
using FishbowlInventory.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace FishbowlInventory
{
    class Program
    {
        private static ApplicationConfig Configuration { get; } = LoadConfiguration();
        private static readonly Random Rand = new();



        static async Task Main()
        {
            // Output Fishbowl configuration
            Console.WriteLine("FishbowlInventory Test Client");
            Console.WriteLine("-------------------------------------");
            Console.WriteLine($"AppName:  {Configuration.AppName}");
            Console.WriteLine($"AppDescription:  {Configuration.AppDescription}");
            Console.WriteLine($"AppId:  {Configuration.AppId}");
            Console.WriteLine($"Username:  {Configuration.Username}");
            Console.WriteLine($"Password:  {Configuration.Password}");
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("");

            // Initialize the Fishbowl Inventory Legacy Api client
            using var fishbowl = new FishbowlInventoryApiClient(Configuration.HostName, Configuration.Port, Configuration.AppName, Configuration.AppDescription, Configuration.AppId, Configuration.Username, Configuration.Password);

            // Terminate the previous Fishbowl session (if token is present), then start a new session
            bool isLoggedIn = await InitializeSession(fishbowl);

            if (isLoggedIn)
            {
                // Queries
                //await TestQueries(fishbowl);

                // Imports
                //await TestImports(fishbowl);

                // Inventory Parts
                //await TestInventoryParts(fishbowl);

                // Location Groups
                //await TestLocationGroups(fishbowl);

                // Manufacture Orders
                //await TestManufactureOrders(fishbowl);

                // Parts
                //await TestParts(fishbowl);
                //await TestActiveParts(fishbowl);

                // Purchase Orders
                //await TestPurchaseOrders(fishbowl);
                //await TestOpenPurchaseOrders(fishbowl);
                //await TestCreatePurchaseOrder(fishbowl);
                //await TestPurchaseOrderNumbers(fishbowl);

                // Units of Measure
                //await TestUnitsOfMeasure(fishbowl);

                // Users
                //await TestUsers(fishbowl);
                //await TestUserGroups(fishbowl);

                // Vendors
                //await TestVendors(fishbowl);
                //await TestActiveVendors(fishbowl);
                await TestCreateVendor(fishbowl);

                // Work Orders
                //await TestWorkOrders(fishbowl);
                //await TestOpenWorkOrders(fishbowl);

                // Terminate the Fishbowl API session
                await TerminateSession(fishbowl);

                Console.WriteLine("All tests completed.  Press any key to exit...");
            }
            else
            {
                Console.WriteLine("Unable to authenticate with Fishbowl. All tests have been cancelled.  Press any key to exit...");
            }            

            // Pause execution
            Console.ReadKey();
        }



        private static async Task<bool> InitializeSession(FishbowlInventoryApiClient fishbowl)
        {
            // Ensure that the previous Fishbowl session was closed
            if (!string.IsNullOrWhiteSpace(Configuration.SessionToken))
            {
                Console.WriteLine("Terminating the previous user session...");

                // Attempt to terminate the previous session token
                try
                {
                    await fishbowl.LogoutAsync(Configuration.SessionToken);
                }
                catch (FishbowlInventoryAuthenticationException) { }  // Swallow 401 error if the server already terminated the token
                catch (Exception ex)
                {
                    OutputException(ex, $"An {ex.GetType().Name} occurred while terminating the previous user session!");
                }

                // Clear the token value
                Configuration.SessionToken = null;
                SaveConfig();


                Console.WriteLine($"Previous user session is terminated.");
                Console.WriteLine();
            }

            // Attempt to login
            try
            {
                Console.WriteLine("Initializing user session...");

                // Login to the Fishbowl Inventory server
                var userInfo = await fishbowl.LoginAsync();

                // Retain the new session token
                Configuration.SessionToken = fishbowl.Token;
                SaveConfig();

                // User details
                Console.WriteLine($"User Name:  {userInfo.FullName}");
                Console.WriteLine($"Allowed Modules ({userInfo.AllowedModules?.Length}):");
                foreach (var module in userInfo.AllowedModules) Console.WriteLine($"  * {module}");
                Console.WriteLine($"Server Version:  {userInfo.ServerVersion}");
                Console.WriteLine();

                return true;
            }
            catch (Exception ex)
            {
                OutputException(ex, $"An {ex.GetType().Name} occurred while initializing the user session!");
                return false;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "<Pending>")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>")]
        private static async Task TestQueries(FishbowlInventoryApiClient fishbowl)
        {
            try
            {
                Console.WriteLine($"Testing ExecuteQuery api...");

                // Get a list of the Work Order numbers
                var woNumbersTable = await fishbowl.ExecuteQueryAsync(sqlQuery: "SELECT wo.num AS Number FROM wo ORDER BY wo.num");
                string[] woNumbers = woNumbersTable == null ? Array.Empty<string>() : woNumbersTable.AsEnumerable().Select(r => r.Field<string>("Number")).ToArray();

                Console.WriteLine($"Work Order Numbers ({woNumbers.Length}):");
                foreach (var woNumber in woNumbers) Console.WriteLine($"  * {woNumber}");
                Console.WriteLine("");
            }
            catch (Exception ex)
            {
                OutputException(ex, $"An {ex.GetType().Name} occurred while testing the ExecuteQuery Api!");
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "<Pending>")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>")]
        private static async Task TestImports(FishbowlInventoryApiClient fishbowl)
        {
            try
            {
                Console.WriteLine($"Testing Imports api...");

                // Get a list of all available Fishbowl Imports
                string[] importNames = await fishbowl.GetImportListAsync();

                Console.WriteLine($"Imports List ({importNames.Length}):");
                foreach (var importName in importNames) Console.WriteLine($"  * {importName}");
                Console.WriteLine("");

                // Check if any Imports were received
                if (importNames.Length == 0)
                {
                    Console.WriteLine("No Imports were returned; terminating test.");
                    return;
                }

                // Get the Import Headers for the first Import in the list
                string importHeaderName = importNames[Rand.Next(importNames.Length)];
                string[] headers = await fishbowl.GetImportHeadersAsync(importHeaderName);

                Console.WriteLine($"{importNames.First()} Import Headers ({headers.Length}):");
                foreach (var header in headers) Console.WriteLine($"  * {header}");
                Console.WriteLine("");
            }
            catch (Exception ex)
            {
                OutputException(ex, $"An {ex.GetType().Name} occurred while testing the Imports Api!");
            }
        }

        /*private static async Task TestInventoryParts(FishbowlInventoryApiClient fishbowl)
        {
            try
            {
                Console.WriteLine($"Testing Inventory Parts api...");

                var inventoryPartsResponse = await fishbowl.SearchInventoryPartsAsync(1);

                Console.WriteLine($"Inventory Parts (Page {inventoryPartsResponse.PageNumber} of {inventoryPartsResponse.TotalPages}):");
                foreach (var partInventory in inventoryPartsResponse.Results)
                {
                    Console.WriteLine($"  [{partInventory.Id}]:  {partInventory.PartNumber} - {partInventory.PartDescription}");
                }
                Console.WriteLine("");
            }
            catch (Exception ex)
            {
                OutputException(ex, $"An {ex.GetType().Name} occurred while testing the Inventory Parts Api!");
            }
        }*/

        /*private static async Task TestLocationGroups(FishbowlInventoryApiClient fishbowl)
        {
            try
            {
                Console.WriteLine($"Testing Location Groups api...");

                var locationGroupsResponse = await fishbowl.SearchLocationGroupsAsync(1);

                Console.WriteLine($"Location Groups (Page {locationGroupsResponse.PageNumber} of {locationGroupsResponse.TotalPages}):");
                foreach (var locationGroup in locationGroupsResponse.Results)
                {
                    Console.WriteLine($"  [{locationGroup.Id}]:  {locationGroup.Name}");
                }
                Console.WriteLine("");
            }
            catch (Exception ex)
            {
                OutputException(ex, $"An {ex.GetType().Name} occurred while testing the Location Groups Api!");
            }
        }*/

        /*private static async Task TestManufactureOrders(FishbowlInventoryApiClient fishbowl)
        {
            try
            {
                Console.WriteLine($"Testing Manufacture Orders api...");

                var manufactureOrdersResponse = await fishbowl.SearchManufactureOrdersAsync(1);

                Console.WriteLine($"Manufacture Orders (Page {manufactureOrdersResponse.PageNumber} of {manufactureOrdersResponse.TotalPages}):");
                foreach (var manufactureOrder in manufactureOrdersResponse.Results)
                {
                    Console.WriteLine($"  [{manufactureOrder.Id}]:  {manufactureOrder.Number} - {manufactureOrder.BillOfMaterialDescription}");
                }
                Console.WriteLine("");
            }
            catch (Exception ex)
            {
                OutputException(ex, $"An {ex.GetType().Name} occurred while testing the Manufacture Orders Api!");
            }
        }*/

        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "<Pending>")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>")]
        private static async Task TestParts(FishbowlInventoryApiClient fishbowl)
        {
            try
            {
                Console.WriteLine($"Testing Parts api...");

                var parts = await fishbowl.GetPartsAsync();

                Console.WriteLine($"Parts ({parts.Length}):");
                foreach (var part in parts)
                {
                    Console.WriteLine($"  [{part.Id}]:  {part.Number} - {part.Description}");
                }
                Console.WriteLine("");
            }
            catch (Exception ex)
            {
                OutputException(ex, $"An {ex.GetType().Name} occurred while testing the Parts Api!");
            }
        }
       
        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "<Pending>")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>")]
        private static async Task TestActiveParts(FishbowlInventoryApiClient fishbowl)
        {
            try
            {
                Console.WriteLine($"Testing Active Inventory Parts api...");

                // Retrieve all active 'Inventory' part numbers from Fishbowl
                string[] fbPartNumbers = await fishbowl.GetActivePartNumbersAsync(PartType.Inventory);
                string[] firstNumbersBatch = fbPartNumbers.Take(50).ToArray();

                // Download the new Fishbowl parts
                var parts = await fishbowl.GetPartsByNumbersAsync(firstNumbersBatch);
                
                Console.WriteLine($"Parts ({parts.Length}):");
                foreach (var part in parts)
                {
                    Console.WriteLine($"  [{part.Id}]:  {part.Number} - {part.Description}");
                }
                Console.WriteLine("");
            }
            catch (Exception ex)
            {
                OutputException(ex, $"An {ex.GetType().Name} occurred while testing the Parts Api!");
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "<Pending>")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>")]
        private static async Task TestPurchaseOrders(FishbowlInventoryApiClient fishbowl)
        {
            try
            {
                Console.WriteLine($"Testing Purchase Order api...");

                var purchaseOrders = await fishbowl.GetPurchaseOrdersAsync();

                Console.WriteLine($"Purchase Orders ({purchaseOrders.Length}):");
                foreach (var purchaseOrder in purchaseOrders)
                {
                    bool incompleteItems = purchaseOrder.Items.Any(i => string.IsNullOrWhiteSpace(i.PartNumber) || string.IsNullOrWhiteSpace(i.Description));

                    Console.WriteLine($"  [{purchaseOrder.Id}]:  {purchaseOrder.Number} - {purchaseOrder.VendorName} - {purchaseOrder.Items.Length:N0} items - Incomplete Items: {incompleteItems}");
                }
                Console.WriteLine("");
            }
            catch (Exception ex)
            {
                OutputException(ex, $"An {ex.GetType().Name} occurred while testing the Purchase Orders Api!");
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "<Pending>")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>")]
        private static async Task TestOpenPurchaseOrders(FishbowlInventoryApiClient fishbowl)
        {
            try
            {
                Console.WriteLine($"Testing Open Purchase Order api...");

                var purchaseOrders = await fishbowl.GetOpenPurchaseOrdersAsync();

                Console.WriteLine($"Open Purchase Orders ({purchaseOrders.Length}):");
                foreach (var purchaseOrder in purchaseOrders)
                {
                    bool incompleteItems = purchaseOrder.Items.Any(i => string.IsNullOrWhiteSpace(i.PartNumber) || string.IsNullOrWhiteSpace(i.Description));

                    Console.WriteLine($"  [{purchaseOrder.Id}]:  {purchaseOrder.Number} - {purchaseOrder.VendorName} - {purchaseOrder.Items.Length:N0} items - Incomplete Items: {incompleteItems}");
                }
                Console.WriteLine("");
            }
            catch (Exception ex)
            {
                OutputException(ex, $"An {ex.GetType().Name} occurred while testing the Open Purchase Orders Api!");
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "<Pending>")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>")]
        private static async Task TestCreatePurchaseOrder(FishbowlInventoryApiClient fishbowl)
        {
            try
            {
                Console.WriteLine($"Testing Create Purchase Order api...");

                var purchaseOrder = new PurchaseOrder
                {
                    Number = "BR549",
                    Status = PurchaseOrderStatus.BidRequest
                };

                var result = await fishbowl.CreatePurchaseOrderAsync(purchaseOrder);

                if (result != null)
                    Console.WriteLine($"  [{result.Id}]:  {result.Number} - {result.VendorName} - {result.Items.Length:N0} items");
                else
                    Console.WriteLine("  UNABLE TO CREATE RECORD IN FISHBOWL!");

                Console.WriteLine("");
            }
            catch (Exception ex)
            {
                OutputException(ex, $"An {ex.GetType().Name} occurred while testing the Create Purchase Order Api!");
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "<Pending>")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>")]
        private static async Task TestPurchaseOrderNumbers(FishbowlInventoryApiClient fishbowl)
        {
            try
            {
                Console.WriteLine($"Testing Purchase Order numbers Api...");

                // Retrieve all Purchase Order numbers from Fishbowl
                string[] fbPurchaseOrderNumbers = await fishbowl.GetPurchaseOrderNumbersAsync();

                // Download the new Fishbowl parts
                Console.WriteLine($"Purchase Order Numbers ({fbPurchaseOrderNumbers.Length}):");
                foreach (string fbPurchaseOrderNumber in fbPurchaseOrderNumbers)
                {
                    Console.WriteLine($"  {fbPurchaseOrderNumber}");
                }
                Console.WriteLine("");
            }
            catch (Exception ex)
            {
                OutputException(ex, $"An {ex.GetType().Name} occurred while testing the Purchase Orders Api!");
            }
        }

        /*private static async Task TestUnitsOfMeasure(FishbowlInventoryApiClient fishbowl)
        {
            try
            {
                Console.WriteLine($"Testing Units of Measure api...");

                var uomResponse = await fishbowl.SearchUnitOfMeasuresAsync(1);

                Console.WriteLine($"Units of Measure (Page {uomResponse.PageNumber} of {uomResponse.TotalPages}):");
                foreach (var uom in uomResponse.Results)
                {
                    Console.WriteLine($"  [{uom.Id}]:  {uom.Name} ({uom.Abbreviation})");
                }
                Console.WriteLine("");
            }
            catch (Exception ex)
            {
                OutputException(ex, $"An {ex.GetType().Name} occurred while testing the Units Of Measure Api!");
            }
        }*/

        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "<Pending>")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>")]
        private static async Task TestUsers(FishbowlInventoryApiClient fishbowl)
        {
            try
            {
                Console.WriteLine($"Testing Users api...");

                var users = await fishbowl.GetUsersAsync();

                Console.WriteLine($"Users ({users.Length}):");
                foreach (var user in users)
                {
                    Console.WriteLine($"  [{user.Id}]:  {user.LastName}, {user.FirstName} ({user.UserName})");
                }
                Console.WriteLine("");
            }
            catch (Exception ex)
            {
                OutputException(ex, $"An {ex.GetType().Name} occurred while testing the Users Api!");
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "<Pending>")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>")]
        private static async Task TestUserGroups(FishbowlInventoryApiClient fishbowl)
        {
            try
            {
                Console.WriteLine($"Testing User Groups api...");

                var users = await fishbowl.GetUsersInGroupAsync("Purchasing");

                Console.WriteLine($"Users ({users.Length}):");
                foreach (var user in users)
                {
                    Console.WriteLine($"  [{user.Id}]:  {user.LastName}, {user.FirstName} ({user.UserName})");
                }
                Console.WriteLine("");
            }
            catch (Exception ex)
            {
                OutputException(ex, $"An {ex.GetType().Name} occurred while testing the Users Api!");
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "<Pending>")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>")]
        private static async Task TestVendors(FishbowlInventoryApiClient fishbowl)
        {
            try
            {
                Console.WriteLine($"Testing Vendors api...");

                var vendors = await fishbowl.GetVendorsAsync();

                Console.WriteLine($"Vendors ({vendors.Length}):");
                foreach (var vendor in vendors)
                {
                    string description = string.IsNullOrWhiteSpace(vendor.AccountNumber) ? vendor.Name : $"{vendor.Name} ({vendor.AccountNumber})";

                    Console.WriteLine($"  [{vendor.Id}]:  {description}");
                }
                Console.WriteLine("");
            }
            catch (Exception ex)
            {
                OutputException(ex, $"An {ex.GetType().Name} occurred while testing the Vendors Api!");
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "<Pending>")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>")]
        private static async Task TestActiveVendors(FishbowlInventoryApiClient fishbowl)
        {
            try
            {
                Console.WriteLine($"Testing active Vendors api...");

                var vendors = await fishbowl.SearchVendorsAsync(active: true);

                Console.WriteLine($"Active Vendors ({vendors.Length}):");
                foreach (var vendor in vendors)
                {
                    Console.WriteLine($"  [{vendor.Id}]:  {(string.IsNullOrWhiteSpace(vendor.AccountNumber) ? vendor.Name : $"{vendor.Name} ({vendor.AccountNumber})")}");
                }
                Console.WriteLine("");
            }
            catch (Exception ex)
            {
                OutputException(ex, $"An {ex.GetType().Name} occurred while testing the Active Vendors Api!");
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "<Pending>")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>")]
        private static async Task TestCreateVendor(FishbowlInventoryApiClient fishbowl)
        {
            try
            {
                Console.WriteLine($"Testing Create Vendor api...");

                var vendor = new Vendor
                {
                    Name = "Hee Haw",
                    IsActive = true,
                    Addresses =
                    {
                        new Address
                        {
                            Type = AddressType.MainOffice,
                            IsDefault = true,

                            StreetAddress = "Kornfield Kounty",
                            City = "Nashville",
                            State = "TN",
                            PostalCode = "37011"
                        }
                    }
                };

                var result = await fishbowl.CreateVendorAsync(vendor);

                if (result != null)
                    Console.WriteLine($"  [{result.Id}]:  {(string.IsNullOrWhiteSpace(result.AccountNumber) ? result.Name : $"{result.Name} ({result.AccountNumber})")}");
                else
                    Console.WriteLine("  UNABLE TO CREATE RECORD IN FISHBOWL!");

                Console.WriteLine("");
            }
            catch (Exception ex)
            {
                OutputException(ex, $"An {ex.GetType().Name} occurred while testing the Create Vendor Api!");
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "<Pending>")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>")]
        private static async Task TestWorkOrders(FishbowlInventoryApiClient fishbowl)
        {
            try
            {
                Console.WriteLine($"Testing Work Order api...");

                var workOrders = await fishbowl.GetWorkOrdersAsync();

                Console.WriteLine($"Work Orders ({workOrders.Length}):");
                foreach (var workOrder in workOrders)
                {
                    Console.WriteLine($"  [{workOrder.Id}]:  {workOrder.Number} - {workOrder.Description}");
                }
                Console.WriteLine("");
            }
            catch (Exception ex)
            {
                OutputException(ex, $"An {ex.GetType().Name} occurred while testing the Work Orders Api!");
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "<Pending>")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>")]
        private static async Task TestOpenWorkOrders(FishbowlInventoryApiClient fishbowl)
        {
            try
            {
                Console.WriteLine($"Testing Open Work Order api...");

                var workOrders = await fishbowl.GetOpenWorkOrdersAsync();

                Console.WriteLine($"Open Work Orders ({workOrders.Length}):");
                foreach (var workOrder in workOrders)
                {
                    Console.WriteLine($"  [{workOrder.Id}]:  {workOrder.Number} - {workOrder.Description}");
                }
                Console.WriteLine("");
            }
            catch (Exception ex)
            {
                OutputException(ex, $"An {ex.GetType().Name} occurred while testing the Open Work Orders Api!");
            }
        }



        private static async Task TerminateSession(FishbowlInventoryApiClient fishbowl)
        {
            try
            {
                Console.WriteLine($"Terminating user session...");

                // Logout from the Fishbowl Inventory server
                await fishbowl.LogoutAsync();

                // Clear the cached SessionToken
                Configuration.SessionToken = null;
                SaveConfig();

                Console.WriteLine($"User session is terminated.");
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                OutputException(ex, $"An {ex.GetType().Name} occurred while terminating the user session!");
            }
        }



        #region Configuration Helper Methods

        private static ApplicationConfig LoadConfiguration() =>
            new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", true, true)
            .Build()
            .Get<ApplicationConfig>();

        /// <summary>
        /// Serialize the config object and overwrite appsettings.json
        /// </summary>
        /// <param name="config"></param>
        private static void SaveConfig()
        {
            // Serialize the Config object
            var jsonWriteOptions = new JsonSerializerOptions()
            {
                WriteIndented = true
            };
            jsonWriteOptions.Converters.Add(new JsonStringEnumConverter());

            var configJson = JsonSerializer.Serialize(Configuration, jsonWriteOptions);

            // Overwrite the appsettings.json file with the new JSON
            var appSettingsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");
            File.WriteAllText(appSettingsPath, configJson);
        }

        #endregion Configuration Helper Methods

        #region Exception Helper Methods

        /// <summary>
        /// Write the exception to the console with a custom format
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="message"></param>
        private static void OutputException(Exception ex, string message = null)
        {
            if (string.IsNullOrWhiteSpace(message))
                message = $"An {ex.GetType().Name} occurred while testing the Fishbowl Inventory REST Api!";

            var currentColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(message);
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
            Console.WriteLine();

            Console.ForegroundColor = currentColor;
        }

        #endregion Exception Helper Methods
    }
}
