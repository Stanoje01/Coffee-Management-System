using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjektniZadatak
{
    internal class CustomerData
    {
        SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Sasa\Documents\baza.mdf;Integrated Security=True;Connect Timeout=30");

        public int CustomerID { set; get; }
        public string TotalPrice { set; get; }
        public string Amount { set; get; }
        public string Change { set; get; }
        public string Date { set; get; }

        public List<CustomerData> allCustomersData()
        {
            List<CustomerData> listData = new List<CustomerData>();

            if (connect.State == ConnectionState.Closed)
            {
                try
                {
                    connect.Open();

                    string selectData = "SELECT * FROM customers";

                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            CustomerData cData = new CustomerData();

                            cData.CustomerID = (int)reader["customer_id"];
                            cData.TotalPrice = reader["total_price"].ToString();
                            cData.Amount = reader["amount"].ToString();
                            cData.Change = reader["change"].ToString();
                            cData.Date = reader["date"].ToString();

                            listData.Add(cData);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Neuspešna konekcija: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connect.Close();
                }
            }

            return listData;
        }

    }
}

