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
    /// Logique d'interaction pour Window7.xaml
    /// </summary>
    public partial class Window7 : Window
    {
        public Window7()
        {
            InitializeComponent();
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
