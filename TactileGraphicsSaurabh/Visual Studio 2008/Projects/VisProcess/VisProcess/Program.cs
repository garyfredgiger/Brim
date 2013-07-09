using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;




namespace BrailleImaging {
    
    class Program {
        //Hashtable charset = new Hashtable();

        static void Main(string[] args) {

            Hashtable charset = new Hashtable();
            string line;
            string [] word;

            // Read the file and display it line by line.
            System.IO.StreamReader file =
               new System.IO.StreamReader("C:\\Documents and Settings\\t-sausan\\Desktop\\charset.txt");
            while ((line = file.ReadLine()) != null)
            {
                word = line.Split(' ');
                Console.Write("Key: " );
                Console.WriteLine(word[0]);
                Console.Write("Value: ");
                Console.WriteLine(word[1]);
                charset.Add(word[0], word[1]);
             
            }
            charset.Add("0", " ");
            file.Close();
            Cell temp1 = new Cell(true, true, true, true,true, true);
            Cell temp2 = new Cell(true, false, false, false, false, false);
            Cell temp3 = new Cell(true, true, false, true, true, true);
            Cell temp4 = new Cell(true, false, true, true, true, true);
            Cell temp5 = new Cell(true, true, true, true, true, false);
            Cell temp6 = new Cell(true, true, false, false, true, true);
            Cell temp7 = new Cell(false, false, true, true, true, true);
            Cell temp8 = new Cell(false, false, false, false, false, false);
            string tem1 = temp1.getKey();
            string tem2 = temp2.getKey();
            string tem7 = temp7.getKey();
            string tem8 = temp8.getKey();
            string name = charset[tem1].ToString();
            Console.WriteLine(name);
            name = charset[tem8].ToString();
            Console.WriteLine(name);

            // Suspend the screen.
            Console.ReadLine();


            Bitmap bmp = new Bitmap("C:\\Documents and Settings\\t-sausan\\My Documents\\My Pictures\\elephant.bmp");
            
            Console.WriteLine("-- READING IMAGE --");
            buildBinImage(bmp, "newImage.bmp");
            Console.WriteLine("-- READING IMAGE COMPLETE --");

            Console.WriteLine();

            Console.WriteLine("-- BUILDING TABLE --");
            int[,] binArray = buildBinTable(bmp);
            Console.WriteLine("-- BUILDING TABLE COMPLETE --");
            Cell[,] fullPage = buildCelTable(bmp);
            TextWriter text = new StreamWriter("test.txt");
            TextWriter braille = new StreamWriter("Braille.txt");
            for (int z = 0; z < bmp.Height / 3; z++)
            {
               
                for (int w = 0; w < bmp.Width / 2; w++)
                {

                    text.WriteLine("\n({0}, {1})", w, z);
                    text.Write("Dot 1");
                    text.WriteLine(fullPage[w, z].getDot(1));
                    text.Write("Dot 2");
                    text.WriteLine(fullPage[w, z].getDot(2));
                    text.Write("Dot 3");
                    text.WriteLine(fullPage[w, z].getDot(3));
                    text.Write("Dot 4");
                    text.WriteLine(fullPage[w, z].getDot(4));
                    text.Write("Dot 5");
                    text.WriteLine(fullPage[w, z].getDot(5));
                    text.Write("Dot 6");
                    text.WriteLine(fullPage[w, z].getDot(6));
                    string tempkey = fullPage[w,z].getKey();
                    braille.Write(charset[tempkey].ToString());
                }
                braille.WriteLine();
           
            }

            text.Close();
            braille.Close();

            Console.WriteLine();

            //printTable(binArray, bmp);

            Console.WriteLine();

            //perform tests on empty cell
            Cell testC1 = new Cell();
            Console.WriteLine(testC1);
            Console.WriteLine("testC1 numDots: {0}\n", testC1.getNumDots());

            Console.WriteLine("--Testing addDot()");
            Console.WriteLine("testC1.addDot(1): {0}", testC1.addDot(1));
            Console.WriteLine("testC1.addDot(5): {0}", testC1.addDot(5));
            Console.WriteLine("testC1 numDots: {0}\n", testC1.getNumDots());

            Console.WriteLine("--Testing removeDot()");
            Console.WriteLine("testC1.removeDot(6): {0}", testC1.removeDot(6));
            Console.WriteLine("testC1.removeDot(5): {0}", testC1.removeDot(5));
            Console.WriteLine("testC1 numDots: {0}\n", testC1.getNumDots());

            Console.WriteLine(testC1);
            Console.WriteLine();

            //perform tests on partially initialized cell
            Cell testC2 = new Cell(true, false, false, true, true, false);
            Console.WriteLine(testC2);
            Console.WriteLine("testC2 numDots: {0}\n", testC2.getNumDots());

            Console.WriteLine("--Testing addDot()");
            Console.WriteLine("testC2.addDot(1): {0}", testC2.addDot(1));
            Console.WriteLine("testC2.addDot(6): {0}", testC2.addDot(6));
            Console.WriteLine("testC2 numDots: {0}\n", testC2.getNumDots());

            Console.WriteLine("--Testing removeDot()");
            Console.WriteLine("testC1.removeDot(5): {0}", testC2.removeDot(5));
            Console.WriteLine("testC1.removeDot(3): {0}", testC2.removeDot(3));
            Console.WriteLine("testC2 numDots: {0}\n", testC2.getNumDots());

            Console.WriteLine(testC2);


      

            //TextWriter text = new StreamWriter("test.txt");
            //text.Write("{0} ", color.GetBrightness());
            //text.WriteLine("---");
            //text.Close();
        }



