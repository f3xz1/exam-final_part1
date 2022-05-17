using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections;

namespace ConsoleApp1
{
    class Question : IFiled
    {
        string quest;
        List<string> L_answers;
        string W_answer;
        public Question(string quest, string W_answer, List<string> answer)
        {
            this.quest = quest;
            this.W_answer = W_answer;
            this.L_answers = answer;
        }
        public bool asking()
        {
            Random r = new Random();
            List<string> temp = this.L_answers;
            int get_ans;
            int rand = r.Next(temp.Count);
            temp.Insert(rand, this.W_answer);
            Console.WriteLine($"{quest}\n\n1. {temp[0]}\t2. {temp[1]}\n3. {temp[2]}\t4. {temp[3]}\n");
            try
            {
                get_ans = int.Parse(Console.ReadLine());
            }
            catch (Exception)/////////////////////////////
            {
                throw;
            }
            if (get_ans == rand + 1)
                return true;
            return false;
        }

        public static List<Question> Get_Guests(string theam_name)
        {
            using (StreamReader reader = new StreamReader(IFiled.question_path + "\\" + theam_name + "\\" + theam_name + ".json"))
            {
                return JsonConvert.DeserializeObject<List<Question>>(reader.ReadLine());
            }
        }
    }
}
