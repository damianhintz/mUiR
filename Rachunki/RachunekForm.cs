using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Rachunki
{
    public partial class RachunekForm : Form
    {
        private EdytorForm edytor = new EdytorForm();
        private string rtf = "";

        public RachunekForm()
        {
            InitializeComponent();
        }

        private void autoPlikCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            autoPlikButton.Enabled = autoPlikCheckBox.Checked;
        }

        private void wKasieCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            bankTextBox.Enabled = !wKasieCheckBox.Checked;
            numerKontaTextBox.Enabled = !wKasieCheckBox.Checked;
        }

        private void kontoTextBox_TextChanged(object sender, EventArgs e)
        {
        }

        private void bankTextBox_TextChanged(object sender, EventArgs e)
        {
        }

        private void osobaTextBox_TextChanged(object sender, EventArgs e)
        {
        }

        private void osobaTextBox_Enter(object sender, EventArgs e)
        {
            if (osobaTextBox.Text == "[Dane osobowe]")
            {
                osobaTextBox.Text = "";
            }
        }

        private void osobaTextBox_Leave(object sender, EventArgs e)
        {
            if (osobaTextBox.Text == "")
            {
                osobaTextBox.Text = "[Dane osobowe]";
            }
        }

        private void bankTextBox_Enter(object sender, EventArgs e)
        {
            if (bankTextBox.Text == "[Bank]")
            {
                bankTextBox.Text = "";
            }
        }

        private void bankTextBox_Leave(object sender, EventArgs e)
        {
            if (bankTextBox.Text == "")
            {
                bankTextBox.Text = "[Bank]";
            }
        }

        private void kontoTextBox_Enter(object sender, EventArgs e)
        {
            if (numerKontaTextBox.Text == "[Numer konta]")
            {
                numerKontaTextBox.Text = "";
            }
        }

        private void kontoTextBox_Leave(object sender, EventArgs e)
        {
            if (numerKontaTextBox.Text == "")
            {
                numerKontaTextBox.Text = "[Numer konta]";
            }
        }

        private void robotaTextBox_Enter(object sender, EventArgs e)
        {
            if (robotaTextBox.Text == "[Robota]")
            {
                robotaTextBox.Text = "";
            }
        }

        private void robotaTextBox_Leave(object sender, EventArgs e)
        {
            if (robotaTextBox.Text == "")
            {
                robotaTextBox.Text = "[Robota]";
            }
        }

        private void rodzajComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string item = (string)rodzajComboBox.SelectedItem;
            if (item.Contains("student") || item.Contains("dzie³o"))
            {
                uz7NumericUpDown.Value = (decimal)0.0f;
                uz9NumericUpDown.Value = (decimal)0.0f;
            }
            else
            {
                uz7NumericUpDown.Value = (decimal)7.75f;
                uz9NumericUpDown.Value = (decimal)9.0f;
            }
        }

        private void wynagrodzenieNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            decimal a = kosztyUzyskaniaNumericUpDown.Value * wynagrodzenieNumericUpDown.Value / (decimal)100;
            kosztyUzyskaniaTextBox.Text = string.Format("{0:F2}", a);
            decimal b = wynagrodzenieNumericUpDown.Value - a;
            podstawaOpodatkowaniaTextBox.Text = string.Format("{0:F2}", b);
            decimal c = uz7NumericUpDown.Value * wynagrodzenieNumericUpDown.Value / (decimal)100.0;
            uz7TextBox.Text = string.Format("{0:F2}", c);
            decimal d = uz9NumericUpDown.Value * wynagrodzenieNumericUpDown.Value / (decimal)100.0;
            uz9TextBox.Text = string.Format("{0:F2}", d);
            decimal ee = podatekDochodowyNumericUpDown.Value * b / (decimal)100.0 - c;
            podatekDochodowyTextBox.Text = string.Format("{0:F2}", ee);
            decimal f = wynagrodzenieNumericUpDown.Value - ee - d;

            doWyplatyTextBox.Text = string.Format("{0:F0}", f);

            slownieTextBox.Text = string.Format("{0} z³", Slownie.innerTrim(Slownie.doubleSlownie(f)));

        }

        private void RachunekForm_Load(object sender, EventArgs e)
        {
            rodzajComboBox.SelectedIndex = 0;
            wynagrodzenieNumericUpDown.Value = (decimal)1.0;
        }

        private void numerUmowyTextBox_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void numerUmowyTextBox_Enter(object sender, EventArgs e)
        {
            if (numerUmowyTextBox.Text == "[Numer umowy]")
            {
                numerUmowyTextBox.Text = "";
            }
        }

        private void numerUmowyTextBox_Leave(object sender, EventArgs e)
        {
            if (numerUmowyTextBox.Text == "")
            {
                numerUmowyTextBox.Text = "[Numer umowy]";
            }
        }

        private void zapiszButton_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(rtf))
                rtf = loadString();

            edytor.loadRtf(rtf);
            //edytor.ShowDialog(this);

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Pliki RTF (*.rtf)|*.rtf";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string fileName = saveFileDialog.FileName;
                
                StreamWriter writer = new StreamWriter(fileName, false);
                writer.Write(rtf);
                writer.Close();

                //edytor.saveRtf(fileName);
            }

        }

        private string loadString()
        {
            
            string all = "";
            string rodzaj = (rodzajComboBox.SelectedItem as string);

            if (rodzaj.Contains("dzie³o"))
                all = Properties.Resources.dzieloString;
            else if (rodzaj.Contains("student"))
                all = Properties.Resources.studentString;
            else if (rodzaj.Contains("zlecenie"))
                all = Properties.Resources.zlecenieString;
            else
                all = Properties.Resources.dzieloString;

            all = all.Replace("%OSOBA%", "" + osobaTextBox.Text);
            all = all.Replace("%DATA_DNIA%", dniaDateTimePicker.Value.ToShortDateString());
            all = all.Replace("%TYTUL_RACHUNKU%", rodzaj.ToUpper());
            all = all.Replace("%NUMER_RACHUNKU%", numerUmowyTextBox.Text);
            all = all.Replace("%WYNAGRODZENIEB%", "" + wynagrodzenieNumericUpDown.Value);
            all = all.Replace("%KOSZTYU_P%", "" + string.Format("{0:F0}", kosztyUzyskaniaNumericUpDown.Value));
            all = all.Replace("%KOSZTYU%", kosztyUzyskaniaTextBox.Text);
            all = all.Replace("%PODSTAWAO%", podstawaOpodatkowaniaTextBox.Text);
            all = all.Replace("%PODATEKD_P%", "" + string.Format("{0:F0}", podatekDochodowyNumericUpDown.Value));

            all = all.Replace("%DOWYPLATY%", doWyplatyTextBox.Text);
            all = all.Replace("%SLOWNIE%", slownieTextBox.Text);

            if (wKasieCheckBox.Checked)
                all = all.Replace("%WKASIE_LUB_NAKONTO%", "P³atne w kasie");
            else
                all = all.Replace("%WKASIE_LUB_NAKONTO%", bankTextBox.Text + " " + numerKontaTextBox.Text);

            all = all.Replace("%DATA_WYKONANIA%", wykonaniaDateTimePicker.Value.ToShortDateString());
            all = all.Replace("%ROBOTA%", robotaTextBox.Text);

            all = all.Replace("%UBEZPIECZENIEZ_P%", "" + string.Format("{0:F2}", uz7NumericUpDown.Value));
            all = all.Replace("%UBEZPIECZENIEZ2_P%", "" + string.Format("{0:F0}", uz9NumericUpDown.Value));
            all = all.Replace("%UBEZPIECZENIEZ%", uz7TextBox.Text);
            all = all.Replace("%UBEZPIECZENIEZ2%", uz9TextBox.Text);

            all = all.Replace("%PODATEKD%", podatekDochodowyTextBox.Text);

            return all;
        }

        private void viewButton_Click(object sender, EventArgs e)
        {
            if(saveCheckBox.Checked == false || string.IsNullOrEmpty(rtf))
                rtf = loadString();

            edytor.loadRtf(rtf);
            edytor.ShowDialog(this);
            if (edytor.getRtf() != rtf)
            {
                rtf = edytor.getRtf();
            }
        }

        private void autoPlikButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Pliki Word (*.doc, *.rtf)|*.doc";
            openFileDialog.Multiselect = false;

            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {

                string numer = "", konto = "", osoba = "", rodzaj = "";
                osoba = openFileDialog.SafeFileName.Replace(".doc", "");

                if (osoba.Contains("_"))
                {
                    osoba = osoba.Split(new char[] { '_' }, 2)[1];
                }

                WordWraper.WordImportUmowa(openFileDialog.FileName, ref rodzaj, ref numer, ref konto, ref osoba);
                numerUmowyTextBox.Text = numer;
                //rodzajUmowyComboBox.SelectedIndex = Util.Util.umowaRodzajIndeks(rodzaj);

                if (rodzaj.Contains("dzie³o"))
                    rodzajComboBox.SelectedIndex = 0;
                else if (rodzaj.Contains("student"))
                    rodzajComboBox.SelectedIndex = 2;
                else if (rodzaj.Contains("zlecenie"))
                    rodzajComboBox.SelectedIndex = 1;
                else
                    rodzajComboBox.SelectedIndex = 0;

                numerKontaTextBox.Text = konto;

                if (zNazwyPlikuCheckBox.Checked)
                    osobaTextBox.Text = openFileDialog.SafeFileName.Replace(".doc", "");
                else
                    osobaTextBox.Text = osoba;
            }

            openFileDialog.Dispose();

        }

    }
}