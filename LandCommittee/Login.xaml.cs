using LandCommittee.Pages;
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

namespace LandCommittee
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void login_Click(object sender, RoutedEventArgs e)
        {
            string login = txtLogin.Text;
            string password = txtPassword.Password;
            //если вход админ
            if (login != "admin")
            {
                ;
            }
            else if (password != "1234")
            {
                ;
            }
            else
            {
                Hide();
                Admin_Dashboard mainWindow = new Admin_Dashboard();
                mainWindow.Show();
            }

            //если вход пользователь
            if (login != "user")
            {
                ;
            }
            else if (password != "1234")
            {
                ;
            }
            else
            {
                Hide();
                MainWindow mainWindow = new MainWindow(1);
                mainWindow.Show();
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        } 
    }
}
