using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static StoreManagement.Models.Enums.LogEnums;

namespace StoreManagement.Repository.IRepository
{
    public interface ILogRepository
    {
        Task<bool> CreateLog(TableEnum table, ActionEnum action, int userId);
    }
}
