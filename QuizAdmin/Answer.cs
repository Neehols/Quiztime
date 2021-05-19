using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizAdmin
{
    public class Answer
    {
        Database database;

        // get; set; is nodig om een list te maken, dit zie je beneden
        public string QuizAnswer { get; set; } = "Answer";
        public answerRight AnswerRight { get; set; }
        public int QuestionId { get; set; }

        public int AnswerId { get; set; }

        public Answer(Database db)
        {
            database = db;
        }


        private Answer()
        {

        }
        // makes a list of Answer object, with properties, to access use code like list[0].AnswerId
        public List<Answer> QueryGetAnswers(string query)
        {
            var tempList = new List<Answer>();
            var table = database.Query4ColsToTuple(query, "answer_id","quiz_answer", "answer_right", "quizz_question_id");
            foreach(Tuple<string,string,string,string> tableLine in table)
            {
                tempList.Add(new Answer() { AnswerId = int.Parse(tableLine.Item1), QuizAnswer = tableLine.Item2, AnswerRight = (answerRight)Enum.Parse(typeof(answerRight),tableLine.Item3), QuestionId = Convert.ToInt32(tableLine.Item4)});
            }
            return tempList;
        }
        public enum answerRight // set YES and NO, condition to execute this function is to right click on an answer
        {
            YES,
            NO
        }
    }
}
