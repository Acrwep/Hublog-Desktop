using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMP
{
    public static class EnumManager
    {
        public enum Status
        {
            Error = 0,
            Success = 1,
            NotFound = 2,
            AlreadyExists = 3
        }
    }
}
