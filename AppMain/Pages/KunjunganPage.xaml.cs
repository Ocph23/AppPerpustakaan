using AppMain.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DocumentFormat.OpenXml.InkML;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace AppMain.Pages
{
    /// <summary>
    /// Interaction logic for KunjunganPage.xaml
    /// </summary>
    public partial class KunjunganPage : Page
    {
        public KunjunganPage()
        {
            InitializeComponent();
            DataContext = new KunjunganPageViewModel();
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var vm = DataContext as KunjunganPageViewModel;
            vm.LoadData();
        }
    }

    public class KunjunganPageViewModel : ObservableObject
    {
        public CollectionView Items { get; set; }

        public ObservableCollection<Kunjungan> _Items { get; set; } = new ObservableCollection<Kunjungan>();



        public KunjunganPageViewModel()
        {
            Items = (CollectionView)CollectionViewSource.GetDefaultView(_Items);
            
           _ =LoadData();
        }

        private bool filterX(object obj)
        {
            var data = (Kunjungan)obj;
            if (DateOnly.FromDateTime(data.Masuk) != DateOnly.FromDateTime(SelectedDate))  
            return false;
            return true;
        }


        public async Task LoadData()
        {
            using var Context = new ApplicationDbContext();
            _Items.Clear();
            foreach (var item in Context.Kunjungans.Where(x => DateOnly.FromDateTime(x.Masuk) == DateOnly.FromDateTime(SelectedDate)).Include(x => x.Anggota).ToList())
            {
                 _Items.Add(item);
            }
            Items.Refresh();
        }

        private DateTime selectedDate = DateTime.Now;
        public DateTime SelectedDate
        {
            get { return selectedDate; }
            set
            {
                SetProperty(ref selectedDate, value);
                Items.Refresh();
            }
        }



        private Kunjungan selectedItem;

        public Kunjungan SelectedItem
        {
            get { return selectedItem; }
            set { SetProperty(ref selectedItem, value); }
        }


    }

}
