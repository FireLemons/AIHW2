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
        /// <returns>A list of all the children tiles of the tile as points</returns>
        public List<Point> getChildren(Point parent, Point tile)
        {
            List<Point> children = new List<Point>();
            if (!(isValidTile(parent) && isValidTile(tile)))
            {
                return null;
            }
            else
            {
                Point up = new Point(tile.X - 1,tile.Y);
                Point down = new Point(tile.X + 1,tile.Y);
                Point left = new Point(tile.X, tile.Y - 1);
                Point right = new Point(tile.X, tile.Y + 1);

                if (isValidTile(up) && !Point.Equals(up, parent))
                {
                    children.Add(up);
                }

                if (isValidTile(down) && !Point.Equals(down, parent))
                {
                    children.Add(down);
                }

                if (isValidTile(left) && !Point.Equals(left, parent))
                {
                    children.Add(left);
                }

                if (isValidTile(right) && !Point.Equals(right, parent))
                {
                    children.Add(right);
                }
                return children;
            }
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

            if (isValidTile(node))
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
            } //End if (isValidTile(node))
            
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

        /**********************************************************************************
         * Helper Methods
         **********************************************************************************/
        
        /// <summary>
        /// Checks whether a tile is out of bounds
        /// </summary>
        /// <param name="tile"></param>
        /// <returns>true if tile is in bounds false otherwise</returns>
        private bool isValidTile(Point tile)
        {
            return tile.X > -1 && tile.X < 3 && tile.Y > -1 && tile.Y < 3;
        }
        
        private bool isAdjacent(Point tile1, Point tile2)
        {
            return Math.Abs(tile1.X - tile2.X) == 1 || Math.Abs(tile1.Y - tile2.Y) == 1; 
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
