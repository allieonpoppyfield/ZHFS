namespace ZHFS
{
    partial class SellForm
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.saveBtn = new DevExpress.XtraEditors.SimpleButton();
            this.lb = new DevExpress.XtraEditors.ListBoxControl();
            this.cblb = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.dtP = new DevExpress.XtraEditors.DateEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cblb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtP.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtP.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.dtP);
            this.panelControl1.Controls.Add(this.simpleButton1);
            this.panelControl1.Controls.Add(this.saveBtn);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 626);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1218, 44);
            this.panelControl1.TabIndex = 6;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(1050, 9);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 23);
            this.simpleButton1.TabIndex = 1;
            this.simpleButton1.Text = "Отмена";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(1131, 9);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(75, 23);
            this.saveBtn.TabIndex = 0;
            this.saveBtn.Text = "Сохранить";
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // lb
            // 
            this.lb.Dock = System.Windows.Forms.DockStyle.Left;
            this.lb.Location = new System.Drawing.Point(0, 0);
            this.lb.Name = "lb";
            this.lb.Size = new System.Drawing.Size(616, 626);
            this.lb.TabIndex = 7;
            // 
            // cblb
            // 
            this.cblb.Dock = System.Windows.Forms.DockStyle.Right;
            this.cblb.Location = new System.Drawing.Point(613, 0);
            this.cblb.Name = "cblb";
            this.cblb.Size = new System.Drawing.Size(605, 626);
            this.cblb.TabIndex = 8;
            // 
            // dtP
            // 
            this.dtP.EditValue = null;
            this.dtP.Location = new System.Drawing.Point(95, 10);
            this.dtP.Name = "dtP";
            this.dtP.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtP.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtP.Size = new System.Drawing.Size(241, 20);
            this.dtP.TabIndex = 2;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(11, 13);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(78, 13);
            this.labelControl1.TabIndex = 3;
            this.labelControl1.Text = "Дата продажи:";
            // 
            // SellForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1218, 670);
            this.Controls.Add(this.cblb);
            this.Controls.Add(this.lb);
            this.Controls.Add(this.panelControl1);
            this.MaximumSize = new System.Drawing.Size(1234, 709);
            this.MinimumSize = new System.Drawing.Size(1234, 709);
            this.Name = "SellForm";
            this.Text = "SellForm";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cblb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtP.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtP.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton saveBtn;
        private DevExpress.XtraEditors.ListBoxControl lb;
        private DevExpress.XtraEditors.CheckedListBoxControl cblb;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DateEdit dtP;
    }
}