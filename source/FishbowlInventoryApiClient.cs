using FishbowlInventory.Api;
using FishbowlInventory.Api.BillsOfMaterial;
using FishbowlInventory.Api.Endian;
using FishbowlInventory.Api.Imports;
using FishbowlInventory.Api.Inventory;
using FishbowlInventory.Api.Login;
using FishbowlInventory.Api.Memos;
using FishbowlInventory.Api.Queries;
using FishbowlInventory.Api.SaleOrders;
using FishbowlInventory.Api.WorkOrders;
using FishbowlInventory.Enumerations;
using FishbowlInventory.Exceptions;
using FishbowlInventory.Extensions;
using FishbowlInventory.Helpers;
using FishbowlInventory.Models;
using FishbowlInventory.Serialization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;

namespace FishbowlInventory
{
    public sealed class FishbowlInventoryApiClient : IDisposable
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the FishbowlInventory.FishbowlInventoryApiClient class.
        /// </summary>
        /// <param name="hostname">The DNS name of the remote host to which you intend to connect (i.e., "localhost").</param>
        /// <param name="port">The port number of the remote host to which you intend to connect (i.e., 28192).</param>
        public FishbowlInventoryApiClient(string hostname, int port, string applicationName = default, string applicationDescription = default, int applicationId = default, 
            string userName = default, string userPassword = default)
        {
            // Correct any DNS Hostname formatting issues
            hostname = hostname.RemoveFromEnd("/");

            // Retain the connection details
            _hostname = hostname;
            _port = port;

            // Initialize the global JSON serialization options
            _jsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web)
            {
                IgnoreReadOnlyProperties = true,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault,  //WhenWritingNull
                Converters =
                {
                    new RowsDataJsonConverter()
                }
            };

