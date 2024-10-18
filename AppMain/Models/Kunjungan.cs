using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppMain.Models
{
    public class Kunjungan : ObservableObject
    {

        private int id;

        public int Id
        {
            get { return id; }
            set { SetProperty(ref id, value); }
        }

        private Anggota anggota;

        public Anggota Anggota
        {
            get { return anggota; }
            set { SetProperty(ref anggota, value); }
        }


        private DateTime masuk;

        public DateTime Masuk
        {
            get { return masuk; }
            set { SetProperty(ref masuk, value); }
        }


        private DateTime? keluar;

        public DateTime? Keluar
        {
            get { return keluar; }
            set { SetProperty(ref keluar, value); }
        }
    }
}
