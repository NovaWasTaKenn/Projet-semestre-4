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
    /// Logique d'interaction pour Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2()
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
                TblCheminImage.Text = MainWindow.ImagePath;
            }
        }

        public void BtnFermer_Click(object sender, RoutedEventArgs e)
        {
            
            bool erreur = false;
            try
            {
                MainWindow.Pourcent_AeR = Convert.ToInt32(TbxPourcent.Text);
            }
            catch 
            {
                MessageBox.Show("La valeur d'agrandissement saisie est incorrecte, veuillez saisir un pourcentage ne contennant que des chiffres ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                erreur = true;
            }
            if (MainWindow.ImagePath == "/foret riviere.bmp")
            {
                erreur = true;
                MessageBox.Show("Aucune image n'est sélectionnée, veuillez cliquer sur \"Parcourir\" pour en sélectionner une", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (!erreur) { this.Owner = null; this.Close(); }
        }
        public void Window2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
