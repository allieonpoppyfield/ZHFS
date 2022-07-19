//<icbNewItemRow>
//</icbNewItemRow>
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPivotGrid;
using ZHFS.Database;

namespace ZHFS
{
    public partial class Form1 : Form
    {
        public Form1()

        {
            InitializeComponent();

            _ = Init();
        }

        private async Task Init()
        {
            using var context = new AppDbContext();
            gridControl1.DataSource = context.Users!.ToList();
            GridView? gridView1 = gridControl1.MainView as GridView;

            GridColumn nameCol = gridView1!.Columns["Name"];
            GridColumn idCol = gridView1!.Columns["Id"];
            GridColumn surnameCol = gridView1!.Columns["Surname"];
            GridColumn phoneNumberCol = gridView1!.Columns["PhoneNumber"];
            nameCol.Caption = "Имя";
            surnameCol.Caption = "Фамилия";
            phoneNumberCol.Caption = "Номер телефона";
            idCol.Visible = false;
        }
    }
}

