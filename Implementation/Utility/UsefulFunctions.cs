using System.Collections;
using System.Drawing;
using System.Reflection.Metadata.Ecma335;
using Implementation.Utility.Interfaces;

namespace Implementation.Utility
{
    public class UsefulFunctions : IUsefulFunctions
    {
        public Color GetColorFromArray(byte[] array, int indexArray)
        {
            // Vérifier si l'index est en dehors des limites du tableau (y compris les index négatifs)
            if (indexArray < 0 || indexArray >= array.Length) return Color.Black;

            // Extraire les valeurs rouge, vert et bleu ou mettre 0 par défaut
            byte red = array[indexArray];
            byte green = indexArray + 1 < array.Length ? array[indexArray + 1] : (byte)0;
            byte blue = indexArray + 2 < array.Length ? array[indexArray + 2] : (byte)0;

            return Color.FromArgb(red, green, blue);
        }

        public byte[] GetByteFromColor(byte[] array, int indexArray, Color squareColor)
        {
            // Vérifier si l'index est en dehors des limites du tableau
            if (indexArray >= array.Length)
                return array;

            // Assigner la valeur rouge (toujours dans les limites car déjà vérifié)
            array[indexArray] = squareColor.R;

            // Assigner les valeurs verte et bleue si les index sont dans les limites
            if (indexArray + 1 < array.Length)
                array[indexArray + 1] = squareColor.G;

            if (indexArray + 2 < array.Length)
                array[indexArray + 2] = squareColor.B;

            return array;
        }
    }
}
