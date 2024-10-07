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
            DataContext =vm= new AddAnggotaFormViewModel() { WindowClose=this.Close};
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

        public Anggota Model { get; set; } = new Anggota();

        public List<string> Agamas { get; set; } = new List<string>();
        public List<string> Kelamins { get; set; } = new List<string>();
        public List<string> DataKelas { get; set; } = new List<string>();
        public List<string> DataJenisKeanggotaan { get; set; } = new List<string>();

        public AddAnggotaFormViewModel()
        {
            Agamas = Enum.GetValues<Agama>().Select(x => Enum.GetName(typeof(Agama), x)).ToList();
            Kelamins = Enum.GetValues<JenisKelamin>().Select(x => Enum.GetName(typeof(JenisKelamin), x)).ToList();
            DataKelas = Enum.GetValues<Kelas>().Select(x => Enum.GetName(typeof(Kelas), x)).ToList();
            DataJenisKeanggotaan = Enum.GetValues<JenisKeanggotaan>().Select(x => Enum.GetName(typeof(JenisKeanggotaan), x)).ToList();
            SaveCommand = new RelayCommand(SaveCommandAction, SaveCommandValidate);
        }

        private bool SaveCommandValidate()
        {
            if (Model == null) { return false; }

            if (string.IsNullOrEmpty(Model.NomorKartu) || string.IsNullOrEmpty(Model.Nama) || string.IsNullOrEmpty(Model.Agama) ||
                 string.IsNullOrEmpty(Model.Alamat) 
                 || string.IsNullOrEmpty(Model.TempatLahir) || Model.TanggalLahir != null || string.IsNullOrEmpty(Model.Kelas)
                 || string.IsNullOrEmpty(Model.NIK))
                return false;

            return true;
        }

        private void SaveCommandAction()
        {
            using var dbContext = new ApplicationDbContext();
            dbContext.Anggotas.Add(Model);
            dbContext.SaveChanges();
            System.Windows.MessageBox.Show("Data berhasil sudah disipan.","Berhasil !");
            this. WindowClose();
            
        }

        private ICommand saveCommand;

        public ICommand SaveCommand
        {
            get { return saveCommand; }
            set { SetProperty(ref saveCommand, value); }
        }

        public Action WindowClose { get;  set; }
    }
}