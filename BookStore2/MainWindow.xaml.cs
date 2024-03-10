using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookStore2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataAccess.InitializeDatabase();
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            if(DataAccess.LoggingIn(userIdTxt.Text, userPasswordTxt.Password))
            {
                HomeWindow homeWindow = new HomeWindow();
                homeWindow.Show();
                Visibility = Visibility.Collapsed;
            }
            else
            {
                MessageBox.Show("ID หรือ Password ไม่ถูกต้อง");
            }
           
        }
    }
}