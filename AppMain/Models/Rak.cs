using CommunityToolkit.Mvvm.ComponentModel;

namespace AppMain.Models
{
    public class Rak : ObservableObject
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { SetProperty(ref id, value); }
        }


        private string kode;

        public string Kode
        {
            get { return kode; }
            set { SetProperty(ref kode, value); }
        }


        private string? lokasi;

        public string? Lokasi
        {
            get { return lokasi; }
            set { SetProperty(ref lokasi, value); }
        }


    }
}