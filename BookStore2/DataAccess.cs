using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;

namespace BookStore2
{
    class DataAccess
    {
        public  async static void  InitializeDatabase()
        {
            //Books TABLE
            using (SqliteConnection db =
               new SqliteConnection("Filename=bookStoreProject1.db"))
            {
                db.Open();
                String tableCommand = "CREATE TABLE IF NOT EXISTS Books " +
                    "(ISBN INTEGER PRIMARY KEY, " +
                    "Title NVARCHAR(255) NOT NULL, " +
                    "Description NVARCHAR(2048) NULL, " +
                    "Price DOUBLE NOT NULL)";
                SqliteCommand createTable = new SqliteCommand(tableCommand, db);
                createTable.ExecuteReader();
            }
            //Customers TABLE
            using (SqliteConnection db =
               new SqliteConnection("Filename=bookStoreProject1.db"))
            {
                db.Open();
                String tableCommand = "CREATE TABLE IF NOT EXISTS Customers " +
                    "(Customer_Id INTEGER(10) PRIMARY KEY, " +
                    "Customer_Name NVARCHAR(255) NOT NULL, " +
                    "Address NVARCHAR(2048) NULL, " +
                    "Phone NVARCHAR(10) NULL,"+
                    "Email NVARCHAR(255) NOT NULL)";
                SqliteCommand createTable = new SqliteCommand(tableCommand, db);
                createTable.ExecuteReader();
            }

            //Transactions TABLE
            using (SqliteConnection db =
               new SqliteConnection("Filename=bookStoreProject1.db"))
            {
                db.Open();
                String tableCommand = "CREATE TABLE IF NOT EXISTS Transactions " +
                    "(ISBN INTEGER NOT NULL, " +
                    "Customer_Id INTEGER(10) NOT NULL, " +
                    "Quantity INTEGER NOT NULL, " +
                    "Total_Price DOUBLE NOT NULL)";
                SqliteCommand createTable = new SqliteCommand(tableCommand, db);
                createTable.ExecuteReader();
            }
            //Users TABLE
            using (SqliteConnection db =
                new SqliteConnection("Filename=bookStoreProject1.db"))
            {
                db.Open();
                String tableCommand = "CREATE TABLE IF NOT EXISTS Users " +
                    "(User_Id NVARCHAR(25) PRIMARY KEY, " +
                    "User_Password NVARCHAR(8) NOT NULL);";
                SqliteCommand createTable = new SqliteCommand(tableCommand, db);
                createTable.ExecuteReader();
            }
        }        
        //Add Book
        public static void AddBook(string bookIsbn, string bookTitle, string bookDescription, string bookPrice)
        {
            using (SqliteConnection db =
                new SqliteConnection("Filename=bookStoreProject1.db"))
            {
                db.Open();
                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;
                insertCommand.CommandText = "INSERT INTO Books (ISBN,Title,Description,Price) VALUES (@ISBN,@Title,@Description,@Price);";
                insertCommand.Parameters.AddWithValue("@ISBN", bookIsbn);
                insertCommand.Parameters.AddWithValue("@Title", bookTitle);
                insertCommand.Parameters.AddWithValue("@Description", bookDescription);
                insertCommand.Parameters.AddWithValue("@Price", bookPrice);
                insertCommand.ExecuteReader();
                db.Close();
            }
        }
        //Edit Book
        public static void EditBook(string bookTitle, string bookDescription, string bookPrice, string selectedItem)
        {
            using (SqliteConnection db =
                new SqliteConnection("Filename=bookStoreProject1.db"))
            {
                db.Open();
                SqliteCommand updateCommand = new SqliteCommand();
                updateCommand.Connection = db;
                updateCommand.CommandText = "UPDATE Books SET Title = @Title, Description = @Description, Price = @Price WHERE ISBN = @SelectedItem";
                updateCommand.Parameters.AddWithValue("@Title", bookTitle);
                updateCommand.Parameters.AddWithValue("@Description", bookDescription);
                updateCommand.Parameters.AddWithValue("@Price", bookPrice);
                updateCommand.Parameters.AddWithValue("@SelectedItem", selectedItem);
                updateCommand.ExecuteReader();
                db.Close();
            }
        }
        //Delete Book
        public static void DeleteBook(string selectedItem)
        {
            using (SqliteConnection db =
                new SqliteConnection("Filename=bookStoreProject1.db"))
            {
                db.Open();
                SqliteCommand deleteCommand = new SqliteCommand();
                deleteCommand.Connection = db;
                deleteCommand.CommandText = "DELETE FROM Books WHERE ISBN = @SelectedItem";
                deleteCommand.Parameters.AddWithValue("@SelectedItem", selectedItem);
                deleteCommand.ExecuteReader();
                db.Close();
            }
        }
        //Add Customer
        public static void AddCustomer(string customerId, string customerName, string customerAddress, string customerPhone, string customerEmail)
        {
            using (SqliteConnection db =
                new SqliteConnection("Filename=bookStoreProject1.db"))
            {
                db.Open();
                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;
                insertCommand.CommandText = "INSERT INTO Customers (Customer_Id,Customer_Name,Address,Phone,Email)" +
                    "VALUES (@CustomerId,@CustomerName,@Address,@Phone,@Email);";
                insertCommand.Parameters.AddWithValue("@CustomerId", customerId);
                insertCommand.Parameters.AddWithValue("@CustomerName", customerName);
                insertCommand.Parameters.AddWithValue("@Address", customerAddress);
                insertCommand.Parameters.AddWithValue("@Phone", customerPhone);
                insertCommand.Parameters.AddWithValue("@Email", customerEmail);
                insertCommand.ExecuteReader();
                db.Close();
            }
        }
        //Edit Customer
        public static void EditCustomer(string customerName, string address, string phone, string email, string selectedItem)
        {
            using (SqliteConnection db =
                new SqliteConnection("Filename=bookStoreProject1.db"))
            {
                db.Open();
                SqliteCommand updateCommand = new SqliteCommand();
                updateCommand.Connection = db;
                updateCommand.CommandText = "UPDATE Customers SET Customer_Name = @CustomerName, Address = @Address," +
                    "Phone = @Phone, Email = @Email WHERE Customer_Id = @SelectedItem";
                updateCommand.Parameters.AddWithValue("@CustomerName", customerName);
                updateCommand.Parameters.AddWithValue("@Address", address);
                updateCommand.Parameters.AddWithValue("@Phone", phone);
                updateCommand.Parameters.AddWithValue("@Email", email);
                updateCommand.Parameters.AddWithValue("@SelectedItem", selectedItem);
                updateCommand.ExecuteReader();
                db.Close();
            }
        }
        //Delete Customer
        public static void DeleteCustomer(string selectedItem)
        {
            using (SqliteConnection db =
                new SqliteConnection("Filename=bookStoreProject1.db"))
            {
                db.Open();
                SqliteCommand deleteCommand = new SqliteCommand();
                deleteCommand.Connection = db;
                deleteCommand.CommandText = "DELETE FROM Customers WHERE Customer_Id = @SelectedItem";
                deleteCommand.Parameters.AddWithValue("@SelectedItem", selectedItem);
                deleteCommand.ExecuteReader();
                db.Close();
            }
        }
        //Add User
        public static void AddUser(string inputID, string InputPw)
        {
            using (SqliteConnection db =
                new SqliteConnection("Filename=bookStoreProject1.db"))
            {
                db.Open();
                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;
                insertCommand.CommandText = "INSERT INTO Users (User_Id,User_Password) VALUES (@UserId,@UserPw);";
                insertCommand.Parameters.AddWithValue("@UserId", inputID);
                insertCommand.Parameters.AddWithValue("@UserPw", InputPw);
                insertCommand.ExecuteReader();
                db.Close();
            }
        }
        //Add Transaction
        public static void AddTransaction(string ISBN, string CustomerId, string Quantity, string TotalPrice)
        {
            using (SqliteConnection db =
                new SqliteConnection("Filename=bookStoreProject1.db"))
            {
                db.Open();
                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;
                insertCommand.CommandText = "INSERT INTO Transactions (ISBN,Customer_Id,Quantity,Total_Price) VALUES (@ISBN,@CustomerId,@Quantity,@TotalPrice)";
                insertCommand.Parameters.AddWithValue("@ISBN", ISBN);
                insertCommand.Parameters.AddWithValue("@CustomerId", CustomerId);
                insertCommand.Parameters.AddWithValue("@Quantity", Quantity);
                insertCommand.Parameters.AddWithValue("@TotalPrice", TotalPrice);
                insertCommand.ExecuteReader();
                db.Close();
            }
        }
        public static string GetBookName(string _isbn)
        {
            using (SqliteConnection db =
                 new SqliteConnection($"Filename=bookStoreProject1.db"))
            {
                string bookName="";
                db.Open();
                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT Title FROM Books WHERE ISBN = '"+_isbn+"'", db);
                SqliteDataReader query = selectCommand.ExecuteReader();
                while (query.Read())
                {
                    bookName = query.GetString(0);
                }
                db.Close();
                return bookName;

            }
        }
       
