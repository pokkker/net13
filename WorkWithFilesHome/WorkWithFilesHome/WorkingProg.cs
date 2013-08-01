using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace WorkWithFilesHome
{
    static class WorkingProg
    {
        delegate void Searcher(string targetDirectory, DateTime d1, DateTime d2);
        static SortedList sl = new SortedList();
        public static void PathIn()
        {
            // Console.WriteLine("Логические устройства днного компа:"+Directory.GetLogicalDrives().to+"\n");
            Console.WriteLine(@"Поумолчанию директория для поиска имеет путь : " + Directory.GetCurrentDirectory() + "\n");
            Console.WriteLine("Изменить дерикторию? (y/n)");
            switch (Console.ReadLine())
            {
                case "y":
                    {
                        Console.Clear();
                        Console.WriteLine("Введите путь к дериктории с которой будем работать");
                        string path = Console.ReadLine();
                        Console.Clear();
                        Console.WriteLine(path);
                        try
                        {
                            if (Directory.Exists(path))
                            {
                                Directory.SetCurrentDirectory(path);
                                Console.WriteLine("Директория изменена успешно.\n");
                                return;
                            }
                            DirectoryInfo di = Directory.CreateDirectory(path);
                        }
                        catch (DirectoryNotFoundException)
                        {
                            Console.WriteLine("Нет директории с по такому пути :( (попробуйте еще раз)\n");
                            PathIn();
                        }
                        break;
                    }
                case "n":
                    {
                        break;
                    }
                default:
                    {
                        Console.Clear();
                        Console.WriteLine("Ошибка ввода! Попробуйте еще раз!\n");
                        PathIn();
                        break;
                    }
            }
            Console.Clear();
            Console.WriteLine(Directory.GetCurrentDirectory() + "\n");
        }
        static byte ParametrForSwitch()
        {
            byte inp;
            string inpp = Console.ReadLine();
            try
            {
                inp = Convert.ToByte(inpp);
            }
            catch (FormatException)
            {
                Console.WriteLine("Ошибка! Введите цифру, а не букву!");
                Menu();
            }
            return inp = Convert.ToByte(inpp);
        }
        public static void Menu()
        {
            Console.WriteLine("Удалить файл(папку) (1)\nПеремещение файлов(папок) (2)");//nКопирование файлов(папок)(3)\nЗамена в выбраных текстовых файлах одной подстроки на другую (4)");

            switch (ParametrForSwitch())
            {
                case 1:
                    {
                        Console.Clear();
                        Delete();
                        break;
                    }
                case 2:
                    {
                        Console.Clear();
                        Moving();
                        break;
                    }
               /* case 3:
                    {

                        break;
                    }
                case 4:
                    {
                        break;
                    }*/
                default:
                    {
                        Console.WriteLine("Не сществует такого пункта меню!");
                        Menu();
                        break;
                    }
            }
        }

        //Delete
        static void Delete()
        {
            try
            {
                string path;
                PrintSl();
                IList myValueList = sl.GetValueList();
                Console.WriteLine("введите ключ по которому будем удалять");
                path = (string)myValueList[Convert.ToInt32(Console.ReadLine()) - 1];
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                    Console.WriteLine("Удалено");
                }
                if (File.Exists(path))
                {
                    File.Delete(path);
                    Console.WriteLine("Удалено");
                }
            }
            catch (Exception)
            {
                Console.Clear();
                Console.WriteLine("Проверьте правильность ввода");
                Delete();
            }
        }

        //Moving
        static void Moving()
        {
            string path;
            PrintSl();
            IList myValueList = sl.GetValueList();
            Console.WriteLine("введите ключ по которому будем перемещать");
            path = (string)myValueList[Convert.ToInt32(Console.ReadLine()) - 1];
            Console.WriteLine("Введите куда будем перемещать");
            string destinationDirectory = Console.ReadLine();
            try
            {
                Directory.Move(path, destinationDirectory);
            }
            catch (Exception e)
            {
                Console.Clear();
                Console.WriteLine(path);
                Console.WriteLine(e.Message + "\n\nПроверьте правильность ввода");
                Moving();
            }
        }

        //Search
        public static void Search()
        {
            Searcher s;
            DateTime[] dat = new DateTime[2];
            Console.WriteLine("Введите критерий поиска\n\nПо имени (1)\nПо размеру (2)\nПо дате создания (3)\nПо дате доступа (4)\nПо дате модификации (5)\nПо заданной строке (6)\n");
            switch (ParametrForSwitch())
            {
                case 1:
                    {
                        SearchForName(Directory.GetCurrentDirectory());
                        PrintSl();
                        //Console.ReadLine();
                        break;
                    }
                case 2:
                    {
                        SearchForSize(Directory.GetCurrentDirectory());
                        break;
                    }
                case 3:
                    {
                        dat = VvodDati();
                        SearchForDate(Directory.GetCurrentDirectory(), s = new Searcher(WorkingProg.DateDirectoryFile), dat);
                        break;
                    }
                case 4:
                    {
                        dat = VvodDati();
                        SearchForDate(Directory.GetCurrentDirectory(), s = new Searcher(WorkingProg.DateAccessDirectoryFile), dat);
                        break;
                    }
                case 5:
                    {
                        dat = VvodDati();
                        SearchForDate(Directory.GetCurrentDirectory(), s = new Searcher(WorkingProg.DateWriteDirectoryFile), dat);
                        break;
                    }
                case 6:
                    {
                        SearchForString(Directory.GetCurrentDirectory());
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Не сществует такого пункта меню!");
                        Search();
                        break;
                    }
            }
        }

        //поиск по имени
        static void SearchForName(string path)
        {
            Console.WriteLine("Введите имя папки(файла)\n");
            string namen = Console.ReadLine();
            Process(path, namen);
        }
        // RecurseMethodForNameSearch
        static void Process(string targetDirectory, string name)
        {
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
            {
                try
                {
                    if (fileName.Remove(fileName.LastIndexOf(".")) == targetDirectory + @"\" + name)
                    {
                        PrintFile();
                        SaveSizeOrDateSl(fileName);
                    }
                }
                catch (Exception) { }
            }
            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
            {
                if (subdirectory == targetDirectory + @"\" + name)
                {
                    PrintFile();
                    SaveSizeOrDateSl(subdirectory);
                }
                Process(subdirectory, name);
            }
        }

        // ShowFiles
        static void PrintFile()
        {
            Console.WriteLine("Файл c таким именем найден!!!");
        }

        // ShowDirectories
        static void PrintDir()
        {
            Console.WriteLine("папка c таким именем найдена!!!");
        }

        //AddToCollection
        static void SaveSizeOrDateSl(string resultSearchFile)//сохраняет найденные значения по размеру, дате , указанной строке,имени
        {
            sl.Add(Convert.ToString(sl.Count + 1), resultSearchFile);
        }

        //ShowResults
        public static void PrintSl()//вывод на екран результатов поиска
        {
            foreach (DictionaryEntry de in sl)
            {
                Console.WriteLine("Ключь {0} / Файл {1}", de.Key, de.Value);
            }
        }

        //Size
        static void SearchForSize(string path)
        {
            try
            {
                Console.WriteLine("Введите диапазон поиска файла (размер необходимо указать в килобайтах):");
                Console.WriteLine("От ");
                long from = Convert.ToInt64(Console.ReadLine());
                Console.WriteLine("До ");
                long to = Convert.ToInt64(Console.ReadLine());
                from = from * 1024;
                to = to * 1024;
                ProcessFilectorySize(path, from, to);
            }
            catch (FormatException)
            {
                Console.WriteLine("Ошибка формата ввода!");
                SearchForSize(path);
            }
        }
        static void ProcessFilectorySize(string targetDirectory, long from, long to)
        {
            string[] fileEntries = Directory.GetFiles(targetDirectory);

            DirectoryInfo di = new DirectoryInfo(targetDirectory);
            FileInfo[] fiArr = di.GetFiles();
            foreach (FileInfo f in fiArr)
            {
                if (f.Length >= from && f.Length <= to)
                {
                    Console.WriteLine("Файл {0} размер {1}.", f.Name, f.Length);
                    SaveSizeOrDateSl(f.FullName);
                }
            }
            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
                ProcessFilectorySize(subdirectory, from, to);
        }

        //поиск по дате создания (Globalization)
        static DateTime[] VvodDati()
        {
            DateTime[] dt = new DateTime[2];
            try
            {
                Console.WriteLine("Введите диапазон даты в формате (год/месяц/день):");
                Console.WriteLine("Начальная дата:");
                dt[0] = Spliting();
                Console.WriteLine("По дату:");
                dt[1] = Spliting();
                return dt;
            }
            catch (FormatException)
            {
                Console.WriteLine("Ошибка! Нужно вводить цифры!");
                VvodDati();
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Ошибка! Проверьте ввод даты!");
                VvodDati();
            }
            return dt;
        }
        static void SearchForDate(string target, Searcher s, DateTime[] d)
        {
            s(target, d[0], d[1]);
        }
        //Method for Split
        static DateTime Spliting()
        {
            string date = Console.ReadLine();
            string[] split = date.Split(new Char[] { '/' });
            DateTime dt = new DateTime(Convert.ToInt32(split[0]), Convert.ToInt32(split[1]), Convert.ToInt32(split[2]));
            return dt;
        }
        //SearchForCreationDate
        static void DateDirectoryFile(string targetDirectory, DateTime d1, DateTime d2)
        {
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
            {
                if (File.GetCreationTime(fileName) >= d1 && File.GetCreationTime(fileName) <= d2)
                {
                    Console.WriteLine("WE hefe such file");
                    SaveSizeOrDateSl(fileName);
                }
            }
            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
            {
                if (Directory.GetCreationTime(subdirectory) >= d1 && Directory.GetCreationTime(subdirectory) <= d2)
                {
                    Console.WriteLine("WE hefe such Dir");
                    SaveSizeOrDateSl(subdirectory);
                }
                DateDirectoryFile(subdirectory, d1, d2);
            }
        }
        //SearchForAccess
        static void DateAccessDirectoryFile(string targetDirectory, DateTime d1, DateTime d2)
        {
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
            {
                if (File.GetLastAccessTime(fileName) >= d1 && File.GetLastAccessTime(fileName) <= d2)
                {
                    Console.WriteLine("WE hefe such file");
                    SaveSizeOrDateSl(fileName);
                }
            }
            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
            {
                if (Directory.GetCreationTime(subdirectory) >= d1 && Directory.GetCreationTime(subdirectory) <= d2)
                {
                    Console.WriteLine("WE hefe such Dir");
                    SaveSizeOrDateSl(subdirectory);
                }
                DateAccessDirectoryFile(subdirectory, d1, d2);
            }
        }
        //SearchForWriting
        static void DateWriteDirectoryFile(string targetDirectory, DateTime d1, DateTime d2)
        {
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
            {
                if (File.GetLastAccessTime(fileName) >= d1 && File.GetLastAccessTime(fileName) <= d2)
                {
                    Console.WriteLine("WE hefe such file");
                    SaveSizeOrDateSl(fileName);
                }
            }
            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
            {
                if (Directory.GetCreationTime(subdirectory) >= d1 && Directory.GetCreationTime(subdirectory) <= d2)
                {
                    Console.WriteLine("WE hefe such Dir");
                    SaveSizeOrDateSl(subdirectory);
                }
                DateWriteDirectoryFile(subdirectory, d1, d2);
            }
        }

        //SearchForString
        static void SearchForString(string path)
        {
            Console.WriteLine("Введите строку для поиска файла:");
            string forSearch = Console.ReadLine();
            ProcessFileSearchString(path, forSearch);
        }
        static void ProcessFileSearchString(string path, string forSearch)
        {
            string[] fileEntries = Directory.GetFiles(path, "*.txt", SearchOption.AllDirectories);
            string st;
            foreach (string item in fileEntries)
            {
                StreamReader sr = new StreamReader(item);
                while (!sr.EndOfStream)
                {
                    st = sr.ReadLine();
                    if (st.StartsWith(forSearch))
                    {
                        Console.WriteLine("ПОИСК ПО СТОРОКЕ В ФАЙЛЕ УСПЕШЕН!!!");
                        SaveSizeOrDateSl(item);
                        break;
                    }
                }
            }
        }
    }
}

