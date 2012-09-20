using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;

namespace BuyTicketFor12306
{
    public enum AlgorithmsType
    {
        MaxValue, //最大值法
        AverageValue, //平均值法
        WeightAverage //加权平均值法
    }

    public class ImageHelper
    {
        /// <summary>
        /// 图片转换灰度
        /// </summary>
        /// <param name="img"></param>
        /// <param name="algo"></param>
        /// <returns></returns>
        public static Image ToGray(Image img, AlgorithmsType algo)
        {
            int width = img.Width;
            int height = img.Height;

            Bitmap bmp = new Bitmap(img);

            //设定实例BitmapData相关信息
            Rectangle rect = new Rectangle(0, 0, width, height);
            ImageLockMode mode = ImageLockMode.ReadWrite;
            PixelFormat format = PixelFormat.Format32bppArgb;

            //锁定bmp到系统内存中
            BitmapData data = bmp.LockBits(rect, mode, format);

            //获取位图中第一个像素数据的地址
            IntPtr ptr = data.Scan0;

            int numBytes = width * height * 4;
            byte[] rgbValues = new byte[numBytes];

            //将bmp数据Copy到申明的数组中
            Marshal.Copy(ptr, rgbValues, 0, numBytes);

            for (int i = 0; i < rgbValues.Length; i += 4)
            {
                int value = 0;
                switch (algo)
                {
                    //最大值法
                    case AlgorithmsType.MaxValue:
                        value = rgbValues[i] > rgbValues[i + 1] ? rgbValues[i] : rgbValues[i + 1];
                        value = value > rgbValues[i + 2] ? value : rgbValues[i + 2];
                        break;
                    //平均值法
                    case AlgorithmsType.AverageValue:
                        value = (int)((rgbValues[i] + rgbValues[i + 1] + rgbValues[i + 2]) / 3);
                        break;
                    //加权平均值法
                    case AlgorithmsType.WeightAverage:
                        value = (int)(rgbValues[i] * 0.1 + rgbValues[i + 1] * 0.2
                        + rgbValues[i + 2] * 0.7);
                        break;
                }

                //将数组中存放R、G、B的值修改为计算后的值
                for (int j = 0; j < 3; j++)
                {
                    rgbValues[i + j] = (byte)value;
                }
            }

            //将数据Copy到内存指针
            Marshal.Copy(rgbValues, 0, ptr, numBytes);

            //从系统内存解锁bmp
            bmp.UnlockBits(data);

            return (Image)bmp;

        }

        public static void ClearAnotherGrayAndSave(Image image)
        {
            Bitmap bitmap = new Bitmap(image);
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    if (bitmap.GetPixel(i, j).ToArgb() > 50)
                    {
                        bitmap.SetPixel(i, j, Color.White);
                    }
                }
            }
            bitmap.Save("temp.jpg");
        }

        public static Bitmap PBinary(Bitmap src, int v)
        {
            int w = src.Width;
            int h = src.Height;
            Bitmap dstBitmap = new Bitmap(src.Width, src.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            System.Drawing.Imaging.BitmapData srcData = src.LockBits(new Rectangle(0, 0, w, h), System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            System.Drawing.Imaging.BitmapData dstData = dstBitmap.LockBits(new Rectangle(0, 0, w, h), System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            unsafe
            {
                byte* pIn = (byte*)srcData.Scan0.ToPointer();
                byte* pOut = (byte*)dstData.Scan0.ToPointer();
                byte* p;
                int stride = srcData.Stride;
                int r, g, b;
                for (int y = 0; y < h; y++)
                {
                    for (int x = 0; x < w; x++)
                    {
                        p = pIn;
                        r = p[2];
                        g = p[1];
                        b = p[0];
                        pOut[0] = pOut[1] = pOut[2] = (byte)(((byte)(0.2125 * r + 0.7154 * g + 0.0721 * b) >= v)
                        ? 255 : 0);
                        pIn += 3;
                        pOut += 3;
                    }
                    pIn += srcData.Stride - w * 3;
                    pOut += srcData.Stride - w * 3;
                }
                src.UnlockBits(srcData);
                dstBitmap.UnlockBits(dstData);
                return dstBitmap;
            }
        }
    }
}
