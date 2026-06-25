# BIntangSyahriMahardika-Module2

Aplikasi desktop **WinForms** untuk manajemen inventory / stok barang berbasis **C# .NET Framework 4.7.2** dan **SQL Server LocalDB**.

## Fitur Utama

- Login user
- Dashboard inventory
- Master barang
- Tambah, edit, dan hapus item
- Stock in
- Stock out

## Struktur Project

- `BIntangSyahriMahardika-Module2/`
  - source code aplikasi
  - file database SQL: `inventory-sql-server.sql`

## Database

File database sudah disediakan di:

`BIntangSyahriMahardika-Module2/inventory-sql-server.sql`

Database ini berisi:

- `users`
- `categories`
- `products`
- `stock_transactions`

Connection string aplikasi mengarah ke:

- `Data Source=(localdb)\MSSQLLocalDB`
- `Initial Catalog=Inventory_db`

## Database Initiator

Saat aplikasi pertama kali dijalankan, `DatabaseInitializer` akan:

1. Membuat database `Inventory_db` jika belum ada.
2. Membuat tabel dasar jika belum ada.
3. Mengisi data awal untuk user, kategori, dan produk.

## Cara Menjalankan Database

1. Buka project di Visual Studio.
2. Jalankan aplikasi.
3. Database akan dibuat otomatis saat startup jika belum ada.
4. Jika ingin manual, Anda tetap bisa menjalankan `BIntangSyahriMahardika-Module2/inventory-sql-server.sql`.

## Cara Menjalankan Aplikasi

1. Buka solusi `BIntangSyahriMahardika-Module2.slnx` di Visual Studio.
2. Restore dan build project.
3. Jalankan aplikasi dari Visual Studio.
4. Login menggunakan data awal dari script SQL:
   - `admin1` / `admin123`
   - `staff1` / `staff123`

## Catatan

- Project ini menggunakan Windows Forms dan membutuhkan lingkungan Windows.
- File `inventory-sql-server.sql` sudah berisi data awal untuk user, kategori, produk, dan transaksi stok.
