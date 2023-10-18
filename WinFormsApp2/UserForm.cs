using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp2.Database.Models;

namespace WinFormsApp2
{
    public partial class UserForm : Form
    {
        public User user;
        Database.ApplicationContext context = new Database.ApplicationContext();
        public UserForm(User? user)
        {
            InitializeComponent();
            if (user.Id != 0) {
                textBox1.Text = user.Name.ToString();
                textBox2.Text = user.Age.ToString();
            }
            context.Roles.Load();
            comboBox1.DataSource = context.Roles.Local.ToList();
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "Id";
            comboBox1.SelectedIndex = 1;
            this.user = user;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.user.Name = textBox1.Text;
            this.user.Age = Convert.ToInt32(textBox2.Text);
            this.user.Role_id = (comboBox1.SelectedItem as Role).Id;
            this.user.Role = comboBox1.SelectedItem as Role;
            context.SaveChanges();
            context.Database.CloseConnection();
            //this.user.Role = context.Roles.FirstOrDefault(x => x.Id == Convert.ToInt32(comboBox1.SelectedValue));
            DialogResult = DialogResult.OK;
        }

    }
}
