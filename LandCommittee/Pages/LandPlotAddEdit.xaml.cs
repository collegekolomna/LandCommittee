using LandCommittee.AppData;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace LandCommittee.Pages
{
    /// <summary>
    /// Логика взаимодействия для LandPlotAddEdit.xaml
    /// </summary>
    public partial class LandPlotAddEdit : Window
    {
        private byte[] newByteImage;
        string pathImage;
        string PathImage
        {
            get
            {
                return pathImage;
            }
            set
            {
                pathImage = value;
            }
        }
        string sPathImage
        {
            get
            {
                return pathImage.Substring(1);
            }
        }
        LandCommittee_Entities db = new LandCommittee_Entities();
        int Status, landPlotID;
        public LandPlotAddEdit(LandPlot landPlot, int status)
        {
            InitializeComponent();
            AddCategoryLand();
            Status = status;
            if (Status == 1)
            {
                landPlotID = landPlot.id;
                txtSquare.Text = Convert.ToString(landPlot.square);
                txtBuilt.Text = Convert.ToString(landPlot.builtSquare);
                txtNumber.Text = landPlot.cadastralNumber;
                txtCost.Text = Convert.ToString(landPlot.cost);
                txtAdress.Text = landPlot.adress;
                Category.SelectedValue = landPlot.idCategoryLand;
                if (Convert.ToString(landPlot.photo) != "")
                {
                    byte[] data = db.LandPlot.Find(landPlot.id).photo;
                    MemoryStream ms = new MemoryStream(data);
                    BitmapImage image = new BitmapImage();
                    image.BeginInit();
                    image.StreamSource = ms;
                    image.EndInit();
                    PhotoImageBox.Source = image;
                }
            }
            else
            {

            }
        }

        public void AddCategoryLand()
        {
            InitializeComponent();
            var result = from CategoryLand in db.CategoryLand
                         select new
                         {
                             id = CategoryLand.id,
                             name = CategoryLand.categoryLand1
                         };
            Category.ItemsSource = result.ToList();
            Category.SelectedValuePath = "id";
            Category.DisplayMemberPath = "name";
        }

        public void Image(LandPlot land)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            LandPlot l = db.LandPlot.Find(land.id);
            if (ofd.FileName != "")
            {

                using (System.IO.FileStream fs = new System.IO.FileStream(ofd.FileName, FileMode.Open))
                {
                    newByteImage = new byte[fs.Length];
                    fs.Read(newByteImage, 0, newByteImage.Length);
                }
                l.photo = newByteImage;
            }
            try
            {
                byte[] data = db.LandPlot.Find(land.id).photo;
                MemoryStream ms = new MemoryStream(data);
                //PhotoImageBox.Source = Image.FromStream(ms);
                //pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = ms;
                image.EndInit();
                PhotoImageBox.Source = image;
            }
            catch (Exception)
            {


            }
        }
        private void ImageSave_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            LandPlot l = db.LandPlot.Find(landPlotID);
            if (ofd.FileName != "")
            {

                using (System.IO.FileStream fs = new System.IO.FileStream(ofd.FileName, FileMode.Open))
                {
                    newByteImage = new byte[fs.Length];
                    fs.Read(newByteImage, 0, newByteImage.Length);
                }
                l.photo = newByteImage;
            }
            try
            {
                byte[] data = db.LandPlot.Find(landPlotID).photo;
                MemoryStream ms = new MemoryStream(data);
                //PhotoImageBox.Source = Image.FromStream(ms);
                //pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = ms;
                image.EndInit();
                PhotoImageBox.Source = image;
            }
            catch (Exception)
            {


            }
        }        

        private void txtCost_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "0123456789 ,".IndexOf(e.Text) < 0;
        }

        private void txtSquare_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "0123456789 ,".IndexOf(e.Text) < 0;
        }

        private void txtBuilt_PreviewTextInput(object sender, TextCompositionEventArgs e)
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
                    var result = db.LandPlot.Where(s => s.id == landPlotID).FirstOrDefault();
                    //Изменение данных найденной услуги
                    result.square = float.Parse(txtSquare.Text);
                    result.builtSquare = float.Parse(txtBuilt.Text);
                    result.cadastralNumber = txtNumber.Text;
                    result.cost = float.Parse(txtSquare.Text);
                    result.adress = txtAdress.Text;
                    result.idCategoryLand = (int)Category.SelectedValue;
                   // result.photo = Convert.ToString(PhotoTextBox.Content);
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
                    string Photo = "";
                    if (Convert.ToString(PhotoTextBox.Content) == "")
                    {
                        Photo = "";
                    }
                    else
                    {
                        Photo = Convert.ToString(PhotoTextBox.Content);
                    }
                    //Создаем новую запись
                    LandPlot landPlot = new LandPlot
                    {
                        square = float.Parse(txtSquare.Text),
                        builtSquare = float.Parse(txtBuilt.Text),
                        cadastralNumber = txtNumber.Text,
                        cost = float.Parse(txtSquare.Text),
                        adress = txtAdress.Text,
                        idCategoryLand = (int)Category.SelectedValue,
                       // photo = Photo,
                        isDeleted = false
                    };
                    //Сохраняем ее
                    db.LandPlot.Add(landPlot);
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
