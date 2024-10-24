using System;
using System.Collections.Generic;

namespace FindStonesAPI.Models
{
    public partial class ItemHistory
    {
        public int HistoryId { get; set; }
        public int? ItemId { get; set; }
        public int? UpdatedBy { get; set; }
        public string? ChangeType { get; set; }
        public string? PreviousValue { get; set; }
        public string? NewValue { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual Item? Item { get; set; }
        public virtual User? UpdatedByNavigation { get; set; }
    }
}
