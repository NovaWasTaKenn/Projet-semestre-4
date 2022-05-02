using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;
using System.IO;

// Proteger le toFIle en mettant un pixel blanc si le pixel est pas initialisé
//Attention si le facteur de réduction est supérieur à la plus petite dimension alors l'image n'est pas lisible par le système --> Tester ds l'interface et avoir un message 
//Revoir le retrecisssement, tronque l'image pour des valeurs de division entre 1 et 2  (la matrice resultat se remplie avant qu'on arrive a la fin de la matrice originale -> les données au bout de cette matrice la sont tronquées)

namespace TD_1
{
    class Program
    {
        static void Main(string[] args)
            {

            #region TD 1 
            //#region rotate
            //Bitmap Image;
            //Image = new Bitmap("fleur.bmp");
            //Image.RotateFlip(RotateFlipType.Rotate90FlipNone) ;
            //Image.Save("fleur90°.bmp");
            //#endregion
            //#region trim
            //Image = new Bitmap("fleur.bmp");
            //Rectangle Section = new Rectangle(118, 203, 520, 340);
            //Image.Clone(Section, Image.PixelFormat).Save("fleurTrim.bmp");
            //#endregion
            //#region negatif
            //Image = new Bitmap("fleur.bmp");
            //for(int i = 0; i< Image.Height; i++)
            //{
            //    for(int j = 0; j< Image.Width; j++)
            //    {

            //        Color oldColor = Image.GetPixel(j, i);
            //        Color newColor = Color.FromArgb(oldColor.A, 255 - oldColor.R , 255 - oldColor.G , 255 - oldColor.B );

            //        Image.SetPixel(j,i, newColor);

            //    }
            //}
            //Image.Save("fleurCouleurInversées.bmp");
            //#endregion

            //Process.Start("fleur.bmp");
            //Process.Start("fleur90°.bmp");
            //Process.Start("fleurTrim.bmp");
            //Process.Start("fleurCouleurInversées.bmp");


            //#region Cryto
            ////Clée
            //int p = 3;
            //int q = 11;
            //int n = p * q;
            //int phi = (p - 1) * (q - 1);
            //int e = 3;
            //int d = 7;
            //int[] clée_publique = { n, e };
            //int[] clée_privée = { n, d };
            ////Cryptage
            //for (int i = 0; i < Image.Height; i++)
            //{
            //    for (int j = 0; j < Image.Width; j++)
            //    {

            //        Color oldColor = Image.GetPixel(j, i);
            //        Color newColor = Color.FromArgb(oldColor.A, Crypte(oldColor.R, clée_publique), Crypte(oldColor.G, clée_publique), Crypte(oldColor.B, clée_publique));
            //        Image.SetPixel(j, i, newColor);

            //    }
            //}
            //Image.Save("fleurCryptée.bmp");
            ////Décryptage
            //Image = new Bitmap("fleurCryptée.bmp");
            //for (int i = 0; i < Image.Height; i++)
            //{
            //    for (int j = 0; j < Image.Width; j++)
            //    {

            //        Color oldColor = Image.GetPixel(j, i);
            //        Color newColor = Color.FromArgb(oldColor.A, Décrypte(oldColor.R, clée_privée), Décrypte(oldColor.G, clée_privée), Décrypte(oldColor.B, clée_privée));
            //        Image.SetPixel(j, i, newColor);

            //    }
            //}
            //Image.Save("fleurDécryptée.bmp");

            //int Crypte(int msg, int[] clée)
            //{
            //    int chiffré = (int) Math.Pow(msg, clée[1])%clée[0];
            //    return chiffré;
            //}
            //int Décrypte(int chiffré, int[] clée)
            //{
            //    //int msg = (int)(uint)(Math.Pow(chiffré, clée[1]) % clée[0]);
            //    int msg = (int)(Math.Pow(chiffré, clée[1]) % clée[0]);
            //    if (msg < 0) { msg = clée[0] + msg; }
            //    return msg;
            //}
            //Process.Start("fleurCryptée.bmp");
            //Process.Start("fleurDécryptée.bmp");

            //#endregion
            #endregion
            #region Afficher fichier bmp
            /*byte[] myFile = File.ReadAllBytes("bmp-icon.bmp");

            Image = new Bitmap("bmp-icon.bmp");
            Console.WriteLine("Header");
            for (int i = 0; i < 14; i++)
            {
                Console.Write(myFile[i] + " ");
            }
            Console.WriteLine("Header info");
            for (int i = 14; i < 54; i++)
            {
                Console.Write(myFile[i] + " ");
            }
            Console.WriteLine("Image");
            for (int i = 54; i <myFile.Length; i = i + Image.Width * 3)
            {
                for (int j = i; j < i + Image.Width * 3; j++)
                {
                    Console.Write(myFile[i] + " ");
                }
                Console.WriteLine();
            }*/
            #endregion

            Console.WriteLine("Projet Scientifique Informatique A2");
            Console.ReadLine();

            Console.Write("Saisir le nom du fichier que vous voulez traiter : ");
            string nomFichier = Console.ReadLine();
            Console.WriteLine();
            while (File.Exists(nomFichier + ".bmp") == false)
            {
                Console.Write("Le fichier que vous voulez traiter n'existe pas. Saisir un autre : ");
                nomFichier = Console.ReadLine();
                Console.Clear();
            }

            Console.Clear();
            string imageTraitee = nomFichier + ".bmp";
            MyImage image = new MyImage(imageTraitee);

            Console.WriteLine("Informations de l'image traitée : \n");
            Console.WriteLine("Nom du fichier : " + nomFichier);
            Console.WriteLine("Type de l'image : " + image.Type);
            Console.WriteLine("Taille en octets de l'image : " + image.Size);
            Console.WriteLine("Largeur en pixels de l'image : " + image.Width);
            Console.WriteLine("Hauteur en pixels de l'image : " + image.Height);
            Console.WriteLine("Nombre de bits par pixel : " + image.Bits_per_color);
            Console.WriteLine();
            Process.Start(imageTraitee);
            Console.ReadKey();

            Console.WriteLine("Si vous voulez arrêter le programme, appuyez sur Escape");
            Console.WriteLine("Sinon, appuyez sur une autre touche");

            while (Console.ReadKey().Key != ConsoleKey.Escape)
            {
                Console.Clear();
                Console.WriteLine("Traitement de l'image\n");
                Console.WriteLine("Tapez le numéro en fonction de ce que vous voulez faire : \n");
                Console.WriteLine("1 : Noir et Blanc");
                Console.WriteLine("2 : Rotation");
                Console.WriteLine("3 : Aggrandissement et Rétrécissement");
                Console.WriteLine("4 : Effet Miroir");
                Console.WriteLine("5 : Détection des contours");
                Console.WriteLine("6 : Renforcement des bords");
                Console.WriteLine("7 : Floutage");
                Console.WriteLine("8 : Repoussage");
                Console.WriteLine("9 : Fractale");
                Console.WriteLine("10 : Histogramme");

                Console.WriteLine("\nPour coder et décoder une image cachée : ");
                Console.WriteLine("11 : Coder");
                Console.WriteLine("12 : Décoder");

                Console.WriteLine("\nPour coder et décoder un QRCode : ");
                Console.WriteLine("13 : Création d'un QRCode");
                Console.WriteLine("14 : Décoder un QRCode");
                Console.WriteLine();

                int reponse = Convert.ToInt32(Console.ReadLine());
                Console.ReadKey();
                Console.Clear();

                switch(reponse)
                {
                    case 1:
                        Console.WriteLine("NOIR ET BLANC");
                        image.CouleurToNoiretBlanc().ToFile(nomFichier + "_NoiretBlanc", "bmp");
                        Console.WriteLine("Nom du fichier : " + nomFichier + "_NoiretBlanc");
                        Process.Start(nomFichier + "_NoiretBlanc.bmp");
                        break;

                    case 2:
                        Console.WriteLine("ROTATION\n");
                        Console.Write("Saisir un angle de rotation de l'image : ");
                        int rotation = Convert.ToInt32(Console.ReadLine());
                        while(rotation < 0)
                        {
                            Console.Write("Saisir un angle de rotation de l'image : ");
                            rotation = Convert.ToInt32(Console.ReadLine());
                        }
                        Console.Write("Sens horaire ? (Oui ou Non) : ");
                        string sens = Console.ReadLine().ToLower();
                        bool sens_horaire;
                        if (sens == "oui")
                        {
                            sens_horaire = true;
                        }
                        else
                        {
                            sens_horaire = false;
                        }

                        image.RotationV2(rotation, sens_horaire).ToFile(nomFichier + "_Rotation", "bmp");
                        Console.WriteLine("\nNom du fichier : " + nomFichier + "_Rotation");
                        Process.Start(nomFichier + "_Rotation.bmp");
                        break;

                    case 3:
                        Console.WriteLine("Aggrandissement et Rétrécissement\n".ToUpper());
                        Console.Write("Saisir une valeur d'aggradissement et de rétrécissement :");
                        int valeur = Convert.ToInt32(Console.ReadLine());
                        while (valeur < 0)
                        {
                            Console.Write("Saisir une valeur d'aggradissement et de rétrécissement :");
                            valeur = Convert.ToInt32(Console.ReadLine());
                        }

                        image.Aggrandir(valeur).ToFile(nomFichier + "_Aggrandie", "bmp");
                        Console.WriteLine("\nNom du fichier : " + nomFichier + "_Aggrandie");
                        Process.Start(nomFichier + "_Aggrandie.bmp");

                        image.Rétrecissement(valeur).ToFile(nomFichier + "_Rétrécie", "bmp");
                        Console.WriteLine("\nNom du fichier : " + nomFichier + "_Rétrécie");
                        Process.Start(nomFichier + "_Rétrécie.bmp");
                        break;

                    case 4:
                        Console.WriteLine("Effet Miroir".ToUpper());
                        image.EffetMiroir().ToFile(nomFichier + "_EffetMiroir", "bmp");
                        Console.WriteLine("Nom du fichier : " + nomFichier + "_EffetMiroir");
                        Process.Start(nomFichier + "_EffetMiroir.bmp");
                        break;

                    case 5:
                        Console.WriteLine("Détection des contours".ToUpper());
                        int[,] matrice_détectiondescontours = { { 0, 1, 0 }, { 1, -4, 1 }, { 0, 1, 0 } };
                        image.Convolution(matrice_détectiondescontours, 1).ToFile(nomFichier + "_DectContour", "bmp");
                        Console.WriteLine("Nom du fichier : " + nomFichier + "_DecContour");
                        Process.Start(nomFichier + "_DectContour.bmp");
                        break;

                    case 6:
                        Console.WriteLine("Renforcement des bords".ToUpper());
                        int[,] matrice_renforcementdesbords = { { 0, 0, 0 }, { -1, 1, 0 }, { 0, 0, 0 } };
                        image.Convolution(matrice_renforcementdesbords, 1).ToFile(nomFichier + "_RenfContour", "bmp");
                        Console.WriteLine("Nom du fichier : " + nomFichier + "_RenfContour");
                        Process.Start(nomFichier + "_RenfContour.bmp");
                        break;

                    case 7:
                        Console.WriteLine("Floutage".ToUpper());
                        int[,] matrice_floutage = { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } };
                        image.Convolution(matrice_floutage, 9).ToFile(nomFichier + "_Flou", "bmp");
                        Console.WriteLine("Nom du fichier : " + nomFichier + "_Flou");
                        Process.Start(nomFichier + "_Flou.bmp");
                        break;

                    case 8:
                        Console.WriteLine("Repoussage".ToUpper());
                        int[,] matrice_repoussage = { { -2, -1, 0 }, { -1, 1, 1 }, { 0, 1, 2 } };
                        image.Convolution(matrice_repoussage, 9).ToFile(nomFichier + "_Repoussage", "bmp");
                        Console.WriteLine("Nom du fichier : " + nomFichier + "_Repoussage");
                        Process.Start(nomFichier + "_Repoussage.bmp");
                        break;

                    case 9:
                        Console.WriteLine("Fractale".ToUpper());
                        break;

                    case 10:
                        Console.WriteLine("Histogramme".ToUpper());
                        image.Histogramme().ToFile(nomFichier + "_Histogramme", "bmp");
                        Console.WriteLine("Nom du fichier : " + nomFichier + "_Histogramme");
                        Process.Start(nomFichier + "_Histogramme.bmp");
                        break;

                    case 11:
                        Console.WriteLine("Coder une image dans une autre".ToUpper());
                        Console.Write("\nSaisir le nom du fichier que vous voulez cacher dans l'image traitée : ");
                        string imagecachee = Console.ReadLine();

                        while(File.Exists(imagecachee + ".bmp") == false)
                        {
                            Console.Write("Le fichier que vous voulez traiter n'existe pas. Saisir un autre : ");
                            imagecachee = Console.ReadLine();
                            Console.Clear();
                        }

                        string imageacachee = imagecachee + ".bmp";
                        MyImage image1 = new MyImage(imageacachee);

                        image.CacherImage_dans_Image(image1).ToFile(imagecachee + "_CachéeDans_" + nomFichier, "bmp");
                        Console.WriteLine("Nom du fichier : " + imagecachee + "_CachéeDans_" + nomFichier);
                        Process.Start(imagecachee + "_CachéeDans_" + nomFichier + ".bmp");
                        break;

                    case 12:
                        Console.WriteLine("Décoder une image".ToUpper());
                        Console.Write("\nSaisir le nom du fichier que vous voulez décoder : ");
                        string decoder = Console.ReadLine();

                        while (File.Exists(decoder + ".bmp") == false)
                        {
                            Console.Write("Le fichier que vous voulez traiter n'existe pas. Saisir un autre : ");
                            decoder = Console.ReadLine();
                            Console.Clear();
                        }

                        string imageDecoder = decoder + ".bmp";
                        MyImage image2 = new MyImage(imageDecoder);

                        image2.DecoderImageCachee().ToFile(decoder + "_Décodée", "bmp");
                        Console.WriteLine("Nom du fichier : " + decoder + "_Décodée");
                        Process.Start(decoder + "_Décodée.bmp");
                        break;

                    case 13:
                        Console.WriteLine("Création d'un QRCode".ToUpper());
                        Console.Write("\nSaisir une phrase à encoder : ");
                        string phrase = Console.ReadLine().ToUpper();
                        while(phrase.Length > 45)
                        {
                            Console.Write("Le code ne traite pas les chaines de caractères supérieures à 45. Saisir une autre phrase : ");
                            phrase = Console.ReadLine().ToUpper();
                        }

                        byte[] phraseEncodee;
                        int[] masque_de_format = { 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 0, 0, 1, 0, 0 };
                        if (phrase.Length <=25)
                        {
                            Console.WriteLine("QRCode Version 1");
                            phraseEncodee = MyImage.Convertir_Chaine_Char(phrase, 1);
                            MyImage.QRCode(1, masque_de_format, phraseEncodee, false).ToFile("QRCodeV1_SansMasque", "bmp");
                            MyImage.QRCode(1, masque_de_format, phraseEncodee, true).ToFile("QRCodeV1_AvecMasque", "bmp");
                            MyImage.QRCode(1, masque_de_format, phraseEncodee, false).Aggrandir(10).ToFile("QRCodeV1_Aggrandi_SansMasque", "bmp");
                            MyImage.QRCode(1, masque_de_format, phraseEncodee, true).Aggrandir(10).ToFile("QRCodeV1_Aggrandi_AvecMasque", "bmp");
                            Console.WriteLine("Nom du fichier sans masque : QRCodeV1_SansMasque");
                            Console.WriteLine("Nom du fichier avec masque : QRCodeV1_AvecMasque");
                            Process.Start("QRCodeV1_Aggrandi_SansMasque.bmp");
                            Process.Start("QRCodeV1_Aggrandi_AvecMasque.bmp");
                        }

                        else
                        {
                            Console.WriteLine("QRCode Version 2");
                            phraseEncodee = MyImage.Convertir_Chaine_Char(phrase, 2);
                            MyImage.QRCode(2, masque_de_format, phraseEncodee, false).ToFile("QRCodeV2_SansMasque", "bmp");
                            MyImage.QRCode(2, masque_de_format, phraseEncodee, true).ToFile("QRCodeV2_AvecMasque", "bmp");
                            MyImage.QRCode(2, masque_de_format, phraseEncodee, false).Aggrandir(10).ToFile("QRCodeV2_Aggrandi_SansMasque", "bmp");
                            MyImage.QRCode(2, masque_de_format, phraseEncodee, true).Aggrandir(10).ToFile("QRCodeV2_Aggrandi_AvecMasque", "bmp");
                            Console.WriteLine("Nom du fichier sans masque : QRCodeV2_SansMasque");
                            Console.WriteLine("Nom du fichier avec masque : QRCodeV2_AvecMasque");
                            Process.Start("QRCodeV2_Aggrandi_SansMasque.bmp");
                            Process.Start("QRCodeV2_Aggrandi_AvecMasque.bmp");

                        }
                        break;

                    case 14:
                        Console.WriteLine("Décoder un QRCode".ToUpper());
                        Console.Write("\nSaisir le nom du fichier du QRCode que vous voulez décoder : ");
                        string QRCode = Console.ReadLine();

                        while (File.Exists(QRCode + ".bmp") == false)
                        {
                            Console.Write("Le fichier que vous voulez traiter n'existe pas. Saisir un autre : ");
                            QRCode = Console.ReadLine();
                            Console.Clear();
                        }

                        string QRCodeDecodee= QRCode + ".bmp";
                        MyImage image3 = new MyImage(QRCodeDecodee);

                        string decodage = image3.Decoder_QRCode();
                        Console.WriteLine(decodage);
                        break;

                    default:
                        Console.WriteLine("Veuillez saisir un nombre parmi la liste");
                        break;
                }
            }
            //Console.WriteLine("Convertir un format little endian en entier : {214 , 5 , 0 , 0} en entier ");
            //byte[] test_convert1 = {214, 5, 0, 0};
            //Console.WriteLine(image.Convertir_Endian_To_Int(test_convert1));
            //Console.WriteLine();
            //Console.WriteLine("Convertir_Int_To_Endian : convertir 1494 en format little endian ");
            //byte[] test_convert2 = image.Convertir_Int_to_Endian(1494);
            //Console.WriteLine(test_convert2[0]+" "+ test_convert2[1]+ " "+test_convert2[2]+" "+test_convert2[3] );
            //Console.WriteLine();
            //image.ToFile("TestToFile", "bmp");

