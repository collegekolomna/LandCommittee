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
    /// Логика взаимодействия для LandsPlotAdmin.xaml
    /// </summary>
    public partial class LandsPlotAdmin : UserControl
    {
        LandCommittee_Entities db = new LandCommittee_Entities();
        public LandsPlotAdmin()
        {
            InitializeComponent();
            GetLPViews();
        }
        public void GetLPViews()
        {
            mainList.ItemsSource = null;
            mainList.Items.Refresh();
            var result = db.LandPlot.Where(p => p.isDeleted == true);
            mainList.ItemsSource = result.ToList();
        }
        public void Delete(LandPlot lp)
        {
            try
            {
                LandPlot land = db.LandPlot.Find(lp.id);
                if (MessageBox.Show("Вы уверенны что хотите восстановить запись?", "Восстановление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    land.isDeleted = false;
                    db.SaveChanges();
                    GetLPViews();
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
            Delete((sender as Button).DataContext as LandPlot);
        }
    }
}
