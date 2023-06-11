using System.ComponentModel.DataAnnotations;

namespace FishbowlInventory.Enumerations
{
    /// <summary>
    /// The type of a BOM item contained in the manufacture order
    /// </summary>
    /// <remarks>'All' | 'Finished Good' | 'Raw Good' | 'Repair' | 'Note' | 'Bill of Materials'</remarks>
    public enum BillOfMaterialItemType
    {
        [Display(Name = "All")] All,
        [Display(Name = "Finished Good")] FinishedGood,
        [Display(Name = "Raw Good")] RawGood,
        [Display(Name = "Repair")] Repair,
        [Display(Name = "Note")] Note,
        [Display(Name = "Bill of Materials")] BillOfMaterials
    }
}
