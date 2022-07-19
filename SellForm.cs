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
    public partial class SellForm : Form
    {
        public SellForm()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            using var context = new AppDbContext();
            lb.DataSource = context.Users!.ToList();
            lb.DisplayMember = "Name";
            lb.ValueMember = "Id";

            cblb.DataSource = context.Products!.ToList();
            cblb.DisplayMember = "Name";
            cblb.ValueMember = "Id";

            dtP.DateTime = DateTime.Now;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private async void saveBtn_Click(object sender, EventArgs e)
        {
            using var context = new AppDbContext();
            var user = await context.Users!.FirstOrDefaultAsync(x => x.Id == ((User)lb.SelectedItem).Id);
            List<Product> products = new();
            var collProducts = cblb.CheckedItems;
            products.AddRange(collProducts.OfType<Product>().ToList());
            foreach (var product in products)
            {
                context.Entry<Product>(product).State = EntityState.Unchanged;
                await context.Sales.AddAsync(new() { User = user, Product = product, CreatedDt = dtP.DateTime });
            }
            await context.SaveChangesAsync();

            this.DialogResult = DialogResult.OK;
        }
    }
}
