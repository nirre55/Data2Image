
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