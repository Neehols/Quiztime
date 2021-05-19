using System;
using System.Collections.Generic;

namespace QuizApp
{ 
    public class Answers
    {
        Database database;
        public string QuizAnswer { get; set; } = "Answer";
        public answerRight AnswerRight { get; set; }
        public string Question { get; set; }
        public string QuestionPicturePath { get; set; }

        public Answers(Database db)
        {
            database = db;
        }
        private Answers(string quizanswer, answerRight answerright, string question)
        {
            AnswerRight = answerright;
            QuizAnswer = quizanswer;
            Question = question;

        }
        private Answers()
        {

        }
        public List<Answers> QueryGetAnswers(string query)
        {
            var result = new List<Answers>();
            var wholeTable = database.Query4ColsToTuple(query, "quiz_answer", "answer_right", "quizz_question", "question_pic");
            foreach (Tuple<string, string, string, string> table in wholeTable)
            {
                result.Add(new Answers() { QuizAnswer = table.Item1, AnswerRight = (answerRight)Enum.Parse(typeof(answerRight), table.Item2), Question = table.Item3, QuestionPicturePath = table.Item4 });
            }
            return result;
        }
        public enum answerRight
        {
            YES,
            NO
        }
    }
}