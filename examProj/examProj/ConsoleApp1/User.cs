using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace ConsoleApp1
{
    class User : IFiled
    {
        public string login { get; set; }
        public string password { get; set; }
        public DateTime birthday { get; set; }
        public User(string login, string password, DateTime birthday)
        {
            this.login = login;
            this.password = password;
            this.birthday = birthday;
        }
        public User()
        {

        }
        public User(string log, string pass)
        {
            this.logining(log, pass);
        }
        public void logining(string login, string password)
        {
            if (!File.Exists(IFiled.user_path + "\\" + login + $"\\{login}.json"))
            {
                Console.WriteLine("Неверный логин или пароль");
                return;
            }
            User temp = null;
            using (StreamReader reader = new StreamReader(IFiled.user_path + "\\" + login + $"\\{login}.json", true))
            {
                temp = JsonConvert.DeserializeObject<User>(reader.ReadLine());
            }
            if (temp.login == login && temp.password == password)
            {
                this.birthday = temp.birthday;
            }
            else
            {
                Console.WriteLine("Неверный логин или пароль");
                return;
            }
            Console.WriteLine($"Добро пожаловать {login}");
            return;
        }
        public void del_user(string password)
        {
            if (this.password == password)
            {
                File.Delete(IFiled.user_path + "\\" + login + "\\" + login + ".dat");
                Directory.Delete(IFiled.user_path + "\\" + login);
                Console.WriteLine("Аккаунт успешно удален");
            }
            else
            {
                Console.WriteLine("Неверный пароль");
            }
        }
        public void chenge_pass(string old, string @new)
        {
            if (this.password == old)
            {
                this.password = @new;
                this.add_toFile();
                Console.WriteLine("Пароль успешно изменен");
            }
            else
            {
                Console.WriteLine("Неверный старый пароль");
            }
        }
        public void chenge_bd(string pass, DateTime date)
        {
            if (this.password == pass)
            {
                this.birthday = date;
                this.add_toFile();
                Console.WriteLine("Дата рождения успешно изменена");
            }
            else
            {
                Console.WriteLine("Неверный пароль");
            }
        }
        public void add_toFile()
        {
            if (File.Exists(IFiled.user_path + "\\" + login + $"\\{login}.json"))
            {
                Console.WriteLine("Пользователь с таким логином уже существует");
            }
            try
            {
                Directory.CreateDirectory(IFiled.user_path + "\\" + login);


                JsonSerializer serializer = new JsonSerializer();

                File.Create(IFiled.user_path + "\\" + login + "\\RecordList.json");
                using (StreamWriter file = new StreamWriter(IFiled.user_path + "\\" + login + $"\\{login}.json", false))
                {
                    serializer.Serialize(file, this);
                }
            }
            catch (Exception)
            {
                throw new Exception("register break");
            }
        }

        public override string ToString()
        {
            return this.login;
        }

    }
}
