using Microsoft.EntityFrameworkCore;
using WinFormsApp2.Database;
using WinFormsApp2.Database.Models;

namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        Database.ApplicationContext context = new Database.ApplicationContext();
        User? _user;
        public Form1()
        {
            InitializeComponent();

            LoadAndTestData();
            
        }
        public void LoadAndTestData() {
            // гарантируем, что база данных создана
            context.Database.EnsureCreated();
            //if (context.Roles.Where(x => x.Name == "Admin").Count() == 0){
            //    var role_admin = new Role();
            //    role_admin.Name = "Admin";
            //    var role_user = new Role();
            //    role_user.Name = "User";
            //    context.Roles.Add(role_admin);
            //    context.Roles.Add(role_user);
            //    context.SaveChanges();
            //}
            //// загружаем данные из БД
            context.Users.Load();
            // и устанавливаем данные в качестве контекста
            dataGridView1.DataSource = context.Users.Local.ToList();
        }
        //private void setDataToDataGrid() {
        //    List<UserView> usersView = new List<UserView>();
        //    foreach (var user in context.Users.Local.ToList())
        //    {
        //        UserView userView = new UserView();
        //        userView.Id = user.Id;
        //        userView.Name = user.Name;
        //        userView.Age = user.Age;
        //        //userView.Role = user.Role.Name;
        //        usersView.Add(userView);
        //    }
        //    dataGridView1.DataSource = usersView;
        //}


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
                dataGridView1.DataSource = context.Users.Local.ToList();
            }

        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            UserForm userForm = new UserForm(_user);
            var dialog = userForm.ShowDialog();
            if (dialog == DialogResult.OK)
            {
                User user = context.Users.First(x => x.Id == userForm.user.Id);
                if (user == null) return; 
                user.Name = userForm.user.Name;
                user.Age = userForm.user.Age;
                context.SaveChanges();
                dataGridView1.DataSource = context.Users.Local.ToList();
                DeleteBtn.Enabled = false;
                EditBtn.Enabled = false;
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (_user != null)
            {
                context.Users.Remove(_user);
                context.SaveChanges();
                dataGridView1.DataSource = context.Users.Local.ToList();
            }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //User? _user = dataGridView1.SelectedRows[0].DataBoundItem as User;
            _user =  dataGridView1.Rows[e.RowIndex].DataBoundItem as User;
            DeleteBtn.Enabled = true;
            EditBtn.Enabled = true;

        }
    }
}