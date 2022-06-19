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
    /// Логика взаимодействия для ContractOfSaleAdmin.xaml
    /// </summary>
    public partial class ContractOfSaleAdmin : UserControl

    {
        LandCommittee_Entities db = new LandCommittee_Entities();
        public ContractOfSaleAdmin()
        {
            InitializeComponent();
            GetContractViews();
        }
        public void GetContractViews()
        {
            mainList.ItemsSource = null;
            mainList.Items.Refresh();
            var result = db.ContractOfSale.Where(p => p.isDeleted == true);
            mainList.ItemsSource = result.ToList();
        }
        public void Delete(ContractOfSale contract)
        {
            try
            {
                ContractOfSale c = db.ContractOfSale.Find(contract.id);
                if (MessageBox.Show("Вы уверенны что хотите восстановить запись?", "Восстановление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    c.isDeleted = false;
                    db.SaveChanges();
                    GetContractViews();
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
            Delete((sender as Button).DataContext as ContractOfSale);
        }
    }
}
