using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace FishbowlInventory.Api.Login
{
    class LogoutRequest : IFishbowlRequest
    {
        public string ElementName => "LogoutRq";
    }
}
