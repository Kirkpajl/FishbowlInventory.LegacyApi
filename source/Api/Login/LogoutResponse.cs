using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace FishbowlInventory.Api.Login
{
    class LogoutResponse : IFishbowlResponse<LogoutRequest>
    {
        public string ElementName => "LogoutRs";

        [JsonPropertyName("statusCode")]
        public int StatusCode { get; set; }
    }
}
