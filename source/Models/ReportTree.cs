using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FishbowlInventory.Serialization;
using System.Threading.Tasks;

namespace FishbowlInventory.Models
{
    /// <summary>
    /// Contains the details about a report tree.
    /// </summary>
    public class ReportTree
    {
        [CsvPropertyName("id")]
        public int Id { get; set; }

        [CsvPropertyName("name")]
        public string Name { get; set; }

        [CsvPropertyName("parentID")]
        public int ParentId { get; set; }

        [CsvPropertyName("readOnly")]
        public bool IsReadOnly { get; set; }

        [CsvPropertyName("userID")]
        public int UserId { get; set; }
    }
}
