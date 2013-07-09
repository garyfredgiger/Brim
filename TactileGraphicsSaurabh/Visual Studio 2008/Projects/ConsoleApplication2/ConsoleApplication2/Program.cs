using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;


namespace BrailleImaging {
    
    class Program {

        static void Main(string[] args) {
            
            string inputFile = "C:\\Documents and Settings\\t-sausan\\My Documents\\My Pictures\\";
            Console.WriteLine("Please enter input filename:\n");
            string namePic = Console.ReadLine();
            inputFile += namePic;
            //inputFile += Console.ReadLine();

            Console.WriteLine(inputFile);
            string [] tokens = namePic.Split(new char[] {'.'});
            Console.WriteLine(tokens[0]);
            Bitmap bmp = new Bitmap(inputFile);
            float threshold = getThreshold(bmp);
            int resizetimes =1;
           // Bitmap coolrescale = scaleDown(bmp, 0.5);
            Bitmap coolrescale = scaleDown(bmp, getScaleFactor(bmp));

            while (bmp.Width/resizetimes > 84 || bmp.Height/resizetimes > 84)
            {
                resizetimes++;                
            }
           // Bitmap resized = Resize2(bmp, bmp.Width/resizetimes, bmp.Height/resizetimes, true);
            Console.WriteLine("-- READING IMAGE --");
            buildBinImage(bmp, "newImage.bmp", threshold);
            buildBinImage(coolrescale, "smallImage.bmp", threshold);
            Console.WriteLine("-- READING IMAGE COMPLETE --");

            Console.WriteLine();

            Console.WriteLine("-- BUILDING TABLE --");
            int[, , ,] binArray = buildBinTable(bmp, threshold);
           // int[, , ,] bin2Array = buildBinTable(resized);
            Console.WriteLine("-- BUILDING TABLE COMPLETE --");

            Console.WriteLine();

            printTable(binArray, bmp);
           // printTable(bin2Array, resized);
            Console.WriteLine();

            //perform tests on empty cell
            /*Cell testC1 = new Cell();
            Console.WriteLine(testC1);
            Console.WriteLine("testC1 numDots: {0}\n", testC1.getNumDots());

            Console.WriteLine(">>Testing addDot()");
            Console.WriteLine("testC1.addDot(1): {0}", testC1.addDot(1));
            Console.WriteLine("testC1.addDot(5): {0}", testC1.addDot(5));
            Console.WriteLine("testC1.addDot(4): {0}", testC1.addDot(4));
            Console.WriteLine("testC1 numDots: {0}\n", testC1.getNumDots());

            Console.WriteLine(">>Testing removeDot()");
            Console.WriteLine("testC1.removeDot(6): {0}", testC1.removeDot(6));
            Console.WriteLine("testC1.removeDot(5): {0}", testC1.removeDot(5));
            Console.WriteLine("testC1 numDots: {0}\n", testC1.getNumDots());

            Console.WriteLine(testC1);
            Console.WriteLine();*/

            //perform tests on partially initialized cell
            /*Cell testC2 = new Cell(true, false, false, true, true, false);
            Console.WriteLine(testC2);
            Console.WriteLine("testC2 numDots: {0}\n", testC2.getNumDots());

            Console.WriteLine(">>Testing addDot()");
            Console.WriteLine("testC2.addDot(1): {0}", testC2.addDot(1));
            Console.WriteLine("testC2.addDot(6): {0}", testC2.addDot(6));
            Console.WriteLine("testC2 numDots: {0}\n", testC2.getNumDots());

            Console.WriteLine(">>Testing removeDot()");
            Console.WriteLine("testC1.removeDot(5): {0}", testC2.removeDot(5));
            Console.WriteLine("testC1.removeDot(3): {0}", testC2.removeDot(3));
            Console.WriteLine("testC2 numDots: {0}\n", testC2.getNumDots());

            Console.WriteLine(testC2);

            Console.WriteLine("testC1.numMatches(testC2): {0}", testC1.numMatches(testC2));

            Console.WriteLine();*/

            //build cell array and perform tests on it
            Console.WriteLine(">>Testing buildCellTable()");
            Cell[,] cellArray = buildCellTable(bmp, threshold);
            Console.WriteLine("Table built");
            //Cell[,] cell2Array = buildCellTable(resized);
            Bitmap AvgImage = resizeIm(bmp, 1);
            while (AvgImage.Width > 84 || AvgImage.Height > 84)
            {
                AvgImage = resizeIm(AvgImage, 2);
            }
            buildBinImage(AvgImage, "resizedImage.bmp", threshold);
   
            Cell[,] AvgArr = buildCellTable(AvgImage, threshold);
            Cell[,] coolArr = buildCellTable(coolrescale, threshold);
            /*Console.WriteLine("writing to output file");
            TextWriter text = new StreamWriter("test.txt");
            for (int y = 0; y < bmp.Height / 3; y++){
                for (int x = 0; x < bmp.Width / 2; x++){
                    text.WriteLine("\n({0}, {1})", x, y);
                    text.WriteLine(cellArray[x, y]);
                    text.WriteLine("getKey(): {0}", cellArray[x, y].getKey());
                }
            }
            text.Close();*/

            //test buildHashtable()
            Hashtable charTable = buildHashtable();
            Cell[,] smallArr = resizeCell(cellArray);
            Cell[,] twofuncArr = resizeCell(coolArr);
            while (twofuncArr.GetLength(0) > 42 || twofuncArr.GetLength(1) > 28)
            {
                twofuncArr = resizeCell(twofuncArr);
            }
            while (smallArr.GetLength(0) > 42 || smallArr.GetLength(1) > 28)
            {
                smallArr = resizeCell(smallArr);
            }
            writeOutput(cellArray, charTable, "output.txt", tokens[0]);
            writeOutput(smallArr, charTable, "outputsmall.txt", tokens[0]);
            //writeOutput(cell2Array, charTable, "outputresized.txt", tokens[0]);
            writeOutput(AvgArr, charTable, "outputAVG.txt", tokens[0]);
            writeOutput(coolArr, charTable, "outputC.txt", tokens[0]);
            writeOutput(twofuncArr, charTable, "output2func.txt", tokens[0]);

        }


