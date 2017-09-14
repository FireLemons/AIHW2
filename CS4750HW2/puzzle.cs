using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS4750HW2
{
    public enum Direction
    {
        NULL = -1,
        Up = 1,
        Down = 2,
        Left = 3,
        Right = 4
    };

    class Puzzle
    {
        private int[,] puzzle;
        private Point emptyPosition;
        private Point previous;
        private List<Direction> path;

        public Puzzle(int[,] puzzle)
        {
            this.puzzle = puzzle;
            this.path = new List<Direction>();
            this.emptyPosition = new Point();
            this.previous = new Point(-1, -1);

            for (int i = 0; i < puzzle.GetLength(0); i++)
            {
                for (int j = 0; j < puzzle.GetLength(1); j++)
                {
                    if (puzzle[i, j] == 0)
                    {
                        emptyPosition.X = i;
                        emptyPosition.Y = j;
                    }
                }
            }
        }
        
        /// <summary>
        ///     Gets the neighboring tiles as points excluding the parent.
        ///     Expansion function.
        /// </summary>
        /// <param name="origin">The tile moved from</param>
        /// <param name="tile">The tile to find children for</param>
        /// <returns>A list of all the children tiles of the tile as points</returns>
        public List<Point> getMovePositions()
        {
            List<Point> children = new List<Point>();
            if (!(isValidTile(emptyPosition)))
            {
                return null;
            }
            else
            {
                Point up = new Point(emptyPosition.X, emptyPosition.Y - 1);
                Point down = new Point(emptyPosition.X, emptyPosition.Y + 1);
                Point left = new Point(emptyPosition.X - 1, emptyPosition.Y);
                Point right = new Point(emptyPosition.X + 1, emptyPosition.Y);

                if (isValidTile(up) && !Point.Equals(up, previous))
                {
                    children.Add(up);
                }

                if (isValidTile(down) && !Point.Equals(down, previous))
                {
                    children.Add(down);
                }

                if (isValidTile(left) && !Point.Equals(left, previous))
                {
                    children.Add(left);
                }

                if (isValidTile(right) && !Point.Equals(right, previous))
                {
                    children.Add(right);
                }

                return children;
            }
        }

        public int getValue(Point p)
        {
            return puzzle[p.X, p.Y];
        }

        /// <summary>
        ///     Gets the state of the board if a move were to be applied.
        /// </summary>
        /// <param name="d">The direction to apply a move.</param>
        /// <returns>A 2d matrix representing the board.</returns>
        public int[,] getState(Direction d)
        {
            int[,] result = puzzle;
            int temp = puzzle[emptyPosition.X, emptyPosition.Y];

            switch (d)
            {
                case Direction.Up:
                    if (!isValidTile(new Point(emptyPosition.X, emptyPosition.Y - 1))) {
                        return null;
                    }
                    
                    puzzle[emptyPosition.X, emptyPosition.Y] = puzzle[emptyPosition.X, emptyPosition.Y - 1];
                    puzzle[emptyPosition.X, emptyPosition.Y - 1] = temp;

                    return result;
                case Direction.Down:
                    if (!isValidTile(new Point(emptyPosition.Y + 1, emptyPosition.X)))
                    {
                        return null;
                    }
                    
                    puzzle[emptyPosition.X, emptyPosition.Y] = puzzle[emptyPosition.X, emptyPosition.Y + 1];
                    puzzle[emptyPosition.X, emptyPosition.Y + 1] = temp;

                    return result;
                case Direction.Left:
                    if (!isValidTile(new Point(emptyPosition.X - 1, emptyPosition.Y)))
                    {
                        return null;
                    }
                    
                    puzzle[emptyPosition.X, emptyPosition.Y] = puzzle[emptyPosition.X - 1, emptyPosition.Y];
                    puzzle[emptyPosition.X - 1, emptyPosition.Y] = temp;

                    return result;
                case Direction.Right:
                    if (!isValidTile(new Point(emptyPosition.Y, emptyPosition.X + 1)))
                    {
                        return null;
                    }
                    
                    puzzle[emptyPosition.X, emptyPosition.Y] = puzzle[emptyPosition.X + 1, emptyPosition.Y];
                    puzzle[emptyPosition.X + 1, emptyPosition.Y] = temp;

                    return result;
                default:
                    return null;
            }
        }

        public bool setState(Direction d)
        {
            path.Add(d);
            previous = emptyPosition;
            int[,] tryMove = getState(d);

            if (tryMove != null)
            {
                puzzle = tryMove;
                switch (d)
                {
                    case Direction.Up:
                        emptyPosition.Y--;
                        break;
                    case Direction.Down:
                        emptyPosition.Y++;
                        break;
                    case Direction.Left:
                        emptyPosition.X--;
                        break;
                    case Direction.Right:
                        emptyPosition.X++;
                        break;
                    default:
                        return false;
                }
                return true;
            }

            return false;
        }

        public bool undo()
        {
            if (path.Count == 0)
            {
                throw new InvalidOperationException("Cannot undo move. No moves have been made yet.");
            }
            else
            {
                path.RemoveAt(path.Count - 1);
                setState(determineDirection(previous));
                path.RemoveAt(path.Count - 1);
                return true;
            }
        }

        public Direction determineDirection(Point nextTile)
        {
            //Declare variables
            Direction dir = Direction.NULL;
            int xDiff = 0;
            int yDiff = 0;
            Point emptyTile = this.getEmptyPosition();

            if (this.isAdjacent(nextTile, emptyTile))
            {
                xDiff = Math.Abs(nextTile.X - this.getEmptyPosition().X);
                yDiff = Math.Abs(nextTile.Y - this.getEmptyPosition().Y);

                if (xDiff == 0)
                {
                    if (nextTile.Y > emptyTile.Y)
                    {
                        dir = Direction.Down;
                    } //End if (nextTile.Y > emptyTile.Y)
                    else if (nextTile.Y < emptyTile.Y)
                    {
                        dir = Direction.Up;
                    } //End else if (nextTile.Y < emptyTile.Y)
                } //End if (xDiff == 0)
                else if (yDiff == 0)
                {
                    if (nextTile.X > emptyTile.X)
                    {
                        dir = Direction.Right;
                    } //End if (nextTile.X > emptyTile.X)
                    else if (nextTile.X < emptyTile.X)
                    {
                        dir = Direction.Left;
                    } //End else if (nextTile.X < emptyTile.X)
                } //End else if (yDiff == 0)
            } //End if (this.PuzzleBoard.isAdjacent(nextTile, emptyTile))

            return dir;
        } //End private Direction determineDirection(Point nextTile)

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
            int distanceSum = 0;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    distanceSum += getSingleManahatanDistance(new Point(j, i));
                } //End for (int j = 0; j < 3; j++)
            } //End for (int i = 0; i < 3; i++)

            return distanceSum;
        } //End public in getManhatanDistanceSum()

        /**********************************************************************************
        * Helper Methods
        **********************************************************************************/
        public Point getEmptyPosition()
        {
            return this.emptyPosition;
        } //End public Point getEmptyPostion()

        public Point getPreviousPosition()
        {
            return this.previous;
        } //End public Point getPReviousPosition()

        public int getTileID(Point tile)
        {
            //Declare variables
            int tileID = -1;

            if (isValidTile(tile))
            {
                tileID = this.puzzle[tile.X, tile.Y];
            } //End if (isValidTile(tile))

            return tileID;
        } //End public int getTileID(Point tile)

        public List<Direction> getPathList()
        {
            return this.path;
        } //End 

        public string printCurBoardState()
        {
            //Declare variables
            string returnString = "";

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    returnString += this.puzzle[j, i].ToString() + " ";
                } //End for (int j = 0; j < 3; j++)
                if (i < 2)
                {
                    returnString += "\n";
                } //End if (i < 2)
            } //End for (int i = 0; i < 3; i++)
            
            return returnString;
        } //End public string printCurBoardState()

        public Direction getReverseDirection(Direction direction)
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
        public bool isAdjacent(Point tile1, Point tile2)
        {
            return (Math.Abs(tile1.X - tile2.X) == 1 && tile1.Y == tile2.Y) || (Math.Abs(tile1.Y - tile2.Y) == 1 && tile1.X == tile2.X); 
        }

        public bool isInGoalState()
        {
            //Declare variables
            bool returnVal = false;

            if (getManhatanDistanceSum() == 0)
            {
                returnVal = true;
            } //End if (getManhatanDistanceSum() == 0)

            return returnVal;
        } //End public bool isInGoalState()

        private int calcManahatanDistance(Point targetTile, Point curTile)
        {
            return Math.Abs(targetTile.X - curTile.X) + Math.Abs(targetTile.Y - curTile.Y);
        } //End private int calcManahatanDistance(Point targetTile, Point curTile)

        public int[,] getPuzzleState()
        {
            return this.puzzle;
        } //End public int[,] getPuzzleState()
    } //End class Puzzle
} //End namespace CS4750HW2
