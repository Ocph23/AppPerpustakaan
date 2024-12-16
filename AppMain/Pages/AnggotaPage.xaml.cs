using AppMain.Models;
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



        private void TextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            var vm = DataContext as AnggotaPageViewModel;
            var textBox = (System.Windows.Controls.TextBox)sender;
            if (!string.IsNullOrEmpty(textBox.Text))
            {
                vm.TextSearch = textBox.Text;
            }
        }

        private void addCommand(object sender, RoutedEventArgs e)
        {
            var form = new AddAnggotaForm();
            form.ShowDialog();
            var vm = form.DataContext as AddAnggotaFormViewModel;
            if (vm.Model.Id > 0) { 
                var anggotaVM = DataContext as AnggotaPageViewModel;
                anggotaVM._Items.Add(vm.Model);
                anggotaVM.Items.Refresh();
            }
        }
    }


    public class AnggotaPageViewModel : ObservableObject
    {
        public CollectionView Items { get; set; }

        public ObservableCollection<Anggota> _Items { get; set; } = new ObservableCollection<Anggota>();

        public ICommand DetailComman { get; set; }

        public ICommand EditCommand { get; set; }
        public ICommand PrintCommand { get; set; }

        public AnggotaPageViewModel()
        {
            Items = (CollectionView)CollectionViewSource.GetDefaultView(_Items);
            Items.Filter += filterX;
            DetailComman = new RelayCommand<Anggota>(DetailCommandAction);
            EditCommand = new RelayCommand<Anggota>(EditCommandAction);
            PrintCommand = new RelayCommand<Anggota>(PritCommandAction);
            LoadData();
        }

        private void PritCommandAction(Anggota? anggota)
        {
            if (SelectedItem != null)
            {
                var window = new PrintKartuPelajarPage();
                window.ShowDialog();

            }
        }

        private void EditCommandAction(Anggota? anggota)
        {
            if (SelectedItem != null)
            {
               
                var window = new AddAnggotaForm(SelectedItem);
                window.ShowDialog();

                var vm = window.DataContext as AddAnggotaFormViewModel;
                //SelectedItem.Photo = vm.Model.Photo;

            }
        }

        private void DetailCommandAction(Anggota? anggota)
        {
            if (SelectedItem != null)
            {
               var window  = new DetailAnggotaPage(SelectedItem);
                window.ShowDialog();

                var vm = window.DataContext as DetailAnggotaViewModel;
                SelectedItem.Photo = vm.Model.Photo;

            }
        }


        private bool filterX(object obj)
        {
            var data = (Anggota)obj;
            if (string.IsNullOrEmpty(TextSearch))
                return true;

            if (data != null && !string.IsNullOrEmpty(TextSearch) && 
                (data.Nama.ToLower().Contains(TextSearch.ToLower()) 
                || data.NomorKartu.ToLower().Contains(TextSearch.ToLower()) 
                || (!string.IsNullOrEmpty(data.NIK) && data.NIK.ToLower().Contains(TextSearch.ToLower()))))
            {
                return true;
            }
            return false;
        }


        private async void LoadData()
        {
            await using var context = new ApplicationDbContext();
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
            set { SetProperty(ref selectedItem, value); }
        }


    }

}
