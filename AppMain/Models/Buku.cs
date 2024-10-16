using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppMain.Models
{
    public class Buku : ObservableObject
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
            set { SetProperty(ref kode , value); }
        }

        private string judul;

        public string Judul
        {
            get { return judul; }
            set { SetProperty(ref judul, value); }
        }


        private int tahun=2024;

        public int Tahun
        {
            get { return tahun; }
            set { SetProperty(ref tahun, value); }
        }


        private int jumlah;

        public int Jumlah
        {
            get { return jumlah; }
            set { SetProperty(ref jumlah, value); }
        }


        private string? isbn;

        public string? ISBN
        {
            get { return isbn; }
            set { SetProperty(ref isbn, value); }
        }


        private Penerbit penerbit;

        public Penerbit Penerbit
        {
            get { return penerbit; }
            set {SetProperty(ref penerbit , value); }
        }


        private Pengarang pengarang;

        public Pengarang Pengarang
        {
            get { return pengarang; }
            set { SetProperty(ref pengarang, value); }
        }


        private Rak rak;

        public Rak Rak
        {
            get { return rak; }
            set { SetProperty(ref rak, value); }
        }




    }
}
