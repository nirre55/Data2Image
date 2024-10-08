﻿
namespace Implementation.Entities
{
    public class EJsonFile
    {
        public int NombreBytes { get; set; }
        public string Extension { get; set; } = string.Empty;
        public bool isBlackAndWhiteImage { get; set; }
        public int NombreDePixel { get; set; }
        public int NombreDePixelOrphelin { get; set; }
        public int NombreImages { get; set; }
        public void CalculeNombrePixel()
        {
            if (isBlackAndWhiteImage)
            {
                NombreDePixel = NombreBytes;
                NombreDePixelOrphelin = 0;
            }
            else
            { //Dans le cas ou l'image choisie est en couleur on devise par 3 car elle dois comporté les 3 valeur RGB
                NombreDePixel = NombreBytes / 3;
                NombreDePixelOrphelin = NombreBytes % 3;
            }
        }

        public void SetImageNumber(int width, int height) 
        {
            if (NombreDePixel > width * height)
                NombreImages = NombreDePixel % (width * height);
            NombreImages = 1;
        }
    }
}
