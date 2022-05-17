using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections;

namespace ConsoleApp1
{
    class Statistics
    {
        public string user_login { get; set; }
        public int score { get; set; }
        public double time { get; set; }
        public Statistics(string login, int score, double time)
        {
            this.user_login = login;
            this.score = score;
            this.time = time;
        }

        public void Add_Stat()
        {
            JsonSerializer serializer = new JsonSerializer();
            using (StreamWriter writer = new StreamWriter(IFiled.user_path + "\\" + this.user_login + "RecordList.json", true))
            {
                serializer.Serialize(writer, this);
            }
        }
        public static List<Statistics> Users_Record_list(string user_login)
        {
            using (StreamReader reader = new StreamReader(IFiled.user_path + "\\" + user_login + "\\RecordList.json"))
            {
                return JsonConvert.DeserializeObject<List<Statistics>>(reader.ReadLine());
            }
        }
        public static List<Statistics> Top20_Record_list()
        {
            List<string> list_of_users = new List<string>();
            List<Statistics> list_of_stats = new List<Statistics>();
            list_of_users.AddRange(Directory.GetFiles(IFiled.user_path));

            foreach (var item in list_of_users)
            {
                list_of_stats.AddRange(Statistics.Users_Record_list(item));
            }
            list_of_stats.Sort();
            return list_of_stats.GetRange(0, 20);
        }
        public override string ToString()
        {
            return new string($"Счет: {score}\nВремя {time}");
        }
        public static bool operator >(Statistics a, Statistics b)
        {
            return a.time > b.time && a.score > b.score;
        }
        public static bool operator <(Statistics a, Statistics b)
        {
            return a.time < b.time && a.score < b.score;
        }
    }
}
