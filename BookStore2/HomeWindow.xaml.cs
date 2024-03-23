using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MessageBox = System.Windows.MessageBox;

namespace BookStore2
{
    /// <summary>
    /// Interaction logic for HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        public HomeWindow()
        {
            InitializeComponent();
            fillCustomersList();
            fillBooksList();
        }
        string searchCondition = "";
        string ouput = "{0,-10}\t{1,-10}";
        public void fillCustomersList()
        {
            CustomerLst.Items.Clear();
            using (SqliteConnection db =
               new SqliteConnection($"Filename=bookStoreProject1.db"))
            {
                db.Open();
                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT Customer_Id,Customer_Name FROM Customers " + searchCondition, db);
                SqliteDataReader query = selectCommand.ExecuteReader();
                while (query.Read())
                {
                    string CustomerId = query.GetString(0);
                    string CustomerName = query.GetString(1);
                    CustomerLst.Items.Add(string.Format(ouput,CustomerId,CustomerName));
                }
                db.Close();
            }
        }
        public void fillBooksList()
        {
            BookLst.Items.Clear();
            using (SqliteConnection db =
               new SqliteConnection($"Filename=bookStoreProject1.db"))
            {
                db.Open();
                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT ISBN,Title from Books " + searchCondition, db);
                SqliteDataReader query = selectCommand.ExecuteReader();
                while (query.Read())
                {
                    string BookId = query.GetString(0);
                    string BookName = query.GetString(1);
                    BookLst.Items.Add(string.Format(ouput,BookId,BookName));
                }
                db.Close();
            }
        }
        private void ManageBooks_Btn_Click(object sender, RoutedEventArgs e)
        {
            BooksWindow booksWindow = new BooksWindow();
            booksWindow.Show();
        }

        private void ManageCustomer_Btn_Click(object sender, RoutedEventArgs e)
        {
            CustomersWindow customerWindow = new CustomersWindow();
            customerWindow.Show();
        }

        private void searchCustomer_Btn_Click(object sender, RoutedEventArgs e)
        {
            searchCondition = "WHERE Customer_Name LIKE '%"+searchCustomers_Txt.Text+"%' OR Customer_Id LIKE '%" + searchCustomers_Txt.Text + "%'";
            fillCustomersList();
        }

        private void searchBooks_Btn_Click(object sender, RoutedEventArgs e)
        {
            searchCondition = "WHERE Title LIKE '%" + searchBooks_Txt.Text + "%' OR ISBN LIKE '%" + searchBooks_Txt.Text + "%'";
            fillBooksList();
        }

        private void CustomerLst_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CustomerLst.SelectedItems.Count > 0)
            {
                using (SqliteConnection db =
                   new SqliteConnection($"Filename=bookStoreProject1.db"))
                {
                    db.Open();
                    SqliteCommand selectCommand = new SqliteCommand
                        (string.Concat("SELECT * from Customers WHERE Customer_Id = '", CustomerLst.SelectedItem.ToString().AsSpan(0, 10), "'"), db);
                    SqliteDataReader query = selectCommand.ExecuteReader();
                    while (query.Read())
                    {
                        string sCustomerId = query.GetInt64(0).ToString();
                        string sCustomerName = query.GetString(1);
                        string sAddress = query.GetString(2);
                        string sPhone = query.GetString(3);
                        string sEmail = query.GetString(4);

                        customerId_Lbl.Content = sCustomerId;
                        customerName_Lbl.Content = sCustomerName;
                        customerAddress_Lbl.Content = sAddress;
                        customerPhone_Lbl.Content = sPhone;
                        customerEmail_Lbl.Content = sEmail;


                    }
                    db.Close();
                }
            }
        }

        private void BookLst_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BookLst.SelectedItems.Count > 0)
            {
                using (SqliteConnection db =
                   new SqliteConnection($"Filename=bookStoreProject1.db"))
                {
                    db.Open();
                    SqliteCommand selectCommand = new SqliteCommand
                        (string.Concat("SELECT * from Books WHERE ISBN = '", BookLst.SelectedItem.ToString().AsSpan(0, 13), "'"), db);
                    SqliteDataReader query = selectCommand.ExecuteReader();
                    while (query.Read())
                    {
                        string sIsbn = query.GetInt64(0).ToString();
                        string sTitle = query.GetString(1);
                        string sDescription = query.GetString(2);
                        string sPrice = query.GetInt32(3).ToString();

                        isbn_Lbl.Content = sIsbn;
                        bookName_Lbl.Content = sTitle;
                        bookDescription_Txt.Text = sDescription;
                        bookPrice_Txt.Text = sPrice;
                        ttlPrice_Lbl.Content = sPrice;


                    }
                    db.Close();
                }
                quantity_Lbl.Content = 1;
            }
        }
        private void plus_Btn_Click(object sender, RoutedEventArgs e)
        {
            int quantity = int.Parse(quantity_Lbl.Content.ToString()) + 1;
            quantity_Lbl.Content = quantity;
            ttlPrice_Lbl.Content = int.Parse(bookPrice_Txt.Text) * quantity;

        }

        private void minus_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (int.Parse(quantity_Lbl.Content.ToString()) > 1)
            {
                int quantity = int.Parse(quantity_Lbl.Content.ToString()) - 1;
                quantity_Lbl.Content = quantity;
                ttlPrice_Lbl.Content = int.Parse(bookPrice_Txt.Text) * quantity;
            }
        }

        private void submitOrder_Btn_Click(object sender, RoutedEventArgs e)
        {
            if(CustomerLst.SelectedItem == null||BookLst.SelectedItem == null) 
            {
                if (CustomerLst.SelectedItem == null)
                {
                    MessageBox.Show("โปรดเลือกลูกค้า");
                }
                if(BookLst.SelectedItem == null)
                {
                    MessageBox.Show("โปรดเลือกสินค้า");
                }
            }
            else
            {
                DataAccess.AddTransaction(isbn_Lbl.Content.ToString(), customerId_Lbl.Content.ToString(), quantity_Lbl.Content.ToString(), ttlPrice_Lbl.Content.ToString());
                MessageBox.Show("สร้างรายการสั่งซื้อสำเร็จ\nลูกค้า : "+CustomerLst.SelectedItem.ToString()+"\nรายการสินค้า : "+BookLst.SelectedItem.ToString()+
                    "\nจำนวน "+quantity_Lbl.Content.ToString()+" เล่ม\nรวมราคา "+ttlPrice_Lbl.Content.ToString()+" บาท");

            }

        }
        private void showTransactions_Btn_Click(object sender, RoutedEventArgs e)
        {
            List<string> transactions = DataAccess.GetTransactions();
            string showTransacts = string.Join(",\n", transactions);
            MessageBox.Show(showTransacts.ToString());
        }
    }
}