        /* create a new binary image of specified filename from specified file */
        public static Bitmap Resize2(Bitmap b, int nWidth, int nHeight, bool bBilinear)
        {
            Bitmap bTemp = (Bitmap)b.Clone();
            b = new Bitmap(nWidth, nHeight, bTemp.PixelFormat);

            double nXFactor = (double)bTemp.Width / (double)nWidth;
            double nYFactor = (double)bTemp.Height / (double)nHeight;

            if (bBilinear)
            {
                double fraction_x, fraction_y, one_minus_x, one_minus_y;
                int ceil_x, ceil_y, floor_x, floor_y;
                Color c1 = new Color();
                Color c2 = new Color();
                Color c3 = new Color();
                Color c4 = new Color();
                byte red, green, blue;

                byte b1, b2;

                for (int x = 0; x < b.Width; ++x)
                    for (int y = 0; y < b.Height; ++y)
                    {
                        // Setup

                        floor_x = (int)Math.Floor(x * nXFactor);
                        floor_y = (int)Math.Floor(y * nYFactor);
                        ceil_x = floor_x + 1;
                        if (ceil_x >= bTemp.Width) ceil_x = floor_x;
                        ceil_y = floor_y + 1;
                        if (ceil_y >= bTemp.Height) ceil_y = floor_y;
                        fraction_x = x * nXFactor - floor_x;
                        fraction_y = y * nYFactor - floor_y;
                        one_minus_x = 1.0 - fraction_x;
                        one_minus_y = 1.0 - fraction_y;

                        c1 = bTemp.GetPixel(floor_x, floor_y);
                        c2 = bTemp.GetPixel(ceil_x, floor_y);
                        c3 = bTemp.GetPixel(floor_x, ceil_y);
                        c4 = bTemp.GetPixel(ceil_x, ceil_y);

                        // Blue
                        b1 = (byte)(one_minus_x * c1.B + fraction_x * c2.B);

                        b2 = (byte)(one_minus_x * c3.B + fraction_x * c4.B);

                        blue = (byte)(one_minus_y * (double)(b1) + fraction_y * (double)(b2));

                        // Green
                        b1 = (byte)(one_minus_x * c1.G + fraction_x * c2.G);

                        b2 = (byte)(one_minus_x * c3.G + fraction_x * c4.G);

                        green = (byte)(one_minus_y * (double)(b1) + fraction_y * (double)(b2));

                        // Red
                        b1 = (byte)(one_minus_x * c1.R + fraction_x * c2.R);

                        b2 = (byte)(one_minus_x * c3.R + fraction_x * c4.R);

                        red = (byte)(one_minus_y * (double)(b1) + fraction_y * (double)(b2));

                        b.SetPixel(x, y, System.Drawing.Color.FromArgb(255, red, green, blue));
                    }
                
            }
            else
            {
                for (int x = 0; x < b.Width; ++x)
                    for (int y = 0; y < b.Height; ++y)
                        b.SetPixel(x, y, bTemp.GetPixel((int)(Math.Floor(x * nXFactor)),
                                  (int)(Math.Floor(y * nYFactor))));
            }

            return b;
        }
        public static Bitmap resizeIm(Bitmap image, int resizeFactor)
        {
            Bitmap newImage = new Bitmap(image.Width / resizeFactor, image.Height / resizeFactor);
            for (int y = 0; y < newImage.Height; y++)
            {
                for (int x = 0; x < newImage.Width; x++)
                {
                    Color Temp = AvgColor(image, resizeFactor, x*resizeFactor, y*resizeFactor);
                    newImage.SetPixel(x, y, Temp);



                }
            }


                    return newImage;
        }
        public static Color AvgColor(Bitmap image, int resizeFactor, int startx, int starty)
        {
            int sumG=0;
            int sumR=0;
            int sumB=0;
            for(int y=starty; y<starty + resizeFactor; y++){
                for(int x = startx; x<startx +resizeFactor; x++) {
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
        public static void buildBinImage(Bitmap image, string fileName, float threshold)
        {
            Bitmap newImage = new Bitmap(image.Width+1, image.Height+1);
            Color color = new Color();

            for (int y = 0; y < image.Height; y++){
                for (int x = 0; x < image.Width; x++){
                    color = image.GetPixel(x, y);
                    if ( color.GetBrightness() < threshold ){
                        newImage.SetPixel(x, y, Color.Black);
                    }
                    else{
                        newImage.SetPixel(x, y, Color.White);
                    }
                }
            }
           
            newImage.Save(fileName);
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
                            if (color.GetBrightness() < threshold){
                                
                                binArr[majr, majc, minr, minc] = 0;
                            }
                            else{
                                binArr[majr, majc, minr, minc] = 1;
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
                            if (color.GetBrightness() < threshold){
                                cellArr[majc, majr].addDot((3 * minc) + minr + 1);
                            }
                        }
                    }
                }
            }
            return cellArr;
        } 
      /*  public static Cell[,] buildCellTable(Bitmap image)
        {
            Cell[,] cellArr = new Cell[image.Width / 4, image.Height / 6];
            Color color = new Color();

            for (int majr = 0; majr < image.Height; majr++)
            {
                int count =0;
                int pixelcount =0;
                for (int majc = 0; majc < image.Width; majc+=2)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        for (j = 0; j < 2; j++)
                        {
                            color = image.GetPixel(majc + j, majr + i);
                            if(color.GetBrightness() < 0.5)
                                 pixelcount ++;
                        }
                    }
                    if (pixelcount > 2)
                        cellArr[majc, majr].addDot(count);
                    switch (count)
                    {


                    }
  
                }
             }
            
            return cellArr;
        } */
     /*   public static Cell[,] resizeCell(Cell[,] Big){
            
            int width = Big.GetLength(0)/2;
            int height = Big.GetLength(1)/3;
            Cell[,] small = new Cell[width, height];
             for (int majr = 0; majr < height; majr++){
                for (int majc = 0; majc < width; majc++){
                    small[majc, majr] = new Cell();
                    
                    for (int minr = 0; minr < 3; minr++){
                //    for (int minr = 0; minr < 2; minr++){
                        for (int minc = 0; minc < 2; minc++){
                            int count =0;
                            for (int i =0; i<6; i++) { 
                            if(Big[(2 * majc) + minc, (3 * majr) + minr].getDot(i))
                                count ++;
                            }
                            if(count >= 2) {
                                small[majc, majr].addDot((3 * minc) + minr + 1);
                            }
                        }
                    }
                }
               
             }

             return small;


        }*/
        public static Cell[,] resizeCell(Cell[,] Big)
        {

            int width = Big.GetLength(0) / 2;
            int height = Big.GetLength(1) / 2;
            int smallw = 0;
            int smallh = 0;
            Cell[,] small = new Cell[width, height];
            for (int majr = 0; majr <Big.GetLength(1); majr+=2)
            {
                if (majr != 0)
                {
                    smallh++;
                }
                    smallw = 0;
                for (int majc = 0; majc < Big.GetLength(0); majc+=2)
                {
                    if(smallw < small.GetLength(0) && smallh < small.GetLength(1)){
                    small[smallw, smallh] = new Cell();
                    int count = 0;
                    int curw = majc;
                    int curl = majr;
                    while(count < 6) {
                        int dotcount=0;
                        switch (count) {
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
                            if (Big[majc, majr+1].getDot(1))
                            dotcount++;
                            if (Big[majc+1, majr+1].getDot(4))
                            dotcount++;
                            if (dotcount >= 1)
                            {
                                small[smallw, smallh].addDot(2);
                            }
                            break;
                            case 3:
                            if (Big[majc, majr+1].getDot(2))
                            dotcount++;
                            if (Big[majc, majr+1].getDot(3))
                            dotcount++;
                            if (Big[majc, majr+1].getDot(5))
                            dotcount++;
                            if (Big[majc, majr+1].getDot(6))
                            dotcount++;
                            if (dotcount >= 1)
                            {
                                small[smallw, smallh].addDot(3);
                            }
                            break;
                            case 4:
                            if (Big[majc+1, majr].getDot(1))
                            dotcount++;
                            if (Big[majc+1, majr].getDot(2))
                            dotcount++;
                            if (Big[majc+1, majr].getDot(4))
                            dotcount++;
                            if (Big[majc+1, majr].getDot(5))
                            dotcount++;
                            if (dotcount >= 1)
                            {
                                small[smallw, smallh].addDot(4);
                            }
                            break;
                             case 5:
                            if (Big[majc+1, majr].getDot(3))
                            dotcount++;
                            if (Big[majc+1, majr].getDot(6))
                            dotcount++;
                            if (Big[majc+1, majr+1].getDot(1))
                            dotcount++;
                            if (Big[majc+1, majr+1].getDot(4))
                            dotcount++;
                            if (dotcount >= 1)
                            {
                                small[smallw, smallh].addDot(5);
                            }
                            break;
                            case6:
                            if (Big[majc+1, majr+1].getDot(2))
                            dotcount++;
                            if (Big[majc+1, majr+1].getDot(5))
                            dotcount++;
                            if (Big[majc+1, majr+1].getDot(3))
                            dotcount++;
                            if (Big[majc+1, majr+1].getDot(6))
                            dotcount++;
                            if (dotcount >= 1)
                            {
                                small[smallw, smallh].addDot(6);
                            }
                            break;

                        }
                        count++;
                        if(count == 6){
                            smallw++;
                        }
                    }
                    }
                }

            }

            return small;


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
        public static void writeOutput(Cell[,] cellArr, Hashtable map, string fileName, string picName){
            TextWriter text = new StreamWriter(fileName);
            text.WriteLine(picName);
            for (int y = 0; y < cellArr.GetLength(1); y++){
                for (int x = 0; x < cellArr.GetLength(0); x++){
                    text.Write(map[cellArr[x, y].getKey()]);
                }
                text.WriteLine();
            }

            text.Close();
        }


        public static Bitmap scaleDown(Bitmap image, double scaleFactor)
        {

            Bitmap newImage = new Bitmap((int)(image.Width * scaleFactor), (int)(image.Height * scaleFactor));

            newImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);   //test

            Graphics g = Graphics.FromImage(newImage);

            g.InterpolationMode = InterpolationMode.HighQualityBilinear;

            g.DrawImage(image,

                new Rectangle(0, 0, (int)(image.Width * scaleFactor), (int)(image.Height * scaleFactor)),

                0, 0, image.Width, image.Height, GraphicsUnit.Pixel);

            newImage.Save("scaled.bmp", ImageFormat.Bmp);

            return newImage;

        }
        public static double getScaleFactor(Bitmap image)
        {

            return (84 / (double)Math.Max(image.Width, image.Height));

        }
        public static float getThreshold(Bitmap image)
        {
            float tInit = 0.5f;
            float tFinal = 0.0f;
            float set1;
            float set2;
            int pix1;
            int pix2;

            Color color = new Color();
            float brightness;

            while (tInit != tFinal)
            {
                set1 = 0.0f;
                set2 = 0.0f;
                pix1 = 0;
                pix2 = 0;
                Console.WriteLine("{0} {1}", tInit, tFinal);
                for (int y = 0; y < image.Height; y++)
                {
                    for (int x = 0; x < image.Width; x++)
                    {
                        color = image.GetPixel(x, y);
                        brightness = color.GetBrightness();
                        if (brightness > tInit)
                        {
                            set1 += brightness;
                            pix1++;
                        }
                        else
                        {
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

    }
}