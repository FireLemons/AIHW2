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
        public enum Direction
        {
            NULL = -1,
            Up = 1,
            Down = 2,
            Left = 3,
            Right = 4
        };
        private int[,] puzzle;
        private Point emptyPosition;
        private List<Direction> path;

        public Puzzle(int[,] puzzle)
        {
            this.puzzle = puzzle;
            this.path = new List<Direction>();

            for (int i = 0; i < puzzle.Length; i++)
            {
                for (int j = 0; j < puzzle.Length; j++)
                {
                    if (puzzle[i, j] == 0)
                    {
                        emptyPosition.X = j;
                        emptyPosition.Y = i;
                    }
                }
            }
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
            if (!(isValidTile(parent) && isValidTile(tile)) || !isAdjacent(parent, tile))
            {
                return null;
            }
            else
            {
                Point up = new Point(tile.X,tile.Y - 1);
                Point down = new Point(tile.X,tile.Y + 1);
                Point left = new Point(tile.X - 1, tile.Y);
                Point right = new Point(tile.X + 1, tile.Y);

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

        public int[,] getState(Direction d)
        {
            int[,] result = puzzle;
            int temp = puzzle[emptyPosition.Y, emptyPosition.X];

            switch (d)
            {
                case Direction.Up:
                    if (!isValidTile(new Point(emptyPosition.X, emptyPosition.Y - 1))) {
                        return null;
                    }
                    
                    puzzle[emptyPosition.Y, emptyPosition.X] = puzzle[emptyPosition.Y - 1, emptyPosition.X];
                    puzzle[emptyPosition.Y - 1, emptyPosition.X] = temp;

                    return result;
                case Direction.Down:
                    if (!isValidTile(new Point(emptyPosition.X, emptyPosition.Y + 1)))
                    {
                        return null;
                    }
                    
                    puzzle[emptyPosition.Y, emptyPosition.X] = puzzle[emptyPosition.Y + 1, emptyPosition.X];
                    puzzle[emptyPosition.Y + 1, emptyPosition.X] = temp;

                    return result;
                case Direction.Left:
                    if (!isValidTile(new Point(emptyPosition.X - 1, emptyPosition.Y)))
                    {
                        return null;
                    }
                    
                    puzzle[emptyPosition.Y, emptyPosition.X] = puzzle[emptyPosition.Y, emptyPosition.X - 1];
                    puzzle[emptyPosition.Y, emptyPosition.X - 1] = temp;

                    return result;
                case Direction.Right:
                    if (!isValidTile(new Point(emptyPosition.X + 1, emptyPosition.Y)))
                    {
                        return null;
                    }
                    
                    puzzle[emptyPosition.Y, emptyPosition.X] = puzzle[emptyPosition.Y, emptyPosition.X + 1];
                    puzzle[emptyPosition.Y, emptyPosition.X + 1] = temp;

                    return result;
                default:
                    return null;
            }
        }

        public bool setState(Direction d)
        {
            return (puzzle = getState(d)) != null;
        }

        public int getSingleManahatanDistance(Point tile)
        {
            //Declare variables
            int distance = -1;
            int tileNum = -1;
            
            if (isValidTile(tile))
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
                        distance = calcManahatanDistance(new Point(1, 0), tile);
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
                } //End switch (nodeNum)
            } //End if (isValidTile(node))
            
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
        private Direction getReverseDirection(Direction direction)
        {
            //Declare variables
            Direction reverseDirection  = Direction.NULL;

            if (isValidDirection(direction))
            {
                switch (direction)
                {
                    case Direction.Up:
                        reverseDirection = Direction.Down;
                        break;
                    case Direction.Down:
                        reverseDirection = Direction.Up;
                        break;
                    case Direction.Left:
                        reverseDirection = Direction.Right;
                        break;
                    case Direction.Right:
                        reverseDirection = Direction.Left;
                        break;
                    default:
                        reverseDirection = Direction.NULL;
                        break;
                } //End switch (direction)
            } //End if (isValidDirection(direction))

                return reverseDirection;
        } //End private Direction getReverseDirection(Direction direction)

        private bool isValidDirection(Direction direction)
        {
            //Declare variables
            bool returnVal = false;

            if ((int)direction >= 1 && (int)direction <= 4)
            {
                returnVal = true;
            } //End if ((int)direction >= 1 && (int)direction <= 4)

            return returnVal;
        } //End private bool isValidDirection(Direction direction)

        /// <summary>
        ///     Checks whether a tile is out of bounds
        /// </summary>
        /// <param name="tile">The position of the tile to be inspected</param>
        /// <returns>true if tile is in bounds false otherwise</returns>
        private bool isValidTile(Point tile)
        {
            return tile.X > -1 && tile.X < 3 && tile.Y > -1 && tile.Y < 3;
        }
        
        /// <summary>
        ///     Checks whether two tiles are adjacent
        /// </summary>
        /// <param name="tile1">The first tile to be checked</param>
        /// <param name="tile2">The second tile to be checked</param>
        /// <returns>true if the tiles are adjacent false otherwise</returns>
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

        private int calcManahatanDistance(Point targetTile, Point curTile)
        {
            return Math.Abs(targetTile.X - curTile.X) + Math.Abs(targetTile.Y - curTile.Y);
        } //End private int calcManahatanDistance(Point targetTile, Point curTile)
    } //End class Puzzle
} //End namespace CS4750HW2