            //image.RotationV2(97.00, true).ToFile("testRotation","bmp");
            //image.CouleurToNoiretBlanc().ToFile("testN&B","bmp");
            //image.Rétrecissement(1.2).ToFile("TestRétrecissement","bmp");

            //int[,] Repoussage = { { -2, -1, 0 }, { -1, 1, 1 }, { 0, 1, 2 } };
            //int[,] Flou = { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } };

            //image.Convolution(Flou, 9).ToFile("TestConvo.bmp");
            //MyImage image1 = new MyImage("carre.bmp");
            //image.CacherImage_dans_Image(image1).ToFile("TestCache", "bmp");
            //Process.Start("TestCache.bmp");

            //MyImage image2 = new MyImage("TestCache.bmp");

            //image2.DecoderImageCachee().ToFile("TestDecoder", "bmp");
            //Process.Start("TestDecoder.bmp");
            //Process.Start("TestConvo.bmp");

            //image.Flou(Flou, 15).ToFile("TestBords","bmp");

            //image.Aggrandir(5).ToFile("TestAgrandi","bmp");

            //MyImage.FractaleJulia(300).ToFile("TestFractale", "bmp");
            //Process.Start("TestFractale.bmp");

            //Process.Start(name);
            //Process.Start("TestBords.bmp");
            //Process.Start("testRotation.bmp");
            //Process.Start("testN&B.bmp");
            //Process.Start("TestRétrecissement.bmp");

