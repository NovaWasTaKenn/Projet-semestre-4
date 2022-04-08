
using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using  TD_1;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Collections.Generic;
using System.Windows.Input;
using System.Linq;

namespace AppProjetSemestre4
{

    /*A faire :
        - Garder en mémoire l'image source et la sortie pour pouvoir faire un autre traitement sans resélectionner
                - Créer automatiquement différents fichiers de sortie pour les différents traitements 
        - try Catch  SUPER IMPORTANT
        - Resize Soit technique des tailles en mode auto soit calculer le ratio de resize et l'appliquer a ttes les tailles
        - revoir position du choix image et sortie car pas nécessaire pr tts les traitements
        - créer des nouvelles images pr croix fermer et trait pour avoir les symboles en blanc
        - Possibilité de faire plusieurs traitements à la fois 
        - Activable depuis un menu option
        - aide mode d'emploi (possiblité d'avoir des tips qui apparaissent qd on hover sur un controle (ToolTip propriété))
        - Gérer les positions d'apparition des fenetres secondaires*/

    //Gérer le cas ou un traitement a ete sélectionné et est déselectionné : dequeue tout ds list supprimer la bonne valeur requeue

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static string imagePath;
        private string savePath;
        private static double angle;
        private static bool sens;
        private static int pourcent_AeR;
        private static int coefficient_flou;

        private SolidColorBrush[] bckColors = new SolidColorBrush[2];
        

        private Queue<string>  queue_fonctions = new Queue<string>();
        public static double Angle 
        {
            get { return angle; }
            set { angle = value; } 
        }
        public static string ImagePath
        {
            get { return imagePath; }
            set { imagePath = value; }
        }
        public static int Pourcent_AeR
        {
            get { return pourcent_AeR; }
            set { pourcent_AeR = value; }
        }
        public static int Coefficient_Flou
        {
            get { return coefficient_flou; }
            set { coefficient_flou = value; }
        }
        public static bool Sens
        {
            get { return sens; }
            set { sens = value; }
        }
        
