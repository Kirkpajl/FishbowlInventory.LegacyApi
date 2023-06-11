using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FishbowlInventory.Serialization;
using System.Threading.Tasks;

namespace FishbowlInventory.Models
{
    internal class FishbowlErrorResponse
    {
        [CsvPropertyName("timeStamp")]
        public DateTime TimeStamp { get; set; }

        [CsvPropertyName("status")]
        public int Status { get; set; }

        [CsvPropertyName("message")]
        public string Message { get; set; }

        [CsvPropertyName("path")]
        public string Path { get; set; }



        //{"timeStamp":"2022-10-21T15:40:18.531-0400","status":401,"message":"Invalid authorization token.","path":"/api/integrations/plugin-info"}
    }
}
