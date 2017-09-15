using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS4750HW2
{
    class DFS //Depth First Search
    {
        private Puzzle initialNode;
        private List<int[,]> closed;
        private Stack<Stack<Tuple<Puzzle, int>>> fringe;//I know it doesn't quite follow the psudeocode but it saves memory
        private Stack<int> path;
        private String output;
        private Stopwatch timer;

        public DFS(int[,] puzzleInfo)
        {
            timer = Stopwatch.StartNew();
            int[,] puzzleInfoCopy = puzzleInfo.Clone() as int[,];
            fringe = new Stack<Stack<Tuple<Puzzle, int>>>();
            closed = new List<int[,]>();
            path = new Stack<int>();

            initialNode = new Puzzle(puzzleInfo);//load initial state of problem
            if (initialNode == null)
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
            timer.Start();
            int nodesExpanded = 0;
            
            output += "\nInitial State:\n";
            output += initialNode.printCurBoardState();
            output += "\n\n";
            
            //initialize fringe
            Stack<Tuple<Puzzle, int>> initialState = new Stack<Tuple<Puzzle, int>>();
            initialState.Push(Tuple.Create(initialNode, 0));
            fringe.Push(initialState);

            Tuple<Puzzle, int> current;
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
                    fringe.Pop();
                    path.Pop();
                }
                else
                {
                    current = fringe.Peek().Pop();
                    //check for goal state
                    if (current.Item1.isInGoalState())
                    {
                        timer.Stop();
                        output += "GOAL FOUND\n\n";
                        output += "Nodes Expanded:" + nodesExpanded + "\n";
                        output += "Moves to solution" + (path.Count + 1) + "\n";
                        output += "Time elapsed: " + this.timer.ElapsedMilliseconds.ToString() + " ms\n";
                        output += "Path taken:";

                        foreach(int option in path.Cast<int>().ToList())
                        {
                            output += option + " ";
                        }
                        output += "\n";
                        return;
                    }

                    // if node is an new state
                    if(!isExplored(current.Item1.getPuzzleState()))
                    {
                        System.Diagnostics.Debug.WriteLine(current.Item1.printCurBoardState() + "\n");
                        nodesExpanded++;
                        // add the expansion of the node to the fringe
                        fringe.Push(sortPointsByValue(current.Item1.getMovePositions(), current.Item1));
                        // add node to closed and path
                        closed.Add(current.Item1.getPuzzleState());
                        path.Push(current.Item2);
                    }
                }
            }
        }

        /// <summary>
        ///     Sorts points by the value of their corresponding tile.
        /// </summary>
        /// <param name="unsortedList">The list of points to be sorted.</param>
        /// <returns>A list of points sorted by their corresponding tile value.</returns>
        public Stack<Tuple<Puzzle, int>> sortPointsByValue(List<Point> unsortedList, Puzzle node)
        {
            if (unsortedList == null || unsortedList.Count == 0)
            {
                return new Stack<Tuple<Puzzle, int>>();
            }
            else
            {
                //sort function made with the help of https://stackoverflow.com/a/4668558
                unsortedList.Sort((a, b) => 
                {
                    return node.getValue(b).CompareTo(node.getValue(a));
                });

                Stack<Tuple<Puzzle, int>> result = new Stack<Tuple<Puzzle, int>>();

                foreach (Point p in unsortedList)
                {
                    result.Push(Tuple.Create(new Puzzle(node.getPotentialState(node.determineDirection(p))), node.getTileID(p)));
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
