using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppMain
{
    public class Anggota
    {
        public int Id { get; set; }
        public string? Nama { get; set; }
        public string? NISN{ get; set; }
        public string? TempatLahir{ get; set; }
        public string? TanggalLahir{ get; set; }
        public string? NIK{ get; set; }
        public string? Agama{ get; set; }
        public string? JenisKelamin { get; set; }
        public string? Alamat { get;  set; }
        public string? Kelas { get; set; }
        public bool StatusAktif { get; set; }
        public JenisKeanggotaan JenisKeanggotaan { get; set; }
    }
}
