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

namespace MasterPolGerasimova
{
    /// <summary>
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {
        private Partners _currentPartners = new Partners();

        public AddEditPage(Partners SelectPartners)
        {
            InitializeComponent();
            if(SelectPartners != null)
            {
                _currentPartners = SelectPartners;
            }
            // Загружаем типы партнеров в ComboBox
            PartnerTypeComboBox.ItemsSource = MasterPol_Entities.GetContext().Pertner_type.ToList();

            DataContext = _currentPartners;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            // Проверка обязательных полей
            if (string.IsNullOrWhiteSpace(_currentPartners.company_name))
                errors.AppendLine("Укажите название компании");
            if (string.IsNullOrWhiteSpace(_currentPartners.address))
                errors.AppendLine("Укажите адрес");
            if (_currentPartners.partner_type_id == 0)
                errors.AppendLine("Выберите тип партнера");
            if (_currentPartners.rating < 0 )
                errors.AppendLine("Рейтинг должен быть неотрицательным числом");
            if ( _currentPartners.rating > 10)
                errors.AppendLine("Рейтинг не может быть больше 10");
            if (string.IsNullOrWhiteSpace(_currentPartners.inn) || _currentPartners.inn.Length != 12 || !_currentPartners.inn.All(char.IsDigit))
                errors.AppendLine("ИНН должен содержать 12 цифр");
            if (string.IsNullOrWhiteSpace(_currentPartners.phone))
            {
                errors.AppendLine("Укажите телефон");
            }
            else
            {
                // Убираем все пробелы из телефона и проверяем длину
                string cleanPhone = _currentPartners.phone.Replace(" ", "");
                if (cleanPhone.Length != 10 || !cleanPhone.All(char.IsDigit))
                {
                    errors.AppendLine("Телефон должен содержать 10 цифр");
                }
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentPartners.partner_id == 0)
                MasterPol_Entities.GetContext().Partners.Add(_currentPartners);

            try
            {
                MasterPol_Entities.GetContext().SaveChanges();
                MessageBox.Show("информация сохранена");
                Manager.MainFrame.GoBack();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message.ToString());
            }


        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            
            if (!char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }
    }
}
