using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Rachunki
{
    public partial class EdytorForm : Form
    {
        public EdytorForm()
        {
            InitializeComponent();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        public string getRtf()
        {
            return rachunekRichTextBox.Rtf;
        }

        public void loadRtf(string rtf)
        {
            //if (!string.IsNullOrEmpty(rachunekRichTextBox.Rtf))
                rachunekRichTextBox.Rtf = rtf;
        }

        public void saveRtf(string rtf)
        {
            rachunekRichTextBox.SaveFile(rtf);
        }

        private void EdytorForm_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void EdytorForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Visible = false;
                    break;
            }
        }

        private void EdytorForm_Load(object sender, EventArgs e)
        {

        }

        private void rachunekRichTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
