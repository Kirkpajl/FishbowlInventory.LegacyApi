using System.ComponentModel.DataAnnotations;

namespace FishbowlInventory.Enumerations
{
    /// <summary>
    /// The order status.
    /// </summary>
    /// <remarks>
    /// 'All' | 'All Open' | 'Entered' | 'Issued' | 'Partial' | 'Fulfilled' | 'Closed Short' | 'Void'
    /// </remarks>
    public enum ManufactureOrderStatus
    {
        [Display(Name = "All")] All,
        [Display(Name = "All Open")] AllOpen,
        [Display(Name = "Entered")] Entered,
        [Display(Name = "Issued")] Issued,
        [Display(Name = "Partial")] Partial,
        [Display(Name = "Fulfilled")] Fulfilled,
        [Display(Name = "Closed Short")] ClosedShort,
        [Display(Name = "Void")] Void
    }
}
