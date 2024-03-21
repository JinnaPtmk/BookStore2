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
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using MessageBox = System.Windows.MessageBox;

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
        string searchCondition = "";
        readonly string output = "{0,-10}\t{1,-10}";
        public void fillCustomersList()
        {
            CustomersLst.Items.Clear();
            using (SqliteConnection db =
               new SqliteConnection($"Filename=bookStoreProject1.db"))
            {
                db.Open();
                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT Customer_Id,Customer_Name FROM Customers " + searchCondition + " ORDER BY " + sortChoice + " ASC", db);
                SqliteDataReader query = selectCommand.ExecuteReader();
                while (query.Read())
                {
                    string CustomerName = query.GetString(1);
                    string CustomerId = query.GetString(0);
                    CustomersLst.Items.Add(string.Format(output,CustomerId,CustomerName));
                }
                db.Close();
            }
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
                fillCustomersList();
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
                        (string.Concat("SELECT * from Customers WHERE Customer_Id = '", CustomersLst.SelectedItem.ToString().AsSpan(0, 10), "'"), db);
                    SqliteDataReader query = selectCommand.ExecuteReader();
                    while (query.Read())
                    {
                        string sCustomerId = query.GetInt64(0).ToString();
                        string sCustomerName = query.GetString(1);
                        string sAddress = query.GetString(2);
                        string sPhone = query.GetString(3);
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
                DataAccess.DeleteCustomer(CustomersLst.SelectedItem.ToString().Substring(0, 10));
                fillCustomersList();
            }
            else
            {
                MessageBox.Show("กรุณาเลือกรายการที่ต้องการจะลบ", "ลบ");
            }
        }


        private void Sort_Cbx_DropDownClosed(object sender, EventArgs e)
        {
            if (Sort_Cbx.SelectedIndex == 0)
            {
                sortChoice = "Customer_Name";
            }
            else
            {
                sortChoice = "Customer_Id";
            }
            fillCustomersList();
        }
        private void Refresh_Btn_Click(object sender, RoutedEventArgs e)
        {
            searchCondition = "";
            CustomerId_Txt.Text = DateTime.Now.ToString("yyMMdd") + (DataAccess.CustomerCount() + 1).ToString("0000");
            CustomerName_Txt.Text = "";
            CustomerAddress_Txt.Text = "";
            CustomerPhone_Txt.Text = "";
            CustomerEmail_Txt.Text = "";
            fillCustomersList();
        }

        private void Search_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (CustomerId_Txt.Text != "")
            {
                searchCondition = "WHERE Customer_Id LIKE '%" + search_Txt.Text.ToString() + "%' OR Customer_Name LIKE '%"+search_Txt.Text.ToString()+"%'";
                fillCustomersList();
            }
            else
            {
                searchCondition = "";
                fillCustomersList();
            }
        }
        private void EditCustomer_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (CustomersLst.SelectedItem != null)
            {
                if (DataAccess.UniqueCustomerCheck(CustomerId_Txt.Text))
                {
                    DataAccess.EditCustomer(CustomerName_Txt.Text, CustomerAddress_Txt.Text, CustomerPhone_Txt.Text,CustomerEmail_Txt.Text, CustomersLst.SelectedItem.ToString().Substring(0, 10));
                    MessageBox.Show("แก้ไขรายการแล้ว", "แก้ไข");
                }
                else
                {
                    MessageBox.Show("ไม่สามารถแก้ไขรหัสลูกค้าได้ กรุณาเพิ่มเป็นรายการใหม่", "เกิดข้อผิดพลาด");
                }

            }
            else
            {
                MessageBox.Show("กรุณาเลือกรายการที่ต้องการแก้ไข", "แก้ไข");
            }
            fillCustomersList();
        }
    }
}
