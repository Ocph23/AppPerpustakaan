using AppMain.Models;
using AppMain.Pages;
using DocumentFormat.OpenXml.Spreadsheet;
using IronBarCode;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace AppMain
{
    /// <summary>
    /// Interaction logic for MainApp.xaml
    /// </summary>
    public partial class MainApp : Window
    {
        private AddKunjunganPage formKunjungan;
        private KunjunganPage kunjunganPage;

        public MainApp()
        {
            InitializeComponent();

            //// ReadBarcode();
            frame.Navigate(new Pages.BukuPage());
            Navigator.NavigationService = frame.NavigationService;
            this.KeyDown += MainApp_KeyDown;
        }

        private void MainApp_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.F1)
                {
                    var nomorKartu = "12345678910";
                    using var context = new ApplicationDbContext();
                    var anggota = context.Anggotas.Single(x => x.NomorKartu == nomorKartu);
                    if (anggota != null)
                    {
                        var now = DateOnly.FromDateTime(DateTime.Now);
                        //var kunjungan = context.Kunjungans.FirstOrDefault();
                        //var xx = DateOnly.FromDateTime(DateTime.Now);
                        var kunjungan = context.Kunjungans.Where(x => x.Anggota.Id == anggota.Id && DateOnly.FromDateTime(x.Masuk.Date) == DateOnly.FromDateTime(DateTime.Now)).Include(x => x.Anggota).FirstOrDefault();
                        if (kunjungan == null)
                        {
                            kunjungan = new Kunjungan() { Anggota = anggota };
                            kunjungan.Masuk = DateTime.Now.ToUniversalTime();
                            context.Kunjungans.Add(kunjungan);
                            context.SaveChanges();
                            formKunjungan = new AddKunjunganPage(kunjungan);
                            _ = CloseForm(formKunjungan);
                            formKunjungan.ShowDialog();
                            formKunjungan = null;
                        }
                        else
                        {
                            if (DateTime.Now.ToUniversalTime().Subtract(kunjungan.Masuk).TotalMinutes <= 5)
                            {
                                Notification.Show($"{anggota.Nama}, Anda Sudah Masuk !");
                            }
                            else if (kunjungan.Keluar == null)
                            {
                                kunjungan.Keluar = DateTime.Now.ToUniversalTime();
                                context.SaveChanges();
                                Notification.Show($"Terima Kasih '{anggota.Nama}' Anda Sudah Berkunjung !");
                            }
                            else
                            {
                                Notification.Show($"Anda Sudah Berkunjung Hari ini. Silahkan datang kembali besok !");
                            }
                        }

                        if (kunjunganPage != null)
                        {
                            var vm = kunjunganPage.DataContext as KunjunganPageViewModel;
                            _ = vm.LoadData();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task CloseForm(AddKunjunganPage form)
        {
            await Task.Delay(5000);
            if (form != null)
                form.Close();
        }

        private async Task<Anggota> CariAnggota(string nomorKartu)
        {
            try
            {
                await using var context = new ApplicationDbContext();
                var anggota = context.Anggotas.Single(x => x.NomorKartu == nomorKartu);
                if (anggota != null)
                {
                    var kunjungan = context.Kunjungans.Where(x => x.Id == anggota.Id && DateOnly.FromDateTime(x.Masuk.Date) == DateOnly.FromDateTime(DateTime.Now));
                    if (kunjungan != null)
                    {

                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        private Task ReadBarcode()
        {
            var myOptionsExample = new BarcodeReaderOptions
            {
                // Choose a speed from: Faster, Balanced, Detailed, ExtremeDetail
                // There is a tradeoff in performance as more Detail is set
                Speed = ReadingSpeed.Balanced,

                // Reader will stop scanning once a barcode is found, unless set to true
                ExpectMultipleBarcodes = true,

                // By default, all barcode formats are scanned for.
                // Specifying one or more, performance will increase.
                ExpectBarcodeTypes = BarcodeEncoding.AllOneDimensional,

                // Utilizes multiple threads to reads barcodes from multiple images in parallel.
                Multithreaded = true,

                // Maximum threads for parallel. Default is 4
                MaxParallelThreads = 2,

            };
            var resultFromPdf = BarcodeReader.Read("barcode.pdf", myOptionsExample);
            for (int i = 0; i < resultFromPdf.Count; i++)
            {
                // Print Barcode Data
                Console.WriteLine($"Value from Barcode # {i} : {resultFromPdf[i]}");
            }

            return Task.CompletedTask;
        }

        private void menuAnggota(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new Pages.AnggotaPage());
        }

        private void menuKunjungan(object sender, RoutedEventArgs e)
        {

            if (kunjunganPage == null)
            {
                kunjunganPage = new Pages.KunjunganPage();
            }
            frame.Navigate(kunjunganPage);
        }

        private void menuStatistik(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new StatistikPage());
        }

        private void menuSetting(object sender, RoutedEventArgs e)
        {
            var x=  new SettingPage();
            x.ShowDialog();
        }

        private void menuKeluar(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = System.Windows.MessageBox.Show("Yakin Keluar ?", "Perhatian", MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
                this.Close();
        }

        private void menuBuku(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new Pages.BukuPage());
        }
    }
}
