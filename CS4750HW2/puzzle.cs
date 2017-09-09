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

        enum Direction {Up, Down, Left, Right};
        private int[,] puzzle;
        private List<Direction> path;

        public Puzzle(int[,] puzzle)
        {
            this.puzzle = puzzle;
            this.path = new List<Direction>();
        }
        
        /// <summary>
        ///     
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="node"></param>
        /// <returns>A list of all the children points of the node</returns>
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

        private bool isValidNode(Point node)
        {
            return node.X > -1 && node.X < 3 && node.Y > -1 && node.Y < 3;
        }
    } //End class Puzzle
} //End namespace CS4750HW2
