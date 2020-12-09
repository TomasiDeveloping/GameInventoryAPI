using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameInventoryAPI.Models
{
    public class PlattformModel
    {
        public int PlattformId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Producer { get; set; }
        public DateTime Release { get; set; }
    }
}