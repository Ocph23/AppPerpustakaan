using AppMain.Models;
using AppMain.Pages;
using BasselTech.UsbBarcodeScanner;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Management;
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
using System.Windows.Threading;
using USBHIDDRIVER;
using MessageBox = System.Windows.MessageBox;

namespace AppMain
{
    /// <summary>
    /// Interaction logic for MainApp.xaml
    /// </summary>
    public partial class MainApp : Window
    {
        private AddKunjunganPage formKunjungan;
        private KunjunganPage kunjunganPage;
        private bool _continue;
        private UsbBarcodeScanner scanner;
        private USBInterface usb;

        public MainApp()
        {
            InitializeComponent();

            //// ReadBarcode();
            frame.Navigate(new Pages.BukuPage());
            Navigator.NavigationService = frame.NavigationService;
            this.KeyDown += MainApp_KeyDown;
            //var ports = SerialPort.GetPortNames();


            // Subscribe to BarcodeScanned event
            scanner = new UsbBarcodeScanner();
            Dispatcher.BeginInvoke(new Action(() =>
            {
                scanner.BarcodeScanned += (sender, args) =>
                {

                    Debug.WriteLine($"Scanned barcode: {args.Barcode}");
                };

            }));

            scanner.Start();
            // Start capturing barcode scans

            //LoadScand();




        }


        private Task LoadScand()
        {
            try
            {


                usb = new USBHIDDRIVER.USBInterface("VID_0483", "PID_5710");

                String[] list = usb.getDeviceList();

                usb.enableUsbBufferEvent(new System.EventHandler(myEventCacher));
                var connect=usb.Connect();


                usb.startRead();

                return Task.CompletedTask;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void myEventCacher(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
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
            var x = new SettingPage();
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
