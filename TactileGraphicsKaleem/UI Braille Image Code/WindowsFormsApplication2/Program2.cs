/* Code Written by Mohammed Kaleemur Rahman and Saurabh Sanghvi modified 31/07/08 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace BrailleImaging {
    
    class Program {

       public static void Braille() {

            string inputFile = "C:\\Documents and Settings\\t-sausan\\My Documents\\My Pictures\\";
            Console.WriteLine("Please enter input image filename:");
            Bitmap bmp = new Bitmap(2, 2);
            OpenFileDialog Open = new OpenFileDialog(); 
           Open.InitialDirectory = "c:\\";
            Open.Filter = "Bitmap files (*.bmp)|*.bmp|Jpeg files (*.jpg)|*.jpg";
            Open.FilterIndex = 2;
            Open.RestoreDirectory = true;
         
            if (DialogResult.OK == Open.ShowDialog())
            {
               
                bmp = (Bitmap)Bitmap.FromFile(Open.FileName, false);
               // bmp.AutoScroll = true;
              //  bmp.AutoScrollMinSize = new Size((bmp.Width), (bmp.Height));
                
            }
           // Bitmap bmp = new Bitmap(inputFile);
           
            bmp = scaleDown(bmp, getScaleFactor(bmp));
            Console.WriteLine("{0} x {1}", bmp.Width, bmp.Height);

            Console.WriteLine(">>Scaling complete");
            //toGrayscale(bmp, "grayscale.bmp");
            float threshold = getThreshold(bmp);
            Console.WriteLine("Threshold: {0}", threshold);

            string outputFile = "output.txt";
            //Console.WriteLine("Please enter output text filename:\n");
            //outputFile += Console.ReadLine();

            Console.WriteLine(">>Reading Image");
            buildBinImage(bmp, threshold, "newImage.bmp");
            
            Console.WriteLine();

            Console.WriteLine(">>Building Table");
            int[, , ,] binArray = buildBinTable(bmp, threshold);

            Console.WriteLine();

            //build cell array and perform tests on it
            Console.WriteLine(">>Testing buildCellTable()");
            Cell[,] cellArray = buildCellTable(bmp, threshold);

            //test buildHashtable()
            Hashtable charTable = buildHashtable();
            writeOutput(cellArray, charTable, outputFile);
        }


        /* create a new binary image of specified filename from specified file */
        public static void buildBinImage(Bitmap image, float threshold, string fileName){
            Bitmap newImage = new Bitmap(image.Width, image.Height);
            Color color = new Color();

            for (int y = 0; y < image.Height; y++){
                for (int x = 0; x < image.Width; x++){
                    color = image.GetPixel(x, y);
                    if ( color.GetBrightness() > threshold ){
                        newImage.SetPixel(x, y, Color.White);
                    }
                    else{
                        newImage.SetPixel(x, y, Color.Black);
                    }
                }
            }
            newImage.Save(fileName, ImageFormat.Bmp);
        }


        /* build 4-dimensional binary array from image */
        public static int[, , ,] buildBinTable(Bitmap image, float threshold){
            int[, , ,] binArr = new int[image.Height / 3, image.Width / 2, 3, 2];
            Color color = new Color();

            for (int majr = 0; majr < binArr.GetLength(0); majr++){
                for (int majc = 0; majc < binArr.GetLength(1); majc++){
                    for (int minr = 0; minr < 3; minr++){
                        for (int minc = 0; minc < 2; minc++){
                            color = image.GetPixel((2 * majc) + minc, (3 * majr) + minr);
                            if (color.GetBrightness() > threshold){
                                binArr[majr, majc, minr, minc] = 1;
                            }
                            else{
                                binArr[majr, majc, minr, minc] = 0;
                            }
                        }
                    }
                }
            }
            return binArr;

        }


        /* print binary table */
        public static void printTable(int[, , ,] arr, Bitmap image){
            for (int majr = 0; majr < arr.GetLength(0); majr++){
                for (int minr = 0; minr < arr.GetLength(2); minr++){
                    for (int majc = 0; majc < arr.GetLength(1); majc++){
                        for (int minc = 0; minc < arr.GetLength(3); minc++){
                            Console.Write(arr[majr, majc, minr, minc]);
                        }
                    }
                    Console.WriteLine();
                }
            }
        }


        /* build cell table */
        public static Cell[,] buildCellTable(Bitmap image, float threshold){
            Cell[,] cellArr = new Cell[image.Width/2, image.Height/3];
            Color color = new Color();

            for (int majr = 0; majr < cellArr.GetLength(1); majr++){
                for (int majc = 0; majc < cellArr.GetLength(0); majc++){
                    cellArr[majc, majr] = new Cell();
                    for (int minr = 0; minr < 3; minr++){
                        for (int minc = 0; minc < 2; minc++){
                            color = image.GetPixel((2 * majc) + minc, (3 * majr) + minr);
                            if (color.GetBrightness() <= threshold){
                                cellArr[majc, majr].addDot((3 * minc) + minr + 1);
                            }
                        }
                    }
                }
            }
            return cellArr;
        }


        /* build characters of hashtable */
        public static Hashtable buildHashtable(){
            Hashtable map = new Hashtable();

            map.Add("0", " ");
            map.Add("1", "a");
            map.Add("2", "1");
            map.Add("3", "'");
            map.Add("4", "@");
            map.Add("5", "\\");
            map.Add("6", ",");
            map.Add("12", "b");
            map.Add("13", "k");
            map.Add("14", "c");
            map.Add("15", "e");
            map.Add("16", "*");
            map.Add("23", "2");
            map.Add("24", "i");
            map.Add("25", "3");
            map.Add("26", "5");
            map.Add("34", "/");
            map.Add("35", "9");
            map.Add("36", "-");
            map.Add("45", "^");
            map.Add("46", ".");
            map.Add("56", ";");
            map.Add("123", "l");
            map.Add("124", "f");
            map.Add("125", "h");
            map.Add("126", "<");
            map.Add("134", "m");
            map.Add("135", "o");
            map.Add("136", "u");
            map.Add("145", "d");
            map.Add("146", "%");
            map.Add("156", ":");
            map.Add("234", "s");
            map.Add("235", "6");
            map.Add("236", "8");
            map.Add("245", "j");
            map.Add("246", "[");
            map.Add("256", "4");
            map.Add("345", ">");
            map.Add("346", "+");
            map.Add("356", "0");
            map.Add("456", "_");
            map.Add("1234", "p");
            map.Add("1235", "r");
            map.Add("1236", "v");
            map.Add("1245", "g");
            map.Add("1246", "$");
            map.Add("1256", "\\\\");
            map.Add("1345", "n");
            map.Add("1346", "x");
            map.Add("1356", "z");
            map.Add("1456", "?");
            map.Add("2345", "t");
            map.Add("2346", "!");
            map.Add("2356", "7");
            map.Add("2456", "w");
            map.Add("3456", "#");
            map.Add("12345", "q");
            map.Add("12346", "&");
            map.Add("12356", "(");
            map.Add("12456", "]");
            map.Add("13456", "y");
            map.Add("23456", ")");
            map.Add("123456", "=");

            return map;
        }


        /* write output file */
        public static void writeOutput(Cell[,] cellArr, Hashtable map, string fileName){
            TextWriter text = new StreamWriter(fileName);

            for (int y = 0; y < cellArr.GetLength(1); y++){
                for (int x = 0; x < cellArr.GetLength(0); x++){
                    text.Write(map[cellArr[x, y].getKey()]);
                }
                text.WriteLine();
            }

            text.Close();
        }


        /* return scaled down image from specified image */
        public static Bitmap scaleDown(Bitmap image, double scaleFactor){
            Bitmap newImage = new Bitmap((int)(image.Width * scaleFactor), (int)(image.Height * scaleFactor));
            Graphics g = Graphics.FromImage(newImage);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(image, 
                new Rectangle(0, 0, (int)(image.Width * scaleFactor), (int)(image.Height * scaleFactor)), 
                0, 0, image.Width, image.Height, GraphicsUnit.Pixel);
            newImage.Save("scaled.bmp", ImageFormat.Bmp);
            return newImage;
        }


        /* convert given image to grayscale */
        public static Bitmap toGrayscale(Bitmap image, String filename)        {
            Bitmap newImage = new Bitmap(image.Width, image.Height);
            Color color = new Color();
            int lum = 0;

            for (int y = 0; y < image.Height; y++){
                for (int x = 0; x < image.Width; x++){
                    color = image.GetPixel(x, y);
                    lum = (int)((0.2125*color.R) + (0.7154*color.G) + (0.0721*color.B));
                    newImage.SetPixel(x, y, Color.FromArgb(lum, lum, lum));
                }
            }
            newImage.Save(filename, ImageFormat.Bmp);
            return newImage;
        }


        /* get scale factor for an 84x84 image */
        public static double getScaleFactor(Bitmap image){
            return (84 / (double)Math.Max(image.Width, image.Height));
        }



        public static float getThreshold(Bitmap image){
            float tInit = 0.5f;
            float tFinal = 0.0f;
            float set1;
            float set2;
            int pix1;
            int pix2;

            Color color = new Color();
            float brightness;

            while (tInit != tFinal){
                set1 = 0.0f;
                set2 = 0.0f;
                pix1 = 0;
                pix2 = 0;
                Console.WriteLine("{0} {1}", tInit, tFinal);
                for (int y = 0; y < image.Height; y++){
                    for (int x = 0; x < image.Width; x++){
                        color = image.GetPixel(x, y);
                        brightness = color.GetBrightness();
                        if (brightness > tInit){
                            set1 += brightness;
                            pix1++;
                        }
                        else {
                            set2 += brightness;
                            pix2++;
                        }
                    }
                }

                set1 = set1 / pix1;
                set2 = set2 / pix2;
                tFinal = tInit;
                tInit = (set1 + set2) / 2;
            }

            return tFinal;
        }


        //following are methods not currently used in code that were additional methods used previously
        public static Color AvgColor(Bitmap image, int resizeFactor, int startx, int starty)
        {
            int sumG = 0;
            int sumR = 0;
            int sumB = 0;
            for (int y = starty; y < starty + resizeFactor; y++)
            {
                for (int x = startx; x < startx + resizeFactor; x++)
                {
                    Color Temp = image.GetPixel(x, y);
                    sumG += Temp.G;
                    sumR += Temp.R;
                    sumB += Temp.B;

                }
            }
            int average = resizeFactor * resizeFactor;
            sumG = sumG / average;
            sumB = sumB / average;
            sumR = sumR / average;
            Color AvgC = Color.FromArgb(sumR, sumG, sumB);
            return AvgC;

        }
        public static void buildBinImage(Bitmap image, string fileName)
        {
            Bitmap newImage = new Bitmap(image.Width + 1, image.Height + 1);
            Color color = new Color();

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    color = image.GetPixel(x, y);
                    if (color.GetBrightness() < 0.55)
                    {
                        newImage.SetPixel(x, y, Color.Black);
                    }
                    else
                    {
                        newImage.SetPixel(x, y, Color.White);
                    }
                }
            }

            newImage.Save(fileName);
        }


        /* build 4-dimensional binary array from image */
        public static int[, , ,] buildBinTable(Bitmap image)
        {
            int[, , ,] binArr = new int[image.Height / 3, image.Width / 2, 3, 2];
            Color color = new Color();

            for (int majr = 0; majr < binArr.GetLength(0); majr++)
            {
                for (int majc = 0; majc < binArr.GetLength(1); majc++)
                {
                    for (int minr = 0; minr < 3; minr++)
                    {
                        for (int minc = 0; minc < 2; minc++)
                        {
                            color = image.GetPixel((2 * majc) + minc, (3 * majr) + minr);
                            if (color.GetBrightness() < 0.55)
                            {

                                binArr[majr, majc, minr, minc] = 0;
                            }
                            else
                            {
                                binArr[majr, majc, minr, minc] = 1;
                            }
                        }
                    }
                }
            }
            return binArr;

        }


        /* print binary table */
        


        /* build cell table */
        public static Cell[,] buildCellTable(Bitmap image)
        {
            Cell[,] cellArr = new Cell[image.Width / 2, image.Height / 3];
            Color color = new Color();

            for (int majr = 0; majr < cellArr.GetLength(1); majr++)
            {
                for (int majc = 0; majc < cellArr.GetLength(0); majc++)
                {
                    cellArr[majc, majr] = new Cell();
                    for (int minr = 0; minr < 3; minr++)
                    {
                        for (int minc = 0; minc < 2; minc++)
                        {
                            color = image.GetPixel((2 * majc) + minc, (3 * majr) + minr);
                            if (color.GetBrightness() < 0.55)
                            {
                                cellArr[majc, majr].addDot((3 * minc) + minr + 1);
                            }
                        }
                    }
                }
            }
            return cellArr;
        }
        //different resize
        public static Cell[,] resizeCell(Cell[,] Big)
        {

            int width = Big.GetLength(0) / 2;
            int height = Big.GetLength(1) / 2;
            int smallw = 0;
            int smallh = 0;
            Cell[,] small = new Cell[width, height];
            for (int majr = 0; majr < Big.GetLength(1); majr += 2)
            {
                if (majr != 0)
                {
                    smallh++;
                }
                smallw = 0;
                for (int majc = 0; majc < Big.GetLength(0); majc += 2)
                {
                    if (smallw < small.GetLength(0) && smallh < small.GetLength(1))
                    {
                        small[smallw, smallh] = new Cell();
                        int count = 0;
                        int curw = majc;
                        int curl = majr;
                        while (count < 6)
                        {
                            int dotcount = 0;
                            switch (count)
                            {
                                case 1:
                                    if (Big[majc, majr].getDot(1))
                                        dotcount++;
                                    if (Big[majc, majr].getDot(2))
                                        dotcount++;
                                    if (Big[majc, majr].getDot(4))
                                        dotcount++;
                                    if (Big[majc, majr].getDot(5))
                                        dotcount++;
                                    if (dotcount >= 1)
                                    {
                                        small[smallw, smallh].addDot(1);
                                    }
                                    break;
                                case 2:
                                    if (Big[majc, majr].getDot(3))
                                        dotcount++;
                                    if (Big[majc, majr].getDot(6))
                                        dotcount++;
                                    if (Big[majc, majr + 1].getDot(1))
                                        dotcount++;
                                    if (Big[majc + 1, majr + 1].getDot(4))
                                        dotcount++;
                                    if (dotcount >= 1)
                                    {
                                        small[smallw, smallh].addDot(2);
                                    }
                                    break;
                                case 3:
                                    if (Big[majc, majr + 1].getDot(2))
                                        dotcount++;
                                    if (Big[majc, majr + 1].getDot(3))
                                        dotcount++;
                                    if (Big[majc, majr + 1].getDot(5))
                                        dotcount++;
                                    if (Big[majc, majr + 1].getDot(6))
                                        dotcount++;
                                    if (dotcount >= 1)
                                    {
                                        small[smallw, smallh].addDot(3);
                                    }
                                    break;
                                case 4:
                                    if (Big[majc + 1, majr].getDot(1))
                                        dotcount++;
                                    if (Big[majc + 1, majr].getDot(2))
                                        dotcount++;
                                    if (Big[majc + 1, majr].getDot(4))
                                        dotcount++;
                                    if (Big[majc + 1, majr].getDot(5))
                                        dotcount++;
                                    if (dotcount >= 1)
                                    {
                                        small[smallw, smallh].addDot(4);
                                    }
                                    break;
                                case 5:
                                    if (Big[majc + 1, majr].getDot(3))
                                        dotcount++;
                                    if (Big[majc + 1, majr].getDot(6))
                                        dotcount++;
                                    if (Big[majc + 1, majr + 1].getDot(1))
                                        dotcount++;
                                    if (Big[majc + 1, majr + 1].getDot(4))
                                        dotcount++;
                                    if (dotcount >= 1)
                                    {
                                        small[smallw, smallh].addDot(5);
                                    }
                                    break;
                                case6:
                                    if (Big[majc + 1, majr + 1].getDot(2))
                                        dotcount++;
                                if (Big[majc + 1, majr + 1].getDot(5))
                                    dotcount++;
                                if (Big[majc + 1, majr + 1].getDot(3))
                                    dotcount++;
                                if (Big[majc + 1, majr + 1].getDot(6))
                                    dotcount++;
                                if (dotcount >= 1)
                                {
                                    small[smallw, smallh].addDot(6);
                                }
                                break;

                            }
                            count++;
                            if (count == 6)
                            {
                                smallw++;
                            }
                        }
                    }
                }

            }

            return small;


        }



    }
}