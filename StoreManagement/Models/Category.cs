using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreManagement.Models
{
    public class Category
    {
        [Key]
        public int categoryId { get; set; }
        [Required]
        public string categoryName { get; set; }
        [Required]
        public string categoryDescription { get; set; }
    }
}
