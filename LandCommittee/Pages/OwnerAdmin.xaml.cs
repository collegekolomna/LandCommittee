using LandCommittee.AppData;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace LandCommittee.Pages
{
    /// <summary>
    /// Логика взаимодействия для Users.xaml
    /// </summary>
    public partial class Users : UserControl
    {
        LandCommittee_Entities db = new LandCommittee_Entities();
        public Users()
        {
            InitializeComponent();
            GetOwnerViews();
        }
        public void GetOwnerViews()
        {
            mainList.ItemsSource = null;
            mainList.Items.Refresh();
            var result = db.Owner.Where(p => p.isDeleted == true);
            mainList.ItemsSource = result.ToList();
        }
        private void AddUser_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Show_Click(object sender, RoutedEventArgs e)
        {

        }
        public void Delete(Owner ow)
        {
            try
            {
                Owner owner = db.Owner.Find(ow.id);
                if (MessageBox.Show("Вы уверенны что хотите восстановить запись?", "Восстановление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    owner.isDeleted = false;
                    db.SaveChanges();
                    GetOwnerViews();
                    MessageBox.Show("Данные успешно восстановлены", "Восстановление", MessageBoxButton.OK);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK);
            }
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Delete((sender as Button).DataContext as Owner);
        }
    }
}