        public static string GetCustomerName(string _csId)
        {
            using (SqliteConnection db =
                 new SqliteConnection($"Filename=bookStoreProject1.db"))
            {
                string csName = "";
                db.Open();
                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT Customer_Name FROM Customers WHERE Customer_Id = " + _csId, db);
                SqliteDataReader query = selectCommand.ExecuteReader();
                while (query.Read())
                {
                    csName = query.GetString(0);
                }
                db.Close();
                return csName;
            }
        }

        public static List<string> GetTransactions()
        {
            List<String> entries = new List<string>();
            using (SqliteConnection db =
                  new SqliteConnection($"Filename=bookStoreProject1.db"))
            {
                db.Open();
                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT * from Transactions" , db);
                SqliteDataReader query = selectCommand.ExecuteReader();
                while (query.Read())
                {
                    string _isbn = query.GetString(0);
                    string _csId = query.GetString(1);
                    string _quantity = query.GetString(2);
                    string _ttlPrice = query.GetString(3);
                    entries.Add("ลูกค้า "+GetCustomerName(_csId) + " สินค้า "+GetBookName(_isbn) + " จำนวน " +_quantity+" เล่ม "+_ttlPrice+" บาท");
                }
                db.Close();
                return entries;
            }
        }
        //Login check
        public static bool LoggingIn(string CheckId, string CheckPw)
        {
            using (SqliteConnection db = new SqliteConnection("Filename=bookStoreProject1.db"))
            {
                bool checker;
                db.Open();
                SqliteCommand selectCommand = new SqliteCommand
                ("SELECT User_Id,User_Password FROM Users WHERE User_Id = @UserId AND User_Password = @UserPw", db);
                selectCommand.Parameters.AddWithValue("@UserId", CheckId);
                selectCommand.Parameters.AddWithValue("@UserPw", CheckPw);
                SqliteDataReader query = selectCommand.ExecuteReader();
                checker = query.Read();
                db.Close();
                return checker;
            }
        }
        //ISBN check
        public static bool UniqueIsbnCheck(string CheckIsbn)
        {
            using (SqliteConnection db = new SqliteConnection("Filename=bookStoreProject1.db"))
            {
                bool checker;
                db.Open();
                SqliteCommand selectCommand = new SqliteCommand
                ("SELECT ISBN FROM Books WHERE ISBN = @Isbn" , db);
                selectCommand.Parameters.AddWithValue("@Isbn", CheckIsbn);
                SqliteDataReader query = selectCommand.ExecuteReader();
                checker = query.Read();
                db.Close();
                return checker;
            }
        }
        //Customer check
        public static bool UniqueCustomerCheck(string CheckCustomerId)
        {
            using (SqliteConnection db = new SqliteConnection("Filename=bookStoreProject1.db"))
            {
                bool checker;
                db.Open();
                SqliteCommand selectCommand = new SqliteCommand
                ("SELECT Customer_Id FROM Customers WHERE Customer_Id = @CustomerId", db);
                selectCommand.Parameters.AddWithValue("@CustomerId", CheckCustomerId);
                SqliteDataReader query = selectCommand.ExecuteReader();
                checker = query.Read();
                db.Close();
                return checker;
            }
        }
        //User check
        public static bool UniqueUserCheck(string CheckUserId)
        {
            using (SqliteConnection db = new SqliteConnection("Filename=bookStoreProject1.db"))
            {
                bool checker;
                db.Open();
                SqliteCommand selectCommand = new SqliteCommand
                ("SELECT User_Id FROM Users WHERE User_Id = @UserId", db);
                selectCommand.Parameters.AddWithValue("@UserId", CheckUserId);
                SqliteDataReader query = selectCommand.ExecuteReader();
                checker = query.Read();
                db.Close();
                return checker;
            }
        }
        //Customer Count
        public static int CustomerCount()
        {
            {
                int ccount=0;
                using (SqliteConnection db = new SqliteConnection("Filename=bookStoreProject1.db"))
                {
                    db.Open();
                    SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT COUNT(*) FROM Customers;", db);
                    SqliteDataReader query = selectCommand.ExecuteReader();
                    while (query.Read())
                    {
                        ccount += query.GetInt32(0);
                    }
                    db.Close();
                    return ccount;
                 }
            }

        }
    }
}
