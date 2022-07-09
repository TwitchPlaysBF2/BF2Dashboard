namespace BF2Dashboard.WindowsApp
{
    partial class LaunchForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LaunchForm));
            this.InfoLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // InfoLabel
            // 
            this.InfoLabel.AutoSize = true;
            this.InfoLabel.Font = new System.Drawing.Font("Bahnschrift", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.InfoLabel.ForeColor = System.Drawing.Color.MediumSlateBlue;
            this.InfoLabel.Location = new System.Drawing.Point(69, 81);
            this.InfoLabel.Size = new System.Drawing.Size(448, 48);
            this.InfoLabel.Text = "BF2.TV App Placeholder";
            this.InfoLabel.TabIndex = 1;
            // 
            // LaunchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Desktop;
            this.ClientSize = new System.Drawing.Size(578, 226);
            this.Controls.Add(this.InfoLabel);
            this.Text = "BF2.TV App";
            this.Name = "LaunchForm";
            this.Icon = Properties.Resources.favicon;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label InfoLabel;
    }
}