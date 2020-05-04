using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace ChinhDo.Functions
{
    /// <summary>
    /// Image processing functions
    /// </summary>
    public class ImageProcessor
    {
        /// <summary>Constructor</summary>
        /// <param name="useUnsafeCode">True to use unsafe (faster code)</param>
        public ImageProcessor() : this(false)
        {
        }


        /// <summary>Constructor</summary>
        /// <param name="useUnsafeCode">True to use unsafe (faster code)</param>
        public ImageProcessor(bool useUnsafeCode) {
            this.useUnsafeCode = useUnsafeCode;
        }

        /// <summary>Returns true if an image is blank.</summary>
        /// <param name="imageFileName">Image file name</param>
        public bool IsBlank(string imageFileName)
        {
            // TODO: Download more images and put in two folders... calculate stats
            double stdDev = useUnsafeCode ? StdDevUnsafe(imageFileName) : StdDev(imageFileName);

            Console.WriteLine(string.Format("img={0} stdDev={1}", imageFileName, stdDev));
            return stdDev < blankThreshold;
        }

        /// <summary>
        /// Get the standard deviation of pixel values.
        /// </summary>
        /// <param name="imageFileName">Name of the image file.</param>
        /// <returns>Standard deviation.</returns>
        public double StdDev(string imageFileName)
        {
            using (Bitmap bitmap1 = new Bitmap(imageFileName))
            {
                Bitmap bitmap2 = Resize(bitmap1);

                double total = 0, totalVariance = 0;
                double stdDev = 0;
                int count = 0;

                for (int x = 0; x < bitmap2.Width; x++)
                {
                    for (int y = 0; y < bitmap2.Height; y++)
                    {
                        count++;
                        Color c = bitmap2.GetPixel(x, y);
                        int pixelValue = c.R + c.G + c.B;

                        total += pixelValue;
                        double avg = total / count;
                        totalVariance += Math.Pow(pixelValue - avg, 2);
                        stdDev = Math.Sqrt(totalVariance / count);
                    }
                }

                return stdDev;
            }
        }

        /// <summary>
        /// Get the standard deviation of pixel values. Use unsafe code for faster performance.
        /// </summary>
        /// <param name="imageFileName">Name of the image file.</param>
        /// <returns>Standard deviation.</returns>
        public double StdDevUnsafe(string imageFileName)
        {
            double total = 0, totalVariance = 0;
            int count = 0;
            double stdDev = 0;            

            using (Bitmap bitmap1 = new Bitmap(imageFileName))
            {
                // TODO: resize
                // TODO: property to use unsafe
                // TODO: test this

                BitmapData bmData = bitmap1.LockBits(new Rectangle(0, 0, bitmap1.Width, bitmap1.Height), ImageLockMode.ReadOnly, bitmap1.PixelFormat);
                int stride = bmData.Stride;
                IntPtr Scan0 = bmData.Scan0;
                unsafe
                {
                    byte* p = (byte*)(void*)Scan0;
                    int nOffset = stride - bitmap1.Width * 3;
                    for (int y = 0; y < bitmap1.Height; y++)
                    {
                        for (int x = 0; x < bitmap1.Width; x++)
                        {
                            count++;

                            byte blue = p[0];
                            byte green = p[1];
                            byte red = p[2];

                            int pixelValue = red + green + blue;
                            total += pixelValue;
                            double avg = total / count;
                            totalVariance += Math.Pow(pixelValue - avg, 2);
                            stdDev = Math.Sqrt(totalVariance / count);

                            p += 3;
                        }
                        p += nOffset;
                    }
                }

                bitmap1.UnlockBits(bmData);
            }

            Console.WriteLine("img=" + imageFileName + " stdDev=" + stdDev);

            return stdDev;
        }

        private float blankThreshold = 30f;
        private bool useUnsafeCode = false;

        private Bitmap Resize(Bitmap bitmap)
        {
            // If image is larger than 100x100 pixels resize it for performance
            if (bitmap.Width > 100 || bitmap.Height > 100)
            {
                int width = 100;
                float ratio = (float)bitmap.Width / (float)bitmap.Height;
                int height = (int)(width / ratio);

                var destRect = new Rectangle(0, 0, width, height);
                Bitmap bitmap2 = new Bitmap(width, height);
                bitmap2.SetResolution(bitmap.HorizontalResolution, bitmap.VerticalResolution);

                using (var graphics = Graphics.FromImage(bitmap2))
                {
                    graphics.CompositingMode = CompositingMode.SourceCopy;
                    graphics.CompositingQuality = CompositingQuality.HighSpeed;
                    graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                    graphics.SmoothingMode = SmoothingMode.HighSpeed;
                    graphics.PixelOffsetMode = PixelOffsetMode.HighSpeed;

                    using (var wrapMode = new ImageAttributes())
                    {
                        wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                        graphics.DrawImage(bitmap, destRect, 0, 0, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel, wrapMode);
                    }
                }

                return bitmap2;
            }
            else {
                return bitmap;
            }
        }
    }
}
