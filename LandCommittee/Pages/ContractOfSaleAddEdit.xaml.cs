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
    /// Логика взаимодействия для ContractOfSaleAddEdit.xaml
    /// </summary>
    public partial class ContractOfSaleAddEdit : Window
    {
        LandCommittee_Entities db = new LandCommittee_Entities();
        int Status, contractID;
        public ContractOfSaleAddEdit(ContractOfSale contractOfSales, int status)
        {
            InitializeComponent();
            AddOwner();
            AddCulc();
            Status = status;
            if (Status == 1)
            {
                var cadast = from LandPlot in db.LandPlot
                             where LandPlot.isDeleted == false
                             select new
                             {
                                 id = LandPlot.id,
                                 number = LandPlot.cadastralNumber
                             };
                txtCadNumber.ItemsSource = cadast.ToList();
                txtCadNumber.SelectedValuePath = "id";
                txtCadNumber.DisplayMemberPath = "number";

                contractID = contractOfSales.id;
                txtCadNumber.SelectedValue = contractOfSales.idLandPlot;
                txtOwner.SelectedValue = contractOfSales.idOwner;
                txtData.SelectedDate = contractOfSales.data;               
                txtUsing.Text = contractOfSales.actualUse;                
                txtCulc.SelectedValue = contractOfSales.idCalculation;
            }
            else
            {
                AddNumber();
            }
        }

        public void AddNumber()
        {
            InitializeComponent();
                var cadast = from LandPlot in db.LandPlot
                                         where LandPlot.isDeleted == false && LandPlot.status == false
                                         select new
                                         {
                                             id = LandPlot.id,
                                             number = LandPlot.cadastralNumber
                                         };
            txtCadNumber.ItemsSource = cadast.ToList();
            txtCadNumber.SelectedValuePath = "id";
            txtCadNumber.DisplayMemberPath = "number";
            
        }
        public void AddOwner()
        {
            InitializeComponent();
            var owner = from Owner in db.Owner
                        where Owner.isDeleted == false
                        select new
                        {
                            id = Owner.id,
                            FIO = Owner.surname +" "+ Owner.name+ " " + Owner.patronomic
                        };
            txtOwner.ItemsSource = owner.ToList();
            txtOwner.SelectedValuePath = "id";
            txtOwner.DisplayMemberPath = "FIO";
        }
        public void AddCulc()
        {
            InitializeComponent();
            var culc = from Culculation in db.Culculation
                       where Culculation.isDeleted == false
                       select new
                       {
                           id = Culculation.id,
                           description = Culculation.description
                       };
            txtCulc.ItemsSource = culc.ToList();
            txtCulc.SelectedValuePath = "id";
            txtCulc.DisplayMemberPath = "description";
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (Status == 1)
            {
                try
                {
                    float S = db.LandPlot.Where(s => s.id == (int)txtCadNumber.SelectedValue).FirstOrDefault().square;
                    float bS = db.LandPlot.Where(b => b.id == (int)txtCadNumber.SelectedValue).FirstOrDefault().builtSquare;
                    float p = db.Culculation.Where(q => q.id == (int)txtCulc.SelectedValue).FirstOrDefault().percentage;

                    //Поиск по ID
                    var result = db.ContractOfSale.Where(s => s.id == contractID).FirstOrDefault();
                    //Изменение данных найденной услуги
                    result.idLandPlot = (int)txtCadNumber.SelectedValue;
                    result.idOwner = (int)txtOwner.SelectedValue;
                    result.data = (DateTime)txtData.SelectedDate;
                    result.actualUse = txtUsing.Text;
                    result.idCalculation = (int)txtCulc.SelectedValue;
                    result.sumCost = 2500 * (S + bS) * p;

                    var land = db.LandPlot.Where(l => l.id == (int)txtCadNumber.SelectedValue).FirstOrDefault();
                    land.status = true;
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
                    float S = db.LandPlot.Where(s => s.id == (int)txtCadNumber.SelectedValue).FirstOrDefault().square;
                    float bS = db.LandPlot.Where(b => b.id == (int)txtCadNumber.SelectedValue).FirstOrDefault().builtSquare;
                    float p = db.Culculation.Where(q => q.id == (int)txtCulc.SelectedValue).FirstOrDefault().percentage;
                    //Создаем новую запись
                    ContractOfSale contractOfSale = new ContractOfSale
                    {
                        idLandPlot = (int)txtCadNumber.SelectedValue,
                        idOwner = (int)txtOwner.SelectedValue,
                        data = (DateTime)txtData.SelectedDate,
                        actualUse = txtUsing.Text,
                        idCalculation = (int)txtCulc.SelectedValue,
                        sumCost = 2500 * (S + bS) * p,
                        isDeleted = false
                    };
                    //Сохраняем ее
                    db.ContractOfSale.Add(contractOfSale);

                    var land = db.LandPlot.Where(l => l.id == (int)txtCadNumber.SelectedValue).FirstOrDefault();
                    land.status = true;

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
