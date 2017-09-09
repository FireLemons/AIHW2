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
        public enum Direction {Up, Down, Left, Right};
        private int[,] puzzle;
        private List<Direction> path;

        public Puzzle(int[,] puzzle)
        {
            this.puzzle = puzzle;
            this.path = new List<Direction>();
        }
        
        /// <summary>
        ///     Gets the neighboring tiles as points excluding the parent.
        /// </summary>
        /// <param name="parent">The parent tile</param>
        /// <param name="tile">The tile to find children for</param>
        /// <returns>A list of all the children points of the node</returns>
        public List<Point> getChildren(Point parent, Point tile)
        {
            List<Point> children = new List<Point>();
            if (!(isValidNode(parent) && isValidNode(tile)))
            {
                return null;
            }
            else
            {
                Point up = new Point(tile.X - 1,tile.Y);
                Point down = new Point(tile.X + 1,tile.Y);
                Point left = new Point(tile.X, tile.Y - 1);
                Point right = new Point(tile.X, tile.Y + 1);

                if (isValidNode(up) && !Point.Equals(up, parent))
                {
                    children.Add(up);
                }

                if (isValidNode(down) && !Point.Equals(down, parent))
                {
                    children.Add(down);
                }

                if (isValidNode(left) && !Point.Equals(left, parent))
                {
                    children.Add(left);
                }

                if (isValidNode(right) && !Point.Equals(right, parent))
                {
                    children.Add(right);
                }
                return children;
            }
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

        /**********************************************************************************
         * Helper Methods
         **********************************************************************************/

        private bool isValidNode(Point node)
        {
            return node.X > -1 && node.X < 3 && node.Y > -1 && node.Y < 3;
        }

        private bool isAdjacent(Point node1, Point node2)
        {
            return Math.Abs(node1.X - node2.X) == 1 || Math.Abs(node1.Y - node2.Y) == 1; 
        }
    } //End class Puzzle
} //End namespace CS4750HW2
