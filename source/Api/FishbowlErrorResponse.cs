using FishbowlInventory.Serialization;
using System;

namespace FishbowlInventory.Api
{
    class FishbowlErrorResponse
    {
        [CsvPropertyName("timeStamp")]
        public DateTime TimeStamp { get; set; }

        [CsvPropertyName("status")]
        public int StatusCode { get; set; }

        [CsvPropertyName("message")]
        public string Message { get; set; }

        [CsvPropertyName("path")]
        public string Path { get; set; }
    }
}
