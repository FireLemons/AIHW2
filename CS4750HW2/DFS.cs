using System;
using System.Collections.Generic;
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

        public DFS(Puzzle puzzle){
            this.puzzle = puzzle;//load initial state of problem
        }

        public void performDepthFirstGraphSearch()
        {
            if (puzzle.isInGoalState()) {

            }
        }
    }
}
