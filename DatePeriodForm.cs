using ZHFS.Database;

namespace ZHFS
{
    public partial class DatePeriodForm : Form
    {
        public DateTime DateFrom { get; private set; }
        public DateTime DateTo { get; private set; }
        public DatePeriodForm()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (dtFrom.Value.Date > dtTo.Value.Date)
            {
                MessageBox.Show("Некорректный интервал");
                return;
            }

            DateFrom = dtFrom.Value;
            DateTo = dtTo.Value;
            this.DialogResult = DialogResult.OK;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
