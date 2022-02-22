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
        }
        public void BtnFermer_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Angle = Convert.ToDouble(TbxAngle.Text);
            MainWindow.Sens = sh;
            this.Close();
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
            BtnSh.BorderBrush = Brdbrush;

            Color Bckcolor_Sah = Color.FromArgb(255, 75, 75, 75);
            SolidColorBrush Bckbrush_Sah = new SolidColorBrush(Bckcolor_Sah);
            BtnSah.Background = Bckbrush_Sah;
            BtnSah.BorderBrush = Brushes.Transparent;

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
            BtnSah.BorderBrush = Brdbrush;

            Color Bckcolor_Sh = Color.FromArgb(255, 75, 75, 75);
            SolidColorBrush Bckbrush_Sh = new SolidColorBrush(Bckcolor_Sh);
            BtnSh.Background = Bckbrush_Sh;
            BtnSh.BorderBrush = Brushes.Transparent;

        }
    }
}
