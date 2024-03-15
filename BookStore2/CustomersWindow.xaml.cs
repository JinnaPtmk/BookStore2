using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookStore2
{
    /// <summary>
    /// Interaction logic for CustomersWindow.xaml
    /// </summary>
    public partial class CustomersWindow : Window
    {
        public CustomersWindow()
        {
            InitializeComponent();
            CustomerId_Txt.Text = DateTime.Now.ToString("yyMMdd") + (DataAccess.CustomerCount()+1).ToString("0000");
            fillCustomersList();
        }
        string sortChoice = "Customer_Name";
        string displayChoice = "Customer_Name";
        string searchCondition = "";
        int listDisplay = 1;
        int CustomerCounter = 0;
        public void fillCustomersList()
        {
            using (SqliteConnection db =
               new SqliteConnection($"Filename=bookStoreProject1.db"))
            {
                db.Open();
                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT Customer_Id,Customer_Name FROM Customers " + searchCondition + " ORDER BY " + sortChoice + " ASC", db);
                SqliteDataReader query = selectCommand.ExecuteReader();
                while (query.Read())
                {
                    string CustomerName = query.GetString(listDisplay);
                    CustomersLst.Items.Add(CustomerName);
                }
                db.Close();
            }
        }
        public void refreshCustomerList()
        {
            CustomersLst.Items.Clear();
            fillCustomersList();

        }
        private void CustomerId_Txt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void CustomerId_Txt_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void CustomerEmail_Txt_Copy_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void CustomerEmail_Txt_Copy_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void AddCustomer_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (CustomerId_Txt.Text.Length != 10 || CustomerName_Txt.Text == "")
            {
                string notice = "";
                if (CustomerId_Txt.Text.Length != 10)
                {
                    notice += "\nกรุณากรอกรหัสลูกค้า 10 หลัก";
                }
                if (CustomerName_Txt.Text == "")
                {
                    notice += "\nกรุณาใส่ชื่อลูกค้า";
                }
                MessageBox.Show(notice, "เกิดข้อผิดพลาด");
            }
            else if (DataAccess.UniqueCustomerCheck(CustomerId_Txt.Text))
            {
                MessageBox.Show("รหัสลูกค้านี้มีอยู่แล้ว", "รหัสซ้ำ");
            }
            else
            {
                DataAccess.AddCustomer(CustomerId_Txt.Text, CustomerName_Txt.Text, CustomerAddress_Txt.Text, CustomerPhone_Txt.Text,CustomerEmail_Txt.Text);
                MessageBox.Show("ชื่อ : " + CustomerName_Txt.Text + "\nรหัสลูกค้า : " + CustomerId_Txt.Text + "เพิ่มรายการแล้ว");
                CustomersLst.SelectedItem = null;
                CustomerName_Txt.Clear();
                CustomerId_Txt.Text = DateTime.Now.ToString("yyMMdd") + (DataAccess.CustomerCount()+1).ToString("0000");
                CustomerAddress_Txt.Clear();
                CustomerPhone_Txt.Clear();
                CustomerEmail_Txt.Clear();
                refreshCustomerList();
            }
        }

        private void CustomersLst_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CustomersLst.SelectedItems.Count > 0)
            {

                using (SqliteConnection db =
                   new SqliteConnection($"Filename=bookStoreProject1.db"))
                {
                    db.Open();
                    SqliteCommand selectCommand = new SqliteCommand
                        ("SELECT * from Customers WHERE " + displayChoice + " ='" + CustomersLst.SelectedItem.ToString() + "'", db);
                    SqliteDataReader query = selectCommand.ExecuteReader();
                    while (query.Read())
                    {
                        string sCustomerId = query.GetInt64(0).ToString();
                        string sCustomerName = query.GetString(1);
                        string sAddress = query.GetString(2);
                        string sPhone = query.GetInt32(3).ToString();
                        string sEmail = query.GetString(4);

                        CustomerId_Txt.Text = sCustomerId;
                        CustomerName_Txt.Text = sCustomerName;
                        CustomerAddress_Txt.Text = sAddress;
                        CustomerPhone_Txt.Text = sPhone;
                        CustomerEmail_Txt.Text = sEmail;


                    }
                    db.Close();
                }


            }
        }

        private void RemoveCustomer_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (CustomersLst.SelectedItem != null)
            {
                DataAccess.DeleteCustomer(CustomersLst.SelectedItem.ToString(), displayChoice);
                refreshCustomerList();
            }
            else
            {
                MessageBox.Show("กรุณาเลือกรายการที่ต้องการจะลบ", "ลบ");
            }
        }
    }
}
