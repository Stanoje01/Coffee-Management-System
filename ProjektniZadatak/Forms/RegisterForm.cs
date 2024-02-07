using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjektniZadatak
{
    public partial class RegisterForm : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Sasa\Documents\baza.mdf;Integrated Security=True;Connect Timeout=30");
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void register_loginBtn_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Hide(); 
        }

        private void register_showPass_CheckedChanged(object sender, EventArgs e)
        {
            register_password.PasswordChar = register_showPass.Checked ? '\0' : '*';
            register_cPassword.PasswordChar = register_showPass.Checked ? '\0' : '*';
        }

        public bool emptyFields()
        {
            if (register_username.Text == "" ||register_password.Text == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void register_btn_Click(object sender, EventArgs e)
        {
            if (emptyFields())
            {
                MessageBox.Show("Sva polja moraju biti popunjena!!!", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if(conn.State == ConnectionState.Closed)
                {
                    try
                    {
                        conn.Open();
                        string selectUsername = "SELECT * FROM users where username = @usern";

                        using (SqlCommand checkUsername = new SqlCommand(selectUsername, conn))
                        {
                            checkUsername.Parameters.AddWithValue("@usern", register_username.Text.Trim());

                            SqlDataAdapter adapter = new SqlDataAdapter(checkUsername);
                            DataTable table = new DataTable();

                            adapter.Fill(table);

                            if (table.Rows.Count >= 1)
                            {
                                string usern = register_username.Text.Substring(0, 1).ToUpper() + register_username.Text.Substring(1);
                                MessageBox.Show(usern + " je vec zauzet!", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            } else if (register_password.Text != register_cPassword.Text)
                            {
                                MessageBox.Show(" Lozinke se ne podudaraju!!", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            } else if (register_password.Text.Length < 6)
                                {
                                MessageBox.Show("Lozinka mora imati više od 6 karaktera!!!", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                string insertData = "INSERT INTO users(username, password, profile_image, role, status, date_reg) " +
                                    "VALUES (@usern, @pass, @image, @role, @status, @date)";
                                DateTime today = DateTime.Today;
                                using (SqlCommand cmd = new SqlCommand(insertData, conn))
                                {
                                    cmd.Parameters.AddWithValue("@usern", register_username.Text.Trim());
                                    cmd.Parameters.AddWithValue("@pass", register_password.Text.Trim());
                                    cmd.Parameters.AddWithValue("@image", "");
                                    cmd.Parameters.AddWithValue("@role", "Kasir");
                                    cmd.Parameters.AddWithValue("@status","Dozvoljen");
                                    cmd.Parameters.AddWithValue("@date",today);

                                    cmd.ExecuteNonQuery();
                                    MessageBox.Show("Uspešna registracija!!!", "Information message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    LoginForm login = new LoginForm();
                                    login.Show();
                                    this.Hide();
                                }
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

