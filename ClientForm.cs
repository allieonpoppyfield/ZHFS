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
    public partial class ClientForm : Form
    {
        private User user = new();
        public ClientForm()
        {
            InitializeComponent();
        }

        public ClientForm(User user) : this()
        {
            this.user = user;
            nameTb.Text = user.Name;
            surnameTb.Text = user.Surname;
            phoneTb.Text = user.PhoneNumber;
        }

        private void CloseBtnClick(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void SaveBtnClick(object sender, EventArgs e)
        {
            using var context = new AppDbContext();
            user.Name = nameTb.Text;
            user.Surname = surnameTb.Text;
            user.PhoneNumber = phoneTb.Text;
            if (user.Id == 0)
            {
                context.Users!.Add(user);
            }
            else
            {
                context.Update<User>(user);
            }
            context.SaveChanges();
            this.DialogResult = DialogResult.OK;
        }
    }
}
