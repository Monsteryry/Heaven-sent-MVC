using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ExampleApi.Models.InputModels.Categories
{
    public class CategoryInputModel
    {
        /// <summary>
        /// Category name
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}