using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemConsoleSimulator
{
    class CreateStartString
    {
        
        public static string MainString(string User, string PC, string CurDir)
        {
            string result = $"{User}@{PC}:{CurDir}$ ";
            return result;
        }
    }
}
