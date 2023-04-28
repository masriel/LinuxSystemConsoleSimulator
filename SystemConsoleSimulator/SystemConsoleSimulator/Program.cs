using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemConsoleSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            //Команды man, ls, pwd, cd, mkdir, rmdir, cat, touch, exit, rm, head, date, arch, clear
            //Команды ps, du, df, wc, kill, tail, uname, cp

            string Command = string.Empty;

        InputCommand:
            Console.Write(CreateStartString.MainString(Environment.UserName, Environment.MachineName, CurrentDirectoryValues.ShowCurrentDirectory()));
            Command = Console.ReadLine();

            string[] CommandWords = Command.Split(' ');
;
            Commands commands = new Commands(CommandWords);
            goto InputCommand;


            //Console.ReadKey();
        }
    }
}
