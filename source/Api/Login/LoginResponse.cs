using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FishbowlInventory.Serialization;
using System.Threading.Tasks;
using FishbowlInventory.Models;
using System.Text.Json.Serialization;

namespace FishbowlInventory.Api.Login
{
    class LoginResponse : IFishbowlResponse<LoginRequest>
    {
        [JsonIgnore]
        public string ElementName => "LoginRs";



        [JsonPropertyName("statusCode")]
        public int StatusCode { get; set; }

        [JsonPropertyName("UserFullName")]
        public string FullName { get; set; }

        [JsonPropertyName("ModuleAccess")]
        public ModuleInformationInformation ModuleAccess { get; set; }

        [JsonPropertyName("ServerVersion")]
        public string ServerVersion { get; set; }
    }

    class ModuleInformationInformation
    {
        [JsonPropertyName("Module")]
        public string[] AllowedModules { get; set; }
    }

    public class UserInformation
    {
        [JsonPropertyName("userFullName")]
        public string FullName { get; set; }

        [JsonPropertyName("moduleAccessList")]
        public string[] AllowedModules { get; set; } = Array.Empty<string>();

        [JsonPropertyName("serverVersion")]
        public string ServerVersion { get; set; }
    }
}
