using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FishbowlInventory.Serialization;
using System.Threading.Tasks;

namespace FishbowlInventory.Models
{
    public class PurchaseOrderContact
    {
        [CsvPropertyName("name")]
        public string Name { get; set; }

        [CsvPropertyName("addressField")]
        public string Address { get; set; }

        [CsvPropertyName("city")]
        public string City { get; set; }

        [CsvPropertyName("state")]
        public string State { get; set; }

        [CsvPropertyName("zip")]
        public string Zip { get; set; }

        [CsvPropertyName("country")]
        public string Country { get; set; }
    }
}