        /* create a new binary image of specified filename from specified file */
        public static void buildBinImage(Bitmap image, string newFile)
        {

            Bitmap newImage = new Bitmap(image.Width, image.Height);
            Color color = new Color();

            for (int y = 0; y < image.Height; y++){
                for (int x = 0; x < image.Width; x++){
                    color = image.GetPixel(x, y);
                    if ( color.GetBrightness() < 0.5 ){
                        newImage.SetPixel(x, y, Color.Black);
                    }
                    else{
                        newImage.SetPixel(x, y, Color.White);
                    }
                }
            }
            newImage.Save(newFile);
        }


        /* build a binary array representing the specified image */
        
        public static Cell[,] buildCelTable(Bitmap image)
        {
             int cellx = 0;
            int celly = 0;
            int cellnum = 0;
            int cellArrx = 0;
            int cellArry = 0;
            Cell[,] fullPage = new Cell[image.Width/2, image.Height/3];
            int[,] binArray = new int[image.Width, image.Height];
            Color color = new Color();
            for (int y = 0; y < image.Height; y += 3)
            {
                for (int x = 0; x < image.Width; x += 2)
                {
                    int count = 1;
                    cellx = 0;
                    int tempx = x;

                    while (cellx < 2)
                    {
                        int tempy = y;
                        celly = 0;
                        while (celly < 3)
                        {
                            color = image.GetPixel(tempx, tempy);
                            switch (count)
                            {
                                case 1:

                                    fullPage[cellArrx, cellArry] = new Cell();
                                    if (color.GetBrightness() < 0.5)
                                    {

                                        binArray[tempx, tempy] = 0;
                                        fullPage[cellArrx, cellArry].addDot(1);
                                    }
                                    else
                                    {
                                        binArray[tempx, tempy] = 1;
                                    }
                                    break;
                                case 2:
                                    if (color.GetBrightness() < 0.5)
                                    {

                                        binArray[tempx, tempy] = 0;
                                        fullPage[cellArrx, cellArry].addDot(2);
                                    }
                                    else
                                    {
                                        binArray[tempx, tempy] = 1;
                                    }
                                    break;
                                case 3:
                                    if (color.GetBrightness() < 0.5)
                                    {

                                        binArray[tempx, tempy] = 0;
                                        fullPage[cellArrx, cellArry].addDot(3);
                                    }
                                    else
                                    {
                                        binArray[tempx, tempy] = 1;

                                    }
                                    break;
                                case 4:
                                    if (color.GetBrightness() < 0.5)
                                    {

                                        binArray[tempx, tempy] = 0;
                                        fullPage[cellArrx, cellArry].addDot(4);
                                    }
                                    else
                                    {
                                        binArray[tempx, tempy] = 1;
                                    }
                                    break;
                                case 5:
                                    if (color.GetBrightness() < 0.5)
                                    {

                                        binArray[tempx, tempy] = 0;
                                        fullPage[cellArrx, cellArry].addDot(5);
                                    }
                                    else
                                    {
                                        binArray[x, tempy] = 1;
                                    }
                                    break;
                                case 6:
                                    if (color.GetBrightness() < 0.5)
                                    {

                                        binArray[tempx, tempy] = 0;
                                        fullPage[cellArrx, cellArry].addDot(6);
                                    }
                                    else
                                    {
                                        binArray[tempx, tempy] = 1;

                                    }
                                    cellnum++;
                                    break;
                            }
                            celly++;
                            tempy++;
                            count++;

                        }
                        cellx++;
                        tempx++;
                    }

                    Console.WriteLine(fullPage[cellArrx, cellArry].getKey());
                    cellArrx++;

                }
                cellArrx = 0;
                cellArry++;
            }
            Console.WriteLine("writing to output file");
            Console.WriteLine("how many cells in the array");
            Console.WriteLine(cellnum);

           
            return fullPage;
        }


        
        public static int[,] buildBinTable(Bitmap image)
        {
            int cellx = 0;
            int celly = 0;
            int cellnum = 0;
            int cellArrx = 0;
            int cellArry = 0;
            Cell[,] fullPage = new Cell[image.Width/2, image.Height/3];
            int[,] binArray = new int[image.Width, image.Height];
            Color color = new Color();
            for (int y = 0; y < image.Height; y+=3)
            {
                for (int x = 0; x < image.Width; x+=2)
                {
                    int count = 1;
                    cellx = 0;
                   int tempx = x;
                    
                    while (cellx < 2)
                    {
                        int tempy = y;
                        celly = 0;
                        while (celly < 3)
                        {
                            color = image.GetPixel(tempx, tempy);
                            switch (count)
                            {
                                case 1:

                                    fullPage[cellArrx, cellArry] = new Cell();
                                    if (color.GetBrightness() < 0.5)
                                    {

                                        binArray[tempx, tempy] = 0;
                                        fullPage[cellArrx, cellArry].addDot(1);
                                    }
                                    else
                                    {
                                        binArray[tempx, tempy] = 1;
                                    }
                                    break;
                                case 2:
                                    if (color.GetBrightness() < 0.5)
                                    {

                                        binArray[tempx, tempy] = 0;
                                        fullPage[cellArrx, cellArry].addDot(2);
                                    }
                                    else
                                    {
                                        binArray[tempx, tempy] = 1;
                                    }
                                    break;
                                case 3:
                                    if (color.GetBrightness() < 0.5)
                                    {

                                        binArray[tempx, tempy] = 0;
                                        fullPage[cellArrx, cellArry].addDot(3);
                                    }
                                    else
                                    {
                                        binArray[tempx, tempy] = 1;
                                        
                                    }
                                    break;
                                case 4:
                                    if (color.GetBrightness() < 0.5)
                                    {

                                        binArray[tempx, tempy] = 0;
                                        fullPage[cellArrx, cellArry].addDot(4);
                                    }
                                    else
                                    {
                                        binArray[tempx, tempy] = 1;
                                    }
                                    break;
                                case 5:
                                    if (color.GetBrightness() < 0.5)
                                    {

                                        binArray[tempx, tempy] = 0;
                                        fullPage[cellArrx, cellArry].addDot(5);
                                    }
                                    else
                                    {
                                        binArray[x, tempy] = 1;
                                    }
                                    break;
                                case 6:
                                    if (color.GetBrightness() < 0.5)
                                    {

                                        binArray[tempx, tempy] = 0;
                                        fullPage[cellArrx, cellArry].addDot(6);
                                    }
                                    else
                                    {
                                        binArray[tempx, tempy] = 1;
                                        
                                    }
                                    cellnum++;
                                    break;
                            }
                            celly++;
                            tempy++;
                            count++;

                        }
                        cellx++;
                        tempx++;
                    }
                    
                    Console.WriteLine(fullPage[cellArrx, cellArry].getKey());
                    cellArrx++;
                   
                }
                cellArrx = 0;
                cellArry++;
            }
            Console.WriteLine("writing to output file");
            Console.WriteLine("how many cells in the array");
                Console.WriteLine(cellnum);

            
            return binArray;
        }


