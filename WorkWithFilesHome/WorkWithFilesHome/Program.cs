using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WorkWithFilesHome
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Привет, пользователь!\n");
            bool what = true;
            while (what)
            {
                WorkingProg.PathIn();
                while (what)
                {
                    WorkingProg.Search();
                    Console.WriteLine("Продолжить поиск?(y/n)");
                    what = Deside(Console.ReadLine());
                }
                Console.Clear();
                Console.WriteLine("Получнный список файлов:\n");
                WorkingProg.PrintSl();
                Console.WriteLine("Поработаем с ним\n");
                WorkingProg.Menu();
                Console.WriteLine("Хотите продолжать работу программы?(y/n)");
                what = Deside(Console.ReadLine());
                Console.Clear();
            }
        }
        public static bool Deside(string s)
        {
            bool w;
            switch (s)
            {
                case "y":
                    {
                        w = true;
                        break;
                    }
                case "n":
                    {
                        w = false;
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Пока!");
                        w = false;
                        break;
                    }
            }
            return w;
        }
    }
}
