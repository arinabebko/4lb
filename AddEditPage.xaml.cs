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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Bebko_Autoservice
{
    /// <summary>
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {

        private Service _currentServise = new Service();
        private int a;

        public AddEditPage(Service SelectedService)
        {
            InitializeComponent();

            if(SelectedService != null)
            {
                _currentServise = SelectedService;
                  a = 1;
            }
            else { a = 0; }

            DataContext = _currentServise;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentServise.Title))
                errors.AppendLine("Укажите название услуги");

            if (_currentServise.Cost <= 0)
                errors.AppendLine("Укажите стоимость услуги");

            //string.IsNullOrWhiteSpace(_currentServise.Discount.ToString()))
            //if (_currentServise.Discount<0 || _currentServise.Discount >100 || _currentServise.Discount.text.Length==0)
            //    errors.AppendLine("Укажите скидку");

            //string.IsNullOrWhiteSpace(_currentServise.Discount.ToString()))
            //if (_currentServise.Discount < 0 || _currentServise.Discount > 100 || string.IsNullOrWhiteSpace(_currentServise.Discount.ToString()))
            if (_currentServise.DiscountInt < 0 || _currentServise.DiscountInt > 100 || string.IsNullOrWhiteSpace(_currentServise.DiscountInt.ToString()))
            {
               
                errors.AppendLine("Укажите скидку от 0 до 100");

            }



          //  if (_currentServise.DurationInSeconds == null || _currentServise.DurationInSeconds <= 0)
           // {
          //      errors.AppendLine("Укажите длительность услуги");
          //  }
            if (_currentServise.DurationInSeconds <= 0)
            {
                errors.AppendLine("Укажите длительность услуги");
            }
            if (_currentServise.DurationInSeconds >240 && a==0) 
            {
                errors.AppendLine("Длительность не может быть больше 240 минут");
            }




            var allServices = BebkoAutoServiceEntities.GetContext().Service.ToList();
            allServices = allServices.Where(propa => propa.Title == _currentServise.Title).ToList();




          


            if (allServices.Count == 0 || a == 1)
            {
                if (_currentServise.ID == 0 )
                 BebkoAutoServiceEntities.GetContext().Service.Add(_currentServise); 
                    try
                    {
                        BebkoAutoServiceEntities.GetContext().SaveChanges();
                        MessageBox.Show("информация сохранена");
                        Manager.MainFrame.GoBack();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
            }
            else
            {
                MessageBox.Show("уже существует такая услуга");
            }

            // if (string.IsNullOrWhiteSpace(_currentServise.DurationInSeconds))
            //    errors.AppendLine("Укажите длительность услуги");


            // if (string.IsNullOrWhiteSpace(_currentServise.DurationInSeconds))
            //    errors.AppendLine("Укажите длительность услуги");


            if (errors.Length>0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

           




        }
    }
}
