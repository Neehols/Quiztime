using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizAdmin
{
    public class Quiz
    {
        // make sure you can access/use the Database.cs files functions in order to 'launch' these SQL queries so you can change the database
        Database database;
        public Quiz(Database db)
        {
            database = db;
        }
        public void CreateQuiz(string quizName)
        {
            database.CustomQuery(String.Format("INSERT INTO `quizzes` (`quiz_name`) VALUES ('{0}')", quizName));
        }
        public void ChangeQuiz(string quizId, string newQuiz)
        {
            database.CustomQuery(String.Format("UPDATE `quizzes` SET quiz_name='{0}' where quiz_id='{1}'", newQuiz, quizId));
        }
        public void ChangeQuestion(string oldQuestionId, string newQuestion)
        {
            database.CustomQuery(String.Format("UPDATE `quizzquestions` SET quizz_question='{0}' where quizz_question_id='{1}'", newQuestion, oldQuestionId));
        }
        public void SetQuestion(string quizId, string question)
        {
            database.CustomQuery(String.Format("INSERT INTO `quizzquestions` (`quizz_question`, `quizz_id`) VALUES ('{0}', '{1}')", question, quizId));
        }
        public Dictionary<string, string> AvailableQuizzes() // below query word doorgevoerd naar een andere class, in die andere class ga je dingen doen met deze query
        {
                return database.QueryColsToDictionary("SELECT * from `quizzes`", "quiz_id", "quiz_name");
        }
        public string QuizId(string quizName)
        {
            return database.QueryColToString(String.Format("SELECT `quiz_id` from `quizzes` where quiz_name = '{0}'", quizName), "quiz_id");
        }
        public Dictionary<string, string> Questions(string quizId) // below query selects all questions and ID of questions of the quizz you have selected in Form1.cs[Design] AKA the quizId found in the query
        {
            return database.QueryColsToDictionary(String.Format("SELECT `quizz_question`,`quizz_question_id` from `quizzquestions` where quizz_id={0}", quizId), "quizz_question_id", "quizz_question");
        }
        public void UpdateRightAnswer(string questionId, string answerId)
        {
            database.CustomQuery(String.Format("UPDATE `quizanswers` SET answer_right='NO' where quizz_question_id={0}",questionId));
            database.CustomQuery(String.Format("UPDATE `quizanswers` SET answer_right='YES' where quizz_question_id='{0}' and answer_id='{1}'", questionId, answerId));
        }

        // 
        public void UpdateAnswer(string newAnswer, string questionId, string answerId)
        {
            database.CustomQuery(String.Format("UPDATE `quizanswers` SET quiz_answer='{0}' where quizz_question_id='{1}' and answer_id='{2}'", newAnswer, questionId, answerId));
        }
        public void UploadPictureToQuestion(string picPath, string questionId)
        {
            database.CustomQuery(String.Format("UPDATE `quizzquestions` SET question_pic='{0}' where quizz_question_id='{1}'", picPath, questionId));
        }
        public void DeletePictureInQuestion(string questionId)
        {
            database.CustomQuery(String.Format("UPDATE `quizzquestions` SET question_pic='' where quizz_question_id='{0}'",questionId));
        }
        // SetAnswer sets the answer to Right answer, to YES
        public void SetAnswer(string answer, string questionId)
        {
            database.CustomQuery(String.Format("INSERT INTO `quizanswers` (`quiz_answer`, `quizz_question_id`, `answer_right`) VALUES ('{0}','{1}', 'NO')", answer, questionId));
        }
        public void SetAnswer(string answer, string questionId, string answerRight)
        {
            database.CustomQuery(String.Format("INSERT INTO `quizanswers` (`quiz_answer`, `quizz_question_id`, `answer_right`) VALUES ('{0}','{1}','{2}')", answer, questionId, answerRight));
        }
        public void DeleteQuiz(string quizId)
        {
            database.CustomQuery(String.Format("DELETE FROM `quizanswers` WHERE quizz_question_id IN( select * from ( SELECT quizz_question_id FROM quizzquestions where quizz_id = '{0}' ) as x );", quizId));
            database.CustomQuery(String.Format("DELETE FROM `quizzquestions` where quizz_id={0}", quizId));
            database.CustomQuery(String.Format("DELETE FROM `quizzes` where quiz_id={0}", quizId));

        }
        public void DeleteQuestion(string questionId)
        {
            database.CustomQuery(String.Format("DELETE FROM `quizanswers` WHERE quizz_question_id={0};", questionId));
            database.CustomQuery(String.Format("DELETE FROM `quizzquestions` where quizz_question_id={0}", questionId));
        }
        public string QuestionCount(string quizId)
        {
            return database.QueryColToString(String.Format("select count(quizz_question_id) from quizzquestions where quizz_id={0}", quizId), "count(quizz_question_id)");
        }
        public string GetMaxId()
        {
         return database.QueryColToString("SELECT MAX(answer_id) FROM quizanswers","MAX(answer_id)");
        }
    }
}
