using System.ComponentModel.DataAnnotations;

namespace FishbowlInventory.Enumerations
{
    /// <summary>
    /// The configuration item type.
    /// </summary>
    /// <remarks>
    /// 'Finished Good' | 'Raw Good' | 'Repair Raw Good' | 'Note' | 'Bill of Materials'
    /// </remarks>
    public enum ConfigurationItemType
    {
        [Display(Name = "Finished Good")] FinishedGood,
        [Display(Name = "Raw Good")] RawGood,
        [Display(Name = "Repair Raw Good")] RepairRawGood,
        [Display(Name = "Note")] Note,
        [Display(Name = "Bill of Materials")] BillOfMaterials
    }
}
