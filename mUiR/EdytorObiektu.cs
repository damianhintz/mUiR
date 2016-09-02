using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;

using muir.Model;

namespace muir
{
    public partial class EdytorObiektu : Form
    {
        public object ObiektEdytora
        {
            get { return obiektPropertyGrid.SelectedObject; }
            set
            {
                Interfejs obiekt = value as Interfejs;
                obiekt.Zachowaj();
                obiektPropertyGrid.SelectedObject = value;
            }
        }

        public EdytorObiektu()
        {
            InitializeComponent();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Interfejs obiekt = obiektPropertyGrid.SelectedObject as Interfejs;
            /*if(false)if (obiekt.jestPoprawny() == false)
            {
                MessageBox.Show(this, "UWAGA: obiekt nie jest poprawny");
                return;
            }
            */
            this.Hide();
            this.DialogResult = DialogResult.OK;
        }

        private void zastosujButton_Click(object sender, EventArgs e)
        {
        }

        private void anulujButton_Click(object sender, EventArgs e)
        {
            Interfejs obiekt = obiektPropertyGrid.SelectedObject as Interfejs;
            obiekt.Przywroc();
        }

        private void obiektPropertyGrid_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
        }

    }
}
