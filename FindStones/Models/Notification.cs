using System;
using System.Collections.Generic;

namespace FindStonesAPI.Models
{
    public partial class Notification
    {
        public int NotificationId { get; set; }
        public int? UserId { get; set; }
        public int? ItemId { get; set; }
        public string? Message { get; set; }
        public bool? IsRead { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual Item? Item { get; set; }
        public virtual User? User { get; set; }
    }
}
