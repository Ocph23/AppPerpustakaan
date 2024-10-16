using AppMain.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AppMain.Pages
{
    /// <summary>
    /// Interaction logic for AddBukuPage.xaml
    /// </summary>
    public partial class AddBukuPage : Page
    {
        private AddBukuViewModel vm;

        public AddBukuPage()
        {
            InitializeComponent();
            DataContext = vm = new AddBukuViewModel();
        }

        public AddBukuPage(Buku selectedItem)
        {
            InitializeComponent();
            DataContext = vm = new AddBukuViewModel(selectedItem);
        }
    }

    internal class AddBukuViewModel : ObservableObject
    {

        private Buku model = new Buku();

        public Buku Model
        {
            get { return model; }
            set { SetProperty(ref model, value); }
        }

        public ObservableCollection<Penerbit> Penerbits { get; set; }
        public ObservableCollection<Pengarang> Pengarangs { get; set; }
        public ObservableCollection<Rak> Raks { get; set; }
        public ICommand CancelCommand { get; set; }
        private ICommand saveCommand;
        private Buku selectedItem;

        public ICommand SaveCommand
        {
            get { return saveCommand; }
            set { SetProperty(ref saveCommand, value); }
        }

        public ICommand AddPenerbitCommand { get; set; }
        public RelayCommand AddPengarangCommand { get; set; }
        public RelayCommand AddRakCommand { get; set; }

        public AddBukuViewModel()
        {
            Init();
            Model.PropertyChanging += Model_PropertyChanging;
        }

        private void Init()
        {
            AddPenerbitCommand = new RelayCommand(AddPenerbitCommandAction);
            AddPengarangCommand = new RelayCommand(AddPengarangCommandAction);
            AddRakCommand = new RelayCommand(AddRakCommandAction);
            CancelCommand = new RelayCommand(() => Model = new Buku());


            using var dbcontext = new ApplicationDbContext();
            Penerbits = new ObservableCollection<Penerbit>(dbcontext.Penerbits.ToList());
            Pengarangs = new ObservableCollection<Pengarang>(dbcontext.Pengarangs.ToList());
            Raks = new ObservableCollection<Rak>(dbcontext.Raks.ToList());
            SaveCommand = new RelayCommand<Buku>(SaveCommanAction, SaveCommandValidate);
        }

        public AddBukuViewModel(Buku selectedItem)
        {
            Init();
            this.Model = selectedItem;
            Model.PropertyChanging += Model_PropertyChanging;
        }

        private void Model_PropertyChanging(object? sender, System.ComponentModel.PropertyChangingEventArgs e)
        {
            SaveCommand = new RelayCommand<Buku>(SaveCommanAction, SaveCommandValidate);
        }

        private void SaveCommanAction(Buku? buku)
        {
            try
            {
                using var dbcontext = new ApplicationDbContext();
                if (Model.Id <= 0)
                {
                    dbcontext.Bukus.Add(Model);
                    dbcontext.Entry(Model.Penerbit).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
                    dbcontext.Entry(Model.Pengarang).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
                    dbcontext.Entry(Model.Rak).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
                    dbcontext.SaveChanges();
                    System.Windows.MessageBox.Show("Data Berhasil Disimpan !", "Info", MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                    Model = new Buku();
                }
                else
                {
                    var oldData = dbcontext.Bukus.Where(x=>x.Id==Model.Id).Include(x => x.Rak).Include(x => x.Penerbit).Include(x => x.Pengarang).FirstOrDefault();
                    dbcontext.Entry(oldData).CurrentValues.SetValues(Model);
                    dbcontext.SaveChanges();
                    System.Windows.MessageBox.Show("Data Berhasil Disimpan !", "Info", MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                    Navigator.GoBack();
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }

        private bool SaveCommandValidate(Buku? obj)
        {
            if (string.IsNullOrEmpty(Model.Kode) || string.IsNullOrEmpty(Model.Judul)
                 || Model.Penerbit == null || Model.Pengarang == null || Model.Rak == null)
                return false;

            return true;
        }

        private void AddRakCommandAction()
        {
            var window = new Pages.AddRakForm();
            window.ShowDialog();
            var vm = window.DataContext as AddRakFormViewModel;
            if (vm.Model.Id > 0)
            {
                Raks.Add(vm.Model);
            }
        }

        private void AddPengarangCommandAction()
        {
            var window = new Pages.AddPengarangForm();
            window.ShowDialog();
            var vm = window.DataContext as AddPengarangFormViewModel;
            if (vm.Model.Id > 0)
            {
                Pengarangs.Add(vm.Model);
            }
        }

        private void AddPenerbitCommandAction()
        {
            var window = new Pages.AddPenerbitForm();
            window.ShowDialog();
            var vm = window.DataContext as AddPenerbitFormViewModel;
            if (vm.Model.Id > 0)
            {
                Penerbits.Add(vm.Model);
            }
        }
    }
}
