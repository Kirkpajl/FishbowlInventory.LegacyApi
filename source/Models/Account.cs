using FishbowlInventory.Serialization;

namespace FishbowlInventory.Models
{
    /// <summary>
    /// Contains information relevant to one of the financial accounts in the Fishbowl database.
    /// </summary>
    public class Account
    {
        [CsvPropertyName("name")]
        public string Name { get; set; }

        [CsvPropertyName("accountingId")]
        public string AccountingId { get; set; }

        [CsvPropertyName("accountType")]
        public int AccountType { get; set; }

        [CsvPropertyName("balance")]
        public string Balance { get; set; }
    }
}
