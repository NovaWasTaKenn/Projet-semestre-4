using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;
using System.IO;

namespace TestUnitairesProjetS4
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Convertir_Int_En_Tab_De_Byte_Test()
        {
            byte[] bytes = TD_1.MyImage.Convertir_Int_En_Tab_De_Byte(779, 11,0,2);
            Assert.AreEqual(97, bytes[0]);
            Assert.AreEqual(96, bytes[1]);

            byte[] bytes2 = TD_1.MyImage.Convertir_Int_En_Tab_De_Byte(13, 6, 0, 1);
            Assert.AreEqual(52, bytes2[0]);
        }

        [TestMethod]
        public void Convertir_Chaine_Char_Test()
        {
            byte[] bytes = TD_1.MyImage.Convertir_Chaine_Char("HELLO WORLD", 1);
            byte[] expected = { 32, 91, 11, 120, 209, 114, 220, 77, 67, 64, 236, 17, 236, 17, 236, 17, 236, 17,236 };
            Assert.AreEqual(expected.Length, bytes.Length);

            for(int i = 0; i< expected.Length; i++)
            {
                Assert.AreEqual(expected[i], bytes[i]);
            }
        }

        [TestMethod]
        public void Convertir_Int_to_Endian_Test()
        {
            TD_1.MyImage image = new TD_1.MyImage(40, 40);
            byte[ ] bytes = image.Convertir_Int_to_Endian(45);
            Assert.AreEqual(45, bytes[0]);

            byte[] bytes2 = image.Convertir_Int_to_Endian(1405);
            Assert.AreEqual(125, bytes2[0]);
            Assert.AreEqual(5, bytes2[1]);
        }
        

        [TestMethod]
        public void ConvertirBinaire_To_Int()
        {
            int[] binaire = { 1, 0, 1, 1, 1, 0, 1, 0 };
            int result = TD_1.MyImage.ConvertirBinaire_To_Int(binaire);
            int expected = 186;
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ConvertirInt_To_Binaire()
        {
            int entier = 128;
            int[] result = TD_1.MyImage.ConvertirInt_To_Binaire(entier);
            int[] expected = { 1, 0, 0, 0, 0, 0, 0, 0 };

            for (int i = 0; i < result.Length; i++)
            {
                Assert.AreEqual(expected[i], result[i]);
            }
        }

        [TestMethod]
        public void ConvertirChar_To_Char()
        {
            char caractere = 'A';
            int entier_du_caractère = TD_1.MyImage.Convertir_Char_En_Int(caractere);
            int expected = 10;
            Assert.AreEqual(expected, entier_du_caractère);
        }
    }
}
