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

            if (new string[] { nameTb.Text, surnameTb.Text, phoneTb.Text }.FirstOrDefault(x => string.IsNullOrWhiteSpace(x)) != null)
            {
                MessageBox.Show("Заполните все данные!");
                return;
            }
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
