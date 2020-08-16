﻿namespace XLIG.ExportTables
{
    partial class ExportTablesMainView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.ServerInput = new System.Windows.Forms.TextBox();
            this.UserNameInput = new System.Windows.Forms.TextBox();
            this.PasswordInput = new System.Windows.Forms.TextBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SchemaInput = new System.Windows.Forms.TextBox();
            this.DatabaseInput = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.CheckTruncateTBL = new System.Windows.Forms.CheckBox();
            this.CheckDrop = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.CheckOnClick = true;
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.HorizontalScrollbar = true;
            this.checkedListBox1.Location = new System.Drawing.Point(32, 404);
            this.checkedListBox1.Margin = new System.Windows.Forms.Padding(4);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(296, 257);
            this.checkedListBox1.TabIndex = 0;
            this.checkedListBox1.ThreeDCheckBoxes = true;
            this.checkedListBox1.SelectedIndexChanged += new System.EventHandler(this.checkedListBox1_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(32, 705);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(134, 58);
            this.button1.TabIndex = 10;
            this.button1.Text = "PUSH";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(195, 705);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(135, 58);
            this.button2.TabIndex = 11;
            this.button2.Text = "REFRESH";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // ServerInput
            // 
            this.ServerInput.Location = new System.Drawing.Point(124, 21);
            this.ServerInput.Margin = new System.Windows.Forms.Padding(4);
            this.ServerInput.Name = "ServerInput";
            this.ServerInput.Size = new System.Drawing.Size(204, 26);
            this.ServerInput.TabIndex = 1;
            this.ServerInput.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // UserNameInput
            // 
            this.UserNameInput.Location = new System.Drawing.Point(124, 144);
            this.UserNameInput.Margin = new System.Windows.Forms.Padding(4);
            this.UserNameInput.Name = "UserNameInput";
            this.UserNameInput.Size = new System.Drawing.Size(204, 26);
            this.UserNameInput.TabIndex = 4;
            this.UserNameInput.TextChanged += new System.EventHandler(this.UserNameInput_TextChanged);
            // 
            // PasswordInput
            // 
            this.PasswordInput.Location = new System.Drawing.Point(124, 183);
            this.PasswordInput.Margin = new System.Windows.Forms.Padding(4);
            this.PasswordInput.Name = "PasswordInput";
            this.PasswordInput.PasswordChar = '*';
            this.PasswordInput.Size = new System.Drawing.Size(204, 26);
            this.PasswordInput.TabIndex = 5;
            this.PasswordInput.TextChanged += new System.EventHandler(this.PasswordInput_TextChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(4, 4);
            this.radioButton1.Margin = new System.Windows.Forms.Padding(4);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(173, 24);
            this.radioButton1.TabIndex = 2;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "SQL Authentication";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(4, 39);
            this.radioButton2.Margin = new System.Windows.Forms.Padding(4);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(98, 24);
            this.radioButton2.TabIndex = 3;
            this.radioButton2.Text = "Windows";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radioButton1);
            this.panel1.Controls.Add(this.radioButton2);
            this.panel1.Location = new System.Drawing.Point(124, 60);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(188, 75);
            this.panel1.TabIndex = 8;
            // 
            // SchemaInput
            // 
            this.SchemaInput.Location = new System.Drawing.Point(124, 261);
            this.SchemaInput.Margin = new System.Windows.Forms.Padding(4);
            this.SchemaInput.Name = "SchemaInput";
            this.SchemaInput.Size = new System.Drawing.Size(204, 26);
            this.SchemaInput.TabIndex = 7;
            this.SchemaInput.TextChanged += new System.EventHandler(this.SchemaInput_TextChanged);
            // 
            // DatabaseInput
            // 
            this.DatabaseInput.Location = new System.Drawing.Point(124, 222);
            this.DatabaseInput.Margin = new System.Windows.Forms.Padding(4);
            this.DatabaseInput.Name = "DatabaseInput";
            this.DatabaseInput.Size = new System.Drawing.Size(204, 26);
            this.DatabaseInput.TabIndex = 6;
            this.DatabaseInput.TextChanged += new System.EventHandler(this.DatabaseInput_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 26);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 20);
            this.label1.TabIndex = 11;
            this.label1.Text = "Server";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 148);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 20);
            this.label2.TabIndex = 12;
            this.label2.Text = "User Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 188);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 20);
            this.label3.TabIndex = 13;
            this.label3.Text = "Password";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 226);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 20);
            this.label4.TabIndex = 14;
            this.label4.Text = "Database";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 266);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 20);
            this.label5.TabIndex = 15;
            this.label5.Text = "Schema";
            // 
            // CheckTruncateTBL
            // 
            this.CheckTruncateTBL.AutoSize = true;
            this.CheckTruncateTBL.Location = new System.Drawing.Point(4, 9);
            this.CheckTruncateTBL.Margin = new System.Windows.Forms.Padding(4);
            this.CheckTruncateTBL.Name = "CheckTruncateTBL";
            this.CheckTruncateTBL.Size = new System.Drawing.Size(149, 24);
            this.CheckTruncateTBL.TabIndex = 8;
            this.CheckTruncateTBL.Text = "Truncate Tables";
            this.CheckTruncateTBL.UseVisualStyleBackColor = true;
            this.CheckTruncateTBL.CheckedChanged += new System.EventHandler(this.CheckTruncateTBL_CheckedChanged);
            // 
            // CheckDrop
            // 
            this.CheckDrop.AutoSize = true;
            this.CheckDrop.Location = new System.Drawing.Point(4, 44);
            this.CheckDrop.Margin = new System.Windows.Forms.Padding(4);
            this.CheckDrop.Name = "CheckDrop";
            this.CheckDrop.Size = new System.Drawing.Size(225, 24);
            this.CheckDrop.TabIndex = 9;
            this.CheckDrop.Text = "Drop and ReCreate Tables";
            this.CheckDrop.UseVisualStyleBackColor = true;
            this.CheckDrop.CheckedChanged += new System.EventHandler(this.CheckDrop_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.CheckDrop);
            this.panel2.Controls.Add(this.CheckTruncateTBL);
            this.panel2.Location = new System.Drawing.Point(87, 300);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(243, 78);
            this.panel2.TabIndex = 18;
            // 
            // ExportTablesMainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SchemaInput);
            this.Controls.Add(this.DatabaseInput);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.PasswordInput);
            this.Controls.Add(this.UserNameInput);
            this.Controls.Add(this.ServerInput);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.checkedListBox1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ExportTablesMainView";
            this.Size = new System.Drawing.Size(365, 1055);
            this.Load += new System.EventHandler(this.ExportTablesMainView_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        public System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.TextBox ServerInput;
        private System.Windows.Forms.TextBox UserNameInput;
        private System.Windows.Forms.TextBox PasswordInput;
        private System.Windows.Forms.TextBox SchemaInput;
        private System.Windows.Forms.TextBox DatabaseInput;
        public System.Windows.Forms.RadioButton radioButton1;
        public System.Windows.Forms.RadioButton radioButton2;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.Label label5;
        public System.Windows.Forms.CheckBox CheckTruncateTBL;
        public System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.CheckBox CheckDrop;
        private System.Windows.Forms.Panel panel2;
    }
}
