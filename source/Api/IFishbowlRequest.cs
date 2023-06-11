using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishbowlInventory.Api
{
    interface IFishbowlRequest
    {
        string ElementName { get; }
    }
}
