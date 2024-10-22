using AppMain.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace AppMain.Pages
{
    /// <summary>
    /// Interaction logic for PenerbitPage.xaml
    /// </summary>
    public partial class PenerbitPage : Page
    {
        public PenerbitPage()
        {
            InitializeComponent();
            DataContext = new PenerbitPageViewModel();
        }



        private void TextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            var vm = DataContext as PenerbitPageViewModel;
            var textBox = (System.Windows.Controls.TextBox)sender;
            if (!string.IsNullOrEmpty(textBox.Text))
            {
                vm.TextSearch = textBox.Text;
            }
        }

        private void addCommand(object sender, RoutedEventArgs e)
        {
            var form = new AddPenerbitForm();
            form.ShowDialog();
        }
    }


    public class PenerbitPageViewModel : ObservableObject
    {
        public CollectionView Items { get; set; }

        public ObservableCollection<Penerbit> _Items { get; set; } = new ObservableCollection<Penerbit>();

        public ICommand DetailComman { get; set; }

        public ICommand EditCommand { get; set; }

        public PenerbitPageViewModel()
        {
            Items = (CollectionView)CollectionViewSource.GetDefaultView(_Items);
            Items.Filter += filterX;
            DetailComman = new RelayCommand<Penerbit>(DetailCommandAction);
            EditCommand = new RelayCommand<Penerbit>(EditCommandAction);
            LoadData();
        }

        private void EditCommandAction(Penerbit? anggota)
        {
            if (SelectedItem != null)
            {
                var window = new AddPenerbitForm(SelectedItem);
                window.ShowDialog();
            }
        }

        private void DetailCommandAction(Penerbit? anggota)
        {
        }


        private bool filterX(object obj)
        {
            var data = (Penerbit)obj;
            if (string.IsNullOrEmpty(TextSearch))
                return true;

            if (data != null && !string.IsNullOrEmpty(TextSearch) &&
                (data.Nama.ToLower().Contains(TextSearch.ToLower())
                ))
            {
                return true;
            }
            return false;
        }


        private async void LoadData()
        {
            await using var context = new ApplicationDbContext();
            var datas = context.Penerbits.ToList();
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



        private Penerbit selectedItem;

        public Penerbit SelectedItem
        {
            get { return selectedItem; }
            set { SetProperty(ref selectedItem, value); }
        }


    }

}
