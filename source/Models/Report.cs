using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FishbowlInventory.Serialization;
using System.Threading.Tasks;

namespace FishbowlInventory.Models
{
	/// <summary>
	/// Contains the details about a report.
	/// </summary>
	public class Report
    {
		[CsvPropertyName("id")]
		public int Id { get; set; }

		[CsvPropertyName("name")]
		public string Name { get; set; }

		[CsvPropertyName("path")]
		public string Path { get; set; }

		[CsvPropertyName("reportTreeID")]
		public int ReportTreeId { get; set; }

		[CsvPropertyName("description")]
		public string Description { get; set; }

		[CsvPropertyName("activeFlag")]
		public bool IsActive { get; set; }

		[CsvPropertyName("readOnly")]
		public bool IsReadOnly { get; set; }

		[CsvPropertyName("createdDate")]
		public DateTime? CreatedDate { get; set; }

		[CsvPropertyName("dateLastModified")]
		public DateTime? DateLastModified { get; set; }

		[CsvPropertyName("userId")]
		public int UserId { get; set; }
	}
}
