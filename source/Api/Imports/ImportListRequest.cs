using FishbowlInventory.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using FishbowlInventory.Serialization;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace FishbowlInventory.Api.Imports
{
    /// <summary>
    /// This request returns a list of your import options.
    /// </summary>
    class ImportListRequest : IFishbowlRequest
    {
        public string ElementName => "ImportListRq";
    }
}
