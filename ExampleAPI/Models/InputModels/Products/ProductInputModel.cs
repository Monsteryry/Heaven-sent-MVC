using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace API.Models.InputModels.Products
{
    public class ProductInputModel
    {
        /// <summary>
        /// Product name
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// Product price
        /// </summary>
        [Range(0,(double)decimal.MaxValue)]
        public decimal Price { get; set; }

        /// <summary>
        /// Category identifier
        /// </summary>
        [Required]
        public int CategoryId { get; set; }
    }
}