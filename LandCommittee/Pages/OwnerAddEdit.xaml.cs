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
using LandCommittee.AppData;

namespace LandCommittee.Pages
{
    /// <summary>
    /// Логика взаимодействия для OwnerAddEdit.xaml
    /// </summary>
    public partial class OwnerAddEdit : Window
    {
        LandCommittee_Entities db = new LandCommittee_Entities();
        int Status, ownerID;
        public OwnerAddEdit(Owner owner, int status)
        {
            InitializeComponent();
            AddCategoryOwner();
            Status = status;
            if (Status == 1)
            {
                ownerID = owner.id;
                txtS.Text = owner.surname;
                txtN.Text = owner.name;
                txtP.Text = owner.patronomic;
                txtPassport.Text = owner.passport;
                txtINN.Text = owner.INN.ToString();
                txtAdress.Text = owner.adress;
                txtPhone.Text = owner.phone;
                Category.SelectedValue = owner.idCategory;
            }
            else
            {

            }
        }

        public void AddCategoryOwner()
        {
            InitializeComponent();
            var result = from categoryOwner in db.categoryOwner
                         select new
                         {
                             id = categoryOwner.id,
                             name = categoryOwner.categoryOwner1
                         };
            Category.ItemsSource = result.ToList();
            Category.SelectedValuePath = "id";
            Category.DisplayMemberPath = "name";
        }

        private void txtINN_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "0123456789 ,".IndexOf(e.Text) < 0;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (Status == 1)
            {
                try
                {
                    //Поиск по ID
                    var result = db.Owner.Where(s => s.id == ownerID).FirstOrDefault();
                    //Изменение данных найденной услуги
                    result.surname = txtS.Text;
                    result.name = txtN.Text;
                    result.patronomic = txtP.Text;
                    result.passport = txtPassport.Text;
                    result.INN = Convert.ToInt64(txtINN.Text);
                    result.adress = txtAdress.Text;
                    result.phone = txtPhone.Text;
                    result.idCategory = (int)Category.SelectedValue;
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
                    Owner owner = new Owner
                    {
                        surname = txtS.Text,
                        name = txtN.Text,
                        patronomic = txtP.Text,
                        passport = txtPassport.Text,
                        INN = Convert.ToInt64(txtINN.Text),
                        adress = txtAdress.Text,
                        phone = txtPhone.Text,
                        idCategory = (int)Category.SelectedValue
                };
                    //Сохраняем ее
                    db.Owner.Add(owner);
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
