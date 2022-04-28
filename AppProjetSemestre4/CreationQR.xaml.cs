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
    /// Logique d'interaction pour CreationQR.xaml
    /// </summary>
    public partial class CreationQR : Window
    {
        public CreationQR()
        {
            InitializeComponent();
        }

        

        public void BtnFermer_Click(object sender, RoutedEventArgs e)
        {
            this.Owner = null;
            MainWindow.TextQR = Convert.ToString(TbxQR.Text);
            this.Close();
        }
        public void Window2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
