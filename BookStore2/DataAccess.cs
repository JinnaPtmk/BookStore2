using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore2
{
    class DataAccess
    {
        public async static void InitializeDatabase()
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
                    "Price INTEGER NOT NULL)";
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
                    "Phone INTEGER NULL,"+
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
                    "(ISBN INTEGER PRIMARY KEY, " +
                    "Customer_Id NVARCHAR(255) NOT NULL, " +
                    "Quantity INTEGER NOT NULL, " +
                    "Total_Price INTEGER NOT NULL)";
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
            //Initial User ID
            using (SqliteConnection db =
               new SqliteConnection("Filename=bookStoreProject1.db"))
            {
                db.Open();
                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;
                insertCommand.CommandText = "INSERT OR IGNORE INTO Users (User_Id,User_Password) VALUES ('admin','1234');";
                insertCommand.ExecuteReader();
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
        //Login Check
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
        //Add Book
        public static void AddBook(string bookIsbn, string bookTitle,string bookDescription, string bookPrice)
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
        public static void EditBook(string bookTitle, string bookDescription, string bookPrice,string displayChoice, string selectedItem)
        {
            using (SqliteConnection db =
                new SqliteConnection("Filename=bookStoreProject1.db"))
            {
                db.Open();
                SqliteCommand updateCommand = new SqliteCommand();
                updateCommand.Connection = db;
                updateCommand.CommandText = "UPDATE Books SET Title = @Title, Description = @Description, Price = @Price WHERE "+displayChoice+" = @SelectedItem";
                updateCommand.Parameters.AddWithValue("@Title", bookTitle);
                updateCommand.Parameters.AddWithValue("@Description", bookDescription);
                updateCommand.Parameters.AddWithValue("@Price", bookPrice);
                updateCommand.Parameters.AddWithValue("@SelectedItem", selectedItem);
                updateCommand.ExecuteReader();
                db.Close();
            }
        }
        //Delete Book
        public static void DeleteBook(string selectedItem,string displayChoice)
        {
            using (SqliteConnection db =
                new SqliteConnection("Filename=bookStoreProject1.db"))
            {
                db.Open();
                SqliteCommand deleteCommand = new SqliteCommand();
                deleteCommand.Connection = db;
                deleteCommand.CommandText = "DELETE FROM Books WHERE "+displayChoice+" = @SelectedItem";
                deleteCommand.Parameters.AddWithValue("@SelectedItem", selectedItem);
                deleteCommand.ExecuteReader();
                db.Close();
            }
        }
        //Add Customer
        public static void AddCustomer(string customerId, string customerName, string customerAddress, string customerPhone,string customerEmail)
        {
            using (SqliteConnection db =
                new SqliteConnection("Filename=bookStoreProject1.db"))
            {
                db.Open();
                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;
                insertCommand.CommandText = "INSERT INTO Customers (Customer_Id,Customer_Name,Address,Phone,Email)"+
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
        //Delete Customer
        public static void DeleteCustomer(string selectedItem, string displayChoice)
        {
            using (SqliteConnection db =
                new SqliteConnection("Filename=bookStoreProject1.db"))
            {
                db.Open();
                SqliteCommand deleteCommand = new SqliteCommand();
                deleteCommand.Connection = db;
                deleteCommand.CommandText = "DELETE FROM Customers WHERE " + displayChoice + " = @SelectedItem";
                deleteCommand.Parameters.AddWithValue("@SelectedItem", selectedItem);
                deleteCommand.ExecuteReader();
                db.Close();
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
