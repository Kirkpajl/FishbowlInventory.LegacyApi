using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FishbowlInventory.Serialization;
using System.Threading.Tasks;

namespace FishbowlInventory.Models
{
    public class UserModificationEvent
    {
        [CsvPropertyName("username")]
        public string Username { get; set; }

        [CsvPropertyName("dateLastModified")]
        public DateTime DateLastModified { get; set; }
    }
}
