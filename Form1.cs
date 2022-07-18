using ZHFS.Database;
using System;
using System.Windows.Forms;
using System.Data;
using DevExpress.XtraGrid.Views.Base;
//<icbNewItemRow>
using DevExpress.XtraGrid.Views.Grid;
//</icbNewItemRow>
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors;
using DevExpress.Utils;
using DevExpress.XtraPivotGrid;

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
            pivotGridControl1.DataSource = context.Users!.ToList();

            PivotGridField fieldCategoryName = new PivotGridField()
            {
                Area = PivotArea.RowArea,
                AreaIndex = 10,
                Caption = "Name"
            };
            DataSourceColumnBinding categoryNameBinding = new DataSourceColumnBinding("Name");
            fieldCategoryName.DataBinding = categoryNameBinding;
            pivotGridControl1.Fields.Add(fieldCategoryName);

        }
    }
}