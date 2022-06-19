using LandCommittee.AppData;
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

namespace LandCommittee.Pages
{
    /// <summary>
    /// Логика взаимодействия для CategoryLandsAddEdit.xaml
    /// </summary>
    public partial class CategoryLandsAddEdit : Window
    {
        LandCommittee_Entities db = new LandCommittee_Entities();
        int Status, categoryLandId;
        public CategoryLandsAddEdit(CategoryLand categoryLand, int status)
        {
            InitializeComponent();
            Status = status;
            if (Status == 1)
            {
                categoryLandId = categoryLand.id;
                txtS.Text = categoryLand.categoryLand1;
                txtN.Text = categoryLand.description;
                
            }
            else
            {

            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (Status == 1)
            {
                try
                {
                    //Поиск по ID
                    var result = db.CategoryLand.Where(s => s.id == categoryLandId).FirstOrDefault();
                    //Изменение данных найденной услуги
                    result.categoryLand1 = txtS.Text;
                    result.description = txtN.Text;

                    //Сохранение
                    db.SaveChanges();
                    this.DialogResult = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK);
                }
            }
            else
            {
                try
                {
                    //Создаем новую запись
                    CategoryLand category = new CategoryLand
                    {
                        categoryLand1 = txtS.Text,
                        description = txtN.Text,

                    };
                    //Сохраняем ее
                    db.CategoryLand.Add(category);
                    db.SaveChanges();
                    this.DialogResult = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK);
                }
            }
        }
    }
}
