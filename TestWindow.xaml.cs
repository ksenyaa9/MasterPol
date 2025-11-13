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

namespace MasterPolGerasimova
{
    public partial class TestWindow : Window
    {
        public TestWindow()
        {
            InitializeComponent();
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            // Проверяем и парсим каждое поле отдельно с обработкой ошибок
            if (!int.TryParse(ProductTypeTextBox.Text, out int productTypeId))
            {
                MessageBox.Show("Пожалуйста, введите корректное целое число для типа продукции.");
                ProductTypeTextBox.Focus();
                return;
            }

            if (!int.TryParse(MaterialTypeTextBox.Text, out int materialTypeId))
            {
                MessageBox.Show("Пожалуйста, введите корректное целое число для типа материала.");
                MaterialTypeTextBox.Focus();
                return;
            }

            if (!int.TryParse(ProductCountTextBox.Text, out int productCount))
            {
                MessageBox.Show("Пожалуйста, введите корректное целое число для количества продукции.");
                ProductCountTextBox.Focus();
                return;
            }

            // Для double используем более гибкий парсинг с учетом региональных настроек
            if (!double.TryParse(Param1TextBox.Text.Replace(',', '.'),
                                System.Globalization.NumberStyles.Any,
                                System.Globalization.CultureInfo.InvariantCulture,
                                out double param1))
            {
                MessageBox.Show("Пожалуйста, введите корректное число для параметра 1.\nИспользуйте точку или запятую как разделитель.");
                Param1TextBox.Focus();
                return;
            }

            if (!double.TryParse(Param2TextBox.Text.Replace(',', '.'),
                                System.Globalization.NumberStyles.Any,
                                System.Globalization.CultureInfo.InvariantCulture,
                                out double param2))
            {
                MessageBox.Show("Пожалуйста, введите корректное число для параметра 2.\nИспользуйте точку или запятую как разделитель.");
                Param2TextBox.Focus();
                return;
            }

            // Выполняем расчет
            int result = MaterialCalculator.CalculateRequiredMaterial(
                productTypeId, materialTypeId, productCount, param1, param2);

            if (result != -1)
            {
                ResultTextBlock.Text = $"Требуется материала: {result} единиц";
            }
            else
            {
                ResultTextBlock.Text = "Ошибка в расчетах! Проверьте входные данные.";
            }
        }


        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
