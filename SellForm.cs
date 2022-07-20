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
        private readonly long? UserId;

        public SellForm()
        {
            InitializeComponent();
        }

        public SellForm(long? userId) : this()
        {
            UserId = userId;
            _ = Init();
        }

        private async Task Init()
        {
            using var context = new AppDbContext();
            var user = await context.Users.FirstOrDefaultAsync(x => x.Id == UserId);
            this.Text = $"Добавление продажи для клиента {user.Name} {user.Surname}";
        }
    }
}
