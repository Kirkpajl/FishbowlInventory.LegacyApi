using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FishbowlInventory.Serialization;
using System.Threading.Tasks;

namespace FishbowlInventory.Models
{
    /// <summary>
    /// The currency used on the order.
    /// </summary>
    public class Currency
    {
        /// <summary>
        /// The unique identification number.
        /// </summary>
        [CsvPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// The name of the object.
        /// </summary>
        [CsvPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// The currency code.
        /// </summary>
        [CsvPropertyName("code")]
        public string Code { get; set; }

        /// <summary>
        /// The currency exchange rate.
        /// </summary>
        [CsvPropertyName("rate")]
        public double Rate { get; set; }

        /// <summary>
        /// Indicates if the currency is the home currency.
        /// </summary>
        [CsvPropertyName("homeCurrency")]
        public bool HomeCurrency { get; set; }
    }
}
