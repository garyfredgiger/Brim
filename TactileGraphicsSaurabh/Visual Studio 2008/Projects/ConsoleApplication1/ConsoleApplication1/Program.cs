using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello world!");
            
            Bitmap bmp = new Bitmap("C:\\Documents and Settings\\t-sausan\\My Documents\\My Pictures\\applelantern3.bmp");
            Console.WriteLine("image read.");
            Color color = new Color();
            Color color_temp = new Color();
            
          int i, j, l;
            for (i = 0; i < 150; ++i)
            {
                for (j = 0; j < 150; ++j)
                {
                    color = bmp.GetPixel(j, i);
                   // Console.Write("{0} ", color);
                    //color.P = 255;
                 //   if (color.Equals(color_temp)){
                    //Console.Write("{0} ", color.ToString());
                    
                        //Console.Write("{0} ", color);
                    if (color.G < 100)
                    {
                        Console.Write("{0} ", color.G);
                    }
                   // }
                }
                Console.WriteLine();
}
       
            Console.WriteLine("{0}\n",Test());
            String str = Console.ReadLine();
        }

        static int Test()
        {
            return 3;
        }


    }
}
