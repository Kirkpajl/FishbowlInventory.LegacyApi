using System.ComponentModel.DataAnnotations;

namespace FishbowlInventory.Enumerations
{
    /// <summary>
    /// The basic type of the part
    /// </summary>
    /// <remarks> 'Inventory' | 'Service' | 'Labor' | 'Overhead' | 'Non-Inventory' | 'Internal Use' | 'Capital Equipment' | 'Shipping'</remarks>
    public enum PartType
    {
        /// <summary>
        /// Regular part kept in inventory.
        /// </summary>
        [Display(Name = "Inventory")]
        Inventory = 10,

        /// <summary>
        /// Service part, can be bought and sold, not stocked.
        /// </summary>
        [Display(Name = "Service")] 
        Service = 20,

        /// <summary>
        /// Labor - can only be added to BOM/WO
        /// </summary>
        [Display(Name = "Labor")] 
        Labor = 21,

        /// <summary>
        /// Overhead - can only be added to BOM/WO
        /// </summary>
        [Display(Name = "Overhead")] 
        Overhead = 22,

        /// <summary>
        /// Part not stocked, can be bought, can be sold. (Grocery Guys)
        /// </summary>
        [Display(Name = "Non-Inventory")] 
        NonInventory = 30,

        /// <summary>
        /// Office supplies, etc. Not stocked, can be bought, not sold.
        /// </summary>
        [Display(Name = "Internal Use")] 
        InternalUse = 40,

        /// <summary>
        /// Equipment that depreciates, not stocked, not sold.
        /// </summary>
        [Display(Name = "Capital Equipment")] 
        CapitalEquipment = 50,

        /// <summary>
        /// A shipping charge.
        /// </summary>
        [Display(Name = "Shipping")] 
        Shipping = 60,

        /// <summary>
        /// A tax charge.
        /// </summary>
        [Display(Name = "Tax")]
        Tax = 70,

        /// <summary>
        /// Miscellaneous.
        /// </summary>
        [Display(Name = "Misc")]
        Misc = 80
    }
}
