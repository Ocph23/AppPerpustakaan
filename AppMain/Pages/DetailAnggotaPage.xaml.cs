using AppMain.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DocumentFormat.OpenXml.Office2016.Drawing.Command;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace AppMain.Pages
{
    /// <summary>
    /// Interaction logic for DetailAnggotaPage.xaml
    /// </summary>
    public partial class DetailAnggotaPage : Window
    {
        private DetailAnggotaViewModel vm;

        public DetailAnggotaPage(Anggota anggota)
        {
            InitializeComponent();
            DataContext = vm = new DetailAnggotaViewModel(anggota);
            LoadImage(anggota);
        }

        private void LoadImage(Anggota anggota)
        {
            try
            {
                var file = $"{AppDomain.CurrentDomain.BaseDirectory}/Photos/{anggota.Photo}";
                if (!string.IsNullOrEmpty(anggota.Photo) && File.Exists(file))
                {
                    imagePhoto.Source = new BitmapImage(new Uri(file));
                }
            }
            catch (Exception)
            {

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Image file | *.bmp;*.png;*.jpg;*.jpeg";
                dialog.FilterIndex = 1;
                var resultDialog = dialog.ShowDialog();
                if (resultDialog == System.Windows.Forms.DialogResult.OK)
                {
                    var image = new BitmapImage(new Uri(dialog.FileName));
                    var encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(image));
                    var file = $"{vm.Model.NomorKartu}.png";
                    using (var filestream = new FileStream($"{AppDomain.CurrentDomain.BaseDirectory}/Photos/{file}", FileMode.Create))
                    {
                        encoder.Save(filestream);
                    }
                    this.imagePhoto.Source = image;
                    _ = vm.UpdatePhoto(file);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }

    internal class DetailAnggotaViewModel : ObservableObject
    {

        public Anggota Model { get; set; }

        public List<Agama> Agamas { get; set; } = new List<Agama>();
        public List<JenisKelamin> Kelamins { get; set; } = new List<JenisKelamin>();
        public List<string> DataKelas { get; set; } = new List<string>();
        public List<JenisKeanggotaan> DataJenisKeanggotaan { get; set; } = new List<JenisKeanggotaan>();

        public DetailAnggotaViewModel(Anggota anggota)
        {
            Agamas = Enum.GetValues<Agama>().ToList();
            Kelamins = Enum.GetValues<JenisKelamin>().ToList();
            DataKelas = Enum.GetValues<Kelas>().Select(x => Enum.GetName(typeof(Kelas), x)).ToList();
            DataJenisKeanggotaan = Enum.GetValues<JenisKeanggotaan>().ToList();
            Model = anggota;
            TTL = $"{anggota.TempatLahir}, {anggota.TanggalLahir.Value.ToString("dd-MM-yyyy")}";
            _ = ShowNewKelas();
            SaveCommand = new RelayCommand(SaveCommandAction, SaveCommandValidate);
            Model.PropertyChanged += Model_PropertyChanged;
        }

        private async Task ShowNewKelas()
        {
            await Task.Delay(500);
            if (Model.JenisKeanggotaan == JenisKeanggotaan.Siswa)
                ShowKelas = Visibility.Visible;
            else
                ShowKelas = Visibility.Collapsed;

        }

        private void Model_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            SaveCommand = new RelayCommand(SaveCommandAction, SaveCommandValidate);
            if (Model.JenisKeanggotaan == JenisKeanggotaan.Siswa)
                ShowKelas = Visibility.Visible;
            else
                ShowKelas = Visibility.Collapsed;

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
                dbContext.Anggotas.Add(Model);
                dbContext.SaveChanges();
                System.Windows.MessageBox.Show("Data berhasil disipan.", "Berhasil !");
                this.WindowClose();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Data Tidak Berhasil Disimpan !", "Error", MessageBoxButton.OK);
            }

        }

        internal async Task UpdatePhoto(string file)
        {
            try
            {
                using var db = new ApplicationDbContext();

                var data = db.Anggotas.SingleOrDefault(x => x.Id == Model.Id);
                if (data != null)
                {
                    data.Photo = file;
                    Model.Photo = file;
                    db.SaveChanges();
                }
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
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

        public Visibility ShowKelas
        {
            get { return showKelas; }
            set { SetProperty(ref showKelas, value); }
        }



        private string tTL;

        public string TTL
        {
            get { return tTL; }
            set { SetProperty(ref tTL, value); }
        }



        public string DisplayedImagePath
        {
            get { return $"/Photos/{Model.Photo}"; }
        }


    }
}
