using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BIntangSyahriMahardika_Module2
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            TampilTop5();
            txtGreet.Text = "Welcome, " + userSession.username;

            using (SqlConnection connection = koneksi.GetConnection())
            {
                string query = "SELECT ISNULL(SUM(stock), 0) AS total FROM products";
                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();
                object total = cmd.ExecuteScalar();
                txtTotal.Text = total.ToString();
            }

            using (SqlConnection connection = koneksi.GetConnection())
            {
                string query = "SELECT COUNT(*) FROM products WHERE stock < 10";
                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();
                object stock = cmd.ExecuteScalar();
                txtStock.Text = stock.ToString();
            }

            using (SqlConnection connection = koneksi.GetConnection())
            {
                string query = "SELECT ISNULL(SUM(price * stock), 0) AS totalAset FROM products";
                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();
                decimal harga = Convert.ToDecimal(cmd.ExecuteScalar());
                txtAset.Text = FormatRupiah(harga);
            }

        }

        private void TampilTop5()
        {
            listViewLowStock.Items.Clear();

            using (SqlConnection connection = koneksi.GetConnection())
            {
                string query = @"
                    SELECT TOP 5
                        sku,
                        name_product,
                        stock
                    FROM dbo.products
                    ORDER BY stock ASC, name_product ASC";

                SqlCommand cmd = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    int nomor = 1;

                    while (reader.Read())
                    {
                        ListViewItem item = new ListViewItem(nomor.ToString());
                        item.SubItems.Add(reader["sku"].ToString());
                        item.SubItems.Add(reader["name_product"].ToString());
                        item.SubItems.Add(reader["stock"].ToString());
                        listViewLowStock.Items.Add(item);
                        nomor++;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memuat data dashboard: " + ex.Message);
                }
            }
        }

        private string FormatRupiah(decimal nilai)
        {
            if (nilai >= 1000000000000m)
                return "Rp " + (nilai / 1000000000000m).ToString("0.##") + "T";
            else if (nilai >= 1000000000m)
                return "Rp " + (nilai / 1000000000m).ToString("0.##") + "M";
            else if (nilai >= 1000000m)
                return "Rp " + (nilai / 1000000m).ToString("0.##") + "Jt";
            else if (nilai >= 1000m)
                return "Rp " + (nilai / 1000m).ToString("0.##") + "Rb";
            else
                return "Rp " + nilai.ToString("0");
        }

        private void label3_Click(object sender, EventArgs e)
        {
            MasterBarang masterBarang = new MasterBarang();
            masterBarang.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoginForm login = new LoginForm();
            login.Show();
            this.Hide();
        }

        private void txtGreet_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MasterBarang mb = new MasterBarang();
            mb.Show();
            this.Hide();
        }
    }
}
