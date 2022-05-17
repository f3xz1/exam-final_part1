using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace ConsoleApp1
{
    class Menu
    {
        public static int choice_unlog()
        {
            Console.Clear();
            while (true)
            {
                Console.WriteLine(
                    "1. Создать аккаунт\n" +
                    "2.Войти в аккаунт");
                int choice = int.Parse(Console.ReadLine());
                if (choice >= 1 && choice <= 2)
                {
                    return choice;
                }
                Console.WriteLine("Выберите номер из предложенных вариантов");
            }
        }
        private static int settings()
        {
            int choice;
            while (true)
            {
                Console.WriteLine(
                    "1.Изменить пароль\n" +
                    "2.Изменить дату рождения");
                choice = int.Parse(Console.ReadLine());
                if (choice >= 1 && choice <= 2)
                {
                    return choice + 9;
                }
                Console.WriteLine("Выберите номер из предложенных вариантов");
            }
        }
        private static int choice_loged(User user)
        {
            while (true)
            {
                Console.WriteLine(
                    "\n1. Начать Викторину\n" +
                    "2. Посмотреть результаты своих прошлых викторин\n" +
                    "3.Посмотреть Топ-20 по конкретной викторине\n" +
                    "4.Настройки\n" +
                    "5.Выход ");
                int choice = int.Parse(Console.ReadLine());

                if (choice >= 1 && choice <= 5)
                {
                    if (choice == 4)
                        return settings();
                    return choice;
                }
                Console.Clear();
                Console.WriteLine("Выберите номер из предложенных вариантов");
            }
        }
        public static void player_menu(User user)
        {
            switch (choice_loged(user))
            {
                case 1:
                    {
                        List<string> theams = new List<string>();
                        theams.AddRange(Directory.GetDirectories(IFiled.question_path));
                        theams = theams.Select(i => i = i.Split("\\")[2]).ToList();
                        Console.WriteLine("Выберите котегорию: ");

                        for (int i = 0; i < theams.Count; i++)
                        {
                            Console.WriteLine(i + 1 + ". " + theams[i]);
                        }
                        int choice = int.Parse(Console.ReadLine());
                        Game game = new Game(user);
                        game.game_start(theams[choice]);
                    }
                    break;
                case 2:
                    {
                        List<Statistics> list = Statistics.Users_Record_list(user.login);
                        foreach (var item in list)
                        {
                            Console.WriteLine(item);
                        }
                        Console.ReadLine();
                    }
                    break;
                case 3:
                    {
                        List<Statistics> list = Statistics.Top20_Record_list();
                        foreach (var item in list)
                        {
                            Console.WriteLine(item);
                        }
                        Console.ReadLine();
                    }
                    break;
                case 10:
                    {
                        Console.WriteLine("Введите старый пароль:");
                        string oldpass = Console.ReadLine();
                        Console.WriteLine("Введите новый пароль:");
                        string newpass = Console.ReadLine();
                        user.chenge_pass(oldpass, newpass);
                        break;
                    }
                case 11:
                    Console.WriteLine("Введите дату рождения(xxxx.xx.xx)");

                    DateTime date = DateTime.Parse(Console.ReadLine());
                    Console.WriteLine("Введите новый пароль:");
                    string pass = Console.ReadLine();
                    user.chenge_bd(pass, date);
                    break;
            }
        }
        public static User log_menu(int choice)
        {
            User reg_user = new User();
            string login;

            switch (choice)
            {
                case 1:
                    {
                        DateTime date;
                        string password;
                        string tmp;
                        while (true)
                        {
                            Console.Write("Введите логин:");
                            login = Console.ReadLine();

                            Console.WriteLine("Введите дату рождения(xxxx.xx.xx)");
                            date = DateTime.Parse(Console.ReadLine());
                            Console.Write("Введите пароль (1):");
                            password = Console.ReadLine();
                            Console.Write("Введите пароль (2):");
                            tmp = Console.ReadLine();
                            if (password != tmp)
                            {
                                Console.WriteLine("Пароли не совпадают");
                            }
                            else
                                reg_user = new User(login, password, date);
                            return reg_user;
                            break;
                        }
                    }
                case 2:
                    {
                        Console.WriteLine("Введите логин:");
                        string log = Console.ReadLine();
                        Console.WriteLine("Введите пароль:");
                        string pass = Console.ReadLine();
                        User log_user = new User(log, pass);
                        return log_user;
                        break;
                    }
                default:
                    throw new Exception("wrong choice");
                    break;
            }
        }
    }
}