        public MainWindow()
        {
            InitializeComponent();
            Color Bckcolor1 = Color.FromArgb(255, 50, 50, 50);
            //Color BckColor2 = Color.FromArgb(255, 75, 75, 75);
            //bckColors[0] = new SolidColorBrush(Bckcolor1);
            //bckColors[1] = new SolidColorBrush();
        }
        public void MainWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }


        public void FcnGénéral_Click(object sender, RoutedEventArgs e)
        {
            Window3 win3 = new Window3();
            win3.Owner = this;
            win3.Left = win3.Owner.Left + win3.Owner.Width;
            win3.Top = win3.Owner.Top;
            win3.Show();

            Button btn = sender as Button;
            queue_fonctions.Enqueue(btn.Name);

            //if(btn.Background == bckColors[1]) { btn.Background = bckColors[0]; }
            //if(btn.Background == bckColors[0]) { btn.Background = bckColors[1]; }
        }

        public void FcnRo_Click(object sender, RoutedEventArgs e)
        {
            Window1 win1 = new Window1();
            win1.Owner = this;
            win1.Left = win1.Owner.Left + win1.Owner.Width;
            win1.Top = win1.Owner.Top;
            win1.Show();

            queue_fonctions.Enqueue("FcnRo");

            Button btn = (Button)sender;
            //if (btn.Background == bckColors[1]) { btn.Background = bckColors[0]; }
            //if (btn.Background == bckColors[0]) { btn.Background = bckColors[1]; }
        }

        public void FcnAeR_Click(object sender, RoutedEventArgs e)
        {
            Window2 win2 = new Window2();
            win2.Owner = this;
            win2.Left = win2.Owner.Left + win2.Owner.Width;
            win2.Top = win2.Owner.Top;
            win2.Show();

            queue_fonctions.Enqueue("FcnAeR");

            Button btn = (Button)sender;
            //if (btn.Background == bckColors[1]) { btn.Background = bckColors[0]; }
            //if (btn.Background == bckColors[0]) { btn.Background = bckColors[1]; }
        }
        public void FcnFl_Click(object sender, RoutedEventArgs e)
        {
            Window5 win5 = new Window5();
            win5.Owner = this;
            win5.Left = win5.Owner.Left + win5.Owner.Width;
            win5.Top = win5.Owner.Top;
            win5.Show();

            queue_fonctions.Enqueue("FcnFl");

            Button btn = (Button)sender;
            //if (btn.Background == bckColors[1]) { btn.Background = bckColors[0]; }
            //if (btn.Background == bckColors[0]) { btn.Background = bckColors[1]; }
        }

        public void FcnCo_Click(object sender, RoutedEventArgs e)
        {
            Window6 win6 = new Window6();
            win6.Owner = this;
            win6.Left = win6.Owner.Left + win6.Owner.Width;
            win6.Top = win6.Owner.Top;
            win6.Show();

            queue_fonctions.Enqueue("FcnCo");

            Button btn = (Button)sender;
            //if (btn.Background == bckColors[1]) { btn.Background = bckColors[0]; }
            //if (btn.Background == bckColors[0]) { btn.Background = bckColors[1]; }
        }
        public void FcnCr_Click(object sender, RoutedEventArgs e)
        {

        }
        public void FcnFrl_Click(object sender, RoutedEventArgs e)
        {

        }

        public void Mn_Click(object sender, RoutedEventArgs e)
        {

        }
        public void RunFlou(object sender, EventArgs e)
        {
            MyImage imageFlou = new MyImage(imagePath);
            int[,] Flou = { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } };
            imageFlou.Convolution(Flou, coefficient_flou);
            ImageBox.Source = new BitmapImage(new Uri(savePath));
        }

        public void Lancer_Click(object sender, RoutedEventArgs e)//Voir comment supprimer certains des Run ex : NeB et les regrouper.
        {
            MyImage image = new MyImage(ImagePath);
            for(int i =0; i<queue_fonctions.Count; i++)
            {
                string fonction = queue_fonctions.Dequeue();

                switch (fonction)
                {

                }
            }
            image.ToFile(savePath);
        }
        public void FermerMain_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        public void MinimiserMain_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        //public void Traitement_Click(object sender, RoutedEventArgs e)
        //{
        //    Color BckColor_Autres1 = Color.FromArgb(255, 75, 75, 75);
        //    SolidColorBrush Bckbrush_Autres1 = new SolidColorBrush(BckColor_Autres1);
        //    foreach (var btn in SpMenu.Children.OfType<Button>().Where(x => x.Name.StartsWith("Fcn")))
        //    {
        //        btn.Background = Bckbrush_Autres1;
        //    }

        //    Color Bckcolor1 = Color.FromArgb(255, 50, 50, 50);
        //    SolidColorBrush Bckbrush1 = new SolidColorBrush(Bckcolor1);
        //    Button button = (Button)sender;
        //    button.Background = Bckbrush1;
        //    traitement = button.Name;
        //}
        //public void Traitement_Click(object sender, RoutedEventArgs e)
        //{
            
        //    traitement = ((ListBoxItem)sender).Name;

        //    switch (((ListBoxItem)sender).Name)// Verifier les noms
        //    {
        //        case "NeB":
        //            traitements["NeB"] = true;
        //            Color Bckcolor1 = Color.FromArgb(255, 50, 50, 50);
        //            SolidColorBrush Bckbrush1 = new SolidColorBrush(Bckcolor1);
        //            NeB.Background = Bckbrush1;

        //            Color BckColor_Autres1 = Color.FromArgb(255, 75, 75, 75);
        //            SolidColorBrush Bckbrush_Autres1 = new SolidColorBrush(BckColor_Autres1);
        //            traitements["Ro"] = false;
        //            Ro.Background = Bckbrush_Autres1;
        //            traitements["AeR"] = false;
        //            AeR.Background = Bckbrush_Autres1;
        //            traitements["Em"] = false;
        //            Em.Background = Bckbrush_Autres1;
        //            traitements["DdC"] = false;
        //            DdC.Background = Bckbrush_Autres1;
        //            traitements["RdB"] = false;
        //            RdB.Background = Bckbrush_Autres1;
        //            traitements["Fl"] = false;
        //            Fl.Background = Bckbrush_Autres1;
        //            traitements["Rpg"] = false;
        //            Rpg.Background = Bckbrush_Autres1;
        //            traitements["Frl"] = false;
        //            Frl.Background = Bckbrush_Autres1;
        //            traitements["Hst"] = false;
        //            Hst.Background = Bckbrush_Autres1;
        //            traitements["Co"] = false;
        //            Co.Background = Bckbrush_Autres1;
        //            traitements["Dc"] = false;
        //            Dc.Background = Bckbrush_Autres1;
        //            traitements["Cr"] = false;
        //            Cr.Background = Bckbrush_Autres1;
        //            traitements["Le"] = false;
        //            Le.Background = Bckbrush_Autres1;
        //            traitements["Cry"] = false;
        //            //Cry.Background = Bckbrush_Autres1;
        //            //traitements["Dcr"] = false;
        //            //Dcr.Background = Bckbrush_Autres1;
        //            break;
        //        case "Ro":
                    
        //            Color Bckcolor2 = Color.FromArgb(255, 50, 50, 50);
        //            SolidColorBrush Bckbrush2 = new SolidColorBrush(Bckcolor2);
        //            traitements["Ro"] = true;
        //            Ro.Background = Bckbrush2;

        //            Color BckColor_Autres2 = Color.FromArgb(255, 75, 75, 75);
        //            SolidColorBrush Bckbrush_Autres2 = new SolidColorBrush(BckColor_Autres2);
                    
        //            traitements["NeB"] = false;
        //            NeB.Background = Bckbrush_Autres2;
        //            traitements["AeR"] = false;
        //            AeR.Background = Bckbrush_Autres2;
        //            traitements["Em"] = false;
        //            Em.Background = Bckbrush_Autres2;
        //            traitements["DdC"] = false;
        //            DdC.Background = Bckbrush_Autres2;
        //            traitements["RdB"] = false;
        //            RdB.Background = Bckbrush_Autres2;
        //            traitements["Fl"] = false;
        //            Fl.Background = Bckbrush_Autres2;
        //            traitements["Rpg"] = false;
        //            Rpg.Background = Bckbrush_Autres2;
        //            traitements["Frl"] = false;
        //            Frl.Background = Bckbrush_Autres2;
        //            traitements["Hst"] = false;
        //            Hst.Background = Bckbrush_Autres2;
        //            traitements["Co"] = false;
        //            Co.Background = Bckbrush_Autres2;
        //            traitements["Dc"] = false;
        //            Dc.Background = Bckbrush_Autres2;
        //            traitements["Cr"] = false;
        //            Cr.Background = Bckbrush_Autres2;
        //            traitements["Le"] = false;
        //            Le.Background = Bckbrush_Autres2;
        //            traitements["Cry"] = false;
        //            Cry.Background = Bckbrush_Autres2;
        //            traitements["Dcr"] = false;
        //            Dcr.Background = Bckbrush_Autres2;
        //            break;
        //        case "AeR":
        //            Color Bckcolor3 = Color.FromArgb(255, 50, 50, 50);
        //            SolidColorBrush Bckbrush3 = new SolidColorBrush(Bckcolor3);
        //            traitements["AeR"] = true;
        //            AeR.Background = Bckbrush3;

        //            Color BckColor_Autres3 = Color.FromArgb(255, 75, 75, 75);
        //            SolidColorBrush Bckbrush_Autres3 = new SolidColorBrush(BckColor_Autres3);

        //            traitements["NeB"] = false;
        //            NeB.Background = Bckbrush_Autres3;
        //            traitements["Ro"] = false;
        //            Ro.Background = Bckbrush_Autres3;
        //            traitements["Em"] = false;
        //            Em.Background = Bckbrush_Autres3;
        //            traitements["DdC"] = false;
        //            DdC.Background = Bckbrush_Autres3;
        //            traitements["RdB"] = false;
        //            RdB.Background = Bckbrush_Autres3;
        //            traitements["Fl"] = false;
        //            Fl.Background = Bckbrush_Autres3;
        //            traitements["Rpg"] = false;
        //            Rpg.Background = Bckbrush_Autres3;
        //            traitements["Frl"] = false;
        //            Frl.Background = Bckbrush_Autres3;
        //            traitements["Hst"] = false;
        //            Hst.Background = Bckbrush_Autres3;
        //            traitements["Co"] = false;
        //            Co.Background = Bckbrush_Autres3;
        //            traitements["Dc"] = false;
        //            Dc.Background = Bckbrush_Autres3;
        //            traitements["Cr"] = false;
        //            Cr.Background = Bckbrush_Autres3;
        //            traitements["Le"] = false;
        //            Le.Background = Bckbrush_Autres3;
        //            traitements["Cry"] = false;
        //            Cry.Background = Bckbrush_Autres3;
        //            traitements["Dcr"] = false;
        //            Dcr.Background = Bckbrush_Autres3;
        //            break;
        //        case "Em":
        //            Color Bckcolor4 = Color.FromArgb(255, 50, 50, 50);
        //            SolidColorBrush Bckbrush4 = new SolidColorBrush(Bckcolor4);
        //            traitements["Em"] = true;
        //            Em.Background = Bckbrush4;

        //            Color BckColor_Autres4 = Color.FromArgb(255, 75, 75, 75);
        //            SolidColorBrush Bckbrush_Autres4 = new SolidColorBrush(BckColor_Autres4);

        //            traitements["NeB"] = false;
        //            NeB.Background = Bckbrush_Autres4;
        //            traitements["Ro"] = false;
        //            Ro.Background = Bckbrush_Autres4;
        //            traitements["AeR"] = false;
        //            AeR.Background = Bckbrush_Autres4;
        //            traitements["DdC"] = false;
        //            DdC.Background = Bckbrush_Autres4;
        //            traitements["RdB"] = false;
        //            RdB.Background = Bckbrush_Autres4;
        //            traitements["Fl"] = false;
        //            Fl.Background = Bckbrush_Autres4;
        //            traitements["Rpg"] = false;
        //            Rpg.Background = Bckbrush_Autres4;
        //            traitements["Frl"] = false;
        //            Frl.Background = Bckbrush_Autres4;
        //            traitements["Hst"] = false;
        //            Hst.Background = Bckbrush_Autres4;
        //            traitements["Co"] = false;
        //            Co.Background = Bckbrush_Autres4;
        //            traitements["Dc"] = false;
        //            Dc.Background = Bckbrush_Autres4;

        //            traitements["Cr"] = false;
        //            Cr.Background = Bckbrush_Autres4;
        //            traitements["Le"] = false;
        //            Le.Background = Bckbrush_Autres4;
        //            traitements["Cry"] = false;
        //            Cry.Background = Bckbrush_Autres4;
        //            traitements["Dcr"] = false;
        //            Dcr.Background = Bckbrush_Autres4;
        //            break;
        //        case "DdC":
        //            Color Bckcolor5 = Color.FromArgb(255, 50, 50, 50);
        //            SolidColorBrush Bckbrush5 = new SolidColorBrush(Bckcolor5);
        //            traitements["DdC"] = true;
        //            DdC.Background = Bckbrush5;
        //            Color BckColor_Autres5 = Color.FromArgb(255, 75, 75, 75);
        //            SolidColorBrush Bckbrush_Autres5 = new SolidColorBrush(BckColor_Autres5);

        //            traitements["NeB"] = false;
        //            NeB.Background = Bckbrush_Autres5;
        //            traitements["Ro"] = false;
        //            Ro.Background = Bckbrush_Autres5;
        //            traitements["AeR"] = false;
        //            AeR.Background = Bckbrush_Autres5;
        //            traitements["Em"] = false;
        //            Em.Background = Bckbrush_Autres5;
                    
        //            traitements["RdB"] = false;
        //            RdB.Background = Bckbrush_Autres5;
        //            traitements["Fl"] = false;
        //            Fl.Background = Bckbrush_Autres5;
        //            traitements["Rpg"] = false;
        //            Rpg.Background = Bckbrush_Autres5;
        //            traitements["Frl"] = false;
        //            Frl.Background = Bckbrush_Autres5;
        //            traitements["Hst"] = false;
        //            Hst.Background = Bckbrush_Autres5;
        //            traitements["Co"] = false;
        //            Co.Background = Bckbrush_Autres5;
        //            traitements["Dc"] = false;
        //            Dc.Background = Bckbrush_Autres5;
        //            traitements["Cr"] = false;
        //            Cr.Background = Bckbrush_Autres5;
        //            traitements["Le"] = false;
        //            Le.Background = Bckbrush_Autres5;
        //            traitements["Cry"] = false;
        //            Cry.Background = Bckbrush_Autres5;
        //            traitements["Dcr"] = false;
        //            Dcr.Background = Bckbrush_Autres5;
        //            break;
        //        case "RdB":
        //            Color Bckcolor6 = Color.FromArgb(255, 50, 50, 50);
        //            SolidColorBrush Bckbrush6 = new SolidColorBrush(Bckcolor6);
        //            traitements["RdB"] = true;
        //            RdB.Background = Bckbrush6;
        //            Color BckColor_Autres6 = Color.FromArgb(255, 75, 75, 75);
        //            SolidColorBrush Bckbrush_Autres6 = new SolidColorBrush(BckColor_Autres6);

        //            traitements["NeB"] = false;
        //            NeB.Background = Bckbrush_Autres6;
        //            traitements["Ro"] = false;
        //            Ro.Background = Bckbrush_Autres6;
        //            traitements["AeR"] = false;
        //            AeR.Background = Bckbrush_Autres6;
        //            traitements["Em"] = false;
        //            Em.Background = Bckbrush_Autres6;
        //            traitements["DdC"] = false;
        //            DdC.Background = Bckbrush_Autres6;
                    
        //            traitements["Fl"] = false;
        //            Fl.Background = Bckbrush_Autres6;
        //            traitements["Rpg"] = false;
        //            Rpg.Background = Bckbrush_Autres6;
        //            traitements["Frl"] = false;
        //            Frl.Background = Bckbrush_Autres6;
        //            traitements["Hst"] = false;
        //            Hst.Background = Bckbrush_Autres6;
        //            traitements["Co"] = false;
        //            Co.Background = Bckbrush_Autres6;
        //            traitements["Dc"] = false;
        //            Dc.Background = Bckbrush_Autres6;
        //            traitements["Cr"] = false;
        //            Cr.Background = Bckbrush_Autres6;
        //            traitements["Le"] = false;
        //            Le.Background = Bckbrush_Autres6;
        //            traitements["Cry"] = false;
        //            Cry.Background = Bckbrush_Autres6;
        //            traitements["Dcr"] = false;
        //            Dcr.Background = Bckbrush_Autres6;
        //            break;
        //        case "Fl":
        //            Color Bckcolor7= Color.FromArgb(255, 50, 50, 50);
        //            SolidColorBrush Bckbrush7 = new SolidColorBrush(Bckcolor7);
        //            traitements["Fl"] = true;
        //            Fl.Background = Bckbrush7;
        //            Color BckColor_Autres7 = Color.FromArgb(255, 75, 75, 75);
        //            SolidColorBrush Bckbrush_Autres7 = new SolidColorBrush(BckColor_Autres7);

        //            traitements["NeB"] = false;
        //            NeB.Background = Bckbrush_Autres7;
        //            traitements["Ro"] = false;
        //            Ro.Background = Bckbrush_Autres7;
        //            traitements["AeR"] = false;
        //            AeR.Background = Bckbrush_Autres7;
        //            traitements["Em"] = false;
        //            Em.Background = Bckbrush_Autres7;
        //            traitements["DdC"] = false;
        //            DdC.Background = Bckbrush_Autres7;
        //            traitements["RdB"] = false;
        //            RdB.Background = Bckbrush_Autres7;
                    
        //            traitements["Rpg"] = false;
        //            Rpg.Background = Bckbrush_Autres7;
        //            traitements["Frl"] = false;
        //            Frl.Background = Bckbrush_Autres7;
        //            traitements["Hst"] = false;
        //            Hst.Background = Bckbrush_Autres7;
        //            traitements["Co"] = false;
        //            Co.Background = Bckbrush_Autres7;
        //            traitements["Dc"] = false;
        //            Dc.Background = Bckbrush_Autres7;

        //            traitements["Cr"] = false;
        //            Cr.Background = Bckbrush_Autres7;
        //            traitements["Le"] = false;
        //            Le.Background = Bckbrush_Autres7;
        //            traitements["Cry"] = false;
        //            Cry.Background = Bckbrush_Autres7;
        //            traitements["Dcr"] = false;
        //            Dcr.Background = Bckbrush_Autres7;
        //            break;
        //        case "Rpg":
        //            Color Bckcolor8 = Color.FromArgb(255, 50, 50, 50);
        //            SolidColorBrush Bckbrush8 = new SolidColorBrush(Bckcolor8);
        //            traitements["Rpg"] = true;
        //            Rpg.Background = Bckbrush8;

        //            Color BckColor_Autres8 = Color.FromArgb(255, 75, 75, 75);
        //            SolidColorBrush Bckbrush_Autres8 = new SolidColorBrush(BckColor_Autres8);

        //            traitements["NeB"] = false;
        //            NeB.Background = Bckbrush_Autres8;
        //            traitements["Ro"] = false;
        //            Ro.Background = Bckbrush_Autres8;
        //            traitements["AeR"] = false;
        //            AeR.Background = Bckbrush_Autres8;
        //            traitements["Em"] = false;
        //            Em.Background = Bckbrush_Autres8;
        //            traitements["DdC"] = false;
        //            DdC.Background = Bckbrush_Autres8;
        //            traitements["RdB"] = false;
        //            RdB.Background = Bckbrush_Autres8;
        //            traitements["Fl"] = false;
        //            Fl.Background = Bckbrush_Autres8;
                    
        //            traitements["Frl"] = false;
        //            Frl.Background = Bckbrush_Autres8;
        //            traitements["Hst"] = false;
        //            Hst.Background = Bckbrush_Autres8;
        //            traitements["Co"] = false;
        //            Co.Background = Bckbrush_Autres8;
        //            traitements["Dc"] = false;
        //            Dc.Background = Bckbrush_Autres8;

        //            traitements["Cr"] = false;
        //            Cr.Background = Bckbrush_Autres8;
        //            traitements["Le"] = false;
        //            Le.Background = Bckbrush_Autres8;
        //            traitements["Cry"] = false;
        //            Cry.Background = Bckbrush_Autres8;
        //            traitements["Dcr"] = false;
        //            Dcr.Background = Bckbrush_Autres8;
        //            break;
        //        case "Frl":
        //            Color Bckcolor9 = Color.FromArgb(255, 50, 50, 50);
        //            SolidColorBrush Bckbrush9 = new SolidColorBrush(Bckcolor9);
        //            traitements["Frl"] = true;
        //            Frl.Background = Bckbrush9;

        //            Color BckColor_Autres9 = Color.FromArgb(255, 75, 75, 75);
        //            SolidColorBrush Bckbrush_Autres9 = new SolidColorBrush(BckColor_Autres9);

        //            traitements["NeB"] = false;
        //            NeB.Background = Bckbrush_Autres9;
        //            traitements["Ro"] = false;
        //            Ro.Background = Bckbrush_Autres9;
        //            traitements["AeR"] = false;
        //            AeR.Background = Bckbrush_Autres9;
        //            traitements["Em"] = false;
        //            Em.Background = Bckbrush_Autres9;
        //            traitements["DdC"] = false;
        //            DdC.Background = Bckbrush_Autres9;
        //            traitements["RdB"] = false;
        //            RdB.Background = Bckbrush_Autres9;
        //            traitements["Fl"] = false;
        //            Fl.Background = Bckbrush_Autres9;
        //            traitements["Rpg"] = false;
        //            Rpg.Background = Bckbrush_Autres9;
                    
        //            traitements["Hst"] = false;
        //            Hst.Background = Bckbrush_Autres9;
        //            traitements["Co"] = false;
        //            Co.Background = Bckbrush_Autres9;
        //            traitements["Dc"] = false;
        //            Dc.Background = Bckbrush_Autres9;

        //            traitements["Cr"] = false;
        //            Cr.Background = Bckbrush_Autres9;
        //            traitements["Le"] = false;
        //            Le.Background = Bckbrush_Autres9;
        //            traitements["Cry"] = false;
        //            Cry.Background = Bckbrush_Autres9;
        //            traitements["Dcr"] = false;
        //            Dcr.Background = Bckbrush_Autres9;
        //            break;
        //        case "Hst":
        //            Color Bckcolor10 = Color.FromArgb(255, 50, 50, 50);
        //            SolidColorBrush Bckbrush10 = new SolidColorBrush(Bckcolor10);
        //            traitements["Hst"] = true;
        //            Hst.Background = Bckbrush10;

        //            Color BckColor_Autres10 = Color.FromArgb(255, 75, 75, 75);
        //            SolidColorBrush Bckbrush_Autres10= new SolidColorBrush(BckColor_Autres10);

        //            traitements["NeB"] = false;
        //            NeB.Background = Bckbrush_Autres10;
        //            traitements["Ro"] = false;
        //            Ro.Background = Bckbrush_Autres10;
        //            traitements["AeR"] = false;
        //            AeR.Background = Bckbrush_Autres10;
        //            traitements["Em"] = false;
        //            Em.Background = Bckbrush_Autres10;
        //            traitements["DdC"] = false;
        //            DdC.Background = Bckbrush_Autres10;
        //            traitements["RdB"] = false;
        //            RdB.Background = Bckbrush_Autres10;
        //            traitements["Fl"] = false;
        //            Fl.Background = Bckbrush_Autres10;
        //            traitements["Rpg"] = false;
        //            Rpg.Background = Bckbrush_Autres10;
        //            traitements["Frl"] = false;
        //            Frl.Background = Bckbrush_Autres10;
        //            traitements["Co"] = false;
        //            Co.Background = Bckbrush_Autres10;
        //            traitements["Dc"] = false;
        //            Dc.Background = Bckbrush_Autres10;
        //            traitements["Cr"] = false;
        //            Cr.Background = Bckbrush_Autres10;
        //            traitements["Le"] = false;
        //            Le.Background = Bckbrush_Autres10;
        //            traitements["Cry"] = false;
        //            Cry.Background = Bckbrush_Autres10;
        //            traitements["Dcr"] = false;
        //            Dcr.Background = Bckbrush_Autres10;
        //            break;
        //        case "Co":
        //            Color Bckcolor11 = Color.FromArgb(255, 50, 50, 50);
        //            SolidColorBrush Bckbrush11 = new SolidColorBrush(Bckcolor11);
        //            traitements["Co"] = true;
        //            Co.Background = Bckbrush11;

        //            Color BckColor_Autres11 = Color.FromArgb(255, 75, 75, 75);
        //            SolidColorBrush Bckbrush_Autres11 = new SolidColorBrush(BckColor_Autres11);

        //            traitements["NeB"] = false;
        //            NeB.Background = Bckbrush_Autres11;
        //            traitements["Ro"] = false;
        //            Ro.Background = Bckbrush_Autres11;
        //            traitements["AeR"] = false;
        //            AeR.Background = Bckbrush_Autres11;
        //            traitements["Em"] = false;
        //            Em.Background = Bckbrush_Autres11;
        //            traitements["DdC"] = false;
        //            DdC.Background = Bckbrush_Autres11;
        //            traitements["RdB"] = false;
        //            RdB.Background = Bckbrush_Autres11;
        //            traitements["Fl"] = false;
        //            Fl.Background = Bckbrush_Autres11;
        //            traitements["Rpg"] = false;
        //            Rpg.Background = Bckbrush_Autres11;
        //            traitements["Frl"] = false;
        //            Frl.Background = Bckbrush_Autres11;
        //            traitements["Hst"] = false;
        //            Hst.Background = Bckbrush_Autres11;

        //            traitements["Dc"] = false;
        //            Dc.Background = Bckbrush_Autres11;
        //            traitements["Cr"] = false;
        //            Cr.Background = Bckbrush_Autres11;
        //            traitements["Le"] = false;
        //            Le.Background = Bckbrush_Autres11;
        //            traitements["Cry"] = false;
        //            Cry.Background = Bckbrush_Autres11;
        //            traitements["Dcr"] = false;
        //            Dcr.Background = Bckbrush_Autres11;
        //            break;
        //        case "Dc":
        //            Color Bckcolor15 = Color.FromArgb(255, 50, 50, 50);
        //            SolidColorBrush Bckbrush15 = new SolidColorBrush(Bckcolor15);
        //            traitements["Dc"] = true;
        //            Dc.Background = Bckbrush15;

        //            Color BckColor_Autres15 = Color.FromArgb(255, 75, 75, 75);
        //            SolidColorBrush Bckbrush_Autres15 = new SolidColorBrush(BckColor_Autres15);

        //            traitements["NeB"] = false;
        //            NeB.Background = Bckbrush_Autres15;
        //            traitements["Ro"] = false;
        //            Ro.Background = Bckbrush_Autres15;
        //            traitements["AeR"] = false;
        //            AeR.Background = Bckbrush_Autres15;
        //            traitements["Em"] = false;
        //            Em.Background = Bckbrush_Autres15;
        //            traitements["DdC"] = false;
        //            DdC.Background = Bckbrush_Autres15;
        //            traitements["RdB"] = false;
        //            RdB.Background = Bckbrush_Autres15;
        //            traitements["Fl"] = false;
        //            Fl.Background = Bckbrush_Autres15;
        //            traitements["Rpg"] = false;
        //            Rpg.Background = Bckbrush_Autres15;
        //            traitements["Frl"] = false;
        //            Frl.Background = Bckbrush_Autres15;
        //            traitements["Hst"] = false;
        //            Hst.Background = Bckbrush_Autres15;
        //            traitements["Co"] = false;
        //            Co.Background = Bckbrush_Autres15;

        //            traitements["Cr"] = false;
        //            Cr.Background = Bckbrush_Autres15;
        //            traitements["Le"] = false;
        //            Le.Background = Bckbrush_Autres15;
        //            traitements["Cry"] = false;
        //            Cry.Background = Bckbrush_Autres15;
        //            traitements["Dcr"] = false;
        //            Dcr.Background = Bckbrush_Autres15;
        //            break;
        //        case "Cr":
        //            Color Bckcolor12 = Color.FromArgb(255, 50, 50, 50);
        //            SolidColorBrush Bckbrush12 = new SolidColorBrush(Bckcolor11);
        //            traitements["Cr"] = true;
        //            Cr.Background = Bckbrush12;

        //            Color BckColor_Autres12 = Color.FromArgb(255, 75, 75, 75);
        //            SolidColorBrush Bckbrush_Autres12 = new SolidColorBrush(BckColor_Autres12);

        //            traitements["NeB"] = false;
        //            NeB.Background = Bckbrush_Autres12;
        //            traitements["Ro"] = false;
        //            Ro.Background = Bckbrush_Autres12;
        //            traitements["AeR"] = false;
        //            AeR.Background = Bckbrush_Autres12;
        //            traitements["Em"] = false;
        //            Em.Background = Bckbrush_Autres12;
        //            traitements["DdC"] = false;
        //            DdC.Background = Bckbrush_Autres12;
        //            traitements["RdB"] = false;
        //            RdB.Background = Bckbrush_Autres12;
        //            traitements["Fl"] = false;
        //            Fl.Background = Bckbrush_Autres12;
        //            traitements["Rpg"] = false;
        //            Rpg.Background = Bckbrush_Autres12;
        //            traitements["Frl"] = false;
        //            Frl.Background = Bckbrush_Autres12;
        //            traitements["Hst"] = false;
        //            Hst.Background = Bckbrush_Autres12;
        //            traitements["Co"] = false;
        //            Co.Background = Bckbrush_Autres12;
        //            traitements["Dc"] = false;
        //            Dc.Background = Bckbrush_Autres12;

        //            traitements["Le"] = false;
        //            Le.Background = Bckbrush_Autres12;
        //            traitements["Cry"] = false;
        //            Cry.Background = Bckbrush_Autres12;
        //            traitements["Dcr"] = false;
        //            Dcr.Background = Bckbrush_Autres12;
        //            break;
        //        case "Le":
        //            Color Bckcolor13 = Color.FromArgb(255, 50, 50, 50);
        //            SolidColorBrush Bckbrush13 = new SolidColorBrush(Bckcolor13);
        //            traitements["Le"] = true;
        //            Le.Background = Bckbrush13;

        //            Color BckColor_Autres13 = Color.FromArgb(255, 75, 75, 75);
        //            SolidColorBrush Bckbrush_Autres13 = new SolidColorBrush(BckColor_Autres13);

        //            traitements["NeB"] = false;
        //            NeB.Background = Bckbrush_Autres13;
        //            traitements["Ro"] = false;
        //            Ro.Background = Bckbrush_Autres13;
        //            traitements["AeR"] = false;
        //            AeR.Background = Bckbrush_Autres13;
        //            traitements["Em"] = false;
        //            Em.Background = Bckbrush_Autres13;
        //            traitements["DdC"] = false;
        //            DdC.Background = Bckbrush_Autres13;
        //            traitements["RdB"] = false;
        //            RdB.Background = Bckbrush_Autres13;
        //            traitements["Fl"] = false;
        //            Fl.Background = Bckbrush_Autres13;
        //            traitements["Rpg"] = false;
        //            Rpg.Background = Bckbrush_Autres13;
        //            traitements["Frl"] = false;
        //            Frl.Background = Bckbrush_Autres13;
        //            traitements["Hst"] = false;
        //            Hst.Background = Bckbrush_Autres13;
        //            traitements["Co"] = false;
        //            Co.Background = Bckbrush_Autres13;
        //            traitements["Dc"] = false;
        //            Dc.Background = Bckbrush_Autres13;

        //            traitements["Cr" ]= false;
        //            Cr.Background = Bckbrush_Autres13;
        //            traitements["Cry"] = false;
        //            Cry.Background = Bckbrush_Autres13;
        //            traitements["Dcr"] = false;
        //            Dcr.Background = Bckbrush_Autres13;
        //            break;
        //        case "Cry":
        //            Color Bckcolor14 = Color.FromArgb(255, 50, 50, 50);
        //            SolidColorBrush Bckbrush14 = new SolidColorBrush(Bckcolor14);
        //            traitements["Cry"] = true;
        //            Cry.Background = Bckbrush14;

        //            Color BckColor_Autres14 = Color.FromArgb(255, 75, 75, 75);
        //            SolidColorBrush Bckbrush_Autres14 = new SolidColorBrush(BckColor_Autres14);

        //            traitements["NeB"] = false;
        //            NeB.Background = Bckbrush_Autres14;
        //            traitements["Ro"] = false;
        //            Ro.Background = Bckbrush_Autres14;
        //            traitements["AeR"] = false;
        //            AeR.Background = Bckbrush_Autres14;
        //            traitements["Em"] = false;
        //            Em.Background = Bckbrush_Autres14;
        //            traitements["DdC"] = false;
        //            DdC.Background = Bckbrush_Autres14;
        //            traitements["RdB"] = false;
        //            RdB.Background = Bckbrush_Autres14;
        //            traitements["Fl"] = false;
        //            Fl.Background = Bckbrush_Autres14;
        //            traitements["Rpg"] = false;
        //            Rpg.Background = Bckbrush_Autres14;
        //            traitements["Frl"] = false;
        //            Frl.Background = Bckbrush_Autres14;
        //            traitements["Hst"] = false;
        //            Hst.Background = Bckbrush_Autres14;
        //            traitements["Co"] = false;
        //            Co.Background = Bckbrush_Autres14;
        //            traitements["Dc"] = false;
        //            Dc.Background = Bckbrush_Autres14;
        //            traitements["Cr"] = false;
        //            Cr.Background = Bckbrush_Autres14;
        //            traitements["Le"] = false;
        //            Le.Background = Bckbrush_Autres14;

        //            traitements["Dcr"] = false;
        //            Dcr.Background = Bckbrush_Autres14;
        //            break;
        //        case "Dcr":
        //            Color Bckcolor16 = Color.FromArgb(255, 50, 50, 50);
        //            SolidColorBrush Bckbrush16 = new SolidColorBrush(Bckcolor16);
        //            traitements["Dcr"] = true;
        //            Dcr.Background = Bckbrush16;

        //            Color BckColor_Autres16 = Color.FromArgb(255, 75, 75, 75);
        //            SolidColorBrush Bckbrush_Autres16 = new SolidColorBrush(BckColor_Autres16);

        //            traitements["NeB"] = false;
        //            NeB.Background = Bckbrush_Autres16;
        //            traitements["Ro"] = false;
        //            Ro.Background = Bckbrush_Autres16;
        //            traitements["AeR"] = false;
        //            AeR.Background = Bckbrush_Autres16;
        //            traitements["Em"] = false;
        //            Em.Background = Bckbrush_Autres16;
        //            traitements["DdC"] = false;
        //            DdC.Background = Bckbrush_Autres16;
        //            traitements["RdB"] = false;
        //            RdB.Background = Bckbrush_Autres16;
        //            traitements["Fl"] = false;
        //            Fl.Background = Bckbrush_Autres16;
        //            traitements["Rpg"] = false;
        //            Rpg.Background = Bckbrush_Autres16;
        //            traitements["Frl"] = false;
        //            Frl.Background = Bckbrush_Autres16;
        //            traitements["Hst"] = false;
        //            Hst.Background = Bckbrush_Autres16;
        //            traitements["Co"] = false;
        //            Co.Background = Bckbrush_Autres16;
        //            traitements["Dc"] = false;
        //            Dc.Background = Bckbrush_Autres16;
        //            traitements["Cr"] = false;
        //            Cr.Background = Bckbrush_Autres16;
        //            traitements["Le"] = false;
        //            Le.Background = Bckbrush_Autres16;
        //            traitements["Cry"] = false;
        //            Cry.Background = Bckbrush_Autres16;

        //            break;
        //    }

           
        //}
        public void Image_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if(openFileDialog.ShowDialog() == true)
            {
                imagePath = openFileDialog.FileName;
            }
            
            ImageBox.Source = new BitmapImage(new Uri(imagePath));
        }
        public void Save_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                savePath = openFileDialog.FileName;
            }
        }
    }
}
