using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FishbowlInventory.Serialization;
using System.Threading.Tasks;

namespace FishbowlInventory.Models
{
	/// <summary>
	/// Contains all the vendor information and details.
	/// </summary>
	public class Vendor
	{
		/// <summary>
		/// The unique identifier.
		/// </summary>
		[CsvPropertyName("id")]
		public int Id { get; set; }

		/// <summary>
		/// The vendor name.
		/// </summary>
		[CsvPropertyName("name")]
		public string Name { get; set; }

		/// <summary>
		/// The vendor account number.
		/// </summary>
		[CsvPropertyName("accountNumber")]
		public string AccountNumber { get; set; }

		/// <summary>
		/// Indicates if the vendor is active.
		/// </summary>
		[CsvPropertyName("active")]
		public bool IsActive { get; set; }

        /// <summary>
        /// A list of the vendor's custom fields.
        /// </summary>
        [CsvPropertyName("customFields")]
        public ICollection<CustomField> CustomFields { get; } = new HashSet<CustomField>();

        [CsvPropertyName("status")]
        public string Status { get; set; }

        [CsvPropertyName("defPaymentTerms")]
        public string DefaultPaymentTerms { get; set; }

        [CsvPropertyName("defCarrier")]
        public string DefaultCarrier { get; set; }

        [CsvPropertyName("defCarrierService")]
        public string DefaultCarrierService { get; set; }

        [CsvPropertyName("defShipTerms")]
        public string DefaultShippingTerms { get; set; }

        [CsvPropertyName("taxRate")]
        public string TaxRate { get; set; }

        [CsvPropertyName("dateCreated")]
        public DateTime? DateCreated { get; set; }

        [CsvPropertyName("dateModified")]
        public DateTime? DateModified { get; set; }

        [CsvPropertyName("lastChangedUser")]
        public string LastChangedUser { get; set; }

        [CsvPropertyName("creditLimit")]
        public decimal CreditLimit { get; set; }

        [CsvPropertyName("minOrderAmount")]
        public decimal MinimumOrderAmount { get; set; }

        [CsvPropertyName("note")]
        public string Note { get; set; }

        [CsvPropertyName("url")]
        public string Url { get; set; }

        [CsvPropertyName("sysUserID")]
        public int SystemUserId { get; set; }

        [CsvPropertyName("accountingID")]
        public int AccountingId { get; set; }

        [CsvPropertyName("accountingHash")]
        public string AccountingHash { get; set; }

        [CsvPropertyName("currencyName")]
        public string CurrencyName { get; set; }

        [CsvPropertyName("currencyRate")]
        public decimal CurrencyRate { get; set; }

        [CsvPropertyName("leadTime")]
        public int LeadTime { get; set; }

        [CsvPropertyName("addresses")]
        public ICollection<Address> Addresses { get; } = new HashSet<Address>();
    }
}