        public static void printTable(int[,] arr, Bitmap image){
            for (int y = 0; y < image.Width; y++){
                for (int x = 0; x < image.Height; x++){
                    Console.Write(arr[x, y]);
                }
                Console.WriteLine();
            }
        }


    }
    public static bool EdgeEnhance(Bitmap b, byte nThreshold)
{
    // This one works by working out the greatest difference between a 
    // nPixel and it's eight neighbours. The threshold allows softer 
    // edges to be forced down to black, use 0 to negate it's effect.
    Bitmap b2 = (Bitmap) b.Clone();

    // GDI+ still lies to us - the return format is BGR, NOT RGB.
    BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), 
                                   ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
    BitmapData bmData2 = b2.LockBits(new Rectangle(0, 0, b.Width, b.Height), 
                                    ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

    int stride = bmData.Stride;
    System.IntPtr Scan0 = bmData.Scan0;
    System.IntPtr Scan02 = bmData2.Scan0;

    unsafe
    {
        byte * p = (byte *)(void *)Scan0;
        byte * p2 = (byte *)(void *)Scan02;

        int nOffset = stride - b.Width*3;
        int nWidth = b.Width * 3;

        int nPixel = 0, nPixelMax = 0;

        p += stride;
        p2 += stride;

        for (int y = 1; y < b.Height-1; ++y)
        {
            p += 3;
            p2 += 3;

            for (int x = 3; x < nWidth-3; ++x)
            {
                nPixelMax = Math.Abs((p2 - stride + 3)[0] - (p2 + stride - 3)[0]);

                nPixel = Math.Abs((p2 + stride + 3)[0] - (p2 - stride - 3)[0]);

                if (nPixel > nPixelMax) nPixelMax = nPixel;

                nPixel = Math.Abs((p2 - stride)[0] - (p2 + stride)[0]);

                if (nPixel > nPixelMax) nPixelMax = nPixel;

                nPixel = Math.Abs((p2 + 3)[0] - (p2 - 3)[0]);

                if (nPixel > nPixelMax) nPixelMax = nPixel;

                if (nPixelMax > nThreshold && nPixelMax > p[0])
                    p[0] = (byte) Math.Max(p[0], nPixelMax);

                ++ p;
                ++ p2;            
            }

            p += nOffset + 3;
            p2 += nOffset + 3;
        }
    }    

    b.UnlockBits(bmData);
    b2.UnlockBits(bmData2);

    return true;

}