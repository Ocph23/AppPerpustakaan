using AppMain.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AppMain.Pages
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        private AddKunjunganPage formKunjungan;

        public HomePage()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            System.Windows.Controls.TextBox tb = (System.Windows.Controls.TextBox)sender;
            if (tb.Text.Length >= 10)
            {
                Cari(tb.Text);

            }
        }
        private async Task CloseForm(AddKunjunganPage form)
        {
            await Task.Delay(5000);
            if (form != null)
                form.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Cari(textCari.Text);
        }


        void Cari(string nomorKartu)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(nomorKartu) || nomorKartu.Length <= 5)
                    return;

                using var context = new ApplicationDbContext();
                var anggota = context.Anggotas.Single(x => x.NomorKartu == nomorKartu);
                if (anggota != null)
                {
                    var now = DateOnly.FromDateTime(DateTime.Now);
                    var kunjungan = context.Kunjungans.Where(x => x.Anggota.Id == anggota.Id && DateOnly.FromDateTime(x.Masuk.Date) == now).Include(x => x.Anggota).FirstOrDefault();
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
                        if (DateTime.Now.ToUniversalTime().Subtract(kunjungan.Masuk.ToUniversalTime()).TotalMinutes <= 5)
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

                    //if (kunjunganPage != null)
                    //{
                    //    var vm = kunjunganPage.DataContext as KunjunganPageViewModel;
                    //    _ = vm.LoadData();
                    //}
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                textCari.Text = string.Empty;
            }
        }

    }
}
