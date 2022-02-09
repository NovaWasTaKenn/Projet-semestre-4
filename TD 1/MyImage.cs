using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace TD_1
{
    class MyImage
    {
        string type;
        int size;
        int offset;
        int width;
        int height;
        int bits_per_color;
        Pixel[,] image;

        public MyImage(string file)
        {
            byte[] file_tab = File.ReadAllBytes(file);

            string type_input = Convert.ToString(Convert.ToChar(file_tab[0])) + Convert.ToString(Convert.ToChar(file_tab[1]));
            this.type = ConvertToType(type_input);

            byte[] size_input = { file_tab[2], file_tab[3], file_tab[4], file_tab[5] };
            this.size = Convertir_Endian_To_Int(size_input);

            byte[] offset_input = {file_tab[10], file_tab[11], file_tab[12], file_tab[13]};
            this.offset = Convertir_Endian_To_Int(offset_input);

            byte[] width_input = { file_tab[18], file_tab[19], file_tab[20], file_tab[21] };
            this.width = Convertir_Endian_To_Int(width_input);

            byte[] height_input = { file_tab[22], file_tab[23], file_tab[24], file_tab[25] };
            this.height = Convertir_Endian_To_Int(height_input);

            byte[] bits_per_color_input = { file_tab[28], file_tab[29] };
            this.bits_per_color = Convertir_Endian_To_Int(bits_per_color_input);
            
            this.image = new Pixel[height, width];
            int ligne = height - 1;
            int colonne;

            for(int i = 54; i < file_tab.Length && ligne >=  0; i += width*3)
            {
                colonne = 0;
                for(int j = i; j < i + width * 3; j += 3)
                {
                    this.image[ligne, colonne] = new Pixel(file_tab[j + 2], file_tab[j + 1], file_tab[j]);
                    colonne++;
                }
                ligne--;
            }
        }

        public MyImage(MyImage myImage, int height, int width)
        {
            this.type = myImage.type;
            this.size = myImage.size;
            this.offset = myImage.offset;
            this.height = myImage.height;
            this.width = myImage.width;
            this.bits_per_color = myImage.bits_per_color;
            this.image = new Pixel[height, width];
        }

        public void ToFile(string name, string type)
        {    
            string path = name + "." + type;

            if (!File.Exists(path))
            {
                using(File.Create(path));
            }

            byte[] bytesToWrite = new byte[size];

            for(int i = 0; i < 54; i++)
            {
                if (i < 2) { bytesToWrite[i] = ConvertTypeToHexa(type)[i]; }
                if(i < 6  && i >= 2) { bytesToWrite[i] = Convertir_Int_to_Endian(size)[i - 2]; }
                byte[] Reserved = { 0,0,0,0 };
                if(i < 10  && i >= 6) { bytesToWrite[i] = Reserved[i - 6]; }
                if(i < 14  && i >= 10) { bytesToWrite[i] = Convertir_Int_to_Endian(offset)[i - 10]; }
                byte[] HeaderSize = { 40,0,0,0 };
                if(i < 18  && i >= 14) { bytesToWrite[i] = HeaderSize[i - 14]; }
                if(i < 22  && i >= 18) { bytesToWrite[i] = Convertir_Int_to_Endian(width)[i - 18]; }
                if(i < 26  && i >= 22) { bytesToWrite[i] = Convertir_Int_to_Endian(height)[i - 22]; }
                byte[] wut = {1,0};
                if(i < 28  && i >= 26) {bytesToWrite[i] = wut[i - 26]; }
                if(i < 30  && i >= 28) {bytesToWrite[i] = Convertir_Int_to_Endian(bits_per_color)[i - 28]; }
                byte[] bordel = { 0,0,0,0,176,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 };
                if(i < 54  && i >= 30) { bytesToWrite[i] = bordel[i - 30]; }
            }

            int index = 54;

            for(int i = image.GetLength(0) - 1; i >= 0; i--)
            {
                for(int j = 0; j < image.GetLength(1); j++)
                {
                    bytesToWrite[index] = (byte) image[i,j].B;
                    index++;
                    bytesToWrite[index] = (byte) image[i,j].G;
                    index++;
                    bytesToWrite[index] = (byte) image[i,j].R;
                    index++;                 
                }
            }
            File.WriteAllBytes(path, bytesToWrite);
        }

        public string Type
        {
            get { return type; }
        }

        public int Size
        {
            get { return size; }
        }

        public int Width
        {
            get { return width; }
            set { width = value;}
        }

        public int Height 
        {
            get { return height; }
            set { height = value;}
        }

        public int Bits_per_color
        {
            get { return bits_per_color; }
        }

        public Pixel[,] Image
        {
            get { return image; }
        }

        public void AfficherImage()
        {
            for(int i = 0; i < image.GetLength(0); i++)
            {
                for(int j = 0; j < image.GetLength(1); j++)
                {
                    Console.WriteLine(Image[i, j].ToString());
                }
                Console.WriteLine("-------------------------------------------------------------------------------------------------");
            }
        }

        public string ConvertToType(string letters)
        {
            string type = "";
            switch (letters)
            {  
                case "BM":
                    type = "bmp";
                    break;
            }
            return type;
        }

        public byte[] ConvertTypeToHexa(string type)
        {
            byte[] retour = new byte[2];
            switch (type)
            {  
                case "bmp":
                    retour[0] = 66;
                    retour[1] = 77;
                    break;
            }
            return retour;
        }

        #region Utilisation de BitConverter
        //public int Convertir_Endian_To_Int32(byte[] tab)
        //{
        //    tab.Reverse<byte>();
        //    return BitConverter.ToInt32(tab, 0);
        //}
        //public int Convertir_Endian_To_Int16(byte[] tab)
        //{
        //    tab.Reverse<byte>();
        //    return BitConverter.ToInt16(tab, 0);
        //}
        //public byte[] Convertir_Int_To_Endian(int nb)
        //{
        //    byte[] retour = BitConverter.GetBytes(nb);
        //    retour.Reverse<byte>();
        //    return retour;
        //}
        #endregion

        public int Convertir_Endian_To_Int(byte[] tab)
        {            
            int retour = 0;
            int index = 0;
            int[] binaire = new int[tab.Length * 8];
            for(int i = tab.Length-1 ; i>= 0 ; i--)
            {
                for(int j = 7; j >= 0; j--)
                {
                    byte puissance = (byte)Math.Pow((double)2,(double)j);
                    if ((tab[i] - puissance) >= 0)
                    {
                        binaire[index] = 1;
                        tab[i] -= puissance;
                    }
                    else
                    {
                        binaire[index] = 0;
                    }
                    index++;
                }
            }
            for(int i = 0 ; i < binaire.Length; i++)
            {
                
                if(binaire[i] == 1)
                {
                    retour += (int)Math.Pow((double)2, (double)(binaire.Length - 1 - i));
                }
            }
            return retour;
        }

        public byte[] Convertir_Int_to_Endian(int nb)
        {
            byte[] retour;
            int[] binaire = new int[32];
            for(int i = 0; i < binaire.Length; i++)
            {
                int puissance = (int)Math.Pow((double)2, (double)binaire.Length - 1 - i);
                if(nb - puissance >= 0)
                {
                    binaire[i] = 1;
                    nb -= puissance;
                }

                else
                {
                    binaire[i] = 0;
                }
            }

            retour = new byte[binaire.Length / 8];
            byte stock;
            int index = 0;
            for(int j = binaire.Length - 1; j >= 0; j -= 8)
            {
                stock = 0;
                for (int p = 0; p < 8; p++)
                {
                    if(binaire[j - p] == 1)
                    {
                        stock += (byte)Math.Pow((double)2, (double)p);
                    }

                    retour[index] = stock;
                }
                index++;
            }
            return retour;
        }

        public void CouleurToNoiretBlanc()
        {
            for(int i = 0 ; i < image.GetLength(0); i ++)
            {
                for(int j = 0 ; j < image.GetLength(1); j++)
                {
                    int new_color = (image[i,j].R + image[i,j].G + image[i,j].B) / 3 ;
                    image[i,j].R = image[i,j].G = image[i,j].B = new_color;
                }
            }
        }

        public MyImage Rotation(int angle, bool sens_horaire)
        {
            int nb_rotation = angle / 90;
            MyImage copie;

            if(nb_rotation % 2 == 0)
            {
                copie = new MyImage(this, this.height, this.width);
            }

            else
            {
                copie = new MyImage(this, this.width, this.height);
            }
            
            for(int k = 0; k < nb_rotation; k++)
            {
                for (int i = 0; i < image.GetLength(0); i++)
                {
                    for (int j = 0; j < image.GetLength(1); j++)
                    {
                        if (sens_horaire)
                        {
                            copie.image[j, copie.image.GetLength(1) - i - 1] =  this.image[i,j]; 
                        }
                        else
                        {
                            copie.image[j,i] = this.image[i, this.image.GetLength(1) - j - 1];
                        }
                    }
                }
            }

            return copie;
        }
    }
}
