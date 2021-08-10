using StoreManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreManagement.Repository.IRepository
{
    public interface ISignupRepository
    {
        Task<bool> SignupNewUser(string email, string password, string fName, string lName);
    }
}
