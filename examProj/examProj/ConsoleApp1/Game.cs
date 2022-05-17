using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections;
namespace ConsoleApp1
{
    class Game
    {
        User user;
        public Game(User user)
        {
            this.user = user;
        }
        public void game_start(string name_theam)
        {
            DateTime timestart = DateTime.Now;
            int score = default;
            List<Question> questions = Question.Get_Guests(name_theam);

            for (int i = 0; i < questions.Count; i++)
            {
                Console.WriteLine(i);
                if (questions[i].asking())
                    score++;
            }
            DateTime timeend = DateTime.Now;

            Statistics @new = new Statistics(user.login, score, timeend.Second - timestart.Second);
            @new.Add_Stat();
        }
    }
}
