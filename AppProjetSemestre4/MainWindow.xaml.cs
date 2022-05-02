
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
using System.IO;

namespace AppProjetSemestre4
{

    /*Fait:
        - Garder en mémoire l'image source et la sortie pour pouvoir faire un autre traitement sans resélectionner              OK
                - Créer automatiquement différents fichiers de sortie pour les différents traitements                           OK  
        pb avec l'image par défaut, elle est considérée nnull qd le diretory de sortie a pas été défini                         OK il faut nécéssairement choisir une image maintenant image par défaut seulement pour le style de la fenetre
        - Possibilité de faire plusieurs traitements à la fois                                                                  OK
                - Gérer lancer                                                                                                  OK
        Fractale à impémenter : menu déroulant a finir peut etre un bouton qui fait apparaitre un Sp de bouton, code intéraction + back    OK
    sur le image_click parfois mainWindow.ImageBox.Source sort ue nullexception avec mainWindow null qd on selectionnne une image                               OK placer le owner = null ds le if blabla ok on ferme
    Retrecissement sort div par 0 qd poucentage = 50                                ok je suis unn débile
    Parfois en tapant le bouton gauche dragmove est activé et renvoie "La méthode DragMove ne peut être appelée que lorsque le bouton principal de la souris est enfoncé"   ca active MainWindow_MouseDow qui active dragmove       Geré A voir au cours de l'utilisation pas de pb poour l'instant    : la event pour activé doit etre MouseLeftDown le bouton gauche pas le droit doit activé la méthode   Nnormalement OK
        */


    /*Reste à faire
        

        Gérer les possibles pb d'IO avec try Catch + messagebox
        POssibilité de fermer les fenetres secondaires sans rien rentrer dedans et sans crash --> soit avoir une valeur par défaut soit try catch + message pour indiquer que l'opération sélectionner ne pourra pas etre réalisée                          En cours jouer avec l'interface pour trouver
                
                        -------->               Bien avancé géré sur entrée du dossier save + entrée image + entrée porcent angle coeff, ...


       
        - aide mode d'emploi (possiblité d'avoir des tips qui apparaissent qd on hover sur un controle (ToolTip propriété))
                Indiquer que par défaut fractale fait le dernier fractale sélectionné ou l'index 0
                Pour Coder Décoder, l'image cachée ne doit pas être trop petite par rapport à l'image cachette sinon on ne la voie pas 
        
       


        Attention a quelles actions peuvent etre faites les unes apres les autres      avoir un bool qui indique si un autre traitement peu etre fait pour bloquer le après (rien après décodage)   et queue count pour verifier que avant bbon

     */

    /*Pas important
     - créer des nouvelles images pr croix fermer et trait pour avoir les symboles en blanc
        - Activable depuis un menu option
        - Resize Soit technique des tailles en mode auto soit calculer le ratio de resize et l'appliquer a ttes les tailles    Commencé à verif mettre une taille min mainwindow le menu resize pas
    Bouton croix pour fermer les fenetres secondaires sans faire d'autres actions
     */


    /*A faire projet entier
     Clean le code 
            Peut impliqué de refaire certaines chose 
     Rétrecissement revoir,                                                                         OK
     rotation peut etre                                                                             Fonctioonne on touche pas
     Histogramme Faudrait faire en sorte que a sortie n'ai pas une hauteur abérrante                la mm


     Test unitaire 
     
     */


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static string imagePath = "/foret riviere.bmp";
        private static string imageName= "";
        private static string savePath;
        private static double angle;

        private static bool sens;
        private static int pourcent_AeR;
        private static int coefficient_flou;
        private static string imageCachéepath;
        private static string textQR = "";
        private Queue<string> queue_fonctions = new Queue<string>();
        private int coté;
        private bool fractale_Random = false;
        private int index_Fractale = 0;
        private bool fractale_Personnalisé = false;
        private double reel_Personnalisé;
        private double im_Personnalisé;
        private int itérations_Personnalisé;
        private int mult_Couleurs_Personnalisé;

        SolidColorBrush bckBrushPressed;
        SolidColorBrush bckBrush;

