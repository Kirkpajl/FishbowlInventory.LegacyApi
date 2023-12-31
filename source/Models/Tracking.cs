﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FishbowlInventory.Serialization;
using System.Threading.Tasks;

namespace FishbowlInventory.Models
{
    /// <summary>
    /// This simply contains the TrackingItem objects which contain the information being tracked.
    /// </summary>
    public class Tracking
    {
        [CsvPropertyName("trackingItem")]
        public TrackingItem TrackingItem { get; set; }
    }
}
