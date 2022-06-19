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
    /// Логика взаимодействия для ContractOfSales.xaml
    /// </summary>
    public partial class ContractOfSales : UserControl
    {
        LandCommittee_Entities db = new LandCommittee_Entities();
        public ContractOfSales()
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
            var result = db.ContractOfSale.Where(p => p.isDeleted == false);
            mainList.ItemsSource = result.ToList();
        }
        private void AddContract_Click(object sender, RoutedEventArgs e)
        {
            ContractOfSaleAddEdit contractOfSaleAddEdit = new ContractOfSaleAddEdit((sender as Button).DataContext as ContractOfSale, 0);
            if (contractOfSaleAddEdit.ShowDialog() == true)
            {
                GetContractViews();
            }
        }

        private void Show_Click(object sender, RoutedEventArgs e)
        {
            ContractOfSaleAddEdit contractOfSaleAddEdit = new ContractOfSaleAddEdit((sender as Button).DataContext as ContractOfSale, 1);
            if (contractOfSaleAddEdit.ShowDialog() == true)
            {
                mainList.ItemsSource = null;
                mainList.Items.Refresh();
                var result = db.ContractOfSale;
                mainList.ItemsSource = result.ToList();
            }
        }
        public void Save_World(ContractOfSale lease)
        {
            // Создаём объект документа
            Microsoft.Office.Interop.Word.Document doc = null;
            try
            {
                string data1 = "", owner = "", number = "", square = "", adress = "", use = "", cost = "";
                // Создаём объект приложения
                Word.Application app = new Word.Application();
                // Путь до шаблона документа
                string source = System.IO.Path.GetFullPath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\\..\\..\\")) + "\\Договор_купли_продажи_шаблон.docx";
                // Открываем
                doc = app.Documents.Open(source);
                //object path = System.IO.Path.GetFullPath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\\..\\..\\")) + "\\Договор_купли_продажи.docx";
                //doc.SaveAs(ref path);
                //// Закрываем документ
                //doc.Close();

                //source = System.IO.Path.GetFullPath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\")) + "\\Договор_купли_продажи.docx";
                ////Открываем
                //doc = app.Documents.Open(source);
                doc.Activate();

                ContractOfSale contract = db.ContractOfSale.Find(lease.id);
                data1 = contract.data.ToString("dd.MM.yyyy");
                owner = contract.Owner.surname +" "+ contract.Owner.name +" "+ contract.Owner.patronomic;
                number = contract.LandPlot.cadastralNumber;
                square = (contract.LandPlot.square + contract.LandPlot.builtSquare).ToString();
                adress = contract.LandPlot.adress;
                use = contract.actualUse;
                cost = contract.sumCost.ToString();

                // Добавляем информацию
                // wBookmarks содержит все закладки
                Word.Bookmarks wBookmarks = doc.Bookmarks;
                Word.Range wRange;
                int i = 0;
                string[] data = new string[7] { adress, cost, data1, number, owner, square, use };
                foreach (Word.Bookmark mark in wBookmarks)
                {
                    wRange = mark.Range;
                    wRange.Text = data[i];
                    i++;
                }

                // Закрываем документ
                doc.Save();
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
            Save_World((sender as Button).DataContext as ContractOfSale);
        }

        public void Delete(ContractOfSale contract)
        {
            try
            {
                ContractOfSale contractOfSale = db.ContractOfSale.Find(contract.id);
                if (MessageBox.Show("Вы уверенны что хотите удалить?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    contractOfSale.isDeleted = true;
                    var land = db.LandPlot.Where(l => l.id == contract.idLandPlot).FirstOrDefault();
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
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Delete((sender as Button).DataContext as ContractOfSale);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application excelapp = new Microsoft.Office.Interop.Excel.Application();
            excelapp.Visible = true;
            Microsoft.Office.Interop.Excel._Workbook workbook = (Microsoft.Office.Interop.Excel._Workbook)(excelapp.Workbooks.Add(Type.Missing));
            Microsoft.Office.Interop.Excel._Worksheet worksheet = (Microsoft.Office.Interop.Excel._Worksheet)workbook.ActiveSheet;
            worksheet.Name = "Договор_купли_продажи";

            Microsoft.Office.Interop.Excel.Range hRange = excelapp.get_Range("A1:H1");
            hRange.Merge(Type.Missing);
            hRange.Merge(Type.Missing);
            worksheet.Cells[1, 1] = "Д О Г О В О Р А ";

            Microsoft.Office.Interop.Excel.Range ThisRange = excelapp.get_Range("A3:H3");
            ThisRange.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            var items = db.ContractOfSale.Where(p => p.isDeleted == false).ToList();
            worksheet.Cells[3, 1] = "Дата начала договора";
            worksheet.Cells[3, 2] = "Кадастровый номер";
            worksheet.Cells[3, 3] = "Адрес участка";
            worksheet.Cells[3, 4] = "Площадь";
            worksheet.Cells[3, 5] = "Застроенная площадь";
            worksheet.Cells[3, 6] = "Собственник";
            worksheet.Cells[3, 7] = "Телефон";
            worksheet.Cells[3, 8] = "Стоимость";

            Microsoft.Office.Interop.Excel.Range TRange = excelapp.get_Range("A4:H15");
            TRange.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;

            int i = 4;
            foreach (var elem in items)
            {
                worksheet.Cells[i, 1] = elem.data;
                worksheet.Cells[i, 2] = elem.LandPlot.cadastralNumber;
                worksheet.Cells[i, 3] = elem.LandPlot.adress;
                worksheet.Cells[i, 4] = elem.LandPlot.square;
                worksheet.Cells[i, 5] = elem.LandPlot.builtSquare;
                worksheet.Cells[i, 6] = elem.Owner.FIO;
                worksheet.Cells[i, 7] = elem.Owner.phone;
                worksheet.Cells[i, 8] = elem.sumCost;
                i++;
            }

            worksheet.Columns.AutoFit();
        }
        public void UpdateData(object sender, object e)
        {
            String text = txtSearch.Text.ToLower();
            mainList.ItemsSource = db.ContractOfSale.Where(x => x.LandPlot.adress.ToLower().StartsWith(text)
            || x.LandPlot.cadastralNumber.ToLower().StartsWith(text) || x.Owner.name.ToLower().StartsWith(text) || x.Owner.surname.ToLower().StartsWith(text) 
            || x.Owner.patronomic.ToLower().StartsWith(text) || x.Owner.phone.ToLower().StartsWith(text)).ToList();
        }
        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += UpdateData;
            timer.Start();
        }
    }
}
