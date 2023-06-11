using FishbowlInventory.Enumerations;
using FishbowlInventory.Serialization;
using System;

namespace FishbowlInventory.Models
{
	/// <summary>
	/// Contains all the work order information and details.
	/// </summary>
	public class WorkOrder
	{
		[CsvPropertyName("Id")]
		public int Id { get; set; }

		[CsvPropertyName("Number")]
		public string Number { get; set; }

		[CsvPropertyName("ManufacturingOrderItemId")]
		public int ManufacturingOrderItemId { get; set; }

        [CsvPropertyName("ManufacturingOrderNumber")]
        public string ManufacturingOrderNumber { get; set; }

        [CsvPropertyName("Description")]
        public string Description { get; set; }

        [CsvPropertyName("Cost")]
		public decimal Cost { get; set; }

		[CsvPropertyName("LocationName")]
		public string LocationName { get; set; }

		[CsvPropertyName("LocationGroupName")]
		public string LocationGroupName { get; set; }

		[CsvPropertyName("Note")]
		public string Note { get; set; }

        [CsvPropertyName("StatusId")]
        public WorkOrderStatus Status { get; set; }

        [CsvPropertyName("QuickBooksClassName")]
		public string QuickBooksClassName { get; set; }

		[CsvPropertyName("QuantityOrdered")]
		public int QuantityOrdered { get; set; }

		[CsvPropertyName("QuantityTarget")]
		public int QuantityTarget { get; set; }

		[CsvPropertyName("DateCreated")]
		public DateTime? DateCreated { get; set; }

		[CsvPropertyName("DateFinished")]
		public DateTime? DateFinished { get; set; }

		[CsvPropertyName("DateLastModified")]
		public DateTime? DateLastModified { get; set; }

		[CsvPropertyName("DateScheduled")]
		public DateTime? DateScheduled { get; set; }

		[CsvPropertyName("DateStarted")]
		public DateTime? DateStarted { get; set; }

		[CsvPropertyName("UserName")]
		public string UserName { get; set; }



        [CsvPropertyName("woItems")]
        public WorkOrderItem[] Items { get; set; }
    }
}
