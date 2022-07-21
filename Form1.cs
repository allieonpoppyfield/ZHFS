using DevExpress.Utils.Menu;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.EntityFrameworkCore;
using ZHFS.Database;
namespace ZHFS;
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
        await InitSaleList();
    }

    private async Task InitUserList()
    {
        clientGrid.DataSource = await GetUsersDataSource();
        clientGrid.ToolTipController = new();
        GridView? gridView1 = clientGrid.MainView as GridView;
        gridView1.OptionsBehavior.Editable = false;
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
        gridView1.OptionsBehavior.Editable = false;
        GridColumn nameCol = gridView1!.Columns["Name"];
        nameCol.Caption = "Наименование";
        GridColumn priceCol = gridView1.Columns["Price"];
        priceCol.Caption = "Цена";
        gridView1.Columns["Id"].Visible = false; ;
    }

    private async Task InitSaleList()
    {
        sellsGrid.DataSource = await GetSellsDataSource();
        GridView? salesGridView = sellsGrid.MainView as GridView;
        salesGridView.OptionsBehavior.Editable = false;
        salesGridView.Columns["UserName"].Caption = "Клиент";
        salesGridView.Columns["ProductName"].Caption = "Товар";
        salesGridView.Columns["Price"].Caption = "Цена товара";
        salesGridView.Columns["Count"].Caption = "Количество";
        salesGridView.Columns["TotalPrice"].Caption = "Сумма";
        salesGridView.Columns["CreatedDt"].Caption = "Дата продажи";
    }

    private void GridView1_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
    {
        if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
        {
            DXMenuItem editUSerItem = new DXMenuItem("Редактирование информации о клиенте");
            editUSerItem.Click += async (o, args) =>
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
            e.Menu.Items.Add(editUSerItem);

            DXMenuItem deleteUSerItem = new DXMenuItem("Удалить клиента");
            deleteUSerItem.Click += async (o, args) =>
            {
                if (XtraMessageBox.Show("Действительно удалить данного клиента? Будет также удалена вся статистика продаж по клиенту."
                    , "Подтверждение удаления", MessageBoxButtons.YesNo) == DialogResult.No)
                    return;

                var s = (GridView)sender;
                var id = (long)s.GetFocusedRowCellValue("Id");
                using var context = new AppDbContext();
                User user = await context.Users!.FirstOrDefaultAsync(x => x.Id == id);
                List<Sale> sales = await context.Sales.Where(x => x.User.Id == user.Id).ToListAsync();
                List<SaleItem> saleItems = await context.SaleItems.Where(x => sales.Select(x => x.Id).Contains(x.Sale.Id)).ToListAsync();
                context.SaleItems.RemoveRange(saleItems);
                context.Sales.RemoveRange(sales);
                context.Users.Remove(user);
                await context.SaveChangesAsync();
                clientGrid.DataSource = await GetUsersDataSource();
                sellsGrid.DataSource = await GetSellsDataSource();
            };
            e.Menu.Items.Add(deleteUSerItem);


            DXMenuItem addSaleItem = new DXMenuItem("Добавить продажу для клиента");
            addSaleItem.Click += async (o, args) =>
            {
                var s = (GridView)sender;
                var id = (long)s.GetFocusedRowCellValue("Id");
                var frm = new SellForm(id);
                if (frm.ShowDialog() == DialogResult.OK)
                    sellsGrid.DataSource = await GetSellsDataSource();
            };
            e.Menu.Items.Add(addSaleItem);

            DXMenuItem createReportItem = new DXMenuItem("Сформировать отчет о проданных товарах по клиенту");
            createReportItem.Click += (o, args) =>
            {
                var datePeriodForm = new DatePeriodForm();
                if (datePeriodForm.ShowDialog() == DialogResult.OK)
                {
                    var s = (GridView)sender;
                    var id = (long)s.GetFocusedRowCellValue("Id");
                    var frm = new ClientReportForm(id, datePeriodForm.DateFrom, datePeriodForm.DateTo);
                    frm.Show(this);
                }
            };
            e.Menu.Items.Add(createReportItem);
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
        var list = await context.Users.Include(x => x.Sales).ThenInclude(x => x.SaleItems).ThenInclude(x => x.Product).ToListAsync();
        var r = list.Select(x => new { x.Id, x.Name, x.Surname, x.PhoneNumber, SellPrice = x.Sales.Sum(x => x.SaleItems.Sum(x => x.Product.Price * x.Count)) });
        return r;
    }

    private async Task<dynamic> GetProductsDataSource()
    {
        using var context = new AppDbContext();
        return await context.Products!.Select(x => new { x.Id, x.Name, x.Price }).ToListAsync();
    }
    private async Task<dynamic> GetSellsDataSource()
    {
        using var context = new AppDbContext();
        var list = await context.SaleItems.Include(x => x.Product)
            .Include(x => x.Sale).ThenInclude(x => x.User)
            .Select(x => new { UserName = x.Sale.User.Name, ProductName = x.Product.Name, x.Sale.CreatedDt, Price = x.Product.Price, Count = x.Count, TotalPrice = x.Count * x.Product.Price }).ToListAsync();
        return list;
    }

    private async void simpleButton2_Click(object sender, EventArgs e)
    {
        var frm = new ProductForm();
        if (frm.ShowDialog() == DialogResult.OK)
            productsGrid.DataSource = await GetProductsDataSource();
    }

    private void simpleButton4_Click(object sender, EventArgs e)
    {
        var datePeriodForm = new DatePeriodForm();
        if (datePeriodForm.ShowDialog() == DialogResult.OK)
        {
            var frm = new SummaryForm(datePeriodForm.DateFrom, datePeriodForm.DateTo);
            frm.Show(this);
        }
    }
}

