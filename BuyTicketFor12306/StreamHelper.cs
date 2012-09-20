using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace BuyTicketFor12306
{
    public class StreamHelper
    {
        private static readonly string FILEDIC = "TempHTMLs";
        private static readonly string FILEEXT = "html";

        public static Image ConvertStreamToImage(Stream stream)
        {
            Image image = Image.FromStream(stream);
            stream.Dispose();
            return image;
        }

        public static string ConvertStreamToString(Stream stream)
        {
            string result = string.Empty;
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                result = reader.ReadToEnd();
                stream.Dispose();
            }
            return result;
        }

        public static string ConvertStreamToHTMLFile(Stream stream)
        {
            string fileName = string.Format("{0}/{1}.{2}", FILEDIC, DateTime.Now.Ticks, FILEEXT);

            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            {
                StreamWriter writer = new StreamWriter(fs, Encoding.UTF8);
                writer.Write(ConvertStreamToString(stream));
                writer.Flush();
                writer.Close();
                fs.Close();
            }

            return System.AppDomain.CurrentDomain.BaseDirectory + fileName;
        }

        public static void DeleteAllTempFiles()
        {
            string[] tempFiles = Directory.GetFiles(FILEDIC);

            foreach (var item in tempFiles)
            {
                File.Delete(item);
            }
        }
    }
}
