namespace QuizAdmin
{
    partial class Form1
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxQuiz = new System.Windows.Forms.ComboBox();
            this.comboBoxQuestion = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnRenameQuestion = new System.Windows.Forms.Button();
            this.btnRenameQuiz = new System.Windows.Forms.Button();
            this.btnDeleteQuiz = new System.Windows.Forms.Button();
            this.btnDeleteQuestion = new System.Windows.Forms.Button();
            this.btnAddQuiz = new System.Windows.Forms.Button();
            this.btnAddQuestion = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.lblIncompleteQuiz = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.uploadPreview = new System.Windows.Forms.PictureBox();
            this.btnUploadPic = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.btnDelPic = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uploadPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 2;
            this.label1.Tag = 0;
            this.label1.Click += new System.EventHandler(this.Label_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(159, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 3;
            this.label2.Tag = 1;
            this.label2.Click += new System.EventHandler(this.Label_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 13);
            this.label3.TabIndex = 4;
            this.label3.Tag = 2;
            this.label3.Click += new System.EventHandler(this.Label_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(34, 276);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(237, 100);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Answers";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(159, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 13);
            this.label4.TabIndex = 5;
            this.label4.Tag = 3;
            this.label4.Click += new System.EventHandler(this.Label_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(31, 66);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Quiz:";
            // 
            // comboBoxQuiz
            // 
            this.comboBoxQuiz.FormattingEnabled = true;
            this.comboBoxQuiz.Location = new System.Drawing.Point(34, 82);
            this.comboBoxQuiz.Name = "comboBoxQuiz";
            this.comboBoxQuiz.Size = new System.Drawing.Size(156, 21);
            this.comboBoxQuiz.TabIndex = 7;
            this.comboBoxQuiz.SelectedIndexChanged += new System.EventHandler(this.ComboBox1_SelectedIndexChanged);
            // 
            // comboBoxQuestion
            // 
            this.comboBoxQuestion.FormattingEnabled = true;
            this.comboBoxQuestion.Location = new System.Drawing.Point(34, 179);
            this.comboBoxQuestion.Name = "comboBoxQuestion";
            this.comboBoxQuestion.Size = new System.Drawing.Size(157, 21);
            this.comboBoxQuestion.TabIndex = 9;
            this.comboBoxQuestion.SelectedIndexChanged += new System.EventHandler(this.ComboBox2_SelectedIndexChanged);
            this.comboBoxQuestion.DataSourceChanged += new System.EventHandler(this.ComboBoxQuestion_DataSourceChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(31, 163);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Question:";
            // 
            // btnRenameQuestion
            // 
            this.btnRenameQuestion.Location = new System.Drawing.Point(34, 209);
            this.btnRenameQuestion.Name = "btnRenameQuestion";
            this.btnRenameQuestion.Size = new System.Drawing.Size(75, 23);
            this.btnRenameQuestion.TabIndex = 10;
            this.btnRenameQuestion.Text = "Rename";
            this.btnRenameQuestion.UseVisualStyleBackColor = true;
            this.btnRenameQuestion.Click += new System.EventHandler(this.Button2_Click);
            // 
            // btnRenameQuiz
            // 
            this.btnRenameQuiz.Location = new System.Drawing.Point(34, 109);
            this.btnRenameQuiz.Name = "btnRenameQuiz";
            this.btnRenameQuiz.Size = new System.Drawing.Size(75, 23);
            this.btnRenameQuiz.TabIndex = 11;
            this.btnRenameQuiz.Text = "Rename";
            this.btnRenameQuiz.UseVisualStyleBackColor = true;
            this.btnRenameQuiz.Click += new System.EventHandler(this.BtnRenameQuiz_Click);
            // 
            // btnDeleteQuiz
            // 
            this.btnDeleteQuiz.Location = new System.Drawing.Point(115, 109);
            this.btnDeleteQuiz.Name = "btnDeleteQuiz";
            this.btnDeleteQuiz.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteQuiz.TabIndex = 12;
            this.btnDeleteQuiz.Text = "Delete";
            this.btnDeleteQuiz.UseVisualStyleBackColor = true;
            this.btnDeleteQuiz.Click += new System.EventHandler(this.BtnDeleteQuiz_Click);
            // 
            // btnDeleteQuestion
            // 
            this.btnDeleteQuestion.Location = new System.Drawing.Point(116, 209);
            this.btnDeleteQuestion.Name = "btnDeleteQuestion";
            this.btnDeleteQuestion.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteQuestion.TabIndex = 13;
            this.btnDeleteQuestion.Text = "Delete";
            this.btnDeleteQuestion.UseVisualStyleBackColor = true;
            this.btnDeleteQuestion.Click += new System.EventHandler(this.BtnDeleteQuestion_Click);
            // 
            // btnAddQuiz
            // 
            this.btnAddQuiz.Location = new System.Drawing.Point(34, 23);
            this.btnAddQuiz.Name = "btnAddQuiz";
            this.btnAddQuiz.Size = new System.Drawing.Size(117, 23);
            this.btnAddQuiz.TabIndex = 14;
            this.btnAddQuiz.Text = "Create new quiz";
            this.btnAddQuiz.UseVisualStyleBackColor = true;
            this.btnAddQuiz.Click += new System.EventHandler(this.BtnAddQuiz_Click);
            // 
            // btnAddQuestion
            // 
            this.btnAddQuestion.Location = new System.Drawing.Point(34, 238);
            this.btnAddQuestion.Name = "btnAddQuestion";
            this.btnAddQuestion.Size = new System.Drawing.Size(156, 23);
            this.btnAddQuestion.TabIndex = 15;
            this.btnAddQuestion.Text = "Add Question";
            this.btnAddQuestion.UseVisualStyleBackColor = true;
            this.btnAddQuestion.Visible = false;
            this.btnAddQuestion.Click += new System.EventHandler(this.BtnAddQuestion_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(194, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(102, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Incomplete Quizzes:";
            // 
            // lblIncompleteQuiz
            // 
            this.lblIncompleteQuiz.AutoSize = true;
            this.lblIncompleteQuiz.ForeColor = System.Drawing.Color.Red;
            this.lblIncompleteQuiz.Location = new System.Drawing.Point(302, 23);
            this.lblIncompleteQuiz.Name = "lblIncompleteQuiz";
            this.lblIncompleteQuiz.Size = new System.Drawing.Size(0, 13);
            this.lblIncompleteQuiz.TabIndex = 17;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(479, 12);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(293, 249);
            this.textBox1.TabIndex = 18;
            // 
            // uploadPreview
            // 
            this.uploadPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.uploadPreview.Image = ((System.Drawing.Image)(resources.GetObject("uploadPreview.Image")));
            this.uploadPreview.Location = new System.Drawing.Point(611, 267);
            this.uploadPreview.Name = "uploadPreview";
            this.uploadPreview.Size = new System.Drawing.Size(161, 148);
            this.uploadPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.uploadPreview.TabIndex = 19;
            this.uploadPreview.TabStop = false;
            // 
            // btnUploadPic
            // 
            this.btnUploadPic.Enabled = false;
            this.btnUploadPic.Location = new System.Drawing.Point(199, 179);
            this.btnUploadPic.Name = "btnUploadPic";
            this.btnUploadPic.Size = new System.Drawing.Size(97, 23);
            this.btnUploadPic.TabIndex = 20;
            this.btnUploadPic.Text = "Add Picture";
            this.btnUploadPic.UseVisualStyleBackColor = true;
            this.btnUploadPic.Click += new System.EventHandler(this.Button1_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(476, 267);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(129, 13);
            this.label8.TabIndex = 21;
            this.label8.Text = "Question Picture Preview:";
            // 
            // btnDelPic
            // 
            this.btnDelPic.Enabled = false;
            this.btnDelPic.Location = new System.Drawing.Point(199, 208);
            this.btnDelPic.Name = "btnDelPic";
            this.btnDelPic.Size = new System.Drawing.Size(97, 24);
            this.btnDelPic.TabIndex = 22;
            this.btnDelPic.Text = "Delete Picture";
            this.btnDelPic.UseVisualStyleBackColor = true;
            this.btnDelPic.Click += new System.EventHandler(this.BtnDelPic_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 422);
            this.Controls.Add(this.btnDelPic);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnUploadPic);
            this.Controls.Add(this.uploadPreview);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.lblIncompleteQuiz);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnAddQuestion);
            this.Controls.Add(this.btnAddQuiz);
            this.Controls.Add(this.btnDeleteQuestion);
            this.Controls.Add(this.btnDeleteQuiz);
            this.Controls.Add(this.btnRenameQuiz);
            this.Controls.Add(this.btnRenameQuestion);
            this.Controls.Add(this.comboBoxQuestion);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.comboBoxQuiz);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Quiz Admin";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uploadPreview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxQuiz;
        private System.Windows.Forms.ComboBox comboBoxQuestion;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnRenameQuestion;
        private System.Windows.Forms.Button btnRenameQuiz;
        private System.Windows.Forms.Button btnDeleteQuiz;
        private System.Windows.Forms.Button btnDeleteQuestion;
        private System.Windows.Forms.Button btnAddQuiz;
        private System.Windows.Forms.Button btnAddQuestion;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblIncompleteQuiz;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.PictureBox uploadPreview;
        private System.Windows.Forms.Button btnUploadPic;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnDelPic;
    }
}

