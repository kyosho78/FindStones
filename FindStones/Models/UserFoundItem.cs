using System;
using System.Collections.Generic;

namespace FindStonesAPI.Models
{
    public partial class UserFoundItem
    {
        public int UserId { get; set; }
        public int ItemId { get; set; }
        public DateTime? FoundAt { get; set; }

        public virtual Item Item { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
