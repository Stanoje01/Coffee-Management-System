using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace ProjektniZadatak
{
    public partial class LoginForm : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Sasa\Documents\baza.mdf;Integrated Security=True;Connect Timeout=30");
        public LoginForm()
        {
            InitializeComponent();
        }

        private void close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void login_registerBtn_Click(object sender, EventArgs e)
        {
            RegisterForm registerForm = new RegisterForm();
            registerForm.Show();
            this.Hide();
        }

        private void login_showPass_CheckedChanged(object sender, EventArgs e)
        {
            login_password.PasswordChar = login_showPass.Checked ? '\0' : '*';
            
        }

        public bool emptyFields()
        {
            if(login_username.Text == "" || login_password.Text == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void login_btn_Click(object sender, EventArgs e)
        {
            if(emptyFields())
            {
                MessageBox.Show("Sva polja moraju biti popunjena!!!","Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (conn.State == ConnectionState.Closed)
                {
                    try
                    {
                        conn.Open();
                        string selectAccount = "SELECT COUNT (*) FROM users where username = @usern AND password = @pass AND status = @status";

                        using (SqlCommand cmd = new SqlCommand(selectAccount, conn))
                        {
                            cmd.Parameters.AddWithValue("@usern", login_username.Text.Trim());
                            cmd.Parameters.AddWithValue("@pass", login_password.Text.Trim());
                            cmd.Parameters.AddWithValue("@status", "Aktiviran");


                            int rowCount = (int)cmd.ExecuteScalar();

                            if(rowCount > 0)
                            {
                                string selectRole = "SELECT role FROM users WHERE username = @usern AND password = @pass";

                                using(SqlCommand getRole = new SqlCommand(selectRole, conn))
                                {
                                    getRole.Parameters.AddWithValue("@usern", login_username.Text.Trim());
                                    getRole.Parameters.AddWithValue("@pass", login_password.Text.Trim());

                                    string userRole = getRole.ExecuteScalar() as string;

                                    MessageBox.Show("Uspesno ste se loginovali", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    if (userRole == "Admin")
                                    {
                                        AdminMainForm adminMainForm = new AdminMainForm();
                                        adminMainForm.Show();
                                        this.Hide();
                                    }
                                    else if (userRole == "Kasir")
                                    {
                                        CashierMainForm cashierMainForm = new CashierMainForm();
                                        cashierMainForm.Show();
                                        this.Hide();
                                    }

                                   

                                }
                            }

                            else
                            {
                                MessageBox.Show("Niste uneli dobar username ili šifru ili nemate dozvolu admina!!!", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }



                        }
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("Konekcija nije uspela!!" + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }
    }
}
