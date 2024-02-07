using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ProjektniZadatak
{
    internal class AdminAddUsersData
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Sasa\Documents\baza.mdf;Integrated Security=True;Connect Timeout=30");
        public int ID { get; set; }
        public string Username {  get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Status { get; set; }
        public string Image { get; set; }
        public string DateRegistered { get; set; }
        public List<AdminAddUsersData> usersListData()
        {
            List<AdminAddUsersData> listaData = new List<AdminAddUsersData>();

            if(conn.State != ConnectionState.Open)
            {
                try
                {
                    conn.Open();

                    string selectData = "SELECT * FROM users";

                        using (SqlCommand cmd = new SqlCommand(selectData, conn))
                    {
                        SqlDataReader reader =  cmd.ExecuteReader();

                        while(reader.Read())
                        {
                            AdminAddUsersData data = new AdminAddUsersData();
                            data.ID = (int)reader["id"];
                            data.Username = reader["username"].ToString();
                            data.Password = reader["password"].ToString();
                            data.Role = reader["role"].ToString();
                            data.Status = reader["status"].ToString();
                            data.Image = reader["profile_image"].ToString();
                            data.DateRegistered = reader["date_reg"].ToString();

                            listaData.Add(data);
                        }
                    }
                }
                catch (Exception ex)
                {

                    Console.WriteLine("Pukla konekcija", ex);
                }
                finally { conn.Close(); }
            }

            return listaData;
        }
    }
}
