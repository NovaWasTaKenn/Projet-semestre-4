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

    //Expander pas de réaction au click et hover 
    //btn perso 1er chgmt de bck ok pas de retour normal apres puis plus de reaction
    //expander trop haut pas aligné 



    /// <summary>
    /// Logique d'interaction pour Fractale.xaml
    /// </summary>
    public partial class Fractale : Window
    {
        public Fractale()
        {
            InitializeComponent();
        }

        public void Aléatoire_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainwindow = Owner as MainWindow;
            mainwindow.Fractale_Random = true;

        }
        public void Personnalisé_Click(object sender, RoutedEventArgs e)
        {
            if (GrdPersonnalisé.Visibility == Visibility.Collapsed)
            {
                MainWindow mainwindow = Owner as MainWindow;
                mainwindow.Fractale_Personnalisé = true;
                GrdPersonnalisé.Visibility = Visibility.Visible;
                Color Bckcolor = Color.FromArgb(255, 50, 50, 50);
                SolidColorBrush Bckbrush = new SolidColorBrush(Bckcolor);
                BtnPersonnalisé.Background = Bckbrush;

            }
            else
            {
                Color Bckcolor_Sah = Color.FromArgb(255, 130, 130, 130);
                SolidColorBrush Bckbrush_Sah = new SolidColorBrush(Bckcolor_Sah);
                BtnPersonnalisé.Background = Bckbrush_Sah;
                GrdPersonnalisé.Visibility = Visibility.Collapsed;
            }
           
        }
        public void Prééxistant_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            MainWindow mainwindow = Owner as MainWindow;
            mainwindow.Index_Fractale = Convert.ToInt32(button.Name.Split('_')[1]);
            Color Bckcolor = Color.FromArgb(255, 50, 50, 50);
            SolidColorBrush Bckbrush = new SolidColorBrush(Bckcolor);
            button.Background = Bckbrush;
        }

        public void BtnFermer_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainwindow = Owner as MainWindow;
            this.Owner = null;
            bool erreur = false;
            try
            {
                mainwindow.Itérations_Personnalisé = Convert.ToInt32(TbxIteration.Text);
                mainwindow.Mult_Couleurs_Personnalisé = Convert.ToInt32(TbxCoeff.Text);
                mainwindow.Reel_Personnalisé = Convert.ToDouble(TbxRe.Text);
                mainwindow.Im_Personnalisé = Convert.ToDouble(TbxIm.Text);

            }
            catch
            {
                MessageBox.Show("L'angle saisi est incorrect, veuillez saisir unn angle ne contennant que des chiffres", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                erreur = true;
            }
            if (MainWindow.ImagePath == "/foret riviere.bmp")
            {
                erreur = true;
                MessageBox.Show("Aucune image n'est sélectionnée, veuillez cliquer sur \"Parcourir\" pour en sélectionner une", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (!erreur) { this.Close(); }

        }
        public void Window2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
