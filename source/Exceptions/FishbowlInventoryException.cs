using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FishbowlInventory.Exceptions
{
    public abstract class FishbowlInventoryException : Exception
    {
        protected FishbowlInventoryException() { }

        protected FishbowlInventoryException(string message) : base(message) { }

        protected FishbowlInventoryException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        protected FishbowlInventoryException(string message, Exception innerException) : base(message, innerException) { }

        protected FishbowlInventoryException(string message, int statusCode, string content) : base(message)
        {
            StatusCode = statusCode;
            Content = content;
        }

        protected FishbowlInventoryException(string message, Exception innerException, string content) : base(message, innerException)
        {
            Content = content;
        }

        protected FishbowlInventoryException(string message, Exception innerException, int statusCode, string content) : base(message, innerException)
        {
            StatusCode = statusCode;
            Content = content;
        }



        public int StatusCode { get; }
        public string Content { get; }
    }
}
