using System;
using System.Collections.Generic;

namespace FindStonesAPI.Models
{
    public partial class Item
    {
        public Item()
        {
            ItemHistories = new HashSet<ItemHistory>();
            Notifications = new HashSet<Notification>();
            UserFoundItems = new HashSet<UserFoundItem>();
        }

        public int ItemId { get; set; }
        public int? LocationId { get; set; }
        public int? CreatorId { get; set; }
        public int? FinderId { get; set; }
        public string ItemName { get; set; } = null!;
        public string? Clue { get; set; }
        public string? ImageUrl { get; set; }
        public bool? IsMissing { get; set; }
        public DateTime? MissingSince { get; set; }
        public string? LastSeenLocation { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual User? Creator { get; set; }
        public virtual User? Finder { get; set; }
        public virtual Location? Location { get; set; }
        public virtual ICollection<ItemHistory> ItemHistories { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<UserFoundItem> UserFoundItems { get; set; }
    }
}
