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
    /// Interaction logic for AddUserWindow.xaml
    /// </summary>
    public partial class AddUserWindow : Window
    {
        public AddUserWindow()
        {
            InitializeComponent();
        }

        private void addUser_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (!DataAccess.UniqueUserCheck(userId_Txt.Text))
            {
                DataAccess.AddUser(userId_Txt.Text,userPw_Txt.Text);
                MessageBox.Show("เพิ่มผู้ใช้แล้ว");
                this.Close();  
            }
            else
            {
                MessageBox.Show("ชื่อผู้ใช้นี้มีอยู่แล้ว");
            }
        }
    }
}
