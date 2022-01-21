using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;
using System.IO;

namespace TD_1
{
    class Program
    {
        static void Main(string[] args)
        {

            Bitmap Image;
            #region ornezg
            //#region rotate

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


            byte[] myFile = File.ReadAllBytes("fleur.bmp");

            Image = new Bitmap("fleur.bmp");
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
            //Console.WriteLine("Image");
            //for (int i = 54; i <myFile.Length; i = i + Image.Width * 3)
            //{
            //    for (int j = i; j < i + Image.Width * 3; j++)
            //    {
            //        Console.Write(myFile[i] + " ");
            //    }
            //    Console.WriteLine();
            //}

            MyImage image = new MyImage("fleur.bmp");
            Console.WriteLine(image.Type);
            Console.WriteLine(image.Size);
            Console.WriteLine(image.Width);  //verif taille correcte
            Console.WriteLine(image.Height);
            Console.WriteLine(image.Bits_per_color);
            Console.ReadLine();
        }
    }
}
