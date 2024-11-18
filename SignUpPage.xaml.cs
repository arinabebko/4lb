using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для SignUpPage.xaml
    /// </summary>
    /// 

  
    public partial class SignUpPage : Page
    {



        private Service _currentService = new Service();
        public SignUpPage(Service SelectedService)
        {
            InitializeComponent();
            if (SelectedService != null)
                this._currentService = SelectedService;

            DataContext = _currentService;

            var _currentClient = BebkoAutoServiceEntities.GetContext().Client.ToList();
            ComboClient.ItemsSource= _currentClient;    

        }



     

      

        private ClientService _currentClientService = new ClientService();


        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (ComboClient.SelectedItem == null)
                errors.AppendLine("Укажите ФИО клиента");

            if (StartDate.Text == "")
                errors.AppendLine("Укажите дату услуги");

            if (TBStart.Text=="")
                errors.AppendLine("Укажите время начала уcлуги");

            string pattern = @"^(0[0-9]|1[0-9]|2[0-3]):([0-5][0-9])$";
            Regex regex = new Regex(pattern);

            if (!regex.IsMatch(TBStart.Text))
            {
                errors.AppendLine("Время должно быть указано в формате hh:mm, где hh - часы (00-23), mm - минуты (00-59)");
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }


            _currentClientService.ClientID = ComboClient.SelectedIndex + 1;
            _currentClientService.ServiceID = _currentService.ID;
            _currentClientService.StartTime=Convert.ToDateTime(StartDate.Text+" "+TBStart.Text);

            if (_currentClientService.ID == 0)
                BebkoAutoServiceEntities.GetContext().ClientService.Add(_currentClientService);


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

        private void TBStart_TextChanged(object sender, TextChangedEventArgs e)
        {
            string s = TBStart.Text;




            string pattern = @"^(0[0-9]|1[0-9]|2[0-3]):([0-5][0-9])$";
            Regex regex = new Regex(pattern);

          //  if (!regex.IsMatch(TBStart.Text))
          //  {
              //("Время должно быть указано в формате hh:mm, где hh - часы (00-23), mm - минуты (00-59)");
            //}




            if (s.Length <= 3 || !s.Contains(':') || !regex.IsMatch(TBStart.Text))
            {
                TBEnd.Text = "";

            }
            else

            {
                
                    string[] start = s.Split(new char[] { ':' });

                    int startHour = Convert.ToInt32(start[0].ToString()) * 60;
                    int starMin = Convert.ToInt32(start[1].ToString());

                    int sum = startHour + starMin + _currentService.DurationInSeconds;


                    int EndHour = sum / 60;
                    int EndMin = sum % 60;
                    s = EndHour.ToString() + ":" + EndMin.ToString();
                    TBEnd.Text = s;

                
            }



        }
    }
}
