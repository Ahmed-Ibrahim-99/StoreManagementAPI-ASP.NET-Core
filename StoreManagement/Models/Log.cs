using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreManagement.Models
{
    public class Log
    {
        [Required]
        public string logDescription { get; set; }
        [Required]
        public int userId { get; set; }
    }
}
