using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisProcess
{
    class BrailleSheet
    {
        //private Cell[,] fullPage = new Cell[42, 28];
        private int cellx = 0;
        private int cellxmax = 42;
        private int celly = 0;
        private int cellymax = 28;
        public BrailleSheet(){
            for (cellx = 0; cellx < cellxmax; cellx++)
            {
                for (celly = 0; celly < cellymax; celly++)
                {
                    // filldotsincell function;
                   // fullPage[cellx, celly].filldot;
                }
            }
        }

    }
}
