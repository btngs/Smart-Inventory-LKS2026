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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BIntangSyahriMahardika_Module2
{
    public partial class AddItem : Form
    {
        public AddItem()
        {
            InitializeComponent();
        }


        void tampilkategori()
        {
            try
            {
                using (SqlConnection connection = koneksi.GetConnection())
                {
                    string query = "SELECT id_category, name_category FROM categories";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    selectKategori.DisplayMember = "name_category";
                    selectKategori.ValueMember = "id_category";
                    selectKategori.DataSource = dt;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat kategori: " + ex.Message);
            }
        }
        string generateSKU()
        {
            string newSKU = "";
            string prefix = "SKU-";

            using (SqlConnection connection = koneksi.GetConnection())
            {
                string query = "SELECT TOP 1 sku FROM products WHERE sku LIKE 'SKU-%' ORDER BY sku DESC";
                SqlCommand cmd = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        string lastSKU = result.ToString();
                        int lastNumber = int.Parse(lastSKU.Substring(prefix.Length));
                        newSKU = prefix + (lastNumber + 1).ToString("D4");
                    }
                    else
                    {
                        newSKU = prefix + "0001";
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Error generating SKU: " + ex.Message);
                    newSKU = prefix + "0001";
                }

            }

            return newSKU;

        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            txtSKU.Text = generateSKU();

            using (SqlConnection connection = koneksi.GetConnection())
            {
                string query = "INSERT INTO products (sku, name_product, id_category, price, stock) VALUES (@sku, @name_product, @id_category, @price, @stock)";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@sku", generateSKU());
                cmd.Parameters.AddWithValue("@name_product", txtNama.Text);
                cmd.Parameters.AddWithValue("@id_category", selectKategori.SelectedValue);
                cmd.Parameters.AddWithValue("@price", numericHarga.Value);
                cmd.Parameters.AddWithValue("@stock", (int)numericStok.Value);
                try
                {
                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Item berhasil ditambahkan!");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Gagal menambahkan item.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void AddItem_Load(object sender, EventArgs e)
        {
            tampilkategori();
        }

        private void selectKategori_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
