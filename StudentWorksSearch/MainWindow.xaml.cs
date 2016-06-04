using StudentWorksSearch.Engines;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StudentWorksSearch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {
            var engine = new UserEngine(txtboxLogIn.Text, passboxPass.Password);
            if (engine.LogInCheck())
            {
                engine.GetUserData();
                Search win = new Search();
                win.Show();
                this.Close();
            }
            else
            {
                txtboxLogIn.Clear();
                passboxPass.Clear();
                lblError.Text = "Неверный логин или пароль!";
            }
        }

        private void btnRegistration_Click(object sender, RoutedEventArgs e)
        {
            Repository.Edit = false;
            Registration win = new Registration();
            win.Show();
            this.Close();
        }
    }
}
