using System;
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
    public partial class StockIN : Form
    {
        private readonly string _sku;

        public StockIN() : this(string.Empty)
        {
        }

        public StockIN(string sku)
        {
            InitializeComponent();
            _sku = sku;

            button1.Click += button1_Click;
            button2.Click += button2_Click;
        }

        private void StockIN_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_sku))
            {
                MessageBox.Show("Produk tidak ditemukan.");
                Close();
                return;
            }

            using (SqlConnection connection = koneksi.GetConnection())
            {
                string query = @"
                    SELECT
                        p.name_product,
                        p.stock,
                        ISNULL(st.notes, '') AS notes
                    FROM dbo.products p
                    OUTER APPLY (
                        SELECT TOP 1 notes
                        FROM dbo.stock_transactions st
                        WHERE st.id_product = p.id_product
                        ORDER BY st.transaction_date DESC
                    ) st
                    WHERE p.sku = @sku";

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
                        textBox3.Text = reader["notes"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("Data produk tidak ditemukan.");
                        Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memuat data stock: " + ex.Message);
                    Close();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int jumlahTambah = (int)numericUpDown1.Value;
            string notes = textBox3.Text.Trim();

            if (jumlahTambah <= 0)
            {
                MessageBox.Show("Jumlah stock in harus lebih dari 0.");
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
                        SET stock = stock + @qty
                        OUTPUT INSERTED.stock
                        WHERE sku = @sku";

                    using (SqlCommand updateCmd = new SqlCommand(updateQuery, connection, transaction))
                    {
                        updateCmd.Parameters.AddWithValue("@qty", jumlahTambah);
                        updateCmd.Parameters.AddWithValue("@sku", _sku);

                        using (SqlDataReader reader = updateCmd.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                throw new Exception("Produk tidak ditemukan.");
                            }

                            stockBaru = Convert.ToInt32(reader["stock"]);
                        }
                    }

                    transaction.Commit();
                    textBox1.Text = stockBaru.ToString();
                    MessageBox.Show("Stock berhasil ditambahkan.");
                    Close();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Gagal melakukan stock in: " + ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