        bool[] button_pressed = new bool[14]; 

        
        #region accesseur
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
        public static string ImageCachéePath
        {
            get { return imageCachéepath; }
            set { imageCachéepath = value; }
        }
        public static string SavePath
        {
            get { return savePath; }
            set { savePath = value; }
        }
        public static string TextQR
        {
            get { return textQR; }
            set { textQR = value; }
        }
        public static int Pourcent_AeR
        {
            get { return pourcent_AeR; }
            set { pourcent_AeR = value; }
        }
        public int Coté
        {
            get { return coté; }
            set { coté = value; }
        }
        public bool Fractale_Random
        {
            get { return fractale_Random; }
          
            set { fractale_Random = value; }
        }
        public bool Fractale_Personnalisé
        {
            get { return fractale_Personnalisé; }
            set { fractale_Personnalisé = value; }
        }
        public int Index_Fractale
        {
            get { return index_Fractale; }
            set { index_Fractale = value; }
        }
        public int Mult_Couleurs_Personnalisé
        {
            get { return mult_Couleurs_Personnalisé; }
            set { mult_Couleurs_Personnalisé = value; }
        }
        public int Itérations_Personnalisé
        {
            get { return itérations_Personnalisé; }
            set { itérations_Personnalisé = value; }
        }
        public double Reel_Personnalisé
        {
            get { return reel_Personnalisé; }
            set { reel_Personnalisé = value; }
        }
        public double Im_Personnalisé
        {
            get { return im_Personnalisé; }
            set { im_Personnalisé = value; }
        }

        public static string ImageName
        {
            get { return imageName; }
            set { imageName = value; }
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
        #endregion

        public MainWindow()
        {
            InitializeComponent();

            this.Show();

            Window4 win4 = new Window4();
            win4.Owner = this;
            win4.Left = this.Left + this.Width / 2 - win4.Width/2;
            win4.Top = this.Top + this.Height / 2  - win4.Height/2;
            win4.Show();

            Color bckColorPressed = Color.FromArgb(255, 50, 50, 50);
            Color bckColor = Color.FromArgb(255, 75, 75, 75);
            bckBrushPressed = new SolidColorBrush(bckColorPressed);
            bckBrush = new SolidColorBrush(bckColor);
        }
        
        public void MainWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch (System.InvalidOperationException) { }
            catch (Exception ex)
            { Console.WriteLine(ex.Message); }
        }

        public void FcnGénéral_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;

            Dictionary<string, int> Fcn_index = new Dictionary<string, int>() 
            {
                {"FcnNeB", 0 },
                {"FcnEm", 3 },
                {"FcnDdC", 4 },
                {"FcnRdB", 5 },
                {"FcnRpg", 7 },
                {"FcnHst", 9 },
                {"FcnDc", 11 },
                {"FcnLc",13 },

            };

            int index = Fcn_index[btn.Name];