            //image.Histogramme().ToFile("TestHistogramme","bmp");
            //Process.Start("TestHistogramme.bmp");

            //for (int i = 0; i < 15; i++)
            //{
            //    MyImage.FractaleJulia(10000).ToFile("TestFractale", "bmp");
            //    Process.Start("TestFractale.bmp");
            //}

            //byte b = 0b_0000_0000;
            //b += 0b_1000_0000;


            //b += (byte)(System.Math.Abs(System.Convert.ToInt32(true) * System.Math.Pow(2, 3)));
            //Console.WriteLine(Convert.ToString(b, 2));

            //bool[] tab = { false, false, false, false, false, true, false, true, true };

            //string Hello = "faut que je trouve un truc a eczqdqzdzqdzqdzqdsdqfghdsdfgbdfsfgffddqfsgdddsfd";
            //Hello = Hello.ToUpper();

            //////int entier_hello = 45*image.Convertir_Char_En_Int('H')+image.Convertir_Char_En_Int('E');
            //////Console.WriteLine(entier_hello);
            //////byte[] tab = image.Convertir_Int_En_Tab_De_Byte(entier_hello, 3, 2, 11);
            //////Console.WriteLine(Convert.ToString(tab[0],2));
            //////Console.WriteLine(Convert.ToString(tab[1],2));


