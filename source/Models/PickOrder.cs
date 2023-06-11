using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FishbowlInventory.Serialization;
using System.Threading.Tasks;

namespace FishbowlInventory.Models
{
    public class PickOrder
    {
        [CsvPropertyName("orderType")]
        public string Type { get; set; }

        [CsvPropertyName("orderTypeID")]
        public int TypeId { get; set; }

        [CsvPropertyName("orderNum")]
        public string Number { get; set; }

        [CsvPropertyName("orderID")]
        public int Id { get; set; }

        [CsvPropertyName("orderTo")]
        public string To { get; set; }

        [CsvPropertyName("note")]
        public string Note { get; set; }
    }
}
