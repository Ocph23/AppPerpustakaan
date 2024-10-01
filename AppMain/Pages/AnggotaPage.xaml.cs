using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
    /// Interaction logic for AnggotaPage.xaml
    /// </summary>
    public partial class AnggotaPage : Page
    {
        public AnggotaPage()
        {
            InitializeComponent();
            DataContext = new AnggotaPageViewModel();
        }
    }


    public class AnggotaPageViewModel : ObservableObject
    {
        public CollectionView Items { get; set; }

        public ObservableCollection<Anggota> _Items { get; set; } = new ObservableCollection<Anggota>();

        public ICommand DetailComman { get; set; }

        public AnggotaPageViewModel()
        {
            Items = (CollectionView)CollectionViewSource.GetDefaultView(_Items);
            Items.Filter += filterX;
            DetailComman = new RelayCommand<Anggota>(DetailCommandAction);
            LoadData();
        }

        private void DetailCommandAction(Anggota? anggota)
        {
            if (SelectedItem!=null)
            {
               Navigator.Navigate(new DetailAnggotaPage(SelectedItem));   
            }
        }


        private bool filterX(object obj)
        {
            var data = (Anggota)obj;
            if (data != null && !string.IsNullOrEmpty(TextSearch) && 
                data.Nama.ToLower().Contains(TextSearch.ToLower()))
            {
                return true;
            }
            return false;
        }


        private void LoadData()
        {
            using var context = new ApplicationDbContext();
            var datas = context.Anggotas.ToList();
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



        private Anggota selectedItem;

        public Anggota SelectedItem
        {
            get { return selectedItem; }
            set {SetProperty(ref selectedItem , value); }
        }


    }

}
