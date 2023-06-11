using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FishbowlInventory.Serialization;
using System.Threading.Tasks;

namespace FishbowlInventory.Models
{
	/// <summary>
	/// Stores sales order information.
	/// </summary>
	public class SalesOrder
	{
		[CsvPropertyName("id")]
		public int Id { get; set; }

		[CsvPropertyName("note")]
		public string Note { get; set; }

		[CsvPropertyName("totalPrice")]
		public double TotalPrice { get; set; }

		[CsvPropertyName("totalTax")]
		public double TotalTax { get; set; }

		[CsvPropertyName("paymentTotal")]
		public double PaymentTotal { get; set; }

		[CsvPropertyName("itemTotal")]
		public double ItemTotal { get; set; }

		[CsvPropertyName("salesman")]
		public string Salesman { get; set; }

		[CsvPropertyName("number")]
		public string Number { get; set; }

		[CsvPropertyName("status")]
		public int Status { get; set; }

		[CsvPropertyName("carrier")]
		public string Carrier { get; set; }

		[CsvPropertyName("firstShipDate")]
		public DateTime? FirstShipDate { get; set; }

		[CsvPropertyName("createdDate")]
		public DateTime? CreatedDate { get; set; }

		[CsvPropertyName("issuedDate")]
		public DateTime? IssuedDate { get; set; }

		[CsvPropertyName("taxRatePercentage")]
		public double TaxRatePercentage { get; set; }

		[CsvPropertyName("taxRateName")]
		public string TaxRateName { get; set; }

		[CsvPropertyName("shippingCost")]
		public double ShippingCost { get; set; }

		[CsvPropertyName("shippingTerms")]
		public string ShippingTerms { get; set; }

		[CsvPropertyName("paymentTerms")]
		public string PaymentTerms { get; set; }

		[CsvPropertyName("customerContact")]
		public string CustomerContact { get; set; }

		[CsvPropertyName("customerName")]
		public string CustomerName { get; set; }

		[CsvPropertyName("customerID")]
		public int CustomerId { get; set; }

		[CsvPropertyName("fob")]
		public string FOB { get; set; }

		[CsvPropertyName("quickBooksClassName")]
		public string QuickBooksClassName { get; set; }

		[CsvPropertyName("locationGroup")]
		public string LocationGroup { get; set; }

		[CsvPropertyName("priorityId")]
		public int PriorityId { get; set; }

		[CsvPropertyName("currencyRate")]
		public double CurrencyRate { get; set; }

		[CsvPropertyName("currencyName")]
		public string CurrencyName { get; set; }

		[CsvPropertyName("priceIsInHomeCurrency")]
		public bool IsPriceInHomeCurrency { get; set; }

		[CsvPropertyName("billTo")]
		public SalesOrderContact BillTo { get; set; }

		[CsvPropertyName("ship")]
		public SalesOrderContact ShipTo { get; set; }

		[CsvPropertyName("issueFlag")]
		public bool IsIssued { get; set; }

		[CsvPropertyName("vendorPO")]
		public string VendorPurchaseOrder { get; set; }

		[CsvPropertyName("customerPO")]
		public string CustomerPurchaseOrder { get; set; }

		[CsvPropertyName("upsServiceID")]
		public int UPSServiceId { get; set; }

		[CsvPropertyName("totalIncludesTax")]
		public bool TotalIncludesTax { get; set; }

		[CsvPropertyName("typeID")]
		public int TypeId { get; set; }

		[CsvPropertyName("url")]
		public string URL { get; set; }

		[CsvPropertyName("cost")]
		public double Cost { get; set; }

		[CsvPropertyName("dateCompleted")]
		public DateTime? DateCompleted { get; set; }

		[CsvPropertyName("dateLastModified")]
		public DateTime? DateLastModified { get; set; }

		[CsvPropertyName("dateRevision")]
		public DateTime? DateRevision { get; set; }

		[CsvPropertyName("registerID")]
		public int RegisterId { get; set; }

		[CsvPropertyName("residentialFlag")]
		public bool IsResidential { get; set; }

		[CsvPropertyName("salesmanInitials")]
		public string SalesmanInitials { get; set; }

		[CsvPropertyName("customFields")]
		public CustomField[] CustomFields { get; set; }

		[CsvPropertyName("memos")]
		public Memo[] Memos { get; set; }

		[CsvPropertyName("items")]
		public SalesOrderItem[] Items { get; set; }
	}
}
