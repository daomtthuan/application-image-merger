using System;
using System.Drawing;
using ImageMerger.Graphic;

namespace ImageMerger
{
    class Program
    {
        static void Main()
        {
            RightMerger rightMerger = new RightMerger(@"..\..\..\Asset\1.png", @"..\..\..\Asset\2.png");
            rightMerger.Merge();
        }
    }
}
