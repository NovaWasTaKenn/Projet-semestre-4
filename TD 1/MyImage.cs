using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;


namespace TD_1
{
    public class MyImage
    {
        string type;
        int size;
        int offset;
        int width;
        int height;
        int bits_per_color;
        Pixel[,] image;

        //Rotation fct pour les multiples de 90° mais artefacts blancs sur les rotations quelconques
        // refaire convolution plus proprement

        #region Constructeurs
        public MyImage (int height, int width)
        {
            this.type = "bmp";
            int nb_remplissage_fin_ligne = 0;
            if ((width * 3) % 4 != 0)
            {
                nb_remplissage_fin_ligne = 4 - (width * 3) % 4;
            }
            this.size = 54 + ((nb_remplissage_fin_ligne + (width * 3)) * height);
            this.offset = 54;
            this.height = height;
            this.width = width;
            this.bits_per_color = 24;
            this.image = new Pixel[height, width];
        }
        public MyImage(MyImage myImage, int height, int width)
        {
            this.type = myImage.type;
            int nb_remplissage_fin_ligne = 0;
            if ((width * 3) % 4 != 0)
            {
                nb_remplissage_fin_ligne = 4 - (width * 3) % 4;
            }
            this.size = 54 + ((nb_remplissage_fin_ligne+(width*3))*height);
            this.offset = myImage.offset;
            this.height = height;
            this.width = width;
            this.bits_per_color = myImage.bits_per_color;
            this.image = new Pixel[height, width];
        }
        #endregion

        #region Fichier vers classe | Classe vers fichier
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
                //Prendre en cpt les cas ou la largeur de l'image n'est pas multiple de 4 , on augmente l'index i en conséquence pr sauter les 0 de remplissage

