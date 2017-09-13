﻿using System;
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
        private Stack<Direction> fringe;
        private String output;

        public DFS(Puzzle puzzle){
            this.puzzle = puzzle;//load initial state of problem
            if (this.puzzle == null)
            {
                throw new NullReferenceException();
            }
        }

        public void performDepthFirstGraphSearch()
        {
            int nodesExpanded = 0;

            if (puzzle.isInGoalState())
            {
                output += puzzle.printCurBoardState();
            }
            else
            {
                output += puzzle.printCurBoardState();
                output += sortPointsByValue(puzzle.getMovePositions()).ToString();
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

        public String getResult()
        {
            return output;
        }
    }
}
