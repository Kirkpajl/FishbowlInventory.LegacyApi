using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FishbowlInventory.Serialization;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace FishbowlInventory.Api.Login
{
    /// <summary>
    /// This logs the user in to Fishbowl and returns the list of Access Rights.
    /// </summary>
    /// <remarks>
    /// The first time your Integrated App logs in, you'll need to approve the App in Fishbowl.
    /// </remarks>
    class LoginRequest : IFishbowlRequest
    {
        [JsonIgnore]
        public string ElementName => "LoginRq";


        /// <summary>
        /// The name of your application.
        /// </summary>
        [JsonPropertyName("IAName")]
        public string AppName { get; set; }

        /// <summary>
        /// A brief description of what your application does.
        /// </summary>
        [JsonPropertyName("IADescription")]
        public string AppDescription { get; set; }

        /// <summary>
        /// The unique identifier for your integrated application. Use any number you want, but we recommend at least 4 digits in length.
        /// </summary>
        [JsonPropertyName("IAID")]
        public int AppId { get; set; }

        /// <summary>
        /// The username of the user logging in.
        /// </summary>
        [JsonPropertyName("UserName")]
        public string Username { get; set; }

        /// <summary>
        /// The user's password.
        /// </summary>
        [JsonPropertyName("UserPassword")]
        public string Password { get; set; }
    }
}
