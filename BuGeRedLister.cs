using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

using System.IO;
using System.Runtime.InteropServices;

namespace bserver
{


    public record BuGeRed
    {

        public byte Blue;
        public byte Green;
        public byte Red;
        public byte Alpha;


        public BuGeRed(int v, byte[] colorsWithAlpha)
        {
            Blue = colorsWithAlpha[0];
            Green = colorsWithAlpha[1];
            Red = colorsWithAlpha[2];
            Alpha = colorsWithAlpha[3];
        }


        public BuGeRed(Color c)
        {
            Blue = c.B;
            Green = c.G;
            Red = c.R;
            Alpha = c.A;
        }


        public BuGeRed(BuGeRed copyMe)
        {

            this.Blue = copyMe.Blue; this.Green = copyMe.Green;
            this.Red = copyMe.Red; this.Alpha = copyMe.Alpha;
        }


        public byte[] GetBytes()
        {
            return new byte[] { this.Blue, this.Green, this.Red, this.Alpha };
        }

        public override int GetHashCode()
        {
            //TODO: Change to b+g+r+a  - what happens if uint is outside int - may happen and as this is a hash:
            // Might have odd behaviour as identical things are not regognised as such.
            if (BitConverter.IsLittleEndian)
                Array.Reverse(this.GetBytes());

            return BitConverter.ToInt32(this.GetBytes(), 0);

            //return (int)this.Blue;
        }


        public BuGeRed(byte[] barr)
        {


            this.Blue = barr[0];
            Green = barr[1];
            Red = barr[2];
            Alpha = barr[3];

        }

    }

    public static class BuGeRedEx
    {
        public static void DoAction(this List<BuGeRed> l, Action<BuGeRed> a)
        {
            l.ForEach(a);

        }
    }

    public class BuGeRedLister
    {



        public List<BuGeRed> GetBuGeRedListFromBitmap(Bitmap sourceImage)
        {

            BitmapData sourceData = sourceImage.LockBits(new Rectangle(0, 0,
                        sourceImage.Width, sourceImage.Height),
                        ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);


            byte[] sourceBuffer = new byte[sourceData.Stride * sourceData.Height];
            //TODO: what to do with cloud and Linux..
            Marshal.Copy(sourceData.Scan0, sourceBuffer, 0, sourceBuffer.Length);
            sourceImage.UnlockBits(sourceData);

            List<BuGeRed> pixelList = new List<BuGeRed>(sourceBuffer.Length / 4);

            using (MemoryStream memoryStream = new MemoryStream(sourceBuffer))
            {
                memoryStream.Position = 0;
                BinaryReader binaryReader = new BinaryReader(memoryStream);

                while (memoryStream.Position + 4 <= memoryStream.Length)
                {
                    BuGeRed pixel = new BuGeRed(binaryReader.ReadBytes(4));
                    pixelList.Add(pixel);
                }
                binaryReader.Close();
            }
            return pixelList;
        }


        public byte[] GetBytesFromBitmap(Bitmap sourceImage)
        {

            BitmapData sourceData = sourceImage.LockBits(new Rectangle(0, 0,
                        sourceImage.Width, sourceImage.Height),
                        ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);


            byte[] sourceBuffer = new byte[sourceData.Stride * sourceData.Height];
            //TODO: what to do with cloud and Linux..
            Marshal.Copy(sourceData.Scan0, sourceBuffer, 0, sourceBuffer.Length);
            sourceImage.UnlockBits(sourceData);

            return sourceBuffer;

        }


        public byte[] GetBytesFromBuGeRedList(List<BuGeRed> pixelList, int width, int height)
        {
            Bitmap resultBitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            BitmapData resultData = resultBitmap.LockBits(new Rectangle(0, 0,
                        resultBitmap.Width, resultBitmap.Height),
                        ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            byte[] resultBuffer = new byte[resultData.Stride * resultData.Height];


            Marshal.Copy(resultData.Scan0, resultBuffer, 0, resultBuffer.Length);

            //Marshal.Copy(resultBuffer, 0, resultData.Scan0, resultBuffer.Length);
            //resultBitmap.UnlockBits(resultData);

            return resultBuffer;
        }


        public Bitmap GetBitmapFromBuGeRedList(List<BuGeRed> pixelList, int width, int height)
        {
            Bitmap resultBitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            BitmapData resultData = resultBitmap.LockBits(new Rectangle(0, 0,
                        resultBitmap.Width, resultBitmap.Height),
                        ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            byte[] resultBuffer = new byte[resultData.Stride * resultData.Height];

            using (MemoryStream memoryStream = new MemoryStream(resultBuffer))
            {
                memoryStream.Position = 0;
                BinaryWriter binaryWriter = new BinaryWriter(memoryStream);

                foreach (BuGeRed pixel in pixelList)
                {
                    binaryWriter.Write(pixel.GetBytes());
                }

                binaryWriter.Close();
            }

            Marshal.Copy(resultBuffer, 0, resultData.Scan0, resultBuffer.Length);
            resultBitmap.UnlockBits(resultData);

            return resultBitmap;
        }





    }
}
