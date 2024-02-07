using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjektniZadatak
{
    public partial class CashierMainForm : Form
    {
        public CashierMainForm()
        {
            InitializeComponent();
        }

        private void close_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Da li ste sigurni da želite da izađete iz aplikacije?", "Potvrdna poruka", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void logout_btn_Click(object sender, EventArgs e)
        {
            DialogResult check = MessageBox.Show("Da li ste sigurni da želite da se izlogujete iz aplikacije?", "Potvrdna poruka", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (check == DialogResult.Yes)
            {
                LoginForm login = new LoginForm();
                login.Show();
                this.Hide();
            }
        }

        private void orderForm1_Load(object sender, EventArgs e)
        {

        }

        private void dashboard_btn_Click(object sender, EventArgs e)
        {
            adminDashboardForm1.Visible = true;
            adminAddProduct1.Visible = false;
            cashierCustomersForm1.Visible = false;
            orderForm1.Visible = false;
            AdminDashboardForm adForm = adminDashboardForm1 as AdminDashboardForm;

            if(adForm != null)
            {
                adForm.refreshData();
            }

        }
        private void addProducts_btn_Click(object sender, EventArgs e)
        {
            adminDashboardForm1.Visible = false;
            adminAddProduct1.Visible = true;
            cashierCustomersForm1.Visible = false;
            orderForm1.Visible = false;
            AdminAddProduct aaProd = adminAddProduct1 as AdminAddProduct;

            if (aaProd != null)
            {
                aaProd.refreshData();
            }
        }

        private void customer_btn_Click(object sender, EventArgs e)
        {
            adminDashboardForm1.Visible = false;
            adminAddProduct1.Visible = false;
            cashierCustomersForm1.Visible = true;
            orderForm1.Visible = false;

            CashierCustomersForm ccForm = cashierCustomersForm1 as CashierCustomersForm;

            if (ccForm != null)
            {
                ccForm.refreshData();
            }
        }

        

        private void order_btn_Click(object sender, EventArgs e)
        {
            adminDashboardForm1.Visible = false;
            adminAddProduct1.Visible = false;
            cashierCustomersForm1.Visible = false;
            orderForm1.Visible = true;

            OrderForm coForm = orderForm1 as OrderForm;

          if( coForm != null)
            {
                coForm.refreshData();   
            }
        }
    }
}
