using Microsoft.EntityFrameworkCore;
using Microsoft.Reporting.WinForms;
using Microsoft.VisualBasic;
using ReportModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AppMain.Pages
{
    /// <summary>
    /// Interaction logic for StatistikPage.xaml
    /// </summary>
    public partial class StatistikPage : Page
    {
        public StatistikPage()
        {
            InitializeComponent();
            List<ReportStatistik> dataSource = new List<ReportStatistik>();

            using var dbcontext = new ApplicationDbContext();
            Stream reportDefinition; // your RDLC from file or resource
            var tahun = 2024;
            var kunjungan = dbcontext.Kunjungans.Where(x => x.Masuk.Year == tahun).Include(x => x.Anggota);
            var g = kunjungan.GroupBy(x => x.Masuk.Month);

            foreach (var item in g)
            {
                dataSource.Add(new ReportStatistik
                {
                    Bulan = Helper.GetBulan(item.First().Masuk.Month),
                    Guru = item.Where(x => x.Anggota.JenisKeanggotaan == JenisKeanggotaan.Guru).Count(),
                    Siswa = item.Where(x => x.Anggota.JenisKeanggotaan == JenisKeanggotaan.Siswa).Count(),
                    Lain = item.Where(x => x.Anggota.JenisKeanggotaan == JenisKeanggotaan.Lain).Count(),
                });
            }



            //LocalReport report = new LocalReport();
            string reportPath = $"{AppDomain.CurrentDomain.BaseDirectory}/Reports/Report1.rdlc";
            this.reportViewer.LocalReport.ReportPath = reportPath;
            this.reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dataSource));

            System.Drawing.Printing.PageSettings ps = new System.Drawing.Printing.PageSettings();
            ps.Landscape = true;
            ps.Margins = new System.Drawing.Printing.Margins(2, 2, 2, 2);
            ps.PaperSize = new System.Drawing.Printing.PaperSize("A4", 827, 1170);
            ps.PaperSize.RawKind = (int)System.Drawing.Printing.PaperKind.A4;
            reportViewer.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            reportViewer.SetPageSettings(ps);


            ReportParameter[] parameters = new ReportParameter[1];
            parameters[0] = new ReportParameter("tahun", DateTime.Now.Year.ToString());
            reportViewer.LocalReport.SetParameters(parameters);
            //byte[] pdf = report.Render("PDF");
            this.reportViewer.RefreshReport();
        }
    }
}
