using System.ComponentModel.DataAnnotations;

namespace FishbowlInventory.Enumerations
{
    /// <summary>
    /// Indicates whether the order is a standard or drop ship purchase order
    /// </summary>
    /// <remarks> 'Standard' | 'Drop Ship'</remarks>
    public enum PurchaseOrderType
    {
        [Display(Name = "Standard")] Standard = 10,
        [Display(Name = "Drop Ship")] DropShip = 20
    }
}
