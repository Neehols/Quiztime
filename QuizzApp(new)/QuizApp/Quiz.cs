using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp
{
    public class Quiz
    {
        Database database;
        Answers answer;
        public Quiz(Database db)
        {
            database = db;
            answer = new Answers(db);
        }
        public Dictionary<string, string> AvailableQuizzes()
        {
            return database.QueryColsToDictionary("SELECT * from `quizzes`", "quiz_id", "quiz_name");
        }
        public List<Answers> QuestionsAndAnswers(string quizId)
        {
            return answer.QueryGetAnswers(String.Format("SELECT quiz_answer,answer_right,question.quizz_question,question.question_pic FROM quizanswers JOIN `quizzquestions` question ON question.quizz_question_id= quizanswers.quizz_question_id where question.quizz_id = {0}", quizId));
        }
        public string QuestionCount(string quizId)
        {
            return database.QueryColToString(String.Format("select count(quizz_question_id) from quizzquestions where quizz_id={0}", quizId), "count(quizz_question_id)");
        }
    }
}
