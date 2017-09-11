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
        public int[,] originalBoardState;

        //Fields
        public Puzzle PuzzleBoard { get; private set; }
        public List<int> firstFiveNodesExpanded { get; set; }
        private List<Point> Fringe { get; set; }
        private List<Node> OrderedFringe { get; set; }
        public int TotalNumNodesExpanded { get; set; }
        public List<int> PathTileIDs { get; set; }
        public List<Node> Path { get; set; }

        /***************CONSTRUCTOR***************/
        public IDS(int[,] puzzle)
        {
            originalBoardState = new int[3,3];
            copyBoardState(puzzle);
            this.PuzzleBoard = new Puzzle(puzzle);
            this.Fringe = new List<Point>();
            this.OrderedFringe = new List<Node>();
            this.firstFiveNodesExpanded = new List<int>();
            this.PathTileIDs = new List<int>();
            this.Path = new List<Node>();
        } //End public IDS(puzzle puzzle)

        /***************METHODS***************/
        public List<Direction> doTreeSearch()
        {
            //Declare variables
            int maxDepth = 0;
            int curDepth = 0;
            int numNodesExpanded = 0;
            Direction nextMove = Direction.NULL;

            //fringe = Insert(Make-Node(Initial-State[problem]), fringe)
            this.Fringe = PuzzleBoard.getMovePositions();
            maxDepth += 1;

            while (!PuzzleBoard.isInGoalState())
            {
                //if fringe is empty then return failure
                /*
                if (this.Fringe.Count <= 0 && this.OrderedFringe.Count <= 0)
                {
                    return null;
                } //End if (this.Fringe.Count <= 0 && this.OrderedFringe.Count <= 0)
                //*/

                //node = Remove-Front(fringe)
                if (curDepth >= maxDepth && this.OrderedFringe.Count <= 0)
                {
                    maxDepth += 1;
                    curDepth = 0;
                    this.PuzzleBoard = new Puzzle(this.originalBoardState);
                    this.Fringe.Clear();
                    this.Fringe = PuzzleBoard.getMovePositions();
                    this.Path.Clear();
                    continue;
                } //End if (curDepth >= maxDepth)

                if (curDepth >= maxDepth)
                {
                    if (curDepth > this.OrderedFringe[0].DepthWhenFound)
                    {
                        while (curDepth > this.OrderedFringe[0].DepthWhenFound)
                        {
                            curDepth -= 1;
                            this.PuzzleBoard.setState(this.PuzzleBoard.getReverseDirection(this.Path.Last().DirUsedToReachTile));
                            this.Path.Remove(this.Path.Last());
                        } //End while (curDepth > this.OrderedFringe[0].DepthWhenFound)

                        nextMove = determineDirection(this.OrderedFringe[0].TileLocation);

                        if (nextMove == Direction.NULL)
                        {
                            return null;
                        } //End  if (nextMove == Direction.NULL)

                        this.PuzzleBoard.setState(nextMove);
                        curDepth += 1;

                        this.OrderedFringe.RemoveAt(0);

                        if (this.firstFiveNodesExpanded.Count < 5)
                        {
                            this.firstFiveNodesExpanded.Add(this.PuzzleBoard.getTileID(this.PuzzleBoard.getPreviousPosition()));
                        } //End if (numNodesExpanded < 5)

                        this.Path.Add(new Node(this.PuzzleBoard.getPreviousPosition(), this.PuzzleBoard.getTileID(this.PuzzleBoard.getPreviousPosition()), curDepth - 1, nextMove));

                        //if Goal-Test(problem,State(node)) then return node
                        if (this.PuzzleBoard.isInGoalState())
                        {
                            break;
                        } //End if (this.PuzzleBoard.isInGoalState())
                    } //End if (curDepth > this.OrderedFringe[0].DepthWhenFound)
                } //End 

                if (curDepth < maxDepth)
                {
                    nextMove = determineNextMove(curDepth);

                    if (nextMove == Direction.NULL)
                    {
                        return null;
                    } //End  if (nextMove == Direction.NULL)

                    this.PuzzleBoard.setState(nextMove);
                    curDepth += 1;

                    this.OrderedFringe.RemoveAt(0);

                    if (this.firstFiveNodesExpanded.Count < 5)
                    {
                        this.firstFiveNodesExpanded.Add(this.PuzzleBoard.getTileID(this.PuzzleBoard.getPreviousPosition()));
                    } //End if (numNodesExpanded < 5)

                    //this.PathTileIDs.Add(this.PuzzleBoard.getTileID(this.PuzzleBoard.getPreviousPosition()));
                    this.Path.Add(new Node(this.PuzzleBoard.getPreviousPosition(), this.PuzzleBoard.getTileID(this.PuzzleBoard.getPreviousPosition()), curDepth - 1, nextMove));
                } //End if (curDepth < maxDepth)



                //if Goal-Test(problem,State(node)) then return node
                if (this.PuzzleBoard.isInGoalState())
                {
                    break;
                } //End if (this.PuzzleBoard.isInGoalState())

                if (this.OrderedFringe.Count <= 0)
                {
                    maxDepth += 1;
                    curDepth = 0;
                    this.PuzzleBoard = new Puzzle(this.originalBoardState);
                    this.Fringe.Clear();
                    this.Fringe = PuzzleBoard.getMovePositions();
                    continue;
                } //End if (curDepth >= maxDepth)

                //fringe = InsertAll(Expand(node, problem), fringe)
                if (curDepth < maxDepth)
                {
                    this.Fringe.Clear();
                    this.Fringe = PuzzleBoard.getMovePositions();
                } //End if (curDepth < maxDepth)
            } //End while (!PuzzleBoard.isInGoalState())

            return this.PuzzleBoard.getPathList();
        } //End 

        private Direction determineNextMove(int curDepth)
        {
            //Declare variables
            Point nextTile = new Point(-1, -1);
            int tileID = 9;
            Direction dir = Direction.NULL;

            while (this.Fringe.Count > 0)
            {
                tileID = 9;
                for (int i = 0; i < this.Fringe.Count; i++)
                {
                    if (this.PuzzleBoard.getTileID(Fringe[i]) < tileID)
                    {
                        tileID = this.PuzzleBoard.getTileID(Fringe[i]);
                        nextTile = Fringe[i];
                    } //End if (this.PuzzleBoard.getTileID(Fringe[i]) > tileID)
                } //End for (int i = 0; i < this.Fringe.Count; i++)

                this.OrderedFringe.Insert(0, new Node(nextTile, this.PuzzleBoard.getTileID(nextTile), curDepth));

                this.Fringe.Remove(nextTile);
            } //End while (this.Fringe.Count > )
            
            dir = determineDirection(this.OrderedFringe[0].TileLocation);

            return dir;
        } //End private Direction determineNextMove()

        private Direction determineDirection(Point nextTile)
        {
            //Declare variables
            Direction dir = Direction.NULL;
            int xDiff = 0;
            int yDiff = 0;
            Point emptyTile = this.PuzzleBoard.getEmptyPosition();

            if (this.PuzzleBoard.isAdjacent(nextTile, emptyTile))
            {
                xDiff = Math.Abs(nextTile.X - this.PuzzleBoard.getEmptyPosition().X);
                yDiff = Math.Abs(nextTile.Y - this.PuzzleBoard.getEmptyPosition().Y);

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

        private void copyBoardState(int[,] puzzle)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    this.originalBoardState[j, i] = puzzle[j, i];
                } //End for (int j = 0; j < 3; j++)
            } //End for (int i = 0; i < 3; i++)
        } //End private void copyBoardState(int[,] puzzle)
    } //End class IDS
} //End namespace CS4750HW2
