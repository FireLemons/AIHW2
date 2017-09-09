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

        private int calcManahatanDistance(int targetX, int targetY, int curX, int curY)
        {
            return Math.Abs(targetX - curX) + Math.Abs(targetY - curY);
        } //End private int calcManahatanDistance(int targetX, int targetY, int curX, int curY)

        public int getSingleManahatanDistance(Point node)
        {
            //Declare variables
            int distance = -1;
            int nodeNum = -1;

            if (isValidNode(node))
            {
                nodeNum = puzzle[node.X, node.Y];

                switch (nodeNum)
                {
                    case 0:
                        distance = calcManahatanDistance(2, 2, node.X, node.Y);
                        break;
                    case 1:
                        distance = calcManahatanDistance(0, 0, node.X, node.Y);
                        break;
                    case 2:
                        distance = calcManahatanDistance(1, 0, node.X, node.Y);
                        break;
                    case 3:
                        distance = calcManahatanDistance(2, 0, node.X, node.Y);
                        break;
                    case 4:
                        distance = calcManahatanDistance(0, 1, node.X, node.Y);
                        break;
                    case 5:
                        distance = calcManahatanDistance(1, 1, node.X, node.Y);
                        break;
                    case 6:
                        distance = calcManahatanDistance(2, 1, node.X, node.Y);
                        break;
                    case 7:
                        distance = calcManahatanDistance(0, 2, node.X, node.Y);
                        break;
                    case 8:
                        distance = calcManahatanDistance(1, 2, node.X, node.Y);
                        break;
                    default:

                        break;
                } //End switch (nodeNum)
            } //End if (isValidNode(node))
            
            return distance;
        } //End public int getSingleManahatanDistance(Point node)

        public int getManhatanDistanceSum()
        {
            //Declare variables
            int distanceSum = -1;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    distanceSum += getSingleManahatanDistance(new Point(i, j));
                } //End for (int j = 0; j < 3; j++)
            } //End for (int i = 0; i < 3; i++)

            return distanceSum;
        } //End public in getManhatanDistanceSum()

        private bool isValidNode(Point node)
        {
            return node.X > -1 && node.X < 3 && node.Y > -1 && node.Y < 3;
        }
    } //End class Puzzle
} //End namespace CS4750HW2
