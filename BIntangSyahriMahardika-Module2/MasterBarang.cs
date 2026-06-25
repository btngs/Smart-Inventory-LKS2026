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
    public partial class MasterBarang : Form
    {
        public MasterBarang()
        {
            InitializeComponent();
        }
        void muatData(string kondisi = "", SqlParameter parameter = null)
        {
            try
            {
                using (SqlConnection connection = koneksi.GetConnection())
                {
                    string query = @"
                        SELECT 
                            p.id_product,
                            p.sku AS [SKU], 
                            p.name_product AS [Nama Produk], 
                            c.name_category AS [Kategori], 
                            p.price AS [Harga], 
                            p.stock AS [Stok],
                            CASE 
                                WHEN p.stock = 0 THEN 'Out of Stock'
                                WHEN p.stock <= p.min_threshold THEN 'Low Stock'
                                ELSE 'In Stock'
                            END AS [Status]
                        FROM products p
                        LEFT JOIN dbo.categories c ON p.id_category = c.id_category";

                    if (!string.IsNullOrEmpty(kondisi))
                    {
                        query += " WHERE " + kondisi;
                    }

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    if (parameter != null)
                    {
                        adapter.SelectCommand.Parameters.Add(parameter);
                    }

                    DataTable da = new DataTable();
                    adapter.Fill(da);
                    dataGridView1.DataSource = da;
                    if (dataGridView1.Columns["Aksi"] != null)
                    {
                        dataGridView1.Columns["Aksi"].DisplayIndex = dataGridView1.Columns.Count - 1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data: " + ex.Message);
            }
        }

        void tampilbarang()
        {
            muatData();
        }

        private int colAksiIndex;

        void tambahKolomAksi()
        {
            if (dataGridView1.Columns["Aksi"] != null) return;

            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            btn.Name = "Aksi";
            btn.HeaderText = "Actions";
            btn.Text = "⋮";
            btn.UseColumnTextForButtonValue = true;
            btn.Width = 50;

            colAksiIndex = dataGridView1.Columns.Add(btn);
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

                comboBox1.DataSource = dt;
                comboBox1.DisplayMember = "name_category";
                comboBox1.ValueMember = "id_category";
            }
        }

        private void MasterBarang_Load(object sender, EventArgs e)
        {
            tampilkategori();
            tampilbarang();
            tambahKolomAksi();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                comboBox1_SelectedIndexChanged(null, null);
                return;
            }

            string kondisi = "(p.name_product LIKE @search OR p.sku LIKE @search)";
            SqlParameter param = new SqlParameter("@search", "%" + textBox1.Text + "%");
            muatData(kondisi, param);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1 || comboBox1.SelectedValue == null || comboBox1.SelectedValue is DataRowView)
                return;

            if (comboBox1.SelectedIndex == 0)
            {
                tampilbarang();
            }
            else
            {
                string kondisi = "p.id_category = @id_category";
                SqlParameter param = new SqlParameter("@id_category", comboBox1.SelectedValue);
                muatData(kondisi, param);
            }
        }
        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void pictureBox2_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }

        private void button2_Click(object sender, EventArgs e)
        {
            AddItem item = new AddItem();
            item.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            LoginForm login = new LoginForm();
            login.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridView1.IsCurrentCellDirty)
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Aksi" && e.RowIndex >= 0)
            {
                object aksiValue = dataGridView1.Rows[e.RowIndex].Cells["Aksi"].Value;
                string aksi = aksiValue == null ? string.Empty : aksiValue.ToString();
                string sku = dataGridView1.Rows[e.RowIndex].Cells["SKU"].Value.ToString();

                if (aksi == "Edit Product")
                {
                    EditItem edit = new EditItem(sku);
                    edit.ShowDialog();
                    tampilbarang();
                }
                else if (aksi == "New Stock In")
                {
                    StockIN stockIn = new StockIN(sku);
                    stockIn.ShowDialog();
                    tampilbarang();
                }
                else if (aksi == "Stock Out")
                {
                    StockOUT SO = new StockOUT(sku);
                    SO.ShowDialog(); 
                    tampilbarang();
                }

                dataGridView1.Rows[e.RowIndex].Cells["Aksi"].Value = null;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        { 
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            this.Close();
        }
    }
}
