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
    /// Логика взаимодействия для LeaseContractAddEdit.xaml
    /// </summary>
    public partial class LeaseContractAddEdit : Window
    {
        LandCommittee_Entities db = new LandCommittee_Entities();
        int Status, contractID;
        public LeaseContractAddEdit(LeaseContract leaseContract, int status)
        {
            InitializeComponent();
            AddOwner();         
            AddCulc();
            Status = status;
            if (Status == 1)
            {
                var number = from LandPlot in db.LandPlot
                             where LandPlot.isDeleted == false
                             select new
                             {
                                 id = LandPlot.id,
                                 number = LandPlot.cadastralNumber
                             };
                txtCadNumber.ItemsSource = number.ToList();
                txtCadNumber.SelectedValuePath = "id";
                txtCadNumber.DisplayMemberPath = "number";

                contractID = leaseContract.id;
                txtCadNumber.SelectedValue = leaseContract.idLandPlot;
                txtTenat.SelectedValue = leaseContract.idTenat;
                txtStart.SelectedDate = leaseContract.startContract;
                txtEnd.SelectedDate = leaseContract.endContract;
                txtUsing.Text = leaseContract.actualUse;
                txtFixing.Text = leaseContract.fixing;
                txtCulc.SelectedValue = leaseContract.idCalculation;
                txtBap.Text = leaseContract.BAP.ToString();
            }
            else
            {
                AddNumber();
            }
        }
        public void AddNumber()
        {
            InitializeComponent();
            var number = from LandPlot in db.LandPlot
                         where LandPlot.isDeleted == false && LandPlot.status == false
                         select new
                         {
                             id = LandPlot.id,
                             number = LandPlot.cadastralNumber
                         };
            txtCadNumber.ItemsSource = number.ToList();
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
                            FIO = Owner.surname + " " + Owner.name+ " " + Owner.patronomic
                        };
            txtTenat.ItemsSource = owner.ToList();
            txtTenat.SelectedValuePath = "id";
            txtTenat.DisplayMemberPath = "FIO";
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
                    var result = db.LeaseContract.Where(s => s.id == contractID).FirstOrDefault();
                    //Изменение данных найденной услуги
                    result.idLandPlot = (int)txtCadNumber.SelectedValue;
                    result.idTenat = (int)txtTenat.SelectedValue;
                    result.startContract = (DateTime)txtStart.SelectedDate; ;
                    result.endContract = (DateTime)txtEnd.SelectedDate; ;
                    result.actualUse = txtUsing.Text;
                    result.fixing = txtFixing.Text;
                    result.idCalculation = (int)txtCulc.SelectedValue;
                    result.BAP = float.Parse(txtBap.Text);
                    result.sumCost = (float.Parse(txtBap.Text) * (S + bS) * p) / 12;

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
                    LeaseContract leaseContract = new LeaseContract
                    {
                        idLandPlot = (int)txtCadNumber.SelectedValue,
                        idTenat = (int)txtTenat.SelectedValue,
                        startContract = (DateTime)txtStart.SelectedDate,
                        endContract = (DateTime)txtEnd.SelectedDate,
                        actualUse = txtUsing.Text,
                        fixing = txtFixing.Text,
                        idCalculation = (int)txtCulc.SelectedValue,
                        BAP = float.Parse(txtBap.Text),
                        sumCost = (float.Parse(txtBap.Text) * (S + bS) * p)/12,
                        isDeleted = false
                };
                    //Сохраняем ее
                    db.LeaseContract.Add(leaseContract);

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

        private void txtBap_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "0123456789 ,".IndexOf(e.Text) < 0;
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
    }
}
