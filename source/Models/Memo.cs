﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FishbowlInventory.Serialization;
using System.Threading.Tasks;

namespace FishbowlInventory.Models
{
    /// <summary>
    /// Stores additional notes on requests and objects.
    /// </summary>
    public class Memo
    {
        /// <summary>
        /// The memo's unique identification number.
        /// </summary>
        [CsvPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// The text inside the memo.
        /// </summary>
        [CsvPropertyName("memo")]
        public string Text { get; set; }

        /// <summary>
        /// Username of the user who created or modified the memo.
        /// </summary>
        [CsvPropertyName("username ")]
        public string UserName { get; set; }

        /// <summary>
        /// The date the memo was created.
        /// </summary>
        [CsvPropertyName("dateCreated")]
        public DateTime DateCreated { get; set; }
    }
}
