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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LandCommittee
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int Role = 0;
        public MainWindow( int role)
        {
            InitializeComponent();
            Role = role;
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            RenderPages.Children.Clear();
            RenderPages.Children.Add(new UserDashboard());
        }

        private void btnOwner_Click(object sender, RoutedEventArgs e)
        {
            RenderPages.Children.Clear();
            RenderPages.Children.Add(new Owners());
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
            Login login = new Login();
            login.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RenderPages.Children.Clear();
            if (Role == 0)
            {
                RenderPages.Children.Add(new AdminDashboard());
            }
            else 
            {
                RenderPages.Children.Add(new UserDashboard());
            }
        }

        private void btnLandPlot_Click(object sender, RoutedEventArgs e)
        {
            RenderPages.Children.Clear();
            RenderPages.Children.Add(new LandPlots());
        }

        private void LeaseContract_Click(object sender, RoutedEventArgs e)
        {
            RenderPages.Children.Clear();
            RenderPages.Children.Add(new LeaseContracts());
        }

        private void ContractOfSale_Click(object sender, RoutedEventArgs e)
        {
            RenderPages.Children.Clear();
            RenderPages.Children.Add(new ContractOfSales());
        }
    }
}
