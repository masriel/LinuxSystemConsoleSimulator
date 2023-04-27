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
                CurrentDirectory = Directory.GetCurrentDirectory();
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
            catch(DirectoryNotFoundException DNFEx)
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
    }
}
