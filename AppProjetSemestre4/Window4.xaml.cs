using System.Windows;
using System.Windows.Input;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace AppProjetSemestre4
{
    /// <summary>
    /// Logique d'interaction pour Window4.xaml
    /// </summary>
    public partial class Window4 : Window
    {
        public Window4()
        {
            InitializeComponent();
        }
        public void Save_Click(object sender, RoutedEventArgs e)
        {

            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = "C:\\Users";
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                MainWindow.SavePath = dialog.FileName;
                TblCheminImage.Text = dialog.FileName;
            }

        }
        public void BtnFermer_Click(object sender, RoutedEventArgs e)
        {
            if(MainWindow.SavePath == null || MainWindow.SavePath.Length == 0)
            {
                MessageBox.Show("Le chemin de sauvegarde n'est pas défini, veuillez sélectionner un fichier de sortie en cliquant \"Parcourir\"", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                this.Close();
            }
            
        }
        public void Window2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
