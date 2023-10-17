using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp2
{
    public partial class UserForm : Form
    {
        public User user;
        public UserForm(User user)
        {
            InitializeComponent();
            this.user = user;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.user.Name = textBox1.Text;
            this.user.Age = Convert.ToInt32(textBox2.Text);
            DialogResult = DialogResult.OK;
        }
    }
}
