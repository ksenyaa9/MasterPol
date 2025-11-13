using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    public partial class PartnerSalesHistory : Page
    {
        private Partners _currentPartner;

        public PartnerSalesHistory(Partners partner)
        {
            InitializeComponent();
            _currentPartner = partner;
            LoadSalesHistory();
        }

        private void LoadSalesHistory()
        {
            using (var context = new MasterPol_Entities())
            {
                // Явно загружаем связанные данные о продуктах
                var salesHistory = context.Partner_Products
                    .Where(pp => pp.id_partner == _currentPartner.partner_id)
                    .Include("Products") // Добавляем загрузку связанных продуктов
                    .OrderByDescending(pp => pp.sales_data)
                    .ToList();

                SalesListView.ItemsSource = salesHistory;

                // Для отладки: выведем информацию о первом элементе
                if (salesHistory.Count > 0)
                {
                    var firstItem = salesHistory[0];
                    System.Diagnostics.Debug.WriteLine($"Product: {firstItem.Products?.product_name}, Quantity: {firstItem.quantity_products}, Date: {firstItem.sales_data}");
                }
            }

            // Устанавливаем заголовок
            PartnerInfo = $"{_currentPartner.company_name} - История продаж";
            DataContext = this; // Убедимся, что DataContext установлен
        }

        public string PartnerInfo { get; set; }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.GoBack();
        }
    }
}