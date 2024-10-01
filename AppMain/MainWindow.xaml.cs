using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
                                        JenisKelamin = list[2],
                                        NISN = list[3],
                                        TempatLahir = list[4].ToUpper(),
                                        TanggalLahir = list[5],
                                        NIK = list[6],
                                        Agama = list[7],
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
            var result = MessageBox.Show("Yakin Menutup Aplikasi ? ", "Keluar", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void btnLogin(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(UserModel.UserName) || string.IsNullOrEmpty(UserModel.Password))
            {
                MessageBox.Show("Username dan Password  Tidak Boleh Kosong !");
            }
            else
            {
                using var context = new ApplicationDbContext();
                var user = context.Users.SingleOrDefault(x => x.UserName.ToLower() == UserModel.UserName.ToLower());
                if (user != null)
                {
                    var xx = User.HashPasword(UserModel.Password);

                    if ( User.VerifyPassword(UserModel.Password, user.Password ))
                    {
                        var form = new MainApp();
                        form.Show();
                        this.Close();
                        return;
                    }
                }
                MessageBox.Show("Anda tidak memiliki akses !");
            }
        }

        private void onchangePassword(object sender, RoutedEventArgs e)
        {
            PasswordBox password = (PasswordBox)sender;
            UserModel.Password = password.Password;
        }
    }
}