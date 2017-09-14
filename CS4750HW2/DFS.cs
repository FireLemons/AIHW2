using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS4750HW2
{
    class DFS 
    {
        private Puzzle puzzle;
        private List<int[,]> closed;
        private Stack<Point> fringe;//I know it doesn't follow the psudeocode but it saves memory
        private String output;

        public DFS(int[,] puzzle)
        {

            fringe = new Stack<Point>();
            closed = new List<int[,]>();

            this.puzzle = new Puzzle(puzzle);//load initial state of problem
            if (this.puzzle == null)
            {
                throw new NullReferenceException("Cannot initialize DFS with");
            }

            output = "***********************************\n" +
                     "*        Depth First Search       *\n" +
                     "***********************************\n";
        }

        /// <summary>
        ///     Performs depth first search
        /// </summary>
        public void performDepthFirstGraphSearch()
        {
            int nodesExpanded = 0;

            if (puzzle.isInGoalState())
            {
                output += "\nInitial State:\n";
                output += puzzle.printCurBoardState();
                output += "\nGoal Found";
            }
            else
            {
                output += "\nInitial State:\n";
                output += puzzle.printCurBoardState();
                output += "\n\n";

                List<Point> sorted = sortPointsByValue(puzzle.getMovePositions());

                foreach (Point p in sorted)
                {
                    fringe.Push(p);
                }

                Point current;
                while (true)
                {
                    try
                    {
                        current = fringe.Pop();
                    }
                    catch (InvalidOperationException ex)
                    {
                        output += "Could not find goal state.";
                        return;
                    }

                    int[,] state = puzzle.getState(puzzle.determineDirection(current));
                    output += isExplored(state) + "\n\n";

                    if (!isExplored(state)){
                        closed.Add(state);
                    }
                }
            }
        }

        /// <summary>
        ///     Sorts points by the value of their corresponding tile.
        /// </summary>
        /// <param name="unsortedList">The list of points to be sorted.</param>
        /// <returns>A list of points sorted by their corresponding tile value.</returns>
        public List<Point> sortPointsByValue(List<Point> unsortedList)
        {
            if (unsortedList == null || unsortedList.Count == 0)
            {
                return new List<Point>();
            }
            else
            {
                //sort function made with the help of https://stackoverflow.com/a/4668558
                unsortedList.Sort((a, b) => 
                {
                    return puzzle.getValue(a).CompareTo(puzzle.getValue(b));
                });
                return unsortedList;
            }
        }

        /// <summary>
        ///     Determines whether a state has been expanded or not
        ///     Created with the help of https://stackoverflow.com/a/9854944
        /// </summary>
        /// <returns>True if the state has been seen already, false otherwise</returns>
        private Boolean isExplored(int[,] state)
        {
            return closed.Find(x => isEqual(x, state)) != null;
        }

        /// <summary>
        ///     Compares two 2D arrays
        ///     copied from https://stackoverflow.com/a/12446807
        /// </summary>
        /// <param name="a">The first 2D array to be compared</param>
        /// <param name="b">The second 2D array to be compared</param>
        /// <returns>True if all of the elements are the same, false otherwise</returns>
        private Boolean isEqual(int[,] a, int[,] b)
        {
            if (!(a.GetLength(0) == b.GetLength(0) && a.GetLength(1) == b.GetLength(1)))
            {
                return false;
            }

            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    if (a[i, j] != b[i, j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        ///     Get a log of what happened
        /// </summary>
        /// <returns>A string describing the result of depth first search.</returns>
        public String getResult()
        {
            return output;
        }
    }
}
