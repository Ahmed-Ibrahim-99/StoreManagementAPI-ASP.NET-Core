using StoreManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreManagement.Repository.IRepository
{
    public interface IUserRepository
    {
        Task<bool> EmailRegisteredBefore(string email);
        Task<User> GetUserById(int Id);
        Task<User> GetUserByEmail(string email);
    }
}
