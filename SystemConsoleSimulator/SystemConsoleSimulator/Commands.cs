using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemConsoleSimulator
{
    class Commands
    {
        public string CommandName = string.Empty;
        public string[] Parameters;

        public Commands(string[] _commandwords)
        {
            CommandName = _commandwords[0];
            if(_commandwords.Length != 1)
            {
                Parameters = new string[_commandwords.Length - 1];
                for (int i = 0; i < Parameters.Length; i++)
                {
                    Parameters[i] = _commandwords[i + 1];
                }
            }
            WhichCommand();
        }

        private void WhichCommand()
        {
            switch (CommandName)
            {
                case "man":
                    CommandMan(Parameters);
                    break;
                case "ls":
                    CommandLs(Parameters);
                    break;
                case "pwd":
                    CommandPwd(Parameters);
                    break;
                case "cd":
                    CommandCd(Parameters);
                    break;
                case "date":
                    CommandDate(Parameters);
                    break;
                default:
                    Console.WriteLine("command not found");
                    break;
            }
        }

        #region Man

        private void CommandMan(string[] parameters)
        {
            parameters = Parameters;
            string com = string.Empty;

            if (parameters == null)
            {
                Console.WriteLine("What manual page do you want?\nFor example, try 'man man'.");
                return;
            }
            com = parameters[0];
            switch (com)
            {
                case "ls":
                    Console.WriteLine(Man.LS);
                    break;
                case "pwd":
                    Console.WriteLine(Man.PWD);
                    break;
                case "cd":
                    Console.WriteLine(Man.CD);
                    break;
                case "mkdir":
                    Console.WriteLine(Man.MKDIR);
                    break;
                case "cat":
                    Console.WriteLine(Man.CAT);
                    break;
                case "touch":
                    Console.WriteLine(Man.TOUCH);
                    break;
                case "exit":
                    Console.WriteLine(Man.EXIT);
                    break;
                case "rm":
                    Console.WriteLine(Man.RM);
                    break;
                case "head":
                    Console.WriteLine(Man.HEAD);
                    break;
                case "date":
                    Console.WriteLine(Man.DATE);
                    break;
                case "arch":
                    Console.WriteLine(Man.ARCH);
                    break;
                case "clear":
                    Console.WriteLine(Man.CLEAR);
                    break;
            }
        }

        #endregion

        #region Ls

        private void CommandLs(string[] parameters)
        {
            parameters = Parameters;
            string CurrentDirectory = string.Empty; ;

            if (parameters == null)
            {
                CurrentDirectory = CurrentDirectoryValues.CurrentDirectory;
                OutputLs(CurrentDirectory);
                return;
            }

            //вывод корневого каталога
            if (parameters[0] == "/")
            {
                CurrentDirectory = Directory.GetCurrentDirectory().Substring(0, 3);
                OutputLs(CurrentDirectory);
                return;
            }

            //сортировка по алфавиту
            string par = string.Empty;
            for (int i = 0; i < parameters.Length; i++)
            {
                par += parameters[i];
            }
            
            if (par.Contains("-X"))
            {
                if (parameters[0] != "-X") CurrentDirectory = parameters[0];
                else CurrentDirectory = Directory.GetCurrentDirectory();

                string[] AllFiles = { };
                string[] AllDir = { };
                try
                {
                    AllFiles = Directory.GetFiles(CurrentDirectory);
                    AllDir = Directory.GetDirectories(CurrentDirectory);
                }
                catch (DirectoryNotFoundException)
                {
                    Console.WriteLine("Directory not found\nTry changing the path");
                }

                AllDir = AllDir.Concat(AllFiles).ToArray();
                Array.Sort(AllDir);

                for (int i = 0; i < AllDir.Length; i++)
                {
                    Console.WriteLine(AllDir[i].Substring(CurrentDirectory.Length));
                }
                return;
            }

            //вывод введенной директории
            CurrentDirectory = parameters[0] + "/";
            OutputLs(CurrentDirectory);
        }

        private void OutputLs(string _currentdirectory)
        {
            string[] AllFiles = { };
            string[] AllDir = { };
            try
            {
                AllFiles = Directory.GetFiles(_currentdirectory);
                AllDir = Directory.GetDirectories(_currentdirectory);
            }
            catch(DirectoryNotFoundException)
            {
                Console.WriteLine("Directory not found\nTry changing the path");
            }

            for (int i = 0; i < AllDir.Length; i++)
            {
                Console.WriteLine(AllDir[i].Substring(_currentdirectory.Length));
            }
            for (int i = 0; i < AllFiles.Length; i++)
            {
                Console.WriteLine(AllFiles[i].Substring(_currentdirectory.Length));
            }
        }

        #endregion

        #region Pwd

        private void CommandPwd(string[] parameters)
        {
            Console.WriteLine(CurrentDirectoryValues.CurrentDirectory);
        }

        #endregion

        #region Cd

        private void CommandCd(string[] parameters)
        {
            if (parameters == null)
            {
                CurrentDirectoryValues.CurrentDirectory = Directory.GetCurrentDirectory().Substring(0, 3);
                return;
            }
            CurrentDirectoryValues.CurrentDirectory += parameters[0];
        }

        #endregion

        #region Date

        private void CommandDate(string[] parameters)
        {
            string DayWeek = DateTime.Now.DayOfWeek.ToString().Substring(0, 3);
            int Month = Convert.ToInt32(DateTime.Now.Month);
            string[] m = { "", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            string Time = DateTime.Now.ToLongTimeString();
            string Year = DateTime.Now.Year.ToString();
            Console.WriteLine($"{DayWeek} {m[Month]} {Time} {Year}");
        }

        #endregion
    }
}
