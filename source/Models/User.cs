using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FishbowlInventory.Serialization;
using System.Threading.Tasks;

namespace FishbowlInventory.Models
{
    /// <summary>
    /// This is an object representing a Fishbowl user.
    /// </summary>
    public class User
    {
        /// <summary>
        /// The user's unique identification number.
        /// </summary>
        [CsvPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// The username.
        /// </summary>
        [CsvPropertyName("username")]
        public string UserName { get; set; }

        /// <summary>
        /// The first name of the user.
        /// </summary>
        [CsvPropertyName("firstName")]
        public string FirstName { get; set; }

        /// <summary>
        /// The last name of the user.
        /// </summary>
        [CsvPropertyName("lastName")]
        public string LastName { get; set; }

        /// <summary>
        /// The initials of the user's name.
        /// </summary>
        [CsvPropertyName("initials")]
        public string Initials { get; set; }

        /// <summary>
        /// The active status of the user.
        /// </summary>
        [CsvPropertyName("active")]
        public bool IsActive { get; set; }

        /// <summary>
        /// A string separated with the pipe character ("|") listing the User Groups the User is a part of.
        /// </summary>
        /// <remarks>
        /// There must be no spaces between delimeters and the text.
        /// </remarks>
        [CsvPropertyName("userGroups")]
        public string UserGroups { get; set; }

        /// <summary>
        /// A string specifying the default Location Group.
        /// </summary>
        /// <remarks>
        /// This string must also appear in the <seealso cref="LocGroups"/> string below.
        /// </remarks>
        [CsvPropertyName("defaultLocGroup")]
        public string DefaultLocGroup { get; set; }

        /// <summary>
        /// A string separated with the pipe character ("|") listing the Location Groups the User is a part of.
        /// </summary>
        /// <remarks>
        /// There must be no spaces between delimeters and the text.
        /// </remarks>
        [CsvPropertyName("locGroups")]
        public string LocGroups { get; set; }

        /// <summary>
        /// The email address of the User.
        /// </summary>
        [CsvPropertyName("email")]
        public string Email { get; set; }

        /// <summary>
        /// The phone number of the User.
        /// </summary>
        [CsvPropertyName("phone")]
        public string Phone { get; set; }

        /// <summary>
        /// A list of the custom fields associated with the user.
        /// </summary>
        public List<CustomField> CustomFields { get; set; }
    }
}
