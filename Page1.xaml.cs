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
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();

            var currentPage = MasterPol_Entities.GetContext().Partners.ToList();

            PageListView.ItemsSource = currentPage;
            UpdatePartners();
        }


        private void UpdatePartners()
        {
            var currentPartnets = MasterPol_Entities.GetContext().Partners.ToList();

            PageListView.ItemsSource = currentPartnets;
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                MasterPol_Entities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                PageListView.ItemsSource = MasterPol_Entities.GetContext().Partners.ToList();

            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage(null));
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage((sender as Button).DataContext as Partners));
        }

        private void SalesHistoryButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedPartner = (sender as Button).DataContext as Partners;
            Manager.MainFrame.Navigate(new PartnerSalesHistory(selectedPartner));
        }

        

        private void TestCalculation_Click(object sender, RoutedEventArgs e)
        {
            TestWindow testWindow = new TestWindow();
            testWindow.ShowDialog();
        }
    }
}
