using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BrailleImaging {
    
    class Cell {

        /* private variables */
        private int numDots;
        private string key;
        private bool dot1;
        private bool dot2;
        private bool dot3;
        private bool dot4;
        private bool dot5;
        private bool dot6;


        /* default constructor */
        public Cell(){
            numDots = 0;
            dot1 = false;
            dot2 = false;
            dot3 = false;
            dot4 = false;
            dot5 = false;
            dot6 = false;
        }


        /* constructor with dots specified */
        public Cell(bool dot1, bool dot2, bool dot3, bool dot4, bool dot5, bool dot6){
            numDots = 0;
            this.dot1 = dot1;
            this.dot2 = dot2;
            this.dot3 = dot3;
            this.dot4 = dot4;
            this.dot5 = dot5;
            this.dot6 = dot6;
            if (dot1) numDots++;
            if (dot2) numDots++;
            if (dot3) numDots++;
            if (dot4) numDots++;
            if (dot5) numDots++;
            if (dot6) numDots++;
        }


        /* add specified dot */
        public bool addDot(int dotNum){
            switch (dotNum){
                case 1:
                    if (!dot1){
                        dot1 = true;
                        numDots++;
                        return true;
                    }
                    return false;
                case 2:
                    if (!dot2){
                        dot2 = true;
                        numDots++;
                        return true;
                    }
                    return false;
                case 3:
                    if (!dot3){
                        dot3 = true;
                        numDots++;
                        return true;
                    }
                    return false;
                case 4:
                    if (!dot4){
                        dot4 = true;
                        numDots++;
                        return true;
                    }
                    return false;
                case 5:
                    if (!dot5){
                        dot5 = true;
                        numDots++;
                        return true;
                    }
                    return false;
                case 6:
                    if (!dot6){
                        dot6 = true;
                        numDots++;
                        return true;
                    }
                    return false;
                default:
                    Console.WriteLine("-- ERROR -> INVALID DOT -- ");
                    return false;
            }
        }

        
        /* remove specified dot */
        public bool removeDot(int dotNum){
            switch (dotNum){
                case 1:
                    if (dot1){
                        dot1 = false;
                        numDots--;
                        return true;
                    }
                    return false;
                case 2:
                    if (dot2){
                        dot2 = false;
                        numDots--;
                        return true;
                    }
                    return false;
                case 3:
                    if (dot3){
                        dot3 = false;
                        numDots--;
                        return true;
                    }
                   return false;
                case 4:
                    if (dot4){
                        dot4 = false;
                        numDots--;
                        return true;
                    }
                    return false;
                case 5:
                    if (dot5){
                        dot5 = false;
                        numDots--;
                        return true;
                    }
                    return false;
                case 6:
                    if (dot6){
                        dot6 = false;
                        numDots--;
                        return true;
                    }
                    return false;
                default:
                    Console.WriteLine("-- ERROR -> INVALID DOT -- ");
                    return false;
            }
        }


        /* get specified dot */
        public bool getDot(int dotNum){
            switch (dotNum){
                case 1:
                    return dot1;
                case 2:
                    return dot2;
                case 3:
                    return dot3;
                case 4:
                    return dot4;
                case 5:
                    return dot5;
                case 6:
                    return dot6;
                default:
                    Console.WriteLine(" -- ERROR -> INVALID DOT -- ");
                    return false;
            }
        }


        /* get number of filled dots */
        public string getKey()
        {
            string temp= null;
            for (int i = 1; i <= 6; i++)
            {
                if (getDot(i))
                    temp += i;

            }
            if (temp==null)
                key = "0";
            else
                key = temp;
            Console.WriteLine(key);
            return key; 
            
        }
        public int getNumDots()
        {
            return numDots;
        }
        public int numMatches(Cell otherCell)
        {

            int numMatches = 0;

            if (dot1 && dot1 == otherCell.dot1) numMatches++;

            if (dot2 && dot2 == otherCell.dot2) numMatches++;

            if (dot3 && dot3 == otherCell.dot3) numMatches++;

            if (dot4 && dot4 == otherCell.dot4) numMatches++;

            if (dot5 && dot5 == otherCell.dot5) numMatches++;

            if (dot6 && dot6 == otherCell.dot6) numMatches++;

            return numMatches;

        }



        /* return the number of matching dots with another cell */


        
        /* return cell string representation */
        public override string ToString(){
            string str = "";

            if (dot1) str += "* ";
            else str += "0 ";

            if (dot4) str += "*\n";
            else str += "0\n";

            if (dot2) str += "* ";
            else str += "0 ";

            if (dot5) str += "*\n";
            else str += "0\n";

            if (dot3) str += "* ";
            else str += "0 ";

            if (dot6) str += "*";
            else str += "0";

            return str;
        }


    }
     
}
