using AppMain.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using System.Windows.Input;

namespace AppMain.Pages
{
    /// <summary>
    /// Interaction logic for AddPengarangForm.xaml
    /// </summary>
    public partial class AddPengarangForm : Window
    {
        private AddPengarangFormViewModel vm;

        public AddPengarangForm()
        {
            InitializeComponent();
            DataContext = vm = new AddPengarangFormViewModel() { WindowClose = this.Close };
        }

        public AddPengarangForm(Pengarang selectedItem)
        {
            InitializeComponent();
            DataContext = vm = new AddPengarangFormViewModel(selectedItem) { WindowClose = this.Close };
        }

        private void btnTambah(object sender, RoutedEventArgs e)
        {

        }

        private void btnBatal(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

    internal class AddPengarangFormViewModel : ObservableObject
    {

        public Pengarang Model { get; set; }

        public AddPengarangFormViewModel()
        {
            Model = new Pengarang() ;
            Model.PropertyChanged += Model_PropertyChanged;
            Init();
        }

        private void Init()
        {
            SaveCommand = new RelayCommand(SaveCommandAction, SaveCommandValidate);
            Model_PropertyChanged(null, null);

        }

        public AddPengarangFormViewModel(Pengarang selectedItem)
        {
            this.Model = selectedItem;
            Model.PropertyChanged += Model_PropertyChanged;
            Init();
        }

        private void Model_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            SaveCommand = new RelayCommand(SaveCommandAction, SaveCommandValidate);
        }

        private bool SaveCommandValidate()
        {
            if (Model == null) { return false; }

            if (string.IsNullOrEmpty(Model.Nama))
                return false;
            return true;
        }

        private void SaveCommandAction()
        {
            try
            {
                using var dbContext = new ApplicationDbContext();

                if (Model.Id > 0)
                {
                    dbContext.Update(Model);
                }
                else
                {
                    dbContext.Pengarangs.Add(Model);
                }

                dbContext.SaveChanges();
                System.Windows.MessageBox.Show("Data berhasil disipan.", "Berhasil !");
                this.WindowClose();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Data Tidak Berhasil Disimpan !", "Error", MessageBoxButton.OK);
            }

        }

        private ICommand saveCommand;

        public ICommand SaveCommand
        {
            get { return saveCommand; }
            set { SetProperty(ref saveCommand, value); }
        }

        public Action WindowClose { get; set; }



    }
}