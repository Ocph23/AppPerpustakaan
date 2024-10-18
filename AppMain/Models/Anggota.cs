using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppMain.Models
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


        private DateTime? tanggalLahir;

        public DateTime? TanggalLahir
        {
            get { return tanggalLahir; }
            set { SetProperty(ref tanggalLahir, value); }
        }



        private string? nik;

        public string? NIK
        {
            get { return nik; }
            set { SetProperty(ref nik, value); }
        }

        private Agama agama;

        public Agama Agama
        {
            get { return agama; }
            set { SetProperty(ref agama, value); }
        }


        private JenisKelamin jenis;

        public JenisKelamin JenisKelamin
        {
            get { return jenis; }
            set { SetProperty(ref jenis, value); }
        }


        private string? alamat;

        public string? Alamat
        {
            get { return alamat; }
            set { SetProperty(ref alamat, value); }
        }

        private string kelas;

        public string Kelas
        {
            get { return kelas; }
            set { SetProperty(ref kelas, value); }
        }

        private bool statusAktif;

        public bool StatusAktif
        {
            get { return statusAktif; }
            set { SetProperty(ref statusAktif, value); }
        }

        private JenisKeanggotaan jenisKeanggotaan;

        public JenisKeanggotaan JenisKeanggotaan
        {
            get { return jenisKeanggotaan; }
            set { SetProperty(ref jenisKeanggotaan, value); }
        }


        private string? photo;

        public string? Photo
        {
            get { return photo; }
            set { SetProperty(ref photo, value); }
        }


        private bool hasPhoto;

        [NotMapped]
        public bool HasPhoto
        {
            get { return !string.IsNullOrEmpty(Photo); }
            set { SetProperty(ref hasPhoto, value); }
        }
    }
}
