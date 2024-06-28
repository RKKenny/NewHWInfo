using Microsoft.Win32;
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
using OfficeOpenXml;
using System.IO;

namespace NewHWInfo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += frmMain_Loaded;
        }

        public void menExport_Click(object sender, RoutedEventArgs e)
        {
            string fileText = "Your output text";
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.FileName = "Характеристики АРМ";
            dlg.DefaultExt = ".xlsx";
            dlg.Filter = "Excel файлы(*.xlsx)|*.xlsx|" +
                "Excel старый формат(*.xls)|*.xls|" +
                "Все файлы(*.*)|*.*";


            if (dlg.ShowDialog() == true)
            {
                CExportHelper.ExportDataGridToExcel(CSystemInformation.Get_OS(),
                    CSystemInformation.Get_CPU(), CSystemInformation.Get_RAM(),
                    CSystemInformation.Get_Display(), dlg.FileName);
            }
        }

            private void menClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void frmMain_Loaded(object sender, RoutedEventArgs e)
        {
            txtOSInfo.Text = CSystemInformation.Get_OS();
            txtCPUInfo.Text = CSystemInformation.Get_CPU();
            txtRAMInfo.Text = CSystemInformation.Get_RAM();
            txtDisplayInfo.Text = CSystemInformation.Get_Display();
        }
    }
}