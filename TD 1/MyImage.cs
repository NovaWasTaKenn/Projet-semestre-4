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
            string type_input = Convert.ToString(Convert.ToChar(file_tab[0])) + Convert.ToString(Convert.ToChar(file_tab[1]));
            this.type = ConvertToType(type_input);
            byte[] size_input = { file_tab[2], file_tab[3], file_tab[4], file_tab[5] };
            //for(int i = 0; i< 4; i++)
            //{
            //    Console.WriteLine(size_input[i]);
            //}
            this.size = Convertir_Endian_To_Int32(size_input);
            byte[] width_input = { file_tab[18], file_tab[19], file_tab[20], file_tab[21] };
            this.width = Convertir_Endian_To_Int32(width_input);
            byte[] height_input = { file_tab[22], file_tab[23], file_tab[24], file_tab[25] };
            this.height = Convertir_Endian_To_Int32(height_input);

            image = new Pixel[height, width];
            Console.WriteLine();

            byte[] bits_per_color_input = { file_tab[28], file_tab[29] };
            this.bits_per_color = Convertir_Endian_To_Int16(bits_per_color_input);
            int ligne = 0;
            int colonne = 0;
            for(int i =54; i<file_tab.Length;i=i+width*3)
            {
                colonne = 0;
                for(int j = i; j <i+ width * 3; j=j+3)
                {
                    ligne = ligne;  //??????
                    this.image[ligne, colonne] = new Pixel(file_tab[j], file_tab[j + 1], file_tab[j + 2]);
                    Console.WriteLine(this.image[ligne, colonne].ToString());
                    colonne++;
                    
                }
                Console.WriteLine("-------------------------------------------------------------------------------------------------");
                ligne++;
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


    }
}
