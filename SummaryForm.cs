﻿using DevExpress.XtraPivotGrid;
using Microsoft.EntityFrameworkCore;
using System.Data;

using ZHFS.Database;

namespace ZHFS
{
    public partial class SummaryForm : Form
    {
        private readonly DateTime dtFrom;
        private readonly DateTime dtTo;
        public SummaryForm()
        {
            InitializeComponent();
        }
        public SummaryForm(DateTime dtFrom, DateTime dtTo) : this()
        {
            this.dtFrom = dtFrom;
            this.dtTo = dtTo;
            _ = InitGrid();
        }

        private async Task InitGrid()
        {
            using var context = new AppDbContext();
            var list = await context.SaleItems.Where(x => x.Sale.CreatedDt.Date >= dtFrom.Date && x.Sale.CreatedDt.Date <= dtTo.Date)
                .Select(x => new
                {
                    UserId = x.Sale.User.Id,
                    UserName = x.Sale.User.Name,
                    ProductId = x.Product.Id,
                    ProductName = x.Product.Name,
                    TotalCount = x.Count,
                    TotalPrice = x.Product.Price * x.Count
                })
                .GroupBy(x => new { x.UserId, x.ProductId, x.UserName, x.ProductName })
                .Select(g => new
                {
                    g.Key.UserId,
                    g.Key.ProductId,
                    g.Key.UserName,
                    g.Key.ProductName,
                    TotalCount = g.Sum(x => x.TotalCount),
                    TotalPrice = g.Sum(x => x.TotalPrice)
                })
                .ToListAsync();
            this.pivotGridControl1.DataSource = list;

            PivotGridField clientIdField = new()
            {
                Area = PivotArea.RowArea,
                AreaIndex = 1,
                Caption = "Id",
                FieldName = "UserId",
                Visible = false
            };
            this.pivotGridControl1.Fields.Add(clientIdField);
            PivotGridField clientNameField = new()
            {
                Area = PivotArea.RowArea,
                AreaIndex = 2,
                Caption = "Клиент",
                FieldName = "UserName"
            };
            this.pivotGridControl1.Fields.Add(clientNameField);

            PivotGridField productIdField = new()
            {
                Area = PivotArea.RowArea,
                AreaIndex = 3,
                Caption = "ProductId",
                FieldName = "ProductId",
                Visible = false
            };
            this.pivotGridControl1.Fields.Add(productIdField);

            PivotGridField productNameFile = new()
            {
                Area = PivotArea.RowArea,
                AreaIndex = 4,
                Caption = "Товар",
                FieldName = "ProductName",
            };
            this.pivotGridControl1.Fields.Add(productNameFile);
            PivotGridField productCountField = new()
            {
                Area = PivotArea.DataArea,
                AreaIndex = 5,
                Caption = "Количество",
                FieldName = "TotalCount",
            };
            this.pivotGridControl1.Fields.Add(productCountField);

            PivotGridField totalPriceField = new()
            {
                Area = PivotArea.DataArea,
                AreaIndex = 6,
                Caption = "Сумма",
                FieldName = "TotalPrice",
            };
            this.pivotGridControl1.Fields.Add(totalPriceField);
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
