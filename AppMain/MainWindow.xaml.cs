using AppMain.Models;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;

namespace AppMain
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public User UserModel { get; set; } = new User();

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            _ = GetDataFromExcel();
        }

        private async Task GetDataFromExcel()
        {
            await using var ctx = new ApplicationDbContext();
            await ctx.Database.MigrateAsync();

            if (!ctx.Users.Any())
            {
                ctx.Users.Add(new User { Aktif = true, Name = "Administrator", UserName = "Admin", Password = User.HashPasword("Password#123") });
                ctx.SaveChanges();
            }

            if (!ctx.Anggotas.Any())
            {
                List<Anggota> data = new List<Anggota>();
                try
                {
                    //Lets open the existing excel file and read through its content . Open the excel using openxml sdk
                    using (SpreadsheetDocument doc = SpreadsheetDocument.Open("databasefix.xlsx", false))
                    {
                        WorkbookPart workbookPart = doc.WorkbookPart;
                        Sheet sheet = workbookPart.Workbook.Sheets.GetFirstChild<Sheet>();
                        WorksheetPart worksheetPart = (WorksheetPart)workbookPart.GetPartById(sheet.Id);
                        OpenXmlReader reader = OpenXmlReader.Create(worksheetPart);
                        while (reader.Read())
                        {
                            if (reader.ElementType == typeof(Row))
                            {
                                Row row = (Row)reader.LoadCurrentElement();
                                List<string> list = new List<string>();
                                if (row.RowIndex >= 3)
                                {
                                    foreach (Cell cell in row.Elements<Cell>())
                                    {
                                        string cellValue = GetCellValue(doc, cell);
                                        list.Add(cellValue);
                                    }

                                    var siswa = new Anggota()
                                    {
                                        Nama = list[1].ToUpper(),
                                        JenisKelamin = (JenisKelamin)Enum.Parse(typeof(JenisKelamin), list[2]),
                                        NomorKartu = list[3],
                                        TempatLahir = list[4].ToUpper(),
                                        TanggalLahir = DateTime.ParseExact(list[5], "dd-MM-yyyy", null).ToUniversalTime(),
                                        NIK = list[6],
                                        Agama = (Agama)Enum.Parse(typeof(Agama), list[7]),
                                        Alamat = list[8],
                                        Kelas = list[9],
                                        StatusAktif = true,
                                        JenisKeanggotaan = JenisKeanggotaan.Siswa,
                                    };

                                    data.Add(siswa);
                                }


                            }
                        }
                    }


                    ctx.Anggotas.AddRange(data);
                    ctx.SaveChanges();


                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        private static string GetCellValue(SpreadsheetDocument doc, Cell cell)
        {
            SharedStringTablePart stringTablePart = doc.WorkbookPart.SharedStringTablePart;
            string value = cell.CellValue.InnerXml;

            if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
            {
                return stringTablePart.SharedStringTable.ChildElements[Int32.Parse(value)].InnerText;
            }
            else
            {
                return value;
            }
        }

        private void btnKeluar(object sender, RoutedEventArgs e)
        {
            var result = System.Windows.MessageBox.Show("Yakin Menutup Aplikasi ? ", "Keluar", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void btnLogin(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(UserModel.UserName) || string.IsNullOrEmpty(UserModel.Password))
                {
                    System.Windows.MessageBox.Show("Username dan Password  Tidak Boleh Kosong !");
                }
                else
                {
                    using var context = new ApplicationDbContext();
                    var user = context.Users.SingleOrDefault(x => x.UserName.ToLower() == UserModel.UserName.ToLower());
                    if (user != null)
                    {
                        var xx = User.HashPasword(UserModel.Password);

                        if (User.VerifyPassword(UserModel.Password, user.Password))
                        {
                            var form = new MainApp();
                            form.Show();
                            this.Close();
                            return;
                        }
                    }
                    System.Windows.MessageBox.Show("Anda tidak memiliki akses !");
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Maaf Terhadi Kesalahan !, Hubungi Administrator.");
            }
        }

        private void onchangePassword(object sender, RoutedEventArgs e)
        {
            PasswordBox password = (PasswordBox)sender;
            UserModel.Password = password.Password;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var setting = new SettingPage();
            setting.ShowDialog();
        }
    }
}