using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreManagement.Models.Dtos
{
    public class SignupDto
    {
        public string email { get; set; }
        public string password { get; set; }
        public string fName { get; set; }
        public string lName { get; set; }
    }
}
