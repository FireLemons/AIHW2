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
        private Stack<Stack<Puzzle>> fringe;//I know it doesn't quite follow the psudeocode but it saves memory
        private Stack<int> path;
        private String output;

        public DFS(int[,] puzzle)
        {

            fringe = new Stack<Stack<Puzzle>>();
            closed = new List<int[,]>();
            path = new Stack<int>();

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
            
            output += "\nInitial State:\n";
            output += puzzle.printCurBoardState();
            output += "\n\n";
            
            //initialize fringe
            Stack<Puzzle> initialState = new Stack<Puzzle>();
            initialState.Push(puzzle);
            fringe.Push(initialState);

            Puzzle current;
            while (true)
            {
                if (fringe.Count == 0 || nodesExpanded >= 100000)
                {
                    output += "Failure. Could not find goal state";
                    return;
                }
                // if Top of stack is empty 
                if (fringe.Peek().Count == 0)
                {
                    //remove the empty entry from the top of the stack
                    //remove the invalid node from the path
                }
                else
                {
                    current = fringe.Peek().Pop();
                    //      if node is an new state
                    if(!isExplored(current.getPuzzleState()))
                    {
                        // add the expansion of the node to the fringe
                        fringe.Push(sortPointsByValue(puzzle.getMovePositions()));
                        // add node to closed and path
                        
                    }
                }

                break;
            }
        }

        /// <summary>
        ///     Sorts points by the value of their corresponding tile.
        /// </summary>
        /// <param name="unsortedList">The list of points to be sorted.</param>
        /// <returns>A list of points sorted by their corresponding tile value.</returns>
        public Stack<Puzzle> sortPointsByValue(List<Point> unsortedList)
        {
            if (unsortedList == null || unsortedList.Count == 0)
            {
                return new Stack<Puzzle>();
            }
            else
            {
                //sort function made with the help of https://stackoverflow.com/a/4668558
                unsortedList.Sort((a, b) => 
                {
                    return puzzle.getValue(b).CompareTo(puzzle.getValue(a));
                });

                Stack<Puzzle> result = new Stack<Puzzle>();

                foreach (Point p in unsortedList)
                {
                    result.Push(new Puzzle(puzzle.getPotentialState(puzzle.determineDirection(p))));
                }
                
                return result;
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
