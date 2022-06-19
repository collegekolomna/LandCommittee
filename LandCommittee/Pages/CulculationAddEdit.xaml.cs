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
    /// Логика взаимодействия для CulculationAddEdit.xaml
    /// </summary>
    public partial class CulculationAddEdit : Window
    {
        LandCommittee_Entities db = new LandCommittee_Entities();
        int Status, culculationId;
        public CulculationAddEdit(Culculation culculations, int status)
        {
            InitializeComponent();
            Status = status;
            if (Status == 1)
            {
                culculationId = culculations.id;
                txtS.Text = culculations.description;
                txtN.Text = Convert.ToString(culculations.percentage);

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
                    var result = db.Culculation.Where(s => s.id == culculationId).FirstOrDefault();
                    //Изменение данных найденной услуги
                    result.description = txtS.Text;
                    result.percentage = Convert.ToInt32(txtN.Text);

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
                    Culculation culculations = new Culculation
                    {
                        description = txtS.Text,
                       percentage = Convert.ToInt32(txtN.Text),

                };
                    //Сохраняем ее
                    db.Culculation.Add(culculations);
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
