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
    public partial class ProductForm : Form
    {
        private Product product = new();
        public ProductForm()
        {
            InitializeComponent();
        }

        public ProductForm(Product product) : this()
        {
            this.product = product;
        }

        private void CloseBtnClick(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void SaveBtnClick(object sender, EventArgs e)
        {
            using var context = new AppDbContext();
            product.Name = nameTb.Text;
            product.Price = Convert.ToDecimal(priceTb.Text);

            if (product.Id == 0)
            {
                context.Products!.Add(product);
            }
            else
            {
                context.Update<Product>(product);
            }
            context.SaveChanges();
            this.DialogResult = DialogResult.OK;
        }

        private void priceTb_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
