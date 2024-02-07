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
    public partial class CashierCustomersForm : UserControl
    {
        public CashierCustomersForm()
        {
            InitializeComponent();
            displayCustomerData();
        }

        public void displayCustomerData()
        {
            CustomerData customerData = new CustomerData();
            List<CustomerData> list = customerData.allCustomersData();
            
            datagridview1.DataSource = list;
            

        }

        public void refreshData()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)refreshData);
                return;
            }
            displayCustomerData();
        }
    }
}
