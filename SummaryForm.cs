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
        private readonly DateTime dtFrom;
        private readonly DateTime dtTo;
        public SummaryForm()
        {
            InitializeComponent();
            _ = InitGrid();
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
            var list = context.SaleItems.Include(x => x.Product).Include(x => x.Sale).ThenInclude(x => x.User).
                Select(x => new { UserName = x.Sale.User.Name, ProductName = x.Product.Name, TotalPrice = x.Product.Price * x.Count });
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
