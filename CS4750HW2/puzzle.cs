using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS4750HW2
{
    class Puzzle
    {

        // Data model of puzzle
        private int[,] puzzle = new int[3, 3];


        public Puzzle(int[,] puzzle)
        {
            this.puzzle = puzzle;
        }


        public Point[] getChildren(Point parent, Point node)
        {
            return null;
        } 

        public int getSingleManahatanDistance(Point node)
        {
            //Declare variables
            int distance = -1;
            int nodeNum = puzzle[node.X, node.Y];

            switch(nodeNum)
            {

            } //End 


            return distance;
        } //End 
    } //End class Puzzle
} //End namespace CS4750HW2