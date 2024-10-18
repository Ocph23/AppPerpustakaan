using CommunityToolkit.Mvvm.ComponentModel;

namespace AppMain.Models
{
    public class Penerbit : ObservableObject
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { SetProperty(ref id, value); }
        }


        private string? nama;

        public string? Nama
        {
            get { return nama; }
            set { SetProperty(ref nama, value); }
        }

        private string? alamat;

        public string? Alamat
        {
            get { return alamat; }
            set { SetProperty(ref alamat, value); }
        }


        private string? telp;

        public string? Telp
        {
            get { return telp; }
            set { SetProperty(ref telp, value); }
        }


    }
}