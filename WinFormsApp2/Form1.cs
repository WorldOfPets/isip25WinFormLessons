using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
            // CREATE TABLE User ( integer ID PRYMARY KEY ....);
            if (context.Roles.Where(x => x.Name == "Admin").Count() == 0) {
                var admin = new Role();
                admin.Name = "Admin";
                var user = new Role();
                user.Name = "User";
                context.Roles.Add(admin);
                context.Roles.Add(user);
                context.SaveChanges();
            }
            //// загружаем данные из БД
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
                //INSERT INTO User () VALUES ()
                User user = new User();
                user.Name = userForm.user.Name;
                user.Age = userForm.user.Age;
                user.Role_id = userForm.user.Role_id;
                user.Role = null;
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
                //UPDATE user
                //SET name = value1, age = value2, ...
                //WHERE id == id;
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
                // DELETE FROM user WHERE id = id;
                context.Users.Remove(_user);
                context.SaveChanges();
                dataGridView1.DataSource = context.Users.Local.ToList();
            }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //User? _user = dataGridView1.SelectedRows[0].DataBoundItem as User;
            _user = dataGridView1.Rows[e.RowIndex].DataBoundItem as User;
            DeleteBtn.Enabled = true;
            EditBtn.Enabled = true;
            

        }
    }
}