            //byte[] Hello_byte = image.Convertir_Chaine_Char(Hello, 2);

            //int[] masque= {1,1,1,0,1,1,1,1,1,0,0,0,1,0,0};
            //image.QRCode(1, masque, Hello_byte, false).ToFile("QRCodeSansMasque","bmp");
            //image.QRCode(1, masque, Hello_byte, true).ToFile("QRCodeAvecMasque", "bmp");
            //image.QRCode(2, masque, Hello_byte, false).ToFile("QRCodeSansMasque1", "bmp");
            //image.QRCode(2, masque, Hello_byte, true).ToFile("QRCodeAvecMasque1", "bmp");
            //MyImage qrcode = new MyImage("QRCodeSansMasque1.bmp");
            //MyImage qr = new MyImage("QRCodeAvecMasque1.bmp");
            //qrcode.Aggrandir(10).ToFile("QRGrandSansMasque1","bmp");
            //qr.Aggrandir(10).ToFile("QRGrandAvecMasque1", "bmp");
            //Process.Start("QRCode.bmp");
            //Process.Start("QRGrandSansMasque1.bmp");
            //Process.Start("QRGrandAvecMasque1.bmp");

            //Console.WriteLine(qr.Decoder_QRCode());
            Console.ReadLine();
        }
    }
}
