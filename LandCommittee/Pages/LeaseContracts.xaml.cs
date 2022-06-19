using Word = Microsoft.Office.Interop.Word;
using LandCommittee.AppData;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
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
    /// Логика взаимодействия для LeaseContracts.xaml
    /// </summary>
    public partial class LeaseContracts : UserControl
    {
        LandCommittee_Entities db = new LandCommittee_Entities();
        public LeaseContracts()
        {
            InitializeComponent();
            GetContractViews();
            string[] str = new string[] { "Все", "По дате заключения договора", "По дате окончания договора" };

            List.ItemsSource = str;
            List.SelectedIndex = 0;
        }

        public void GetContractViews()
        {
            mainList.ItemsSource = null;
            mainList.Items.Refresh();
            var result = db.LeaseContract.Where(p => p.isDeleted == false);
            mainList.ItemsSource = result.ToList();
        }

        private void AddContract_Click(object sender, RoutedEventArgs e)
        {
            LeaseContractAddEdit leaseContractAddEdit = new LeaseContractAddEdit((sender as Button).DataContext as LeaseContract, 0);
            if (leaseContractAddEdit.ShowDialog() == true)
            {
                GetContractViews();
            }
        }
        public void UpdateData(object sender, object e)
        {
            String text = txtSearch.Text.ToLower();
            mainList.ItemsSource = db.LeaseContract.Where(x => x.LandPlot.adress.ToLower().StartsWith(text)
            || x.LandPlot.cadastralNumber.ToLower().StartsWith(text) || x.Owner.name.ToLower().StartsWith(text) || x.Owner.surname.ToLower().StartsWith(text)
            || x.Owner.patronomic.ToLower().StartsWith(text) || x.Owner.phone.ToLower().StartsWith(text)).ToList();
        }

        

        private void Show_Click(object sender, RoutedEventArgs e)
        {
            LeaseContractAddEdit leaseContractAddEdit = new LeaseContractAddEdit((sender as Button).DataContext as LeaseContract, 1);
            if (leaseContractAddEdit.ShowDialog() == true)
            {
                mainList.ItemsSource = null;
                mainList.Items.Refresh();
                var result = db.LeaseContract;
                mainList.ItemsSource = result.ToList();
            }
        }
        
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Delete((sender as Button).DataContext as LeaseContract);
        }

        public void Delete(LeaseContract lease)
        {
            try
            {
                LeaseContract leaseContract = db.LeaseContract.Find(lease.id);
                if (MessageBox.Show("Вы уверенны что хотите удалить?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    leaseContract.isDeleted = true;

                    var land = db.LandPlot.Where(l => l.id == lease.idLandPlot).FirstOrDefault();
                    land.status = true;
                    db.SaveChanges();
                    GetContractViews();
                    MessageBox.Show("Данные успешно удалены", "Удаление", MessageBoxButton.OK);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK);
            }
        }
        public void Save_World(LeaseContract lease)
        {
            // Создаём объект документа
            Word.Document doc = null;
            try
            {
                string start1 = "", tenat = "", tenat1 = "", square = "", adress = "", use = "", start = "", end = "", cost = "";
                // Создаём объект приложения
                Word.Application app = new Word.Application();
                // Путь до шаблона документа
                string source = System.IO.Path.GetFullPath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\\..\\..\\")) + "Договор_аренды_шаблон.docx";
                // Открываем
                doc = app.Documents.Open(source);
                object path = System.IO.Path.GetFullPath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\\..\\..\\")) + "Договор_аренды.docx";
                doc.SaveAs(ref path);
                // Закрываем документ
                doc.Close();

                source = System.IO.Path.GetFullPath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\\..\\..\\")) + "Договор_аренды.docx";
                //Открываем
                doc = app.Documents.Open(source);
                doc.Activate();

                LeaseContract leaseContract = db.LeaseContract.Find(lease.id);
                start1 = leaseContract.startContract.ToString("dd.MM.yyyy");
                tenat = leaseContract.Owner.surname + leaseContract.Owner.name + leaseContract.Owner.patronomic;
                tenat1 = leaseContract.Owner.surname + leaseContract.Owner.name + leaseContract.Owner.patronomic;
                square = (leaseContract.LandPlot.square + leaseContract.LandPlot.builtSquare).ToString();
                adress = leaseContract.LandPlot.adress;
                use = leaseContract.actualUse;
                start = leaseContract.startContract.ToString("dd.MM.yyyy");
                end = leaseContract.startContract.ToString("dd.MM.yyyy");
                cost = leaseContract.sumCost.ToString();

                // Добавляем информацию
                // wBookmarks содержит все закладки
                Word.Bookmarks wBookmarks = doc.Bookmarks;
                Word.Range wRange;
                int i = 0;
                string[] data = new string[9] { adress, cost, end, square, start, start1, tenat, tenat1, use };
                foreach (Word.Bookmark mark in wBookmarks)
                {

                    wRange = mark.Range;
                    wRange.Text = data[i];
                    i++;
                }
                
                // Закрываем документ
                doc.Close();
                doc = null;
            }
            catch (SqlException ex)
            {
                // Если произошла ошибка, то
                // закрываем документ и выводим информацию
                doc.Close();
                doc = null;
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK);
            }
        }
        private void Download_Click(object sender, RoutedEventArgs e)
        {
            Save_World((sender as Button).DataContext as LeaseContract);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application excelapp = new Microsoft.Office.Interop.Excel.Application();
            excelapp.Visible = true;
            Microsoft.Office.Interop.Excel._Workbook workbook = (Microsoft.Office.Interop.Excel._Workbook)(excelapp.Workbooks.Add(Type.Missing));
            Microsoft.Office.Interop.Excel._Worksheet worksheet = (Microsoft.Office.Interop.Excel._Worksheet)workbook.ActiveSheet;
            worksheet.Name = "Договор_аренды";

            Microsoft.Office.Interop.Excel.Range hRange = excelapp.get_Range("A1:J1");
            hRange.Merge(Type.Missing);
            hRange.Merge(Type.Missing);
            worksheet.Cells[1, 1] = "Д О Г О В О Р А";

            Microsoft.Office.Interop.Excel.Range ThisRange = excelapp.get_Range("A3:J3");
            ThisRange.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            var items = db.LeaseContract.Where(p => p.isDeleted == false).ToList();
            worksheet.Cells[3, 1] = "Дата начала";
            worksheet.Cells[3, 2] = "Дата окончания";
            worksheet.Cells[3, 3] = "Кадастровый номер";
            worksheet.Cells[3, 4] = "Адрес";
            worksheet.Cells[3, 5] = "Площадь";
            worksheet.Cells[3, 6] = "Застроенная площадь";
            worksheet.Cells[3, 7] = "Назначение";
            worksheet.Cells[3, 8] = "Арендатор";
            worksheet.Cells[3, 9] = "Телефон";
            worksheet.Cells[3, 10] = "Аренда в месяц";

            Microsoft.Office.Interop.Excel.Range TRange = excelapp.get_Range("A4:J15");
            TRange.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;

            int i = 4;
            foreach (var elem in items)
            {
                worksheet.Cells[i, 1] = elem.startContract;
                worksheet.Cells[i, 2] = elem.endContract;
                worksheet.Cells[i, 3] = elem.LandPlot.cadastralNumber;
                worksheet.Cells[i, 4] = elem.LandPlot.adress;
                worksheet.Cells[i, 5] = elem.LandPlot.square;
                worksheet.Cells[i, 6] = elem.LandPlot.builtSquare;
                worksheet.Cells[i, 7] = elem.actualUse;
                worksheet.Cells[i, 8] = elem.Owner.FIO;
                worksheet.Cells[i, 9] = elem.Owner.phone;
                worksheet.Cells[i, 10] = elem.sumCost;
                i++;
            }

            worksheet.Columns.AutoFit();
        }
       
        private void List_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (List.SelectedIndex == -1)
            {
                GetContractViews();
            }
            else if (List.SelectedIndex == 1)
            {
                var result = db.LeaseContract.Where(l => l.isDeleted == false).OrderBy(l => l.startContract);
                mainList.ItemsSource = result.ToList();
            }
            else
            {
                var result = db.LeaseContract.Where(l => l.isDeleted == false).OrderBy(l => l.endContract);
                mainList.ItemsSource = result.ToList();
            }
        }
    
        private void txtSearch_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += UpdateData;
            timer.Start();
        }
    }
}
