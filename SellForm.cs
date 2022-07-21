using DevExpress.Utils.Menu;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Data;
using System.Printing;
using ZHFS.Database;

namespace ZHFS
{
    public partial class SellForm : Form
    {
        private List<SaleData> currentProducts = new();
        private readonly long? UserId;

        public SellForm()
        {
            InitializeComponent();
            dtp.Value = DateTime.Now;
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
            await InitAvalaibleProductsGrid();
            await InitCurrentProducts();
        }

        private async Task InitAvalaibleProductsGrid()
        {
            avalaibleProductsGrid.DataSource = await GetAvalaibleProductsDataSource();

            GridView? avalaibleProductsGridView = avalaibleProductsGrid.MainView as GridView;
            avalaibleProductsGridView.OptionsView.ShowGroupPanel = false;
            avalaibleProductsGridView.OptionsBehavior.Editable = false;
            avalaibleProductsGridView.Columns["Id"].Visible = false;
            avalaibleProductsGridView.Columns["Name"].Caption = "Наименование товара";
            avalaibleProductsGridView.Columns["Price"].Caption = "Цена";
            avalaibleProductsGridView.PopupMenuShowing += GridView1_PopupMenuShowing;
        }

        private async Task InitCurrentProducts()
        {
            currentProductsGrid.DataSource = this.currentProducts;
            var currentProductGridView = currentProductsGrid.MainView as GridView;
            currentProductGridView.Columns["productId"].Visible = false;
            currentProductGridView.Columns["count"].Caption = "Количество";
            currentProductGridView.Columns["name"].Caption = "Наименование товара";
            currentProductGridView.OptionsView.ShowGroupPanel = currentProductGridView.OptionsBehavior.Editable = false;
            currentProductGridView.PopupMenuShowing += CurrentProductsGridViewPopupMenuShowing; ;
        }

        private void CurrentProductsGridViewPopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
            {
                DXMenuItem deleteProductItem = new DXMenuItem("Удалить товар из продажи");
                deleteProductItem.Click += async (o, a) =>
                {
                    var s = (GridView)sender;
                    var productId = (int)s.GetFocusedRowCellValue("productId");
                    var remProduct = currentProducts.FirstOrDefault(x => x.productId == productId);
                    currentProducts.Remove(remProduct);
                    currentProductsGrid.RefreshDataSource();
                };
                e.Menu.Items.Add(deleteProductItem);
            }
        }

        private void GridView1_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
            {
                DXMenuItem addProductItem = new DXMenuItem("Добавить товар в текущую продажу");
                addProductItem.Click += async (o, a) =>
                {
                    var s = (GridView)sender;
                    var productId = (int)s.GetFocusedRowCellValue("Id");

                    TextEdit textEdit = new TextEdit();
                    textEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                    textEdit.Properties.Mask.EditMask = "n0";
                    XtraInputBoxArgs args = new();
                    args.Caption = "Добавление товара в текущую продажу";
                    args.Prompt = "Укажите количество товара";
                    args.DefaultButtonIndex = 0;
                    args.DefaultResponse = "Test";
                    args.Editor = textEdit;

                    if (int.TryParse((string)XtraInputBox.Show(args), out int result))
                    {
                        using var context = new AppDbContext();
                        var product = await context.Products.FirstOrDefaultAsync(x => x.Id == productId);
                        this.currentProducts.Add(new(productId, result, product!.Name));
                        currentProductsGrid.RefreshDataSource();
                    }
                };
                e.Menu.Items.Add(addProductItem);
            }
        }

        private async Task<dynamic> GetAvalaibleProductsDataSource()
        {
            using var context = new AppDbContext();
            return await context.Products!.Select(x => new { x.Id, x.Name, x.Price }).ToListAsync();
        }

        private async void saveBtn_Click(object sender, EventArgs e)
        {
            using var context = new AppDbContext();
            var user = await context.Users.FirstOrDefaultAsync(x => x.Id == UserId);
            var sale = new Sale()
            {
                User = user,
                CreatedDt = dtp.Value,
            };
            await context.Sales.AddAsync(sale);

            ArgumentNullException.ThrowIfNull(user);
            foreach (var product in currentProducts)
            {
                var saleItem = new SaleItem()
                {
                    Sale = sale,
                    Count = product.count,
                    Product = await context.Products.FirstOrDefaultAsync(x => x.Id == product.productId)
                };
                await context.SaleItems.AddAsync(saleItem);
            }
            await context.SaveChangesAsync();
            this.DialogResult = DialogResult.OK;
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
    public record SaleData(int productId, int count, string name);
}
