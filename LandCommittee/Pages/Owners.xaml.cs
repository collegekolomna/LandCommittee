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
using System.Windows.Threading;

namespace LandCommittee.Pages
{
    /// <summary>
    /// Логика взаимодействия для Owners.xaml
    /// </summary>
    public partial class Owners : UserControl
    {
        LandCommittee_Entities db = new LandCommittee_Entities();
        public Owners()
        {
            InitializeComponent();
            GetOwnerViews();
            List<categoryOwner> categoryOwners = db.categoryOwner.ToList();
            categoryOwner allTypes = new categoryOwner { id = -1, categoryOwner1 = "Все типы" };
            categoryOwners = categoryOwners.Where(p => p.isDeleted == false).Prepend(allTypes).ToList();
            List.ItemsSource = categoryOwners;
            List.SelectedIndex = 0;

            
        }

        public void GetOwnerViews()
        {
            mainList.ItemsSource = null;
            mainList.Items.Refresh();
            var result = db.Owner.Where(p => p.isDeleted == false);
            mainList.ItemsSource = result.ToList();
        }

        private void AddOwner_Click(object sender, RoutedEventArgs e)
        {
            OwnerAddEdit owner = new OwnerAddEdit((sender as Button).DataContext as Owner, 0);
            if (owner.ShowDialog() == true)
            {
                GetOwnerViews();
            }
        }

        public void Delete(Owner ow)
        {
            try
            {
                Owner owner = db.Owner.Find(ow.id);
                if (MessageBox.Show("Вы уверенны что хотите удалить?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    owner.isDeleted = true;
                    db.SaveChanges();
                    GetOwnerViews();
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
            Delete((sender as Button).DataContext as Owner);
        }

        private void Show_Click(object sender, RoutedEventArgs e)
        {
            OwnerAddEdit ownerAddEdit = new OwnerAddEdit((sender as Button).DataContext as Owner, 1);
            if (ownerAddEdit.ShowDialog() == true)
            {
                mainList.ItemsSource = null;
                mainList.Items.Refresh();
                var result = db.Owner;
                mainList.ItemsSource = result.ToList();
            }
        }

        public void UpdateData(object sender, object e)
        {
            String text = txtSearch.Text.ToLower();
            mainList.ItemsSource = db.Owner.Where(x => x.name.ToLower().StartsWith(text) || x.surname.ToLower().StartsWith(text) || x.patronomic.ToLower().StartsWith(text) || x.phone.ToLower().StartsWith(text)).ToList();
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += UpdateData;
            timer.Start();
        }

        void filter(categoryOwner value)
        {
            if (value.id == -1)
            {
                GetOwnerViews();
            }
            else
            {
                var result = db.Owner.Where(p => p.categoryOwner.id == value.id);
                mainList.ItemsSource = result.ToList();
            }
        }

        private void List_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            filter(List.SelectedItem as categoryOwner);
        }

        private void btnExcel_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application excelapp = new Microsoft.Office.Interop.Excel.Application();
            excelapp.Visible = true;
            Microsoft.Office.Interop.Excel._Workbook workbook = (Microsoft.Office.Interop.Excel._Workbook)(excelapp.Workbooks.Add(Type.Missing));
            Microsoft.Office.Interop.Excel._Worksheet worksheet = (Microsoft.Office.Interop.Excel._Worksheet)workbook.ActiveSheet;
            worksheet.Name = "Собственники_арендаторы";

            Microsoft.Office.Interop.Excel.Range hRange = excelapp.get_Range("A1:H1");
            hRange.Merge(Type.Missing);
            hRange.Merge(Type.Missing);
            worksheet.Cells[1, 1] = "С О Б С Т В Е Н Н И К И  /  А Р Е Н Д А Т О Р Ы";

            Microsoft.Office.Interop.Excel.Range ThisRange = excelapp.get_Range("A3:H3");
            ThisRange.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            var items = db.Owner.Where(p => p.isDeleted == false).ToList();
            worksheet.Cells[3, 1] = "Фамилия";
            worksheet.Cells[3, 2] = "Имя";
            worksheet.Cells[3, 3] = "Отчество";
            worksheet.Cells[3, 4] = "Телефон";
            worksheet.Cells[3, 5] = "Паспорт";
            worksheet.Cells[3, 6] = "Адрес";
            worksheet.Cells[3, 7] = "ИНН";
            worksheet.Cells[3, 8] = "Категория";

            Microsoft.Office.Interop.Excel.Range TRange = excelapp.get_Range("A4:H15");
            TRange.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;

            int i = 4;
            foreach (var elem in items)
            {
                worksheet.Cells[i, 1] = elem.surname;
                worksheet.Cells[i, 2] = elem.name;
                worksheet.Cells[i, 3] = elem.patronomic;
                worksheet.Cells[i, 4] = elem.phone;
                worksheet.Cells[i, 5] = elem.passport;
                worksheet.Cells[i, 6] = elem.adress;
                worksheet.Cells[i, 7] = Convert.ToInt64(elem.INN);
                worksheet.Cells[i, 8] = elem.categoryOwner.categoryOwner1;
                i++;
            }

            worksheet.Columns.AutoFit();
        }
    }
}
