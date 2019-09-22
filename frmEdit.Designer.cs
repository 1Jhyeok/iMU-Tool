namespace iMU_Tool
{
    partial class frmEdit
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEdit));
            this.txtbox_PW = new MetroFramework.Controls.MetroTextBox();
            this.txtbox_ID = new MetroFramework.Controls.MetroTextBox();
            this.txtbox_Tag = new MetroFramework.Controls.MetroTextBox();
            this.Btn_IniSave = new MetroFramework.Controls.MetroButton();
            this.metroStyleManager1 = new MetroFramework.Components.MetroStyleManager(this.components);
            this.metroButton1 = new MetroFramework.Controls.MetroButton();
            this.metroButton2 = new MetroFramework.Controls.MetroButton();
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtbox_PW
            // 
            this.txtbox_PW.Location = new System.Drawing.Point(32, 91);
            this.txtbox_PW.Name = "txtbox_PW";
            this.txtbox_PW.Size = new System.Drawing.Size(127, 23);
            this.txtbox_PW.TabIndex = 3;
            this.txtbox_PW.Text = "패스워드";
            this.txtbox_PW.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // txtbox_ID
            // 
            this.txtbox_ID.Location = new System.Drawing.Point(32, 62);
            this.txtbox_ID.Name = "txtbox_ID";
            this.txtbox_ID.Size = new System.Drawing.Size(127, 23);
            this.txtbox_ID.Style = MetroFramework.MetroColorStyle.Teal;
            this.txtbox_ID.TabIndex = 2;
            this.txtbox_ID.Text = "아이디";
            this.txtbox_ID.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // txtbox_Tag
            // 
            this.txtbox_Tag.Location = new System.Drawing.Point(32, 33);
            this.txtbox_Tag.Name = "txtbox_Tag";
            this.txtbox_Tag.Size = new System.Drawing.Size(176, 23);
            this.txtbox_Tag.TabIndex = 1;
            this.txtbox_Tag.Text = "배틀태그";
            this.txtbox_Tag.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // Btn_IniSave
            // 
            this.Btn_IniSave.Location = new System.Drawing.Point(214, 33);
            this.Btn_IniSave.Name = "Btn_IniSave";
            this.Btn_IniSave.Size = new System.Drawing.Size(81, 81);
            this.Btn_IniSave.TabIndex = 6;
            this.Btn_IniSave.TabStop = false;
            this.Btn_IniSave.Text = "저장하기";
            this.Btn_IniSave.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.Btn_IniSave.Click += new System.EventHandler(this.Btn_IniSave_Click);
            // 
            // metroStyleManager1
            // 
            this.metroStyleManager1.Owner = this;
            this.metroStyleManager1.Style = MetroFramework.MetroColorStyle.Teal;
            this.metroStyleManager1.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // metroButton1
            // 
            this.metroButton1.Location = new System.Drawing.Point(165, 62);
            this.metroButton1.Name = "metroButton1";
            this.metroButton1.Size = new System.Drawing.Size(43, 23);
            this.metroButton1.TabIndex = 7;
            this.metroButton1.TabStop = false;
            this.metroButton1.Text = "복사";
            this.metroButton1.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroButton1.Click += new System.EventHandler(this.MetroButton1_Click);
            // 
            // metroButton2
            // 
            this.metroButton2.Location = new System.Drawing.Point(165, 91);
            this.metroButton2.Name = "metroButton2";
            this.metroButton2.Size = new System.Drawing.Size(43, 23);
            this.metroButton2.TabIndex = 8;
            this.metroButton2.TabStop = false;
            this.metroButton2.Text = "복사";
            this.metroButton2.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroButton2.Click += new System.EventHandler(this.MetroButton2_Click);
            // 
            // frmEdit
            // 
            this.AcceptButton = this.Btn_IniSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(326, 149);
            this.Controls.Add(this.metroButton2);
            this.Controls.Add(this.metroButton1);
            this.Controls.Add(this.Btn_IniSave);
            this.Controls.Add(this.txtbox_Tag);
            this.Controls.Add(this.txtbox_ID);
            this.Controls.Add(this.txtbox_PW);
            this.DisplayHeader = false;
            this.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEdit";
            this.Padding = new System.Windows.Forms.Padding(20, 30, 20, 20);
            this.Resizable = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Style = MetroFramework.MetroColorStyle.Teal;
            this.Text = "수정";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.Load += new System.EventHandler(this.Form_Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private MetroFramework.Controls.MetroTextBox txtbox_PW;
        private MetroFramework.Controls.MetroTextBox txtbox_ID;
        private MetroFramework.Controls.MetroTextBox txtbox_Tag;
        private MetroFramework.Controls.MetroButton Btn_IniSave;
        private MetroFramework.Components.MetroStyleManager metroStyleManager1;
        private MetroFramework.Controls.MetroButton metroButton2;
        private MetroFramework.Controls.MetroButton metroButton1;
    }
}