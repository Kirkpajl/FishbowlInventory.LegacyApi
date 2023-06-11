using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using FishbowlInventory.Serialization;
using System.Threading.Tasks;

namespace FishbowlInventory.Models
{
    /// <summary>
    /// Stores the details about a Customer. You must include the JobDepth field. There are no other 
    /// fields that are essential for the use of a Customer object. Use only those fields that you need.
    /// </summary>
    public class Customer
    {
        [CsvPropertyName("customerID")]
        public int CustomerId { get; set; }

        [CsvPropertyName("accountID")]
        public int AccountId { get; set; }

        [CsvPropertyName("defPaymentTerms")]
        public string DefaultPaymentTerms { get; set; }

        [CsvPropertyName("defShipTerms")]
        public string DefaultShipTerms { get; set; }

        [CsvPropertyName("taxRate")]
        public string TaxRate { get; set; }

        [CsvPropertyName("name")]
        public string Name { get; set; }

        [CsvPropertyName("number")]
        public string Number { get; set; }

        [CsvPropertyName("dateCreated")]
        public DateTime DateCreated { get; set; }

        [CsvPropertyName("dateLastModified")]
        public DateTime DateLastModified { get; set; }

        [CsvPropertyName("lastChangedUser")]
        public string LastChangedUser { get; set; }

        [CsvPropertyName("creditLimit")]
        public decimal CreditLimit { get; set; }

        [CsvPropertyName("taxExempt")]
        public bool IsTaxExempt { get; set; }

        [CsvPropertyName("taxExemptNumber")]
        public string TaxExemptNumber { get; set; }

        [CsvPropertyName("note")]
        public string Note { get; set; }

        [CsvPropertyName("activeFlag")]
        public bool IsActive { get; set; }

        [CsvPropertyName("accountingID")]
        public string AccountingId { get; set; }

        [CsvPropertyName("currencyName")]
        public string CurrencyName { get; set; }

        [CsvPropertyName("currencyRate")]
        public double CurrencyRate { get; set; }

        [CsvPropertyName("defaultSalesman")]
        public string DefaultSalesman { get; set; }

        [CsvPropertyName("defaultCarrier")]
        public string DefaultCarrier { get; set; }

        [CsvPropertyName("defaultShipService")]
        public string DefaultShipService { get; set; }

        [CsvPropertyName("jobDepth"), Required]
        public int JobDepth { get; set; }

        [CsvPropertyName("quickBooksClassName")]
        public string QuickBooksClassName { get; set; }

        [CsvPropertyName("parentID")]
        public int ParentID { get; set; }

        [CsvPropertyName("pipelineAccount")]
        public int PipelineAccount { get; set; }

        [CsvPropertyName("URL")]
        public string URL { get; set; }

        [CsvPropertyName("addresses")]
        public Address[] Addresses { get; set; }

        [CsvPropertyName("customFields")]
        public CustomField[] CustomFields { get; set; }
    }
}
