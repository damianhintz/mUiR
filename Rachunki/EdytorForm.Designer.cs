namespace Rachunki
{
    partial class EdytorForm
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
            this.rachunekRichTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // rachunekRichTextBox
            // 
            this.rachunekRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rachunekRichTextBox.Location = new System.Drawing.Point(0, 0);
            this.rachunekRichTextBox.Name = "rachunekRichTextBox";
            this.rachunekRichTextBox.Size = new System.Drawing.Size(650, 735);
            this.rachunekRichTextBox.TabIndex = 1;
            this.rachunekRichTextBox.Text = "";
            this.rachunekRichTextBox.WordWrap = false;
            this.rachunekRichTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EdytorForm_KeyDown);
            this.rachunekRichTextBox.TextChanged += new System.EventHandler(this.rachunekRichTextBox_TextChanged);
            // 
            // EdytorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 735);
            this.ControlBox = false;
            this.Controls.Add(this.rachunekRichTextBox);
            this.Name = "EdytorForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edytor rachunku - naciśnij [Escape], aby zakończyć";
            this.Load += new System.EventHandler(this.EdytorForm_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.EdytorForm_KeyPress);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EdytorForm_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rachunekRichTextBox;
    }
}