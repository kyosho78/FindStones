using System;
using System.Collections.Generic;

namespace FindStonesAPI.Models
{
    public partial class User
    {
        public User()
        {
            ItemCreators = new HashSet<Item>();
            ItemFinders = new HashSet<Item>();
            ItemHistories = new HashSet<ItemHistory>();
            Locations = new HashSet<Location>();
            Notifications = new HashSet<Notification>();
            UserFoundItems = new HashSet<UserFoundItem>();
        }

        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }

        public virtual ICollection<Item> ItemCreators { get; set; }
        public virtual ICollection<Item> ItemFinders { get; set; }
        public virtual ICollection<ItemHistory> ItemHistories { get; set; }
        public virtual ICollection<Location> Locations { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<UserFoundItem> UserFoundItems { get; set; }
    }
}
