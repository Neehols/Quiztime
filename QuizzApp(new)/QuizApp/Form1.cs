using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuizApp
{
    public partial class Form1 : Form
    {
        // make sure we can use other classes/files
        Database database;
        Quiz quiz;
        // Database database1;
        // these variables contain ID's, questions and pictures of a question
        string[] quizIds = { };
        string[] questions = { };
        string[] questionPictures = { };
        string time;
        // een C# list met answers (antwoorden)
        List<Answers> answers = new List<Answers>();
        public Form1()
        {

            string connectionString = "";
            try
            {
                // gebruikt connectionstring.txt file
                connectionString = File.ReadAllText(Directory.GetCurrentDirectory() + @"\connectionstring.txt");
            }
            catch
            {
                MessageBox.Show("Please make sure connectionstring.txt is in the same folder as this program.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }

            // ook mogelijk, database returns "hello", ONLY string works
            //database1 = new Database(test());
    
            // pass connectionString over to the database class en return this data (connection to database) in variable(change this) database (in database code staat een return)
            database = new Database(connectionString);
            // database is de connection naar database (deze word ge'return'ed in Database.cs)
            quiz = new Quiz(database);
            InitializeComponent();
            updateQuizzes();

            //fullscreen
            //FormBorderStyle = FormBorderStyle.None;
            //WindowState = FormWindowState.Maximized;

            richTextBox1.Visible = true;
        }

        //public string test()
        //{
        //    string echo = "hello";
        //    return echo;
        //}

        // haal meer data op (voor om de quiz te kunnen spelen) en doe die in de variabelen die helemaal boven staan
        private void loadAnswers(string quizId)
        {
            // doet alle answers in een list van de geselecteerde quiz
            answers = quiz.QuestionsAndAnswers(quizId);
            // pakt alle question
            questions = answers.Select(x => x.Question).Distinct().ToArray();
            // pakt alle pictures
            questionPictures = answers.Select(x => x.QuestionPicturePath).Distinct().ToArray();
        }

        // laat quizzes zien in combobox
        private void updateQuizzes()
        {
            // haal quizzes van de database
            var availableQuizzes = quiz.AvailableQuizzes();
            // doe de quizzes die je uit de database hebt gehaald in de combobox
            comboBoxQuiz.DataSource = availableQuizzes.Values.ToArray();
            // de data die je krijgt is een dictionary met quiz_id als key en quiz_name als value, met deze code selecteer je de Key AKA quiz_id in dit geval
            // en de variabele die deze value krijgt heet quizIds
            quizIds = availableQuizzes.Keys.ToArray();
            // als quizIds een value heeft... oftewel if quizzes exists (cause they have a number AKA id's)
            if (quizIds.Length != 0)
            {
                // tel hoeveel vragen de geselecteerde quizz (geselecteerde quizz is [0]) heeft
                var firstQuizQuestionCount = quiz.QuestionCount(quizIds[0]);
                // van de string een nummer maken en als de vragen minder dan 10 zijn..
                if (int.Parse(firstQuizQuestionCount) < 10)
                {
                    btnStartQuiz.ForeColor = Color.Red; // makes sure we cant start incomplete quizz (with less than 10 questions)
                    btnStartQuiz.Tag = firstQuizQuestionCount;
                }
            }
                // if no quizzes found/exist, reset/delete the data inside the combobox
                if (availableQuizzes.Keys.Count == 0)
                {
                    comboBoxQuiz.ResetText();
                    comboBoxQuiz.SelectedIndex = -1;
                }    
        }

        // reload button voor het reloaden van de quizzes (AKA C# dictionary updaten)
        private void BtnReload_Click(object sender, EventArgs e)
        {
            updateQuizzes();
        }

        // checked voor quizzes die je kan spelen
        private void ComboBoxQuiz_SelectedIndexChanged(object sender, EventArgs e)
        {
            // if combobox contains values AKA quizzes
            if (comboBoxQuiz.Items.Count != 0 && quizIds.Length != 0)
            {
                // telt op hoeveel vragen (de ID's) een quizz heeft
                var QuizQuestionCount = quiz.QuestionCount(quizIds[comboBoxQuiz.SelectedIndex]);
                // if less than 10 questions prevent to start the quizz
                if (int.Parse(QuizQuestionCount) < 10)
                {
                    btnStartQuiz.ForeColor = Color.Red;
                    btnStartQuiz.Tag = QuizQuestionCount;
                }
                // you can start the quizz
                else
                {
                    btnStartQuiz.ForeColor = SystemColors.ControlText;
                    btnStartQuiz.Tag = null;
                    loadAnswers(quizIds[comboBoxQuiz.SelectedIndex]);
                }
            }
        }

        // select time with radiobuttons
        private void RadioButtons_CheckedChanged(object sender, EventArgs e)
        {
            // als je een quizz hebt geselecteerd, de start quiz button enablen
            if (comboBoxQuiz.Items.Count != 0)
                btnStartQuiz.Enabled = true;
            // the .Text waar de timer komt te staan, we pakken hiervan de seconden die ja kan selecteren met radio buttons
            time = ((RadioButton)sender).Text.Substring(0, 2);
        }

        // button om quiz te starten
        private void BtnStartQuiz_Click(object sender, EventArgs e)
        {
            ComboBoxQuiz_SelectedIndexChanged(new { }, null);
            // make sure we cant start incomplete quizzes (in de andere if boven is dat ook gedaan met code)
            if (btnStartQuiz.ForeColor == Color.Red)
            {
                MessageBox.Show(String.Format("Sorry, the selected quiz only has {0} question(s) and the minimum is 10 questions", btnStartQuiz.Tag), "Unable to start quiz", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            // elke keer als we een quiz starten word currentQuestion meteen gezet naar 0 bijvoorbeeld om zo altijd te beginnen met de eerste vraag van de
            // geselcteerde quizz
            else
            {
                // loads all answers of selected quizz
                //loadAnswers(quizIds[comboBoxQuiz.SelectedIndex]);
                // disables the quizz reload button so you cant do anything else other than playing the quizz
                btnReload.Enabled = false;
                // disables the quizz start button so you cant do anything else other than playing the quizz
                btnStartQuiz.Enabled = false;
                // disable panel1 AKA waar je de secondes kan selecteren voor de timer
                panel1.Enabled = false;
                // disable combobox quizz waar je de quizz kan selecteren
                comboBoxQuiz.Enabled = false;
                // de rechtertextbox, daar komen alle vragen en antwoorden van de geselecteerde quizz, deze is onzichtbaar wanneer je de quizz speelt, later
                // word deze Visible = true zodat je deze dingen kan zien (alleen nadat je de quizz hebt gespeeld)
                richTextBox1.Visible = false;
                // disables the quizz reload button so you cant do anything else other than playing the quizz
                btnReload.Enabled = false;
                var fullscreen = new FullScreen(questions, questionPictures, answers, time);
                fullscreen.ShowDialog();
                // enable alle buttons en zet richtTextBox1 to visible true (zodat je alle antwoorden kan zien)
                richTextBox1.Visible = true;
                richTextBox1.Text = "";
                AppendFormattedText(richTextBox1, Environment.NewLine + "The quiz is over, click anywhere here to continue", Color.GhostWhite, true, HorizontalAlignment.Center);
                btnStartQuiz.Enabled = true;
                btnReload.Enabled = true;
                comboBoxQuiz.Enabled = true;
                panel1.Enabled = true;
            }
        }

        // laat antwoorden en vragen zien aan eind van de quizz
        private void updateAnswers(string quizId)
        {
            // zorgt ervoor dat de timer thread data uit de andere thread kan halen (die de applicatie runned)
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<string>(updateAnswers), quizId);
            }
            // laat antwoorden en vragen zien aan eind van de quizz
            else
            {
                richTextBox1.Text = "";
                var sb = new StringBuilder();
                // loop through all questions and answers of the selected quizz and display them in richTextBox1.Text
                foreach (string question in questions)
                {
                    var tsp = new StringBuilder();
                    var x = answers.Where(y => y.Question == question).ToArray();
                    // use this function to style texting and everything
                    AppendFormattedText(richTextBox1, Environment.NewLine + question + ":", Color.Black, true, HorizontalAlignment.Center);
                    foreach (Answers answer in x)
                    {
                        AppendFormattedText(richTextBox1, " | ", Color.Black, false, HorizontalAlignment.Left);
                        AppendFormattedText(richTextBox1, answer.QuizAnswer, answer.AnswerRight == Answers.answerRight.YES ? Color.LightGreen : Color.Black, false, HorizontalAlignment.Left);
                    }
                    AppendFormattedText(richTextBox1, " | ", Color.Black, false, HorizontalAlignment.Left);
                    richTextBox1.AppendText(Environment.NewLine);
                }
            }
        }
        // stijl de antwoorden enzo aan de eind van de quizz
        private void AppendFormattedText(RichTextBox rtb, string text, Color textColour, Boolean isBold, HorizontalAlignment alignment)
        {
            int start = rtb.TextLength;
            rtb.AppendText(text);
            int end = rtb.TextLength; // now longer by length of appended text

            // Select text that was appended
            rtb.Select(start, end - start);

            #region Apply Formatting
            rtb.SelectionColor = textColour;
            rtb.SelectionAlignment = alignment;
            rtb.SelectionFont = new Font(
                 rtb.SelectionFont.FontFamily,
                 rtb.SelectionFont.Size,
                 (isBold ? FontStyle.Bold : FontStyle.Regular));
            #endregion

            // Unselect text
            rtb.SelectionLength = 0;
        }
        // display alle antwoorden en vragen van de geselecteerde quiz (quizz geselecteerd in combobox) 
        private void RichTextBox1_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            updateAnswers(quizIds[comboBoxQuiz.SelectedIndex]);

            //fullscreen
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;

            // invisible everything
            comboBoxQuiz.Visible = false;
            btnReload.Visible = false;
            label1.Visible = false;
            btnStartQuiz.Visible = false;
            panel1.Visible = false;

            radioButton1.Visible = false;
            radioButton2.Visible = false;
            radioButton3.Visible = false;
            radioButton4.Visible = false;

            // change location and size of textbox
            richTextBox1.Location = new Point(0, 0);
            //richTextBox1.Height = 1500;
            //richTextBox1.Width = 1500;

            // set label in the middle
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
            // richTextBox1.TextAlign = HorizontalAlignment.Center;

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
