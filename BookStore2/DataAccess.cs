﻿using Microsoft.Data.Sqlite;
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
                    "(Customer_Id INTEGER PRIMARY KEY, " +
                    "Customer_Name NVARCHAR(255) NOT NULL, " +
                    "Address NVARCHAR(2048) NULL, " +
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

        public static bool LoggingIn(string CheckId, string CheckPw)
        {
            using (SqliteConnection db = new SqliteConnection("Filename=bookStoreProject1.db"))
            {
                bool checker;
                db.Open();
                SqliteCommand selectCommand = new SqliteCommand
                ("SELECT * FROM Users WHERE User_Id = '" + CheckId + "' AND User_Password = '" + CheckPw + "'", db);
                SqliteDataReader query = selectCommand.ExecuteReader();
                checker = query.Read();
                db.Close();
                return checker;


            }

        }
    }
}
