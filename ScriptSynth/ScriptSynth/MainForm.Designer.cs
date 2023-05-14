namespace ScriptSynth
{
    partial class MainForm
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
            this.tonesDataGridView = new System.Windows.Forms.DataGridView();
            this.Tone440Button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.tonesDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // tonesDataGridView
            // 
            this.tonesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tonesDataGridView.Location = new System.Drawing.Point(43, 99);
            this.tonesDataGridView.Name = "tonesDataGridView";
            this.tonesDataGridView.Size = new System.Drawing.Size(625, 414);
            this.tonesDataGridView.TabIndex = 0;
            // 
            // Tone440Button
            // 
            this.Tone440Button.Location = new System.Drawing.Point(327, 542);
            this.Tone440Button.Name = "Tone440Button";
            this.Tone440Button.Size = new System.Drawing.Size(75, 23);
            this.Tone440Button.TabIndex = 1;
            this.Tone440Button.Text = "440";
            this.Tone440Button.UseVisualStyleBackColor = true;
            this.Tone440Button.Click += new System.EventHandler(this.Tone440Button_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(717, 621);
            this.Controls.Add(this.Tone440Button);
            this.Controls.Add(this.tonesDataGridView);
            this.Name = "MainForm";
            this.Text = "Script Synth";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tonesDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView tonesDataGridView;
        private System.Windows.Forms.Button Tone440Button;
    }
}

