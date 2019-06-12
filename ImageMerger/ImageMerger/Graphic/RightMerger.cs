using System.Drawing;

namespace ImageMerger.Graphic
{
    public class RightMerger
    {
        #region Asset
        private readonly Bitmap bitmap1;
        private readonly Bitmap bitmap2;
        #endregion

        #region Constructor
        /// <summary>
        ///     <para>RightMerger's Constructor function.</para>
        ///     <para>Creating RightMerger to merge two Images with thier filename path.</para>
        /// </summary>
        /// <param name="filenameImage1">Left Image's path</param>
        /// <param name="filenameImage2">Right Image's path</param>
        public RightMerger(string filenameImage1, string filenameImage2)
        {
            bitmap1 = new Bitmap(filenameImage1);
            bitmap2 = new Bitmap(filenameImage2);
        }
        #endregion

        #region Method
        /// <summary>
        ///     <para>Comparing two Colors.</para>
        ///     <para>Return true if these Colors are same. Otherwise, false</para>
        /// </summary>
        /// <param name="color1">The first Color</param>
        /// <param name="color2">The second Color</param>
        /// <returns>bool type: True or false</returns>
        private bool Compare(Color color1, Color color2)
        {
            if (color1.R == color2.R)
                if (color1.G == color2.G)
                    if (color1.B == color2.B)
                        return true;
            return false;
        }

        /// <summary>
        ///     <para>
        ///         Get Match between two Bitmaps at the column which is determined by X-axis.
        ///         The second Bitmap will be moved for seaching
        ///     </para>
        ///     <para>Return the Pixel array with four elements.</para>
        ///     <para>
        ///         The first Bitmap's matched area is determined by (Pixel[0], Pixel[1]).
        ///         The second Bitmap's matched area is determined by (Pixel[2], Pixel[3]).
        ///     </para>
        /// </summary>
        /// <param name="bitmap1">The first Bitmap</param>
        /// <param name="X1">The first Bitmap's X-axis</param>
        /// <param name="bitmap2">The second Bitmap</param>
        /// <param name="X2">The second Bitmap's X-axis</param>
        /// <returns>Return the Pixel array with four elements.</returns>
        private Pixel[] GetMatchedAreaAtColumnMoveBitmap(Bitmap bitmap1, int X1, Bitmap bitmap2, int X2)
        {
            int Y2 = 0,
                y1 = 0,
                y2 = Y2;
            while (Y2 < bitmap2.Height)
            {
                while (y2 < bitmap2.Height && Compare(bitmap2.GetPixel(X2, y2), (bitmap1.GetPixel(X1, y1))))
                {
                    y1++;
                    y2++;
                }
                if (y2 == bitmap2.Height)
                    return new Pixel[] { new Pixel(X1, 0), new Pixel(X1, y1 - 1), new Pixel(X2, Y2), new Pixel(X2, y2 - 1) };
                y1 = 0;
                y2 = ++Y2;
            }
            return null;
        }

        /// <summary>
        ///     <para>
        ///         Get Match between two Bitmaps at the column which is determined by x-axis.
        ///     </para>
        ///     <para>Return the Pixel array with four elements.</para>
        ///     <para>
        ///         The first Bitmap's matched area is determined by (Pixel[0], Pixel[1]).
        ///         The second Bitmap's matched area is determined by (Pixel[2], Pixel[3]).
        ///     </para>
        /// </summary>
        /// <param name="X1">The first Bitmap's X-axis</param>
        /// <param name="X2">The second Bitmap's X-axis</param>
        /// <returns>Return the Pixel array with four elements.</returns>
        private Pixel[] GetMatchedAreaAtColumn(int X1, int X2)
        {
            Pixel[] matchArea = GetMatchedAreaAtColumnMoveBitmap(bitmap1, X1, bitmap2, X2);
            if (matchArea != null) return matchArea;
            else
            {
                matchArea = GetMatchedAreaAtColumnMoveBitmap(bitmap2, X2, bitmap1, X1);
                if (matchArea != null)
                    return new Pixel[] { matchArea[2], matchArea[3], matchArea[0], matchArea[1] };
                return null;
            }
        }

        /// <summary>
        ///     <para>
        ///         Get Match between two Bitmaps.
        ///     </para>
        ///     <para>Return the Pixel array with four elements.</para>
        ///     <para>
        ///         The first Bitmap's matched area is determined by from (Pixel[0], Pixel[1]) to the end of it.
        ///         The second Bitmap's matched area is determined by (Pixel[2], Pixel[3]) to the end of it.
        ///     </para>
        /// </summary>
        /// <returns>Return the Pixel array with four elements.</returns>
        private Pixel[] GetMathedArea()
        {
            int X1 = 0,
                x1 = X1,
                x2 = 0;
            Pixel[] temp = null;
            while (X1 < bitmap1.Width)
            {
                while (x1 < bitmap1.Width && (temp = GetMatchedAreaAtColumn(x1, x2)) != null)
                {
                    x1++;
                    x2++;
                }
                if (x1 == bitmap1.Width)
                    return new Pixel[] { new Pixel(X1, temp[0].Y), new Pixel(X1, temp[1].Y), new Pixel(x2, temp[2].Y), new Pixel(x2, temp[3].Y) };
                x1 = ++X1;
                x2 = 0;
            }
            return null;
        }

        public void Merge()
        {
            Pixel[] mathedArea = GetMathedArea();
            Bitmap bitmap = new Bitmap(mathedArea[0].X + bitmap2.Width, mathedArea[2].Y + mathedArea[0].Y + (mathedArea[0].Y == 0 ? bitmap1.Height : bitmap2.Height));

            int x, y;
            for (x = 0; x < bitmap1.Width; x++)
            {
                for (y = 0; y < bitmap1.Height; y++)
                    bitmap.SetPixel(x, y + mathedArea[2].Y, bitmap1.GetPixel(x, y));

                for (y = 0; y < bitmap2.Height; y++)
                    bitmap.SetPixel(x + mathedArea[0].X, y + mathedArea[0].Y, bitmap2.GetPixel(x, y));
            }

            bitmap.Save(@"E:\Github\Project-X180519\ImageMerger\result.png");
            System.Diagnostics.Process.Start(@"E:\Github\Project-X180519\ImageMerger\result.png");
        }
        #endregion  
    }
}
