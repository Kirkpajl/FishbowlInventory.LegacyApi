using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FishbowlInventory.Serialization;
using System.Threading.Tasks;

namespace FishbowlInventory.Models
{
    /// <summary>
    /// This is a storage object for Order History.
    /// </summary>
    public class OrderHistory
    {
        [CsvPropertyName("id")]
        public int Id { get; set; }

        [CsvPropertyName("userName")]
        public string UserName { get; set; }

        [CsvPropertyName("dateCreated")]
        public DateTime DateCreated { get; set; }

        [CsvPropertyName("comment")]
        public string Comment { get; set; }

        [CsvPropertyName("tableID")]
        public int TableId { get; set; }

        [CsvPropertyName("recordID")]
        public int RecordId { get; set; }
    }
}
