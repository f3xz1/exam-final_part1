using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections;

namespace ConsoleApp1
{
    class admin_mode : IFiled
    {
        string log;
        string pass;
        public admin_mode(string log, string pass)
        {
            this.log = "admin";
            this.pass = "admin";
            if (this.log == log && this.pass == pass)
            {
                Console.WriteLine("Welcome");
            }
            else
            {
                throw new Exception("Brake");
            }
        }
        public static void add_theam(string theam_name, List<Question> questions)
        {
            JsonSerializer serializer = new JsonSerializer();
            File.Create(IFiled.question_path + "\\" + theam_name + ".json");
            using (StreamWriter wt = new StreamWriter(IFiled.question_path+ "\\" + theam_name + ".json", false))///////////////////
            {
                serializer.Serialize(wt, questions);
            }
        }
        public static void delete_theam(string theam_name)
        {
            if (Directory.Exists(IFiled.question_path +"\\" + theam_name))
            {
                File.Delete(IFiled.question_path+ "\\" + theam_name + ".json");
            }
        }
        public static void delete_user(string user)
        {
            if (File.Exists(IFiled.user_path + "\\" + user))
            {
                File.Delete(IFiled.user_path + "\\" + user + "\\" + user + ".json");
                File.Delete(IFiled.user_path + "\\" + user + "\\RecordList.json");
                Directory.Delete(IFiled.user_path + "\\" + user);
            }
        }
    }
    class admin_menu
    {
        static public int choice()
        {
            while (true)
            {

            Console.WriteLine("1.Добавить тему" +
                "\n2.Удалить тему" +
                "\n3.Удалить пользователя" +
                "\n4.Выйти");
            int choice = int.Parse(Console.ReadLine());
            if(!(choice >0 && choice < 5))
                {
                    Console.WriteLine("Выберите номер из предложенных вариантов");
                }
                else
                {
                    return choice;
                }
            }
        }

        static public void realiz(int choice)
        {
            switch (choice)
            {
                case 1:
                    {

                    List<Question> questions = new List<Question>();
                    Console.WriteLine("Введите название темы: ");
                    string theam_name = Console.ReadLine();
                    Console.WriteLine("Введите количество вопрсов: ");
                    int count = int.Parse(Console.ReadLine());

                    string quest;
                    string W_ans;
                    List<string> L_ans = new List<string>();
                    for (int i = 0; i < count; i++)
                    {
                        Console.WriteLine("Введите вопрос: ");
                        quest = Console.ReadLine();
                        Console.WriteLine("Введите правельный ответ: ");
                        W_ans = Console.ReadLine();
                        for (int j = 0; j < 3; j++)
                        {
                            Console.WriteLine("Введите 3 неправельных ответа");
                            L_ans.Add(Console.ReadLine());
                        }
                        Question question = new Question(quest,W_ans,L_ans);
                        L_ans.Clear();
                        questions.Add(question);
                    }
                    admin_mode.add_theam(theam_name,questions);
                    }
                    break;
                case 2:
                    {
                    Console.WriteLine("Введите название темы: ");
                    string theam_name = Console.ReadLine();
                    admin_mode.delete_theam(theam_name);
                    }
                    break;
                case 3:
                    Console.WriteLine("Введите логин пользователя: ");
                    admin_mode.delete_user(Console.ReadLine());
                    break;
                case 4:
                    return;
                    break;
                default:
                    break;
            }
        }


    }
    class admin
    {
        static void Main(string[] args)
        {
            admin_menu.realiz(admin_menu.choice());
        }
    }
}