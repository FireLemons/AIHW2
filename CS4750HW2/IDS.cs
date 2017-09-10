using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS4750HW2
{
    class IDS
    {
        /***************ATTRIBUTES***************/
        //Properties
        

        //Fields
        public int[,] originalBoardState;
        public Puzzle PuzzleBoard { get; private set; }

        /***************CONSTRUCTOR***************/
        public IDS(int[,] puzzle)
        {
            this.originalBoardState = puzzle;
            this.PuzzleBoard = new Puzzle(puzzle);
        } //End public IDS(puzzle puzzle)

        /***************METHODS***************/
        public List<Direction> doTreeSearch()
        {
            //Declare variables
            int depth = 0;
            int numNodesExpanded = 0;
            List<Point> fringe = new List<Point>();

            //fringe = Insert(Make-Node(Initial-State[problem]), fringe)
            fringe = PuzzleBoard.getMovePositions();

            while (!PuzzleBoard.isInGoalState())
            {
                //if fringe is empty then return failure
                if (fringe.Count == 0)
                {
                    return null;
                } //End if (fringe.Count == 0)

                //node = Remove-Front(fringe)

                //if Goal-Test(problem,State(node)) then return node

                //fringe = InsertAll(Expand(node, problem), fringe)
            } //End while (!PuzzleBoard.isInGoalState())

            return this.PuzzleBoard.getPathList();
        } //End 

    } //End class IDS
} //End namespace CS4750HW2
