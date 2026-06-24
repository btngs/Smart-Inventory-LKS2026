using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BIntangSyahriMahardika_Module2
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        public static string hashPassword(string password)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }

        private void btnAuth_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text)) 
            {
                MessageBox.Show("Semua Field Harus Diisi");
                return;
            }
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = koneksi.connString;
                connection.Open();
                try
                {
                    string query = "SELECT COUNT(1) FROM users WHERE username = @username AND password = @passwordHash";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", txtUsername.Text.Trim());
                        command.Parameters.AddWithValue("@passwordHash", hashPassword(txtPassword.Text));

                        int totaluser = (int)command.ExecuteScalar();

                        if (totaluser > 0)
                        {
                            userSession.username = txtUsername.Text;
                            Dashboard dashboard = new Dashboard();
                            dashboard.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("username atau password salah", "Login gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }

            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked) 
            {
                txtPassword.UseSystemPasswordChar = false;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
            }
        }
    }
}
