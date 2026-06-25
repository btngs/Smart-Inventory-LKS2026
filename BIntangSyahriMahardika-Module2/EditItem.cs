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
    public partial class EditItem : Form
    {
        public EditItem(string sku)
        {
            InitializeComponent();
            this.txtSKU.Text = sku;
        }

        void tampilkategori()
        {
            using (SqlConnection connection = koneksi.GetConnection())
            {
                string query = "SELECT id_category, name_category FROM categories";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                DataRow row = dt.NewRow();
                row["id_category"] = 0;
                row["name_category"] = "--Pilih Kategori--";
                dt.Rows.InsertAt(row, 0);

                selectKategori.DataSource = dt;
                selectKategori.DisplayMember = "name_category";
                selectKategori.ValueMember = "id_category";
            }
        }

        private void EditItem_Load(object sender, EventArgs e)
        {
            tampilkategori();

            using (SqlConnection connection = koneksi.GetConnection())
            {
                string query = "SELECT * FROM products WHERE sku = @sku";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@sku", txtSKU.Text);
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read()) 
                {
                    txtNamaBarang.Text = reader["name_product"].ToString();
                    numHarga.Value = Convert.ToDecimal(reader["price"]);
                    selectKategori.SelectedValue = reader["id_category"];
                    
                }
            }
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = koneksi.GetConnection())
            {
                string query = @"UPDATE products
                                 SET name_product = @nama,
                                     price = @harga,
                                     id_category = @cat
                                 WHERE sku = @sku";

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@nama", txtNamaBarang.Text);
                cmd.Parameters.AddWithValue("@harga", numHarga.Value);
                cmd.Parameters.AddWithValue("@cat", selectKategori.SelectedValue);
                cmd.Parameters.AddWithValue("@sku", txtSKU.Text);

                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data Berhasil Di Update");
                    this.Close();
                }
                catch (Exception ex) 
                { 
                    MessageBox.Show(ex.Message);
                }

            }
        }

        private void btnBatal_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
