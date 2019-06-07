namespace ImageMerger.Graphic
{
    public class Pixel
    {
        private readonly int x;
        private readonly int y;

        public Pixel(int X, int Y)
        {
            x = X;
            y = Y;
        }

        public Pixel(Pixel pixel)
        {
            x = pixel.X;
            y = pixel.Y;
        }

        public override string ToString()
        {
            return X + "," + Y;
        }

        public int X => x;
        public int Y => y;
    }
}
