using Implementation.Wrapper.Interfaces;
using System.Drawing;


namespace Implementation.Wrapper
{
    public class GraphicsWrapper : IGraphics
    {
        private readonly Graphics _graphics;

        public GraphicsWrapper(Graphics graphics)
        {
            _graphics = graphics;
        }

        public void FillRectangle(SolidBrush brush, Rectangle rect)
        {
            _graphics.FillRectangle(brush, rect);
        }

    }
}
