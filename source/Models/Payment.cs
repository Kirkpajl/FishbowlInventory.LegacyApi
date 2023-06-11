using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FishbowlInventory.Serialization;
using System.Threading.Tasks;

namespace FishbowlInventory.Models
{
    /// <summary>
    /// Contains payment information.
    /// </summary>
    public class Payment
    {
        [CsvPropertyName("amount")]
        public double Amount { get; set; }

        [CsvPropertyName("salesOrderNumber")]
        public string SalesOrderNumber { get; set; }

        [CsvPropertyName("currencyRate")]
        public double CurrencyRate { get; set; }

        [CsvPropertyName("paymentDate")]
        public DateTime? PaymentDate { get; set; }

        [CsvPropertyName("paymentMethod")]
        public string PaymentMethod { get; set; }

        [CsvPropertyName("reference")]
        public string Reference { get; set; }

        [CsvPropertyName("confirmation")]
        public string Confirmation { get; set; }

        [CsvPropertyName("expirationDate")]
        public DateTime? ExpirationDate { get; set; }

        [CsvPropertyName("depositAccountName")]
        public string DepositAccountName { get; set; }

        [CsvPropertyName("transactionID")]
        public string TransactionId { get; set; }

        [CsvPropertyName("authorizationCode")]
        public string AuthorizationCode { get; set; }

        [CsvPropertyName("merchantAccount")]
        public string MerchantAccount { get; set; }

        [CsvPropertyName("miscCreditCard")]
        public string MiscellanousCreditCard { get; set; }

        [CsvPropertyName("creditCard")]
        public CreditCard CreditCard { get; set; }
    }
}
