
using System;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace _21Ochko_Serialize_
{
    /*//Interface for getting a result of everybody in the game
    interface IRezultable
    {
        bool Result(Table t);
        bool GetCountPlayer(Table t);
        bool GetCountDealer(Table t);
    }
    public interface IDistributeCart
    {
        void DistributeCart(_21Ochko.Table t, bool choise);
    }
    interface IVisualizable
    {
        void Show(Table t);
    }*/
    class Casino//:IDistributeCart,IRezultable,IVisualizable
    {
        private const int _kriterium = 17;//criterion of diler passing
        private bool _choise = true;
        private static bool _first = true;//First enter to game
        private int _tableChoise;
        FileStream _t0f = new FileStream("t0.dat", FileMode.Create, FileAccess.ReadWrite);
        FileStream _t1f = new FileStream("t1.dat", FileMode.Create, FileAccess.ReadWrite);
        FileStream _t2f = new FileStream("t2.dat", FileMode.Create, FileAccess.ReadWrite);
        FileStream _t3f = new FileStream("t3.dat", FileMode.Create, FileAccess.ReadWrite);
        FileStream _t4f = new FileStream("t4.dat", FileMode.Create, FileAccess.ReadWrite);
        FileStream _t5f = new FileStream("t5.dat", FileMode.Create, FileAccess.ReadWrite);
        FileStream _t6f = new FileStream("t6.dat", FileMode.Create, FileAccess.ReadWrite);
        FileStream _t7f = new FileStream("t7.dat", FileMode.Create, FileAccess.ReadWrite);
        FileStream _t8f = new FileStream("t8.dat", FileMode.Create, FileAccess.ReadWrite);
        FileStream _t9f = new FileStream("t9.dat", FileMode.Create, FileAccess.ReadWrite);
        BinaryFormatter bf = new BinaryFormatter();
        private static Table t;
        private void Deserial()
        {
            Console.WriteLine("\nВведите номер стола! <0-9>");
            var keypress = Console.ReadKey();
            switch (keypress.Key)
            {
                case (ConsoleKey)'1':
                    {
                        _tableChoise = 1;
                        try
                        {
                            t = (Table)bf.Deserialize(_t1f);
                        }
                        catch (Exception)
                        {
                            t = new Table();
                        }
                        break;
                    }
                case (ConsoleKey)'2':
                    {
                        _tableChoise = 2;
                        try
                        {
                            t = (Table)bf.Deserialize(_t2f);
                        }
                        catch (Exception)
                        {
                            t = new Table();
                        }
                        break;
                    }
                case (ConsoleKey)'3':
                    {
                        _tableChoise = 3;
                        try
                        {
                            t = (Table)bf.Deserialize(_t3f);
                        }
                        catch (Exception)
                        {
                            t = new Table();
                        }
                        break;
                    }
                case (ConsoleKey)'4':
                    {
                        _tableChoise = 4;
                        try
                        {
                            t = (Table)bf.Deserialize(_t4f);
                        }
                        catch (Exception)
                        {
                            t = new Table();
                        }
                        break;
                    }
                case (ConsoleKey)'5':
                    {
                        _tableChoise = 5;
                        try
                        {
                            t = (Table)bf.Deserialize(_t5f);
                        }
                        catch (Exception)
                        {
                            t = new Table();
                        }
                        break;
                    }
                case (ConsoleKey)'6':
                    {
                        _tableChoise = 6;
                        try
                        {
                            t = (Table)bf.Deserialize(_t6f);
                        }
                        catch (Exception)
                        {
                            t = new Table();
                        }
                        break;
                    }
                case (ConsoleKey)'7':
                    {
                        _tableChoise = 7;
                        try
                        {
                            t = (Table)bf.Deserialize(_t7f);
                        }
                        catch (Exception)
                        {
                            t = new Table();
                        }
                        break;
                    }
                case (ConsoleKey)'8':
                    {
                        _tableChoise = 8;
                        try
                        {
                            t = (Table)bf.Deserialize(_t8f);
                        }
                        catch (Exception)
                        {
                            t = new Table();
                        }
                        break;
                    }
                case (ConsoleKey)'9':
                    {
                        _tableChoise = 9;
                        try
                        {
                            t = (Table)bf.Deserialize(_t9f);
                        }
                        catch (Exception)
                        {
                            t = new Table();
                        }
                        break;
                    }
                case (ConsoleKey)'0':
                    {
                        _tableChoise = 0;
                        try
                        {
                            t = (Table)bf.Deserialize(_t0f);
                        }
                        catch (Exception)
                        {
                            t = new Table();
                        }
                        break;
                    }
                default:
                    {
                        Console.Clear();
                        Console.WriteLine("\nНекоректный ввод! Попробуйте еще раз!!!");
                        Deserial();
                        break;
                    }
            }
        }
        public void DistributeCart(bool choise)
        {
            var r = new Random();
            var k = r.Next(1, t.Deck.Count);
            if (choise)
            {
                t.Player.Add(t.Deck[k]);
                t.Deck.RemoveAt(k);
            }
            else
            {
                if (GetCountDealer())
                {
                    t.Dealer.Add(t.Deck[k]);
                    t.Deck.RemoveAt(k);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Игра закончена!");
                    Show();
                }
            }
        }
        public void Show()
        {
            var sump = t.Player.Cast<int>().Sum();
            var sumd = t.Dealer.Cast<int>().Sum();
            Console.WriteLine("Ваш счет    " + sump + "\nСчет Дилера " + sumd);
            if (sump == sumd)
            {
                Console.WriteLine("\nНичья");
                if (sumd >= 17 && sumd < 22)
                    Console.WriteLine("\nЛогика Дилера построена на том, что ключевым числом является 17! Свыше-он открывается");
                return;
            }
            if ((sumd > sump && sumd < 22) || (sumd < sump && sumd >= 22))
            {
                Console.WriteLine("\nДилер выиграл!");
                if (sumd >= 17 && sumd < 22)
                    Console.WriteLine("\nЛогика Дилера построена на том, что ключевым числом является 17! Свыше-он открывается");
            }
            else
                if ((sump < 22 && sump != sumd) || (sump < sumd && sump >= 22))
                {
                    Console.WriteLine("Вы выиграли)");
                    if (sumd >= 17 && sumd < 22)
                        Console.WriteLine("\nЛогика Дилера построена на том, что ключевым числом является 17! Свыше-он открывается");
                }
                else
                    Console.WriteLine("Вы проиграли(");
            Console.WriteLine();
        }
        public bool Result()
        {
            var sumd = t.Dealer.Cast<int>().Sum();
            if (sumd >= _kriterium)
                return false;
            var sump = t.Player.Cast<int>().Sum();
            if (sumd <= 21 && sump <= 21)
                return true;
            else
            {
                Console.Clear();
                Console.WriteLine("Игра окончена!\nСумма дилера {0}\nСумма игрока {1}", sumd, sump);
                return false;
            }
        }
        public bool GetCountPlayer()
        {
            var sump = 0;
            Console.WriteLine();
            for (var i = 0; i < t.Player.Count; i++)
            {
                Console.WriteLine("{0}-я катра {1}-цена карты", i + 1, t.Player[i]);
                sump += (int)t.Player[i];
            }
            Console.WriteLine("Ваш счет " + sump + "\n\nПродолжить брать карту-<1>\nОткрыться-<2>\nПропускаю ход-<3>\nперейти на другой стол-<ALt>+<F>");
            var keypress = Console.ReadKey();
            if ((ConsoleModifiers.Alt & keypress.Modifiers) != 0)
            {
                if (keypress.KeyChar == 'f')
                {
                    Console.Clear();
                    switch (_tableChoise)
                    {
                        case 1:
                            {
                                bf.Serialize(_t1f, t);
                                break;
                            }
                        case 2:
                            {
                                bf.Serialize(_t2f, t);
                                break;
                            }
                        case 3:
                            {
                                bf.Serialize(_t3f, t);
                                break;
                            }
                        case 4:
                            {
                                bf.Serialize(_t4f, t);
                                break;
                            }
                        case 5:
                            {
                                bf.Serialize(_t5f, t);
                                break;
                            }
                        case 6:
                            {
                                bf.Serialize(_t6f, t);
                                break;
                            }
                        case 7:
                            {
                                bf.Serialize(_t7f, t);
                                break;
                            }
                        case 8:
                            {
                                bf.Serialize(_t8f, t);
                                break;
                            }
                        case 9:
                            {
                                bf.Serialize(_t9f, t);
                                break;
                            }
                        case 0:
                            {
                                bf.Serialize(_t0f, t);
                                break;
                            }
                    }
                    Deserial();
                    return false;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Некоректный ввод! Попробуйте еще раз!!!");
                    GetCountPlayer();
                }
                return false;
            }
            switch (keypress.Key)
            {
                case (ConsoleKey)'1':
                    {
                        return true;
                    }
                case (ConsoleKey)'2':
                    {
                        _choise = false;
                        return false;
                    }
                case (ConsoleKey)'3':
                    {
                        _choise = true;
                        return false;
                    }
                default:
                    {
                        Console.Clear();
                        Console.WriteLine("Некоректный ввод! Попробуйте еще раз!!!");
                        GetCountPlayer();
                        return false;
                    }
            }
        }
        public bool GetCountDealer()
        {
            var sumd = t.Dealer.Cast<int>().Sum();
            return sumd < _kriterium;
        }

        public static void Shuffle()
        {
            var c = new Casino();
            Console.Clear();
            Console.WriteLine("Нaчнем ИГРУ!!!");
            var b = true;
            while (b)
            {
                if (_first)
                {
                    c.Deserial();
                    _first = false;
                }
                else
                {
                    t = new Table();
                }
                Console.Clear();
                c.DistributeCart(true);
                c.DistributeCart(true);
                c.DistributeCart(false);
                c.DistributeCart(false);
                while (c.Result())
                {
                    if (c.GetCountPlayer())
                    {
                        c.DistributeCart(true);
                        c.DistributeCart(false);
                    }
                    else
                        if (c._choise)
                            c.DistributeCart(false);
                        else
                            break;
                }
                Console.Clear();
                c.Show();
                Console.WriteLine("Чтобы нaчать игру заново нажмите  <y>");
                if (Console.ReadLine() == "y") continue;
                b = false;
                Console.Clear();
                Console.WriteLine("Game over!");
                Console.ReadKey();
            }
        }
    }
}