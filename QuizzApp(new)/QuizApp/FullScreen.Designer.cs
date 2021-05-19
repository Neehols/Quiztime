using System.Windows.Forms;

namespace QuizApp
{
    partial class FullScreen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FullScreen));
            this.lblTime = new System.Windows.Forms.Label();
            this.questionPicture = new System.Windows.Forms.PictureBox();
            this.txtQuestion = new System.Windows.Forms.TextBox();
            this.txtA = new System.Windows.Forms.TextBox();
            this.txtB = new System.Windows.Forms.TextBox();
            this.txtC = new System.Windows.Forms.TextBox();
            this.txtD = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.questionPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTime
            // 
            this.lblTime.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTime.AutoSize = true;
            this.lblTime.BackColor = System.Drawing.Color.White;
            this.lblTime.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.Location = new System.Drawing.Point(1204, 77);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(64, 46);
            this.lblTime.TabIndex = 21;
            this.lblTime.Text = "00";
            this.lblTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // questionPicture
            // 
            this.questionPicture.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.questionPicture.Image = ((System.Drawing.Image)(resources.GetObject("questionPicture.Image")));
            this.questionPicture.Location = new System.Drawing.Point(482, 39);
            this.questionPicture.Name = "questionPicture";
            this.questionPicture.Size = new System.Drawing.Size(422, 260);
            this.questionPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.questionPicture.TabIndex = 22;
            this.questionPicture.TabStop = false;
            // 
            // txtQuestion
            // 
            this.txtQuestion.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtQuestion.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtQuestion.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtQuestion.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQuestion.ForeColor = System.Drawing.SystemColors.Window;
            this.txtQuestion.Location = new System.Drawing.Point(166, 330);
            this.txtQuestion.Multiline = true;
            this.txtQuestion.Name = "txtQuestion";
            this.txtQuestion.ReadOnly = true;
            this.txtQuestion.Size = new System.Drawing.Size(1049, 111);
            this.txtQuestion.TabIndex = 15;
            this.txtQuestion.TabStop = false;
            this.txtQuestion.Text = "Example Question";
            this.txtQuestion.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtQuestion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FullScreen_KeyPress);
            // 
            // txtA
            // 
            this.txtA.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtA.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtA.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtA.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtA.ForeColor = System.Drawing.SystemColors.Window;
            this.txtA.Location = new System.Drawing.Point(143, 501);
            this.txtA.Multiline = true;
            this.txtA.Name = "txtA";
            this.txtA.ReadOnly = true;
            this.txtA.Size = new System.Drawing.Size(444, 68);
            this.txtA.TabIndex = 17;
            this.txtA.TabStop = false;
            this.txtA.Text = "A";
            this.txtA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtA.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FullScreen_KeyPress);
            // 
            // txtB
            // 
            this.txtB.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtB.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtB.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtB.ForeColor = System.Drawing.SystemColors.Window;
            this.txtB.Location = new System.Drawing.Point(836, 501);
            this.txtB.Multiline = true;
            this.txtB.Name = "txtB";
            this.txtB.ReadOnly = true;
            this.txtB.Size = new System.Drawing.Size(422, 68);
            this.txtB.TabIndex = 18;
            this.txtB.TabStop = false;
            this.txtB.Text = "B";
            this.txtB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtB.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FullScreen_KeyPress);
            // 
            // txtC
            // 
            this.txtC.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtC.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtC.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtC.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtC.ForeColor = System.Drawing.SystemColors.Window;
            this.txtC.Location = new System.Drawing.Point(143, 618);
            this.txtC.Multiline = true;
            this.txtC.Name = "txtC";
            this.txtC.ReadOnly = true;
            this.txtC.Size = new System.Drawing.Size(444, 66);
            this.txtC.TabIndex = 19;
            this.txtC.TabStop = false;
            this.txtC.Text = "C";
            this.txtC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtC.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FullScreen_KeyPress);
            // 
            // txtD
            // 
            this.txtD.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtD.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtD.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtD.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtD.ForeColor = System.Drawing.SystemColors.Window;
            this.txtD.Location = new System.Drawing.Point(836, 618);
            this.txtD.Multiline = true;
            this.txtD.Name = "txtD";
            this.txtD.ReadOnly = true;
            this.txtD.Size = new System.Drawing.Size(422, 59);
            this.txtD.TabIndex = 20;
            this.txtD.TabStop = false;
            this.txtD.Text = "D";
            this.txtD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtD.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FullScreen_KeyPress);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(-4, -2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1363, 724);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 16;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // FullScreen
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1360, 720);
            this.Controls.Add(this.txtC);
            this.Controls.Add(this.txtA);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.questionPicture);
            this.Controls.Add(this.txtQuestion);
            this.Controls.Add(this.txtB);
            this.Controls.Add(this.txtD);
            this.Controls.Add(this.pictureBox1);
            this.Name = "FullScreen";
            this.Text = "FullScreen";
            this.Load += new System.EventHandler(this.FullScreen_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FullScreen_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.questionPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.PictureBox questionPicture;
        private System.Windows.Forms.TextBox txtQuestion;
        private System.Windows.Forms.TextBox txtA;
        private System.Windows.Forms.TextBox txtB;
        private System.Windows.Forms.TextBox txtC;
        private System.Windows.Forms.TextBox txtD;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}