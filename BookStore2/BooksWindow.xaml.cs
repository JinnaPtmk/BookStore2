using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for BooksWindow.xaml
    /// </summary>
    public partial class BooksWindow : Window
    {
        public BooksWindow()
        {
            InitializeComponent();
            fillBooksList();

        }
        string sortChoice = "Title";
        string searchCondition = "";
        readonly string output = "{0,-10}\t{1,-10}";
        public void fillBooksList()
        {
            BookLst.Items.Clear();
            using SqliteConnection db =
               new($"Filename=bookStoreProject1.db");
            db.Open();
            SqliteCommand selectCommand = new SqliteCommand
                ("SELECT ISBN,Title from Books " + searchCondition + " ORDER BY " + sortChoice + " ASC", db);
            SqliteDataReader query = selectCommand.ExecuteReader();
            while (query.Read())
            {
                string BookName = query.GetString(1);
                string BookISBN = query.GetString(0);
                BookLst.Items.Add(string.Format(output, BookISBN, BookName));
            }
            db.Close();
        }
        
        private void Isbn_Txt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void Isbn_Txt_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void Price_Txt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void AddBook_Btn_Click_1(object sender, RoutedEventArgs e)
        {
            if (Isbn_Txt.Text.Length != 13 || BookName_Txt.Text == "" || Price_Txt.Text == "")
            {
                string notice = "";
                if (Isbn_Txt.Text.Length != 13)
                {
                    notice += "\nกรุณากรอกเลข ISBN 13 หลัก";
                }
                if (BookName_Txt.Text == "")
                {
                    notice += "\nกรุณาใส่ชื่อหนังสือ";
                }
                if (Price_Txt.Text == "")
                {
                    notice += "\nกรุณาใส่ราคา";
                }
                MessageBox.Show(notice, "เกิดข้อผิดพลาด");
            }
            else if (DataAccess.UniqueIsbnCheck(Isbn_Txt.Text))
            {
                MessageBox.Show("รหัส ISBN นี้มีอยู่แล้ว", "รหัสซ้ำ");
            }
            else
            {
                DataAccess.AddBook(Isbn_Txt.Text, BookName_Txt.Text, Description_Txt.Text, Price_Txt.Text);
                MessageBox.Show("ชื่อ : " + BookName_Txt.Text + "\nISBN : " + Isbn_Txt.Text + "\nราคา : " + Price_Txt.Text, "เพิ่มรายการแล้ว");
                BookLst.SelectedItem = null;
                BookName_Txt.Clear();
                Isbn_Txt.Clear();
                Description_Txt.Clear();
                Price_Txt.Clear();
                fillBooksList();
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
                        (string.Concat("SELECT * from Books WHERE ISBN ='", BookLst.SelectedItem.ToString().AsSpan(0, 13), "'"), db);
                    SqliteDataReader query = selectCommand.ExecuteReader();
                    while (query.Read())
                    {
                        string sIsbn = query.GetInt64(0).ToString();
                        string sTitle = query.GetString(1);
                        string sDescription = query.GetString(2);
                        string sPrice = query.GetInt32(3).ToString();

                        Isbn_Txt.Text = sIsbn;
                        BookName_Txt.Text = sTitle;
                        Description_Txt.Text = sDescription;
                        Price_Txt.Text = sPrice;


                    }
                    db.Close();
                }
            }
        }

        private void EditBook_Btn_Click(object sender, RoutedEventArgs e)
        {
            if(BookLst.SelectedItem != null) 
            {
                if (DataAccess.UniqueIsbnCheck(Isbn_Txt.Text))
                {
                    DataAccess.EditBook(BookName_Txt.Text, Description_Txt.Text, Price_Txt.Text, BookLst.SelectedItem.ToString().Substring(0,13));
                    MessageBox.Show("แก้ไขรายการแล้ว","แก้ไข");
                }
                else
                {
                    MessageBox.Show("ไม่สามารถแก้ไขรหัส ISBN ได้ กรุณาเพิ่มเป็นรายการใหม่", "เกิดข้อผิดพลาด");
                }
                
            }
            else
            {
                MessageBox.Show("กรุณาเลือกรายการที่ต้องการแก้ไข", "แก้ไข");
            }
            fillBooksList();
        }

        private void DeleteBook_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (BookLst.SelectedItem != null)
            {
                DataAccess.DeleteBook(BookLst.SelectedItem.ToString().Substring(0,13));
                fillBooksList();
            }
            else
            {
                MessageBox.Show("กรุณาเลือกรายการที่ต้องการจะลบ", "ลบ");
            }
        }

        private void Sort_Cbx_DropDownClosed(object sender, EventArgs e)
        {
           if(Sort_Cbx.SelectedIndex == 0)
            {
                sortChoice = "Title";
            }
            else
            {
                sortChoice = "ISBN";
            }
            fillBooksList();
        }
        private void search_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (searchTxt.Text != "")
            {
                searchCondition = "WHERE ISBN LIKE '%" + searchTxt.Text.ToString() + "%' OR Title LIKE '%" + searchTxt.Text.ToString() + "%'";
                fillBooksList();
            }
            else
            {
                searchCondition = "";
                fillBooksList();
            }
        }

        private void refresh_Btn_Click(object sender, RoutedEventArgs e)
        {
            searchCondition = "";
            fillBooksList();
        }
    }
}
