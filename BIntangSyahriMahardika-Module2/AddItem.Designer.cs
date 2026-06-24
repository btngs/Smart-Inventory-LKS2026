using System;

namespace BIntangSyahriMahardika_Module2
{
    partial class AddItem
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtNama = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.selectKategori = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSimpan = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.numericHarga = new System.Windows.Forms.NumericUpDown();
            this.numericStok = new System.Windows.Forms.NumericUpDown();
            this.txtSKU = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericHarga)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericStok)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(26, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(178, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tambah Barang";
            // 
            // txtNama
            // 
            this.txtNama.Location = new System.Drawing.Point(31, 154);
            this.txtNama.Name = "txtNama";
            this.txtNama.Size = new System.Drawing.Size(268, 20);
            this.txtNama.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Kode Barang";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 138);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Nama Barang";
            // 
            // selectKategori
            // 
            this.selectKategori.FormattingEnabled = true;
            this.selectKategori.Location = new System.Drawing.Point(31, 200);
            this.selectKategori.Name = "selectKategori";
            this.selectKategori.Size = new System.Drawing.Size(268, 21);
            this.selectKategori.TabIndex = 5;
            this.selectKategori.SelectedIndexChanged += new System.EventHandler(this.selectKategori_SelectedIndexChanged_1);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 184);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Kategori";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(28, 231);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Harga (IDR)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(207, 231);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Stok";
            // 
            // btnSimpan
            // 
            this.btnSimpan.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnSimpan.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnSimpan.Location = new System.Drawing.Point(210, 306);
            this.btnSimpan.Name = "btnSimpan";
            this.btnSimpan.Size = new System.Drawing.Size(89, 23);
            this.btnSimpan.TabIndex = 11;
            this.btnSimpan.Text = "Simpan";
            this.btnSimpan.UseVisualStyleBackColor = false;
            this.btnSimpan.Click += new System.EventHandler(this.btnSimpan_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(115, 306);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(89, 23);
            this.button2.TabIndex = 12;
            this.button2.Text = "Batal";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(28, 50);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(195, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Tambahkan barang ke sistem inventaris";
            // 
            // numericHarga
            // 
            this.numericHarga.Location = new System.Drawing.Point(31, 248);
            this.numericHarga.Maximum = new decimal(new int[] {
            1874919423,
            2328306,
            0,
            0});
            this.numericHarga.Name = "numericHarga";
            this.numericHarga.Size = new System.Drawing.Size(173, 20);
            this.numericHarga.TabIndex = 14;
            // 
            // numericStok
            // 
            this.numericStok.Location = new System.Drawing.Point(210, 247);
            this.numericStok.Name = "numericStok";
            this.numericStok.Size = new System.Drawing.Size(89, 20);
            this.numericStok.TabIndex = 15;
            // 
            // txtSKU
            // 
            this.txtSKU.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.txtSKU.Location = new System.Drawing.Point(31, 110);
            this.txtSKU.Name = "txtSKU";
            this.txtSKU.ReadOnly = true;
            this.txtSKU.Size = new System.Drawing.Size(268, 20);
            this.txtSKU.TabIndex = 1;
            // 
            // AddItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(337, 357);
            this.Controls.Add(this.numericStok);
            this.Controls.Add(this.numericHarga);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnSimpan);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.selectKategori);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtNama);
            this.Controls.Add(this.txtSKU);
            this.Controls.Add(this.label1);
            this.Name = "AddItem";
            this.Text = "AddItem";
            this.Load += new System.EventHandler(this.AddItem_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericHarga)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericStok)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void selectKategori_SelectedIndexChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }


        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNama;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox selectKategori;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnSimpan;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numericHarga;
        private System.Windows.Forms.NumericUpDown numericStok;
        private System.Windows.Forms.TextBox txtSKU;
    }
}