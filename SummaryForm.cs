using DevExpress.XtraPivotGrid;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZHFS.Database;

namespace ZHFS
{
    public partial class SummaryForm : Form
    {
        public SummaryForm()
        {
            InitializeComponent();
            dtFrom.Value = DateTime.Now.AddDays(-10);
            dtTo.Value = DateTime.Now;
            InitControl();
            ReloadDate();
            dtFrom.ValueChanged += DateChanged;
            dtTo.ValueChanged += DateChanged;

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void InitControl()
        {
            PivotGridField fieldProductName = new PivotGridField()
            {
                Area = PivotArea.RowArea,
                AreaIndex = 0,
                Caption = "Клиент",
                FieldName = "Name"
            };
            PivotGridField fieldSales = new PivotGridField()
            {
                Area = PivotArea.DataArea,
                AreaIndex = 2,
                Caption = "Сумма",
                FieldName = "TotalSale"
            };
            DataSourceColumnBinding TotalSale = new DataSourceColumnBinding("TotalSale");
            fieldProductName.DataBinding = TotalSale;

            DataSourceColumnBinding ClientName = new DataSourceColumnBinding("Name");
            fieldProductName.DataBinding = ClientName;


            PivotGridField fieldaijdi = new PivotGridField()
            {
                Area = PivotArea.RowArea,
                AreaIndex = 1,
                Caption = "Продукт",
                FieldName = "ProductName"
            };
            DataSourceColumnBinding ProductName = new DataSourceColumnBinding("ProductName");
            fieldProductName.DataBinding = TotalSale;

            pivotGridControl1.Fields.AddRange(new PivotGridField[] { fieldProductName, fieldSales, fieldaijdi });
        }

        private void DateChanged(object sender, EventArgs e)
        {
            ReloadDate();
        }

        private void ReloadDate()
        {
            if (dtFrom.Value > dtTo.Value)
            {
                MessageBox.Show("Некорректная дата");
                return;
            }

            using var context = new AppDbContext();
            pivotGridControl1.DataSource = context.Users.Select(x => x.Name);
        }
    }
}
