//<icbNewItemRow>
//</icbNewItemRow>
using DevExpress.XtraPivotGrid;
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
            using var context = new AppDbContext();

            await pivotGridControl1.SetFieldSortingAsync(null, PivotSortOrder.Ascending);

            PivotGridField name = new PivotGridField()
            {
                Area = PivotArea.RowArea,
                Caption = "Имя UJD^YJ FMFRF",
                FieldName = "name",
            };
            PivotGridField surname = new PivotGridField()
            {
                Area = PivotArea.RowArea,
                Caption = "Фамилия",
                SortOrder = PivotSortOrder.Ascending,
            };
            PivotGridField phoneNumber = new PivotGridField()
            {
                Area = PivotArea.RowArea,
                Caption = "Номер телефона"
            };


            pivotGridControl1.Fields.Add(name);
            pivotGridControl1.Fields.Add(surname);
            pivotGridControl1.Fields.Add(phoneNumber);
            pivotGridControl1.DataSource = context.Users!.ToList();
        }
    }
}

