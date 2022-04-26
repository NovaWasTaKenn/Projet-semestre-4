using Microsoft.Win32;
using System;
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
    /// Logique d'interaction pour Window6.xaml
    /// </summary>
    public partial class Window6 : Window
    {
        public Window6()
        {
            InitializeComponent();
            TblCheminImage2.Text = MainWindow.ImageName;

        }
        public void Image_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Owner;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string imagePath= openFileDialog.FileName;
                string nom = "";
                for(int i = imagePath.Length; !imagePath[i].Equals('\u005C') ; i--)
                {
                    nom =  imagePath[i] + nom;
                }

                MainWindow.ImagePath = imagePath;
                MainWindow.ImageName = nom;
                TblCheminImage2.Text = MainWindow.ImageName;
            }

            mainWindow.ImageBox.Source = new BitmapImage(new Uri(MainWindow.ImagePath));
        }

        public void Cachée_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Owner;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string imagecachéPath = openFileDialog.FileName;
                string nom = "";
                for (int i = imagecachéPath.Length; !imagecachéPath[i].Equals('\u005C'); i--)
                {
                    nom = imagecachéPath[i] + nom;
                }
                MainWindow.ImageCachéePath = imagecachéPath;
                TblCheminImage1.Text = nom;
            }
        }
        public void BtnFermer_Click(object sender, RoutedEventArgs e)
        {
            this.Owner = null;
            this.Close();
        }
        public void Window2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
