using DevExpress.XtraPivotGrid;
using Microsoft.EntityFrameworkCore;
using System.Data;

using ZHFS.Database;

namespace ZHFS
{
    public partial class ClientReportForm : Form
    {
        private readonly DateTime dtFrom;
        private readonly DateTime dtTo;
        private readonly long UserId;
        public ClientReportForm()
        {
            InitializeComponent();
        }
        public ClientReportForm(long UserId, DateTime dtFrom, DateTime dtTo) : this()
        {
            this.dtFrom = dtFrom;
            this.dtTo = dtTo;
            this.UserId = UserId;
            _ = InitGrid();
        }

        private async Task InitGrid()
        {
            using var context = new AppDbContext();
            var user = await context.Users.FirstOrDefaultAsync(x => x.Id == UserId);
            this.Text = $"Отчет оказанных услуг по клиенту {user.Name} {user.Surname}";

            var list = await context.SaleItems.Where(x => x.Sale.CreatedDt.Date >= dtFrom.Date && x.Sale.CreatedDt.Date <= dtTo.Date && x.Sale.User.Id == UserId)
                .Select(x => new
                {
                    ProductId = x.Product.Id,
                    ProductName = x.Product.Name,
                    TotalCount = x.Count,
                    TotalPrice = x.Product.Price * x.Count,
                    Month = x.Sale.CreatedDt.Month,
                    Year = x.Sale.CreatedDt.Year
                })
                .GroupBy(x => new { x.ProductId, x.ProductName, x.Year, x.Month })
                .Select(g => new
                {
                    g.Key.ProductId,
                    g.Key.ProductName,
                    g.Key.Year,
                    g.Key.Month,
                    TotalCount = g.Sum(x => x.TotalCount),
                    TotalPrice = g.Sum(x => x.TotalPrice)
                })
                .ToListAsync();
            this.pivotGridControl1.DataSource = list;


            PivotGridField productIdField = new()
            {
                Area = PivotArea.RowArea,
                AreaIndex = 1,
                Caption = "ProductId",
                FieldName = "ProductId",
                Visible = false
            };
            this.pivotGridControl1.Fields.Add(productIdField);

            PivotGridField productNameField = new()
            {
                Area = PivotArea.RowArea,
                AreaIndex = 2,
                Caption = "Товар",
                FieldName = "ProductName",
            };
            this.pivotGridControl1.Fields.Add(productNameField);

            PivotGridField monthField = new()
            {
                Area = PivotArea.RowArea,
                AreaIndex = 3,
                Caption = "Месяц",
                FieldName = "Month",
            };
            this.pivotGridControl1.Fields.Add(monthField);

            PivotGridField yearField = new()
            {
                Area = PivotArea.RowArea,
                AreaIndex = 4,
                Caption = "Год",
                FieldName = "Year",
            };
            this.pivotGridControl1.Fields.Add(yearField);


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
            this.Close();
        }
    }
}
