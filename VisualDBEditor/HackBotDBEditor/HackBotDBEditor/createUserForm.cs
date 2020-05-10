using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HackBotDBEditor
{
    public partial class createUserForm : Form
    {
        DatabaseAdapter DBAdapter = new DatabaseAdapter();
        public createUserForm()
        {
            InitializeComponent();
        }

        private void createUser_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(loginText.Text) && !string.IsNullOrEmpty(lastNameText.Text)
                && !string.IsNullOrEmpty(patronymText.Text))
            {
                if (DBAdapter.createUser(loginText.Text, lastNameText.Text, firstNameText.Text, patronymText.Text))
                    this.Close();
            }
            else
            {
                MessageBox.Show("Не все поля заполнены");
            }
        }
    }
}
