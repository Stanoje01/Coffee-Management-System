using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjektniZadatak
{
    public partial class AdminMainForm : Form
    {
        public AdminMainForm()
        {
            InitializeComponent();
        }

        private void AdminMainForm_Load(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void close_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Da li ste sigurni da zelite da izadjete iz aplikacije?","Potvrdna poruka",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if(result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void logout_btn_Click(object sender, EventArgs e)
        {
            DialogResult check = MessageBox.Show("Da li ste sigurni da zelite da se izlogujete iz aplikacije?", "Potvrdna poruka", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if(check == DialogResult.Yes)
            {
                LoginForm login = new LoginForm();
                login.Show();
                this.Hide();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            adminDashboardForm1.Visible = true;
            adminAddUsers1.Visible = false;
            adminAddProduct1.Visible = false;

            AdminDashboardForm adForm = adminDashboardForm1 as AdminDashboardForm;
           
            if(adForm != null)
            {
                adForm.refreshData();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            adminDashboardForm1.Visible = false;
            adminAddUsers1.Visible = true;
            adminAddProduct1.Visible = false;

            AdminAddUsers aaUsers = adminAddUsers1 as AdminAddUsers;

            if (aaUsers != null)
            {
                aaUsers.refreshData();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            adminDashboardForm1.Visible = false;
            adminAddUsers1.Visible = false;
            adminAddProduct1.Visible = true;

            AdminAddProduct aaProd = adminAddProduct1 as AdminAddProduct;
            if(aaProd != null)
            {
                aaProd.refreshData();
            }
        }
    }
}
