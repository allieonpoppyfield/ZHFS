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

            GridView? gridView1 = avalaibleProductsGrid.MainView as GridView;
            gridView1.OptionsView.ShowGroupPanel = false;
            gridView1.OptionsBehavior.Editable = false;
            gridView1.Columns["Id"].Visible = false;
            gridView1.Columns["Name"].Caption = "Наименование товара";
            gridView1.Columns["Price"].Caption = "Цена";
            gridView1.PopupMenuShowing += GridView1_PopupMenuShowing;
        }

        private async Task InitCurrentProducts()
        {
            currentProductsGrid.DataSource = this.currentProducts;
        }

        private void GridView1_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
            {
                DXMenuItem editUSerItem = new DXMenuItem("Добавить товар в текущую продажу");
                editUSerItem.Click += async (o, a) =>
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
                e.Menu.Items.Add(editUSerItem);
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
