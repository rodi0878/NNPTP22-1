using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNPTPZ1 {
    class Image {
        public int width { get; }
        public int height { get; }
        public Bitmap bitmap { get; }

        private string filename;

        private readonly static Color[] colors = new Color[]
           {
                 Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
            };

        public Image(int height, int width, string filename) {
            this.filename = filename;
            this.height = height;
            this.width = width;
            this.bitmap = new Bitmap(width, height);
        }
        public void saveImage() {
            bitmap.Save(filename ?? "../../../out.png");
        }
        public void ColorizePixel(int indexOfRoot, int xCoordinateOfPixel, int yCoordinateOfPixel) {
            var color = colors[indexOfRoot % colors.Length];
            bitmap.SetPixel(xCoordinateOfPixel, yCoordinateOfPixel, color);
        }
    }
}
