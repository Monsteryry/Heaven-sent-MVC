using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models.OutputModels.Products
{
    public class ProductOutputModel
    {
        /// <summary>
        /// Product identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Product name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Product category name
        /// </summary>
        public string CategoryName { get; set; }
    }
}