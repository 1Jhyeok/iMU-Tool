namespace iMU_Tool
{
    partial class frmUpdater
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUpdater));
            this.progressBar = new MetroFramework.Controls.MetroProgressBar();
            this.labelSpeed = new MetroFramework.Controls.MetroLabel();
            this.SuspendLayout();
            // 
            // progressBar
            // 
            this.progressBar.HideProgressText = false;
            this.progressBar.Location = new System.Drawing.Point(23, 79);
            this.progressBar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(192, 29);
            this.progressBar.TabIndex = 0;
            this.progressBar.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // labelSpeed
            // 
            this.labelSpeed.AutoSize = true;
            this.labelSpeed.Location = new System.Drawing.Point(171, 112);
            this.labelSpeed.Name = "labelSpeed";
            this.labelSpeed.Size = new System.Drawing.Size(44, 19);
            this.labelSpeed.TabIndex = 1;
            this.labelSpeed.Text = "0 kb/s";
            this.labelSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelSpeed.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // frmUpdater
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(256, 145);
            this.ControlBox = false;
            this.Controls.Add(this.labelSpeed);
            this.Controls.Add(this.progressBar);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmUpdater";
            this.Padding = new System.Windows.Forms.Padding(20, 75, 20, 25);
            this.Resizable = false;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultLocation;
            this.Style = MetroFramework.MetroColorStyle.Teal;
            this.Text = "다운로드";
            this.TextAlign = System.Windows.Forms.VisualStyles.HorizontalAlign.Center;
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroProgressBar progressBar;
        private MetroFramework.Controls.MetroLabel labelSpeed;
    }
}