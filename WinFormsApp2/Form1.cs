using Microsoft.EntityFrameworkCore;

namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        ApplicationContext context = new ApplicationContext();
        public Form1()
        {
            InitializeComponent();

            // гарантируем, что база данных создана
            context.Database.EnsureCreated();
            // загружаем данные из БД
            context.Users.Load();
            // и устанавливаем данные в качестве контекста
            dataGridView1.DataSource = context.Users.Local.ToList();
        }


        private void AddBtn_Click(object sender, EventArgs e)
        {
            UserForm userForm = new UserForm(new User());
            var dialog = userForm.ShowDialog();
            if (dialog == DialogResult.OK) 
            { 
                User user = new User();
                user.Name = userForm.user.Name;
                user.Age = userForm.user.Age;
                context.Users.Add(user);
                context.SaveChanges();
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = context.Users.Local.ToList();
            }

        }

        private void EditBtn_Click(object sender, EventArgs e)
        {

        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {

        }
    }
}