                colonne = 0;
                for(int j = i; j < i + width * 3; j += 3)
                {
                    this.image[ligne, colonne] = new Pixel(file_tab[j + 2], file_tab[j + 1], file_tab[j]);
                    colonne++;
                }
                if ((width * 3) % 4 != 0)
                {
                    int nb_remplissage_fin_ligne = 4 - (width * 3) % 4;
                    i = i + nb_remplissage_fin_ligne;
                }
                ligne--;
            }
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
                byte[] bordel = { 0,0,0,0,176,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 };                       //Nom de var bordel a changer
                if(i < 54  && i >= 30) { bytesToWrite[i] = bordel[i - 30]; }
            }

            int index = 54;

            for(int i = this.image.GetLength(0) - 1; i >= 0; i--)
            {
                for(int j = 0; j < this.image.GetLength(1); j++)
                {
                    bytesToWrite[index] = (byte)this.image[i,j].B;
                    index++;
                    bytesToWrite[index] = (byte)this.image[i,j].G;
                    index++;
                    bytesToWrite[index] = (byte)this.image[i,j].R;
                    index++;                 
                }

                #region Switch     prendre en cpt les cas ou longueur pas multiple de 4, ajouter des 0 a la fin de chaque ligne pour arriver a un multiple de 4 
                int fin_de_ligne = 4 - (this.width*3)%4;
                switch (fin_de_ligne)
                {
                    case 1 : 
                        bytesToWrite[index] = 0;
                        index++;
                        break;
                    case 2 :
                        bytesToWrite[index] = 0;
                        index++;
                        bytesToWrite[index] = 0;
                        index++;
                        break;
                    case 3 :
                        bytesToWrite[index] = 0;
                        index++;
                        bytesToWrite[index] = 0;
                        index++;
                        bytesToWrite[index] = 0;
                        index++;
                        break;
                }

                #endregion
            }
            File.WriteAllBytes(path, bytesToWrite);
        }
        public void ToFile(string path)
        {

            if (!File.Exists(path))
            {
                using (File.Create(path)) ;
            }

            byte[] bytesToWrite = new byte[size];

            for (int i = 0; i < 54; i++)
            {
                if (i < 2) { bytesToWrite[i] = ConvertTypeToHexa(type)[i]; }
                if (i < 6 && i >= 2) { bytesToWrite[i] = Convertir_Int_to_Endian(size)[i - 2]; }
                byte[] Reserved = { 0, 0, 0, 0 };
                if (i < 10 && i >= 6) { bytesToWrite[i] = Reserved[i - 6]; }
                if (i < 14 && i >= 10) { bytesToWrite[i] = Convertir_Int_to_Endian(offset)[i - 10]; }
                byte[] HeaderSize = { 40, 0, 0, 0 };
                if (i < 18 && i >= 14) { bytesToWrite[i] = HeaderSize[i - 14]; }
                if (i < 22 && i >= 18) { bytesToWrite[i] = Convertir_Int_to_Endian(width)[i - 18]; }
                if (i < 26 && i >= 22) { bytesToWrite[i] = Convertir_Int_to_Endian(height)[i - 22]; }
                byte[] wut = { 1, 0 };
                if (i < 28 && i >= 26) { bytesToWrite[i] = wut[i - 26]; }
                if (i < 30 && i >= 28) { bytesToWrite[i] = Convertir_Int_to_Endian(bits_per_color)[i - 28]; }
                byte[] bordel = { 0, 0, 0, 0, 176, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };                       //Nom de var bordel a changer
                if (i < 54 && i >= 30) { bytesToWrite[i] = bordel[i - 30]; }
            }

            int index = 54;

            for (int i = this.image.GetLength(0) - 1; i >= 0; i--)
            {
                for (int j = 0; j < this.image.GetLength(1); j++)
                {
                    bytesToWrite[index] = (byte)this.image[i, j].B;
                    index++;
                    bytesToWrite[index] = (byte)this.image[i, j].G;
                    index++;
                    bytesToWrite[index] = (byte)this.image[i, j].R;
                    index++;
                }

                #region Switch     prendre en cpt les cas ou longueur pas multiple de 4, ajouter des 0 a la fin de chaque ligne pour arriver a un multiple de 4 
                int fin_de_ligne = 4 - (this.width * 3) % 4;
                switch (fin_de_ligne)
                {
                    case 1:
                        bytesToWrite[index] = 0;
                        index++;
                        break;
                    case 2:
                        bytesToWrite[index] = 0;
                        index++;
                        bytesToWrite[index] = 0;
                        index++;
                        break;
                    case 3:
                        bytesToWrite[index] = 0;
                        index++;
                        bytesToWrite[index] = 0;
                        index++;
                        bytesToWrite[index] = 0;
                        index++;
                        break;
                }

                #endregion
            }
            File.WriteAllBytes(path, bytesToWrite);
        }
        #endregion

        #region accesseurs
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
        #endregion

        #region utilitaire

        public override string ToString()
        {
            return "height : " + height + " | width : " + width + " height matrice : " + image.GetLength(0) + "width matrice : "+ image.GetLength(1); 
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
        public int Puissance(int x, int exp, int valeur = 1)
        {
            if(exp == 0)
            {
                return valeur;
            }

            return Puissance(x, exp - 1, valeur * x);
        }
        public void FondBlanc()
        {
            for(int i = 0; i<this.image.GetLength(0); i++)
            {
                for(int j = 0; j< this.image.GetLength(1); j++)
                {
                    this.image[i, j] = new Pixel(255,255,255);
                }
            }
        }
        public void FondGris()
        {
            for (int i = 0; i < this.image.GetLength(0); i++)
            {
                for (int j = 0; j < this.image.GetLength(1); j++)
                {
                    this.image[i, j] = new Pixel(34, 34, 34);
                }
            }
        }
        public void FondGrisClair()
        {
            for (int i = 0; i < this.image.GetLength(0); i++)
            {
                for (int j = 0; j < this.image.GetLength(1); j++)
                {
                    this.image[i, j] = new Pixel(130, 130, 130);
                }
            }
        }
        #endregion

        #region Conversion
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
            for(int i = tab.Length - 1 ; i >= 0 ; i--)
            {
                for(int j = 7; j >= 0; j--)
                {
                    byte puissance = (byte)Puissance(2, j);
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
                    retour += Puissance(2,binaire.Length - 1 - i);
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
                int puissance = Puissance(2, binaire.Length - 1 - i);
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
                        stock += (byte)Puissance(2, p);
                    }

                    retour[index] = stock;
                }
                index++;
            }
            return retour;
        }

        public bool[] Convertir_Int_to_Bool_Tab_9(int nb, int taille)
        {
            bool[] binaire = new bool[taille];
            for (int i = 0; i < binaire.Length; i++)
            {
                int puissance = Puissance(2, binaire.Length - 1 - i);
                if (nb - puissance >= 0)
                {
                    binaire[i] = true;
                    nb -= puissance;
                }

                else
                {
                    binaire[i] = false;
                }
            }

            
            return binaire;
        }
        #endregion

        #region TD 3
        public MyImage CouleurToNoiretBlanc()
        {
            MyImage copie = new MyImage(this, this.height, this.width);

            for(int i = 0 ; i < this.image.GetLength(0); i ++)
            {
                for(int j = 0 ; j < this.image.GetLength(1); j++)
                {
                    int new_color = (this.image[i,j].R + this.image[i,j].G + this.image[i,j].B) / 3 ;
                    copie.image[i,j] = new Pixel(new_color, new_color,new_color);
                }
            }
            return copie;
        }
        public MyImage Rotation(int angle, bool sens_horaire)
        {
            int nb_rotation = angle / 90;
            MyImage copie = new MyImage(this, this.height, this.width);



            for(int k = 0; k < nb_rotation; k++)
            {              
                copie = new MyImage(this, this.width, this.height);
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
                            copie.image[copie.image.GetLength(0)-j-1, i] = this.image[i,j];
                        }
                    }
                }

                this.image = copie.image;
            }

            return copie;
        }
        public MyImage RotationV2(double angle, bool sens_horaire) //essayer avec x' = x*cos O + y*sin O et y' = -x*sin 0 + y * sin O
        {

            if (angle > 360)
            {
                angle = angle % 360;
            }
            if (angle > 180)
            {
                angle = 360 - angle;
                sens_horaire = !sens_horaire;
            }

            double angle_rad = (Math.PI / 180) * angle;
            int height = 0;
            int width = 0;
            if (angle <= 90 && 0 <= angle)
            {
                 height = Math.Abs((int)Math.Round(Convert.ToDouble(this.width) * Math.Sin(angle_rad) + Convert.ToDouble(this.height) * Math.Cos(angle_rad)));
                 width = Math.Abs((int)Math.Round(Convert.ToDouble(this.width) * Math.Cos(angle_rad) + Convert.ToDouble(this.height) * Math.Sin(angle_rad)));
            }
            else
            {
                height = Math.Abs((int)Math.Round(Convert.ToDouble(this.width) * Math.Sin(Math.PI - angle_rad) + Convert.ToDouble(this.height) * Math.Cos(Math.PI - angle_rad)));
                width = Math.Abs((int)Math.Round(Convert.ToDouble(this.width) * Math.Cos(Math.PI - angle_rad) + Convert.ToDouble(this.height) * Math.Sin(Math.PI - angle_rad)));
            }


            MyImage copie = new MyImage(this, height, width);

            copie.FondGris();

            for (int i  = 0; i< this.image.GetLength(0); i++)
            {
                for(int j =0;j< this.image.GetLength(1); j++)
                {
                    int j_new = 0;
                    int i_new = 0;
                    if (sens_horaire)           // Les formules ne fonctionne pas pour une rotation de 180°   probablement besoin de trouver les formules pour -pi/2 - pi/2
                    {
                        if (angle<=90 && 0<=angle)
                        {
                            j_new = (int)Math.Round(Math.Cos(angle_rad) * Convert.ToDouble(j) + Math.Sin(angle_rad) * Convert.ToDouble(this.height - i - 1));
                            i_new = (int)Math.Round(Math.Cos(angle_rad) * Convert.ToDouble(i) + Math.Sin(angle_rad) * Convert.ToDouble(j));
                        }
                        if(angle <= 180 && 90 < angle)
                        {
                            j_new = (int)Math.Round(Convert.ToDouble(copie.width-1) - Math.Cos(Math.PI - angle_rad) * Convert.ToDouble(j) - Math.Sin(Math.PI - angle_rad) * Convert.ToDouble(i));
                            i_new = (int)Math.Round(Convert.ToDouble(copie.height-1) + Math.Sin(Math.PI - angle_rad) * Convert.ToDouble(j) - Math.Cos(Math.PI - angle_rad) * Convert.ToDouble(i) - Math.Sin(Math.PI - angle_rad) * Convert.ToDouble(this.width - 1));
                        }

                    }
                    else
                    {
                        if (angle <= 90 && 0 <= angle)
                        {
                            j_new = (int)Math.Round(Math.Cos(angle_rad) * Convert.ToDouble(j) + Math.Sin(angle_rad) * Convert.ToDouble(i));
                            i_new = (int)Math.Round(Math.Cos(angle_rad) * Convert.ToDouble(i) + Math.Sin(angle_rad) * Convert.ToDouble(this.width - j - 1));
                        }
                        if(angle <= 180 && 90 < angle)
                        {
                            j_new = (int)Math.Round(Convert.ToDouble(copie.width - 1) - Math.Cos(Math.PI - angle_rad) * Convert.ToDouble(j) + Math.Sin(Math.PI - angle_rad) * Convert.ToDouble(i) - Math.Sin(Math.PI - angle_rad) * Convert.ToDouble(this.height - 1));
                            i_new = (int)Math.Round(Convert.ToDouble(copie.height - 1) - Math.Sin(Math.PI - angle_rad) * Convert.ToDouble(j) - Math.Cos(Math.PI - angle_rad) * Convert.ToDouble(i));
                        }

                    }
                    if(i_new>= 0 && i_new < copie.image.GetLength(0) && j_new>= 0 && j_new < copie.image.GetLength(1))
                    {
                        copie.image[i_new, j_new] = new Pixel(this.image[i, j].R, this.image[i, j].G, this.image[i, j].B);
                        if (i != this.image.GetLength(0) - 1 && j != this.image.GetLength(1) - 1)
                        {
                            if (sens_horaire && angle <= 90 && 0 <= angle)
                            {
                                if (i_new + 1 < copie.height) { copie.image[i_new + 1, j_new] = new Pixel(this.image[i, j].R, this.image[i, j].G, this.image[i, j].B); }
                                if (j_new - 1 >= 0 && i_new + 1 > copie.height) { copie.image[i_new + 1, j_new - 1] = new Pixel(this.image[i, j].R, this.image[i, j].G, this.image[i, j].B); }
                                if (i_new + 1 < copie.height && j_new + 1 < copie.width) { copie.image[i_new + 1, j_new + 1] = new Pixel(this.image[i, j].R, this.image[i, j].G, this.image[i, j].B); }
                            }
                            if (sens_horaire && angle <= 180 && 90 < angle)
                            {
                                if (j_new - 1 >= 0) { copie.image[i_new, j_new - 1] = new Pixel(this.image[i, j].R, this.image[i, j].G, this.image[i, j].B); }
                                if (j_new - 1 >= 0 && i_new + 1 > copie.height) { copie.image[i_new + 1, j_new - 1] = new Pixel(this.image[i, j].R, this.image[i, j].G, this.image[i, j].B); }
                                if (i_new - 1 >= 0 && j_new - 1 >= 0) { copie.image[i_new - 1, j_new - 1] = new Pixel(this.image[i, j].R, this.image[i, j].G, this.image[i, j].B); }
                            }
                            if (!sens_horaire && angle <= 90 && 0 <= angle)
                            {
                                if (j_new + 1 < copie.height) { copie.image[i_new, j_new + 1] = new Pixel(this.image[i, j].R, this.image[i, j].G, this.image[i, j].B); }
                                if (j_new + 1 < copie.width && i_new + 1 < copie.height) { copie.image[i_new + 1, j_new + 1] = new Pixel(this.image[i, j].R, this.image[i, j].G, this.image[i, j].B); }
                                if (i_new - 1 >= 0 && j_new + 1 < copie.width) { copie.image[i_new - 1, j_new + 1] = new Pixel(this.image[i, j].R, this.image[i, j].G, this.image[i, j].B); }
                            }
                            if (!sens_horaire && angle <= 180 && 90 < angle)
                            {
                                if (i_new - 1 >= 0) { copie.image[i_new - 1, j_new] = new Pixel(this.image[i, j].R, this.image[i, j].G, this.image[i, j].B); }
                                if (j_new + 1 < copie.width && i_new - 1 >= 0) { copie.image[i_new - 1, j_new + 1] = new Pixel(this.image[i, j].R, this.image[i, j].G, this.image[i, j].B); }
                                if (i_new - 1 >= 0 && j_new - 1 >= 0) { copie.image[i_new - 1, j_new - 1] = new Pixel(this.image[i, j].R, this.image[i, j].G, this.image[i, j].B); }
                            }
                        }
                    }


                }
            }
            return copie;
        }
        public MyImage EffetMiroir()
        {
            MyImage copie = new MyImage(this, this.height, this.width);

            for(int i = 0; i < this.height; i++)
            {
                for(int j = 0; j < this.width; j++)
                {
                    copie.image[i, j] = this.image[i, this.width - 1 - j];
                }
            }
            return copie;
        }
        public MyImage Aggrandir(int multiplicateur)
        {
            MyImage copie = new MyImage(this, image.GetLength(0) * multiplicateur, image.GetLength(1) * multiplicateur);
            int stock1 = 0;
            int stock2 = 0;
            for (int i = 0; i < image.GetLength(0); i++)
            {
                for (int j = 0; j < image.GetLength(1); j++)
                {
                    for (int h = stock1; h < stock1 + multiplicateur; h++)
                    {
                        for (int w = stock2; w < stock2 + multiplicateur; w++)
                        {
                            copie.image[h, w] = this.image[i, j];
                        }
                    }
                    stock2 += multiplicateur;
                }

                stock1 += multiplicateur;
                stock2 = 0;
            }
            return copie;
        }
        public MyImage Rétrecissement(double val_rétrecissement)
        {
            MyImage copie = new MyImage(this, (int)((double)this.image.GetLength(0) / val_rétrecissement), (int)(((double)this.image.GetLength(1)) / val_rétrecissement));
            List<int> valeurs_rectangles = new List<int>();
            int précision = 10;
            if (val_rétrecissement % 1 !=0)
            {
                int nb_grd_valeur = (int)((val_rétrecissement - ((int)val_rétrecissement)) * précision);
                int nb_ptte_valeur = précision - nb_grd_valeur;
                for(int m =0; m< précision; m++)
                {
                    if (m < nb_ptte_valeur) { valeurs_rectangles.Add((int)val_rétrecissement); }
                    else { valeurs_rectangles.Add((int)val_rétrecissement + 1); }
                }
            }
            else
            {
                valeurs_rectangles.Add((int)val_rétrecissement);
            }
            int i = 0;
            int j = 0;
            int i_rétréci = 0;
            int j_rétréci;
            int somme_R;
            int somme_G;
            int somme_B;
            int hauteur = valeurs_rectangles[i];
            int largeur = valeurs_rectangles[j];
            for (int i_originale =0; i_originale < this.image.GetLength(0); i_originale += hauteur)
            {
                j_rétréci = 0;
                j = 0;
                for(int j_originale= 0; j_originale < this.image.GetLength(1); j_originale += largeur)
                {
                    somme_R = 0;
                    somme_G = 0;
                    somme_B = 0;
                    for (int k = 0; k< hauteur; k++)
                    {
                        for(int l = 0; l< largeur; l++)
                        {
                            if(i_originale+k < this.image.GetLength(0) && j_originale +l< this.image.GetLength(1) && i_originale+k >= 0 && j_originale+l >= 0)
                            {
                                somme_R += this.image[i_originale + k, j_originale + l].R;
                                somme_G += this.image[i_originale + k, j_originale + l].G;
                                somme_B += this.image[i_originale + k, j_originale + l].B;
                            }

                            //Console.WriteLine(somme_R + " " + somme_G + " " + somme_B);
                        }
                    }
                    if(i_rétréci < copie.image.GetLength(0) && j_rétréci<copie.image.GetLength(1) && i_rétréci>=0 && j_rétréci >= 0)
                    {
                        copie.image[i_rétréci, j_rétréci] = new Pixel( somme_R / (hauteur * largeur), somme_G / (hauteur * largeur), somme_B / (hauteur * largeur));
                    }
                    j_rétréci++;
                    j++;
                    if (j > valeurs_rectangles.Count - 1) { j = 0; }
                    largeur = valeurs_rectangles[j];
                }
                i_rétréci++;
                if(i_rétréci == 6) 
                { }
                i++;
                if (i > valeurs_rectangles.Count -1) { i = 0; }
                hauteur = valeurs_rectangles[i];
            }
            return copie;
        }
        #endregion
        #region Flou_Convolution
        public MyImage Flou( int[,] matrice_convolution, int coeff)
{
    MyImage copie = new MyImage(this, this.height, this.width);
    for(int i = 0; i< this.image.GetLength(0); i++)
    {
        for(int j = 0; j< this.image.GetLength(1); j++)
        {

            if(i==0 && j == 0)
            {
                int R= (int) (this.image[this.height - 1, this.width - 1].R * matrice_convolution[0, 0] + this.image[this.height - 1, j].R * matrice_convolution[0, 1] + this.image[this.height - 1, j + 1].R * matrice_convolution[0, 2] + this.image[i, j + 1].R * matrice_convolution[1, 2] + this.image[i + 1, j + 1].R * matrice_convolution[2, 2] + this.image[i + 1, j].R * matrice_convolution[2, 1] + this.image[i + 1, this.width - 1].R * matrice_convolution[2, 0] + this.image[i, this.width - 1].R * matrice_convolution[1, 0] + this.image[i, j].R * matrice_convolution[1, 1]);
                int G=(int) (this.image[this.height - 1, this.width - 1].G * matrice_convolution[0, 0] + this.image[this.height - 1, j].G * matrice_convolution[0, 1] + this.image[this.height - 1, j + 1].G * matrice_convolution[0, 2] + this.image[i, j + 1].G * matrice_convolution[1, 2] + this.image[i + 1, j + 1].G * matrice_convolution[2, 2] + this.image[i + 1, j].G * matrice_convolution[2, 1] + this.image[i + 1, this.width - 1].G * matrice_convolution[2, 0] + this.image[i, this.width - 1].G * matrice_convolution[1, 0] + this.image[i, j].G * matrice_convolution[1, 1]);
                int B=(int) (this.image[this.height - 1, this.width - 1].B * matrice_convolution[0, 0] + this.image[this.height - 1, j].B * matrice_convolution[0, 1] + this.image[this.height - 1, j + 1].B * matrice_convolution[0, 2] + this.image[i, j + 1].B * matrice_convolution[1, 2] + this.image[i + 1, j + 1].B * matrice_convolution[2, 2] + this.image[i + 1, j].B * matrice_convolution[2, 1] + this.image[i + 1, this.width - 1].B * matrice_convolution[2, 0] + this.image[i, this.width - 1].B * matrice_convolution[1, 0] + this.image[i, j].B * matrice_convolution[1, 1]);
                R = R/coeff;
                G = G/coeff;
                B = B/coeff;
                if (R > 255) { R = 255;}
                if (R < 0) { R = 0;}
                if (G > 255) { G = 255;}
                if (G < 0) { G = 0;}
                if (B > 255) { B = 255;}
                if (B < 0) { B = 0;}
                copie.image[i,j] = new Pixel(R, G, B);
            }
            if(i == 0 && j == this.image.GetLength(1) - 1)
            {
                int R= (int) (this.image[this.height - 1, j - 1].R * matrice_convolution[0, 0] + this.image[this.height-1, j].R * matrice_convolution[0, 1] + this.image[this.height - 1, 0].R * matrice_convolution[0, 2] + this.image[i, 0].R * matrice_convolution[1, 2] + this.image[i + 1, 0].R * matrice_convolution[2, 2] + this.image[i + 1, j].R * matrice_convolution[2, 1] + this.image[i + 1, j - 1].R * matrice_convolution[2, 0] + this.image[i, j - 1].R * matrice_convolution[1, 0] + this.image[i, j].R * matrice_convolution[1, 1]);
                int G= (int) (this.image[this.height - 1, j - 1].G * matrice_convolution[0, 0] + this.image[this.height-1, j].G * matrice_convolution[0, 1] + this.image[this.height - 1, 0].G * matrice_convolution[0, 2] + this.image[i, 0].G * matrice_convolution[1, 2] + this.image[i + 1, 0].G * matrice_convolution[2, 2] + this.image[i + 1, j].G * matrice_convolution[2, 1] + this.image[i + 1, j - 1].G * matrice_convolution[2, 0] + this.image[i, j - 1].G * matrice_convolution[1, 0] + this.image[i, j].G * matrice_convolution[1, 1]);
                int B= (int) (this.image[this.height - 1, j - 1].B * matrice_convolution[0, 0] + this.image[this.height-1, j].B * matrice_convolution[0, 1] + this.image[this.height - 1, 0].B * matrice_convolution[0, 2] + this.image[i, 0].B * matrice_convolution[1, 2] + this.image[i + 1, 0].B * matrice_convolution[2, 2] + this.image[i + 1, j].B * matrice_convolution[2, 1] + this.image[i + 1, j - 1].B * matrice_convolution[2, 0] + this.image[i, j - 1].B * matrice_convolution[1, 0] + this.image[i, j].B * matrice_convolution[1, 1]);
                R = R/coeff;
                G = G/coeff;
                B = B/coeff;
                if (R > 255) { R = 255;}
                if (R < 0) { R = 0;}
                if (G > 255) { G = 255;}
                if (G < 0) { G = 0;}
                if (B > 255) { B = 255;}
                if (B < 0) { B = 0;}
                copie.image[i,j] = new Pixel(R, G, B);               
            }
            if(i==this.image.GetLength(0) - 1 && j == this.image.GetLength(1) - 1)
            {
                int R= (int) (this.image[i - 1, j - 1].R * matrice_convolution[0, 0] + this.image[i - 1, j].R * matrice_convolution[0, 1] + this.image[i - 1, 0].R * matrice_convolution[0, 2] + this.image[i, 0].R * matrice_convolution[1, 2] + this.image[0, 0].R * matrice_convolution[2, 2] + this.image[0, j].R * matrice_convolution[2, 1] + this.image[0, j - 1].R * matrice_convolution[2, 0] + this.image[i, j - 1].R * matrice_convolution[1, 0] + this.image[i, j].R * matrice_convolution[1, 1]);
                int G= (int) (this.image[i - 1, j - 1].G * matrice_convolution[0, 0] + this.image[i - 1, j].G * matrice_convolution[0, 1] + this.image[i - 1, 0].G * matrice_convolution[0, 2] + this.image[i, 0].G * matrice_convolution[1, 2] + this.image[0, 0].G * matrice_convolution[2, 2] + this.image[0, j].G * matrice_convolution[2, 1] + this.image[0, j - 1].G * matrice_convolution[2, 0] + this.image[i, j - 1].G * matrice_convolution[1, 0] + this.image[i, j].G * matrice_convolution[1, 1]);
                int B = (int) (this.image[i - 1, j - 1].B * matrice_convolution[0, 0] + this.image[i - 1, j].B * matrice_convolution[0, 1] + this.image[i - 1, 0].B * matrice_convolution[0, 2] + this.image[i, 0].B * matrice_convolution[1, 2] + this.image[0, 0].B * matrice_convolution[2, 2] + this.image[0, j].B * matrice_convolution[2, 1] + this.image[0, j - 1].B * matrice_convolution[2, 0] + this.image[i, j - 1].B * matrice_convolution[1, 0] + this.image[i, j].B * matrice_convolution[1, 1]);
                R = R/coeff;
                G = G/coeff;
                B = B/coeff;
                if (R > 255) { R = 255;}
                if (R < 0) { R = 0;}
                if (G > 255) { G = 255;}
                if (G < 0) { G = 0;}
                if (B > 255) { B = 255;}
                if (B < 0) { B = 0;}
                copie.image[i,j] = new Pixel(R, G, B);
            }
            if(i == this.image.GetLength(0) - 1 && j == 0)
            {
                int R= (int) (this.image[i - 1, this.width - 1].R * matrice_convolution[0, 0] + this.image[i - 1, j].R * matrice_convolution[0, 1] + this.image[i - 1, j + 1].R * matrice_convolution[0, 2] + this.image[i, j + 1].R * matrice_convolution[1, 2] + this.image[0, j + 1].R * matrice_convolution[2, 2] + this.image[0, j].R * matrice_convolution[2, 1] + this.image[0, this.width - 1].R * matrice_convolution[2, 0] + this.image[i, this.width - 1].R * matrice_convolution[1, 0] + this.image[i, j].R * matrice_convolution[1, 1]);
                int G= (int) (this.image[i - 1, this.width - 1].G * matrice_convolution[0, 0] + this.image[i - 1, j].G * matrice_convolution[0, 1] + this.image[i - 1, j + 1].G * matrice_convolution[0, 2] + this.image[i, j + 1].G * matrice_convolution[1, 2] + this.image[0, j + 1].G * matrice_convolution[2, 2] + this.image[0, j].G * matrice_convolution[2, 1] + this.image[0, this.width - 1].G * matrice_convolution[2, 0] + this.image[i, this.width - 1].G * matrice_convolution[1, 0] + this.image[i, j].G * matrice_convolution[1, 1]);
                int B = (int) (this.image[i - 1, this.width - 1].B * matrice_convolution[0, 0] + this.image[i - 1, j].B * matrice_convolution[0, 1] + this.image[i - 1, j + 1].B * matrice_convolution[0, 2] + this.image[i, j + 1].B * matrice_convolution[1, 2] + this.image[0, j + 1].B * matrice_convolution[2, 2] + this.image[0, j].B * matrice_convolution[2, 1] + this.image[0, this.width - 1].B * matrice_convolution[2, 0] + this.image[i, this.width - 1].B * matrice_convolution[1, 0] + this.image[i, j].B * matrice_convolution[1, 1]);
                R = R/coeff;
                G = G/coeff;
                B = B/coeff;
                if (R > 255) { R = 255;}
                if (R < 0) { R = 0;}
                if (G > 255) { G = 255;}
                if (G < 0) { G = 0;}
                if (B > 255) { B = 255;}
                if (B < 0) { B = 0;}
                copie.image[i,j] = new Pixel(R, G, B);
            }
            if(i== 0 && j != 0 && j != this.width -1)
            {
                int R= (int) (this.image[this.height -1, j - 1].R * matrice_convolution[0, 0] + this.image[this.height -1, j].R * matrice_convolution[0, 1] + this.image[this.height -1, j + 1].R * matrice_convolution[0, 2] + this.image[i, j + 1].R * matrice_convolution[1, 2] + this.image[i + 1, j + 1].R * matrice_convolution[2, 2] + this.image[i + 1, j].R * matrice_convolution[2, 1] + this.image[i + 1, j - 1].R * matrice_convolution[2, 0] + this.image[i, j - 1].R * matrice_convolution[1, 0] + this.image[i, j].R * matrice_convolution[1, 1]);
                int G= (int) (this.image[this.height -1, j - 1].G * matrice_convolution[0, 0] + this.image[this.height -1, j].G * matrice_convolution[0, 1] + this.image[this.height -1, j + 1].G * matrice_convolution[0, 2] + this.image[i, j + 1].G * matrice_convolution[1, 2] + this.image[i + 1, j + 1].G * matrice_convolution[2, 2] + this.image[i + 1, j].G * matrice_convolution[2, 1] + this.image[i + 1, j - 1].G * matrice_convolution[2, 0] + this.image[i, j - 1].G * matrice_convolution[1, 0] + this.image[i, j].G * matrice_convolution[1, 1]);
                int B = (int) (this.image[this.height -1, j - 1].B * matrice_convolution[0, 0] + this.image[this.height -1, j].B * matrice_convolution[0, 1] + this.image[this.height -1, j + 1].B * matrice_convolution[0, 2] + this.image[i, j + 1].B * matrice_convolution[1, 2] + this.image[i + 1, j + 1].B * matrice_convolution[2, 2] + this.image[i + 1, j].B * matrice_convolution[2, 1] + this.image[i + 1, j - 1].B * matrice_convolution[2, 0] + this.image[i, j - 1].B * matrice_convolution[1, 0] + this.image[i, j].B * matrice_convolution[1, 1]);
                R = R/coeff;
                G = G/coeff;
                B = B/coeff;
                if (R > 255) { R = 255;}
                if (R < 0) { R = 0;}
                if (G > 255) { G = 255;}
                if (G < 0) { G = 0;}
                if (B > 255) { B = 255;}
                if (B < 0) { B = 0;}
                copie.image[i,j] = new Pixel(R, G, B);  
            }
            if(i == this.image.GetLength(0) - 1 && j != 0 && j != this.width -1 )
            {
                int R= (int) (this.image[i - 1, j - 1].R * matrice_convolution[0, 0] + this.image[i - 1, j].R * matrice_convolution[0, 1] + this.image[i - 1, j + 1].R * matrice_convolution[0, 2] + this.image[i, j + 1].R * matrice_convolution[1, 2] + this.image[0, j + 1].R * matrice_convolution[2, 2] + this.image[0, j].R * matrice_convolution[2, 1] + this.image[0, j - 1].R * matrice_convolution[2, 0] + this.image[i, j - 1].R * matrice_convolution[1, 0] + this.image[i, j].R * matrice_convolution[1, 1]);
                int G=  (int) (this.image[i - 1, j - 1].G * matrice_convolution[0, 0] + this.image[i - 1, j].G * matrice_convolution[0, 1] + this.image[i - 1, j + 1].G * matrice_convolution[0, 2] + this.image[i, j + 1].G * matrice_convolution[1, 2] + this.image[0, j + 1].G * matrice_convolution[2, 2] + this.image[0, j].G * matrice_convolution[2, 1] + this.image[0, j - 1].G * matrice_convolution[2, 0] + this.image[i, j - 1].G * matrice_convolution[1, 0] + this.image[i, j].G * matrice_convolution[1, 1]);
                int B = (int) (this.image[i - 1, j - 1].B * matrice_convolution[0, 0] + this.image[i - 1, j].B * matrice_convolution[0, 1] + this.image[i - 1, j + 1].B * matrice_convolution[0, 2] + this.image[i, j + 1].B * matrice_convolution[1, 2] + this.image[0, j + 1].B * matrice_convolution[2, 2] + this.image[0, j].B * matrice_convolution[2, 1] + this.image[0, j - 1].B * matrice_convolution[2, 0] + this.image[i, j - 1].B * matrice_convolution[1, 0] + this.image[i, j].B * matrice_convolution[1, 1]);
                R = R/coeff;
                G = G/coeff;
                B = B/coeff;
                if (R > 255) { R = 255;}
                if (R < 0) { R = 0;}
                if (G > 255) { G = 255;}
                if (G < 0) { G = 0;}
                if (B > 255) { B = 255;}
                if (B < 0) { B = 0;}
                copie.image[i,j] = new Pixel(R, G, B);
            }
            if(j==this.image.GetLength(1) - 1 && i != 0 && i != this.height -1 )
            {
                int R= (int) (this.image[i - 1, j - 1].R * matrice_convolution[0, 0] + this.image[i - 1, j].R * matrice_convolution[0, 1] + this.image[i - 1, 0].R * matrice_convolution[0, 2] + this.image[i, 0].R * matrice_convolution[1, 2] + this.image[i + 1, 0].R * matrice_convolution[2, 2] + this.image[i + 1, j].R * matrice_convolution[2, 1] + this.image[i + 1, j - 1].R * matrice_convolution[2, 0] + this.image[i, j - 1].R * matrice_convolution[1, 0] + this.image[i, j].R * matrice_convolution[1, 1]);
                int G=  (int) (this.image[i - 1, j - 1].G * matrice_convolution[0, 0] + this.image[i - 1, j].G * matrice_convolution[0, 1] + this.image[i - 1, 0].G * matrice_convolution[0, 2] + this.image[i, 0].G * matrice_convolution[1, 2] + this.image[i + 1, 0].G * matrice_convolution[2, 2] + this.image[i + 1, j].G * matrice_convolution[2, 1] + this.image[i + 1, j - 1].G * matrice_convolution[2, 0] + this.image[i, j - 1].G * matrice_convolution[1, 0] + this.image[i, j].G * matrice_convolution[1, 1]);
                int B = (int) (this.image[i - 1, j - 1].B * matrice_convolution[0, 0] + this.image[i - 1, j].B * matrice_convolution[0, 1] + this.image[i - 1, 0].B * matrice_convolution[0, 2] + this.image[i, 0].B * matrice_convolution[1, 2] + this.image[i + 1, 0].B * matrice_convolution[2, 2] + this.image[i + 1, j].B * matrice_convolution[2, 1] + this.image[i + 1, j - 1].B * matrice_convolution[2, 0] + this.image[i, j - 1].B * matrice_convolution[1, 0] + this.image[i, j].B * matrice_convolution[1, 1]);
                R = R/coeff;
                G = G/coeff;
                B = B/coeff;
                if (R > 255) { R = 255;}
                if (R < 0) { R = 0;}
                if (G > 255) { G = 255;}
                if (G < 0) { G = 0;}
                if (B > 255) { B = 255;}
                if (B < 0) { B = 0;}
                 copie.image[i,j] = new Pixel(R, G, B);
            }
            if(j == 0 && i != 0 && i != this.height -1 )
            {
                int R= (int) (this.image[i - 1, this.width - 1].R * matrice_convolution[0, 0] + this.image[i - 1, j].R * matrice_convolution[0, 1] + this.image[i - 1, j + 1].R * matrice_convolution[0, 2] + this.image[i, j + 1].R * matrice_convolution[1, 2] + this.image[i + 1, j + 1].R * matrice_convolution[2, 2] + this.image[i + 1, j].R * matrice_convolution[2, 1] + this.image[i + 1, this.width - 1].R * matrice_convolution[2, 0] + this.image[i, this.width - 1].R * matrice_convolution[1, 0] + this.image[i, j].R * matrice_convolution[1, 1]);
                int G= (int) (this.image[i - 1, this.width - 1].G * matrice_convolution[0, 0] + this.image[i - 1, j].G * matrice_convolution[0, 1] + this.image[i - 1, j + 1].G * matrice_convolution[0, 2] + this.image[i, j + 1].G * matrice_convolution[1, 2] + this.image[i + 1, j + 1].G * matrice_convolution[2, 2] + this.image[i + 1, j].G * matrice_convolution[2, 1] + this.image[i + 1, this.width - 1].G * matrice_convolution[2, 0] + this.image[i, this.width - 1].G * matrice_convolution[1, 0] + this.image[i, j].G * matrice_convolution[1, 1]);
                int B = (int) (this.image[i - 1, this.width - 1].B * matrice_convolution[0, 0] + this.image[i - 1, j].B * matrice_convolution[0, 1] + this.image[i - 1, j + 1].B * matrice_convolution[0, 2] + this.image[i, j + 1].B * matrice_convolution[1, 2] + this.image[i + 1, j + 1].B * matrice_convolution[2, 2] + this.image[i + 1, j].B * matrice_convolution[2, 1] + this.image[i + 1, this.width - 1].B * matrice_convolution[2, 0] + this.image[i, this.width - 1].B * matrice_convolution[1, 0] + this.image[i, j].B * matrice_convolution[1, 1]);
                R = R/coeff;
                G = G/coeff;
                B = B/coeff;
                if (R > 255) { R = 255;}
                if (R < 0) { R = 0;}
                if (G > 255) { G = 255;}
                if (G < 0) { G = 0;}
                if (B > 255) { B = 255;}
                if (B < 0) { B = 0;}
                copie.image[i,j] = new Pixel(R, G, B);
            }

            if(j>0 && i>0 && i<this.image.GetLength(0) -1 && j < this.image.GetLength(1) -1)
            {
                int R= (int) (this.image[i - 1, j - 1].R * matrice_convolution[0, 0] + this.image[i - 1, j].R * matrice_convolution[0, 1] + this.image[i - 1, j + 1].R * matrice_convolution[0, 2] + this.image[i, j + 1].R * matrice_convolution[1, 2] + this.image[i + 1, j + 1].R * matrice_convolution[2, 2] + this.image[i + 1, j].R * matrice_convolution[2, 1] + this.image[i + 1, j - 1].R * matrice_convolution[2, 0] + this.image[i, j - 1].R * matrice_convolution[1, 0] + this.image[i, j].R * matrice_convolution[1, 1]);
                int G= (int) (this.image[i - 1, j - 1].G * matrice_convolution[0, 0] + this.image[i - 1, j].G * matrice_convolution[0, 1] + this.image[i - 1, j + 1].G * matrice_convolution[0, 2] + this.image[i, j + 1].G * matrice_convolution[1, 2] + this.image[i + 1, j + 1].G * matrice_convolution[2, 2] + this.image[i + 1, j].G * matrice_convolution[2, 1] + this.image[i + 1, j - 1].G * matrice_convolution[2, 0] + this.image[i, j - 1].G * matrice_convolution[1, 0] + this.image[i, j].G * matrice_convolution[1, 1]);
                int B= (int) (this.image[i - 1, j - 1].B * matrice_convolution[0, 0] + this.image[i - 1, j].B * matrice_convolution[0, 1] + this.image[i - 1, j + 1].B * matrice_convolution[0, 2] + this.image[i, j + 1].B * matrice_convolution[1, 2] + this.image[i + 1, j + 1].B * matrice_convolution[2, 2] + this.image[i + 1, j].B * matrice_convolution[2, 1] + this.image[i + 1, j - 1].B * matrice_convolution[2, 0] + this.image[i, j - 1].B * matrice_convolution[1, 0] + this.image[i, j].B * matrice_convolution[1, 1]);
                R = R/coeff;
                G = G/coeff;
                B = B/coeff;
                if (R > 255) { R = 255;}
                if (R < 0) { R = 0;}
                if (G > 255) { G = 255;}
                if (G < 0) { G = 0;}
                if (B > 255) { B = 255;}
                if (B < 0) { B = 0;}
                copie.image[i,j] = new Pixel(R, G, B);
            }
        }
    }
    return copie;
}
        public MyImage Matrice_de_convolution( int[,] matrice_convolution)
{
    MyImage copie = new MyImage(this, this.height, this.width);
    for(int i = 0; i< this.image.GetLength(0); i++)
    {
        for(int j = 0; j< this.image.GetLength(1); j++)
        {

            if(i==0 && j == 0)
            {
                int R= (int) (this.image[this.height - 1, this.width - 1].R * matrice_convolution[0, 0] + this.image[this.height - 1, j].R * matrice_convolution[0, 1] + this.image[this.height - 1, j + 1].R * matrice_convolution[0, 2] + this.image[i, j + 1].R * matrice_convolution[1, 2] + this.image[i + 1, j + 1].R * matrice_convolution[2, 2] + this.image[i + 1, j].R * matrice_convolution[2, 1] + this.image[i + 1, this.width - 1].R * matrice_convolution[2, 0] + this.image[i, this.width - 1].R * matrice_convolution[1, 0] + this.image[i, j].R * matrice_convolution[1, 1]);
                int G=(int) (this.image[this.height - 1, this.width - 1].G * matrice_convolution[0, 0] + this.image[this.height - 1, j].G * matrice_convolution[0, 1] + this.image[this.height - 1, j + 1].G * matrice_convolution[0, 2] + this.image[i, j + 1].G * matrice_convolution[1, 2] + this.image[i + 1, j + 1].G * matrice_convolution[2, 2] + this.image[i + 1, j].G * matrice_convolution[2, 1] + this.image[i + 1, this.width - 1].G * matrice_convolution[2, 0] + this.image[i, this.width - 1].G * matrice_convolution[1, 0] + this.image[i, j].G * matrice_convolution[1, 1]);
                int B=(int) (this.image[this.height - 1, this.width - 1].B * matrice_convolution[0, 0] + this.image[this.height - 1, j].B * matrice_convolution[0, 1] + this.image[this.height - 1, j + 1].B * matrice_convolution[0, 2] + this.image[i, j + 1].B * matrice_convolution[1, 2] + this.image[i + 1, j + 1].B * matrice_convolution[2, 2] + this.image[i + 1, j].B * matrice_convolution[2, 1] + this.image[i + 1, this.width - 1].B * matrice_convolution[2, 0] + this.image[i, this.width - 1].B * matrice_convolution[1, 0] + this.image[i, j].B * matrice_convolution[1, 1]);
                if (R > 255) { R = 255;}
                if (R < 0) { R = 0;}
                if (G > 255) { G = 255;}
                if (G < 0) { G = 0;}
                if (B > 255) { B = 255;}
                if (B < 0) { B = 0;}
                copie.image[i,j] = new Pixel(R,G,B);
            }
            if(i == 0 && j == this.image.GetLength(1) - 1)
            {
                int R= (int) (this.image[this.height - 1, j - 1].R * matrice_convolution[0, 0] + this.image[this.height-1, j].R * matrice_convolution[0, 1] + this.image[this.height - 1, 0].R * matrice_convolution[0, 2] + this.image[i, 0].R * matrice_convolution[1, 2] + this.image[i + 1, 0].R * matrice_convolution[2, 2] + this.image[i + 1, j].R * matrice_convolution[2, 1] + this.image[i + 1, j - 1].R * matrice_convolution[2, 0] + this.image[i, j - 1].R * matrice_convolution[1, 0] + this.image[i, j].R * matrice_convolution[1, 1]);
                int G= (int) (this.image[this.height - 1, j - 1].G * matrice_convolution[0, 0] + this.image[this.height-1, j].G * matrice_convolution[0, 1] + this.image[this.height - 1, 0].G * matrice_convolution[0, 2] + this.image[i, 0].G * matrice_convolution[1, 2] + this.image[i + 1, 0].G * matrice_convolution[2, 2] + this.image[i + 1, j].G * matrice_convolution[2, 1] + this.image[i + 1, j - 1].G * matrice_convolution[2, 0] + this.image[i, j - 1].G * matrice_convolution[1, 0] + this.image[i, j].G * matrice_convolution[1, 1]);
                int B= (int) (this.image[this.height - 1, j - 1].B * matrice_convolution[0, 0] + this.image[this.height-1, j].B * matrice_convolution[0, 1] + this.image[this.height - 1, 0].B * matrice_convolution[0, 2] + this.image[i, 0].B * matrice_convolution[1, 2] + this.image[i + 1, 0].B * matrice_convolution[2, 2] + this.image[i + 1, j].B * matrice_convolution[2, 1] + this.image[i + 1, j - 1].B * matrice_convolution[2, 0] + this.image[i, j - 1].B * matrice_convolution[1, 0] + this.image[i, j].B * matrice_convolution[1, 1]);
                if (R > 255) { R = 255;}
                if (R < 0) { R = 0;}
                if (G > 255) { G = 255;}
                if (G < 0) { G = 0;}
                if (B > 255) { B = 255;}
                if (B < 0) { B = 0;}
                copie.image[i,j] = new Pixel(R,G,B);
            }
            if(i==this.image.GetLength(0) - 1 && j == this.image.GetLength(1) - 1)
            {
                int R= (int) (this.image[i - 1, j - 1].R * matrice_convolution[0, 0] + this.image[i - 1, j].R * matrice_convolution[0, 1] + this.image[i - 1, 0].R * matrice_convolution[0, 2] + this.image[i, 0].R * matrice_convolution[1, 2] + this.image[0, 0].R * matrice_convolution[2, 2] + this.image[0, j].R * matrice_convolution[2, 1] + this.image[0, j - 1].R * matrice_convolution[2, 0] + this.image[i, j - 1].R * matrice_convolution[1, 0] + this.image[i, j].R * matrice_convolution[1, 1]);
                int G= (int) (this.image[i - 1, j - 1].G * matrice_convolution[0, 0] + this.image[i - 1, j].G * matrice_convolution[0, 1] + this.image[i - 1, 0].G * matrice_convolution[0, 2] + this.image[i, 0].G * matrice_convolution[1, 2] + this.image[0, 0].G * matrice_convolution[2, 2] + this.image[0, j].G * matrice_convolution[2, 1] + this.image[0, j - 1].G * matrice_convolution[2, 0] + this.image[i, j - 1].G * matrice_convolution[1, 0] + this.image[i, j].G * matrice_convolution[1, 1]);
                int B = (int) (this.image[i - 1, j - 1].B * matrice_convolution[0, 0] + this.image[i - 1, j].B * matrice_convolution[0, 1] + this.image[i - 1, 0].B * matrice_convolution[0, 2] + this.image[i, 0].B * matrice_convolution[1, 2] + this.image[0, 0].B * matrice_convolution[2, 2] + this.image[0, j].B * matrice_convolution[2, 1] + this.image[0, j - 1].B * matrice_convolution[2, 0] + this.image[i, j - 1].B * matrice_convolution[1, 0] + this.image[i, j].B * matrice_convolution[1, 1]);
                if (R > 255) { R = 255;}
                if (R < 0) { R = 0;}
                if (G > 255) { G = 255;}
                if (G < 0) { G = 0;}
                if (B > 255) { B = 255;}
                if (B < 0) { B = 0;}
                copie.image[i,j] = new Pixel(R,G,B);
            }
            if(i == this.image.GetLength(0) - 1 && j == 0)
            {
                int R= (int) (this.image[i - 1, this.width - 1].R * matrice_convolution[0, 0] + this.image[i - 1, j].R * matrice_convolution[0, 1] + this.image[i - 1, j + 1].R * matrice_convolution[0, 2] + this.image[i, j + 1].R * matrice_convolution[1, 2] + this.image[0, j + 1].R * matrice_convolution[2, 2] + this.image[0, j].R * matrice_convolution[2, 1] + this.image[0, this.width - 1].R * matrice_convolution[2, 0] + this.image[i, this.width - 1].R * matrice_convolution[1, 0] + this.image[i, j].R * matrice_convolution[1, 1]);
                int G= (int) (this.image[i - 1, this.width - 1].G * matrice_convolution[0, 0] + this.image[i - 1, j].G * matrice_convolution[0, 1] + this.image[i - 1, j + 1].G * matrice_convolution[0, 2] + this.image[i, j + 1].G * matrice_convolution[1, 2] + this.image[0, j + 1].G * matrice_convolution[2, 2] + this.image[0, j].G * matrice_convolution[2, 1] + this.image[0, this.width - 1].G * matrice_convolution[2, 0] + this.image[i, this.width - 1].G * matrice_convolution[1, 0] + this.image[i, j].G * matrice_convolution[1, 1]);
                int B = (int) (this.image[i - 1, this.width - 1].B * matrice_convolution[0, 0] + this.image[i - 1, j].B * matrice_convolution[0, 1] + this.image[i - 1, j + 1].B * matrice_convolution[0, 2] + this.image[i, j + 1].B * matrice_convolution[1, 2] + this.image[0, j + 1].B * matrice_convolution[2, 2] + this.image[0, j].B * matrice_convolution[2, 1] + this.image[0, this.width - 1].B * matrice_convolution[2, 0] + this.image[i, this.width - 1].B * matrice_convolution[1, 0] + this.image[i, j].B * matrice_convolution[1, 1]);
                if (R > 255) { R = 255;}
                if (R < 0) { R = 0;}
                if (G > 255) { G = 255;}
                if (G < 0) { G = 0;}
                if (B > 255) { B = 255;}
                if (B < 0) { B = 0;}
                copie.image[i,j] = new Pixel(R,G,B);
            }
            if(i== 0 && j != 0 && j != this.width -1)
            {
                int R= (int) (this.image[this.height -1, j - 1].R * matrice_convolution[0, 0] + this.image[this.height -1, j].R * matrice_convolution[0, 1] + this.image[this.height -1, j + 1].R * matrice_convolution[0, 2] + this.image[i, j + 1].R * matrice_convolution[1, 2] + this.image[i + 1, j + 1].R * matrice_convolution[2, 2] + this.image[i + 1, j].R * matrice_convolution[2, 1] + this.image[i + 1, j - 1].R * matrice_convolution[2, 0] + this.image[i, j - 1].R * matrice_convolution[1, 0] + this.image[i, j].R * matrice_convolution[1, 1]);
                int G= (int) (this.image[this.height -1, j - 1].G * matrice_convolution[0, 0] + this.image[this.height -1, j].G * matrice_convolution[0, 1] + this.image[this.height -1, j + 1].G * matrice_convolution[0, 2] + this.image[i, j + 1].G * matrice_convolution[1, 2] + this.image[i + 1, j + 1].G * matrice_convolution[2, 2] + this.image[i + 1, j].G * matrice_convolution[2, 1] + this.image[i + 1, j - 1].G * matrice_convolution[2, 0] + this.image[i, j - 1].G * matrice_convolution[1, 0] + this.image[i, j].G * matrice_convolution[1, 1]);
                int B = (int) (this.image[this.height -1, j - 1].B * matrice_convolution[0, 0] + this.image[this.height -1, j].B * matrice_convolution[0, 1] + this.image[this.height -1, j + 1].B * matrice_convolution[0, 2] + this.image[i, j + 1].B * matrice_convolution[1, 2] + this.image[i + 1, j + 1].B * matrice_convolution[2, 2] + this.image[i + 1, j].B * matrice_convolution[2, 1] + this.image[i + 1, j - 1].B * matrice_convolution[2, 0] + this.image[i, j - 1].B * matrice_convolution[1, 0] + this.image[i, j].B * matrice_convolution[1, 1]);
                if (R > 255) { R = 255;}
                if (R < 0) { R = 0;}
                if (G > 255) { G = 255;}
                if (G < 0) { G = 0;}
                if (B > 255) { B = 255;}
                if (B < 0) { B = 0;}
                copie.image[i,j] = new Pixel(R,G,B);
            }
            if(i == this.image.GetLength(0) - 1 && j != 0 && j != this.width -1 )
            {
                int R= (int) (this.image[i - 1, j - 1].R * matrice_convolution[0, 0] + this.image[i - 1, j].R * matrice_convolution[0, 1] + this.image[i - 1, j + 1].R * matrice_convolution[0, 2] + this.image[i, j + 1].R * matrice_convolution[1, 2] + this.image[0, j + 1].R * matrice_convolution[2, 2] + this.image[0, j].R * matrice_convolution[2, 1] + this.image[0, j - 1].R * matrice_convolution[2, 0] + this.image[i, j - 1].R * matrice_convolution[1, 0] + this.image[i, j].R * matrice_convolution[1, 1]);
                int G=  (int) (this.image[i - 1, j - 1].G * matrice_convolution[0, 0] + this.image[i - 1, j].G * matrice_convolution[0, 1] + this.image[i - 1, j + 1].G * matrice_convolution[0, 2] + this.image[i, j + 1].G * matrice_convolution[1, 2] + this.image[0, j + 1].G * matrice_convolution[2, 2] + this.image[0, j].G * matrice_convolution[2, 1] + this.image[0, j - 1].G * matrice_convolution[2, 0] + this.image[i, j - 1].G * matrice_convolution[1, 0] + this.image[i, j].G * matrice_convolution[1, 1]);
                int B = (int) (this.image[i - 1, j - 1].B * matrice_convolution[0, 0] + this.image[i - 1, j].B * matrice_convolution[0, 1] + this.image[i - 1, j + 1].B * matrice_convolution[0, 2] + this.image[i, j + 1].B * matrice_convolution[1, 2] + this.image[0, j + 1].B * matrice_convolution[2, 2] + this.image[0, j].B * matrice_convolution[2, 1] + this.image[0, j - 1].B * matrice_convolution[2, 0] + this.image[i, j - 1].B * matrice_convolution[1, 0] + this.image[i, j].B * matrice_convolution[1, 1]);
                if (R > 255) { R = 255;}
                if (R < 0) { R = 0;}
                if (G > 255) { G = 255;}
                if (G < 0) { G = 0;}
                if (B > 255) { B = 255;}
                if (B < 0) { B = 0;}
                copie.image[i,j] = new Pixel(R,G,B);
            }
            if(j==this.image.GetLength(1) - 1 && i != 0 && i != this.height -1 )
            {
                int R= (int) (this.image[i - 1, j - 1].R * matrice_convolution[0, 0] + this.image[i - 1, j].R * matrice_convolution[0, 1] + this.image[i - 1, 0].R * matrice_convolution[0, 2] + this.image[i, 0].R * matrice_convolution[1, 2] + this.image[i + 1, 0].R * matrice_convolution[2, 2] + this.image[i + 1, j].R * matrice_convolution[2, 1] + this.image[i + 1, j - 1].R * matrice_convolution[2, 0] + this.image[i, j - 1].R * matrice_convolution[1, 0] + this.image[i, j].R * matrice_convolution[1, 1]);
                int G=  (int) (this.image[i - 1, j - 1].G * matrice_convolution[0, 0] + this.image[i - 1, j].G * matrice_convolution[0, 1] + this.image[i - 1, 0].G * matrice_convolution[0, 2] + this.image[i, 0].G * matrice_convolution[1, 2] + this.image[i + 1, 0].G * matrice_convolution[2, 2] + this.image[i + 1, j].G * matrice_convolution[2, 1] + this.image[i + 1, j - 1].G * matrice_convolution[2, 0] + this.image[i, j - 1].G * matrice_convolution[1, 0] + this.image[i, j].G * matrice_convolution[1, 1]);
                int B = (int) (this.image[i - 1, j - 1].B * matrice_convolution[0, 0] + this.image[i - 1, j].B * matrice_convolution[0, 1] + this.image[i - 1, 0].B * matrice_convolution[0, 2] + this.image[i, 0].B * matrice_convolution[1, 2] + this.image[i + 1, 0].B * matrice_convolution[2, 2] + this.image[i + 1, j].B * matrice_convolution[2, 1] + this.image[i + 1, j - 1].B * matrice_convolution[2, 0] + this.image[i, j - 1].B * matrice_convolution[1, 0] + this.image[i, j].B * matrice_convolution[1, 1]);
                if (R > 255) { R = 255;}
                if (R < 0) { R = 0;}
                if (G > 255) { G = 255;}
                if (G < 0) { G = 0;}
                if (B > 255) { B = 255;}
                if (B < 0) { B = 0;}
                copie.image[i,j] = new Pixel(R,G,B);
            }
            if(j == 0 && i != 0 && i != this.height -1 )
            {
                int R= (int) (this.image[i - 1, this.width - 1].R * matrice_convolution[0, 0] + this.image[i - 1, j].R * matrice_convolution[0, 1] + this.image[i - 1, j + 1].R * matrice_convolution[0, 2] + this.image[i, j + 1].R * matrice_convolution[1, 2] + this.image[i + 1, j + 1].R * matrice_convolution[2, 2] + this.image[i + 1, j].R * matrice_convolution[2, 1] + this.image[i + 1, this.width - 1].R * matrice_convolution[2, 0] + this.image[i, this.width - 1].R * matrice_convolution[1, 0] + this.image[i, j].R * matrice_convolution[1, 1]);
                int G= (int) (this.image[i - 1, this.width - 1].G * matrice_convolution[0, 0] + this.image[i - 1, j].G * matrice_convolution[0, 1] + this.image[i - 1, j + 1].G * matrice_convolution[0, 2] + this.image[i, j + 1].G * matrice_convolution[1, 2] + this.image[i + 1, j + 1].G * matrice_convolution[2, 2] + this.image[i + 1, j].G * matrice_convolution[2, 1] + this.image[i + 1, this.width - 1].G * matrice_convolution[2, 0] + this.image[i, this.width - 1].G * matrice_convolution[1, 0] + this.image[i, j].G * matrice_convolution[1, 1]);
                int B = (int) (this.image[i - 1, this.width - 1].B * matrice_convolution[0, 0] + this.image[i - 1, j].B * matrice_convolution[0, 1] + this.image[i - 1, j + 1].B * matrice_convolution[0, 2] + this.image[i, j + 1].B * matrice_convolution[1, 2] + this.image[i + 1, j + 1].B * matrice_convolution[2, 2] + this.image[i + 1, j].B * matrice_convolution[2, 1] + this.image[i + 1, this.width - 1].B * matrice_convolution[2, 0] + this.image[i, this.width - 1].B * matrice_convolution[1, 0] + this.image[i, j].B * matrice_convolution[1, 1]);
                if (R > 255) { R = 255;}
                if (R < 0) { R = 0;}
                if (G > 255) { G = 255;}
                if (G < 0) { G = 0;}
                if (B > 255) { B = 255;}
                if (B < 0) { B = 0;}
                copie.image[i,j] = new Pixel(R,G,B);
            }

                    if(j>0 && i>0 && i<this.image.GetLength(0) -1 && j < this.image.GetLength(1) -1)
                    {
                        int R= (int) (this.image[i - 1, j - 1].R * matrice_convolution[0, 0] + this.image[i - 1, j].R * matrice_convolution[0, 1] + this.image[i - 1, j + 1].R * matrice_convolution[0, 2] + this.image[i, j + 1].R * matrice_convolution[1, 2] + this.image[i + 1, j + 1].R * matrice_convolution[2, 2] + this.image[i + 1, j].R * matrice_convolution[2, 1] + this.image[i + 1, j - 1].R * matrice_convolution[2, 0] + this.image[i, j - 1].R * matrice_convolution[1, 0] + this.image[i, j].R * matrice_convolution[1, 1]);
                        int G= (int) (this.image[i - 1, j - 1].G * matrice_convolution[0, 0] + this.image[i - 1, j].G * matrice_convolution[0, 1] + this.image[i - 1, j + 1].G * matrice_convolution[0, 2] + this.image[i, j + 1].G * matrice_convolution[1, 2] + this.image[i + 1, j + 1].G * matrice_convolution[2, 2] + this.image[i + 1, j].G * matrice_convolution[2, 1] + this.image[i + 1, j - 1].G * matrice_convolution[2, 0] + this.image[i, j - 1].G * matrice_convolution[1, 0] + this.image[i, j].G * matrice_convolution[1, 1]);
                        int B= (int) (this.image[i - 1, j - 1].B * matrice_convolution[0, 0] + this.image[i - 1, j].B * matrice_convolution[0, 1] + this.image[i - 1, j + 1].B * matrice_convolution[0, 2] + this.image[i, j + 1].B * matrice_convolution[1, 2] + this.image[i + 1, j + 1].B * matrice_convolution[2, 2] + this.image[i + 1, j].B * matrice_convolution[2, 1] + this.image[i + 1, j - 1].B * matrice_convolution[2, 0] + this.image[i, j - 1].B * matrice_convolution[1, 0] + this.image[i, j].B * matrice_convolution[1, 1]);
                        if (R > 255) { R = 255;}
                        if (R < 0) { R = 0;}
                        if (G > 255) { G = 255;}
                        if (G < 0) { G = 0;}
                        if (B > 255) { B = 255;}
                        if (B < 0) { B = 0;}
                        copie.image[i,j] = new Pixel(R,G,B);
                    }
                }
            }
            return copie;
        }
        #endregion

        #region TD 5 



        public static MyImage Fractale_MandelBrot(int coté)
        {
            MyImage fractale = new MyImage(coté,coté);
            double Reel_C;
            double Im_C;
            double Reel_Z;
            double Im_Z ;
            double MinX = -2.1;
            double MaxX = 2.1;
            double MinY = -1.2;
            double MaxY = 1.2;
            double module;
            double tmp;
            bool fuyant;
            int iterations = 20;
            Random rnd = new Random();
            int R;
            int G;
            int B;
            int mult_pas_R = rnd.Next(1, 10);
            int mult_pas_G = rnd.Next(1, 10);
            int mult_pas_B = rnd.Next(1, 10);

            for (int i = 0; i< coté; i++)
            {
                for(int j =0; j< coté; j++)
                {
                    fuyant = false;
                    Reel_C = MinX + ((Math.Abs(MinX)+ MaxX)/ coté) *i;
                    Im_C = MinY + ((Math.Abs(MinY)+ MaxY)/ coté) *j;
                    Reel_Z = 0;
                    Im_Z = 0;
                    R = 0;
                    G = 0;
                    B = 0;
                    for (int k = 0; k<iterations; k++)
                    {
                        tmp = Reel_Z;
                        Reel_Z = (Reel_Z*Reel_Z)-(Im_Z*Im_Z)+ Reel_C;
                        Im_Z = 2*tmp*Im_Z + Im_C;
                        module = Reel_Z*Reel_Z + Im_Z*Im_Z;
                        if(module > 4) { fractale.Image[i,j] = new Pixel(R, G, B); k=iterations; fuyant = true; }
                        R += mult_pas_R* (255 / iterations);
                        G += mult_pas_G * (255 / iterations);
                        B += mult_pas_B * (255 / iterations);
                        if (R > 255) { R = 122; }
                        if (G > 255) { G = 203; }
                        if (B > 255) { B = 50; }
                    }
                    if (!fuyant)
                    {
                        fractale.Image[i, j] = new Pixel(0,0,0);
                    }
                    
                }
            }
            return fractale;
        }
        public static MyImage FractaleJulia(
            int coté, 
            bool fractale_Random = true, 
            int index_Fractale = 0, 
            bool fractale_Personnalisé = false, 
            double reel_Personnalisé = 0,
            double im_Personnalisé = 0, 
            int itérations_Personnalisé = 0, 
            int mult_Couleurs_Personnalisé = 0)
        {
            // rajouter des valeurs d'itération et des mult pr les valeurs RGB des les tableaux
            #region Liste_Fractales
            List<double[]> Complexes_Julia = new List<double[]>();
            //0
            double[] Julia = { -1, 0 , 20,1};
            Complexes_Julia.Add(Julia);
            //1
            double[] Dendrite = { 0, 1, 20, 1 };
            Complexes_Julia.Add(Dendrite);
            //2
            double[] Poussière_Fatou1 = { -0.63, 0.67, 20, 1 };
            Complexes_Julia.Add(Poussière_Fatou1);
            //3
            double[] Poussière_Fatou2 = { -0.76, 0.12, 20, 1 };
            Complexes_Julia.Add(Poussière_Fatou2);
            //4
            double[] Lapin_Douady = { -0.122, 0.744, 20, 1 };
            Complexes_Julia.Add(Lapin_Douady);
            //5
            double[] Chou_Fleur = { 0.25, 0.0000015, 20, 1 };
            Complexes_Julia.Add(Chou_Fleur);
            //6
            double[] Siegel_Epais = { 0.375, 0.22, 20, 1 };
            Complexes_Julia.Add(Siegel_Epais);
            //7
            double[] Avion = { -1.75, 0, 20, 1 };
            Complexes_Julia.Add(Avion);
            //8
            double[] Avion_Epais = { -1.31, 0, 20, 1 };
            Complexes_Julia.Add(Avion_Epais);
            //9
            double[] Lemniscate_Bernouilli = { -2, 0, 20, 1 };
            Complexes_Julia.Add(Lemniscate_Bernouilli);
            //10
            double[] Poussière_Fatou3 = { 0.35, 0.05, 20, 1 };// Au dessus 20 1
            Complexes_Julia.Add(Poussière_Fatou3);
            //11
            double[] Galaxie1 = { -0.7, 0.26,230,20 }; //Rajouter valeurs d'itération 230 ( doit être inférieure à 255) et mult couleurs : 20
            Complexes_Julia.Add(Galaxie1);
            //12
            double[] Galaxie2 = { -0.7, 0.27,200,20 };//200 20
            Complexes_Julia.Add(Galaxie2);
            //13
            double[] Galaxie3 = { -0.7, 0.287,150,1 };//150 1
            Complexes_Julia.Add(Galaxie3);
            //14
            double[] Galaxie4 = { -0.7, 0.3 ,150,1};//150 1
            Complexes_Julia.Add(Galaxie4);
            #endregion

            MyImage fractale = new MyImage(coté, coté);
            double Reel_Z;
            double Im_Z;
            double MinX = -2.1;
            double MaxX = 2.1;
            double MinY = -1.2;
            double MaxY = 1.2;
            double module;
            double tmp;
            bool fuyant;
            Random rnd = new Random();

            int R;
            int G;
            int B;
            int mult_pas_R = rnd.Next(1, 10);
            int mult_pas_G = rnd.Next(1, 10);
            int mult_pas_B = rnd.Next(1, 10);

            if (fractale_Random) { index_Fractale = rnd.Next(0, 15); }
            if (fractale_Personnalisé) {
                //15
                double[] fractale_perso = { reel_Personnalisé, im_Personnalisé, itérations_Personnalisé, mult_Couleurs_Personnalisé };
                Complexes_Julia.Add(fractale_perso);
                index_Fractale = 15;
            }

            int iterations = (int)Complexes_Julia[index_Fractale][2];
            int mult_couleurs = (int)Complexes_Julia[index_Fractale][3];
            Console.WriteLine(index_Fractale);
            Console.WriteLine(iterations);
            Console.WriteLine(mult_couleurs);


            for (int i = 0; i < coté; i++)
            {
                for (int j = 0; j < coté; j++)
                {
                    fuyant = false;
                    Reel_Z = MinX + ((Math.Abs(MinX) + MaxX) / coté) * i;
                    Im_Z = MinY + ((Math.Abs(MinY) + MaxY) / coté) * j;
                    R = mult_pas_R*20;
                    G = mult_pas_G*15;
                    B = mult_pas_B*10;
                    for (int k = 0; k < iterations; k++)
                    {
                        tmp = Reel_Z;
                        Reel_Z = (Reel_Z * Reel_Z) - (Im_Z * Im_Z) + Complexes_Julia[index_Fractale][0];
                        Im_Z = 2 * tmp * Im_Z + Complexes_Julia[index_Fractale][1];
                        module = Reel_Z * Reel_Z + Im_Z * Im_Z;
                        if (module > 4) { fractale.Image[i, j] = new Pixel(R, G, B); k = iterations; fuyant = true; }
                        R += mult_pas_R * (255 / iterations)* mult_couleurs;
                        G += mult_pas_G * (255 / iterations)* mult_couleurs;
                        B += mult_pas_B * (255 / iterations)*mult_couleurs;
                        if (R > 255) { R = 203; }
                        if (G > 255) { G = 122; }
                        if (B > 255) { B = 50; }
                    }
                    if (!fuyant)
                    {
                        fractale.Image[i, j] = new Pixel(0, 0, 0);
                    }

                }
            }
            return fractale;
        }
        public MyImage Histogramme()
        {
            int[] stockR = new int[256];
            int[] stockB = new int[256];
            int[] stockV = new int[256];

            for (int i = 0; i < this.height; i++)
            {
                for (int j = 0; j < this.width; j++)
                {
                    stockR[this.image[i, j].R]++;
                    stockB[this.image[i, j].B]++;
                    stockV[this.image[i, j].G]++;
                }
            }

            int max_pixel = 0;
            for (int index = 0; index < 256; index++)
            {
                if (stockR[index] >= max_pixel)
                {
                    max_pixel = stockR[index];

                    if (stockB[index] >= max_pixel)
                    {
                        max_pixel = stockB[index];

                        if (stockV[index] >= max_pixel)
                        {
                            max_pixel = stockV[index];
                        }
                    }

                    else if (stockV[index] >= max_pixel)
                    {
                        max_pixel = stockV[index];
                    }
                }

                else if (stockB[index] >= max_pixel)
                {
                    max_pixel = stockB[index];

                    if (stockV[index] >= max_pixel)
                    {
                        max_pixel = stockV[index];
                    }
                }

                else if (stockV[index] >= max_pixel)
                {
                    max_pixel = stockV[index];
                }
            }

            int multi = 3;
            MyImage histogramme = new MyImage(this, this.height * 10 + 1, 256 * multi + 1);
            for (int i = 0; i < histogramme.height; i++)
            {
                for (int j = 0; j < histogramme.width; j++)
                {
                    histogramme.image[i, j] = new Pixel(0, 0, 0);
                }
            }

            int k = 0;
            for (int j = 1; j < histogramme.width; j += multi)
            {

                for (int indexR = histogramme.height - 2; indexR >= histogramme.height - stockR[k]; indexR--)
                {
                    for (int l = 0; l < multi; l++)
                    {
                        histogramme.image[indexR, j + l] = new Pixel(255, 0, 0);
                    }
                }

                for (int indexB = histogramme.height - 2; indexB >= histogramme.height - stockB[k]; indexB--)
                {
                    for (int l = 0; l < multi; l++)
                    {
                        histogramme.image[indexB, j + l] = new Pixel(histogramme.image[indexB, j].R, 0, 255);
                    }
                }

                for (int indexV = histogramme.height - 2; indexV >= histogramme.height - stockV[k]; indexV--)
                {
                    for (int l = 0; l < multi; l++)
                    {
                        histogramme.image[indexV, j + l] = new Pixel(histogramme.image[indexV, j].R, 255, histogramme.image[indexV, j].B);
                    }
                }
                k++;
            }
            return histogramme;
        }

        #region QrCode

        public int Convertir_Char_En_Int(char c)
        {

            switch  (c)
            {
                case ((int)c >= 55):
                    return (int)c - 55;
                    break;

                case ' ':
                    return 36;
                    break;

                case '$':
                    return 37;
                    break;

                case '%':
                    return 38;
                    break;

                case '*':
                    return 39;
                    break;

                case '+':
                    return 40;
                    break;

                case '-':
                    return 41;
                    break;

                case '.':
                    return 42;
                    break;

                case '/':
                    return 43;
                    break;

                case ':':
                    return 44;
                    break;

                default:
                    return (int)c - 48;
                    break;
            }

            
        }

        

        public byte[] Convertir_Chaine_Char(string chaine)
        {
            bool[] chaine_finale = {false,false,true,false };
            int taille_Chaine = chaine.Length;
            chaine_finale = chaine_finale.Concat<bool>(this.Convertir_Int_to_Bool_Tab_9(taille_Chaine, 9)).ToArray();
            Dictionary<char, int> code_Charactères = new Dictionary<char, int>()
            {
                {' ', 36},
                {'$', 37},
                {'%', 38},
                {'*', 39},
                {'+', 40},
                {'-', 41},
                {'.', 42},
                {'/', 43},
                {':', 44},
            };



            for(int i = 0; i< chaine.Length; i += 2)
            { 
                int int_Couple_Char;
                bool[] binaire_Couple_Char;
                if (i + 1 > chaine.Length-1) 
                {
                    int_Couple_Char =  Convertir_Char_En_Int(chaine[i]);
                    binaire_Couple_Char = Convertir_Int_to_Bool_Tab_9(int_Couple_Char, 6);
                }
                else
                {
                    int_Couple_Char = 45 * Convertir_Char_En_Int(chaine[i]) + Convertir_Char_En_Int(chaine[i+1]);
                    binaire_Couple_Char = Convertir_Int_to_Bool_Tab_9(int_Couple_Char, 11);
                }

                
                chaine_finale = chaine_finale.Concat<bool>(binaire_Couple_Char).ToArray();
            }

            



            /*
                Plan
            
            Chaine stockée ds tableau d'octets



             */
        }

        #endregion
        #endregion TD 5 

    }
}
