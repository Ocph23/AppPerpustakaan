using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPerpustakaan
{
    public class Siswa
    {
        public string Nama { get; set; }
        public string NISN{ get; set; }
        public string TempatLahir{ get; set; }
        public string TanggalLahir{ get; set; }
        public string NIK{ get; set; }
        public string Agama{ get; set; }
        public string JenisKelamin { get; internal set; }
    }
}
