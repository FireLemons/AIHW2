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

        private int calcManahatanDistance(Point targetTile, Point curTile)
        {
            return Math.Abs(targetTile.X - curTile.X) + Math.Abs(targetTile.Y - curTile.Y);
        } //End private int calcManahatanDistance(Point targetTile, Point curTile)

        public int getSingleManahatanDistance(Point tile)
        {
            //Declare variables
            int distance = -1;
            int tileNum = -1;

            if (isValidNode(tile))
            {
                tileNum = puzzle[tile.X, tile.Y];

                switch (tileNum)
                {
                    case 0:
                        distance = calcManahatanDistance(new Point(2, 2), tile);
                        break;
                    case 1:
                        distance = calcManahatanDistance(new Point(0, 0), tile);
                        break;
                    case 2:
                        distance = calcManahatanDistance(new Point(1, 0), tile;
                        break;
                    case 3:
                        distance = calcManahatanDistance(new Point(2, 0), tile);
                        break;
                    case 4:
                        distance = calcManahatanDistance(new Point(0, 1), tile);
                        break;
                    case 5:
                        distance = calcManahatanDistance(new Point(1, 1), tile);
                        break;
                    case 6:
                        distance = calcManahatanDistance(new Point(2, 1), tile);
                        break;
                    case 7:
                        distance = calcManahatanDistance(new Point(0, 2), tile);
                        break;
                    case 8:
                        distance = calcManahatanDistance(new Point(1, 2), tile);
                        break;
                    default:

                        break;
                } //End switch (tileNum)
            } //End if (isValidNode(tile))

            return distance;
        } //End public int getSingleManahatanDistance(Point tile)

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

        public bool isInGoalState(int[,] curBoardState)
        {
            //Declare variables
            bool returnVal = false;

            if (getManhatanDistanceSum() == 0)
            {
                returnVal = true;
            } //End if (getManhatanDistanceSum() == 0)

            return returnVal;
        } //End public bool isInGoalState(int[,] curBoardState)
    } //End class Puzzle
} //End namespace CS4750HW2
