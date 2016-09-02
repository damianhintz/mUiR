namespace muir
{
    partial class EdytorObiektu
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
            this.obiektPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.okButton = new System.Windows.Forms.Button();
            this.anulujButton = new System.Windows.Forms.Button();
            this.zastosujButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // obiektPropertyGrid
            // 
            this.obiektPropertyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.obiektPropertyGrid.Location = new System.Drawing.Point(3, 0);
            this.obiektPropertyGrid.Name = "obiektPropertyGrid";
            this.obiektPropertyGrid.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.obiektPropertyGrid.Size = new System.Drawing.Size(527, 363);
            this.obiektPropertyGrid.TabIndex = 0;
            this.obiektPropertyGrid.ToolbarVisible = false;
            this.obiektPropertyGrid.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.obiektPropertyGrid_PreviewKeyDown);
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.Location = new System.Drawing.Point(374, 369);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 1;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // anulujButton
            // 
            this.anulujButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.anulujButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.anulujButton.Location = new System.Drawing.Point(455, 369);
            this.anulujButton.Name = "anulujButton";
            this.anulujButton.Size = new System.Drawing.Size(75, 23);
            this.anulujButton.TabIndex = 2;
            this.anulujButton.Text = "Anuluj";
            this.anulujButton.UseVisualStyleBackColor = true;
            this.anulujButton.Click += new System.EventHandler(this.anulujButton_Click);
            // 
            // zastosujButton
            // 
            this.zastosujButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.zastosujButton.Location = new System.Drawing.Point(293, 369);
            this.zastosujButton.Name = "zastosujButton";
            this.zastosujButton.Size = new System.Drawing.Size(75, 23);
            this.zastosujButton.TabIndex = 3;
            this.zastosujButton.Text = "Zastosuj";
            this.zastosujButton.UseVisualStyleBackColor = true;
            this.zastosujButton.Visible = false;
            this.zastosujButton.Click += new System.EventHandler(this.zastosujButton_Click);
            // 
            // EdytorObiektu
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.anulujButton;
            this.ClientSize = new System.Drawing.Size(532, 395);
            this.Controls.Add(this.zastosujButton);
            this.Controls.Add(this.anulujButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.obiektPropertyGrid);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EdytorObiektu";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edytor obiektu";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PropertyGrid obiektPropertyGrid;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button anulujButton;
        private System.Windows.Forms.Button zastosujButton;
    }
}