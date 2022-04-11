using System;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AppProjetSemestre4
{
    /// <summary>
    /// Logique d'interaction pour Window5.xaml
    /// </summary>
    public partial class Window5 : Window
    {
        public Window5()
        {
            InitializeComponent();
            TblCheminImage.Text = MainWindow.ImagePath;
        }
        public void Image_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow) Owner ;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                MainWindow.ImagePath = openFileDialog.FileName;
            }

            mainWindow.ImageBox.Source = new BitmapImage(new Uri(MainWindow.ImagePath));
            TblCheminImage.Text = MainWindow.ImagePath;
        }
        public void BtnFermer_Click(object sender, RoutedEventArgs e)
        {
            this.Owner = null;
            MainWindow.Coefficient_Flou = Convert.ToInt32(TbxCoeff.Text);
            this.Close();
        }
        public void Window2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
