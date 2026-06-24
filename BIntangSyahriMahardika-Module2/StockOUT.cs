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
    public partial class StockOUT : Form
    {
        private string _sku;

        public StockOUT(string sku)
        {
            InitializeComponent();
            _sku = sku;
            numericUpDown1.Minimum = 1;
        }

        private void StockOUT_Load(object sender, EventArgs e)
        {
            using (SqlConnection connection = koneksi.GetConnection())
            {
                string query = @"SELECT
                                    p.name_product,
                                    p.stock
                                FROM products p
                                WHERE sku = @sku";

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@sku", _sku);

                try
                {
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        txtBarang.Text = reader["name_product"].ToString();
                        textBox1.Text = reader["stock"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("Data Tidak Ditemukan");
                    }
                }
                catch (Exception ex) 
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int jumlahKeluar = (int)numericUpDown1.Value;

            if (jumlahKeluar <= 0)
            {
                MessageBox.Show("Jumlah stock out harus lebih dari 0.");
                return;
            }

            using (SqlConnection connection = koneksi.GetConnection())
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    int stockBaru;

                    string updateQuery = @"
                        UPDATE dbo.products
                        SET stock = stock - @qty
                        OUTPUT INSERTED.stock
                        WHERE sku = @sku
                          AND stock >= @qty";

                    using (SqlCommand updateCmd = new SqlCommand(updateQuery, connection, transaction))
                    {
                        updateCmd.Parameters.AddWithValue("@qty", jumlahKeluar);
                        updateCmd.Parameters.AddWithValue("@sku", _sku);

                        using (SqlDataReader reader = updateCmd.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                throw new Exception("Stock tidak mencukupi atau produk tidak ditemukan.");
                            }

                            stockBaru = Convert.ToInt32(reader["stock"]);
                        }
                    }

                    transaction.Commit();
                    textBox1.Text = stockBaru.ToString();
                    MessageBox.Show("Stock berhasil dikurangi.");
                    Close();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Gagal melakukan stock out: " + ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
