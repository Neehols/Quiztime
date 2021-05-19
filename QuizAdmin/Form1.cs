using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Microsoft.VisualBasic;
using System.IO;

namespace QuizAdmin
{
    public partial class Form1 : Form
    {
        Database database; // gets database class
        Quiz quiz; // gets quizz class

        string[] questionIds= { };  // has all question ID's
        string[] quizIds = { }; // has all quiz ID's
        string[] answerIds = new string[4]; // has all answer ID's, 4 possible answers in label/textbox
        Dictionary<string,string> IncompleteQuizzes = new Dictionary<string,string>(); // dictionary gebruiken we later

        public Form1()
        {
            // connection naar database maken
            string connectionString="";
            try
            {
              // connectionstring is de file waar de connection naar database staat
              connectionString = File.ReadAllText(Directory.GetCurrentDirectory() + @"\connectionstring.txt");
            }
            catch
            {
                MessageBox.Show("Please make sure connectionstring.txt is in the same folder as this program.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }
            // connection naar de database, deze variabele connectionstring (met wachtwoordgegevens van de database en meer) word doorgevoerd naar Database class
            database = new Database(connectionString);
            // quizz class can now make use of Database class to connect to database
            quiz = new Quiz(database);
            InitializeComponent();
            textBox1.Text = "Instructions: "+ Environment.NewLine + Environment.NewLine + "Click in an empty question on the question box to add a new question." +Environment.NewLine+ Environment.NewLine + "Click an answer to change its value." +Environment.NewLine+ Environment.NewLine + "Right click it to set as correct answer."+ Environment.NewLine + Environment.NewLine +"Your quizzes MUST have at least 10 questions, if they dont they will be marked as incomplete and wont appear in the quiz application.";
            // executes updatequizzes
            updateQuizzes();
            // if there are quizzes in the database (quizIds), execute if statement (since more ID's than 0), since every quizz has a ID (quizIds)
            if(quizIds.Length > 0)
                // reloads a question, this being explained below
                updateQuestions(quizIds[comboBoxQuiz.SelectedIndex], false);
            // executes a function
            DefineIncompleteQuizzes();
            // if there are quizzes to be found, execute if statement
            if (quizIds.Length != 0 && questionIds.Length != 0)
            {
                // display image
                if (File.Exists(Directory.GetCurrentDirectory() + @"\" + "question" + quizIds[comboBoxQuiz.SelectedIndex] + "-" + questionIds[comboBoxQuestion.SelectedIndex] + ".bmp"))
                    uploadPreview.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\" + "question" + quizIds[comboBoxQuiz.SelectedIndex] + "-" + questionIds[comboBoxQuestion.SelectedIndex] + ".bmp");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        // nu gaan we data van de database uithalen en deze in een 'dictionary' zetten/doen zodat we deze data kunnen gebruiken in deze code...
        // oftewel, we kunnen nu de data laten zien/bewerken in de applicatie zelf in plaats van dat alleen doen in de database
        private void updateQuizzes()
        {
            // get values in dictionary vorm
            var availableQuizzes = quiz.AvailableQuizzes();
            // doe de namen van de quizzen in de combobox
            comboBoxQuiz.DataSource = availableQuizzes.Values.ToArray();
            // doe de quiz_ids in de quizIds{} (boven doorgenomen)
            quizIds = availableQuizzes.Keys.ToArray();

            // counts the amount of keys of availableQuizzes, in other words, if there are no quizzes available (if there are no keys present in the dictionary
            // then delete all values inside the comboboxes (where you can view and select quizzes/questions)
            if (availableQuizzes.Keys.Count == 0)
            {
                comboBoxQuiz.ResetText();
                comboBoxQuiz.SelectedIndex = -1;
                comboBoxQuestion.DataSource = null;
                comboBoxQuestion.ResetText();
                comboBoxQuestion.SelectedIndex = -1;
                UpdateAnswers("0", false);
            }
        }

        // checks voor incomplete quizzes en zorg ervoor dat de combobox 10 inputs heeft (na elke input beneden weer een input, inputs zijn leeg) - later komt
        // daar een value bij, "Empty questions"
        private void DefineIncompleteQuizzes()
        {
            var quizzes = (string[])comboBoxQuiz.DataSource;
            int i = 0;
            // quizId gets the values of quizIds this is possible due to the foreach loops that loops through the dictionary (AKA quizIds)
            foreach (string quizId in quizIds)
            {
                var questionCount = int.Parse(quiz.QuestionCount(quizId));
                int id = int.Parse(quizId);
                // If the quizz has less than 10 real questions...
                if (questionCount < 10)
                {
                    // fill with 'inputs' that are empty with no value, later on we will add 'Empty Question" on those empy inputs
                    IncompleteQuizzes.Add(quizId, quizzes[i].ToString());
                }
                i++;
            }
            // laat incompletequizzes list/dictionary in een label zien - 'incomplete quizzes' label (dit gebeurt wanneer je de values hebt opgehaald)
            StringBuilder finalIncompleteQuizzes = new StringBuilder();
            foreach (string value in IncompleteQuizzes.Values)
            {
                finalIncompleteQuizzes.Append(value + Environment.NewLine);
            }
            lblIncompleteQuiz.Text = finalIncompleteQuizzes.ToString();
        }

        // voeg Empty Question toe aan de inputs van de combobox (van de geselecteerde quiz) en 'forceer' 10 vragen
        private List<KeyValuePair<string,string>> force10Questions(string quizId)
        {
            // get a list of questions of the selected quiz
            var tempQuestions = quiz.Questions(quizId).ToList();
            // hetzelfde verhaal als boven, maar hierkijken we of de quizz wel 10 vragen heeft ja of nee
            if (tempQuestions.Count < 10)
            {
                int questionsLeft = 10 - tempQuestions.Count;
                for (int i = 0; i < questionsLeft; i++)
                {
                    // als er minder dan 10 vragen zijn, voeg een 'empty question' in de combobox toe zodat de user daar een vraag MOET toevoegen
                    tempQuestions.Add(new KeyValuePair<string, string>("0", "Empty Question"));
                }
            }
            return tempQuestions;
        }

        // reload vragen uit een combobox van de geselecteerde quiz
        public void updateQuestions(string quizId, bool shouldCreateAnswers)
        {
            // Get the index of the item that is currently selected in the combobox
            int currentIndex = comboBoxQuestion.SelectedIndex;

            // get data from force10questions and put it in this variable
            var tempQuestions = force10Questions(quizId);

            // Create a new list to put our data in
            var questionsArray = new List<string>();
            // put tempQuestions data into questionsArray.
            tempQuestions.ForEach(x => questionsArray.Add(x.Value));

            // put list data (now array) into the combobox
            comboBoxQuestion.DataSource = questionsArray.ToArray();
            // uitvogelen welke vraag je selecteert.
            currentIndex = currentIndex == -1 ? comboBoxQuestion.SelectedIndex : currentIndex;

            // Create a new list to put the question IDs in
            var idsArray = new List<string>();
            // put ID into array
            tempQuestions.ForEach(x => idsArray.Add(x.Key));
            // questionIds (de ID's van de vragen) worden geupdate/gevuld, en in een array gezet, deze staan helemaal boven en deze kan je gebruiken in sql
            //queries bijvoorbeeld
            questionIds = idsArray.ToArray();

            // update antwoorden
            if (shouldCreateAnswers == true)
            {
                UpdateAnswers(questionIds[currentIndex], true);
            }
            else
            {
                UpdateAnswers(questionIds[currentIndex], false);
            }
        }

        // if I click on "empty question" inside the combobox, then... execute this code (combobox2 is de 2de combobox die alle vragen bevatten)
        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var index = ((ComboBox)sender).SelectedIndex;
            if (index != -1)
            {
                // permissions
                btnUploadPic.Enabled = true;
                btnDelPic.Enabled = true;
                // if the clicked value is "emtpy question" execute below code
                if (((ComboBox)sender).SelectedItem.ToString() == "Empty Question")
                {
                    // creating new question
                    btnRenameQuestion.Text = "Create";
                    string newAnswer = ""; // string voor user_input vraag
                    newAnswer = index == 0 ? "" : Interaction.InputBox("Please name your new question below", "Create question");
                    if (newAnswer != "") // if question is NOT an empty question AKA if a valid question has been entered...
                    {
                        if (comboBoxQuestion.Items.Contains(newAnswer)) // if question already exists...
                        {
                            MessageBox.Show("Please type in an unique question", "Failed to create question", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            updateQuestions(quizIds[comboBoxQuiz.SelectedIndex], true);
                        }
                        else // else, just implement the new question into the quizz
                        {
                            // implement question in db
                            quiz.SetQuestion(quizIds[comboBoxQuiz.SelectedIndex], newAnswer);
                            // reload question so you can view the newly made question
                            updateQuestions(quizIds[comboBoxQuiz.SelectedIndex], true);
                            comboBoxQuestion.SelectedIndex = index;

                        }
                    }
                    else
                    {
                        if (comboBoxQuestion.Items[0].ToString() != "Empty Question")
                            comboBoxQuestion.SelectedIndex = 0;
                    }
                }
                else // laat foto zien van de geselecteerde question/vraag
                {
                    btnRenameQuestion.Text = "Rename";
                    if (quizIds.Length != 0 && questionIds.Length != 0)
                    {
                        if (File.Exists(Directory.GetCurrentDirectory() + @"\" + "question" + quizIds[comboBoxQuiz.SelectedIndex] + "-" + questionIds[comboBoxQuestion.SelectedIndex] + ".bmp"))
                            uploadPreview.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\" + "question" + quizIds[comboBoxQuiz.SelectedIndex] + "-" + questionIds[comboBoxQuestion.SelectedIndex] + ".bmp");
                        else
                            uploadPreview.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\" + "noPic.png");
                    }
                    
                } // update answers en vragen gebaseerd als er geen empty questions zijn, etc
                if (questionIds.Length != 0 && comboBoxQuestion.SelectedItem.ToString() != "Empty Question" && questionIds[comboBoxQuestion.SelectedIndex] != "0")
                    UpdateAnswers(questionIds[comboBoxQuestion.SelectedIndex],false);
                else if (((string[])comboBoxQuestion.DataSource).Length==0)
                    updateQuestions(quizIds[comboBoxQuestion.SelectedIndex],false);
            }
            // later uitgelegd wat dit doet
            AddCanExist();
        }
            
        // updates alle antwoorden in de C# list/dictionaries, zodat je de resultaat ook in de applicatie kan zien en niet alleen in de database
        private void UpdateAnswers(string questionId, bool shouldCreateAnswers)
        {
            if (comboBoxQuestion.SelectedIndex == -1) // if its a invalid index (AKA if question doesnt exist in combobox), execute below code
            {
                ClearAnswers(); // deletes answers on visual part of the combobox (het uiterlijk AKA tekst van vraag in combobox)
            }
            else // if question found in combobox
            {
                // de value van {0} is de variabele questionId, dit is weer een nummer (ID) dat aangeeft welke vraag we selecteren in de SQL query below
                string query = String.Format("SELECT * from `quizanswers` where quizz_question_id = '{0}';", questionId);
                // query word uitgevoerd in database
                var Answers = new Answer(database).QueryGetAnswers(query);
                var questionId_as_Int = questionId == "0" ? "1" : questionId;
                // if selected question is NOT 'empty question', execute following code
                if (comboBoxQuestion.SelectedItem.ToString() != "Empty Question")
                {
                    // if one of the conditions is true, execute code below (usually for newly created questions)
                    if ((questionId == "0" || Answers.Count == 0  || shouldCreateAnswers) && Answers.Count != 4)
                    {
                        for (int i = 0; i < groupBox1.Controls.Count; i++) // GroupBox1 has only 4 labels, so that .count is and will be only 4
                        {
                            // 4 'answers' will show in the label, answer1 is setted as YES
                            var label = (Label)groupBox1.Controls[i];
                            label.Text = "Answer" + " " + (i + 1).ToString(); // puts answer 1 till 4 in the application, example: answer 3 (you can view it in application)
                            label.ForeColor = SystemColors.ControlText;
                            if (i == 0)
                            {
                                quiz.SetAnswer("Answer" + " " + (i + 1).ToString(), questionId_as_Int, "YES");
                                label.ForeColor = Color.Green;
                            }
                            else
                                quiz.SetAnswer("Answer" + " " + (i + 1).ToString(), questionId_as_Int);
                            answerIds[i] = int.Parse(quiz.GetMaxId()).ToString();
                        }
                    }
                    else // if conditions are not met, usually for questions that already have been edited/created, execute below code
                    {
                        // hier kijken we naar welke antwoorden er zijn in de C# list en deze laten we displayen
                        int i = 0;
                        foreach (Answer answer in Answers) // Answers is de C# list met daarin alle antwoorden van de geselecteerde vraag, deze values ga je loopen
                            // in dit geval een foreach loop
                        {
                            var label = (Label)groupBox1.Controls[i];
                            label.Text = answer.QuizAnswer; // show all answers of selected question in label
                            label.ForeColor = SystemColors.ControlText;
                            // if YES answer gevonden is in C# list/dictionary...
                            if (answer.AnswerRight == Answer.answerRight.YES)
                            {
                                // right answer becomes green in color
                                label.ForeColor = Color.Green;
                            }
                            answerIds[i] = answer.AnswerId.ToString();
                            i++;
                        }
                    }
                }
                else // if selected question does not exist whatsoever, delete answers of that question that doesnt exist
                {
                    ClearAnswers();
                }
            }
        }

        // AKA delete all answers (of a selected question)
        private void ClearAnswers()
        {
            ((Label)groupBox1.Controls[0]).Text = "Create your question first!";
            ((Label)groupBox1.Controls[0]).ForeColor = SystemColors.ControlText;
            ((Label)groupBox1.Controls[1]).Text = "";
            ((Label)groupBox1.Controls[2]).Text = "";
            ((Label)groupBox1.Controls[3]).Text = "";
        }

        // update een answers value met linkermuisklik, zet een answer to YES met rechtermuisklik (functions voor om de C# list EN de database te updaten zijn
        // al toegevoegd AKA UpdateAnswers en quiz.UpdateAnswer)
        private void Label_Click(object sender, EventArgs e)
        {
            if (comboBoxQuestion.Items.Count == 0) // zorgt ervoor dat we uberhaupt answers kunnen bewerken
            {
                MessageBox.Show("Please create your quiz first!");
            }
            else
            {
                var label = (Label)sender;
                MouseEventArgs me = (MouseEventArgs)e;
                // if the selected question says "empty question" do the if
                if (comboBoxQuestion.SelectedItem.ToString() == "Empty Question")
                {
                    MessageBox.Show("Please rename your question first!");
                }
                else
                {
                    if (me.Button == MouseButtons.Left) // verander de antwoord als je linkermuisklik doet/klikt op een antwoord
                    {
                        var newAnswer = Interaction.InputBox(String.Format("Old answer: {0}" + Environment.NewLine + Environment.NewLine + "Please type your new answer below", label.Text), "Change answer");
                        // als tenminste IETS is ingetypt in plaats van niks...
                        if (newAnswer != "")
                        {
                            // zet de nieuwe antwoord in de database
                            quiz.UpdateAnswer(newAnswer, questionIds[comboBoxQuestion.SelectedIndex], answerIds[(int)(label.Tag)].ToString());
                            // zorg ervoor dat de nieuwe answer value kan worden getoond in de applicatie
                            UpdateAnswers(questionIds[comboBoxQuestion.SelectedIndex], false);
                        }
                    }
                    else if (me.Button == MouseButtons.Right) // right-clicking a answer changes answer to 'correct answer' AKA YES in database
                    {
                        var yesorno = MessageBox.Show(String.Format("Change {0} to correct answer?", label.Text.ToLower()), "Update correct answer", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (yesorno == DialogResult.Yes)
                        {
                            // zet 'correct answer' in de database
                            quiz.UpdateRightAnswer(questionIds[comboBoxQuestion.SelectedIndex], answerIds[(int)(label.Tag)].ToString());
                            // zorg ervoor dat je de nieuwe 'correct answer' kan zien in de applicatie
                            UpdateAnswers(questionIds[comboBoxQuestion.SelectedIndex], false);
                        }
                        else if (yesorno == DialogResult.No)
                        {

                        }
                    }
                }
            }
        }

        // rename questions here, with the rename button
        private void Button2_Click(object sender, EventArgs e)
        {
            // als er uberhaupt vragen zijn
            if (comboBoxQuestion.DataSource != null)
            {
                string newAnswer = "";
                var comboOption = comboBoxQuestion;
                int oldIndex = comboOption.SelectedIndex;
                // if the selected question (that you use the rename button for) says "empty question" do the if
                if (comboOption.SelectedItem.ToString() == "Empty Question")
                {
                    newAnswer = Interaction.InputBox(String.Format("Please type your new question below", comboOption.SelectedItem.ToString()), "Create question");
                    // prevent emtpy value
                    if (newAnswer != "")
                    {   
                        if (comboBoxQuestion.Items.Contains(newAnswer)) // check for if question already exisits
                        {
                            MessageBox.Show("Please type in an unique question", "Failed to create question", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            // updateQuestions neemt quizIds mee om zo de juiste quiz_id te selecteren en dan de vragen te updaten voor de view
                            updateQuestions(quizIds[comboBoxQuiz.SelectedIndex],true);
                            comboOption.SelectedIndex = oldIndex;
                        }
                        else
                        {
                            // rename de vraag in de database
                            quiz.SetQuestion(quizIds[comboBoxQuiz.SelectedIndex], newAnswer);
                            // zorg ervoor dat de nieuwe vraag die ge'renamed' is in de combobox terecht komt (zodat de applicatie dit kan showen)
                            updateQuestions(quizIds[comboBoxQuiz.SelectedIndex], true);
                        }
                    }
                }
                else // if not "Empty question" is present, meestal voor vragen die al gemaakt zijn...
                {
                    newAnswer = Interaction.InputBox(String.Format("Old Question: {0}" + Environment.NewLine + Environment.NewLine + "Please type your new question below", comboOption.SelectedItem.ToString()), "Change Question");
                    if (newAnswer != "") // if answer has a value...
                    {
                        if (comboBoxQuestion.Items.Contains(newAnswer)) // if answer has same value as one of the other answers
                        {
                            MessageBox.Show("Please type in an unique question", "Failed to rename question", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            updateQuestions(quizIds[comboBoxQuiz.SelectedIndex], true);
                            comboOption.SelectedIndex = oldIndex;
                        }
                        else
                        {
                            // change question in database
                            quiz.ChangeQuestion(questionIds[comboBoxQuestion.SelectedIndex], newAnswer);
                            // zorg ervoor dat je de nieuwe vraag die ge'renamed' is kan showen in de applicatie (komt in de combobox terecht)
                            updateQuestions(quizIds[comboBoxQuiz.SelectedIndex], true);
                        }
                    }
                }
                AddCanExist();
            }
        }

        // rename quizz button, just click on the button and rename the quizz
        private void BtnRenameQuiz_Click(object sender, EventArgs e)
        {
            if (comboBoxQuestion.DataSource != null) // if selected quiz name has a value, just to make sure that we are actually changing a existing quizz
            {
                var oldquiz = comboBoxQuiz.SelectedIndex;
                var newAnswer = Interaction.InputBox(String.Format("Old quiz name: {0}" + Environment.NewLine + Environment.NewLine + "Please type your new quiz name below", comboBoxQuiz.SelectedItem.ToString()), "Change quiz name");
                if (newAnswer != "") // if user_input has value, execute it
                {
                    // rename quizz in database
                    quiz.ChangeQuiz(quizIds[comboBoxQuiz.SelectedIndex], newAnswer);
                    // display new renamed quizz in the application
                    updateQuizzes();
                    comboBoxQuiz.SelectedIndex = oldquiz;
                }
            }
        }

        // if quizzes are available, load the questions
        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(quizIds.Length>0)
                updateQuestions(quizIds[comboBoxQuiz.SelectedIndex == -1 ? 0 : comboBoxQuiz.SelectedIndex], true);
        }
        // delete quizz
        private void BtnDeleteQuiz_Click(object sender, EventArgs e)
        {
            if (comboBoxQuestion.DataSource != null) // if selected quiz (name) has a value
            {
                var result = MessageBox.Show("This will delete every question and answer inside this quiz!", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                // if clicked on Yes
                if (result == DialogResult.Yes)
                {
                    // delete quizz in database
                    quiz.DeleteQuiz(quizIds[comboBoxQuiz.SelectedIndex]);
                    // delete quizz in the C# list to show that the quizz has been deleted in the application
                    updateQuizzes();
                }
            }
        }
        // create quizz
        private void BtnAddQuiz_Click(object sender, EventArgs e)
        {
            var oldquiz = comboBoxQuiz.SelectedIndex;
            var newAnswer = Interaction.InputBox("Please type your new quiz name below", "Create Quiz");
            // if user typed a value
            if (newAnswer != "")
            {
                // create quizz in database
                quiz.CreateQuiz(newAnswer);
                // create quizz in C# list so you can view the newly created quizz in the application
                updateQuizzes();

                // deze uitleggen
                if(oldquiz == -1)
                {
                    comboBoxQuiz.SelectedIndex = 0;
                    updateQuestions(quizIds[0], true);
                }
                else
                {
                    comboBoxQuiz.SelectedIndex = oldquiz+1;
                    updateQuestions(quizIds[oldquiz+1], true);
                }
                
            }
        }
        // dit zorgt ervoor dat je meer dan 10 vragen kan maken
        private void AddCanExist()
        {
            // if comboboxquestion shows no value/questions, button addquestion will be invisible
            if(comboBoxQuestion.DataSource==null)
            {
                btnAddQuestion.Visible = false;
            }
            // if combobox has no more "empty question" AKA all 10 questions have been created, show the addquestion button in order to add more questions
            else if (((string[])comboBoxQuestion.DataSource).Contains("Empty Question") == false)
            {
                btnAddQuestion.Visible = true;
            }
            // all else, just set addquestion button invisible
            else
            {
                btnAddQuestion.Visible = false;
            }
        }
        // button to add questions
        private void BtnAddQuestion_Click(object sender, EventArgs e)
        {
            int oldIndex = comboBoxQuestion.SelectedIndex;
            string newAnswer = Interaction.InputBox(String.Format("Please type your new question below", comboBoxQuestion.SelectedItem.ToString()), "Create question");
           
            if (newAnswer != "") // if a value has been entered execute below code
            {
                if (comboBoxQuestion.Items.Contains(newAnswer)) // if question already exists, execute below data
                {
                    MessageBox.Show("Please type in an unique question", "Failed to create question", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    updateQuestions(quizIds[comboBoxQuiz.SelectedIndex], true);
                    comboBoxQuestion.SelectedIndex = oldIndex;
                }
                else
                {
                    // add the new question into the database
                    quiz.SetQuestion(quizIds[comboBoxQuiz.SelectedIndex], newAnswer);
                    // show/add new question into the application
                    updateQuestions(quizIds[comboBoxQuiz.SelectedIndex], true);
                    comboBoxQuestion.SelectedIndex = comboBoxQuestion.Items.Count-1;
                }
            }
        }

        // verwijder een vraag (verwijder button)
        private void BtnDeleteQuestion_Click(object sender, EventArgs e)
        {
            if (comboBoxQuestion.DataSource != null) // if question has value... AKA als je uberhaupt een vraag hebt geselecteerd, then...
            {
                bool shouldCreateDefaultAnswers = true;
                if (((string[])comboBoxQuestion.DataSource).Count(x => x != "Empty Question")==1)
                {
                    shouldCreateDefaultAnswers = false;
                }
                // changes made in database
                quiz.DeleteQuestion(questionIds[comboBoxQuestion.SelectedIndex]);
                // changes made in C# list
                updateQuestions(quizIds[comboBoxQuiz.SelectedIndex],shouldCreateDefaultAnswers);
            }
        }

        // laat incomplete quizzes in een label zien
        private void ComboBoxQuestion_DataSourceChanged(object sender, EventArgs e)
        {
            if (comboBoxQuestion.Items.Count!=0) // if there are questions in the combobox
            {
                // if less than 10 questions... AKA quiz is incomplete
                if (((string[])comboBoxQuestion.DataSource).Contains("Empty Question"))
                {
                    // if thats the case add that quizz to the incomplete quizzes dictionary
                    if (IncompleteQuizzes.ContainsKey(quizIds[comboBoxQuiz.SelectedIndex]) == false) 
                    {
                        IncompleteQuizzes.Add(quizIds[comboBoxQuiz.SelectedIndex], comboBoxQuiz.SelectedItem.ToString());
                    }
                    else 
                    {
                        IncompleteQuizzes[quizIds[comboBoxQuiz.SelectedIndex]] = comboBoxQuiz.SelectedItem.ToString();
                    }
                }
                // if a quiz has more than 10 questions, remove the quiz with 10 questions or more from the incomplete quiz dictionary
                else
                {
                    if (IncompleteQuizzes.ContainsKey(quizIds[comboBoxQuiz.SelectedIndex]) == true)
                    {
                        IncompleteQuizzes.Remove(quizIds[comboBoxQuiz.SelectedIndex]);
                    }
                }
                // laat incomplete quizzes in een label zien - 'incomplete quizzes' label
                StringBuilder finalIncompleteQuizzes = new StringBuilder();
                foreach (string value in IncompleteQuizzes.Values)
                {
                    finalIncompleteQuizzes.Append(value + Environment.NewLine);
                }
                lblIncompleteQuiz.Text = finalIncompleteQuizzes.ToString();
            }
        }

        // zorgt ervoor als je minder dan 10 vragen hebt voor een quizz, dat je dan word tegengehouden zodra je de applicatie sluit
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (IncompleteQuizzes.Count != 0)
                {
                    e.Cancel = MessageBox.Show(@"Are you sure you want to leave? You have incomplete quizzes (less than 10 questions) that will not appear in the quiz.", Application.ProductName, MessageBoxButtons.YesNo) == DialogResult.No;
                }
            }
        }

        // foto uploaden
        private void Button1_Click(object sender, EventArgs e)
        {
            if (comboBoxQuestion.Items.Count != 0)
            {
                if (comboBoxQuestion.SelectedItem.ToString() != "Empty Question")
                {
                    string imagePath = ""; // imagepath
                    OpenFileDialog fileDialog = new OpenFileDialog();
                    fileDialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
                    var result = fileDialog.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        using (MemoryStream sr = new MemoryStream())
                        {
                            using (Stream file = fileDialog.OpenFile())
                            {
                                file.CopyTo(sr);
                                var filename = "question" + quizIds[comboBoxQuiz.SelectedIndex] + "-" + questionIds[comboBoxQuestion.SelectedIndex] + ".bmp";
                                uploadPreview.Image = new Bitmap(sr);
                                var image = uploadPreview.Image;

                                if (System.IO.File.Exists(Directory.GetCurrentDirectory() + @"\" + filename))
                                    System.IO.File.Delete(Directory.GetCurrentDirectory() + @"\" + filename);
                                image.Save(filename);

                                imagePath = filename;
                                quiz.UploadPictureToQuestion(imagePath, questionIds[comboBoxQuestion.SelectedIndex]);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please create your question first", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please create a quiz first", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //deletes pic
        private void BtnDelPic_Click(object sender, EventArgs e)
        {
            if (comboBoxQuestion.Items.Count != 0)
            {
                if (comboBoxQuestion.SelectedItem.ToString() != "Empty Question")
                {
                    uploadPreview.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\" + "noPic.png");
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    var filename = "question" + quizIds[comboBoxQuiz.SelectedIndex] + "-" + questionIds[comboBoxQuestion.SelectedIndex] + ".bmp";
                    if (File.Exists(Directory.GetCurrentDirectory() + @"\" + filename))
                        System.IO.File.Delete(Directory.GetCurrentDirectory() + @"\" + filename);
                    else
                        MessageBox.Show("The question already has no picture!", "Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);

                    quiz.DeletePictureInQuestion(questionIds[comboBoxQuestion.SelectedIndex]);
                    
                }
                else
                {
                    MessageBox.Show("Please create your question first", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please create a quiz first", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
