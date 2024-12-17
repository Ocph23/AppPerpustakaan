using AppMain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Reporting.WinForms;
using ReportModel;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Forms;

namespace AppMain.Pages
{
    /// <summary>
    /// Interaction logic for PrintKartuPelajarPage.xaml
    /// </summary>
    public partial class PrintKartuPelajarPage : Window
    {
        public PrintKartuPelajarPage()
        {
            InitializeComponent();
            List<AnggotaModel> dataSource = new List<AnggotaModel>();

            using var dbcontext = new ApplicationDbContext();
            Stream reportDefinition; // your RDLC from file or resource
            var tahun = 2024;


            var source = dbcontext.Anggotas.Take(5).ToList();
            foreach (var x in source)
            {
               var itemx = new AnggotaModel
                {
                    Agama = x.Agama.ToString(),
                    JenisKelamin = x.JenisKelamin == JenisKelamin.P ?"Perempuan":"Laki-Laki",
                    TTTL = $"{x.TempatLahir.ToUpperInvariant()}, {x.TanggalLahir.Value.ToString("dd MMMM yyyy")}",
                    Nama = x.Nama,
                    NIS = x.NomorKartu,
                    Photo = GetImage($"{AppDomain.CurrentDomain.BaseDirectory}Photos\\{x.Photo}", x.NomorKartu),
                    Keahlian = x.Kelas
                };

                dataSource.Add(itemx);

            }

            string reportPath = $"{AppDomain.CurrentDomain.BaseDirectory}/Reports/KartuPelajar.rdlc";
            this.reportViewer.LocalReport.EnableExternalImages = true;
            this.reportViewer.LocalReport.ReportPath = reportPath;
            this.reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dataSource));

            System.Drawing.Printing.PageSettings ps = new System.Drawing.Printing.PageSettings();
            ps.Landscape = false;
            ps.Margins = new System.Drawing.Printing.Margins(2, 2, 2, 2);
            ps.PaperSize = new System.Drawing.Printing.PaperSize("A4", 827, 1170);
            ps.PaperSize.RawKind = (int)System.Drawing.Printing.PaperKind.A4;
            reportViewer.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            reportViewer.SetPageSettings(ps);


            ReportParameter[] parameters = new ReportParameter[1];
            //parameters[0] = new ReportParameter("tahun", DateTime.Now.Year.ToString());
            //reportViewer.LocalReport.SetParameters(parameters);
            //byte[] pdf = report.Render("PDF");
            this.reportViewer.RefreshReport();
        }

        private string GetImage(string file, string nomorKartu)
        {
            try
            {

                if (!File.Exists(file))
                    return "";

                var newFileName = $"{nomorKartu}.png";
                if (!Directory.Exists("Print"))
                    Directory.CreateDirectory("Print");

                var bitmap1 = (Bitmap)Bitmap.FromFile(file);
                bitmap1.RotateFlip(RotateFlipType.Rotate90FlipXY);
                var fileName = $"{AppDomain.CurrentDomain.BaseDirectory}Print\\{newFileName}";
                bitmap1.Save(fileName);
                return $"File:\\{fileName}";
            }
            catch (System.IO.FileNotFoundException)
            {
                return string.Empty;
            }
        }
    }
}
