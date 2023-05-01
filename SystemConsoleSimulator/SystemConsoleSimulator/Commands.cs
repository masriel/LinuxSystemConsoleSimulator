using System;
using System.IO;
using System.Linq;
using System.Web.Security;

namespace SystemConsoleSimulator
{
    class Commands
    {
        private readonly string _commandName;
        private readonly string[] _parameters;

        public Commands(string[] commandwords)
        {
            _commandName = commandwords[0];
            if(commandwords.Length != 1)
            {
                _parameters = new string[commandwords.Length - 1];
                for (int i = 0; i < _parameters.Length; i++)
                {
                    _parameters[i] = commandwords[i + 1];
                }
            }
            WhichCommand();
        }

        private void WhichCommand()
        {
            switch (_commandName)
            {
                case "man":
                    CommandMan(_parameters);
                    break;
                case "ls":
                    CommandLs(_parameters);
                    break;
                case "pwd":
                    CommandPwd(_parameters);
                    break;
                case "cd":
                    CommandCd(_parameters);
                    break;
                case "date":
                    CommandDate(_parameters);
                    break;
                case "mkdir":
                    CommandMkdir(_parameters);
                    break;
                case "rmdir":
                    CommandRmdir(_parameters);
                    break;
                case "touch":
                    CommandTouch(_parameters);
                    break;
                case "clear":
                    CommandClear(_parameters);
                    break;
                case "arch":
                    CommandArch(_parameters);
                    break;
                case "exit":
                    CommandExit(_parameters);
                    break;
                case "df":
                    CommandDf(_parameters);
                    break;
                case "du":
                    CommandDu(_parameters);
                    break;
                case "uname":
                    CommandUname(_parameters);
                    break;
                case "pwgen":
                    CommandPwgen(_parameters);
                    break;
                case "cat":
                    CommandCat(_parameters);
                    break;
                case "rm":
                    CommandRm(_parameters);
                    break;
                default:
                    Console.WriteLine("command not found");
                    break;
            }
        }

        #region Man

