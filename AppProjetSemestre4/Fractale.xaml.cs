using System;
using System.Collections.Generic;
using System.Linq;
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
    //btn perso 1er chgmt de bck ok pas de retour normal apres puis plus de reaction  OK
    //expander trop haut pas aligné 
    // Tennter de varier le background de prééxistant click pendant une courte durée : peut etre sleep + async pour pas bloquer le reste du programme



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
            Button button = sender as Button;
            Color Bckcolor_Sah = Color.FromArgb(255, 130, 130, 130);
            SolidColorBrush Bckbrush_Sah = new SolidColorBrush(Bckcolor_Sah);
            Color Bckcolor = Color.FromArgb(255, 75, 75, 75);
            SolidColorBrush Bckbrush = new SolidColorBrush(Bckcolor);

            if (mainwindow.Fractale_Random == false)
            {
                mainwindow.Fractale_Random = true;
                button.Background = Bckbrush;
            }
            else
            {
                mainwindow.Fractale_Random = false;
                button.Background = Bckbrush_Sah;
            }

        }
        public void Personnalisé_Click(object sender, RoutedEventArgs e)
        {
            if (GrdPersonnalisé.Visibility == Visibility.Collapsed)
            {
                MainWindow mainwindow = Owner as MainWindow;
                mainwindow.Fractale_Personnalisé = true;
                GrdPersonnalisé.Visibility = Visibility.Visible;
                Color Bckcolor = Color.FromArgb(255, 75, 75, 75);
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

            Color Bckcolor_Sah = Color.FromArgb(255, 130, 130, 130);
            SolidColorBrush Bckbrush_Sah = new SolidColorBrush(Bckcolor_Sah);

            Color Bckcolor = Color.FromArgb(255, 100, 100, 100);
            SolidColorBrush Bckbrush = new SolidColorBrush(Bckcolor);
            button.Background = Bckbrush;

            foreach (var element in SpFractale.Children.OfType<Button>())
            {
                if (element != button)
                {
                    element.Background = Bckbrush_Sah;
                }
                
            }
        }

        public void BtnFermer_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainwindow = Owner as MainWindow;
            
            bool erreur = false;
            try
            {
                if (mainwindow.Fractale_Random)
                {
                    mainwindow.Coté = Convert.ToInt32(TbxCoté.Text);
                }
                if (mainwindow.Fractale_Personnalisé) 
                {
                    mainwindow.Itérations_Personnalisé = Convert.ToInt32(TbxIteration.Text);
                    mainwindow.Mult_Couleurs_Personnalisé = Convert.ToInt32(TbxCoeff.Text);
                    mainwindow.Reel_Personnalisé = Convert.ToDouble(TbxRe.Text);
                    mainwindow.Im_Personnalisé = Convert.ToDouble(TbxIm.Text);
                    mainwindow.Coté = Convert.ToInt32(TbxCoté.Text);
                }
                if (!mainwindow.Fractale_Personnalisé && !mainwindow.Fractale_Random)
                {
                    mainwindow.Coté = Convert.ToInt32(TbxCoté.Text);
                }

            }
            catch
            {
                MessageBox.Show("Une des valeurs saisie est incorecte, veuillez saisir à nouveau", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                erreur = true;
            }
            if (!erreur) { this.Owner = null; this.Close(); }

        }
        public void Window2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void ExpFractale_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            Scv_Prééxistant.ScrollToVerticalOffset(Scv_Prééxistant.VerticalOffset - 0.2*e.Delta);
            e.Handled = true;
        }
    }
}
