using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuizApp
{
    public partial class FullScreen : Form
    {
        // waardes in lists/dictionaries
        string[] questions = { };
        string[] questionPictures = { };
        List<Answers> answers = new List<Answers>();
        public FullScreen()
        {
            InitializeComponent();
        }
        // neemt vragen, antwoorden en pictures mee vanuit een andere file in deze function, start quizz en maximum form grootte 
        // (deze function staat in Form1.cs, deze function staat in regel 160)
        public FullScreen(string[] questions, string[] pictures, List<Answers> answers, string time)
        {
            this.questions = questions;
            this.questionPictures = pictures;
            this.answers = answers;
            InitializeComponent();
            this.lblTime.Text = time;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            Start();
        }


        // declare variable currentQuestion
        int currentQuestion = 0;
        public void Start()
        {
            // zet vraag naar 0 AKA eerste vraag
            currentQuestion = 0;
            // start de timer
            QuizTimer(int.Parse(lblTime.Text));
        }

        private void QuizTimer(int originalTimer)
        {
            // zie je later
            UpdateQuestion();
            // start another thread (voor de timer) en start de timer
            System.Threading.Tasks.Task t1 = new Task(() => Timer(originalTimer));
            t1.Start();
        }

        private void UpdateQuestion()
        {
            // als alle vragen gesteld zijn (dit zijn GEEN dubbele if statements dus je kan hiertussen deze 2 if statements wel wat tussen zetten
            if (currentQuestion == questions.Length)
            {
                // zorgt ervoor dat de timer thread data uit de andere thread kan halen (die de applicatie runned)
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(UpdateQuestion));
                }
                // sluit screen
                else
                {
                    this.Close();
                }

            }
            else // zoniet stel dan de volgende vraag (en laat de mogelijke antwoorden zien)
            {
                if (questionPictures.Length - 1 >= currentQuestion && questionPictures[currentQuestion] != "")
                    ChangePictureInThread(questionPicture, Directory.GetCurrentDirectory() + @"\" + questionPictures[currentQuestion]);
                else
                    ChangePictureInThread(questionPicture, Directory.GetCurrentDirectory() + @"\" + "noPic.png");

                ChangeTextInThread(txtQuestion, questions[currentQuestion]);
                ChangeTextInThread(txtA, answers[0 + currentQuestion * 4].QuizAnswer);
                ChangeTextInThread(txtB, answers[1 + currentQuestion * 4].QuizAnswer);
                ChangeTextInThread(txtC, answers[2 + currentQuestion * 4].QuizAnswer);
                ChangeTextInThread(txtD, answers[3 + currentQuestion * 4].QuizAnswer);
            }
        }

        // update vragen elke keer nadat de timer klaar is met aftellen
        private void Timer(int originalTimer)
        {
            // if timer is NOT 0 AKA, the timer is around 5 sec, then...
            if (lblTime.Text != "0")
            {
                // time is sec into numbers
                var time = int.Parse(lblTime.Text);
                // execute every second, just a like -1 every second timer
                System.Threading.Thread.Sleep(1000);
                try
                {
                    // change timer text -1 every second
                    ChangeTextInThread(lblTime, (time - 1).ToString());
                    // if second/timer is NOT 0, execute this function all over again
                    Timer(originalTimer);
                }
                catch { }
            }
            else
            {
                // zet weer originele originaltimer terug
                ChangeTextInThread(lblTime, originalTimer.ToString());
                // prepare next question
                currentQuestion++;
                // ask/put/use next question in the application
                UpdateQuestion();
                // if not all questions are asked/used of the selected quizz, execute this function again
                if (currentQuestion != questions.Length)
                {
                    Timer(originalTimer);
                }

            }
        }

        // we gebruiken invokerequired omdat de timer en de applicatie NIET op dezelfde threads runnen, om dan toch data vanuit een andere thread te halen
        // gebruiken we de invokerequired (in dit geval om de vragen en antwoorden en pictures te halen uit de andere thread en deze displayen in UpdateQuestion())

        // laat de questions en de answers zien wanneer je de quizz speelt
        private void ChangeTextInThread(Label l, string text)
        {
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action<Label, string>(ChangeTextInThread), l, text);
                }
                else
                {
                    l.Text = text;
                }
        }
        // zelfde als boven
        private void ChangeTextInThread(TextBox l, string text)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<TextBox, string>(ChangeTextInThread), l, text);
            }
            else
            {
                l.Text = text;
            }
        }
        // load een picture als je een vraag stelt wanneer je de quizz speelt (bijna hetzelfde als boven)
        private void ChangePictureInThread(PictureBox p, string text)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<PictureBox, string>(ChangePictureInThread), p, text);
            }
            else
            {
                p.Image = Image.FromFile(text);
            }
        }

        private void FullScreen_Load(object sender, EventArgs e)
        {
        }

        private void FullScreen_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Escape)
            {
                FormBorderStyle = FormBorderStyle.Sizable;
                WindowState = FormWindowState.Normal;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
