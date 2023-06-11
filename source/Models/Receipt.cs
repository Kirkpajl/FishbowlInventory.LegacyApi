using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FishbowlInventory.Serialization;
using System.Threading.Tasks;

namespace FishbowlInventory.Models
{
    /// <summary>
    /// Contains receipt information.
    /// </summary>
    public class Receipt
    {
        [CsvPropertyName("id")]
        public int Id { get; set; }

        [CsvPropertyName("statusID")]
        public int StatusId { get; set; }

        [CsvPropertyName("typeID")]
        public int TypeId { get; set; }

        [CsvPropertyName("orderTypeID")]
        public int OrderTypeId { get; set; }

        [CsvPropertyName("soID")]
        public int SalesOrderId { get; set; }

        [CsvPropertyName("poID")]
        public int PurchaseOrderId { get; set; }

        [CsvPropertyName("xoID")]
        public int TransferOrderId { get; set; }

        [CsvPropertyName("userID")]
        public int UserId { get; set; }

        [CsvPropertyName("locationGroupID")]
        public int LocationGroupId { get; set; }

        [CsvPropertyName("receiptItems")]
        public ReceiveItem[] ReceiptItems { get; set; }
    }
}
