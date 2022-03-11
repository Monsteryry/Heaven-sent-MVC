using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class Item
    {
        public int ItemId { get; set; }
        public string Title { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public string ItemArtUrl { get; set; }
        public Category Category { get; set; }
    }
}