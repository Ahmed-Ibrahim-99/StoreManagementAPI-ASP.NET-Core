using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreManagement.Repository.IRepository
{
    public interface ILoginRepository
    {
        Task<bool> CheckCredentials(string email, string password);
    }
}
