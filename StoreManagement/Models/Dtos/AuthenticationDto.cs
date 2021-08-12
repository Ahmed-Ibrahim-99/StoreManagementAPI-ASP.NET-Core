using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreManagement.Models.Dtos
{
    public class AuthenticationDto
    {
        public int userId { get; set; }
        public string email { get; set; }
        public string fName { get; set; }
        public string lName { get; set; }
        public string token { get; set; }
        public AuthenticationDto(User user, string newToken)
        {
            userId = user.userId;
            email = user.email;
            fName = user.fName;
            lName = user.lName;
            token = newToken;
        }

    }
}