        private void CommandMan(string[] parameters)
        {
            parameters = _parameters;
            string com;

            if (parameters == null)
            {
                Console.WriteLine("What manual page do you want?\nFor example, try 'man man'.");
                return;
            }
            if (parameters[0] == "--help")
            {
                Console.WriteLine("Usage: man [OPTION...] [SECTION] PAGE...\nOptions:\n\t\tprint the value of $PWD if it names the current working directory\n\t-P\tprint the physical directory, without any symbolic links");
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

        #region Ls (/, -X)

        private void CommandLs(string[] parameters)
        {
            parameters = _parameters;
            string currentDirectory;

            if (parameters == null)
            {
                currentDirectory = CurrentDirectoryValues.CurrentDirectory;
                OutputLs(currentDirectory);
                return;
            }

            /*//вывод корневого каталога
            if (parameters[0] == "/")
            {
                currentDirectory = Directory.GetCurrentDirectory().Substring(0, 3);
                OutputLs(currentDirectory);
                return;
            }*/

            //сортировка по алфавиту
            string par = string.Empty;
            for (int i = 0; i < parameters.Length; i++)
            {
                par += parameters[i];
            }
            
            if (par.Contains("-X"))
            {
                if (parameters[0] != "-X") currentDirectory = parameters[0];
                else currentDirectory = CurrentDirectoryValues.CurrentDirectory;

                string[] allFiles = { };
                string[] allDir = { };
                try
                {
                    allFiles = Directory.GetFiles(currentDirectory);
                    allDir = Directory.GetDirectories(currentDirectory);
                }
                catch (DirectoryNotFoundException)
                {
                    Console.WriteLine("Directory not found\nTry changing the path");
                }

                allDir = allDir.Concat(allFiles).ToArray();
                Array.Sort(allDir);

                for (int i = 0; i < allDir.Length; i++)
                {
                    Console.WriteLine(allDir[i].Substring(currentDirectory.Length));
                }
                return;
            }

            //вывод введенной директории
            currentDirectory = parameters[0] + "/";
            OutputLs(currentDirectory);
        }

        private void OutputLs(string currentdirectory)
        {
            string[] allFiles = { };
            string[] allDir = { };
            try
            {
                allFiles = Directory.GetFiles(currentdirectory);
                allDir = Directory.GetDirectories(currentdirectory);
            }
            catch(DirectoryNotFoundException)
            {
                Console.WriteLine("Directory not found\nTry changing the path");
            }

            for (int i = 0; i < allDir.Length; i++)
            {
                Console.WriteLine(allDir[i].Substring(currentdirectory.Length));
            }
            for (int i = 0; i < allFiles.Length; i++)
            {
                Console.WriteLine(allFiles[i].Substring(currentdirectory.Length));
            }
        }

        #endregion

        #region Pwd ()

        private void CommandPwd(string[] parameters)
        {
            if (parameters == null) 
            { 
                Console.WriteLine(CurrentDirectoryValues.CurrentDirectory);
                return;
            }
            if (parameters[0] == "--help")
            {
                Console.WriteLine("pwd: pwd [-LP]\n\tPrint the name of the current working directory.\nOptions:\n\t-L\tprint the value of $PWD if it names the current working directory\n\t-P\tprint the physical directory, without any symbolic links");
            }
        }

        #endregion

        #region Cd

        private void CommandCd(string[] parameters)
        {
            if (parameters == null)
            {
                CurrentDirectoryValues.CurrentDirectory = Directory.GetDirectoryRoot(Directory.GetCurrentDirectory());
                return;
            }
            if (CurrentDirectoryValues.CurrentDirectory.Contains(parameters[0]))
            {
                CurrentDirectoryValues.CurrentDirectory = parameters[0];
                return;
            }
            CurrentDirectoryValues.CurrentDirectory += $@"{parameters[0]}\";
        }

        #endregion

        #region Date

        private void CommandDate(string[] parameters)
        {
            string dayWeek = DateTime.Now.DayOfWeek.ToString().Substring(0, 3);
            int month = Convert.ToInt32(DateTime.Now.Month);
            string[] m = { "", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            string time = DateTime.Now.ToLongTimeString();
            string year = DateTime.Now.Year.ToString();
            Console.WriteLine($"{dayWeek} {m[month]} {time} {year}");
        }

        #endregion

        #region Mkdir

        private void CommandMkdir(string[] parameters)
        {
            if(parameters == null)
            {
                Console.WriteLine("mkdir: missing operand");
                return;
            }
            if (parameters.Length == 1)
            {
                Directory.CreateDirectory($"{CurrentDirectoryValues.CurrentDirectory}{parameters[0]}");
            }
            else
            {
                for (int i = 0; i < parameters.Length; i++)
                {
                    Directory.CreateDirectory($"{CurrentDirectoryValues.CurrentDirectory}{parameters[i]}");
                }
            }
        }

        #endregion

        #region Rmdir

        private void CommandRmdir(string[] parameters)
        {
            if (parameters == null)
            {
                Console.WriteLine("rmdir: missing operand");
                return;
            }

            if (Directory.GetDirectories($"{CurrentDirectoryValues.CurrentDirectory}{parameters[0]}").Length + Directory.GetFiles($"{CurrentDirectoryValues.CurrentDirectory}{parameters[0]}").Length > 0)
            {
                Console.WriteLine("Directory not empty");
                return;
            }

            Directory.Delete($"{CurrentDirectoryValues.CurrentDirectory}{parameters[0]}");
        }

        #endregion

        #region Cat

        private void CommandCat(string[] parameters)
        {
            if (parameters == null)
            {
                Console.WriteLine("file not found");
                return;
            }
            using(StreamReader reader = new StreamReader($@"{CurrentDirectoryValues.CurrentDirectory}{parameters[0]}.txt"))
            {
                while(!(reader.EndOfStream))
                    Console.WriteLine(reader.ReadLine());
            }
        }

        #endregion

        #region Touch

        private void CommandTouch(string[] parameters)
        {
            if (parameters == null)
            {
                Console.WriteLine("touch: missing file operand");
                return;
            }
            for (int i = 0; i < parameters.Length; i++)
            {
                File.Create($"{CurrentDirectoryValues.CurrentDirectory}{parameters[i]}");
            }
        }

        #endregion

        #region Clear

        private void CommandClear(string[] parameters)
        {
            Console.Clear();
        }

        #endregion

        #region Arch

        private void CommandArch(string[] parameters)
        {
            Console.WriteLine(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE"));
        }

        #endregion

        #region Head

        private void CommandHead(string[] parameters)
        {

        }

        #endregion

        #region Exit

        private void CommandExit(string[] parameters)
        {
            Environment.Exit(0);
        }

        #endregion

        #region Rm

        private void CommandRm(string[] parameters)
        {
            if (parameters == null)
            {
                Console.WriteLine("rm: missing operand");
                return;
            }
            File.Delete(parameters[0] + ".txt");
        }

        #endregion

        #region Df

        private void CommandDf(string[] parameters)
        {
            DriveInfo[] drive = DriveInfo.GetDrives();
            Console.WriteLine(drive[1].TotalFreeSpace + " bytes");
        }

        #endregion

        #region Du

        private void CommandDu(string[] parameters)
        {
            DriveInfo[] drive = DriveInfo.GetDrives();
            Console.WriteLine((drive[1].TotalSize - drive[1].TotalFreeSpace) + " bytes");
        }

        #endregion

        #region Uname

        private void CommandUname(string[] parameters)
        {
            Console.WriteLine("Windows");
        }

        #endregion

        #region Pwgen (-1)

        private void CommandPwgen(string[] parameters)
        {
            if (parameters == null)
            {
                string[] pwd = new string[64];
                for (int i = 0; i < 64; i++)
                {
                    pwd[i] = Membership.GeneratePassword(8, 2);
                }

                int count = 0;
                for (int i = 0; i < pwd.Length; i++)
                {
                    count++;
                    if (count == 8)
                    {
                        Console.WriteLine(pwd[i]);
                        count = 0;
                    }
                    else Console.Write(pwd[i] + " ");
                                                     
                }
                return;
            }

            if (parameters[0] == "-1") Console.WriteLine(Membership.GeneratePassword(8, 2));
        }

        #endregion
    }
}
