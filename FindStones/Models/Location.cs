using System;
using System.Collections.Generic;

namespace FindStonesAPI.Models
{
    public partial class Location
    {
        public Location()
        {
            Items = new HashSet<Item>();
        }

        public int LocationId { get; set; }
        public int? UserId { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual User? User { get; set; }
        public virtual ICollection<Item> Items { get; set; }
    }
}
