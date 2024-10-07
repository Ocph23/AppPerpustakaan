using IronBarCode;
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
        public MainApp()
        {
            InitializeComponent();

           //// ReadBarcode();

            frame.Navigate(new Pages.BukuPage());
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
            frame.Navigate(new Pages.KunjunganPage());
        }

        private void menuStatistik(object sender, RoutedEventArgs e)
        {

        }

        private void menuSetting(object sender, RoutedEventArgs e)
        {

        }

        private void menuKeluar(object sender, RoutedEventArgs e)
        {

        }
    }
}
