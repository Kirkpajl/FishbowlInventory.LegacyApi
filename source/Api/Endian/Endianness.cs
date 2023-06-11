using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishbowlInventory.Api.Endian
{
    /// <summary>
    /// Endianness of a converter
    /// </summary>
    internal enum Endianness
    {
        /// <summary>
        /// Little endian - least significant byte first
        /// </summary>
        LittleEndian,

        /// <summary>
        /// Big endian - most significant byte first
        /// </summary>
        BigEndian
    }
}
