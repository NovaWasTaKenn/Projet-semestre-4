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
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {

        
        private bool sh = false;
        private bool sah = false;
        

        public Window1()
        {
            InitializeComponent();
            TblCheminImage.Text = MainWindow.ImageName;
        }

        public void Image_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Owner;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string imagePath = openFileDialog.FileName;
                string nom = "";
                for (int i = imagePath.Length-1; !imagePath[i].Equals('\\'); i--)
                {
                    nom = imagePath[i] + nom;
                }

                MainWindow.ImagePath = imagePath;
                MainWindow.ImageName = nom;
            }
            if (MainWindow.ImagePath != "" && MainWindow.ImagePath != null && MainWindow.ImagePath != "/foret riviere.bmp")
            {
                mainWindow.ImageBox.Source = new BitmapImage(new Uri(MainWindow.ImagePath));
                TblCheminImage.Text = MainWindow.ImageName;
            }
               
        }

        public void BtnFermer_Click(object sender, RoutedEventArgs e)
        {
            
            bool erreur = false;
            try
            {
                MainWindow.Angle = Convert.ToDouble(TbxAngle.Text);
                MainWindow.Sens = sh;
            }
            catch 
            {
                MessageBox.Show("L'angle saisi est incorrect, veuillez saisir unn angle ne contennant que des chiffres", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                erreur = true;
            }
            if(MainWindow.ImagePath == "/foret riviere.bmp") 
            { 
                erreur = true;
                MessageBox.Show("Aucune image n'est sélectionnée, veuillez cliquer sur \"Parcourir\" pour en sélectionner une", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (!erreur) { this.Owner = null; this.Close(); }
            
        }
        public void BtnSh_Click(object sender, RoutedEventArgs e)
        {
            if (!sah && !sh) { sh = true; }
            else
            {
                if (!sh)
                {
                    sh = true;
                    sah = false;
                }
            }
            Color Bckcolor = Color.FromArgb(255, 50, 50, 50);
            SolidColorBrush Bckbrush = new SolidColorBrush(Bckcolor);
            BtnSh.Background = Bckbrush;
            Color Brdcolor = Color.FromArgb(255, 130, 130, 130);
            SolidColorBrush Brdbrush = new SolidColorBrush(Brdcolor);
            //BtnSh.BorderBrush = Brdbrush;

            Color Bckcolor_Sah = Color.FromArgb(255, 75, 75, 75);
            SolidColorBrush Bckbrush_Sah = new SolidColorBrush(Bckcolor_Sah);
            BtnSah.Background = Brdbrush;
            //BtnSah.BorderBrush = Brushes.Transparent;

        }
        public void BtnSah_Click(object sender, RoutedEventArgs e)
        {
            if (!sah && !sh) { sah = true; }
            else
            {
                if (!sah)
                {
                    sh = false;
                    sah = true;
                }
            }
            Color Bckcolor = Color.FromArgb(255, 50, 50, 50);
            SolidColorBrush Bckbrush = new SolidColorBrush(Bckcolor);
            BtnSah.Background = Bckbrush;
            Color Brdcolor = Color.FromArgb(255, 130, 130, 130);
            SolidColorBrush Brdbrush = new SolidColorBrush(Brdcolor);
            //BtnSah.BorderBrush = Brdbrush;

            Color Bckcolor_Sh = Color.FromArgb(255, 75, 75, 75);
            SolidColorBrush Bckbrush_Sh = new SolidColorBrush(Bckcolor_Sh);
            BtnSh.Background = Brdbrush;
            //BtnSh.BorderBrush = Brushes.Transparent;

        }
        public void Window1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
