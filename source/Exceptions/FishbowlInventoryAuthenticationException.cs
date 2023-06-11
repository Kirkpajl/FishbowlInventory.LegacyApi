using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FishbowlInventory.Exceptions
{
    public class FishbowlInventoryAuthenticationException : FishbowlInventoryException
    {
        public FishbowlInventoryAuthenticationException() { }

        public FishbowlInventoryAuthenticationException(string message) : base(message) { }

        public FishbowlInventoryAuthenticationException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public FishbowlInventoryAuthenticationException(string message, Exception innerException) : base(message, innerException) { }

        public FishbowlInventoryAuthenticationException(string message, int statusCode, string content) : base(message, statusCode, content) { }

        public FishbowlInventoryAuthenticationException(string message, Exception innerException, int statusCode, string content) : base(message, innerException, statusCode, content) { }
    }
}