            if (button_pressed[index] == true) //Tester le background du bouton aurait été mieux
            { 
                button_pressed[index] = false; 
                btn.Background = bckBrush;
                int count = queue_fonctions.Count;
                for(int i = 0; i< count; i++)
                {
                    string element = queue_fonctions.Dequeue();
                    if(element != btn.Name) { queue_fonctions.Enqueue(element); }
                }
                
            }
            else
            {
                if (!queue_fonctions.Contains("FcnLc"))
                {
                    Window3 win3 = new Window3();
                    win3.Owner = this;
                    win3.Left = win3.Owner.Left + win3.Owner.Width;
                    win3.Top = win3.Owner.Top;
                    win3.Show();
                    button_pressed[index] = true; btn.Background = bckBrushPressed;
                    queue_fonctions.Enqueue(btn.Name);
                }
            }
        }

        public void FcnRo_Click(object sender, RoutedEventArgs e)
        {

            if (button_pressed[1] == true) 
            { 
                button_pressed[1] = false; 
                FcnRo.Background = bckBrush;
                int count = queue_fonctions.Count;
                for (int i = 0; i < count; i++)
                {
                    string element = queue_fonctions.Dequeue();
                    if (element != "FcnRo") { queue_fonctions.Enqueue(element); }
                    
                }
                
            }
            else
            {
                if (!queue_fonctions.Contains("FcnLc"))
                {


                    Window1 win1 = new Window1();
                    win1.Owner = this;
                    win1.Left = win1.Owner.Left + win1.Owner.Width;
                    win1.Top = win1.Owner.Top;
                    win1.Show();
                    button_pressed[1] = true;
                    FcnRo.Background = bckBrushPressed;
                    queue_fonctions.Enqueue("FcnRo");
                }
            }

        }

        public void FcnAeR_Click(object sender, RoutedEventArgs e)
        {

            Button btn = (Button)sender;
            if (button_pressed[2] == true) 
            { 
                button_pressed[2] = false; 
                FcnAeR.Background = bckBrush;
                int count = queue_fonctions.Count;
                for (int i = 0; i < count; i++)
                {
                    string element = queue_fonctions.Dequeue();
                    if (element != "FcnAeR") { queue_fonctions.Enqueue(element); }
                }
                
            }
            else
            {
                if (!queue_fonctions.Contains("FcnLc"))
                {


                    Window2 win2 = new Window2();
                    win2.Owner = this;
                    win2.Left = win2.Owner.Left + win2.Owner.Width;
                    win2.Top = win2.Owner.Top;
                    win2.Show();
                    button_pressed[2] = true;
                    FcnAeR.Background = bckBrushPressed;
                    queue_fonctions.Enqueue("FcnAeR");
                }
            }
        }
        public void FcnFl_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if (button_pressed[6] == true) 
            { 
                button_pressed[6] = false; 
                FcnFl.Background = bckBrush;
                int count = queue_fonctions.Count;
                for (int i = 0; i < count; i++)
                {
                    string element = queue_fonctions.Dequeue();
                    if (element != "FcnFl") { queue_fonctions.Enqueue(element); }
                }
                
            }
            else
            {
                if (!queue_fonctions.Contains("FcnLc"))
                {


                    Window5 win5 = new Window5();
                    win5.Owner = this;
                    win5.Left = win5.Owner.Left + win5.Owner.Width;
                    win5.Top = win5.Owner.Top;
                    win5.Show();
                    button_pressed[6] = true;
                    FcnFl.Background = bckBrushPressed;
                    queue_fonctions.Enqueue("FcnFl");
                }
            }
        }

        public void FcnCo_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if (button_pressed[10] == true) 
            { 
                button_pressed[10] = false; 
                FcnCo.Background = bckBrush;
                int count = queue_fonctions.Count;
                for (int i = 0; i < count; i++)
                {
                    string element = queue_fonctions.Dequeue();
                    if (element != "FcnCo") { queue_fonctions.Enqueue(element); }
                }
                
            }
            else
            {
                if (!queue_fonctions.Contains("FcnLc"))
                {


                    Window6 win6 = new Window6();
                    win6.Owner = this;
                    win6.Left = win6.Owner.Left + win6.Owner.Width;
                    win6.Top = win6.Owner.Top;
                    win6.Show();
                    button_pressed[10] = true;
                    FcnCo.Background = bckBrushPressed;
                    queue_fonctions.Enqueue("FcnCo");
                }
            }
        }
        public void FcnFrl_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if (button_pressed[8] == true)
            {
                button_pressed[8] = false;
                FcnFrl.Background = bckBrush;
                int count = queue_fonctions.Count;
                for (int i = 0; i < count; i++)
                {
                    string element = queue_fonctions.Dequeue();
                    if (element != "FcnFrl") { queue_fonctions.Enqueue(element); }
                }

            }
            else
            {


                if (button_pressed[8] == false && queue_fonctions.Count == 0 && !queue_fonctions.Contains("FcnLc"))
                {
                    Fractale fractale = new Fractale();
                    fractale.Owner = this;
                    fractale.Left = fractale.Owner.Left + fractale.Owner.Width;
                    fractale.Top = fractale.Owner.Top;
                    fractale.Show();
                    button_pressed[8] = true;
                    FcnFrl.Background = bckBrushPressed;
                    queue_fonctions.Enqueue("FcnFrl");
                }
            }
        } 
        public void FcnCr_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if (button_pressed[12] == true)
            {
                button_pressed[12] = false;
                FcnCr.Background = bckBrush;
                int count = queue_fonctions.Count;
                for (int i = 0; i < count; i++)
                {
                    string element = queue_fonctions.Dequeue();
                    if (element != "FcnCr") { queue_fonctions.Enqueue(element); }
                }

            }
            else
            {
                if (button_pressed[12] == false && queue_fonctions.Count == 0 && !queue_fonctions.Contains("FcnLc"))
                {
                    CreationQR creationQR = new CreationQR();
                    creationQR.Owner = this;
                    creationQR.Left = creationQR.Owner.Left + creationQR.Owner.Width;
                    creationQR.Top = creationQR.Owner.Top;
                    creationQR.Show();
                    button_pressed[12] = true;
                    FcnCr.Background = bckBrushPressed;
                    queue_fonctions.Enqueue("FcnCr");

                }
            }
        }
        public void Lancer_Click(object sender, RoutedEventArgs e)//Gérer me cas ou la queue est vide
        {
            MyImage image = null;
            bool tests = true;
            try
            {
                image = new MyImage(ImagePath);
            }
            catch
            {
                MessageBox.Show("L'image sélectionnée est invalide ou aucune image n'est sélectionnée ", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                tests = false;
            }

            if (queue_fonctions.Count < 1)  // Cas ou queue vide
            {
                MessageBox.Show("Veuillez selectionner un traitement", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                tests = false;
            }

            if (tests)
            {
                MyImage image_fcn = null;
                string fonction = "";
                string nom_fichier;
                int queueCount = queue_fonctions.Count;

               


                for (int i = 0; i < queueCount; i++)
                {
                    fonction = queue_fonctions.Dequeue();

                    switch (fonction)
                    {
                        case "FcnNeB":
                            image_fcn = image.CouleurToNoiretBlanc();
                            break;
                        case "FcnRo":
                            image_fcn = image.RotationV2(angle, sens);
                            break;
                        case "FcnAeR":
                            if (pourcent_AeR >= 100)
                            {
                                image_fcn = image.Aggrandir((int)pourcent_AeR / 100);
                            }
                            else
                            {
                                image_fcn = image.Rétrecissement((int) 100/pourcent_AeR );
                            }
                            break;
                        case "FcnEm":
                            image_fcn = image.EffetMiroir();
                            break;
                        case "FcnDdC":
                            int[,] DdC = { { 0, 1, 0 }, { 1, -4, 1 }, { 0, 1, 0 } };
                            image_fcn = image.Convolution(DdC);
                            break;
                        case "FcnRdB":
                            int[,] RdB = { { 0, 0, 0 }, { 1, -1, 0 }, { 0, 0, 0 } };
                            image_fcn = image.Convolution(RdB);
                            break;
                        case "FcnRpg":
                            int[,] Repoussage = { { -2, -1, 0 }, { -1, 1, 1 }, { 0, 1, 2 } };
                            image_fcn = image.Convolution(Repoussage);
                            break;
                        case "FcnFl":
                            int[,] Flou = { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } };
                            image_fcn = image.Convolution(Flou);
                            break;
                        case "FcnFrl":
                            image_fcn = MyImage.FractaleJulia(coté, fractale_Random, index_Fractale, fractale_Personnalisé, reel_Personnalisé, im_Personnalisé, itérations_Personnalisé, mult_Couleurs_Personnalisé);
                            break;
                        case "FcnHst":
                            image_fcn = image.Histogramme();
                            break;
                        case "FcnCo":
                            MyImage imageCachée = new MyImage(imageCachéepath);
                            image_fcn = image.CacherImage_dans_Image(imageCachée);
                            break;
                        case "FcnDc":
                            image_fcn = image.DecoderImageCachee();
                            break;
                        case "FcnCr":
                            int version = 1;
                            if (textQR.Length <= 25) { version = 1; }
                            if (textQR.Length > 25 && textQR.Length <= 47) { version = 2; }
                            if (textQR.Length > 47)
                            {
                                MessageBox.Show("La phrase doit comprendre au maximum 47 caractères", "erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                            byte[] donnee = MyImage.Convertir_Chaine_Char(textQR.ToUpper(), version);
                            int[] masquage = { 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 0, 0, 1, 0, 0 };
                            image_fcn = MyImage.QRCode(version, masquage, donnee, true);

                            ImageBorder.MaxHeight = 150;
                            ImageBorder.MaxWidth = 150;
                            break;
                        case "FcnLc":
                            string message = image.Decoder_QRCode();
                            MessageBox.Show(message, "Message", MessageBoxButton.OK, MessageBoxImage.Information);

                            break;
                    }
                    image = image_fcn;
                }

                if (queueCount > 1) { fonction = "mixte"; }

                if (fonction != "FcnLc")
                {
                    string[] fichier_sortie_existants = Directory.GetFiles(savePath, fonction + "*");
                    nom_fichier = fonction + Convert.ToString(fichier_sortie_existants.Length);
                    image.ToFile(savePath + "\\" + nom_fichier + ".bmp");
                    ImageBox.Source = new BitmapImage(new Uri(savePath + "\\" + nom_fichier + ".bmp"));
                }
            }
            
           


            foreach(var element in SpMenu.Children.OfType<Button>())
            {
                element.Background = bckBrush;
            }
            for(int i = 0; i<button_pressed.Length; i++)
            {
                button_pressed[i] = false;
            }

            ImageBox.MaxHeight = 10000;
            ImageBox.MaxWidth = 10000;
        }
        public void FermerMain_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        public void MinimiserMain_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
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
        private void Help_Click(object sender, RoutedEventArgs e)
        {
            Window7 window7 = new Window7();
            window7.Owner = this;
            window7.Show();
        }
    }
}
