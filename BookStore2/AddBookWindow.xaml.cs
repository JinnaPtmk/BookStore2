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
    /// Interaction logic for AddBookWindow.xaml
    /// </summary>
    public partial class AddBookWindow : Window
    {
        public AddBookWindow()
        {
            InitializeComponent();
            if (TitleTxt.Text != "" || IsbnTxt.Text != "" || DescriptionTxt.Text != "" || PriceTxt.Text != "")
            {
                CancelBtn.Content = "{Clear}";
            }
        }

        private void PriceTxt_previewtextinput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }


        private void IsbnTxt_previewtextinput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }
        private void IsbnTxt_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            if(TitleTxt.Text == "" && IsbnTxt.Text == "" && DescriptionTxt.Text == "" && PriceTxt.Text == "")
            {
                this.Close();
            }
            else
            {
                TitleTxt.Clear();
                IsbnTxt.Clear();
                DescriptionTxt.Clear();
                PriceTxt.Clear();
            }
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if(IsbnTxt.Text.Length != 13||TitleTxt.Text == ""||PriceTxt.Text == "")
            {
                string notice = "";
                if (IsbnTxt.Text.Length != 13)
                {
                    notice += "\nกรุณากรอกเลข ISBN 13 หลัก";
                }
                if (TitleTxt.Text == "")
                {
                    notice += "\nกรุณาใส่ชื่อหนังสือ";
                }
                if (PriceTxt.Text == "")
                {
                    notice += "\nกรุณาใส่ราคา";
                }
                MessageBox.Show(notice,"เกิดข้อผิดพลาด");
            }
            else if (DataAccess.UniqueCheck(IsbnTxt.Text))
            {
                MessageBox.Show("รหัส ISBN นี้มีอยู่แล้ว","หรัสซ้ำ");
            }
            else
            {
                DataAccess.AddBook(IsbnTxt.Text, TitleTxt.Text, DescriptionTxt.Text, PriceTxt.Text);
                MessageBox.Show("ชื่อ : " + TitleTxt.Text + "\nISBN : " + IsbnTxt.Text + "\nราคา : " + PriceTxt.Text,"เพิ่มรายการแล้ว");
                TitleTxt.Clear();
                IsbnTxt.Clear();
                DescriptionTxt.Clear();
                PriceTxt.Clear();
            }
        }

        
    }
}
