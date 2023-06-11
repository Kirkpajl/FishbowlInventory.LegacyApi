using System.ComponentModel.DataAnnotations;

namespace FishbowlInventory.Enumerations
{
    /// <summary>
    /// The order status.
    /// </summary>
    /// <remarks>
    /// 'All' | 'All Open' | 'Bid Request' | 'Pending Approval' | 'Issued' | 'Picking' | 'Partial' | 'Picked' | 'Shipped' | 'Fulfilled' | 'Closed Short' | 'Void' | 'Historical'
    /// </remarks>
    public enum PurchaseOrderStatus
    {        
        [Display(Name = "For Calendar")] ForCalendar = 2,
        [Display(Name = "Bid Request")] BidRequest = 10,
        [Display(Name = "Pending Approval")] PendingApproval = 15,
        [Display(Name = "Issued")] Issued = 20,
        [Display(Name = "Picking")] Picking = 30,
        [Display(Name = "Partial")] Partial = 40,
        [Display(Name = "Picked")] Picked = 50,
        [Display(Name = "Shipped")] Shipped = 55,
        [Display(Name = "Fulfilled")] Fulfilled = 60,
        [Display(Name = "Closed Short")] ClosedShort = 70,
        [Display(Name = "Void")] Void = 80,
        [Display(Name = "Historical")] Historical = 95
    }
}
