using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Configuration;
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

namespace AppMain
{
    /// <summary>
    /// Interaction logic for SettingPage.xaml
    /// </summary>
    public partial class SettingPage : Window
    {
        public SettingPage()
        {
            InitializeComponent();
            DataContext = new SettingPageViewModel();
        }

        private void btnBatal(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnTambah(object sender, RoutedEventArgs e)
        {

        }
    }

    internal class SettingPageViewModel : ObservableObject
    {

        public RelayCommand SaveCommand { get; }

        public SettingPageViewModel()
        {
            SaveCommand = new RelayCommand(() =>
            {
                Properties.Settings.Default.Save();
                System.Windows.MessageBox.Show("Data berhasil disimpan !");
            });

        }
    }
}
