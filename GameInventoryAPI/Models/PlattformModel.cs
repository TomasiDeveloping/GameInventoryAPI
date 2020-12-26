using System;

namespace GameInventoryAPI.Models
{
    public class PlattformModel
    {
        public int PlattformId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Producer { get; set; }
        public DateTime Release { get; set; }
        public string PhotoUrl { get; set; }
    }
}