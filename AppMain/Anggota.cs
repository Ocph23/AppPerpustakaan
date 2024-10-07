using CommunityToolkit.Mvvm.ComponentModel;

namespace AppMain
{
    public class Anggota : ObservableObject
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { SetProperty(ref id, value); }
        }


        private string nama;

        public string Nama
        {
            get { return nama; }
            set { SetProperty(ref nama, value); }
        }



        private string nomorKartu;

        public string NomorKartu
        {
            get { return nomorKartu; }
            set { SetProperty(ref nomorKartu, value); }
        }


        private string tempatLahir;

        public string TempatLahir
        {
            get { return tempatLahir; }
            set { SetProperty(ref tempatLahir, value); }
        }


        private DateOnly? tanggalLahir;

        public DateOnly? TanggalLahir
        {
            get { return tanggalLahir; }
            set { tanggalLahir = value; }
        }



        private string nik;

        public string NIK
        {
            get { return nik; }
            set { nik = value; }
        }

        private string agama;

        public string Agama
        {
            get { return agama; }
            set { SetProperty(ref agama , value); }
        }


        private string jenis;

        public string JenisKelamin
        {
            get { return jenis; }
            set { SetProperty(ref jenis , value); }
        }


        private string? alamat;

        public string? Alamat
        {
            get { return alamat; }
            set {SetProperty(ref alamat , value); }
        }

        private string kelas;

        public string Kelas
        {
            get { return kelas; }
            set { SetProperty (ref kelas , value); }
        }

        private bool statusAktif;

        public bool StatusAktif
        {
            get { return statusAktif; }
            set { SetProperty(ref statusAktif , value); }
        }

        private JenisKeanggotaan jenisKeanggotaan;

        public JenisKeanggotaan JenisKeanggotaan
        {
            get { return jenisKeanggotaan; }
            set {SetProperty(ref jenisKeanggotaan, value); }
        }

    }
}
