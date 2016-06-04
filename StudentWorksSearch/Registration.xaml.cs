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
using StudentWorksSearch.Engines;

namespace StudentWorksSearch
{
    /// <summary>
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();

            var engine = new DBEngine();
            cmbboxUni.ItemsSource = engine.GetUni();
            if (Repository.Edit)
            {
                lblHead.Text = "Изменение данных";
                btnReg.Content = "Сохранить";
                txtboxLogIn.Text = Repository.User.Login;
                txtboxLogIn.IsEnabled = false;
                lblName.Foreground = Brushes.DimGray;
                lblPass.Foreground = Brushes.DimGray;
                passboxPass.IsEnabled = false;
                txtboxMail.Text = Repository.User.E_mail;
                txtboxName.Text = Repository.User.Name;
                if (Repository.User.University != null)
                {
                    cmbboxUni.SelectedItem = Repository.User.University;
                }
            }
        }

        private void btnReg_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
                if (Repository.Edit == false)
                {
                    if (txtboxLogIn.Text != "" && txtboxMail.Text != "" && passboxPass.Password != "")
                    {
                        bool result;
                        var engine = new UserEngine(txtboxLogIn.Text, passboxPass.Password);

                        if (cmbboxUni.SelectedIndex != -1)
                        {
                            engine.AddUser(txtboxMail.Text, txtboxName.Text, cmbboxUni.SelectedItem.ToString(), out result);
                        }
                        else
                        {
                            engine.AddUser(txtboxMail.Text, txtboxName.Text, "", out result);
                        }

                        if (result)
                        {
                            engine.GetUserData();
                            Search win = new Search();
                            win.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Пользователь с таким логином уже существует!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                            txtboxLogIn.Clear();
                        }
                    }
                    else
                        MessageBox.Show("Введеные не все данные!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    if (txtboxLogIn.Text != "" && txtboxMail.Text != "")
                    {
                        var engine = new UserEngine(txtboxLogIn.Text, "");
                    if (cmbboxUni.SelectedIndex != -1)
                    {
                        engine.UpdUserData(txtboxMail.Text, txtboxName.Text, cmbboxUni.SelectedItem.ToString());
                    }
                    else
                    {
                        engine.UpdUserData(txtboxMail.Text, txtboxName.Text, null);
                    }
                        this.Close();
                    }
                    else
                        MessageBox.Show("Введеные не все данные!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            //}
            //catch (Exception)
            //{
            //    MessageBox.Show("Введеные не все данные!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            //}
        }
    }
}
