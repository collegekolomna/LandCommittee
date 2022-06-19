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
    /// Логика взаимодействия для CategoryOwnersAddEdit.xaml
    /// </summary>
    public partial class CategoryOwnersAddEdit : Window
    {
        LandCommittee_Entities db = new LandCommittee_Entities();
        int Status, categoryOwnerID;

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (Status == 1)
            {
                try
                {
                    //Поиск по ID
                    var result = db.categoryOwner.Where(s => s.id == categoryOwnerID).FirstOrDefault();
                    //Изменение данных найденной услуги
                    result.categoryOwner1 = txtS.Text;
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
                    categoryOwner category = new categoryOwner
                    {
                        categoryOwner1 = txtS.Text,
                        description = txtN.Text,

                    };
                    //Сохраняем ее
                    db.categoryOwner.Add(category);
                    db.SaveChanges();
                    this.DialogResult = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK);
                }
            }
        }

        public CategoryOwnersAddEdit(categoryOwner categoryOwner, int status)
        {
            InitializeComponent();

            Status = status;
            if (Status == 1)
            {
                categoryOwnerID = categoryOwner.id;
                txtS.Text = categoryOwner.categoryOwner1;
                txtN.Text = categoryOwner.description;

            }
            else
            {

            }
        }
    }
}