            // Retain any Fishbowl configuration values
            _applicationName = applicationName;
            _applicationDescription = applicationDescription;
            _applicationId = applicationId;
            _userName = userName;
            _userPassword = userPassword;
        }

        ~FishbowlInventoryApiClient()
        {
            Dispose(false);
        }

        #endregion Constructors

        #region Private Fields

        private readonly string _hostname;
        private readonly int _port;
        private readonly JsonSerializerOptions _jsonOptions;
        private bool _disposed;

        private string _token, _applicationName, _applicationDescription, _userName, _userPassword;
        private int _applicationId;
        private int? _userId;
        private readonly object _syncLock = new object();

        #endregion Private Fields

        #region Public Properties

        /// <summary>
        /// Authorization Token for the current session.
        /// </summary>
        public string Token => _token;

        /// <summary>
        /// The unique identifier for the authenticated user in the current session.
        /// </summary>
        public int? UserId => _userId;

        /// <summary>
        /// Returns TRUE if an authorization token has been received from the Fishbowl server, otherwise FALSE.
        /// </summary>
        public bool IsLoggedIn => !string.IsNullOrWhiteSpace(_token);



        /// <summary>
        /// Fishbowl User Account Name
        /// </summary>
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        /// <summary>
        /// Fishbowl User Account Password
        /// </summary>
        public string UserPassword
        {
            get { return _userPassword; }
            set { _userPassword = value; }
        }



        /// <summary>
        /// Application ID; can be any number, but must be unique in Fishbowl
        /// </summary>
        public int ApplicationId
        {
            get { return _applicationId; }
            set { _applicationId = value; }
        }

        /// <summary>
        /// Application Name, must be unique in Fishbowl
        /// </summary>
        public string ApplicationName
        {
            get { return _applicationName; }
            set { _applicationName = value; }
        }

        /// <summary>
        /// Application Description, must be unique in Fishbowl
        /// </summary>
        public string ApplicationDescription
        {
            get { return _applicationDescription; }
            set { _applicationDescription = value; }
        }

        #endregion Public Properties

        
        
        #region User Session

        public Task<UserInformation> LoginAsync(string appName, string appDescription, int appId, string username, string password, CancellationToken cancellationToken = default)
        {
            // Update the Fishbowl connection properties
            _applicationName = appName;
            _applicationDescription = appDescription;
            _applicationId = appId;
            _userName = username;
            _userPassword = password;

            // Execute the login request
            return LoginAsync(cancellationToken);
        }

        public async Task<UserInformation> LoginAsync(CancellationToken cancellationToken = default)
        {
            var request = new LoginRequest() 
            { 
                AppName = _applicationName, 
                AppDescription = _applicationDescription, 
                AppId = _applicationId, 
                Username = _userName, 
                Password = _userPassword 
            };

            var response = await SendAsync<LoginRequest, LoginResponse>(request, cancellationToken);

            return new UserInformation
            {
                FullName = response?.FullName,
                AllowedModules = response?.ModuleAccess?.AllowedModules,
                ServerVersion = response?.ServerVersion
            };
        }



        public Task LogoutAsync(string token, CancellationToken cancellationToken = default)
        {
            // Overwrite the cached token with the supplied value
            if (!string.IsNullOrWhiteSpace(token))
            {
                lock (_syncLock)
                {
                    _token = token;
                }
            }

            // Terminate the specified session
            return LogoutAsync(cancellationToken);
        }

        public async Task LogoutAsync(CancellationToken cancellationToken = default)
        {
            // Don't proceed if the session has already ended
            if (!IsLoggedIn) return;

            // End the current Fishbowl session
            await SendAsync<LogoutRequest, LogoutResponse>(new LogoutRequest(), cancellationToken);

            // Ensure that the cached token is cleared
            lock (_syncLock)
            {
                _token = null;
            }
        }

        #endregion User Session

        #region Bills of Material

        /// <summary>
        /// Adjust the inventory in a tag.
        /// </summary>
        /// <param name="number"></param>
        /// <param name="quantity"></param>
        /// <param name="dateScheduled"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task BuildBillOfMaterialAsync(string number, int quantity, DateTime dateScheduled, CancellationToken cancellationToken = default)
            => SendAsync<BuildBillOfMaterialRequest, BuildBillOfMaterialResponse>(
                new BuildBillOfMaterialRequest
                {
                    Number = number,
                    Quantity = quantity,
                    DateScheduled = dateScheduled
                },
                cancellationToken);

        #endregion Bills of Material

        #region Imports/Exports

        /// <summary>
        /// Returns a list of your import options.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<string[]> GetImportListAsync(CancellationToken cancellationToken = default)
        {
            // Send the ImportHeader request
            var response = await SendAsync<ImportListRequest, ImportListResponse>(
                new ImportListRequest(),
                cancellationToken);

            // Return the list of Import Names
            return response.Data.Names;
        }

        /// <summary>
        /// Get the headers of an import.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<string[]> GetImportHeadersAsync(string type, CancellationToken cancellationToken = default)
        {
            // Send the ImportHeader request
            var response = await SendAsync<ImportHeaderRequest,ImportHeaderResponse>(
                new ImportHeaderRequest { Type = type }, 
                cancellationToken);

            // Return the Import Names
            return response.Header.GetValues();
        }

        /// <summary>
        /// This request allows you to import CSV data.  Data columns can be blank, but each data column must be represented in the request. 
        /// </summary>
        /// <remarks>
        /// It is best practice to always include the header rows when importing data.
        /// </remarks>
        /// <param name="type"></param>
        /// <param name="data"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task ImportAsync(string type, string[,] data, CancellationToken cancellationToken = default)
        {
            // Serialize the data into CSV
            var csvLines = new List<string>();
            for (int row = 0; row < data.GetLength(0); row++)
            {
                var cells = new List<string>();
                for (int col = 0; col < data.GetLength(1); col++)
                {
                    cells.Add($"\"{data[row, col]}\"");
                }
                csvLines.Add(string.Join(',', cells));                
            }

            // Import the CSV data
            return ImportAsync(type, csvLines.ToArray(), cancellationToken);
        }

        /// <summary>
        /// This request allows you to import CSV data.  Data columns can be blank, but each data column must be represented in the request. 
        /// </summary>
        /// <remarks>
        /// It is best practice to always include the header rows when importing data.
        /// </remarks>
        /// <param name="type"></param>
        /// <param name="lines"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task ImportAsync(string type, string[] csvLines, CancellationToken cancellationToken = default) 
            => SendAsync<ImportRequest, ImportResponse>(
                new ImportRequest
                {
                    Type = type,
                    Data = new RowsData
                    {
                        Rows = csvLines
                    }
                },
                cancellationToken);

        #endregion Imports/Exports

        #region Inventory

        /// <summary>
        /// Adds initial inventory of a part.
        /// </summary>
        /// <param name="partNumber"></param>
        /// <param name="quantity"></param>
        /// <param name="unitOfMeasureId"></param>
        /// <param name="cost"></param>
        /// <param name="note"></param>
        /// <param name="tracking"></param>
        /// <param name="locationTagNumber"></param>
        /// <param name="tagNumber"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task AddInventoryAsync(string partNumber, int quantity, int unitOfMeasureId, double cost, string note, Tracking tracking,
            int locationTagNumber, int tagNumber, CancellationToken cancellationToken = default) 
            => SendAsync<AddInventoryRequest, AddInventoryResponse>(new AddInventoryRequest
            {
                PartNumber = partNumber,
                Quantity = quantity,
                UnitOfMeasureId = unitOfMeasureId,
                Cost = cost,
                Note = note,
                Tracking = tracking,
                LocationTagNumber = locationTagNumber,
                TagNumber = tagNumber
            }, cancellationToken);

        #endregion Inventory

        #region Memos

        /// <summary>
        /// Adds a memo to the specified object.
        /// </summary>
        /// <param name="itemType"></param>
        /// <param name="partNumber"></param>
        /// <param name="productNumber"></param>
        /// <param name="orderNumber"></param>
        /// <param name="customerNumber"></param>
        /// <param name="vendorNumber"></param>
        /// <param name="memo"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task AddMemoAsync(FishbowlItemType itemType, string partNumber, string productNumber, string orderNumber, string customerNumber, 
            string vendorNumber, Memo memo, CancellationToken cancellationToken = default)
            => SendAsync<AddMemoRequest, AddMemoResponse>(new AddMemoRequest
            {
                ItemType = itemType,
                PartNumber = partNumber,
                ProductNumber = productNumber,
                OrderNumber = orderNumber,
                CustomerNumber = customerNumber,
                VendorNumber = vendorNumber,
                Memo = memo
            }, cancellationToken);

        #endregion Memos

        #region Parts

        private const string PART_IMPORT_NAME = "ImportPart";
        private const string PART_AVG_COST_IMPORT_NAME = "ImportPartCost";
        private const string PART_STD_COST_IMPORT_NAME = "ImportPartStdCost";

        private const string PART_SELECT_QUERY =
            @"SELECT
                p.id AS Id,
                p.num AS PartNumber, 
                p.description AS PartDescription, 
                p.details AS PartDetails, 
                u.code AS UOM, 
                p.upc AS UPC, 
                t.name AS PartType,
                p.activeFlag AS Active, 
                p.abcCode AS ABCCode, 
                p.weight AS Weight, 
                uw.code AS WeightUOM, 
                p.width AS Width, 
                p.height AS Height, 
                p.len AS Length,
                us.code AS SizeUOM, 
                p.alertNote AS AlertNote, 
                p.stdCost AS StandardCost, 
                ch.avgCost AS AverageCost
                
            FROM part p

            INNER JOIN parttype AS t ON t.id = p.typeId
            LEFT JOIN partcosthistory AS ch ON ch.partId = p.id
            INNER JOIN uom AS u ON u.id = p.uomId
            INNER JOIN uom AS uw ON uw.id = p.weightUomId
            INNER JOIN uom AS us ON us.id = p.sizeUomId";



        /// <summary>
        /// Get all Parts
        /// </summary>
        public async Task<Part[]> GetPartsAsync(PartType? partType = null, CancellationToken cancellationToken = default)
        {
            // Build the MySQL SELECT query
            string sqlQuery = $"{PART_SELECT_QUERY} {(partType.HasValue ? $"WHERE p.typeId = '{(int)partType.Value}'" : "")} ORDER BY p.num";

            // Execute the SELECT query
            var partsTable = await ExecuteQueryAsync(sqlQuery: sqlQuery, cancellationToken: cancellationToken);

            // Parse the rows into Part objects
            return ToParts(partsTable);
        }

        /// <summary>
        /// Get all parts that match the search criteria
        /// </summary>
        /// <param name="number">The part number.</param>
        /// <param name="vendorName">The part description.</param>
        /// <param name="upc">The UPC code for the part.</param>
        /// <param name="type">The basic type of the part.</param>
        /// <param name="abc">The ABC code for the part.</param>
        /// <param name="details">The part details.</param>
        /// <param name="hasBom">
        /// Indicates if the part has an associated bill of materials. True returns parts with an 
        /// associated bill of materials, false does not filter the results.
        /// </param>
        /// <param name="active">The active status of the UOM.</param>
        /// <param name="productNumber">The associated product number.</param>
        /// <param name="productDescription">The product description.</param>
        /// <param name="vendorPartNumber">The vendor part number.</param>
        /// <param name="vendorName">The name of the associated vendor.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Part[]> SearchPartsAsync(string number = null, string vendorName = null, string upc = null, PartType? type = null, string abc = null,
            string details = null, bool? active = null, CancellationToken cancellationToken = default)
        {
            // Assemble the WHERE statement
            var queryParameters = new Dictionary<string, string>();
            queryParameters.AddParameterIfNotNull("p.num", number);
            queryParameters.AddParameterIfNotNull("p.description", vendorName);
            queryParameters.AddParameterIfNotNull("p.upc", upc);
            queryParameters.AddParameterIfNotNull("p.typeId", (int)type);
            queryParameters.AddParameterIfNotNull("p.abcCode", abc);
            queryParameters.AddParameterIfNotNull("p.details", details);
            queryParameters.AddParameterIfNotNull("p.activeFlag", active);

            string whereConditions = string.Join(" AND ", queryParameters.Select(qp => $"{qp.Key}='{qp.Value}'"));

            // Don't proceed if no search parameters were provided
            if (queryParameters.Count == 0) return await GetPartsAsync();

            // Build the MySQL SELECT query
            string sqlQuery = $"{PART_SELECT_QUERY} WHERE {whereConditions} ORDER BY p.num";

            // Execute the SELECT query
            var partsTable = await ExecuteQueryAsync(sqlQuery: sqlQuery, cancellationToken: cancellationToken);

            // Parse the rows into Part objects
            return ToParts(partsTable);
        }

        /// <summary>
        /// Retrieves the details of an existing part. You only need to provide the unique part ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Part> GetPartAsync(int id, CancellationToken cancellationToken = default)
        {
            // Build the MySQL SELECT query
            string sqlQuery = $"{PART_SELECT_QUERY} WHERE p.id = '{id}'";

            // Execute the SELECT query
            var partsTable = await ExecuteQueryAsync(sqlQuery: sqlQuery, cancellationToken: cancellationToken);

            // Parse the rows into Part objects
            return ToParts(partsTable).FirstOrDefault();
        }

        /// <summary>
        /// Retrieves the details of an existing part. You only need to provide the unique part number.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public async Task<Part> GetPartByNumberAsync(string number, CancellationToken cancellationToken = default)
        {
            // Build the MySQL SELECT query
            string sqlQuery = $"{PART_SELECT_QUERY} WHERE p.num = '{number}'";

            // Execute the SELECT query
            var partsTable = await ExecuteQueryAsync(sqlQuery: sqlQuery, cancellationToken: cancellationToken);

            // Parse the rows into Part objects
            return ToParts(partsTable).FirstOrDefault();
        }

        /// <summary>
        /// Retrieves the details of any existing parts possessing one of the provided the unique part numbers.
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        public async Task<Part[]> GetPartsByNumbersAsync(string[] numbers, CancellationToken cancellationToken = default)
        {
            // Ensure that at least 1 number was received
            if (numbers == null || numbers.Length == 0)
                return await GetPartsAsync();

            // Escape any apostrophes in the provided part numbers
            for (int i=0; i<numbers.Length; i++)
            {
                numbers[i] = numbers[i].Replace("'", "\\'");
            }

            // Build the MySQL SELECT query
            string numberSet = $"({string.Join(',', numbers.Select(n => $"'{n}'"))})";
            string sqlQuery = $"{PART_SELECT_QUERY} WHERE p.num IN {numberSet}";
            
            // Execute the SELECT query
            var partsTable = await ExecuteQueryAsync(sqlQuery: sqlQuery, cancellationToken: cancellationToken);

            // Parse the rows into Part objects
            return ToParts(partsTable);
        }

        /// <summary>
        /// Create a Part
        /// </summary>
        /// <param name="part"></param>
        /// <returns></returns>
        public async Task<Part> CreatePartAsync(Part part, CancellationToken cancellationToken = default)
        {
            // Attempt to import the CSV rows into Fishbowl
            await ImportAsync(PART_IMPORT_NAME, ToCsv(part), cancellationToken);
            await ImportAsync(PART_STD_COST_IMPORT_NAME, ToStdCostCsv(part), cancellationToken);            
            if (part.AverageCost > 0) await ImportAsync(PART_AVG_COST_IMPORT_NAME, ToAvgCostCsv(part), cancellationToken);

            // Return the new PO record
            return await GetPartByNumberAsync(part.Number, cancellationToken);
        }

        /// <summary>
        /// Updates a Part. Optional parameters that are not passed in will be reset to their default values. Best practice is to send the complete object you would like to save.
        /// </summary>
        /// <param name="part"></param>
        /// <returns></returns>
        public Task<Part> UpdatePartAsync(Part part, CancellationToken cancellationToken = default) => CreatePartAsync(part, cancellationToken);



        /// <summary>
        /// Deserialize CSV data to Part DTOs
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        private Part[] ToParts(DataTable table)
        {
            // Don't proceed if no table was provided
            if (table == null) return Array.Empty<Part>();

            // Initialize the collection
            var items = new List<Part>();

            // Iterate through the data rows
            foreach (DataRow row in table.Rows)
            {
                // Parse the data row
                int.TryParse(row.Field<string>("Id"), out int id);
                decimal.TryParse(row.Field<string>("StandardCost"), out decimal standardCost);
                decimal.TryParse(row.Field<string>("AverageCost"), out decimal averageCost);
                Enum.TryParse(row.Field<string>("UOM"), out UnitOfMeasureType uomType);
                bool.TryParse(row.Field<string>("Active"), out bool isActive);

                var item = new Part
                {
                    Id = id,
                    Number = row.Field<string>("PartNumber"),
                    Description = row.Field<string>("PartDescription"),
                    Details = row.Field<string>("PartDetails"),
                    StandardCost = standardCost,
                    AverageCost = averageCost,
                    UnitOfMeasure = new UnitOfMeasure
                    {
                        Type = uomType
                    },
                    IsActive = isActive
                };

                // Add the item to the collection
                items.Add(item);
            }

            // Return the completed collection
            return items.ToArray();
        }

        /// <summary>
        /// Serialize the Part DTO to CSV data.
        /// </summary>
        /// <param name="part"></param>
        /// <returns></returns>
        private static string[] ToCsv(Part part)
        {
            // Initialize CSV line collection
            var lines = new List<string>
            {
                "\"PartNumber\",\"PartDescription\",\"PartDetails\",\"UOM\",\"UPC\",\"PartType\",\"Active\",\"ABCCode\",\"Weight\",\"WeightUOM\",\"Width\",\"Height\",\"Length\",\"SizeUOM\",\"PrimaryTracking\",\"AlertNote\",\"PictureUrl\""
            };
            foreach (var track in part.Tracks) lines.Add($"\"Tracks-{track.Name}\"");  // "Tracks-Lot Number", \"Tracks-Revision Level\",\"Tracks-Expiration Date\",\"Tracks-Serial Number\",\"Tracks-Heat Number\",
            foreach (var field in part.CustomFields) lines.Add($"\"CF-{field.Name}\"");  // \"CF-Heat #\""

            // Append the CSV line for the Part
            var csv = new CsvBuilder();

            csv.Add(part.Number);  // "PartNumber"
            csv.Add(part.Description);  // "PartDescription"
            csv.Add(part.Details);  // "PartDetails"
            csv.Add(part.UnitOfMeasure?.Abbreviation);  // "UOM"
            csv.Add(part.UPC);  // "UPC"
            csv.Add(part.Type.GetDisplayName());  // "PartType"
            csv.Add(part.IsActive.ToString());  // "Active"
            csv.Add(part.ABCCode);  // "ABCCode"
            csv.Add(part.Weight.ToString());  // "Weight"
            csv.Add(part.WeightUnitOfMeasure?.Abbreviation);  // "WeightUOM"
            csv.Add(part.Width.ToString());  // "Width"
            csv.Add(part.Height.ToString());  // "Height"
            csv.Add(part.SizeUnitOfMeasure?.Abbreviation);  // "SizeUOM"
            csv.Add(part.ConsumptionRate.ToString("F"));  // "ConsumptionRate"
            csv.Add(part.AlertNote);  // "AlertNote"
            csv.Add(part.ImageUrl);  // "PictureUrl"
            csv.Add(part.Revision);  // "Revision"
            csv.Add(part.PurchaseOrderItemType.GetDisplayName());  // "POItemType"
            csv.Add(part.DefaultOutsourcedReturnItem);  // "DefaultOutsourcedReturnItem"
            csv.Add(part.PrimaryTracking?.Name);  // "PrimaryTracking"
            foreach (var track in part.Tracks) lines.Add($"\"{track.IsActive}\"");
            foreach (var field in part.CustomFields) lines.Add($"\"{field.Value}\"");

            lines.Add(csv.ToString());

            // Return the CSV lines
            return lines.ToArray();
        }

        /// <summary>
        /// Serialize the Part DTO to CSV data.
        /// </summary>
        /// <param name="part"></param>
        /// <returns></returns>
        private static string[] ToStdCostCsv(Part part)
        {
            // Initialize CSV line collection
            var lines = new List<string>
            {
                "\"PartNumber\",\"PartDescription\",\"StandardCost\""
            };

            // Append the CSV line for the Part
            var csv = new CsvBuilder();

            csv.Add(part.Number);  // "PartNumber"
            csv.Add(part.Description);  // "PartDescription"
            csv.Add(part.StandardCost.ToString("F"));  // "StandardCost"

            lines.Add(csv.ToString());

            // Return the CSV lines
            return lines.ToArray();
        }

        /// <summary>
        /// Serialize the Part DTO to CSV data.
        /// </summary>
        /// <param name="part"></param>
        /// <returns></returns>
        private static string[] ToAvgCostCsv(Part part)
        {
            // Initialize CSV line collection
            var lines = new List<string>
            {
                "\"PartNumber\",\"PartDescription\",\"AveragePartCost\",\"LastPartCost\""
            };

            // Append the CSV line for the Part
            var csv = new CsvBuilder();

            csv.Add(part.Number);  // "PartNumber"
            csv.Add(part.Description);  // "PartDescription"
            csv.Add(part.Details);  // "PartDetails"

            lines.Add(csv.ToString());

            // Return the CSV lines
            return lines.ToArray();
        }

        #endregion Parts

        #region Purchase Orders

        private const string PURCHASEORDER_IMPORT_NAME = "ImportPurchaseOrder";
        private const string PURCHASEORDER_SELECT_QUERY =
            @"SELECT
            po.id AS Id,
            po.buyer AS BuyerUserName,
            po.buyerId AS BuyerId,
            po.currencyId AS CurrencyId,
            po.currencyRate AS CurrencyRate,
            po.customerSO AS CustomerSONum,
            po.dateCompleted AS CompletedDate,
            po.dateConfirmed AS ConfirmedDate,
            po.dateCreated AS CreatedDate,
            po.dateFirstShip AS FulfillmentDate,
            po.dateIssued AS IssuedDate,
            po.dateLastModified AS LastModifiedDate,
            po.dateRevision AS RevisionDate,
            po.deliverTo AS DeliverToName,
            po.email AS Email,
            po.fobPointId AS FobPointId,
            po.locationGroupId AS LocationGroupId,
            po.note AS Note,
            po.num AS PONum,
            po.paymentTermsId AS PaymentTermsId,
            po.phone AS Phone,
            po.remitAddress AS RemitToStreetAddress,
            po.remitCity AS RemitToCity,
            po.remitToName AS RemitToName,
            po.remitZip AS RemitToZip,
            po.revisionNum AS RevisionNumber,
            po.shipTermsId AS ShippingTermsId,
            po.shipToAddress AS ShipToStreetAddress,
            po.shipToCity AS ShipToCity,
            po.shipToName AS ShipToName,
            po.shipToZip AS ShipToZip,
            po.statusId AS Status,
            po.taxRateName AS TaxRateName,
            po.totalIncludesTax TotalIncludesTax,
            po.totalTax AS TotalTax,
            po.typeId AS TypeId,
            po.url AS URL,
            po.username AS Username,
            po.vendorContact AS VendorContact,
            po.vendorId AS VendorId,
            po.vendorSO AS VendorSONum,

            buyer.firstName AS BuyerFirstName,
            buyer.lastName AS BuyerLastName,
            carrier.name AS CarrierName,
            carsrv.name AS CarrierService,
            curr.name AS CurrencyName,
            fp.name AS FobPointName,
            loc.name AS LocationGroupName,
            pterm.name AS PaymentTermsName,
            qb.name AS QuickBooksClassName,
            rctry.name AS RemitToCountry,
            rstate.name AS RemitToState,
            sctry.name AS ShipToCountry,
            sstate.name AS ShipToState,
            stat.name AS StatusName,
            sterm.name AS ShippingTermsName,
            type.name AS TypeName,
            vendor.name AS VendorName
                
            FROM po

            INNER JOIN sysuser AS buyer ON buyer.id = po.buyerId
            INNER JOIN carrier ON carrier.id = po.carrierId
            LEFT JOIN carrierservice AS carsrv ON carsrv.id = po.carrierServiceId
            LEFT JOIN currency AS curr ON curr.id = po.currencyId
            INNER JOIN fobpoint AS fp ON fp.id = po.fobPointId
            INNER JOIN locationgroup AS loc ON loc.id = po.locationGroupId
            INNER JOIN paymentterms AS pterm ON pterm.id = po.paymentTermsId
            LEFT JOIN qbclass AS qb ON qb.id = po.qbClassId
            LEFT JOIN countryconst AS rctry ON rctry.id = po.remitCountryId
            LEFT JOIN stateconst AS rstate ON rstate.id = po.remitStateId
            LEFT JOIN countryconst AS sctry ON sctry.id = po.shipToCountryId
            LEFT JOIN stateconst AS sstate ON sstate.id = po.shipToStateId
            INNER JOIN postatus AS stat ON stat.id = po.statusId
            INNER JOIN shipterms AS sterm ON sterm.id = po.shipTermsId
            INNER JOIN potype AS type ON type.id = po.typeId
            INNER JOIN vendor ON vendor.id = po.vendorId";
        


        /// <summary>
        /// Return all Purchase Orders in Fishbowl
        /// </summary>
        /// <returns></returns>
        public async Task<PurchaseOrder[]> GetPurchaseOrdersAsync(bool includeItems = false, CancellationToken cancellationToken = default)
        {
            // Build the MySQL SELECT query
            string sqlQuery = $"{PURCHASEORDER_SELECT_QUERY} ORDER BY po.num";

            // Execute the SELECT query and cast to object collection
            var purchaseOrdersTable = await ExecuteQueryAsync(sqlQuery: sqlQuery, cancellationToken: cancellationToken);

            // Parse the rows into PurchaseOrder/PurchaseOrderItem objects
            return ToPurchaseOrders(purchaseOrdersTable, includeItems);
        }

        /// <summary>
        /// Get all parts that match the search criteria
        /// </summary>
        /// <param name="number">The part number.</param>
        /// <param name="vendorName">The name of the associated vendor.</param>
        /// <param name="status">The status of the PO.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<PurchaseOrder[]> SearchPurchaseOrdersAsync(string number = null, string vendorName = null, PurchaseOrderStatus? status = null, bool includeItems = false, CancellationToken cancellationToken = default)
        {
            // Assemble the WHERE statement
            var queryParameters = new Dictionary<string, string>();
            queryParameters.AddParameterIfNotNull("po.num", number);
            queryParameters.AddParameterIfNotNull("vendor.name", vendorName);
            queryParameters.AddParameterIfNotNull("po.statusId", (int?)status);

            string whereConditions = string.Join(" AND ", queryParameters.Select(qp => $"{qp.Key}='{qp.Value}'"));

            // Don't proceed if no search parameters were provided
            if (queryParameters.Count == 0) return await GetPurchaseOrdersAsync();

            // Build the MySQL SELECT query
            string sqlQuery = $"{PURCHASEORDER_SELECT_QUERY} WHERE {whereConditions} ORDER BY po.num";

            // Execute the SELECT query
            var purchaseOrdersTable = await ExecuteQueryAsync(sqlQuery: sqlQuery, cancellationToken: cancellationToken);

            // Parse the rows into Purchase Order objects
            return ToPurchaseOrders(purchaseOrdersTable, includeItems);
        }

        /// <summary>
        /// Retrieves the details of an existing purchase order. You only need to provide the unique purchase order ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<PurchaseOrder> GetPurchaseOrderAsync(int id, CancellationToken cancellationToken = default)
        {
            // Build the MySQL SELECT query
            string sqlQuery = $"{PURCHASEORDER_SELECT_QUERY} WHERE po.id = '{id}'";

            // Execute the SELECT query
            var purchaseOrdersTable = await ExecuteQueryAsync(sqlQuery: sqlQuery, cancellationToken: cancellationToken);

            // Parse the rows into PurchaseOrder/PurchaseOrderItem objects
            return ToPurchaseOrders(purchaseOrdersTable, true).FirstOrDefault();
        }

        /// <summary>
        /// Retrieves the details of an existing purchase order. You only need to provide the unique purchase order number.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public async Task<PurchaseOrder> GetPurchaseOrderByNumberAsync(string number, CancellationToken cancellationToken = default)
        {
            // Build the MySQL SELECT query
            string sqlQuery = $"{PURCHASEORDER_SELECT_QUERY} WHERE po.num = '{number}'";

            // Execute the SELECT query
            var purchaseOrdersTable = await ExecuteQueryAsync(sqlQuery: sqlQuery, cancellationToken: cancellationToken);

            // Parse the rows into PurchaseOrder/PurchaseOrderItem objects
            return ToPurchaseOrders(purchaseOrdersTable, true).FirstOrDefault();
        }

        /// <summary>
        /// Return all active Purchase Order Items in Fishbowl belonging to the specified Purchase Order
        /// </summary>
        /// <param name="poNumber">The unique number of the Purchase Order</param>
        /// <returns></returns>
        private Task<PurchaseOrderItem[]> GetPurchaseOrderItemsAsync(string poNumber, CancellationToken cancellationToken = default)
        {
            // Build the MySQL SELECT query
            string sqlQuery = $@"
                SELECT
                poitem.id AS Id,
                poitem.customerId AS CustomerId,
                poitem.dateLastFulfillment AS LastFulfillmentDate,
                poitem.dateScheduledFulfillment AS FulfillmentDate,
                poitem.description AS Description,
                poitem.mcTotalCost AS McTotalCost,
                poitem.note AS Note,
                poitem.partNum AS PartNumber,
                poitem.poLineItem AS LineItem,
                poitem.qtyFulfilled AS FulfilledQuantity,
                poitem.qtyPicked AS PickedQuantity,
                poitem.qtyToFulfill AS PartQuantity,
                poitem.repairFlag AS Repair,
                poitem.revLevel AS RevisionLevel,
                poitem.statusId AS StatusId,
                poitem.taxRate AS TaxRate,
                poitem.tbdCostFlag AS CostToBeDetermined,
                poitem.totalCost AS TotalCost,
                poitem.typeId AS TypeId,
                poitem.unitCost AS PartPrice,
                poitem.vendorPartNum AS VendorPartNumber,
                poitem.outsourcedPartNumber AS OutsourcedPartNumber,
                poitem.outsourcedPartDescription AS OutsourcedPartDescription,

                customer.name AS CustomerName,
                qb.name AS QuickBooksClassName,
                status.name AS StatusName,
                taxrate.name AS TaxRateName,
                type.name AS TypeName,
                uom.code AS UOM,
                part.description AS PartDescription

                FROM poitem

                LEFT JOIN customer ON customer.id = poitem.customerId
                LEFT JOIN part ON part.id = poitem.partId
                LEFT JOIN part AS outsourcedPart ON outsourcedPart.id = poitem.outsourcedPartId
                INNER JOIN po ON po.id = poitem.poId
                INNER JOIN poitemtype AS type ON type.id = poitem.typeId
                INNER JOIN poitemstatus AS status ON status.id = poitem.statusId
                LEFT JOIN qbclass AS qb ON qb.id = poitem.qbClassId
                INNER JOIN taxrate ON taxrate.id = poitem.taxId
                LEFT JOIN uom ON uom.id = poitem.uomId

                WHERE po.num = '{poNumber}'";

            // Execute the SELECT query
            return ExecuteQueryAsync<PurchaseOrderItem>(sqlQuery: sqlQuery, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Create a purchase order
        /// </summary>
        /// <param name="purchaseOrder"></param>
        /// <returns></returns>
        public async Task<PurchaseOrder> CreatePurchaseOrderAsync(PurchaseOrder purchaseOrder, CancellationToken cancellationToken = default)
        {
            // Ensure that the required fields have values
            if (purchaseOrder.Status == 0) purchaseOrder.Status = PurchaseOrderStatus.Historical;
            
            if (string.IsNullOrWhiteSpace(purchaseOrder.VendorName)) purchaseOrder.VendorName = "[UNIDENTIFIED VENDOR]";
            
            if (string.IsNullOrWhiteSpace(purchaseOrder.RemitToAddress.Name)) purchaseOrder.RemitToAddress.Name = "[REMIT TO NAME]";
            if (string.IsNullOrWhiteSpace(purchaseOrder.RemitToAddress.StreetAddress)) purchaseOrder.RemitToAddress.StreetAddress = "[REMIT TO ADDRESS]";
            if (string.IsNullOrWhiteSpace(purchaseOrder.RemitToAddress.City)) purchaseOrder.RemitToAddress.City = "[REMIT TO CITY]";
            if (string.IsNullOrWhiteSpace(purchaseOrder.RemitToAddress.State)) purchaseOrder.RemitToAddress.State = "[REMIT TO STATE]";
            if (string.IsNullOrWhiteSpace(purchaseOrder.RemitToAddress.PostalCode)) purchaseOrder.RemitToAddress.PostalCode = "[REMIT TO ZIP]";
            if (string.IsNullOrWhiteSpace(purchaseOrder.RemitToAddress.Country)) purchaseOrder.RemitToAddress.Country = "USA";

            if (string.IsNullOrWhiteSpace(purchaseOrder.ShipToAddress.Name)) purchaseOrder.ShipToAddress.Name = "[SHIP TO NAME]";
            if (string.IsNullOrWhiteSpace(purchaseOrder.DeliverTo)) purchaseOrder.DeliverTo = "[DELIVER TO NAME]";
            if (string.IsNullOrWhiteSpace(purchaseOrder.ShipToAddress.StreetAddress)) purchaseOrder.ShipToAddress.StreetAddress = "[SHIP TO ADDRESS]";
            if (string.IsNullOrWhiteSpace(purchaseOrder.ShipToAddress.City)) purchaseOrder.ShipToAddress.City = "[SHIP TO CITY]";
            if (string.IsNullOrWhiteSpace(purchaseOrder.ShipToAddress.State)) purchaseOrder.ShipToAddress.State = "[SHIP TO STATE]";
            if (string.IsNullOrWhiteSpace(purchaseOrder.ShipToAddress.PostalCode)) purchaseOrder.ShipToAddress.PostalCode = "[SHIP TO ZIP]";
            if (string.IsNullOrWhiteSpace(purchaseOrder.ShipToAddress.Country)) purchaseOrder.ShipToAddress.Country = "USA";

            if (string.IsNullOrWhiteSpace(purchaseOrder.CarrierName)) purchaseOrder.CarrierName = "Will Call";

            // Compile the CSV rows for the specified Purchase Order objects
            string[] purchaseOrderLines = ToCsv(purchaseOrder);

            // Attempt to import the CSV rows into Fishbowl
            await ImportAsync(PURCHASEORDER_IMPORT_NAME, purchaseOrderLines, cancellationToken);

            // Return the new PO record
            return await GetPurchaseOrderByNumberAsync(purchaseOrder.Number, cancellationToken);
        }

        /// <summary>
        /// Updates a purchase order. Optional parameters that are not passed in will be reset to their default values. Best practice is to send the complete object you would like to save.
        /// </summary>
        /// <param name="purchaseOrder"></param>
        /// <returns></returns>
        public Task<PurchaseOrder> UpdatePurchaseOrderAsync(PurchaseOrder purchaseOrder, CancellationToken cancellationToken = default) => CreatePurchaseOrderAsync(purchaseOrder, cancellationToken);



        /// <summary>
        /// Deserialize CSV data to PurchaseOrder/PurchaseOrderItem DTOs
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        private PurchaseOrder[] ToPurchaseOrders(DataTable table, bool includeItems = false)
        {
            // Ensure that a DataTable was provided
            if (table == null) return Array.Empty<PurchaseOrder>();

            // Initialize the collection
            var items = new List<PurchaseOrder>();

            // Iterate through the data rows
            foreach (DataRow row in table.Rows)
            {
                // Parse the top-level properties
                var item = row.ToObject<PurchaseOrder>();

                // Populate the RemitTo address
                item.RemitToAddress = new Address
                {
                    Name = row.Field<string>("RemitToName"),
                    StreetAddress = row.Field<string>("RemitToStreetAddress"),
                    City = row.Field<string>("RemitToCity"),
                    State = row.Field<string>("RemitToState"),
                    PostalCode = row.Field<string>("RemitToZip"),
                    Country = row.Field<string>("RemitToCountry")
                };

                // Populate the ShipTo address
                item.ShipToAddress = new Address
                {
                    Name = row.Field<string>("ShipToName"),
                    StreetAddress = row.Field<string>("ShipToStreetAddress"),
                    City = row.Field<string>("ShipToCity"),
                    State = row.Field<string>("ShipToState"),
                    PostalCode = row.Field<string>("ShipToZip"),
                    Country = row.Field<string>("ShipToCountry")
                };

                // Populate the Items collection
                if (includeItems)
                    item.Items = GetPurchaseOrderItemsAsync(item.Number).GetAwaiter().GetResult();

                // Add the item to the collection
                items.Add(item);
            }

            // Return the completed collection
            return items.ToArray();
        }

        /// <summary>
        /// Serialize the PurchaseOrder/PurchaseOrderItem DTOs to CSV data.
        /// </summary>
        /// <param name="purchaseOrder"></param>
        /// <returns></returns>
        private static string[] ToCsv(PurchaseOrder purchaseOrder)
        {
            // Initialize CSV line collection
            var lines = new List<string>
            {
                "\"Flag\",\"PONum\",\"Status\",\"VendorName\",\"VendorContact\",\"RemitToName\",\"RemitToAddress\",\"RemitToCity\",\"RemitToState\",\"RemitToZip\",\"RemitToCountry\",\"ShipToName\",\"DeliverToName\",\"ShipToAddress\",\"ShipToCity\",\"ShipToState\",\"ShipToZip\",\"ShipToCountry\",\"CarrierName\",\"CarrierService\",\"VendorSONum\",\"CustomerSONum\",\"CreatedDate\",\"CompletedDate\",\"ConfirmedDate\",\"FulfillmentDate\",\"IssuedDate\",\"Buyer\",\"ShippingTerms\",\"PaymentTerms\",\"FOB\",\"Note\",\"QuickBooksClassName\",\"LocationGroupName\",\"URL\",\"Phone\",\"Email\"",
                "\"Flag\",\"POItemTypeID\",\"PartNumber\",\"VendorPartNumber\",\"PartQuantity\",\"FulfilledQuantity\",\"PickedQuantity\",\"UOM\",\"PartPrice\",\"FulfillmentDate\",\"LastFulfillmentDate\",\"RevisionLevel\",\"Note\",\"QuickBooksClassName\",\"CustomerJob\""
            };

            // Append the CSV line for the PO
            var csv = new CsvBuilder();

            csv.Add("PO");  // "Flag"
            csv.Add(purchaseOrder.Number);  // "PONum"
            csv.Add(((int)purchaseOrder.Status).ToString());  // "Status"
            csv.Add(purchaseOrder.VendorName);  // "VendorName"
            csv.Add(purchaseOrder.RemitToAddress.Attention);  // "VendorContact"
            csv.Add(purchaseOrder.RemitToAddress.Name);  // "RemitToName"
            csv.Add(purchaseOrder.RemitToAddress.StreetAddress);  // "RemitToAddress"
            csv.Add(purchaseOrder.RemitToAddress.City);  // "RemitToCity"
            csv.Add(purchaseOrder.RemitToAddress.State);  // "RemitToState"
            csv.Add(purchaseOrder.RemitToAddress.PostalCode);  // "RemitToZip"
            csv.Add(purchaseOrder.RemitToAddress.Country);  // "RemitToCountry"
            csv.Add(purchaseOrder.ShipToAddress.Name);  // "ShipToName"
            csv.Add(purchaseOrder.DeliverTo);  // "DeliverToName"
            csv.Add(purchaseOrder.ShipToAddress.StreetAddress);  // "ShipToAddress"
            csv.Add(purchaseOrder.ShipToAddress.City);  // "ShipToCity"
            csv.Add(purchaseOrder.ShipToAddress.State);  // "ShipToState"
            csv.Add(purchaseOrder.ShipToAddress.PostalCode);  // "ShipToZip"
            csv.Add(purchaseOrder.ShipToAddress.Country);  // "ShipToCountry"
            csv.Add(purchaseOrder.CarrierName);  // "CarrierName"
            csv.Add(purchaseOrder.CarrierServiceName);  // "CarrierService"
            csv.Add(purchaseOrder.VendorSalesOrderNumber);  // "VendorSONum"
            csv.Add(purchaseOrder.CustomerSalesOrderNumber);  // "CustomerSONum"
            csv.Add(purchaseOrder.DateCreated.ToShortDateString());  // "CreatedDate"
            csv.Add(purchaseOrder.DateCompleted?.ToShortDateString());  // "CompletedDate"
            csv.Add(purchaseOrder.DateConfirmed?.ToShortDateString());  // "ConfirmedDate"
            csv.Add(purchaseOrder.DateScheduled?.ToShortDateString());  // "FulfillmentDate"
            csv.Add(purchaseOrder.DateLastModified?.ToShortDateString());  // "IssuedDate"
            csv.Add(purchaseOrder.BuyerUserName);  // "Buyer"
            csv.Add(purchaseOrder.ShippingTerms.GetDisplayName());  // "ShippingTerms"
            csv.Add(purchaseOrder.PaymentTermsName);  // "PaymentTerms"
            csv.Add(purchaseOrder.FobPointName);  // "FOB"
            csv.Add(purchaseOrder.Note);  // "Note"
            csv.Add(purchaseOrder.QuickBooksClassName);  // "QuickBooksClassName"
            csv.Add(purchaseOrder.LocationGroupName);  // "LocationGroupName"
            csv.Add(purchaseOrder.Url);  // "URL"
            csv.Add(purchaseOrder.VendorPhone);  // "Phone"
            csv.Add(purchaseOrder.VendorEmail);  // "Email"

            lines.Add(csv.ToString());

            // Add the CSV lines for the PO Items
            if (purchaseOrder.Items != null)
            {
                foreach (var item in purchaseOrder.Items) lines.Add(ToCsv(item));
            }            

            // Return the CSV lines
            return lines.ToArray();
        }

        /// <summary>
        /// Serialize the PurchaseOrderItem DTO to CSV data.
        /// </summary>
        /// <param name="purchaseOrder"></param>
        /// <returns></returns>
        private static string ToCsv(PurchaseOrderItem purchaseOrderItem)
        {
            // Build the CSV line for the PO Item
            var csv = new CsvBuilder();

            csv.Add("Item");  // "Flag"
            csv.Add(purchaseOrderItem.Type.GetDisplayName());  // "POItemTypeID"
            csv.Add(purchaseOrderItem.PartNumber);  // "PartNumber"
            csv.Add(purchaseOrderItem.VendorPartNumber);  // "VendorPartNumber"
            csv.Add(purchaseOrderItem.QuantityToFulfill.ToString());  // "PartQuantity"
            csv.Add(purchaseOrderItem.QuantityFulfilled.ToString());  // "FulfilledQuantity"
            csv.Add(purchaseOrderItem.QuantityPicked.ToString());  // "PickedQuantity"
            csv.Add(purchaseOrderItem.UnitOfMeasure);  // "UOM"
            csv.Add(purchaseOrderItem.UnitCost.ToString());  // "PartPrice"
            csv.Add(purchaseOrderItem.DateScheduled?.ToFishbowlDateString());  // "FulfillmentDate"
            csv.Add(purchaseOrderItem.DateLastFulfilled?.ToFishbowlDateString());  // "LastFulfillmentDate"
            csv.Add(purchaseOrderItem.Revision);  // "RevisionLevel"
            csv.Add(purchaseOrderItem.Note);  // "Note"
            csv.Add(purchaseOrderItem.QuickBooksClassName);  // "QuickBooksClassName"
            csv.Add(purchaseOrderItem.CustomerName);  // "CustomerJob"

            // Return the CSV lines
            return csv.ToString();
        }

        #endregion Purchase Orders

        #region Queries

        /// <summary>
        /// Returns results of sql query in csv format. Two options are available. 
        /// A query that has been saved in the Fishbowl Client data module can be executed using <Name>
        /// or a query can be placed directly in the call using <Query>.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="sqlQuery"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<DataTable> ExecuteQueryAsync(string name = null, string sqlQuery = null, CancellationToken cancellationToken = default)
        {
            // Clean up sqlQuery formatting
            sqlQuery = sqlQuery?.Replace(Environment.NewLine, " ").Trim();

            // Send the request
            var response = await SendAsync<ExecuteQueryRequest, ExecuteQueryResponse>(
                new ExecuteQueryRequest { Name = name, Query = sqlQuery }, 
                cancellationToken);

            // Return the populated DataTable
            return response.Data.GetTable();
        }

        /// <summary>
        /// Returns results of sql query cast to objects. Two options are available. 
        /// A query that has been saved in the Fishbowl Client data module can be executed using <Name>
        /// or a query can be placed directly in the call using <Query>.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="sqlQuery"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<T[]> ExecuteQueryAsync<T>(string name = null, string sqlQuery = null, CancellationToken cancellationToken = default) where T:new()
        {
            // Send the request
            var table = await ExecuteQueryAsync(name, sqlQuery, cancellationToken);

            // Cast the populated DataTable to a collection of objects
            return table.ToObjects<T>();
        }

        /// <summary>
        /// Execute a scalar data query against the database using SQL.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="sqlQuery"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<TResult> ExecuteScalarQueryAsync<TResult>(string sqlQuery, CancellationToken cancellationToken = default)
        {
            // Execute the data query
            var table = await ExecuteQueryAsync(sqlQuery: sqlQuery, cancellationToken: cancellationToken);

            // Extract the scalar result from the datatable
            string fieldValue = table.Rows[0].Field<string>(0);

            return (TResult)Convert.ChangeType(fieldValue, typeof(TResult));
        }

        #endregion Queries

        #region Sales Orders

        /// <summary>
        /// Adds an item to a Sales Order.
        /// </summary>
        /// <param name="salesOrderNumber"></param>
        /// <param name="item"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task AddSalesOrderItemAsync(string salesOrderNumber, SalesOrderItem item, CancellationToken cancellationToken = default) 
            => SendAsync<AddSalesOrderItemRequest, AddSalesOrderItemResponse>(
                new AddSalesOrderItemRequest
                {
                    SalesOrderNumber = salesOrderNumber,
                    Item = item
                },
                cancellationToken);

        #endregion Sales Orders

        #region Users

        private const string USER_IMPORT_NAME = "ImportUser";

        private const string USER_SELECT_QUERY =
            @"SELECT
                userName AS UserName, 
                firstName AS FirstName, 
                lastName AS LastName, 
                initials AS Initials, 
                activeFlag AS Active,
                email AS Email, 
                phone AS Phone, 
                userPwd AS Password
                
            FROM sysuser AS user";
        private const string GROUP_SELECT_QUERY =
            @"SELECT
                user.userName AS UserName, 
                user.firstName AS FirstName, 
                user.lastName AS LastName, 
                user.initials AS Initials, 
                user.activeFlag AS Active,
                user.email AS Email, 
                user.phone AS Phone, 
                user.userPwd AS Password
                
            FROM usergroup AS grp

            INNER JOIN usergrouprel AS rel ON rel.groupId = grp.id
            INNER JOIN sysuser AS user ON user.id = rel.userId";



        /// <summary>
        /// Get all Users
        /// </summary>
        public async Task<User[]> GetUsersAsync(CancellationToken cancellationToken = default)
        {
            // Build the MySQL SELECT query
            string sqlQuery = $"{USER_SELECT_QUERY} ORDER BY lastName,firstName";

            // Execute the SELECT query
            var usersTable = await ExecuteQueryAsync(sqlQuery: sqlQuery, cancellationToken: cancellationToken);

            // Parse the rows into User objects
            return ToUsers(usersTable);
        }

        /// <summary>
        /// Get all users that match the search criteria
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="firstName">The first name of the user.</param>
        /// <param name="lastName">The last name of the user.</param>
        /// <param name="initials">The initials of the user's name.</param>
        /// <param name="active">The active status of the user.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<User[]> SearchUsersAsync(string username = null, string firstName = null, string lastName = null,
            string initials = null, bool? active = null, CancellationToken cancellationToken = default)
        {
            // Assemble the WHERE statement
            var queryParameters = new Dictionary<string, string>();
            queryParameters.AddParameterIfNotNull("userName", username);
            queryParameters.AddParameterIfNotNull("firstName", firstName);
            queryParameters.AddParameterIfNotNull("lastName", lastName);
            queryParameters.AddParameterIfNotNull("initials", initials);
            queryParameters.AddParameterIfNotNull("activeFlag", active);

            string whereConditions = string.Join(" AND ", queryParameters.Select(qp => $"{qp.Key}='{qp.Value}'"));

            // Don't proceed if no search parameters were provided
            if (queryParameters.Count == 0) return await GetUsersAsync();

            // Build the MySQL SELECT query
            string sqlQuery = $"{USER_SELECT_QUERY} WHERE '{whereConditions}' ORDER BY lastName, firstName";

            // Execute the SELECT query
            var usersTable = await ExecuteQueryAsync(sqlQuery: sqlQuery, cancellationToken: cancellationToken);

            // Parse the rows into User objects
            return ToUsers(usersTable);
        }

        /// <summary>
        /// Retrieves the details of an existing purchase order. You only need to provide the unique purchase order number.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public async Task<User[]> GetUsersInGroupAsync(string groupName, CancellationToken cancellationToken = default)
        {
            // Build the MySQL SELECT query
            string sqlQuery = $"{GROUP_SELECT_QUERY} WHERE grp.name = '{groupName}' ORDER BY user.lastName, user.firstName";

            // Execute the SELECT query
            var usersTable = await ExecuteQueryAsync(sqlQuery: sqlQuery, cancellationToken: cancellationToken);

            // Parse the rows into User objects
            return ToUsers(usersTable);
        }

        /// <summary>
        /// Retrieves the details of an existing user. You only need to provide the unique user ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<User> GetUserAsync(int id, CancellationToken cancellationToken = default)
        {
            // Build the MySQL SELECT query
            string sqlQuery = $"{USER_SELECT_QUERY} WHERE user.id = '{id}'";

            // Execute the SELECT query
            var usersTable = await ExecuteQueryAsync(sqlQuery: sqlQuery, cancellationToken: cancellationToken);

            // Parse the rows into User objects
            return ToUsers(usersTable).FirstOrDefault();
        }

        /// <summary>
        /// Retrieves the details of an existing user. You only need to provide the unique <paramref name="userName"/>.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<User> GetUserByNameAsync(string userName, CancellationToken cancellationToken = default)
        {
            // Build the MySQL SELECT query
            string sqlQuery = $"{USER_SELECT_QUERY} WHERE user.userName = '{userName}'";

            // Execute the SELECT query
            var usersTable = await ExecuteQueryAsync(sqlQuery: sqlQuery, cancellationToken: cancellationToken);

            // Parse the rows into User objects
            return ToUsers(usersTable).FirstOrDefault();
        }

        /// <summary>
        /// Create a User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<User> CreateUserAsync(User user, CancellationToken cancellationToken = default)
        {
            // Compile the CSV rows for the specified Purchase Order objects
            string[] userLines = ToCsv(user);

            // Attempt to import the CSV rows into Fishbowl
            await ImportAsync(USER_IMPORT_NAME, userLines, cancellationToken);

            // Return the new PO record
            return await GetUserByNameAsync(user.UserName, cancellationToken);
        }

        /// <summary>
        /// Updates an User. Optional parameters that are not passed in will be reset to their default values. Best practice is to send the complete object you would like to save.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<User> UpdateUserAsync(User user, CancellationToken cancellationToken = default) => CreateUserAsync(user, cancellationToken);



        /// <summary>
        /// Deserialize CSV data to User DTOs
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        private User[] ToUsers(DataTable table)
        {
            // Don't proceed if no table was provided
            if (table == null) return Array.Empty<User>();

            // Initialize the collection
            var items = new List<User>();

            // Iterate through the data rows
            foreach (DataRow row in table.Rows)
            {
                // Parse the top-level properties
                var item = row.ToObject<User>();

                // Add the item to the collection
                items.Add(item);
            }

            // Return the completed collection
            return items.ToArray();
        }

        /// <summary>
        /// Serialize the User DTOs to CSV data.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private static string[] ToCsv(User user)
        {
            // Initialize CSV line collection
            var lines = new List<string>
            {
                "\"UserName\",\"FirstName\",\"LastName\",\"Initials\",\"Active\",\"UserGroups\",\"DefaultLocGroup\",\"LocGroups\",\"Email\",\"Phone\""
            };

            // Append the CSV line for the PO
            var csv = new CsvBuilder();

            csv.Add(user.UserName);  // "UserName"
            csv.Add(user.FirstName);  // "FirstName"
            csv.Add(user.LastName);  // "LastName"
            csv.Add(user.Initials);  // "Initials"
            csv.Add(user.IsActive.ToString());  // "Active"
            csv.Add(user.UserGroups);  // "UserGroups"
            csv.Add(user.DefaultLocGroup);  // "DefaultLocGroup"
            csv.Add(user.LocGroups);  // "LocGroups"
            csv.Add(user.Email);  // "Email"
            csv.Add(user.Phone);  // "Phone"

            lines.Add(csv.ToString());

            // Return the CSV lines
            return lines.ToArray();
        }

        #endregion Users

        #region Vendors

        private const string Vendor_Import_Name = "ImportVendors";
        private const string VENDOR_SELECT_QUERY =
            @"SELECT 
                vendor.id AS Id,
                vendor.name AS Name, 
                addr.addressName AS AddressName, 
                cont.contactName AS AddressContact, 
                addr.typeID AS AddressType, 
                addr.defaultFlag AS IsDefault,
                addr.address AS Address, 
                addr.city AS City, state.code AS State, 
                addr.zip AS Zip, 
                country.name AS Country, 
                addr.residentialFlag AS Residential, 
                mainContact.datus AS Main,
                homeContact.datus AS Home, 
                workContact.datus AS Work, 
                mobileContact.datus AS Mobile, 
                faxContact.datus AS Fax, 
                emailContact.datus AS Email, 
                pagerContact.datus AS Pager,
                webContact.datus AS Web, 
                otherContact.datus AS Other, 
                status.name AS Status, 
                vendor.activeFlag AS Active, 
                curr.name AS CurrencyName, 
                vendor.currencyRate AS CurrencyRate,
                carr.name AS DefaultCarrier, 
                ship.name AS DefaultShippingTerms, 
                vendor.accountNum AS AccountNumber, 
                vendor.minOrderAmount AS MinOrderAmount,
                vendor.note AS AlertNotes, 
                vendor.url AS Url, 
                carsrv.name AS DefaultCarrierService, 
                pymt.name AS DefaultTerms, 
                vendor.creditLimit AS CreditLimit,
                vendor.dateEntered AS DateEntered, 
                vendor.dateLastModified AS DateLastModified, 
                vendor.lastChangedUser AS LastChangedUser, 
                vendor.leadTime AS LeadTime,
                tax.name AS TaxRate
                FROM vendor
                LEFT JOIN address AS addr ON addr.accountId = vendor.accountId
                LEFT JOIN contact AS cont ON (cont.addressId = addr.id AND cont.accountId = vendor.accountId)
                LEFT JOIN stateconst AS state ON state.id = addr.stateId
                LEFT JOIN countryconst AS country ON country.id = addr.countryId
                LEFT JOIN contact AS homeContact ON (homeContact.accountId = vendor.accountId AND homeContact.typeID = '10')
                LEFT JOIN contact AS workContact ON (workContact.accountId = vendor.accountId AND workContact.typeID = '20')
                LEFT JOIN contact AS mobileContact ON (mobileContact.accountId = vendor.accountId AND mobileContact.typeID = '30')
                LEFT JOIN contact AS faxContact ON (faxContact.accountId = vendor.accountId AND faxContact.typeID = '40')
                LEFT JOIN contact AS mainContact ON (mainContact.accountId = vendor.accountId AND mainContact.typeID = '50')
                LEFT JOIN contact AS emailContact ON (emailContact.accountId = vendor.accountId AND emailContact.typeID = '60')
                LEFT JOIN contact AS pagerContact ON (pagerContact.accountId = vendor.accountId AND pagerContact.typeID = '70')
                LEFT JOIN contact AS otherContact ON (otherContact.accountId = vendor.accountId AND otherContact.typeID = '80')
                LEFT JOIN contact AS webContact ON (webContact.accountId = vendor.accountId AND webContact.typeID = '90')
                LEFT JOIN carrier AS carr ON carr.id = vendor.defaultCarrierId
                LEFT JOIN vendorstatus AS status ON status.id = vendor.statusId
                LEFT JOIN shipterms AS ship ON ship.id = vendor.defaultShipTermsId
                LEFT JOIN carrierservice AS carsrv ON carsrv.id = vendor.defaultCarrierServiceId
                LEFT JOIN taxrate AS tax ON tax.id = vendor.taxRateId
                LEFT JOIN account AS acct ON acct.id = vendor.accountId
                LEFT JOIN paymentterms AS pymt ON pymt.id = vendor.defaultPaymentTermsId
                LEFT JOIN currency AS curr ON curr.id = vendor.currencyId";



        /// <summary>
        /// Get all Vendors
        /// </summary>
        public async Task<Vendor[]> GetVendorsAsync(CancellationToken cancellationToken = default)
        {
            // Build the MySQL SELECT query
            string sqlQuery = $"{VENDOR_SELECT_QUERY} GROUP BY addr.id ORDER BY vendor.name";

            // Execute the SELECT query
            var vendorsTable = await ExecuteQueryAsync(sqlQuery: sqlQuery, cancellationToken: cancellationToken);

            // Parse the rows into Vendor objects
            return ToVendors(vendorsTable);
        }

        /// <summary>
        /// Search for vendors.
        /// </summary>
        /// <param name="name">The vendor name.</param>
        /// <param name="accountNumber">The vendor account number.</param>
        /// <param name="city">The vendor address city.</param>
        /// <param name="state">The vendor address state.</param>
        /// <param name="country">The vendor address country</param>
        /// <param name="active">Indicates if the vendor is active.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Vendor[]> SearchVendorsAsync(string name = null, string accountNumber = null, string city = null,
            string state = null, string country = null, bool? active = null, CancellationToken cancellationToken = default)
        {
            // Compile the filter statements
            var filterStatements = new List<string>();

            if (!string.IsNullOrWhiteSpace(name)) filterStatements.Add($"(LOWER(vendor.name) LIKE '%{name.ToLower()}%')");
            if (!string.IsNullOrWhiteSpace(accountNumber)) filterStatements.Add($"(LOWER(vendor.accountNum) LIKE '%{accountNumber.ToLower()}%')");
            if (!string.IsNullOrWhiteSpace(city)) filterStatements.Add($"(LOWER(addr.city) LIKE '%{city.ToLower()}%')");
            if (!string.IsNullOrWhiteSpace(state)) filterStatements.Add($"(LOWER(state.code) LIKE '%{state.ToLower()}%')");
            if (!string.IsNullOrWhiteSpace(country)) filterStatements.Add($"(LOWER(country.name) LIKE '%{country.ToLower()}%')");
            if (active.HasValue) filterStatements.Add($"(vendor.activeFlag = {active.Value.ToString().ToUpper()})");
            filterStatements.Add("(addr.defaultFlag = TRUE)");

            string filterQuery = string.Join(" AND ", filterStatements);

            // Build the MySQL SELECT query
            string sqlQuery = $"{VENDOR_SELECT_QUERY} WHERE {filterQuery} GROUP BY addr.id ORDER BY vendor.name";

            // Execute the SELECT query
            var vendorTable = await ExecuteQueryAsync(sqlQuery: sqlQuery, cancellationToken: cancellationToken);

            // Parse the rows into Vendor/Address/Contact objects
            return ToVendors(vendorTable);
        }

        /// <summary>
        /// Return the Vendor containing the specified id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Vendor> GetVendorAsync(int id, CancellationToken cancellationToken = default)
        {
            // Build the MySQL SELECT query
            string sqlQuery = $"{VENDOR_SELECT_QUERY} WHERE (vendor.id = {id}) AND (addr.defaultFlag = TRUE) GROUP BY addr.id";

            // Execute the SELECT query
            var vendorsTable = await ExecuteQueryAsync(sqlQuery: sqlQuery, cancellationToken: cancellationToken);

            // Parse the rows into Vendor/Address/Contact objects
            return ToVendors(vendorsTable).FirstOrDefault();
        }

        /// <summary>
        /// Return the Vendor containing the specified name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<Vendor> GetVendorByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            // Build the MySQL SELECT query
            string sqlQuery = $"{VENDOR_SELECT_QUERY} WHERE (LOWER(vendor.name) LIKE \"{name.ToLower()}\") AND (addr.defaultFlag = TRUE) GROUP BY addr.id";

            // Execute the SELECT query
            var vendorTable = await ExecuteQueryAsync(sqlQuery: sqlQuery, cancellationToken: cancellationToken);

            // Parse the rows into Vendor/Address/Contact objects
            return ToVendors(vendorTable).FirstOrDefault();
        }

        /// <summary>
        /// Create a vendor
        /// </summary>
        /// <param name="vendor"></param>
        /// <returns></returns>
        public async Task<Vendor> CreateVendorAsync(Vendor vendor, CancellationToken cancellationToken = default)
        {
            // Ensure that the vendor record has a name
            if (string.IsNullOrWhiteSpace(vendor.Name)) throw new ArgumentException("Vendor must have a valid name!");

            // Ensure that the vendor record has a default main office address
            if (vendor.Addresses == null || vendor.Addresses.Any(a => a.IsDefault == true && a.Type == AddressType.MainOffice) == false)
                throw new ArgumentException("Vendor must have a default main office address!");

            // Compile the CSV rows for the specified Vendor objects
            string[] csvLines = ToCsv(vendor);

            // Attempt to import the CSV rows into Fishbowl
            await ImportAsync(Vendor_Import_Name, csvLines, cancellationToken);

            // Return the new Vendor record
            return await GetVendorByNameAsync(vendor.Name, cancellationToken);
        }

        /// <summary>
        /// Updates a vendor. Optional parameters that are not passed in will be reset to their default values. Best practice is to send the complete object you would like to save.
        /// </summary>
        /// <param name="vendor"></param>
        /// <returns></returns>
        public Task<Vendor> UpdateVendorAsync(Vendor vendor, CancellationToken cancellationToken = default) => CreateVendorAsync(vendor, cancellationToken);




        /// <summary>
        /// Deserialize CSV data to Vendor/Address/Contact DTOs
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        private Vendor[] ToVendors(DataTable table)
        {
            // Ensure that a valid DataTable was provided
            if (table == null) return Array.Empty<Vendor>();

            // Initialize CSV dto collection
            var vendors = new List<Vendor>();

            // Iterate through the VendorCSV rows
            foreach (DataRow row in table.Rows)
            {
                // Create/Retrieve the Vendor DTO by the csv's Name
                var vendor = vendors.SingleOrDefault(c => c.Name == row.Field<string>("Name"));
                if (vendor == null)
                {
                    int.TryParse(row.Field<string>("Id"), out int id);
                    decimal.TryParse(row.Field<string>("CurrencyRate"), out decimal currencyRate);
                    bool.TryParse(row.Field<string>("Active"), out bool isActive);

                    vendor = new Vendor
                    {
                        Id = id,
                        Name = row.Field<string>("Name"),
                        CurrencyName = row.Field<string>("CurrencyName"),
                        CurrencyRate = currencyRate,
                        DefaultPaymentTerms = row.Field<string>("DefaultTerms"),
                        DefaultCarrier = row.Field<string>("DefaultCarrier"),
                        DefaultShippingTerms = row.Field<string>("DefaultShippingTerms"),
                        Status = row.Field<string>("Status"),
                        AccountNumber = row.Field<string>("AccountNumber"),
                        IsActive = isActive,
                        Note = row.Field<string>("AlertNotes"),
                        Url = row.Field<string>("Url"),
                        DefaultCarrierService = row.Field<string>("DefaultCarrierService")
                    };

                    vendors.Add(vendor);
                }

                // Create/Retrieve the Address DTO by the csv's AddressName
                var address = vendor.Addresses.SingleOrDefault(a => a.Name == row.Field<string>("AddressName"));
                if (address == null)
                {
                    bool.TryParse(row.Field<string>("IsDefault"), out bool isDefault);
                    
                    int.TryParse(row.Field<string>("AddressType"), out int addressTypeVal);
                    AddressType addressType = Enum.IsDefined(typeof(AddressType), addressTypeVal) ? (AddressType)addressTypeVal : AddressType.MainOffice;
                    
                    address = new Address
                    {                        
                        Name = row.Field<string>("AddressName"),
                        Attention = row.Field<string>("AddressContact"),
                        Type = addressType,
                        IsDefault = isDefault,
                        StreetAddress = row.Field<string>("Address"),
                        City = row.Field<string>("City"),
                        State = row.Field<string>("State"),
                        PostalCode = row.Field<string>("Zip"),
                        Country = row.Field<string>("Country")
                    };

                    vendor.Addresses.Add(address);
                }

                // Create/Retrieve the Contact records
                var homeContact = AddUpdateContact(address.Contacts, row.Field<string>("AddressContact"), ContactType.Home, row.Field<string>("Home"));
                var workContact = AddUpdateContact(address.Contacts, row.Field<string>("AddressContact"), ContactType.Work, row.Field<string>("Work"));
                var mobileContact = AddUpdateContact(address.Contacts, row.Field<string>("AddressContact"), ContactType.Mobile, row.Field<string>("Mobile"));
                var faxContact = AddUpdateContact(address.Contacts, row.Field<string>("AddressContact"), ContactType.Fax, row.Field<string>("Fax"));
                var mainContact = AddUpdateContact(address.Contacts, row.Field<string>("AddressContact"), ContactType.Main, row.Field<string>("Main"));
                var emailContact = AddUpdateContact(address.Contacts, row.Field<string>("AddressContact"), ContactType.Email, row.Field<string>("Email"));
                var pagerContact = AddUpdateContact(address.Contacts, row.Field<string>("AddressContact"), ContactType.Pager, row.Field<string>("Pager")); 
                var otherContact = AddUpdateContact(address.Contacts, row.Field<string>("AddressContact"), ContactType.Other, row.Field<string>("Other")); 
                var webContact = AddUpdateContact(address.Contacts, row.Field<string>("AddressContact"), ContactType.Web, row.Field<string>("Web"));
            }

            // Return the populated collection
            return vendors.ToArray();
        }

        /// <summary>
        /// Return the Contact record matching the Type and Datum (if it exists).  Otherwise, a new Contact record is created.
        /// </summary>
        /// <param name="contacts"></param>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="datum"></param>
        /// <returns></returns>
        private Contact AddUpdateContact(ICollection<Contact> contacts, string name, ContactType type, string datum)
        {
            // Search for a Contact record matching the specified Type and Datum
            var contact = contacts.SingleOrDefault(c => c.Type == type && c.Datum == datum);

            // Create a new Contact record (if necessary)
            if (contact == null)
            {
                contact = new Contact
                {
                    Name = name,
                    Type = type,
                    Datum = datum,
                    IsDefault = !contacts.Any(c => c.Type == type)
                };

                contacts.Add(contact);
            }

            // Return the Contact record
            return contact;
        }



        /// <summary>
        /// Serialize Vendor/Address/Contact DTOs to CSV data
        /// </summary>
        /// <param name="vendors"></param>
        /// <returns></returns>
        private string[] ToCsv(Vendor vendor, bool includeHeaderRow = true)
        {
            // Initialize CSV lines collection
            var lines = new List<string>();

            if (includeHeaderRow)
                lines.Add("Name,AddressName,AddressContact,AddressType,IsDefault,Address,City,State,Zip,Country,Main,Home,Work,Mobile,Fax,Email,Pager,Web,Other,CurrencyName,CurrencyRate,DefaultTerms,DefaultCarrier,DefaultShippingTerms,Status,AccountNumber,Active,MinOrderAmount,AlertNotes,URL,DefaultCarrierService");
            
            // Iterate through the Address DTOs
            foreach (var address in vendor.Addresses)
            {
                // Create pointers to address contacts
                var homeContact = address.Contacts.SingleOrDefault(c => c.Type == ContactType.Home);
                var workContact = address.Contacts.SingleOrDefault(c => c.Type == ContactType.Work);
                var mobileContact = address.Contacts.SingleOrDefault(c => c.Type == ContactType.Mobile);
                var faxContact = address.Contacts.SingleOrDefault(c => c.Type == ContactType.Fax);
                var mainContact = address.Contacts.SingleOrDefault(c => c.Type == ContactType.Main);
                var emailContact = address.Contacts.SingleOrDefault(c => c.Type == ContactType.Email);
                var pagerContact = address.Contacts.SingleOrDefault(c => c.Type == ContactType.Pager);
                var otherContact = address.Contacts.SingleOrDefault(c => c.Type == ContactType.Other);
                var webContact = address.Contacts.SingleOrDefault(c => c.Type == ContactType.Web);

                // Create a CSV record for this Vendor-Address-Contact row
                var csv = new CsvBuilder();

                csv.Add(vendor.Name);  // Name
                csv.Add(address.Name);  // AddressName
                csv.Add(address.Attention);  // AddressContact
                csv.Add(((int)address.Type).ToString());  // AddressType
                csv.Add(address.IsDefault.ToString());  // IsDefault
                csv.Add(address.StreetAddress);  // Address
                csv.Add(address.City);  // City
                csv.Add(address.State);  // State
                csv.Add(address.PostalCode);  // Zip
                csv.Add(address.Country);  // Country
                csv.Add(mainContact?.Datum);  // Main
                csv.Add(homeContact?.Datum);  // Home
                csv.Add(workContact?.Datum);  // Work
                csv.Add(mobileContact?.Datum);  // Mobile
                csv.Add(faxContact?.Datum);  // Fax
                csv.Add(emailContact?.Datum);  // Email
                csv.Add(pagerContact?.Datum);  // Pager
                csv.Add(webContact?.Datum);  // Web
                csv.Add(otherContact?.Datum);  // Other
                csv.Add(vendor.CurrencyName);  // CurrencyName
                csv.Add(vendor.CurrencyRate.ToString());  // CurrencyRate
                csv.Add(vendor.DefaultPaymentTerms);  // DefaultTerms
                csv.Add(vendor.DefaultCarrier);  // DefaultCarrier
                csv.Add(vendor.DefaultShippingTerms);  // DefaultShippingTerms
                csv.Add(vendor.Status);  // Status
                csv.Add(vendor.AccountNumber);  // AccountNumber
                csv.Add(vendor.IsActive.ToString());  // Active
                csv.Add(vendor.MinimumOrderAmount.ToString());  // MinOrderAmount
                csv.Add(vendor.Note);  // AlertNotes
                csv.Add(vendor.Url);  // URL
                csv.Add(vendor.DefaultCarrierService);  // DefaultCarrierService

                // Add the completed CSV line to the collection
                lines.Add(csv.ToString());
            }

            // Return the populated collection
            return lines.ToArray();
        }

        #endregion Vendors

        #region Work Orders

        private const string WORKORDER_SELECT_QUERY =
            @"SELECT
            wo.id AS Id, 
            wo.calCategoryId AS CalCategoryId, 
            wo.cost AS Cost, 
            wo.customerId AS CustomerId,
            wo.dateCreated AS DateCreated, 
            wo.dateFinished AS DateFinished,
            wo.dateLastModified AS DateLastModified, 
            wo.dateScheduled AS DateScheduled,
            wo.dateScheduledToStart AS DateScheduledToStart, 
            wo.dateStarted AS DateStarted,
            wo.locationGroupId AS LocationGroupId, 
            wo.locationId AS LocationId,
            wo.moItemId AS ManufacturingOrderItemId, 
            wo.note AS Note, 
            wo.num AS Number, 
            wo.priorityId AS PriorityId, 
            wo.qbClassId AS QuickBooksClassId, 
            wo.qtyOrdered AS QuantityOrdered,
            wo.qtyScrapped AS QuantityScrapped, 
            wo.qtyTarget AS QuantityTarget,
            wo.statusId AS StatusId, 
		    wo.userId AS UserId, 
                
		    location.name AS LocationName,
		    lg.name AS LocationGroupName,
            mo.num AS ManufacturingOrderNumber, 
            moitem.description AS Description,
		    qb.name AS QuickBooksClassName,
		    stat.name AS StatusName,
		    user.userName AS UserName

            FROM wo

            LEFT JOIN calcategory AS calcat ON calcat.id = wo.calCategoryId
            LEFT JOIN customer ON customer.id = wo.customerId
            LEFT JOIN location ON location.id = wo.locationId
            LEFT JOIN locationgroup AS lg ON lg.id = wo.locationGroupId
            INNER JOIN moitem ON moitem.id = wo.moItemId
            LEFT JOIN mo ON moitem.moId = mo.id
            INNER JOIN priority ON priority.id = wo.priorityId
            LEFT JOIN qbclass AS qb ON qb.id = wo.qbClassId
            INNER JOIN sysuser AS user ON user.id = wo.userId
            INNER JOIN wostatus AS stat ON stat.id = wo.statusId";



        /// <summary>
        /// Return all Work Orders in Fishbowl
        /// </summary>
        /// <returns></returns>
        public async Task<WorkOrder[]> GetWorkOrdersAsync(bool includeItems = false, CancellationToken cancellationToken = default)
        {
            // Build the MySQL SELECT query
            string sqlQuery = $"{WORKORDER_SELECT_QUERY} ORDER BY wo.num";

            // Execute the SELECT query
            var workOrdersTable = await ExecuteQueryAsync(sqlQuery: sqlQuery, cancellationToken: cancellationToken);

            // Parse the rows into WorkOrder/WorkOrderItem objects
            return ToWorkOrders(workOrdersTable, includeItems);
        }

        /// <summary>
        /// Return all Work Orders in Fishbowl belonging to the specified Manufacturing Order
        /// </summary>
        /// <param name="moNumber"></param>
        /// <returns></returns>
        public async Task<WorkOrder[]> GetWorkOrdersAsync(string moNumber, bool includeItems = false, CancellationToken cancellationToken = default)
        {
            // Build the MySQL SELECT query
            string sqlQuery = $"{WORKORDER_SELECT_QUERY} WHERE mo.num = '{moNumber}' ORDER BY wo.num";

            // Execute the SELECT query
            var workOrdersTable = await ExecuteQueryAsync(sqlQuery: sqlQuery, cancellationToken: cancellationToken);

            // Parse the rows into WorkOrder/WorkOrderItem objects
            return ToWorkOrders(workOrdersTable, includeItems);
        }

        /// <summary>
        /// Get all Work Orders that match the search criteria
        /// </summary>
        /// <param name="number">The part number.</param>
        /// <param name="moNumber">The number of the associated Manufacturing Order.</param>
        /// <param name="status">The status of the WO.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<WorkOrder[]> SearchWorkOrdersAsync(string number = null, string moNumber = null, WorkOrderStatus? status = null, bool includeItems = false, CancellationToken cancellationToken = default)
        {
            // Assemble the WHERE statement
            var queryParameters = new Dictionary<string, string>();
            queryParameters.AddParameterIfNotNull("wo.num", number);
            queryParameters.AddParameterIfNotNull("mo.num", moNumber);
            queryParameters.AddParameterIfNotNull("wo.statusId", (int?)status);

            string whereConditions = string.Join(" AND ", queryParameters.Select(qp => $"{qp.Key}='{qp.Value}'"));

            // Don't proceed if no search parameters were provided
            if (queryParameters.Count == 0) return await GetWorkOrdersAsync();

            // Build the MySQL SELECT query
            string sqlQuery = $"{WORKORDER_SELECT_QUERY} WHERE {whereConditions} ORDER BY wo.num";

            // Execute the SELECT query
            var workOrdersTable = await ExecuteQueryAsync(sqlQuery: sqlQuery, cancellationToken: cancellationToken);

            // Parse the rows into Purchase Order objects
            return ToWorkOrders(workOrdersTable, includeItems);
        }

        /// <summary>
        /// Retrieves the details of an existing work order. You only need to provide the unique work order ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<WorkOrder> GetWorkOrderAsync(int id, CancellationToken cancellationToken = default)
        {
            // Build the MySQL SELECT query
            string sqlQuery = $"{WORKORDER_SELECT_QUERY} WHERE wo.id = '{id}'";

            // Execute the SELECT query
            var workOrdersTable = await ExecuteQueryAsync(sqlQuery: sqlQuery, cancellationToken: cancellationToken);

            // Parse the rows into WorkOrder/WorkOrderItem objects
            return ToWorkOrders(workOrdersTable, true).FirstOrDefault();
        }

        /// <summary>
        /// Retrieves the details of an existing work order. You only need to provide the unique work order number.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public async Task<WorkOrder> GetWorkOrderByNumberAsync(string number, CancellationToken cancellationToken = default)
        {
            // Build the MySQL SELECT query
            string sqlQuery = $"{WORKORDER_SELECT_QUERY} WHERE wo.num = '{number}'";

            // Execute the SELECT query
            var workOrdersTable = await ExecuteQueryAsync(sqlQuery: sqlQuery, cancellationToken: cancellationToken);

            // Parse the rows into WorkOrder/WorkOrderItem objects
            return ToWorkOrders(workOrdersTable, true).FirstOrDefault();
        }



        /// <summary>
        /// Add an item to an existing open WO.
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <param name="item"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task AddWorkOrderItemAsync(string orderNumber, WorkOrderItem item, CancellationToken cancellationToken = default)
            => SendAsync<AddWorkOrderItemRequest, AddWorkOrderItemResponse>(
                new AddWorkOrderItemRequest
                {
                    OrderNumber = orderNumber,
                    TypeId = item.TypeId,
                    Description = item.Description,
                    PartNumber = item.PartNumber,
                    Quantity = item.QuantityTarget,
                    UnitOfMeasureCode = item.UnitOfMeasure?.Abbreviation,
                    Cost = item.Cost
                },
                cancellationToken);

        /// <summary>
        /// Return all Work Order Items in Fishbowl belonging to the specified Work Order
        /// </summary>
        /// <param name="woNumber">The unique number of the Work Order</param>
        /// <returns></returns>
        public async Task<WorkOrderItem[]> GetWorkOrderItemsAsync(string woNumber, CancellationToken cancellationToken = default)
        {
            // Build the MySQL SELECT query
            string sqlQuery =
                @$"SELECT
                woitem.id AS Id, 
                woitem.moItemId AS ManufacturingOrderItemId, 
                woitem.partId AS PartId,
                woitem.typeId AS TypeId, 
                woitem.uomId AS UomId, 
                woitem.woId AS WorkOrderId, 
                woitem.cost AS Cost,
                woitem.standardCost AS StandardCost, 
                woitem.description AS Description, 
                woitem.qtyScrapped AS QuantityScrapped,
                woitem.qtyTarget AS QuantityTarget, 
                woitem.qtyUsed AS QuantityUsed, 
                woitem.sortId AS SortId, 
                woitem.oneTimeItem AS OneTimeItem,

                part.num AS PartNumber,
                uom.code AS UOM

                FROM woitem

                LEFT JOIN part ON woitem.partId = part.id
                LEFT JOIN moitem ON woitem.moItemId = moitem.id
                LEFT JOIN uom ON woitem.uomId = uom.id
                LEFT JOIN wo ON woitem.woId = wo.id

                WHERE wo.num = '{woNumber}'";

            // Execute the SELECT query
            var dt = await ExecuteQueryAsync(sqlQuery: sqlQuery, cancellationToken: cancellationToken);

            // Parse the rows into WorkOrderItem objects
            return ToWorkOrderItems(dt);
        }



        /// <summary>
        /// Deserialize CSV data to WorkOrder DTOs
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        private WorkOrder[] ToWorkOrders(DataTable table, bool includeItems = false)
        {
            // Don't proceed if no table was provided
            if (table == null) return Array.Empty<WorkOrder>();

            // Initialize the collection
            var items = new List<WorkOrder>();

            // Iterate through the data rows
            foreach (DataRow row in table.Rows)
            {
                // Parse the top-level properties
                //var workOrder = row.ToObject<WorkOrder>();

                int.TryParse(row.Field<string>("Id"), out int id);
                decimal.TryParse(row.Field<string>("Cost"), out decimal cost);
                DateTime? dateCreated = NullableDateTryParse(row.Field<string>("DateCreated"));
                DateTime? dateFinished = NullableDateTryParse(row.Field<string>("DateFinished"));
                DateTime? dateLastModified = NullableDateTryParse(row.Field<string>("DateLastModified"));
                DateTime? dateScheduled = NullableDateTryParse(row.Field<string>("DateScheduled"));
                DateTime? dateStarted = NullableDateTryParse(row.Field<string>("DateStarted"));
                int.TryParse(row.Field<string>("ManufacturingOrderItemId"), out int moItemId);
                int.TryParse(row.Field<string>("QuantityOrdered"), out int qtyOrdered);
                int.TryParse(row.Field<string>("QuantityTarget"), out int qtyTarget);

                Enum.TryParse(row.Field<string>("StatusName"), out WorkOrderStatus status);

                var workOrder = new WorkOrder
                {
                    Id = id,
                    Cost = cost,
                    DateCreated = dateCreated,
                    DateFinished = dateFinished,
                    DateLastModified = dateLastModified,                    
                    DateScheduled = dateScheduled,
                    DateStarted = dateStarted,
                    ManufacturingOrderItemId = moItemId,
                    Note = row.Field<string>("Note"),
                    Number = row.Field<string>("Number"),
                    QuantityOrdered = qtyOrdered,
                    QuantityTarget = qtyTarget,                    
                    
                    LocationName = row.Field<string>("LocationName"),
                    LocationGroupName = row.Field<string>("LocationGroupName"),
                    ManufacturingOrderNumber = row.Field<string>("ManufacturingOrderNumber"),
                    Description = row.Field<string>("Description"),
                    QuickBooksClassName = row.Field<string>("QuickBooksClassName"),
                    Status = status,
                    UserName = row.Field<string>("Username")
                };

                // Populate the Items collection
                if (includeItems)
                    workOrder.Items = GetWorkOrderItemsAsync(workOrder.Number).GetAwaiter().GetResult();

                // Add the item to the collection
                items.Add(workOrder);
            }

            // Return the completed collection
            return items.ToArray();
        }

        /// <summary>
        /// Deserialize CSV data to WorkOrderItem DTOs
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        private WorkOrderItem[] ToWorkOrderItems(DataTable table)
        {
            // Don't proceed if no table was provided
            if (table == null) return Array.Empty<WorkOrderItem>();

            // Initialize the collection
            var items = new List<WorkOrderItem>();

            // Iterate through the data rows
            foreach (DataRow row in table.Rows)
            {
                int.TryParse(row.Field<string>("Id"), out int id);
                int.TryParse(row.Field<string>("ManufacturingOrderItemId"), out int moItemId);
                int.TryParse(row.Field<string>("TypeId"), out int typeId);
                decimal.TryParse(row.Field<string>("Cost"), out decimal cost);
                int.TryParse(row.Field<string>("QuantityScrapped"), out int qtyScrapped);
                int.TryParse(row.Field<string>("QuantityTarget"), out int qtyTarget);
                int.TryParse(row.Field<string>("QuantityUsed"), out int qtyUsed);
                int.TryParse(row.Field<string>("SortId"), out int sortId);

                var item = new WorkOrderItem
                {
                    Id = id,
                    ManufacturingOrderItemId = moItemId,
                    TypeId = typeId,
                    PartNumber = row.Field<string>("PartNumber"),
                    Description = row.Field<string>("Description"),
                    Cost = cost,
                    QuantityScrapped = qtyScrapped,
                    QuantityTarget = qtyTarget,
                    QuantityUsed = qtyUsed,

                    UnitOfMeasure = new UnitOfMeasure
                    {
                        Abbreviation = row.Field<string>("UOM")
                    },

                    SortId = sortId
                };

                // Add the item to the collection
                items.Add(item);
            }

            // Return the completed collection
            return items.ToArray();
        }

        #endregion Work Orders



        #region API Helper Methods

        private async Task<TResponse> SendAsync<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default)
            where TRequest : IFishbowlRequest
            where TResponse : IFishbowlResponse<TRequest>, new()
        {
            try
            {
                // Connect to the Fishbowl server
                using var client = new TcpClient();
                client.ReceiveTimeout = 5000;
                client.SendTimeout = 5000;

                await client.ConnectAsync(_hostname, _port);

                // Initialize stream reader/writer
                using var networkStream = client.GetStream();
                using var bw = new EndiannessAwareBinaryWriter(networkStream);
                using var br = new EndiannessAwareBinaryReader(networkStream);

                // Serialize the Request object into JSON
                string requestJson = SerializeRequest(request);
#if DEBUG
                //Debug.WriteLine($"{typeof(TRequest).Name}:  {requestJson}");
#endif

                // Serialize the JSON into a byte array
                byte[] requestData = Encoding.ASCII.GetBytes(requestJson);

                // Send the serialized Request and wait for a response
                string responseJson = await Task.Run(() => 
                {
                    // Inform the server of the request buffer size
                    bw.Write(requestData.Length);

                    // Send the serialized Request data
                    bw.Write(requestData);
                    bw.Flush();

                    // Wait for the server to process the request
                    Thread.Sleep(1000);

                    // Initialize the response buffer
                    int bufferSize = br.ReadInt32();
                    byte[] responseData = new byte[bufferSize];

                    // Get the response byte array
                    br.Read(responseData, 0, bufferSize);

                    // Deserialize the byte array into JSON
                    return Encoding.ASCII.GetString(responseData, 0, bufferSize);
                }, cancellationToken);
#if DEBUG
                //Debug.WriteLine($"{typeof(TResponse).Name}:  {responseJson}");
#endif

                // Disconnect from Fishbowl
                client.Close();

                // Deserialize/return the response object from JSON
                return DeserializeResponse<TRequest, TResponse>(responseJson);
            }
            catch (FishbowlInventoryException ex) { throw ex; }  // If the exception has already been processed/wrapped, return it
            catch (Exception ex)  { throw new FishbowlInventoryServerException($"An {ex.GetType().Name} occurred while processing the {typeof(TRequest).Name} request.", ex); }  // Encapsulate any non-local exceptions
        }

        private string SerializeRequest<TRequest>(TRequest request) where TRequest : IFishbowlRequest
        {
            try
            {
                // Serialize the request object            
                var requestObject = JsonNode.Parse(JsonSerializer.Serialize(request, _jsonOptions));

                // Build JsonObject
                var fbiJson = new JsonObject
                {
                    // Add the 'Ticket' node (use blank string if a token hasn't been cached)
                    { "Ticket", new JsonObject { new KeyValuePair<string, JsonNode>("Key", string.IsNullOrWhiteSpace(_token) ? "" : _token) } },

                    // Add the request object
                    { "FbiMsgsRq", new JsonObject { new KeyValuePair<string, JsonNode>(request.ElementName, requestObject) } }
                };

                // Build the Fishbowl API request
                var serializedRequest = new JsonObject
                {
                    { "FbiJson", fbiJson }
                };

                // Return the completed JsonObject as a string
                return serializedRequest.ToJsonString();
            }
            catch (Exception ex)
            {
                throw new FishbowlInventoryServerException($"An {ex.GetType().Name} occurred while serializing the request:  {ex.Message}", ex);
            }
        }

        private TResponse DeserializeResponse<TRequest,TResponse>(string json)
            where TRequest : IFishbowlRequest
            where TResponse : IFishbowlResponse<TRequest>, new()
        {
            try
            {
                if (JsonDocument.Parse(json).RootElement.TryGetProperty("FbiJson", out var fbiJson))
                {
                    // Get the Ticket section data
                    if (fbiJson.TryGetProperty("Ticket", out var ticketSection))
                    {
                        // Update the cached token/UserID
                        lock (_syncLock)
                        {
                            string tokenValue = ticketSection.GetProperty("Key").GetString();
                            _token = tokenValue == "null" ? null : tokenValue;

                            if (ticketSection.TryGetProperty("UserID", out var userIdElement))
                                _userId = userIdElement.TryGetInt32(out int userId) ? userId : (int?)null;
                        }
                    }

                    // Get the Result section data
                    if (fbiJson.TryGetProperty("FbiMsgsRs", out var resultSection))
                    {
                        // Get the result of the operation
                        int statusCode = resultSection.GetProperty("statusCode").GetInt32();

                        // Don't proceed if the session has ended
                        if (statusCode == 1164) return default;

                        // Attempt to deserialize the Response object if the request was processed by the server (StatusCode = 900 or 1000)
                        if ((statusCode == 900 || statusCode == 1000) && resultSection.TryGetProperty(new TResponse().ElementName, out JsonElement responseElement))
                        {
                            // Deserialize the Response object
                            TResponse response = DeserializeResponseProperty<TResponse>(responseElement);

                            // Return the Response object if the operation was successful
                            if (response.StatusCode == 900 || response.StatusCode == 1000)
                                return response;

                            // The operation failed--throw an appropriate exception
                            else
                                throw HandleFishbowlRequestFailure(statusCode, json, responseElement);
                        }
                        // The server could not process the request--throw an appropriate exception
                        else
                            throw HandleFishbowlRequestFailure(statusCode, json, resultSection);
                    }
                }

                throw new FishbowlInventoryOperationException("Unknown exception occurred.", -1, json);
            }
            catch (FishbowlInventoryException ex) { throw ex; }  // If the exception has already been processed/wrapped, return it
            catch (Exception ex) { throw new FishbowlInventoryServerException($"An {ex.GetType().Name} occurred while deserializing the response from the Fishbowl server:  {ex.Message}", ex, json); }  // Encapsulate any non-local exceptions
        }

        private TResponse DeserializeResponseProperty<TResponse>(JsonElement element)
        {
            try
            {
                return JsonSerializer.Deserialize<TResponse>(element.GetRawText(), _jsonOptions);
            }
            catch (JsonException ex)
            {
                string message = $"Unable to deserialize JSON to response property of type {typeof(TResponse).Name}.{Environment.NewLine}{element.GetRawText()}";
                throw new FishbowlInventoryOperationException(message, ex);
            }
        }

        private static Exception HandleFishbowlRequestFailure(int statusCode, string content, JsonElement responseElement)
        {
            // Code  Error
            // 900   Success! This API request is deprecated and could be removed in the future.
            // 1000  Success!

            string statusMessage = GetInnerStatusMessage(responseElement);

            // Throw the appropriate exception
            return statusCode switch
            {
                1001 => new FishbowlInventoryOperationException("Unknown message received." + statusMessage, statusCode, content),
                1002 => new FishbowlInventoryServerException("Connection to Fishbowl server was lost." + statusMessage, statusCode, content),
                1003 => new FishbowlInventoryOperationException("Some requests had errors." + statusMessage, statusCode, content),
                1004 => new FishbowlInventoryOperationException("There was an error with the database." + statusMessage, statusCode, content),
                1009 => new FishbowlInventoryServerException("Fishbowl server has been shut down." + statusMessage, statusCode, content),
                1010 => new FishbowlInventoryServerException("You have been logged off the server by an administrator." + statusMessage, statusCode, content),
                1011 => new FishbowlInventoryOperationException("Not found." + statusMessage, statusCode, content),
                1012 => new FishbowlInventoryOperationException("General error." + statusMessage, statusCode, content),
                1013 => new FishbowlInventoryOperationException("Dependencies need to be deleted." + statusMessage, statusCode, content),
                1014 => new FishbowlInventoryServerException("Unable to establish network connection." + statusMessage, statusCode, content),
                1015 => new FishbowlInventoryServerException("Your subscription date is greater than your server date." + statusMessage, statusCode, content),
                1016 => new FishbowlInventoryServerException("Incompatible database version." + statusMessage, statusCode, content),

                1100 => new FishbowlInventoryAuthenticationException("Unknown login error occurred." + statusMessage, statusCode, content),
                1109 => new FishbowlInventoryServerException("This integrated application registration key is already in use." + statusMessage, statusCode, content),
                1110 => new FishbowlInventoryServerException("A new integrated application has been added to Fishbowl. Please contact the Fishbowl administrator to approve this integrated application." + statusMessage, statusCode, content),
                1111 => new FishbowlInventoryServerException("This integrated application registration key does not match." + statusMessage, statusCode, content),
                1112 => new FishbowlInventoryServerException("This integrated application has not been approved by the Fishbowl administrator." + statusMessage, statusCode, content),
                1120 => new FishbowlInventoryAuthenticationException("Invalid username or password." + statusMessage, statusCode, content),
                1130 => new FishbowlInventoryAuthenticationException("Invalid ticket passed to Fishbowl server." + statusMessage, statusCode, content),
                1131 => new FishbowlInventoryAuthenticationException("Invalid ticket key passed to Fishbowl server." + statusMessage, statusCode, content),
                1140 => new FishbowlInventoryAuthenticationException("Initialization token is not correct type." + statusMessage, statusCode, content),
                1150 => new FishbowlInventoryOperationException("Request was invalid." + statusMessage, statusCode, content),
                1161 => new FishbowlInventoryOperationException("Response was invalid." + statusMessage, statusCode, content),
                1162 => new FishbowlInventoryServerException("The login limit has been reached for the server's key." + statusMessage, statusCode, content),
                1163 => new FishbowlInventoryServerException("Your API session has timed out." + statusMessage, statusCode, content),
                1164 => new FishbowlInventoryServerException("Your API session has been logged out." + statusMessage, statusCode, content),

                1200 => new FishbowlInventoryOperationException("Custom field is invalid." + statusMessage, statusCode, content),

                1300 => new FishbowlInventoryOperationException("Was not able to find the memo _________." + statusMessage, statusCode, content),
                1301 => new FishbowlInventoryOperationException("The memo was invalid." + statusMessage, statusCode, content),

                1400 => new FishbowlInventoryOperationException("Was not able to find the order history." + statusMessage, statusCode, content),
                1401 => new FishbowlInventoryOperationException("The order history was invalid." + statusMessage, statusCode, content),

                1500 => new FishbowlInventoryOperationException("The import was not properly formed." + statusMessage, statusCode, content),
                1501 => new FishbowlInventoryOperationException("That import type is not supported." + statusMessage, statusCode, content),
                1502 => new FishbowlInventoryOperationException("File not found." + statusMessage, statusCode, content),
                1503 => new FishbowlInventoryOperationException("That export type is not supported." + statusMessage, statusCode, content),
                1504 => new FishbowlInventoryOperationException("Unable to write to file." + statusMessage, statusCode, content),
                1505 => new FishbowlInventoryOperationException("The import data was of the wrong type." + statusMessage, statusCode, content),
                1506 => new FishbowlInventoryOperationException("Import requires a header." + statusMessage, statusCode, content),

                1600 => new FishbowlInventoryOperationException("Unable to load the user." + statusMessage, statusCode, content),
                1601 => new FishbowlInventoryOperationException("Unable to find the user." + statusMessage, statusCode, content),

                2000 => new FishbowlInventoryOperationException("Was not able to find the part _________." + statusMessage, statusCode, content),
                2001 => new FishbowlInventoryOperationException("The part was invalid." + statusMessage, statusCode, content),
                2002 => new FishbowlInventoryOperationException("Was not able to find a unique part." + statusMessage, statusCode, content),
                2003 => new FishbowlInventoryOperationException("BOM had an error on the part." + statusMessage, statusCode, content),

                2100 => new FishbowlInventoryOperationException("Was not able to find the product _________." + statusMessage, statusCode, content),
                2101 => new FishbowlInventoryOperationException("The product was invalid." + statusMessage, statusCode, content),
                2102 => new FishbowlInventoryOperationException("The product is not unique." + statusMessage, statusCode, content),
                2120 => new FishbowlInventoryOperationException("The kit item was invalid." + statusMessage, statusCode, content),
                2121 => new FishbowlInventoryOperationException("The associated product was invalid." + statusMessage, statusCode, content),

                2200 => new FishbowlInventoryOperationException("Yield failed." + statusMessage, statusCode, content),
                2201 => new FishbowlInventoryOperationException("Commit failed." + statusMessage, statusCode, content),
                2202 => new FishbowlInventoryOperationException("Add initial inventory failed." + statusMessage, statusCode, content),
                2203 => new FishbowlInventoryOperationException("Cannot adjust committed inventory." + statusMessage, statusCode, content),
                2204 => new FishbowlInventoryOperationException("Invalid quantity." + statusMessage, statusCode, content),
                2205 => new FishbowlInventoryOperationException("Quantity must be greater than zero." + statusMessage, statusCode, content),
                2206 => new FishbowlInventoryOperationException("Serial number _________ already committed." + statusMessage, statusCode, content),
                2207 => new FishbowlInventoryOperationException("Part _________ is not an inventory part." + statusMessage, statusCode, content),
                2208 => new FishbowlInventoryOperationException("Not enough available quantity in _________." + statusMessage, statusCode, content),
                2209 => new FishbowlInventoryOperationException("Move failed." + statusMessage, statusCode, content),
                2210 => new FishbowlInventoryOperationException("Cycle count failed." + statusMessage, statusCode, content),

                2300 => new FishbowlInventoryOperationException("Was not able to find the tag number _________." + statusMessage, statusCode, content),
                2301 => new FishbowlInventoryOperationException("The tag was invalid." + statusMessage, statusCode, content),
                2302 => new FishbowlInventoryOperationException("The tag move failed." + statusMessage, statusCode, content),
                2303 => new FishbowlInventoryOperationException("Was not able to save tag number _________." + statusMessage, statusCode, content),
                2304 => new FishbowlInventoryOperationException("Not enough available inventory in tag number _________." + statusMessage, statusCode, content),
                2305 => new FishbowlInventoryOperationException("Tag number _________ is a location." + statusMessage, statusCode, content),

                2400 => new FishbowlInventoryOperationException("Invalid UOM." + statusMessage, statusCode, content),
                2401 => new FishbowlInventoryOperationException("UOM _________ not found." + statusMessage, statusCode, content),
                2402 => new FishbowlInventoryOperationException("Integer UOM _________ cannot have non-integer quantity." + statusMessage, statusCode, content),
                2403 => new FishbowlInventoryOperationException("The UOM is not compatible with the part's base UOM." + statusMessage, statusCode, content),
                2404 => new FishbowlInventoryOperationException("Cannot convert to the requested UOM." + statusMessage, statusCode, content),
                2405 => new FishbowlInventoryOperationException("Cannot convert to the requested UOM." + statusMessage, statusCode, content),
                2406 => new FishbowlInventoryOperationException("The quantity must be a whole number." + statusMessage, statusCode, content),
                2407 => new FishbowlInventoryOperationException("The UOM conversion for the quantity must be a whole number." + statusMessage, statusCode, content),

                2500 => new FishbowlInventoryOperationException("The tracking is not valid." + statusMessage, statusCode, content),
                2501 => new FishbowlInventoryOperationException("Part tracking not found." + statusMessage, statusCode, content),
                2502 => new FishbowlInventoryOperationException("The part tracking name is required." + statusMessage, statusCode, content),
                2503 => new FishbowlInventoryOperationException("The part tracking name _________ is already in use." + statusMessage, statusCode, content),
                2504 => new FishbowlInventoryOperationException("The part tracking abbreviation is required." + statusMessage, statusCode, content),
                2505 => new FishbowlInventoryOperationException("The part tracking abbreviation _________ is already in use." + statusMessage, statusCode, content),
                2506 => new FishbowlInventoryOperationException("The part tracking _________ is in use or was used and cannot be deleted." + statusMessage, statusCode, content),
                2510 => new FishbowlInventoryOperationException("Serial number is missing." + statusMessage, statusCode, content),
                2511 => new FishbowlInventoryOperationException("Serial number is null." + statusMessage, statusCode, content),
                2512 => new FishbowlInventoryOperationException("Duplicate serial number." + statusMessage, statusCode, content),
                2513 => new FishbowlInventoryOperationException("The serial number is not valid." + statusMessage, statusCode, content),
                2514 => new FishbowlInventoryOperationException("Tracking is not equal." + statusMessage, statusCode, content),
                2515 => new FishbowlInventoryOperationException("The tracking _________ was not found in location _________' or is committed to another order." + statusMessage, statusCode, content),
                
                2600 => new FishbowlInventoryOperationException("Location _________ not found." + statusMessage, statusCode, content),
                2601 => new FishbowlInventoryOperationException("Invalid location." + statusMessage, statusCode, content),
                2602 => new FishbowlInventoryOperationException("Location group _________ not found." + statusMessage, statusCode, content),
                2603 => new FishbowlInventoryOperationException("Default customer not specified for location _________." + statusMessage, statusCode, content),
                2604 => new FishbowlInventoryOperationException("Default vendor not specified for location _________." + statusMessage, statusCode, content),
                2605 => new FishbowlInventoryOperationException("Default location for part _________ not found." + statusMessage, statusCode, content),
                2606 => new FishbowlInventoryOperationException("_________ is not a pickable location." + statusMessage, statusCode, content),
                2607 => new FishbowlInventoryOperationException("_________ is not a receivable location." + statusMessage, statusCode, content),
                
                2700 => new FishbowlInventoryOperationException("Location group not found." + statusMessage, statusCode, content),
                2701 => new FishbowlInventoryOperationException("Invalid location group." + statusMessage, statusCode, content),
                2702 => new FishbowlInventoryOperationException("User does not have access to location group _________." + statusMessage, statusCode, content),
                
                3000 => new FishbowlInventoryOperationException("Customer _________ not found." + statusMessage, statusCode, content),
                3001 => new FishbowlInventoryOperationException("Customer is invalid." + statusMessage, statusCode, content),
                3002 => new FishbowlInventoryOperationException("Customer _________ must have a default main office address." + statusMessage, statusCode, content),
                
                3100 => new FishbowlInventoryOperationException("Vendor _________ not found." + statusMessage, statusCode, content),
                3101 => new FishbowlInventoryOperationException("Vendor is invalid." + statusMessage, statusCode, content),
                
                3300 => new FishbowlInventoryOperationException("Address not found" + statusMessage, statusCode, content),
                3301 => new FishbowlInventoryOperationException("Invalid address" + statusMessage, statusCode, content),
                
                4000 => new FishbowlInventoryOperationException("There was an error loading PO _________." + statusMessage, statusCode, content),
                4001 => new FishbowlInventoryOperationException("Unknown status _________." + statusMessage, statusCode, content),
                4002 => new FishbowlInventoryOperationException("Unknown carrier _________." + statusMessage, statusCode, content),
                4003 => new FishbowlInventoryOperationException("Unknown QuickBooks class _________." + statusMessage, statusCode, content),
                4004 => new FishbowlInventoryOperationException("PO does not have a PO number. Please turn on the auto-assign PO number option in the purchase order module options." + statusMessage, statusCode, content),
                4005 => new FishbowlInventoryOperationException("Duplicate order number _________." + statusMessage, statusCode, content),
                4006 => new FishbowlInventoryOperationException("Cannot create PO with configurable parts: _________." + statusMessage, statusCode, content),
                4007 => new FishbowlInventoryOperationException("The following parts were not added to the purchase order. They have no default vendor:" + statusMessage, statusCode, content),
                4008 => new FishbowlInventoryOperationException("Unknown type _________." + statusMessage, statusCode, content),
                
                4100 => new FishbowlInventoryOperationException("There was an error loading SO _________." + statusMessage, statusCode, content),
                4101 => new FishbowlInventoryOperationException("Unknown salesman _________." + statusMessage, statusCode, content),
                4102 => new FishbowlInventoryOperationException("Unknown tax rate _________." + statusMessage, statusCode, content),
                4103 => new FishbowlInventoryOperationException("Cannot create SO with configurable parts: _________." + statusMessage, statusCode, content),
                4104 => new FishbowlInventoryOperationException("The sales order item is invalid: _________." + statusMessage, statusCode, content),
                4105 => new FishbowlInventoryOperationException("SO does not have a SO number. Please turn on the auto-assign SO numbers option in the sales order module options." + statusMessage, statusCode, content),
                4106 => new FishbowlInventoryOperationException("Cannot create SO with kit products" + statusMessage, statusCode, content),
                4107 => new FishbowlInventoryOperationException("A kit item must follow a kit header." + statusMessage, statusCode, content),
                4108 => new FishbowlInventoryOperationException("Sales order cannot be found." + statusMessage, statusCode, content),
                
                4200 => new FishbowlInventoryOperationException("There was an error loading BOM _________." + statusMessage, statusCode, content),
                4201 => new FishbowlInventoryOperationException("Bill of materials cannot be found." + statusMessage, statusCode, content),
                4202 => new FishbowlInventoryOperationException("Duplicate BOM number _________." + statusMessage, statusCode, content),
                4203 => new FishbowlInventoryOperationException("The bill of materials is not up to date and must be reloaded." + statusMessage, statusCode, content),
                4204 => new FishbowlInventoryOperationException("Bill of materials was not saved." + statusMessage, statusCode, content),
                4205 => new FishbowlInventoryOperationException("Bill of materials is in use and cannot be deleted" + statusMessage, statusCode, content),
                4206 => new FishbowlInventoryOperationException("requires a raw good and a finished good, or a repair." + statusMessage, statusCode, content),
                4207 => new FishbowlInventoryOperationException("This change would make this a recursive bill of materials." + statusMessage, statusCode, content),
                4210 => new FishbowlInventoryOperationException("There was an error loading MO _________." + statusMessage, statusCode, content),
                4211 => new FishbowlInventoryOperationException("Manufacture order cannot be found." + statusMessage, statusCode, content),
                4212 => new FishbowlInventoryOperationException("No manufacture order was created. Duplicate order number _________." + statusMessage, statusCode, content),
                4213 => new FishbowlInventoryOperationException("The manufacture order is not up to date and must be reloaded." + statusMessage, statusCode, content),
                4214 => new FishbowlInventoryOperationException("Manufacture order was not saved." + statusMessage, statusCode, content),
                4215 => new FishbowlInventoryOperationException("Manufacture order is closed and cannot be modified." + statusMessage, statusCode, content),
                4220 => new FishbowlInventoryOperationException("There was an error loading WO _________." + statusMessage, statusCode, content),
                4221 => new FishbowlInventoryOperationException("Work order cannot be found." + statusMessage, statusCode, content),
                4222 => new FishbowlInventoryOperationException("Duplicate work order number _________." + statusMessage, statusCode, content),
                4223 => new FishbowlInventoryOperationException("The work order is not up to date and must be reloaded." + statusMessage, statusCode, content),
                4224 => new FishbowlInventoryOperationException("Work order was not saved." + statusMessage, statusCode, content),
                
                4300 => new FishbowlInventoryOperationException("There was an error loading TO _________." + statusMessage, statusCode, content),
                4301 => new FishbowlInventoryOperationException("Unknown status _________." + statusMessage, statusCode, content),
                4302 => new FishbowlInventoryOperationException("Unknown carrier _________." + statusMessage, statusCode, content),
                4303 => new FishbowlInventoryOperationException("Transfer order cannot be found." + statusMessage, statusCode, content),
                4304 => new FishbowlInventoryOperationException("TO does not have a TO number. Please turn on the auto-assign TO number option in the Transfer Order module options." + statusMessage, statusCode, content),
                4305 => new FishbowlInventoryOperationException("Duplicate order number _________." + statusMessage, statusCode, content),
                4306 => new FishbowlInventoryOperationException("Unknown type _________." + statusMessage, statusCode, content),
                4307 => new FishbowlInventoryOperationException("Transfer order was not saved." + statusMessage, statusCode, content),
                4308 => new FishbowlInventoryOperationException("The transfer order is not up to date and must be reloaded." + statusMessage, statusCode, content),
                
                5000 => new FishbowlInventoryOperationException("There was a receiving error." + statusMessage, statusCode, content),
                5001 => new FishbowlInventoryOperationException("Receive ticket invalid." + statusMessage, statusCode, content),
                5002 => new FishbowlInventoryOperationException("Could not find a line item for part number _________." + statusMessage, statusCode, content),
                5003 => new FishbowlInventoryOperationException("Could not find a line item for product number _________." + statusMessage, statusCode, content),
                5004 => new FishbowlInventoryOperationException("Not a valid receive type." + statusMessage, statusCode, content),
                5005 => new FishbowlInventoryOperationException("The receipt is not up to date and must be reloaded." + statusMessage, statusCode, content),
                5006 => new FishbowlInventoryOperationException("A location is required to receive this part. Part num: _________" + statusMessage, statusCode, content),
                5007 => new FishbowlInventoryOperationException("Cannot receive or reconcile more than the quantity ordered on a TO." + statusMessage, statusCode, content),
                5008 => new FishbowlInventoryOperationException("Receipt not found _________." + statusMessage, statusCode, content),
                
                5100 => new FishbowlInventoryOperationException("Pick invalid" + statusMessage, statusCode, content),
                5101 => new FishbowlInventoryOperationException("Pick not found _________." + statusMessage, statusCode, content),
                5102 => new FishbowlInventoryOperationException("Pick not saved." + statusMessage, statusCode, content),
                5103 => new FishbowlInventoryOperationException("An order on pick _________ has a problem." + statusMessage, statusCode, content),
                5104 => new FishbowlInventoryOperationException("Pick item not found _________." + statusMessage, statusCode, content),
                5105 => new FishbowlInventoryOperationException("Could not finalize pick. Quantity is not correct." + statusMessage, statusCode, content),
                5106 => new FishbowlInventoryOperationException("The pick is not up to date and must be reloaded." + statusMessage, statusCode, content),
                5107 => new FishbowlInventoryOperationException("The part in tag _________ does not match part _________." + statusMessage, statusCode, content),
                5108 => new FishbowlInventoryOperationException("Incorrect slot for this item. Item must be placed with others for this order." + statusMessage, statusCode, content),
                5109 => new FishbowlInventoryOperationException("Wrong number of serial numbers sent for pick." + statusMessage, statusCode, content),
                5110 => new FishbowlInventoryOperationException("Pick items must be started to assign tag." + statusMessage, statusCode, content),
                5111 => new FishbowlInventoryOperationException("Order must be picked from location group _________." + statusMessage, statusCode, content),
                5112 => new FishbowlInventoryOperationException("The item must be picked from _________." + statusMessage, statusCode, content),
                
                5200 => new FishbowlInventoryOperationException("Shipment invalid" + statusMessage, statusCode, content),
                5201 => new FishbowlInventoryOperationException("Shipment not found _________." + statusMessage, statusCode, content),
                5202 => new FishbowlInventoryOperationException("Shipment status error" + statusMessage, statusCode, content),
                5203 => new FishbowlInventoryOperationException("Unable to process shipment." + statusMessage, statusCode, content),
                5204 => new FishbowlInventoryOperationException("Carrier not found _________." + statusMessage, statusCode, content),
                5205 => new FishbowlInventoryOperationException("The shipment _________ has already been shipped." + statusMessage, statusCode, content),
                5206 => new FishbowlInventoryOperationException("Cannot ship order _________. The customer has a ship hold." + statusMessage, statusCode, content),
                5207 => new FishbowlInventoryOperationException("Cannot ship order _________. The vendor has a ship hold." + statusMessage, statusCode, content),
                
                5300 => new FishbowlInventoryOperationException("Could not load RMA." + statusMessage, statusCode, content),
                5301 => new FishbowlInventoryOperationException("Could not find RMA." + statusMessage, statusCode, content),
                
                5400 => new FishbowlInventoryOperationException("Could not take payment." + statusMessage, statusCode, content),
                
                5500 => new FishbowlInventoryOperationException("Could not load the calendar." + statusMessage, statusCode, content),
                5501 => new FishbowlInventoryOperationException("Could not find the calendar." + statusMessage, statusCode, content),
                5502 => new FishbowlInventoryOperationException("Could not save the calendar." + statusMessage, statusCode, content),
                5503 => new FishbowlInventoryOperationException("Could not delete the calendar." + statusMessage, statusCode, content),
                5504 => new FishbowlInventoryOperationException("Could not find the calendar activity." + statusMessage, statusCode, content),
                5505 => new FishbowlInventoryOperationException("Could not save the calendar activity." + statusMessage, statusCode, content),
                5506 => new FishbowlInventoryOperationException("Could not delete the calendar activity." + statusMessage, statusCode, content),
                5507 => new FishbowlInventoryOperationException("The start date must be before the stop date." + statusMessage, statusCode, content),
                
                6000 => new FishbowlInventoryOperationException("Account invalid" + statusMessage, statusCode, content),
                6001 => new FishbowlInventoryOperationException("Discount invalid" + statusMessage, statusCode, content),
                6002 => new FishbowlInventoryOperationException("Tax rate invalid" + statusMessage, statusCode, content),
                6003 => new FishbowlInventoryOperationException("Accounting connection failed" + statusMessage, statusCode, content),
                6005 => new FishbowlInventoryOperationException("Accounting system not defined" + statusMessage, statusCode, content),
                6006 => new FishbowlInventoryOperationException("Accounting brought back a null result" + statusMessage, statusCode, content),
                6007 => new FishbowlInventoryOperationException("Accounting synchronization error" + statusMessage, statusCode, content),
                6008 => new FishbowlInventoryOperationException("The export failed" + statusMessage, statusCode, content),
                6009 => new FishbowlInventoryOperationException("Fishbowl and Quickbooks multiple currency features don't match" + statusMessage, statusCode, content),
                6010 => new FishbowlInventoryOperationException("The data validation for the export has failed." + statusMessage, statusCode, content),
                6011 => new FishbowlInventoryOperationException("Accounting integration is not configured. Please reintegrate." + statusMessage, statusCode, content),
                
                6100 => new FishbowlInventoryOperationException("Class already exists" + statusMessage, statusCode, content),
                
                7000 => new FishbowlInventoryOperationException("Pricing rule error" + statusMessage, statusCode, content),
                7001 => new FishbowlInventoryOperationException("Pricing rule not found" + statusMessage, statusCode, content),
                7002 => new FishbowlInventoryOperationException("The pricing rule name is not unique" + statusMessage, statusCode, content),
                
                8000 => new FishbowlInventoryOperationException("Unknown FOB _________." + statusMessage, statusCode, content),

                _ => new FishbowlInventoryOperationException("Unknown exception occurred." + statusMessage, statusCode, content)
            };
        }

        private static string GetInnerStatusMessage(JsonElement element)
        {
            try
            {
                return element.TryGetProperty("statusMessage", out JsonElement statusMessage) ? "  " + statusMessage.GetString() : string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }

        #endregion API Helper Methods

        #region DataTable Helper Methods

        private static DateTime? NullableDateTryParse(string text) => DateTime.TryParse(text, out var date) ? date : (DateTime?)null;

        #endregion DataTable Helper Methods

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                // Terminate the session (if logged in)
                if (!string.IsNullOrWhiteSpace(Token))
                    Task.Run(() => LogoutAsync()).GetAwaiter().GetResult();

                // Dispose managed objects
                // ...
            }

            // Dispose unmanaged objects
            // ...

            // Set the Disposed flag to TRUE
            _disposed = true;
        }

        #endregion IDisposable Members
    }
}