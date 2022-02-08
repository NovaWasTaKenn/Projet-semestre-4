using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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

            //byte[] test = Convertir_Int_to_Endian2(1494);

            string type_input = Convert.ToString(Convert.ToChar(file_tab[0])) + Convert.ToString(Convert.ToChar(file_tab[1]));
            this.type = ConvertToType(type_input);

            byte[] size_input = { file_tab[2], file_tab[3], file_tab[4], file_tab[5] };
            this.size = Convertir_Endian_To_Int(size_input);

            byte[] offset_input = {file_tab[10], file_tab[11],file_tab[12],file_tab[13]};
            this.offset = Convertir_Endian_To_Int(offset_input);

            byte[] width_input = { file_tab[18], file_tab[19], file_tab[20], file_tab[21] };
            this.width = Convertir_Endian_To_Int(width_input);

            byte[] height_input = { file_tab[22], file_tab[23], file_tab[24], file_tab[25] };
            this.height = Convertir_Endian_To_Int(height_input);

            byte[] bits_per_color_input = { file_tab[28], file_tab[29] };
            this.bits_per_color = Convertir_Endian_To_Int(bits_per_color_input);
            
            this.image = new Pixel[height, width];
            //Console.WriteLine();
            int ligne = height-1;
            int colonne = 0;
            for(int i =54; i<file_tab.Length && ligne >=  0;i=i+width*3)
            {
                colonne = 0;
                for(int j = i; j <i+ width * 3; j=j+3)
                {
                    this.image[ligne, colonne] = new Pixel(file_tab[j+2], file_tab[j + 1], file_tab[j]);
                    //Console.WriteLine(this.image[ligne, colonne].ToString());
                    colonne++;
                    
                }
                //Console.WriteLine("-------------------------------------------------------------------------------------------------");
                ligne--;
            }
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
        }
        public int Height 
        {
            get { return height; }
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
            for(int i = 0; i< image.GetLength(0); i++)
            {
                for(int j = 0; j< image.GetLength(1); j++)
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
        public int Convertir_Endian_To_Int32(byte[] tab)
        {
            tab.Reverse<byte>();
            return BitConverter.ToInt32(tab, 0);
        }
        public int Convertir_Endian_To_Int16(byte[] tab)
        {
            tab.Reverse<byte>();
            return BitConverter.ToInt16(tab, 0);
        }
        public byte[] Convertir_Int_To_Endian(int nb)
        {
            byte[] retour = BitConverter.GetBytes(nb);
            retour.Reverse<byte>();
            return retour;
        }
        public int Convertir_Endian_To_Int(byte[] tab)
        {
            //Valeurs en Little endian
            
            int retour = 0;
            int index = 0;
            int[] binaire = new int[tab.Length * 8];
            for(int i = tab.Length-1 ; i>= 0 ; i--)
            {
                for(int j =7; j >=0 ; j--)
                {
                    byte puissance = (byte) Math.Pow((double) 2,(double) j);
                    if ((tab[i] - puissance)>=0)
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
            for(int i = 0 ; i< binaire.Length; i++)
            {
                
                if(binaire[i] == 1)
                {
                    retour+= (int) Math.Pow((double) 2, (double) (binaire.Length - 1 -i));
                }
            }

           
            return retour;
        }

        public byte[] Convertir_Int_to_Endian2(int nb)
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
                //Console.Write(binaire[i]);
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
            
            //for(int i = 0; i < retour.Length; i++)
            //{
            //    Console.WriteLine(retour[i]);
            //}
            return retour;
        }
        public void CouleurToNoiretBlanc()
        {
            for(int i = 0 ; i< image.GetLength(0);i ++)
            {
                for(int j = 0 ; j< image.GetLength(1);j++)
                {
                    int new_color = (image[i,j].R + image[i,j].G+ image[i,j].B)/3 ;
                    image[i,j].R = image[i,j].G = image[i,j].B = new_color;
                }
            }
        }
         /*L'image n'est pas forcément un carré a prendre en cpt*/
        public void Rotation(int angle , bool sens_horaire)
        {
            Pixel[,] copie = new Pixel[image.GetLength(0), image.GetLength(1)];
            for (int i = 0; i < image.GetLength(0); i++)
            {
                for (int j = 0; j < image.GetLength(1); j++)
                {
                    copie[i, j] = image[i, j];
                }
            }
            int nb_rotation = angle / 90;
            for(int k = 0; k< nb_rotation; k++)
            {
                for (int i = 0; i < image.GetLength(0); i++)
                {
                    for (int j = 0; j < image.GetLength(1); j++)
                    {
                        if (sens_horaire)
                        {
                            image[j, image.GetLength(1)-i] = copie[i, j]; // Index out of bound par ici probablement du a image rectangle
                        }
                        else
                        {
                            image[j,i] = copie[i, image.GetLength(1)-j];
                        }
                    }
                }
            }
            
        }
    }
}
