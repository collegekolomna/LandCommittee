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
    /// Логика взаимодействия для CategoryLands.xaml
    /// </summary>
    public partial class CategoryLands : UserControl
    {
        LandCommittee_Entities db = new LandCommittee_Entities();
        public CategoryLands()
        {
            InitializeComponent();
            GetCategoryViews();
        }
        public void GetCategoryViews()
        {
            mainList.ItemsSource = null;
            mainList.Items.Refresh();
            var result = db.CategoryLand.Where(p => p.isDeleted== false);
            mainList.ItemsSource = result.ToList();
        }

        private void AddCategory_Click(object sender, RoutedEventArgs e)
        {
            CategoryLandsAddEdit category = new CategoryLandsAddEdit((sender as Button).DataContext as CategoryLand, 0);
            if (category.ShowDialog() == true)
            {
                GetCategoryViews();
            }
        }
        public void Delete(CategoryLand category)
        {
            try
            {
                CategoryLand categoryLand = db.CategoryLand.Find(category.id);
                if (MessageBox.Show("Вы уверенны что хотите удалить?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    categoryLand.isDeleted = true;
                    db.SaveChanges();
                    GetCategoryViews();
                    MessageBox.Show("Данные успешно удалены", "Удаление", MessageBoxButton.OK);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK);
            }
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Delete((sender as Button).DataContext as CategoryLand);
        }

        private void Show_Click(object sender, RoutedEventArgs e)
        {
            CategoryLandsAddEdit category = new CategoryLandsAddEdit((sender as Button).DataContext as CategoryLand, 1);
            if (category.ShowDialog() == true)
            {
                GetCategoryViews();
            }
        }
    }
}
