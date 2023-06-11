using System.ComponentModel.DataAnnotations;

namespace FishbowlInventory.Enumerations
{
    /// <summary>
    /// The basic type of the UOM.
    /// </summary>
    /// <remarks>'Area' | 'Count' | 'Length' | 'Time' | 'Volume' | 'Weight'</remarks>
    public enum UnitOfMeasureType
    {
        [Display(Name = "Area")] Area,
        [Display(Name = "Count")] Count,
        [Display(Name = "Length")] Length,
        [Display(Name = "Time")] Time,
        [Display(Name = "Volume")] Volume,
        [Display(Name = "Weight")] Weight
    }
}
