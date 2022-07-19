using DevExpress.Utils.Menu;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.EntityFrameworkCore;
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
            await InitUserList();
            await InitProductList();
        }

        private async Task InitUserList()
        {
            clientGrid.DataSource = await GetUsersDataSource();
            clientGrid.ToolTipController = new();
            GridView? gridView1 = clientGrid.MainView as GridView;
            gridView1!.PopupMenuShowing += GridView1_PopupMenuShowing;
            GridColumn nameCol = gridView1!.Columns["Name"];
            GridColumn idCol = gridView1!.Columns["Id"];
            GridColumn surnameCol = gridView1!.Columns["Surname"];
            GridColumn phoneNumberCol = gridView1!.Columns["PhoneNumber"];
            GridColumn sellPriceCol = gridView1!.Columns["SellPrice"];
            nameCol.Caption = "Имя";
            surnameCol.Caption = "Фамилия";
            phoneNumberCol.Caption = "Номер телефона";
            sellPriceCol.Caption = "Сумма продаж";
            idCol.Visible = false;
        }

        private async Task InitProductList()
        {
            productsGrid.DataSource = await GetProductsDataSource();
            GridView? gridView1 = productsGrid.MainView as GridView;
            GridColumn nameCol = gridView1!.Columns["Name"];
            nameCol.Caption = "Нименование";
            GridColumn priceCol = gridView1.Columns["Price"];
            priceCol.Caption = "Цена";
        }


        private void GridView1_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
            {
                DXMenuItem item = new DXMenuItem("Редактирование информации о клиенте");
                item.Click += async (o, args) =>
                {
                    var s = (GridView)sender;
                    var id = (long)s.GetFocusedRowCellValue("Id");
                    using var context = new AppDbContext();
                    User user = await context.Users!.FirstOrDefaultAsync(x => x.Id == id);
                    if (user != null)
                    {
                        ClientForm form = new(user);
                        if (form.ShowDialog() == DialogResult.OK)
                            clientGrid.DataSource = await GetUsersDataSource();
                    }
                };
                e.Menu.Items.Add(item);
            }
        }
        private async void simpleButton1_Click(object sender, EventArgs e)
        {
            var frm = new ClientForm();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                clientGrid.DataSource = await GetUsersDataSource();
            }
        }

        private async Task<dynamic> GetUsersDataSource()
        {
            using var context = new AppDbContext();
            var list = await context.Users.Include(x => x.Sales).ThenInclude(x => x.Product).ToListAsync();
            return list.Select(x => new { x.Id, x.Name, x.Surname, x.PhoneNumber, SellPrice = x.Sales?.Sum(a => a.Product?.Price) }); ;
        }

        private async Task<dynamic> GetProductsDataSource()
        {
            using var context = new AppDbContext();
            return await context.Products!.ToListAsync();
        }

        private async void simpleButton2_Click(object sender, EventArgs e)
        {
            var frm = new ProductForm();
            if (frm.ShowDialog() == DialogResult.OK)
                productsGrid.DataSource = await GetProductsDataSource();
        }

        private async void simpleButton3_Click(object sender, EventArgs e)
        {
            var frm = new SellForm();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                clientGrid.DataSource = await GetUsersDataSource();
            }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            var frm = new SummaryForm();
            frm.Show(this);
        }
    }
}

