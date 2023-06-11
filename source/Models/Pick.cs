using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FishbowlInventory.Serialization;
using System.Threading.Tasks;

namespace FishbowlInventory.Models
{
    /// <summary>
    /// Stores all the information associated with a pick.
    /// </summary>
    public class Pick
    {
        [CsvPropertyName("pickID")]
        public int Id { get; set; }

        [CsvPropertyName("number")]
        public string Number { get; set; }

        [CsvPropertyName("type")]
        public string Type { get; set; }

        [CsvPropertyName("typeID")]
        public int TypeId { get; set; }

        [CsvPropertyName("status")]
        public string Status { get; set; }

        [CsvPropertyName("statusID")]
        public int StatusId { get; set; }

        [CsvPropertyName("priority")]
        public string Priority { get; set; }

        [CsvPropertyName("priorityID")]
        public int PriorityId { get; set; }

        [CsvPropertyName("locationGroupID")]
        public int LocationGroupId { get; set; }

        [CsvPropertyName("dateLastModified")]
        public DateTime? DateLastModified { get; set; }

        [CsvPropertyName("dateScheduled")]
        public DateTime? DateScheduled { get; set; }

        [CsvPropertyName("dateCreated")]
        public DateTime? DateCreated { get; set; }

        [CsvPropertyName("dateStarted")]
        public DateTime? DateStarted { get; set; }

        [CsvPropertyName("dateFinished")]
        public DateTime? DateFinished { get; set; }

        [CsvPropertyName("userName")]
        public string UserName { get; set; }

        [CsvPropertyName("pickOrders")]
        public PickOrder[] PickOrders { get; set; }

        [CsvPropertyName("pickItems")]
        public PickItem[] PickItems { get; set; }
    }
}
