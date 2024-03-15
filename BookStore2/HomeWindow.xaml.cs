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
        }

        private void ManageBooksBtn_Click(object sender, RoutedEventArgs e)
        {
            BooksWindow booksWindow = new BooksWindow();
            booksWindow.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CustomersWindow customerWindow = new CustomersWindow();
            customerWindow.Show();
        }
    }
}
