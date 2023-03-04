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
using LopushokKekis.Classes;
using LopushokKekis.Model;

namespace LopushokKekis
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeComponent();
            RefreshPAgination();
            RefreshComboBox();
            Sortinfo();
            RefreshButtons();
            CmbSort.Items.Add("Сортировать");
            CmbFilter.ItemsSource = DBconnection.connect.ProductType.ToList();
            LvProduct.ItemsSource = DBconnection.connect.Product.ToList();
        }
        int pageNumber;
        private void RefreshPAgination()
        {
            LvProduct.ItemsSource = null;
            if (CmbSort.Text != null)
            {
                Sortinfo();
            }
            else
            {
                LvProduct.ItemsSource = DBconnection.connect.Product.Skip(pageNumber * 20).Take(20).ToList();
            }
        }
        private void RefreshComboBox()
        {
            CmbSort.Items.Add("От А-Я");
            CmbSort.Items.Add("От Я-А");
            CmbSort.Items.Add("Номер цеха по возростанию");
            CmbSort.Items.Add("Номер цеха по убыванию");

        }
        private void Sortinfo()
        {
            switch (CmbSort.SelectedItem)
            {
                case "От А-Я":
                    LvProduct.ItemsSource = null;
                    LvProduct.ItemsSource = DBconnection.connect.Product.OrderBy(x => x.Title).Skip(pageNumber * 20).Take(20).ToList();
                    break;
                case "От Я-А":
                    LvProduct.ItemsSource = null;
                    LvProduct.ItemsSource = DBconnection.connect.Product.OrderByDescending(x => x.Title).Skip(pageNumber * 20).Take(20).ToList();
                    break;
                case "Номер цеха по возростанию":
                    LvProduct.ItemsSource = null;
                    LvProduct.ItemsSource = DBconnection.connect.Product.OrderBy(x => x.ProductionWorkshopNumber).Skip(pageNumber * 20).Take(20).ToList();
                    break;
                case "Номер цеха по убыванию":
                    LvProduct.ItemsSource = null;
                    LvProduct.ItemsSource = DBconnection.connect.Product.OrderByDescending(x => x.ProductionWorkshopNumber).Skip(pageNumber * 10).Take(10).ToList();
                    break;
                default:
                    LvProduct.ItemsSource = null;
                    LvProduct.ItemsSource = DBconnection.connect.Product.ToList();
                    break;
            }
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            LvProduct.ItemsSource = DBconnection.connect.Product.Where(z => z.Title.Contains(TxtSearch.Text)).ToList();
        }

        private void BtnDelted_Click(object sender, RoutedEventArgs e)
        {

            var a = LvProduct.SelectedItem as Product;
            if (a != null)
            {
                DBconnection.connect.Product.Remove(a);

                DBconnection.connect.SaveChanges();
            }
            else { MessageBox.Show("такой записи нее"); }
            refresh();


        }
        public void refresh()
        {
            LvProduct.ItemsSource = DBconnection.connect.Product.ToList();
        }

        private void BtnRight_Click(object sender, RoutedEventArgs e)
        {
            if (DBconnection.connect.Product.ToList().Count % 10 == 0)
            {
                if (pageNumber == (DBconnection.connect.Product.ToList().Count / 10) - 1)
                    return;
            }
            else
            {
                if (pageNumber == (DBconnection.connect.Product.ToList().Count / 10))
                    return;
            }
            pageNumber++;
            RefreshPAgination();
        }

        private void BtnLeft_Click(object sender, RoutedEventArgs e)
        {
            if (pageNumber == 0)
                return;
            pageNumber--;
            RefreshPAgination();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            pageNumber = Convert.ToInt32(button.Content) - 1;
            RefreshPAgination();
        }
        private void RefreshButtons()
        {
            WPButtons.Children.Clear();
            if (DBconnection.connect.Product.ToList().Count % 10 == 0)
            {
                for (int i = 0; i < DBconnection.connect.Product.ToList().Count / 10; i++)
                {
                    Button button = new Button();
                    button.Content = i + 1;
                    button.Click += Button_Click;
                    button.Margin = new Thickness(5);
                    button.Width = 20;
                    button.Height = 20;
                    button.FontSize = 14;
                    WPButtons.Children.Add(button);
                }
            }
            else
            {
                for (int i = 0; i < DBconnection.connect.Product.ToList().Count / 10 + 1; i++)
                {
                    Button button = new Button();
                    button.Content = i + 1;
                    button.Click += Button_Click;
                    button.Margin = new Thickness(5);
                    button.Width = 20;
                    button.Height = 20;
                    button.FontSize = 14;
                    WPButtons.Children.Add(button);
                }
            }
        }

        private void CmbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Sortinfo();
        }

        private void CmbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var c = CmbFilter.SelectedItem as ProductType;
            LvProduct.ItemsSource = DBconnection.connect.Product.Where(z => z.ProductTypeID == c.ID).ToList();
        }
    }
}
