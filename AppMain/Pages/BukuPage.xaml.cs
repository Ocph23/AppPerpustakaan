using AppMain.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
    /// Interaction logic for BukuPage.xaml
    /// </summary>
    public partial class BukuPage : Page
    {
        public BukuPage()
        {
            InitializeComponent();
            DataContext = new BukuPageViewModel();
        }



        private void TextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            var vm = DataContext as BukuPageViewModel;
            var textBox = (System.Windows.Controls.TextBox)sender;
            if (!string.IsNullOrEmpty(textBox.Text))
            {
                vm.TextSearch = textBox.Text;
            }
        }

        private void addCommand(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddBukuPage());
        }
    }


    public class BukuPageViewModel : ObservableObject
    {
        public CollectionView Items { get; set; }

        public ObservableCollection<Buku> _Items { get; set; } = new ObservableCollection<Buku>();

        public ICommand DetailComman { get; set; }

        public ICommand EditCommand { get; set; }

        public BukuPageViewModel()
        {
            Items = (CollectionView)CollectionViewSource.GetDefaultView(_Items);
            Items.Filter += filterX;
            DetailComman = new RelayCommand<Buku>(DetailCommandAction);
            EditCommand = new RelayCommand<Buku>(EditCommandAction);
            LoadData();
        }

        private void EditCommandAction(Buku? anggota)
        {
            if (SelectedItem != null)
            {
                var window = new AddBukuPage(SelectedItem);
                Navigator.Navigate(window);

            }
        }

        private void DetailCommandAction(Buku? anggota)
        {
        }


        private bool filterX(object obj)
        {
            var data = (Buku)obj;
            if (string.IsNullOrEmpty(TextSearch))
                return true;

            if (data != null && !string.IsNullOrEmpty(TextSearch) &&
                (data.Judul.ToLower().Contains(TextSearch.ToLower())
                || data.Kode.ToLower().Contains(TextSearch.ToLower())
                || data.ISBN.ToLower().Contains(TextSearch.ToLower())
                || data.Tahun.ToString().Contains(TextSearch.ToLower())
                || data.Pengarang.Nama.Contains(TextSearch.ToLower())
                || data.Penerbit.Nama.Contains(TextSearch.ToLower())
                || data.Rak.Kode.Contains(TextSearch.ToLower())
                || data.Rak.Lokasi.Contains(TextSearch.ToLower())
                ))
            {
                return true;
            }
            return false;
        }


        private async void LoadData()
        {
            await using var context = new ApplicationDbContext();
            var datas = context.Bukus.Include(x=>x.Pengarang).Include(x=>x.Penerbit).Include(x=>x.Rak);
            foreach (var item in datas)
            {
                _Items.Add(item);
            }
            Items.Refresh();
        }

        private string textSearch;
        public string TextSearch
        {
            get { return textSearch; }
            set
            {
                SetProperty(ref textSearch, value);
                Items.Refresh();
            }
        }



        private Buku selectedItem;

        public Buku SelectedItem
        {
            get { return selectedItem; }
            set { SetProperty(ref selectedItem, value); }
        }


    }

}
