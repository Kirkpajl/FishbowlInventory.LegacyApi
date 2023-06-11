using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FishbowlInventory.Serialization;
using System.Threading.Tasks;

namespace FishbowlInventory.Models
{
    public class TempAccount
    {
        [CsvPropertyName("id")]
        public int Id { get; set; }

        [CsvPropertyName("type")]
        public int Type { get; set; }
    }
}
