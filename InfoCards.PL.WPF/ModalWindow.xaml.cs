using Microsoft.Win32;
using System.Windows;

namespace InfoCards.PL.WPF
{
    public partial class ModalWindow : Window
    {
        public static string ImageFilePath { get; set; }
        public static string InfoCardName { get; set; }
        public static bool IsClosed { get; set; }

        private readonly OpenFileDialog openFileDialog;

        public ModalWindow()
        {
            openFileDialog = new OpenFileDialog();
            InitializeComponent();
        }

        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            openFileDialog.Filter = "Image files (*.bmp, *.png, *.jpg, *.jpeg, )|*.bmp; *.png; *.jpg;*.jpeg";

            if (openFileDialog.ShowDialog() == true)
            {
                tbImageFile.Text = openFileDialog.FileName;
            }
        }

        private void btnSaveData_Click(object sender, RoutedEventArgs e)
        {
            ImageFilePath = openFileDialog.FileName;
            InfoCardName = tbInfoCardName.Text;
            Close();
        }

        private void btnCloseWindow_Click(object sender, RoutedEventArgs e)
        {
            IsClosed = true;
            Close();
        }
    }
}