
using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using  TD_1;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Collections.Generic;
using System.Windows.Input;

namespace AppProjetSemestre4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string traitement;
        private string imagePath;
        private string savePath;
        private static double angle;
        private static bool sens;
        private Dictionary<string,bool> traitements= new Dictionary<string,bool>();
        Window1 win1;
        public static double Angle 
        {
            get { return angle; }
            set { angle = value; } 
        }
        public static bool Sens
        {
            get { return sens; }
            set { sens = value; }
        }
        
        public MainWindow()
        {
            InitializeComponent();
            
        }
       
        private void RunRotation(object sender, EventArgs e)
        {
            MyImage imageRo = new MyImage(imagePath);
            imageRo.RotationV2(angle, sens).ToFile(savePath);
            ImageBox.Source = new BitmapImage(new Uri(savePath));
        }
        public void Lancer_Click(object sender, RoutedEventArgs e)
        {
            switch (traitement)
            {
                case "NeB":
                    MyImage imageNeB = new MyImage(imagePath);
                    imageNeB.CouleurToNoiretBlanc().ToFile(savePath);
                    ImageBox.Source = new BitmapImage(new Uri(savePath));
                    break;
                case "Aer":
                    MyImage imageAeR = new MyImage(imagePath);
                    //imageAeR..ToFile(savePath);
                    break;
                case "Ro":
                    win1 = new Window1();
                    win1.Closed += new EventHandler(RunRotation);
                    win1.Show();
                    break;
                case "Em":
                    MyImage imageEm = new MyImage(imagePath);
                    imageEm.EffetMiroir().ToFile(savePath);
                    ImageBox.Source = new BitmapImage(new Uri(savePath));
                    break;
                case "Mc":
                    //MyImage imageMc = new MyImage(imagePath);
                    //imageMc.CouleurToNoiretBlanc().ToFile(savePath);
                    break;
                case "Cr":
                    //MyImage imageCr = new MyImage(imagePath);
                    //imageCr.CouleurToNoiretBlanc().ToFile(savePath);
                    break;
                case "Le":
                    //MyImage imageLe = new MyImage(imagePath);
                    //imageLe.CouleurToNoiretBlanc().ToFile(savePath);
                    break;
            }
        }
        public void Traitement_Click(object sender, RoutedEventArgs e)
        {
            
            traitement = ((TreeViewItem)sender).Name;

            switch (((TreeViewItem)sender).Name)// Verifier les noms
            {
                case "NeB":
                    traitements["NeB"] = true;
                    Color Bckcolor1 = Color.FromArgb(255, 50, 50, 50);
                    SolidColorBrush Bckbrush1 = new SolidColorBrush(Bckcolor1);
                    NeB.Background = Bckbrush1;

                    Color BckColor_Autres1 = Color.FromArgb(255, 75, 75, 75);
                    SolidColorBrush Bckbrush_Autres1 = new SolidColorBrush(BckColor_Autres1);
                    traitements["Ro"] = false;
                    Ro.Background = Bckbrush_Autres1;
                    traitements["AeR"] = false;
                    AeR.Background = Bckbrush_Autres1;
                    traitements["Em"] = false;
                    Em.Background = Bckbrush_Autres1;
                    traitements["DdC"] = false;
                    DdC.Background = Bckbrush_Autres1;
                    traitements["RdB"] = false;
                    RdB.Background = Bckbrush_Autres1;
                    traitements["Fl"] = false;
                    Fl.Background = Bckbrush_Autres1;
                    traitements["Rpg"] = false;
                    Rpg.Background = Bckbrush_Autres1;
                    traitements["Frl"] = false;
                    Frl.Background = Bckbrush_Autres1;
                    traitements["Hst"] = false;
                    Hst.Background = Bckbrush_Autres1;
                    traitements["CeD"] = false;
                    CeD.Background = Bckbrush_Autres1;
                    traitements["Cr"] = false;
                    Cr.Background = Bckbrush_Autres1;
                    traitements["Le"] = false;
                    Le.Background = Bckbrush_Autres1;
                    traitements["Cry"] = false;
                    Cry.Background = Bckbrush_Autres1;
                    break;
                case "Ro":
                    
                    Color Bckcolor2 = Color.FromArgb(255, 50, 50, 50);
                    SolidColorBrush Bckbrush2 = new SolidColorBrush(Bckcolor2);
                    traitements["Ro"] = true;
                    Ro.Background = Bckbrush2;

                    Color BckColor_Autres2 = Color.FromArgb(255, 75, 75, 75);
                    SolidColorBrush Bckbrush_Autres2 = new SolidColorBrush(BckColor_Autres2);
                    
                    traitements["NeB"] = false;
                    NeB.Background = Bckbrush_Autres2;
                    traitements["AeR"] = false;
                    AeR.Background = Bckbrush_Autres2;
                    traitements["Em"] = false;
                    Em.Background = Bckbrush_Autres2;
                    traitements["DdC"] = false;
                    DdC.Background = Bckbrush_Autres2;
                    traitements["RdB"] = false;
                    RdB.Background = Bckbrush_Autres2;
                    traitements["Fl"] = false;
                    Fl.Background = Bckbrush_Autres2;
                    traitements["Rpg"] = false;
                    Rpg.Background = Bckbrush_Autres2;
                    traitements["Frl"] = false;
                    Frl.Background = Bckbrush_Autres2;
                    traitements["Hst"] = false;
                    Hst.Background = Bckbrush_Autres2;
                    traitements["CeD"] = false;
                    CeD.Background = Bckbrush_Autres2;
                    traitements["Cr"] = false;
                    Cr.Background = Bckbrush_Autres2;
                    traitements["Le"] = false;
                    Le.Background = Bckbrush_Autres2;
                    traitements["Cry"] = false;
                    Cry.Background = Bckbrush_Autres2;
                    break;
                case "AeR":
                    Color Bckcolor3 = Color.FromArgb(255, 50, 50, 50);
                    SolidColorBrush Bckbrush3 = new SolidColorBrush(Bckcolor3);
                    traitements["AeR"] = true;
                    AeR.Background = Bckbrush3;

                    Color BckColor_Autres3 = Color.FromArgb(255, 75, 75, 75);
                    SolidColorBrush Bckbrush_Autres3 = new SolidColorBrush(BckColor_Autres3);

                    traitements["NeB"] = false;
                    NeB.Background = Bckbrush_Autres3;
                    traitements["Ro"] = false;
                    Ro.Background = Bckbrush_Autres3;
                    traitements["Em"] = false;
                    Em.Background = Bckbrush_Autres3;
                    traitements["DdC"] = false;
                    DdC.Background = Bckbrush_Autres3;
                    traitements["RdB"] = false;
                    RdB.Background = Bckbrush_Autres3;
                    traitements["Fl"] = false;
                    Fl.Background = Bckbrush_Autres3;
                    traitements["Rpg"] = false;
                    Rpg.Background = Bckbrush_Autres3;
                    traitements["Frl"] = false;
                    Frl.Background = Bckbrush_Autres3;
                    traitements["Hst"] = false;
                    Hst.Background = Bckbrush_Autres3;
                    traitements["CeD"] = false;
                    CeD.Background = Bckbrush_Autres3;
                    traitements["Cr"] = false;
                    Cr.Background = Bckbrush_Autres3;
                    traitements["Le"] = false;
                    Le.Background = Bckbrush_Autres3;
                    traitements["Cry"] = false;
                    Cry.Background = Bckbrush_Autres3;
                    break;
                case "Em":
                    Color Bckcolor4 = Color.FromArgb(255, 50, 50, 50);
                    SolidColorBrush Bckbrush4 = new SolidColorBrush(Bckcolor4);
                    traitements["Em"] = true;
                    Em.Background = Bckbrush4;

                    Color BckColor_Autres4 = Color.FromArgb(255, 75, 75, 75);
                    SolidColorBrush Bckbrush_Autres4 = new SolidColorBrush(BckColor_Autres4);

                    traitements["NeB"] = false;
                    NeB.Background = Bckbrush_Autres4;
                    traitements["Ro"] = false;
                    Ro.Background = Bckbrush_Autres4;
                    traitements["AeR"] = false;
                    AeR.Background = Bckbrush_Autres4;
                    traitements["Em"] = false;
                    Em.Background = Bckbrush_Autres4;
                    traitements["DdC"] = false;
                    DdC.Background = Bckbrush_Autres4;
                    traitements["RdB"] = false;
                    RdB.Background = Bckbrush_Autres4;
                    traitements["Fl"] = false;
                    Fl.Background = Bckbrush_Autres4;
                    traitements["Rpg"] = false;
                    Rpg.Background = Bckbrush_Autres4;
                    traitements["Frl"] = false;
                    Frl.Background = Bckbrush_Autres4;
                    traitements["Hst"] = false;
                    Hst.Background = Bckbrush_Autres4;
                    traitements["CeD"] = false;
                    CeD.Background = Bckbrush_Autres4;
                    traitements["Cr"] = false;
                    Cr.Background = Bckbrush_Autres4;
                    traitements["Le"] = false;
                    Le.Background = Bckbrush_Autres4;
                    traitements["Cry"] = false;
                    Cry.Background = Bckbrush_Autres4;
                    break;
                case "DdC":
                    Color Bckcolor5 = Color.FromArgb(255, 50, 50, 50);
                    SolidColorBrush Bckbrush5 = new SolidColorBrush(Bckcolor5);
                    traitements["DdC"] = true;
                    DdC.Background = Bckbrush5;
                    Color BckColor_Autres5 = Color.FromArgb(255, 75, 75, 75);
                    SolidColorBrush Bckbrush_Autres5 = new SolidColorBrush(BckColor_Autres5);

                    traitements["NeB"] = false;
                    NeB.Background = Bckbrush_Autres5;
                    traitements["Ro"] = false;
                    Ro.Background = Bckbrush_Autres5;
                    traitements["AeR"] = false;
                    AeR.Background = Bckbrush_Autres5;
                    traitements["Em"] = false;
                    Em.Background = Bckbrush_Autres5;
                    
                    traitements["RdB"] = false;
                    RdB.Background = Bckbrush_Autres5;
                    traitements["Fl"] = false;
                    Fl.Background = Bckbrush_Autres5;
                    traitements["Rpg"] = false;
                    Rpg.Background = Bckbrush_Autres5;
                    traitements["Frl"] = false;
                    Frl.Background = Bckbrush_Autres5;
                    traitements["Hst"] = false;
                    Hst.Background = Bckbrush_Autres5;
                    traitements["CeD"] = false;
                    CeD.Background = Bckbrush_Autres5;
                    traitements["Cr"] = false;
                    Cr.Background = Bckbrush_Autres5;
                    traitements["Le"] = false;
                    Le.Background = Bckbrush_Autres5;
                    traitements["Cry"] = false;
                    Cry.Background = Bckbrush_Autres5;
                    break;
                case "RdB":
                    Color Bckcolor6 = Color.FromArgb(255, 50, 50, 50);
                    SolidColorBrush Bckbrush6 = new SolidColorBrush(Bckcolor6);
                    traitements["RdB"] = true;
                    RdB.Background = Bckbrush6;
                    Color BckColor_Autres6 = Color.FromArgb(255, 75, 75, 75);
                    SolidColorBrush Bckbrush_Autres6 = new SolidColorBrush(BckColor_Autres6);

                    traitements["NeB"] = false;
                    NeB.Background = Bckbrush_Autres6;
                    traitements["Ro"] = false;
                    Ro.Background = Bckbrush_Autres6;
                    traitements["AeR"] = false;
                    AeR.Background = Bckbrush_Autres6;
                    traitements["Em"] = false;
                    Em.Background = Bckbrush_Autres6;
                    traitements["DdC"] = false;
                    DdC.Background = Bckbrush_Autres6;
                    
                    traitements["Fl"] = false;
                    Fl.Background = Bckbrush_Autres6;
                    traitements["Rpg"] = false;
                    Rpg.Background = Bckbrush_Autres6;
                    traitements["Frl"] = false;
                    Frl.Background = Bckbrush_Autres6;
                    traitements["Hst"] = false;
                    Hst.Background = Bckbrush_Autres6;
                    traitements["CeD"] = false;
                    CeD.Background = Bckbrush_Autres6;
                    traitements["Cr"] = false;
                    Cr.Background = Bckbrush_Autres6;
                    traitements["Le"] = false;
                    Le.Background = Bckbrush_Autres6;
                    traitements["Cry"] = false;
                    Cry.Background = Bckbrush_Autres6;
                    break;
                case "Fl":
                    Color Bckcolor7= Color.FromArgb(255, 50, 50, 50);
                    SolidColorBrush Bckbrush7 = new SolidColorBrush(Bckcolor7);
                    traitements["Fl"] = true;
                    Fl.Background = Bckbrush7;
                    Color BckColor_Autres7 = Color.FromArgb(255, 75, 75, 75);
                    SolidColorBrush Bckbrush_Autres7 = new SolidColorBrush(BckColor_Autres7);

                    traitements["NeB"] = false;
                    NeB.Background = Bckbrush_Autres7;
                    traitements["Ro"] = false;
                    Ro.Background = Bckbrush_Autres7;
                    traitements["AeR"] = false;
                    AeR.Background = Bckbrush_Autres7;
                    traitements["Em"] = false;
                    Em.Background = Bckbrush_Autres7;
                    traitements["DdC"] = false;
                    DdC.Background = Bckbrush_Autres7;
                    traitements["RdB"] = false;
                    RdB.Background = Bckbrush_Autres7;
                    
                    traitements["Rpg"] = false;
                    Rpg.Background = Bckbrush_Autres7;
                    traitements["Frl"] = false;
                    Frl.Background = Bckbrush_Autres7;
                    traitements["Hst"] = false;
                    Hst.Background = Bckbrush_Autres7;
                    traitements["CeD"] = false;
                    CeD.Background = Bckbrush_Autres7;
                    traitements["Cr"] = false;
                    Cr.Background = Bckbrush_Autres7;
                    traitements["Le"] = false;
                    Le.Background = Bckbrush_Autres7;
                    traitements["Cry"] = false;
                    Cry.Background = Bckbrush_Autres7;
                    break;
                case "Rpg":
                    Color Bckcolor8 = Color.FromArgb(255, 50, 50, 50);
                    SolidColorBrush Bckbrush8 = new SolidColorBrush(Bckcolor8);
                    traitements["Rpg"] = true;
                    Rpg.Background = Bckbrush8;

                    Color BckColor_Autres8 = Color.FromArgb(255, 75, 75, 75);
                    SolidColorBrush Bckbrush_Autres8 = new SolidColorBrush(BckColor_Autres8);

                    traitements["NeB"] = false;
                    NeB.Background = Bckbrush_Autres8;
                    traitements["Ro"] = false;
                    Ro.Background = Bckbrush_Autres8;
                    traitements["AeR"] = false;
                    AeR.Background = Bckbrush_Autres8;
                    traitements["Em"] = false;
                    Em.Background = Bckbrush_Autres8;
                    traitements["DdC"] = false;
                    DdC.Background = Bckbrush_Autres8;
                    traitements["RdB"] = false;
                    RdB.Background = Bckbrush_Autres8;
                    traitements["Fl"] = false;
                    Fl.Background = Bckbrush_Autres8;
                    
                    traitements["Frl"] = false;
                    Frl.Background = Bckbrush_Autres8;
                    traitements["Hst"] = false;
                    Hst.Background = Bckbrush_Autres8;
                    traitements["CeD"] = false;
                    CeD.Background = Bckbrush_Autres8;
                    traitements["Cr"] = false;
                    Cr.Background = Bckbrush_Autres8;
                    traitements["Le"] = false;
                    Le.Background = Bckbrush_Autres8;
                    traitements["Cry"] = false;
                    Cry.Background = Bckbrush_Autres8;
                    break;
                case "Frl":
                    Color Bckcolor9 = Color.FromArgb(255, 50, 50, 50);
                    SolidColorBrush Bckbrush9 = new SolidColorBrush(Bckcolor9);
                    traitements["Frl"] = true;
                    Frl.Background = Bckbrush9;

                    Color BckColor_Autres9 = Color.FromArgb(255, 75, 75, 75);
                    SolidColorBrush Bckbrush_Autres9 = new SolidColorBrush(BckColor_Autres9);

                    traitements["NeB"] = false;
                    NeB.Background = Bckbrush_Autres9;
                    traitements["Ro"] = false;
                    Ro.Background = Bckbrush_Autres9;
                    traitements["AeR"] = false;
                    AeR.Background = Bckbrush_Autres9;
                    traitements["Em"] = false;
                    Em.Background = Bckbrush_Autres9;
                    traitements["DdC"] = false;
                    DdC.Background = Bckbrush_Autres9;
                    traitements["RdB"] = false;
                    RdB.Background = Bckbrush_Autres9;
                    traitements["Fl"] = false;
                    Fl.Background = Bckbrush_Autres9;
                    traitements["Rpg"] = false;
                    Rpg.Background = Bckbrush_Autres9;
                    
                    traitements["Hst"] = false;
                    Hst.Background = Bckbrush_Autres9;
                    traitements["CeD"] = false;
                    CeD.Background = Bckbrush_Autres9;
                    traitements["Cr"] = false;
                    Cr.Background = Bckbrush_Autres9;
                    traitements["Le"] = false;
                    Le.Background = Bckbrush_Autres9;
                    traitements["Cry"] = false;
                    Cry.Background = Bckbrush_Autres9;
                    break;
                case "Hst":
                    Color Bckcolor10 = Color.FromArgb(255, 50, 50, 50);
                    SolidColorBrush Bckbrush10 = new SolidColorBrush(Bckcolor10);
                    traitements["Hst"] = true;
                    Hst.Background = Bckbrush10;

                    Color BckColor_Autres10 = Color.FromArgb(255, 75, 75, 75);
                    SolidColorBrush Bckbrush_Autres10= new SolidColorBrush(BckColor_Autres10);

                    traitements["NeB"] = false;
                    NeB.Background = Bckbrush_Autres10;
                    traitements["Ro"] = false;
                    Ro.Background = Bckbrush_Autres10;
                    traitements["AeR"] = false;
                    AeR.Background = Bckbrush_Autres10;
                    traitements["Em"] = false;
                    Em.Background = Bckbrush_Autres10;
                    traitements["DdC"] = false;
                    DdC.Background = Bckbrush_Autres10;
                    traitements["RdB"] = false;
                    RdB.Background = Bckbrush_Autres10;
                    traitements["Fl"] = false;
                    Fl.Background = Bckbrush_Autres10;
                    traitements["Rpg"] = false;
                    Rpg.Background = Bckbrush_Autres10;
                    traitements["Frl"] = false;
                    Frl.Background = Bckbrush_Autres10;
                    traitements["CeD"] = false;
                    CeD.Background = Bckbrush_Autres10;
                    traitements["Cr"] = false;
                    Cr.Background = Bckbrush_Autres10;
                    traitements["Le"] = false;
                    Le.Background = Bckbrush_Autres10;
                    traitements["Cry"] = false;
                    Cry.Background = Bckbrush_Autres10;
                    break;
                case "CeD":
                    Color Bckcolor11 = Color.FromArgb(255, 50, 50, 50);
                    SolidColorBrush Bckbrush11 = new SolidColorBrush(Bckcolor11);
                    traitements["CeD"] = true;
                    CeD.Background = Bckbrush11;

                    Color BckColor_Autres11 = Color.FromArgb(255, 75, 75, 75);
                    SolidColorBrush Bckbrush_Autres11 = new SolidColorBrush(BckColor_Autres11);

                    traitements["NeB"] = false;
                    NeB.Background = Bckbrush_Autres11;
                    traitements["Ro"] = false;
                    Ro.Background = Bckbrush_Autres11;
                    traitements["AeR"] = false;
                    AeR.Background = Bckbrush_Autres11;
                    traitements["Em"] = false;
                    Em.Background = Bckbrush_Autres11;
                    traitements["DdC"] = false;
                    DdC.Background = Bckbrush_Autres11;
                    traitements["RdB"] = false;
                    RdB.Background = Bckbrush_Autres11;
                    traitements["Fl"] = false;
                    Fl.Background = Bckbrush_Autres11;
                    traitements["Rpg"] = false;
                    Rpg.Background = Bckbrush_Autres11;
                    traitements["Frl"] = false;
                    Frl.Background = Bckbrush_Autres11;
                    traitements["Hst"] = false;
                    Hst.Background = Bckbrush_Autres11;
                   
                    traitements["Cr"] = false;
                    Cr.Background = Bckbrush_Autres11;
                    traitements["Le"] = false;
                    Le.Background = Bckbrush_Autres11;
                    traitements["Cry"] = false;
                    Cry.Background = Bckbrush_Autres11;
                    break;
                case "Cr":
                    Color Bckcolor12 = Color.FromArgb(255, 50, 50, 50);
                    SolidColorBrush Bckbrush12 = new SolidColorBrush(Bckcolor11);
                    traitements["Cr"] = true;
                    Cr.Background = Bckbrush12;

                    Color BckColor_Autres12 = Color.FromArgb(255, 75, 75, 75);
                    SolidColorBrush Bckbrush_Autres12 = new SolidColorBrush(BckColor_Autres12);

                    traitements["NeB"] = false;
                    NeB.Background = Bckbrush_Autres12;
                    traitements["Ro"] = false;
                    Ro.Background = Bckbrush_Autres12;
                    traitements["AeR"] = false;
                    AeR.Background = Bckbrush_Autres12;
                    traitements["Em"] = false;
                    Em.Background = Bckbrush_Autres12;
                    traitements["DdC"] = false;
                    DdC.Background = Bckbrush_Autres12;
                    traitements["RdB"] = false;
                    RdB.Background = Bckbrush_Autres12;
                    traitements["Fl"] = false;
                    Fl.Background = Bckbrush_Autres12;
                    traitements["Rpg"] = false;
                    Rpg.Background = Bckbrush_Autres12;
                    traitements["Frl"] = false;
                    Frl.Background = Bckbrush_Autres12;
                    traitements["Hst"] = false;
                    Hst.Background = Bckbrush_Autres12;
                    traitements["CeD"] = false;
                    CeD.Background = Bckbrush_Autres12;
                    
                    traitements["Le"] = false;
                    Le.Background = Bckbrush_Autres12;
                    traitements["Cry"] = false;
                    Cry.Background = Bckbrush_Autres12;
                    break;
                case "Le":
                    Color Bckcolor13 = Color.FromArgb(255, 50, 50, 50);
                    SolidColorBrush Bckbrush13 = new SolidColorBrush(Bckcolor13);
                    traitements["Le"] = true;
                    Le.Background = Bckbrush13;

                    Color BckColor_Autres13 = Color.FromArgb(255, 75, 75, 75);
                    SolidColorBrush Bckbrush_Autres13 = new SolidColorBrush(BckColor_Autres13);

                    traitements["NeB"] = false;
                    NeB.Background = Bckbrush_Autres13;
                    traitements["Ro"] = false;
                    Ro.Background = Bckbrush_Autres13;
                    traitements["AeR"] = false;
                    AeR.Background = Bckbrush_Autres13;
                    traitements["Em"] = false;
                    Em.Background = Bckbrush_Autres13;
                    traitements["DdC"] = false;
                    DdC.Background = Bckbrush_Autres13;
                    traitements["RdB"] = false;
                    RdB.Background = Bckbrush_Autres13;
                    traitements["Fl"] = false;
                    Fl.Background = Bckbrush_Autres13;
                    traitements["Rpg"] = false;
                    Rpg.Background = Bckbrush_Autres13;
                    traitements["Frl"] = false;
                    Frl.Background = Bckbrush_Autres13;
                    traitements["Hst"] = false;
                    Hst.Background = Bckbrush_Autres13;
                    traitements["CeD"] = false;
                    CeD.Background = Bckbrush_Autres13;
                    traitements["Cr"] = false;
                    Cr.Background = Bckbrush_Autres13;
                    
                    traitements["Cry"] = false;
                    Cry.Background = Bckbrush_Autres13;
                    break;
                case "Cry":
                    Color Bckcolor14 = Color.FromArgb(255, 50, 50, 50);
                    SolidColorBrush Bckbrush14 = new SolidColorBrush(Bckcolor14);
                    traitements["Cry"] = true;
                    Cry.Background = Bckbrush14;

                    Color BckColor_Autres14 = Color.FromArgb(255, 75, 75, 75);
                    SolidColorBrush Bckbrush_Autres14 = new SolidColorBrush(BckColor_Autres14);

                    traitements["NeB"] = false;
                    NeB.Background = Bckbrush_Autres14;
                    traitements["Ro"] = false;
                    Ro.Background = Bckbrush_Autres14;
                    traitements["AeR"] = false;
                    AeR.Background = Bckbrush_Autres14;
                    traitements["Em"] = false;
                    Em.Background = Bckbrush_Autres14;
                    traitements["DdC"] = false;
                    DdC.Background = Bckbrush_Autres14;
                    traitements["RdB"] = false;
                    RdB.Background = Bckbrush_Autres14;
                    traitements["Fl"] = false;
                    Fl.Background = Bckbrush_Autres14;
                    traitements["Rpg"] = false;
                    Rpg.Background = Bckbrush_Autres14;
                    traitements["Frl"] = false;
                    Frl.Background = Bckbrush_Autres14;
                    traitements["Hst"] = false;
                    Hst.Background = Bckbrush_Autres14;
                    traitements["CeD"] = false;
                    CeD.Background = Bckbrush_Autres14;
                    traitements["Cr"] = false;
                    Cr.Background = Bckbrush_Autres14;
                    traitements["Le"] = false;
                    Le.Background = Bckbrush_Autres14;
                    break;
            }

           
        }
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
