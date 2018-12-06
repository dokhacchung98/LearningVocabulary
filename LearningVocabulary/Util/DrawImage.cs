using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using LearningVocabulary.Entity;
using static LearningVocabulary.Util.Wallpaper;

namespace LearningVocabulary.Util
{
    class DrawImage
    {
        private static string TAG = "DrawImage";
        public static void Draw(List<Vocabulary> list)
        {
            List<PointF> lPointFs = new List<PointF>();
            lPointFs.Add(new PointF(3160f, 150f));
            lPointFs.Add(new PointF(3160f, 235f));
            lPointFs.Add(new PointF(3160f, 320f));
            lPointFs.Add(new PointF(3160f, 405f));
            lPointFs.Add(new PointF(3160f, 490f));

            string imageFilePath = "./Image/image.png";
            string imageFilePathSave = "./Image/image2.png";
            Bitmap bitmap = Convert(imageFilePath);

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                using (Font arialFont = new Font("Arial", 14))
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        graphics.DrawString(list.ElementAt(i).vocabulary + "  -  " +
                                            list.ElementAt(i).spelling + "  -  " + list.ElementAt(i).means,
                            arialFont, Brushes.Black, lPointFs.ElementAt(i));
                    }
                }
            }

            try
            {
                bitmap.Save(imageFilePathSave);
            }
            catch (Exception e)
            {
                WriteLog.WriteLogException(TAG, "Draw()", e.Message);
            }
            bitmap.Dispose();
            bitmap = null;
            Wallpaper.Set(imageFilePathSave, Style.Centered);
        }

        private static Bitmap Convert(string fileName)
        {
            Bitmap bitmap;
            using (Stream bmpStream = File.Open(fileName, FileMode.Open))
            {
                Image image = Image.FromStream(bmpStream);

                bitmap = new Bitmap(image);
            }
            return bitmap;
        }
    }
}
