using System;
using System.Data;
using System.Data.SqlClient;

namespace BIntangSyahriMahardika_Module2
{
    internal static class DatabaseInitializer
    {
        private const string DatabaseName = "Inventory_db";

        public static void Initialize()
        {
            EnsureDatabaseExists();
            EnsureSchemaExists();
            SeedBaseData();
        }

        private static void EnsureDatabaseExists()
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;"))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = @"
IF DB_ID(@dbName) IS NULL
BEGIN
    DECLARE @sql NVARCHAR(MAX) = N'CREATE DATABASE ' + QUOTENAME(@dbName);
    EXEC(@sql);
END";
                command.Parameters.AddWithValue("@dbName", DatabaseName);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        private static void EnsureSchemaExists()
        {
            using (SqlConnection connection = koneksi.GetConnection())
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = @"
IF OBJECT_ID('dbo.users', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.users (
        id_user INT IDENTITY(1,1) PRIMARY KEY,
        username VARCHAR(50) NOT NULL UNIQUE,
        password VARCHAR(255) NOT NULL,
        full_name VARCHAR(100) NULL,
        role VARCHAR(20) NOT NULL CHECK (role IN ('Admin', 'Staff'))
    );
END;

IF OBJECT_ID('dbo.categories', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.categories (
        id_category INT IDENTITY(1,1) PRIMARY KEY,
        name_category VARCHAR(100) NOT NULL
    );
END;

IF OBJECT_ID('dbo.products', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.products (
        id_product INT IDENTITY(1,1) PRIMARY KEY,
        sku VARCHAR(20) NOT NULL UNIQUE,
        name_product VARCHAR(150) NOT NULL,
        image VARCHAR(255) NULL,
        description VARCHAR(255) NULL,
        id_category INT NULL,
        price DECIMAL(15, 2) NOT NULL,
        stock INT NOT NULL DEFAULT 0,
        min_threshold INT NOT NULL DEFAULT 10,
        CONSTRAINT FK_Product_Category FOREIGN KEY (id_category) REFERENCES dbo.categories(id_category)
    );
END;

IF OBJECT_ID('dbo.stock_transactions', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.stock_transactions (
        id_transaction INT IDENTITY(1,1) PRIMARY KEY,
        id_product INT NULL,
        id_user INT NULL,
        type VARCHAR(10) NOT NULL CHECK (type IN ('IN', 'OUT')),
        qty INT NOT NULL,
        reference_no VARCHAR(50) NULL,
        notes NVARCHAR(MAX) NULL,
        transaction_date DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
        CONSTRAINT FK_Trans_Product FOREIGN KEY (id_product) REFERENCES dbo.products(id_product),
        CONSTRAINT FK_Trans_User FOREIGN KEY (id_user) REFERENCES dbo.users(id_user)
    );
END;";

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        private static void SeedBaseData()
        {
            using (SqlConnection connection = koneksi.GetConnection())
            {
                connection.Open();

                if (TableIsEmpty(connection, "dbo.users"))
                {
                    ExecuteNonQuery(connection, @"
INSERT INTO dbo.users (username, password, full_name, role) VALUES
('admin1', '0192023a7bbd73250516f069df18b500', 'Admin User', 'Admin'),
('staff1', 'de9bf5643eabf80f4a56fda3bbb84483', 'Staff User', 'Staff');");
                }

                if (TableIsEmpty(connection, "dbo.categories"))
                {
                    ExecuteNonQuery(connection, @"
INSERT INTO dbo.categories (name_category) VALUES
('Electronics'),
('Packaging'),
('Warehouse Tools'),
('Office Supplies');");
                }

                if (TableIsEmpty(connection, "dbo.products"))
                {
                    ExecuteNonQuery(connection, @"
INSERT INTO dbo.products (sku, name_product, description, image, id_category, price, stock, min_threshold) VALUES
('SKU-1001', 'Industrial Tablet X5', 'Ruggedized Series', 'tablet-x5.png', 1, 12500000, 42, 10),
('SKU-1002', 'Bubble Wrap Roll 100m', 'Standard Shipping', 'bubble-wrap.png', 2, 450000, 12, 15),
('SKU-1003', 'Digital Caliper Pro', 'Precision Instruments', 'caliper.png', 3, 2100000, 156, 20),
('SKU-1004', 'Thermal Label Printer', 'Logistics Series', 'printer.png', 1, 3750000, 0, 10),
('SKU-1005', 'Pallet Jack Heavy', 'Load Capacity 2.5T', 'pallet-jack.png', 3, 4200000, 8, 5),
('SKU-1006', 'Cardboard Box A4', 'Bulk Pack (50pcs)', 'box-a4.png', 2, 125000, 540, 50);");
                }
            }
        }

        private static bool TableIsEmpty(SqlConnection connection, string tableName)
        {
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = $"SELECT CASE WHEN EXISTS (SELECT 1 FROM {tableName}) THEN 0 ELSE 1 END";
                return Convert.ToInt32(command.ExecuteScalar()) == 1;
            }
        }

        private static void ExecuteNonQuery(SqlConnection connection, string sql)
        {
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = sql;
                command.ExecuteNonQuery();
            }
        }
    }
}
