using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreManagement.Models.Enums
{
    public class LogEnums
    {
        public enum TableEnum
        {
            Product,
            Category
        }

        public enum ActionEnum
        {
            Create,
            Read,
            Update,
            Delete
        }
    }
}
