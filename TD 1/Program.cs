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
            string name = "foret1600par900.bmp";
            MyImage image = new MyImage(name);
    
            Console.WriteLine("Type de l'image : "+image.Type);
            Console.WriteLine("Taille en octets de l'image : " + image.Size);
            Console.WriteLine("Largeur en pixels de l'image : " + image.Width);
            Console.WriteLine("Hauteur en pixels de l'image : " + image.Height);
            Console.WriteLine("Nombre de bits par pixel :" + image.Bits_per_color);
            Console.WriteLine();
            Console.WriteLine("Convertir un format little endian en entier : {214 , 5 , 0 , 0} en entier ");
            byte[] test_convert1 = {214, 5,0,0};
            Console.WriteLine(image.Convertir_Endian_To_Int(test_convert1));
            Console.WriteLine();
            Console.WriteLine("Convertir_Int_To_Endian : convertir 1494 en format little endian ");
            byte[] test_convert2 = image.Convertir_Int_to_Endian(1494);
            Console.WriteLine(test_convert2[0]+" "+ test_convert2[1]+ " "+test_convert2[2]+" "+test_convert2[3] );
            Console.WriteLine();
            image.ToFile("TestToFile", "bmp");

            //image.RotationV2(97.00, true).ToFile("testRotation","bmp");
            //image.CouleurToNoiretBlanc().ToFile("testN&B","bmp");
            //image.Rétrecissement(1.2).ToFile("TestRétrecissement","bmp");

            //int[,] Repoussage = { { -2, -1, 0 }, { -1,1, 1 }, { 0,1,2} };
            //int[,] Flou = { { 1 ,1,1},{1,1,1},{1,1,1}};
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

            for (int i = 0; i < 15; i++)
            {
                MyImage.FractaleJulia(500).ToFile("TestFractale", "bmp");
                Process.Start("TestFractale.bmp");
            }

            Console.ReadLine();
        }
    }
}
