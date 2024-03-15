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

        }
        string sortChoice = "Title";
        string displayChoice = "Title";
        string searchCondition = "";
        int listDisplay = 1;
        public void fillBooksList()
        {
            using (SqliteConnection db =
               new SqliteConnection($"Filename=bookStoreProject1.db"))
            {
                db.Open();
                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT ISBN,Title from Books "+searchCondition+" ORDER BY "+sortChoice+" ASC", db);
                SqliteDataReader query = selectCommand.ExecuteReader();
                while (query.Read())
                {
                    string BookName = query.GetString(listDisplay);
                    BookLst.Items.Add(BookName);
                }
                db.Close();
            }
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
                refreshBookList();
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
                        ("SELECT * from Books WHERE "+displayChoice+" ='" + BookLst.SelectedItem.ToString() + "'", db);
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
                    DataAccess.EditBook(BookName_Txt.Text, Description_Txt.Text, Price_Txt.Text,displayChoice, BookLst.SelectedItem.ToString());
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
            refreshBookList();
        }

        private void DeleteBook_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (BookLst.SelectedItem != null)
            {
                DataAccess.DeleteBook(BookLst.SelectedItem.ToString(),displayChoice);
                refreshBookList();
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
            refreshBookList();
        }
        public void refreshBookList()
        {
            BookLst.Items.Clear();
            fillBooksList();

        }

        private void searchByIsbn_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Isbn_Txt.Text != "")
            {
                searchCondition = "WHERE ISBN LIKE '%" + Isbn_Txt.Text.ToString() + "%'";
                refreshBookList();
            }
            else
            {
                searchCondition = "";
                refreshBookList();
            }
        }

        private void searchByTitle_btn_Click(object sender, RoutedEventArgs e)
        {
            if (BookName_Txt.Text != "")
            {
                searchCondition = "WHERE Title LIKE '%" + BookName_Txt.Text + "%'";
                refreshBookList();
            }
            else
            {
                searchCondition = "";
                refreshBookList();
            }

        }

        private void title_RadBtn_Checked(object sender, RoutedEventArgs e)
        {
            listDisplay = 1;
            displayChoice = "Title";
            refreshBookList();
        }

        private void isbn_RadBtn_Checked(object sender, RoutedEventArgs e)
        {
            listDisplay = 0;
            displayChoice = "ISBN";
            refreshBookList();
        }

        private void refresh_Btn_Click(object sender, RoutedEventArgs e)
        {
            searchCondition = "";
            refreshBookList();
        }
    }
}
