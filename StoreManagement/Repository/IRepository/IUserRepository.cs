using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreManagement.Repository.IRepository
{
    public interface IUserRepository
    {
        Task<bool> EmailRegisteredBefore(string email);
    }
}
