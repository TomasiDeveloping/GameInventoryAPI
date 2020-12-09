using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameInventoryAPI.Models
{
    public class PublisherModel
    {
        public int PublisherId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
    }
}