﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishbowlInventory.Api
{
    interface IFishbowlResponse<TRequest>
        where TRequest : IFishbowlRequest
    {
        string ElementName { get; }
        int StatusCode { get; }
    }
}
