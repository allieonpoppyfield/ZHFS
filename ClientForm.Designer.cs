namespace ZHFS
{
    partial class ClientForm
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.nameTb = new DevExpress.XtraEditors.TextEdit();
            this.surnameTb = new DevExpress.XtraEditors.TextEdit();
            this.phoneTb = new DevExpress.XtraEditors.TextEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.nameTb.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.surnameTb.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.phoneTb.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(14, 13);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(23, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Имя:";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 39);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(48, 13);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "Фамилия:";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(12, 65);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(88, 13);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "Номер телефона:";
            // 
            // nameTb
            // 
            this.nameTb.Location = new System.Drawing.Point(123, 10);
            this.nameTb.Name = "nameTb";
            this.nameTb.Size = new System.Drawing.Size(323, 20);
            this.nameTb.TabIndex = 3;
            // 
            // surnameTb
            // 
            this.surnameTb.Location = new System.Drawing.Point(123, 36);
            this.surnameTb.Name = "surnameTb";
            this.surnameTb.Size = new System.Drawing.Size(323, 20);
            this.surnameTb.TabIndex = 4;
            // 
            // phoneTb
            // 
            this.phoneTb.Location = new System.Drawing.Point(123, 62);
            this.phoneTb.Name = "phoneTb";
            this.phoneTb.Size = new System.Drawing.Size(323, 20);
            this.phoneTb.TabIndex = 5;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.simpleButton2);
            this.panelControl1.Controls.Add(this.simpleButton1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 124);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(478, 44);
            this.panelControl1.TabIndex = 6;
            // 
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(397, 9);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(75, 23);
            this.simpleButton2.TabIndex = 3;
            this.simpleButton2.Text = "Отмена";
            this.simpleButton2.Click += new System.EventHandler(this.CloseBtnClick);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(312, 9);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 23);
            this.simpleButton1.TabIndex = 2;
            this.simpleButton1.Text = "Сохранить";
            this.simpleButton1.Click += new System.EventHandler(this.SaveBtnClick);
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 168);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.phoneTb);
            this.Controls.Add(this.surnameTb);
            this.Controls.Add(this.nameTb);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.MaximumSize = new System.Drawing.Size(494, 207);
            this.MinimumSize = new System.Drawing.Size(494, 207);
            this.Name = "ClientForm";
            this.Text = "Редактирование информации о клиенте";
            ((System.ComponentModel.ISupportInitialize)(this.nameTb.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.surnameTb.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.phoneTb.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit nameTb;
        private DevExpress.XtraEditors.TextEdit surnameTb;
        private DevExpress.XtraEditors.TextEdit phoneTb;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
    }
}