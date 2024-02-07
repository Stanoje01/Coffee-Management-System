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
using System.IO;
using System.Runtime.Remoting.Contexts;


namespace ProjektniZadatak
{
    public partial class AdminAddUsers : UserControl
    {

        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Sasa\Documents\baza.mdf;Integrated Security=True;Connect Timeout=30");
        public AdminAddUsers()
        {
            InitializeComponent();

            displayAddUsersData();
        }

        public void refreshData()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)refreshData);
                return;
            }

            displayAddUsersData();
        }

        public void displayAddUsersData()
        {
            AdminAddUsersData usersData = new AdminAddUsersData();
            List<AdminAddUsersData> listData = usersData.usersListData();

            dataGridView1.DataSource = listData;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void AdminAddUsers_Load(object sender, EventArgs e)
        {

        }

        public bool emptyFields()
        {
            if (adminAddUsers_username.Text == "" || adminAddUsers_password.Text == "" || adminAddUsers_role.Text == "" || adminAddUsers_status.Text == "" || adminAddUsers_imageView.Image == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void adminAddUsers_addBtn_Click(object sender, EventArgs e)
        {
            if (emptyFields())
            {
                MessageBox.Show("Sva polja moraju biti popunjena!!!", "Greska", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (conn.State == ConnectionState.Closed)
                {
                    try
                    {
                        conn.Open();

                        string selectUsern = "SELECT * FROM users WHERE username = @usern";

                        using (SqlCommand cmd = new SqlCommand(selectUsern, conn))
                        {
                            cmd.Parameters.AddWithValue("@usern", adminAddUsers_username.Text.Trim());
                            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                            DataTable table = new DataTable();
                            adapter.Fill(table);

                            if (table.Rows.Count >= 1)
                            {
                                string usern = adminAddUsers_username.Text.Substring(0, 1).ToUpper() + adminAddUsers_username.Text.Substring(1);
                                MessageBox.Show(usern + " vec ima u bazi!", "Greska", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                string insertData = "INSERT INTO users(username, password, profile_image, role, status, date_reg) " +
                                    "VALUES (@usern, @pass, @image, @role, @status, @date)";

                                DateTime today = DateTime.Today;

                                string path = Path.Combine(@"C:\Users\Sasa\Desktop\C#\ProjektniZadatak\ProjektniZadatak\SlikeKorisnika\" + adminAddUsers_username.Text.Trim() + ".jpg");


                                string directoryPath = Path.GetDirectoryName(path);
                                if (!Directory.Exists(directoryPath))
                                {
                                    Directory.CreateDirectory(directoryPath);
                                }

                                File.Copy(adminAddUsers_imageView.ImageLocation, path, true);
                                using (SqlCommand cmd2 = new SqlCommand(insertData, conn))
                                {
                                    cmd2.Parameters.AddWithValue("@usern", adminAddUsers_username.Text.Trim());
                                    cmd2.Parameters.AddWithValue("@pass", adminAddUsers_password.Text.Trim());
                                    cmd2.Parameters.AddWithValue("@image", path);
                                    cmd2.Parameters.AddWithValue("@role", adminAddUsers_role.Text.Trim());
                                    cmd2.Parameters.AddWithValue("@status", adminAddUsers_status.Text.Trim());
                                    cmd2.Parameters.AddWithValue("@date", today);

                                    cmd2.ExecuteNonQuery();
                                    MessageBox.Show("Uspešno unesen nov korisnik!!!", "Information message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    displayAddUsersData();

                                }
                            }
                        }


                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("Konekcija nije uspela" + ex, "Greska", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

        }

        private void adminAddUsers_importBtn_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Image Files(*.jpg; *.png|*.jpg;*.png)";
                string imagePath = "";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    imagePath = dialog.FileName;
                    adminAddUsers_imageView.ImageLocation = imagePath;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Greška!!!" + ex, "Greska", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int id = 0;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
            id = (int)row.Cells[0].Value;
            adminAddUsers_username.Text = row.Cells[1].Value.ToString();
            adminAddUsers_password.Text = row.Cells[2].Value.ToString();
            adminAddUsers_role.Text = row.Cells[3].Value.ToString();
            adminAddUsers_status.Text = row.Cells[4].Value.ToString();

            string imagePath = row?.Cells[5].Value.ToString();

            if (imagePath != null && File.Exists(imagePath))
            {
                adminAddUsers_imageView.Image = Image.FromFile(imagePath);
            }
            else
            {
             
                adminAddUsers_imageView.Image = null;
            }
        }

        private void adminAddUsers_updateBtn_Click(object sender, EventArgs e)
        {
            if (emptyFields())
            {

                MessageBox.Show("Sva polja moraju biti popunjena!!!", "Greska", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else
            {
                DialogResult result = MessageBox.Show("Da li ste sigurni da želite da promenite svoj username: " + adminAddUsers_username.Text.Trim() + "?", "Potvrdna poruka",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        try
                        {
                            conn.Open();

                            string updateData = "UPDATE users SET username = @usern, password=@pass, role = @role, status = @status WHERE id = @id";

                            using (SqlCommand cmd = new SqlCommand(updateData, conn))
                            {
                                cmd.Parameters.AddWithValue("@usern", adminAddUsers_username.Text.Trim());
                                cmd.Parameters.AddWithValue("@pass", adminAddUsers_password.Text.Trim());
                                cmd.Parameters.AddWithValue("@role", adminAddUsers_role.Text.Trim());
                                cmd.Parameters.AddWithValue("@status", adminAddUsers_status.Text.Trim());
                                cmd.Parameters.AddWithValue("@id", id);

                                cmd.ExecuteNonQuery();


                                clearFileds();

                                MessageBox.Show("Uspešno ste ažurirali!", "Potvrdna poruka", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                displayAddUsersData();
                            }
                        }
                        catch (Exception ex)
                        {

                            MessageBox.Show("Neuspešna konekcija: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        finally
                        {
                            conn.Close();
                        }
                    }
                }
            }
        }


        private void adminAddUsers_clearBtn_Click(object sender, EventArgs e)
        {
            clearFileds();
        }

        private void adminAddUsers_deleteBtn_Click(object sender, EventArgs e)
        {
            if (emptyFields())
            {
                MessageBox.Show("Sva polja moraju biti popunjena!!!.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult result = MessageBox.Show("Da li ste sigurni da zelite da izbrisete Username: " + adminAddUsers_username.Text.Trim()
                    + "?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        try
                        {
                            conn.Open();

                            string deleteData = "DELETE FROM users WHERE id = @id";

                            using (SqlCommand cmd = new SqlCommand(deleteData, conn))
                            {
                                cmd.Parameters.AddWithValue("@id", id);

                                cmd.ExecuteNonQuery();
                                clearFileds();

                                MessageBox.Show("Uspešno obrisan korisnik!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                displayAddUsersData();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Neuspešna konekcija: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }
                }
            }
        }
        public void clearFileds()
        {
            adminAddUsers_username.Text = "";
            adminAddUsers_password.Text = "";
            adminAddUsers_role.SelectedIndex = -1;
            adminAddUsers_status.SelectedIndex = -1;
            adminAddUsers_imageView.Image = null;
        }
    }
}
