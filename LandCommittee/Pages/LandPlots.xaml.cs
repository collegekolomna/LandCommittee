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
    /// Логика взаимодействия для LandPlots.xaml
    /// </summary>
    public partial class LandPlots : UserControl
    {
        LandCommittee_Entities db = new LandCommittee_Entities();
        public LandPlots()
        {
            InitializeComponent();
            GetLandPlotViews();
            List<CategoryLand> categoryLands = db.CategoryLand.ToList();
            CategoryLand allTypes = new CategoryLand { id = -1, categoryLand1 = "Все типы" };
            categoryLands = categoryLands.Where(p => p.isDeleted == false).Prepend(allTypes).ToList();
            List.ItemsSource = categoryLands;
            List.SelectedIndex = 0;
        }
        public void GetLandPlotViews()
        {
            mainList.ItemsSource = null;
            mainList.Items.Refresh();
            var result = db.LandPlot.Where(p => p.isDeleted == false);
            mainList.ItemsSource = result.ToList();
        }
        public void UpdateData(object sender, object e)
        {
            String text = txtSearch.Text.ToLower();
            mainList.ItemsSource = db.LandPlot.Where(x => x.adress.ToLower().StartsWith(text) || x.cadastralNumber.ToLower().StartsWith(text)).ToList();
        }
        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += UpdateData;
            timer.Start();
        }

        private void List_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            filter(List.SelectedItem as CategoryLand);
        }
        void filter(CategoryLand value)
        {
            if (value.id == -1)
            {
                GetLandPlotViews();
            }
            else
            {
                var result = db.LandPlot.Where(p => p.CategoryLand.id == value.id && p.isDeleted==false);
                mainList.ItemsSource = result.ToList();
            }
        }
        public void Delete(LandPlot land)
        {
            try
            {
                LandPlot landPlot = db.LandPlot.Find(land.id);
                if (MessageBox.Show("Вы уверенны что хотите удалить?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    landPlot.isDeleted = true;
                    db.SaveChanges();
                    GetLandPlotViews();
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
            Delete((sender as Button).DataContext as LandPlot);
        }

        private void Show_Click(object sender, RoutedEventArgs e)
        {
            LandPlotAddEdit landPlotAddEdit = new LandPlotAddEdit((sender as Button).DataContext as LandPlot, 1);
            if (landPlotAddEdit.ShowDialog() == true)
            {
                mainList.ItemsSource = null;
                mainList.Items.Refresh();
                var result = db.LandPlot.Where(p => p.isDeleted == false);
                mainList.ItemsSource = result.ToList();
            }
        }

        private void AddLandPlot_Click(object sender, RoutedEventArgs e)
        {
            LandPlotAddEdit landPlotAddEdit = new LandPlotAddEdit((sender as Button).DataContext as LandPlot, 0);
            if (landPlotAddEdit.ShowDialog() == true)
            {
                GetLandPlotViews();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application excelapp = new Microsoft.Office.Interop.Excel.Application();
            excelapp.Visible = true;
            Microsoft.Office.Interop.Excel._Workbook workbook = (Microsoft.Office.Interop.Excel._Workbook)(excelapp.Workbooks.Add(Type.Missing));
            Microsoft.Office.Interop.Excel._Worksheet worksheet = (Microsoft.Office.Interop.Excel._Worksheet)workbook.ActiveSheet;
            worksheet.Name = "Земельные участки";

            Microsoft.Office.Interop.Excel.Range hRange = excelapp.get_Range("A1:F1");
            hRange.Merge(Type.Missing);
            hRange.Merge(Type.Missing);
            worksheet.Cells[1, 1] = "З Е М Е Л Ь Н Ы Е   У Ч А С Т К И";

            Microsoft.Office.Interop.Excel.Range ThisRange = excelapp.get_Range("A3:F3");
            ThisRange.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            var items = db.LandPlot.Where(p => p.isDeleted == false).ToList();
            worksheet.Cells[3, 1] = "Площадь";
            worksheet.Cells[3, 2] = "Застроеная площадь";
            worksheet.Cells[3, 3] = "Кадастровый номер";
            worksheet.Cells[3, 4] = "Стоимость";
            worksheet.Cells[3, 5] = "Тип";
            worksheet.Cells[3, 6] = "Адресс";

            Microsoft.Office.Interop.Excel.Range TRange = excelapp.get_Range("A4:F15");
            TRange.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;

            int i = 4;
            foreach (var elem in items)
            {
                worksheet.Cells[i, 1] = elem.square;
                worksheet.Cells[i, 2] = elem.builtSquare;
                worksheet.Cells[i, 3] = elem.cadastralNumber;
                worksheet.Cells[i, 4] = elem.cost;
                worksheet.Cells[i, 5] = elem.CategoryLand.categoryLand1;
                worksheet.Cells[i, 6] = elem.adress;
                i++;
            }

            worksheet.Columns.AutoFit();
        }
    }
}
