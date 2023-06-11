﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FishbowlInventory.Exceptions
{
    public sealed class FishbowlInventoryOperationException : FishbowlInventoryException
    {
        public FishbowlInventoryOperationException() { }

        public FishbowlInventoryOperationException(string message) : base(message) { }

        public FishbowlInventoryOperationException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public FishbowlInventoryOperationException(string message, Exception innerException) : base(message, innerException) { }

        public FishbowlInventoryOperationException(string message, int statusCode, string content) : base(message, statusCode, content) { }

        public FishbowlInventoryOperationException(string message, Exception innerException, int statusCode, string content) : base(message, innerException, statusCode, content) { }
    }
}
