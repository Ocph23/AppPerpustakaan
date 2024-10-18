using AppMain.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using System.Windows.Input;

namespace AppMain.Pages
{
    /// <summary>
    /// Interaction logic for AddAnggotaForm.xaml
    /// </summary>
    public partial class AddAnggotaForm : Window
    {
        private AddAnggotaFormViewModel vm;

        public AddAnggotaForm()
        {
            InitializeComponent();
            DataContext = vm = new AddAnggotaFormViewModel() { WindowClose = this.Close };
        }

        public AddAnggotaForm(Anggota selectedItem)
        {
            InitializeComponent();
            DataContext = vm = new AddAnggotaFormViewModel(selectedItem) { WindowClose = this.Close };
        }

        private void btnTambah(object sender, RoutedEventArgs e)
        {

        }

        private void btnBatal(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

    internal class AddAnggotaFormViewModel : ObservableObject
    {

        public Anggota Model { get; set; }

        public List<Agama> Agamas { get; set; } = new List<Agama>();
        public List<JenisKelamin> Kelamins { get; set; } = new List<JenisKelamin>();
        public List<string> DataKelas { get; set; } = new List<string>();
        public List<JenisKeanggotaan> DataJenisKeanggotaan { get; set; } = new List<JenisKeanggotaan>();

        public AddAnggotaFormViewModel()
        {
            Model = new Anggota() { StatusAktif=true};
            Model.PropertyChanged += Model_PropertyChanged;
            Init();
        }

        private void Init()
        {
            Agamas = Enum.GetValues<Agama>().ToList();
            Kelamins = Enum.GetValues<JenisKelamin>().ToList();
            DataKelas = Enum.GetValues<Kelas>().Select(x => Enum.GetName(typeof(Kelas), x)).ToList();
            DataJenisKeanggotaan = Enum.GetValues<JenisKeanggotaan>().ToList();
            SaveCommand = new RelayCommand(SaveCommandAction, SaveCommandValidate);
            Model_PropertyChanged(null, null);

        }

        public AddAnggotaFormViewModel(Anggota selectedItem)
        {
            this.Model = selectedItem;
            Model.PropertyChanged += Model_PropertyChanged;
            Init();
        }

        private void Model_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            SaveCommand = new RelayCommand(SaveCommandAction, SaveCommandValidate);
            if (Model != null)
            {
                if (Model.JenisKeanggotaan == JenisKeanggotaan.Siswa)
                    ShowKelas = Visibility.Visible;
                else
                    ShowKelas = Visibility.Collapsed;
            }

        }

        private bool SaveCommandValidate()
        {
            if (Model == null) { return false; }

            if (string.IsNullOrEmpty(Model.NomorKartu)
                || string.IsNullOrEmpty(Model.Nama)
                || string.IsNullOrEmpty(Model.Alamat)
                 || string.IsNullOrEmpty(Model.TempatLahir)
                || Model.TanggalLahir == null)

                return false;


            if (Model.JenisKeanggotaan == JenisKeanggotaan.Siswa && (string.IsNullOrEmpty(Model.Kelas) || Model.Kelas.ToLower().Trim() == "none"))
                return false;

            return true;
        }

        private void SaveCommandAction()
        {
            try
            {
                using var dbContext = new ApplicationDbContext();

                if (Model.JenisKeanggotaan != JenisKeanggotaan.Siswa)
                {
                    Model.Kelas = "None";
                }
                Model.TanggalLahir = Model.TanggalLahir.Value.ToUniversalTime();

                if (Model.Id > 0)
                {
                    //var oldData = dbContext.Anggotas.SingleOrDefault(x => x.Id == Model.Id);
                    //if (oldData != null)
                    //{
                    //}
                    dbContext.Update(Model);
                }
                else
                {
                    dbContext.Anggotas.Add(Model);
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


        private Visibility showKelas = Visibility.Collapsed;
        private Anggota selectedItem;

        public Visibility ShowKelas
        {
            get { return showKelas; }
            set { SetProperty(ref showKelas, value); }
        }

    }
}