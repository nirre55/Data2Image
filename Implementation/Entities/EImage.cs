using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Entities
{
    public class EImage
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int squareSize { get; set; }

        public bool isCorrectImageSize
        {
            get
            {
                return (Width % squareSize == 0 && Height % squareSize == 0);
            }
        }
    }
}