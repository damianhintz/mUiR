namespace Rachunki
{
    partial class RachunekForm
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
            this.autoPlikCheckBox = new System.Windows.Forms.CheckBox();
            this.rodzajComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.autoPlikButton = new System.Windows.Forms.Button();
            this.numerKontaTextBox = new System.Windows.Forms.TextBox();
            this.bankTextBox = new System.Windows.Forms.TextBox();
            this.wKasieCheckBox = new System.Windows.Forms.CheckBox();
            this.osobaTextBox = new System.Windows.Forms.TextBox();
            this.zapiszButton = new System.Windows.Forms.Button();
            this.wynagrodzenieNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.robotaTextBox = new System.Windows.Forms.TextBox();
            this.uz7NumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.uz9NumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.podatekDochodowyNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.kosztyUzyskaniaNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.dniaDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.wykonaniaDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.zNazwyPlikuCheckBox = new System.Windows.Forms.CheckBox();
            this.doWyplatyTextBox = new System.Windows.Forms.TextBox();
            this.uz9TextBox = new System.Windows.Forms.TextBox();
            this.uz7TextBox = new System.Windows.Forms.TextBox();
            this.kosztyUzyskaniaTextBox = new System.Windows.Forms.TextBox();
            this.podatekDochodowyTextBox = new System.Windows.Forms.TextBox();
            this.slownieTextBox = new System.Windows.Forms.TextBox();
            this.numerUmowyTextBox = new System.Windows.Forms.TextBox();
            this.podstawaOpodatkowaniaTextBox = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.szablonCheckBox = new System.Windows.Forms.CheckBox();
            this.viewButton = new System.Windows.Forms.Button();
            this.saveCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.wynagrodzenieNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uz7NumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uz9NumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.podatekDochodowyNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kosztyUzyskaniaNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // autoPlikCheckBox
            // 
            this.autoPlikCheckBox.AutoSize = true;
            this.autoPlikCheckBox.Location = new System.Drawing.Point(135, 14);
            this.autoPlikCheckBox.Name = "autoPlikCheckBox";
            this.autoPlikCheckBox.Size = new System.Drawing.Size(301, 17);
            this.autoPlikCheckBox.TabIndex = 0;
            this.autoPlikCheckBox.Text = "autodetekcja danych rachunku na podstawie pliku umowy";
            this.autoPlikCheckBox.UseVisualStyleBackColor = true;
            this.autoPlikCheckBox.CheckedChanged += new System.EventHandler(this.autoPlikCheckBox_CheckedChanged);
            // 
            // rodzajComboBox
            // 
            this.rodzajComboBox.FormattingEnabled = true;
            this.rodzajComboBox.Items.AddRange(new object[] {
            "Rachunek do umowy o dzie³o",
            "Rachunek do umowy zlecenie",
            "Rachunek do umowy zlecenie - student",
            "Rachunek czêœciowy do umowy o dzie³o",
            "Rachunek ostateczny do umowy o dzie³o",
            "Rachunek czêœciowy do umowy zlecenie",
            "Rachunek ostateczny do umowy zlecenie",
            "Rachunek czêœciowy do umowy zlecenie - student",
            "Rachunek ostateczny do umowy zlecenie - student"});
            this.rodzajComboBox.Location = new System.Drawing.Point(244, 35);
            this.rodzajComboBox.Name = "rodzajComboBox";
            this.rodzajComboBox.Size = new System.Drawing.Size(379, 21);
            this.rodzajComboBox.TabIndex = 1;
            this.rodzajComboBox.SelectedIndexChanged += new System.EventHandler(this.rodzajComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(152, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "rodzaj rachunku";
            // 
            // autoPlikButton
            // 
            this.autoPlikButton.Enabled = false;
            this.autoPlikButton.Location = new System.Drawing.Point(555, 8);
            this.autoPlikButton.Name = "autoPlikButton";
            this.autoPlikButton.Size = new System.Drawing.Size(68, 23);
            this.autoPlikButton.TabIndex = 3;
            this.autoPlikButton.Text = "Plik umowy lub rachunku";
            this.autoPlikButton.UseVisualStyleBackColor = true;
            this.autoPlikButton.Click += new System.EventHandler(this.autoPlikButton_Click);
            // 
            // numerKontaTextBox
            // 
            this.numerKontaTextBox.Location = new System.Drawing.Point(244, 62);
            this.numerKontaTextBox.Name = "numerKontaTextBox";
            this.numerKontaTextBox.Size = new System.Drawing.Size(379, 20);
            this.numerKontaTextBox.TabIndex = 4;
            this.numerKontaTextBox.Text = "[Numer konta]";
            this.numerKontaTextBox.TextChanged += new System.EventHandler(this.kontoTextBox_TextChanged);
            this.numerKontaTextBox.Enter += new System.EventHandler(this.kontoTextBox_Enter);
            this.numerKontaTextBox.Leave += new System.EventHandler(this.kontoTextBox_Leave);
            // 
            // bankTextBox
            // 
            this.bankTextBox.Location = new System.Drawing.Point(244, 88);
            this.bankTextBox.Name = "bankTextBox";
            this.bankTextBox.Size = new System.Drawing.Size(379, 20);
            this.bankTextBox.TabIndex = 5;
            this.bankTextBox.Text = "[Bank]";
            this.bankTextBox.TextChanged += new System.EventHandler(this.bankTextBox_TextChanged);
            this.bankTextBox.Enter += new System.EventHandler(this.bankTextBox_Enter);
            this.bankTextBox.Leave += new System.EventHandler(this.bankTextBox_Leave);
            // 
            // wKasieCheckBox
            // 
            this.wKasieCheckBox.AutoSize = true;
            this.wKasieCheckBox.Location = new System.Drawing.Point(135, 65);
            this.wKasieCheckBox.Name = "wKasieCheckBox";
            this.wKasieCheckBox.Size = new System.Drawing.Size(96, 17);
            this.wKasieCheckBox.TabIndex = 6;
            this.wKasieCheckBox.Text = "p³atne w kasie";
            this.wKasieCheckBox.UseVisualStyleBackColor = true;
            this.wKasieCheckBox.CheckedChanged += new System.EventHandler(this.wKasieCheckBox_CheckedChanged);
            // 
            // osobaTextBox
            // 
            this.osobaTextBox.Location = new System.Drawing.Point(244, 114);
            this.osobaTextBox.Name = "osobaTextBox";
            this.osobaTextBox.Size = new System.Drawing.Size(379, 20);
            this.osobaTextBox.TabIndex = 7;
            this.osobaTextBox.Text = "[Dane osobowe]";
            this.osobaTextBox.TextChanged += new System.EventHandler(this.osobaTextBox_TextChanged);
            this.osobaTextBox.Enter += new System.EventHandler(this.osobaTextBox_Enter);
            this.osobaTextBox.Leave += new System.EventHandler(this.osobaTextBox_Leave);
            // 
            // zapiszButton
            // 
            this.zapiszButton.Location = new System.Drawing.Point(555, 353);
            this.zapiszButton.Name = "zapiszButton";
            this.zapiszButton.Size = new System.Drawing.Size(70, 23);
            this.zapiszButton.TabIndex = 8;
            this.zapiszButton.Text = "Zapisz rachunek";
            this.zapiszButton.UseVisualStyleBackColor = true;
            this.zapiszButton.Click += new System.EventHandler(this.zapiszButton_Click);
            // 
            // wynagrodzenieNumericUpDown
            // 
            this.wynagrodzenieNumericUpDown.Location = new System.Drawing.Point(376, 300);
            this.wynagrodzenieNumericUpDown.Maximum = new decimal(new int[] {
            1215752191,
            23,
            0,
            0});
            this.wynagrodzenieNumericUpDown.Name = "wynagrodzenieNumericUpDown";
            this.wynagrodzenieNumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.wynagrodzenieNumericUpDown.TabIndex = 9;
            this.wynagrodzenieNumericUpDown.ValueChanged += new System.EventHandler(this.wynagrodzenieNumericUpDown_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(241, 302);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Wynagrodzenie";
            // 
            // robotaTextBox
            // 
            this.robotaTextBox.Location = new System.Drawing.Point(12, 353);
            this.robotaTextBox.Name = "robotaTextBox";
            this.robotaTextBox.Size = new System.Drawing.Size(200, 20);
            this.robotaTextBox.TabIndex = 11;
            this.robotaTextBox.Text = "[Robota]";
            this.robotaTextBox.Enter += new System.EventHandler(this.robotaTextBox_Enter);
            this.robotaTextBox.Leave += new System.EventHandler(this.robotaTextBox_Leave);
            // 
            // uz7NumericUpDown
            // 
            this.uz7NumericUpDown.DecimalPlaces = 2;
            this.uz7NumericUpDown.Location = new System.Drawing.Point(376, 218);
            this.uz7NumericUpDown.Name = "uz7NumericUpDown";
            this.uz7NumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.uz7NumericUpDown.TabIndex = 12;
            this.uz7NumericUpDown.Value = new decimal(new int[] {
            775,
            0,
            0,
            131072});
            this.uz7NumericUpDown.ValueChanged += new System.EventHandler(this.wynagrodzenieNumericUpDown_ValueChanged);
            // 
            // uz9NumericUpDown
            // 
            this.uz9NumericUpDown.DecimalPlaces = 2;
            this.uz9NumericUpDown.Location = new System.Drawing.Point(376, 244);
            this.uz9NumericUpDown.Name = "uz9NumericUpDown";
            this.uz9NumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.uz9NumericUpDown.TabIndex = 13;
            this.uz9NumericUpDown.Value = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.uz9NumericUpDown.ValueChanged += new System.EventHandler(this.wynagrodzenieNumericUpDown_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(241, 225);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(129, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Ubezpieczenie zdrowotne";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(241, 251);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(129, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Ubezpieczenie zdrowotne";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(241, 173);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Koszty uzyskania";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(241, 277);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(105, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Podatek dochodowy";
            // 
            // podatekDochodowyNumericUpDown
            // 
            this.podatekDochodowyNumericUpDown.DecimalPlaces = 2;
            this.podatekDochodowyNumericUpDown.Location = new System.Drawing.Point(375, 270);
            this.podatekDochodowyNumericUpDown.Name = "podatekDochodowyNumericUpDown";
            this.podatekDochodowyNumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.podatekDochodowyNumericUpDown.TabIndex = 18;
            this.podatekDochodowyNumericUpDown.Value = new decimal(new int[] {
            18,
            0,
            0,
            0});
            this.podatekDochodowyNumericUpDown.ValueChanged += new System.EventHandler(this.wynagrodzenieNumericUpDown_ValueChanged);
            // 
            // kosztyUzyskaniaNumericUpDown
            // 
            this.kosztyUzyskaniaNumericUpDown.DecimalPlaces = 2;
            this.kosztyUzyskaniaNumericUpDown.Location = new System.Drawing.Point(376, 166);
            this.kosztyUzyskaniaNumericUpDown.Name = "kosztyUzyskaniaNumericUpDown";
            this.kosztyUzyskaniaNumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.kosztyUzyskaniaNumericUpDown.TabIndex = 19;
            this.kosztyUzyskaniaNumericUpDown.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.kosztyUzyskaniaNumericUpDown.ValueChanged += new System.EventHandler(this.wynagrodzenieNumericUpDown_ValueChanged);
            // 
            // dniaDateTimePicker
            // 
            this.dniaDateTimePicker.Location = new System.Drawing.Point(12, 192);
            this.dniaDateTimePicker.Name = "dniaDateTimePicker";
            this.dniaDateTimePicker.Size = new System.Drawing.Size(200, 20);
            this.dniaDateTimePicker.TabIndex = 23;
            // 
            // wykonaniaDateTimePicker
            // 
            this.wykonaniaDateTimePicker.Location = new System.Drawing.Point(12, 299);
            this.wykonaniaDateTimePicker.Name = "wykonaniaDateTimePicker";
            this.wykonaniaDateTimePicker.Size = new System.Drawing.Size(200, 20);
            this.wykonaniaDateTimePicker.TabIndex = 24;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 173);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 13);
            this.label10.TabIndex = 25;
            this.label10.Text = "Data dnia";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 277);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(139, 13);
            this.label11.TabIndex = 26;
            this.label11.Text = "Data wykonania i odebrania";
            // 
            // zNazwyPlikuCheckBox
            // 
            this.zNazwyPlikuCheckBox.AutoSize = true;
            this.zNazwyPlikuCheckBox.Location = new System.Drawing.Point(135, 117);
            this.zNazwyPlikuCheckBox.Name = "zNazwyPlikuCheckBox";
            this.zNazwyPlikuCheckBox.Size = new System.Drawing.Size(89, 17);
            this.zNazwyPlikuCheckBox.TabIndex = 30;
            this.zNazwyPlikuCheckBox.Text = "z nazwy pliku";
            this.zNazwyPlikuCheckBox.UseVisualStyleBackColor = true;
            // 
            // doWyplatyTextBox
            // 
            this.doWyplatyTextBox.Enabled = false;
            this.doWyplatyTextBox.Location = new System.Drawing.Point(502, 299);
            this.doWyplatyTextBox.Name = "doWyplatyTextBox";
            this.doWyplatyTextBox.Size = new System.Drawing.Size(121, 20);
            this.doWyplatyTextBox.TabIndex = 32;
            // 
            // uz9TextBox
            // 
            this.uz9TextBox.Enabled = false;
            this.uz9TextBox.Location = new System.Drawing.Point(503, 244);
            this.uz9TextBox.Name = "uz9TextBox";
            this.uz9TextBox.Size = new System.Drawing.Size(120, 20);
            this.uz9TextBox.TabIndex = 33;
            // 
            // uz7TextBox
            // 
            this.uz7TextBox.Enabled = false;
            this.uz7TextBox.Location = new System.Drawing.Point(503, 219);
            this.uz7TextBox.Name = "uz7TextBox";
            this.uz7TextBox.Size = new System.Drawing.Size(120, 20);
            this.uz7TextBox.TabIndex = 34;
            // 
            // kosztyUzyskaniaTextBox
            // 
            this.kosztyUzyskaniaTextBox.Enabled = false;
            this.kosztyUzyskaniaTextBox.Location = new System.Drawing.Point(503, 165);
            this.kosztyUzyskaniaTextBox.Name = "kosztyUzyskaniaTextBox";
            this.kosztyUzyskaniaTextBox.Size = new System.Drawing.Size(120, 20);
            this.kosztyUzyskaniaTextBox.TabIndex = 36;
            // 
            // podatekDochodowyTextBox
            // 
            this.podatekDochodowyTextBox.Enabled = false;
            this.podatekDochodowyTextBox.Location = new System.Drawing.Point(502, 270);
            this.podatekDochodowyTextBox.Name = "podatekDochodowyTextBox";
            this.podatekDochodowyTextBox.Size = new System.Drawing.Size(121, 20);
            this.podatekDochodowyTextBox.TabIndex = 37;
            // 
            // slownieTextBox
            // 
            this.slownieTextBox.Location = new System.Drawing.Point(12, 327);
            this.slownieTextBox.Name = "slownieTextBox";
            this.slownieTextBox.Size = new System.Drawing.Size(614, 20);
            this.slownieTextBox.TabIndex = 38;
            this.slownieTextBox.Text = "[S³ownie]";
            // 
            // numerUmowyTextBox
            // 
            this.numerUmowyTextBox.Location = new System.Drawing.Point(244, 140);
            this.numerUmowyTextBox.Name = "numerUmowyTextBox";
            this.numerUmowyTextBox.Size = new System.Drawing.Size(379, 20);
            this.numerUmowyTextBox.TabIndex = 39;
            this.numerUmowyTextBox.Text = "[Numer umowy]";
            this.numerUmowyTextBox.TextChanged += new System.EventHandler(this.numerUmowyTextBox_TextChanged);
            this.numerUmowyTextBox.Enter += new System.EventHandler(this.numerUmowyTextBox_Enter);
            this.numerUmowyTextBox.Leave += new System.EventHandler(this.numerUmowyTextBox_Leave);
            // 
            // podstawaOpodatkowaniaTextBox
            // 
            this.podstawaOpodatkowaniaTextBox.Enabled = false;
            this.podstawaOpodatkowaniaTextBox.Location = new System.Drawing.Point(503, 191);
            this.podstawaOpodatkowaniaTextBox.Name = "podstawaOpodatkowaniaTextBox";
            this.podstawaOpodatkowaniaTextBox.Size = new System.Drawing.Size(120, 20);
            this.podstawaOpodatkowaniaTextBox.TabIndex = 40;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(241, 198);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(130, 13);
            this.label13.TabIndex = 41;
            this.label13.Text = "Podstawa opodatkowania";
            // 
            // szablonCheckBox
            // 
            this.szablonCheckBox.AutoSize = true;
            this.szablonCheckBox.Checked = true;
            this.szablonCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.szablonCheckBox.Enabled = false;
            this.szablonCheckBox.Location = new System.Drawing.Point(322, 357);
            this.szablonCheckBox.Name = "szablonCheckBox";
            this.szablonCheckBox.Size = new System.Drawing.Size(227, 17);
            this.szablonCheckBox.TabIndex = 42;
            this.szablonCheckBox.Text = "wykorzystaj wbudowany szablon rachunku";
            this.szablonCheckBox.UseVisualStyleBackColor = true;
            // 
            // viewButton
            // 
            this.viewButton.Location = new System.Drawing.Point(241, 353);
            this.viewButton.Name = "viewButton";
            this.viewButton.Size = new System.Drawing.Size(75, 23);
            this.viewButton.TabIndex = 43;
            this.viewButton.Text = "Edytuj";
            this.viewButton.UseVisualStyleBackColor = true;
            this.viewButton.Click += new System.EventHandler(this.viewButton_Click);
            // 
            // saveCheckBox
            // 
            this.saveCheckBox.AutoSize = true;
            this.saveCheckBox.Checked = true;
            this.saveCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.saveCheckBox.Location = new System.Drawing.Point(322, 381);
            this.saveCheckBox.Name = "saveCheckBox";
            this.saveCheckBox.Size = new System.Drawing.Size(276, 17);
            this.saveCheckBox.TabIndex = 44;
            this.saveCheckBox.Text = "po wyjœciu z edytora, zachowaj wprowadzone zmiany";
            this.saveCheckBox.UseVisualStyleBackColor = true;
            // 
            // RachunekForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 404);
            this.Controls.Add(this.saveCheckBox);
            this.Controls.Add(this.viewButton);
            this.Controls.Add(this.szablonCheckBox);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.podstawaOpodatkowaniaTextBox);
            this.Controls.Add(this.numerUmowyTextBox);
            this.Controls.Add(this.slownieTextBox);
            this.Controls.Add(this.podatekDochodowyTextBox);
            this.Controls.Add(this.kosztyUzyskaniaTextBox);
            this.Controls.Add(this.uz7TextBox);
            this.Controls.Add(this.uz9TextBox);
            this.Controls.Add(this.doWyplatyTextBox);
            this.Controls.Add(this.zNazwyPlikuCheckBox);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.wykonaniaDateTimePicker);
            this.Controls.Add(this.dniaDateTimePicker);
            this.Controls.Add(this.kosztyUzyskaniaNumericUpDown);
            this.Controls.Add(this.podatekDochodowyNumericUpDown);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.uz9NumericUpDown);
            this.Controls.Add(this.uz7NumericUpDown);
            this.Controls.Add(this.robotaTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.wynagrodzenieNumericUpDown);
            this.Controls.Add(this.zapiszButton);
            this.Controls.Add(this.osobaTextBox);
            this.Controls.Add(this.wKasieCheckBox);
            this.Controls.Add(this.bankTextBox);
            this.Controls.Add(this.numerKontaTextBox);
            this.Controls.Add(this.autoPlikButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rodzajComboBox);
            this.Controls.Add(this.autoPlikCheckBox);
            this.Name = "RachunekForm";
            this.Text = "Nowy rachunek do umowy - v1.0-beta";
            this.Load += new System.EventHandler(this.RachunekForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.wynagrodzenieNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uz7NumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uz9NumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.podatekDochodowyNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kosztyUzyskaniaNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox autoPlikCheckBox;
        private System.Windows.Forms.ComboBox rodzajComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button autoPlikButton;
        private System.Windows.Forms.TextBox numerKontaTextBox;
        private System.Windows.Forms.TextBox bankTextBox;
        private System.Windows.Forms.CheckBox wKasieCheckBox;
        private System.Windows.Forms.TextBox osobaTextBox;
        private System.Windows.Forms.Button zapiszButton;
        private System.Windows.Forms.NumericUpDown wynagrodzenieNumericUpDown;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox robotaTextBox;
        private System.Windows.Forms.NumericUpDown uz7NumericUpDown;
        private System.Windows.Forms.NumericUpDown uz9NumericUpDown;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown podatekDochodowyNumericUpDown;
        private System.Windows.Forms.NumericUpDown kosztyUzyskaniaNumericUpDown;
        private System.Windows.Forms.DateTimePicker dniaDateTimePicker;
        private System.Windows.Forms.DateTimePicker wykonaniaDateTimePicker;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox zNazwyPlikuCheckBox;
        private System.Windows.Forms.TextBox doWyplatyTextBox;
        private System.Windows.Forms.TextBox uz9TextBox;
        private System.Windows.Forms.TextBox uz7TextBox;
        private System.Windows.Forms.TextBox kosztyUzyskaniaTextBox;
        private System.Windows.Forms.TextBox podatekDochodowyTextBox;
        private System.Windows.Forms.TextBox slownieTextBox;
        private System.Windows.Forms.TextBox numerUmowyTextBox;
        private System.Windows.Forms.TextBox podstawaOpodatkowaniaTextBox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.CheckBox szablonCheckBox;
        private System.Windows.Forms.Button viewButton;
        private System.Windows.Forms.CheckBox saveCheckBox;
    }
}

