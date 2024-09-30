using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AppPerpustakaan
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            GetDataFromExcel();


        }

        private void GetDataFromExcel()
        {
                    List<Siswa> data = new List<Siswa>();
            try
            {
                //Lets open the existing excel file and read through its content . Open the excel using openxml sdk
                using (SpreadsheetDocument doc = SpreadsheetDocument.Open("database1.xlsx", false))
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
                                var siswa = new Siswa()
                                {
                                    Nama = list[1],
                                    JenisKelamin = list[2],
                                    NISN = list[3],
                                    TempatLahir = list[4],
                                    TanggalLahir = list[5],
                                    NIK = list[6],
                                    Agama = list[7],
                                    Alamat = list[8],
                                    Kelas= list[9],
                                    StatusAktif =true,
                                    JenisKeanggotaan=JenisKeanggotaan.Siswa,
                                };

                                data.Add(siswa);
                            }


                        }
                    }
                }
            }
            catch (Exception ex)
            {

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
    }
}
