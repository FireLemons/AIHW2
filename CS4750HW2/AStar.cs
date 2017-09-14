using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS4750HW2
{
    class AStar
    {
        /***************ATTRIBUTES***************/
        //Properties
        public int[,] originalBoardState;

        //Fields
        public Puzzle PuzzleBoard { get; private set; }
        public List<int[,]> FirstFiveNodesExpanded { get; set; }
        private List<Point> Fringe { get; set; }
        private List<Node> OrderedFringe { get; set; }
        public int TotalNumNodesExpanded { get; set; }
        public List<int> PathTileIDs { get; set; }
        public List<Node> Path { get; set; }

        /***************CONSTRUCTOR***************/
        public AStar(int[,] puzzle)
        {
            originalBoardState = new int[3, 3];
            copyBoardState(puzzle);
            this.PuzzleBoard = new Puzzle(puzzle);
            this.Fringe = new List<Point>();
            this.OrderedFringe = new List<Node>();
            this.FirstFiveNodesExpanded = new List<int[,]>();
            this.TotalNumNodesExpanded = 0;
            this.PathTileIDs = new List<int>();
            this.Path = new List<Node>();
        } //End public IDS(puzzle puzzle)

        /***************METHODS***************/
        public List<Node> doTreeSearch()
        {
            //Declare variables
            int maxDepth = 0;
            int curDepth = 0;
            Direction nextMove = Direction.NULL;

            //fringe = Insert(Make-Node(Initial-State[problem]), fringe)
            //this.Fringe = PuzzleBoard.getMovePositions();
            this.FirstFiveNodesExpanded.Add(copyState(new int[3, 3], this.originalBoardState));
            //maxDepth += 1;
            this.TotalNumNodesExpanded += 1;

            while (!PuzzleBoard.isInGoalState())
            {
                //if fringe is empty then return failure
                //if ((curDepth >= maxDepth && this.OrderedFringe.Count <= 0) || (this.OrderedFringe.Count <= 0 && this.Fringe.Count <= 0))
                if (this.OrderedFringe.Count <= 0 && this.Fringe.Count <= 0 && TotalNumNodesExpanded > 1)
                {
                    return null;
                } //End if (curDepth >= maxDepth)

                //node = Remove-Front(fringe)
                /**
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
                    } //End if (curDepth > this.OrderedFringe[0].DepthWhenFound)
                } //End if (curDepth >= maxDepth)
                //*/

                if (this.OrderedFringe.Count > 0 && this.OrderedFringe[0].DepthWhenFound < curDepth - 1)
                {
                    this.PuzzleBoard = new Puzzle(copyState(new int[3, 3], this.OrderedFringe[0].state));
                    this.Path = new List<Node>(this.OrderedFringe[0].Path);
                } //End if (this.OrderedFringe.Count > 0 && this.OrderedFringe[0].DepthWhenFound < curDepth)

                this.Fringe.Clear();
                this.Fringe = PuzzleBoard.getMovePositions();
                this.TotalNumNodesExpanded += this.Fringe.Count;

                determineNextMove(curDepth);
                nextMove = this.PuzzleBoard.determineDirection(this.OrderedFringe[0].TileLocation);
                //nextMove = determineDirection(this.OrderedFringe[0].TileLocation);

                if (nextMove == Direction.NULL)
                {
                    return null;
                } //End  if (nextMove == Direction.NULL)

                this.PuzzleBoard.setState(nextMove);
                curDepth += 1;
                this.TotalNumNodesExpanded += 1;

                this.OrderedFringe.RemoveAt(0);

                if (this.FirstFiveNodesExpanded.Count < 5)
                {
                    this.FirstFiveNodesExpanded.Add(copyState(new int[3, 3], this.PuzzleBoard.getPuzzleState()));
                    //this.FirstFiveNodesExpanded.Add(this.PuzzleBoard.getTileID(this.PuzzleBoard.getPreviousPosition()));
                } //End if (numNodesExpanded < 5)

                this.Path.Add(new Node(this.PuzzleBoard.getPreviousPosition(), this.PuzzleBoard.getTileID(this.PuzzleBoard.getPreviousPosition()), curDepth - 1, this.originalBoardState, nextMove));

                //if Goal-Test(problem,State(node)) then return node
                if (this.PuzzleBoard.isInGoalState())
                {
                    break;
                } //End if (this.PuzzleBoard.isInGoalState())

                //fringe = InsertAll(Expand(node, problem), fringe)
                
                /**
                if (curDepth < maxDepth)
                {
                    this.Fringe.Clear();
                    this.Fringe = PuzzleBoard.getMovePositions();
                    this.TotalNumNodesExpanded += this.Fringe.Count;
                } //End if (curDepth < maxDepth)
                //*/

                if (this.TotalNumNodesExpanded >= 100000)
                {
                    return null;
                } //End if (this.TotalNumNodesExpanded >= 100000)
            } //End while (!PuzzleBoard.isInGoalState())

            return this.Path;
        } //End public List<Node> doTreeSearch()

        private void determineNextMove(int curDepth)
        {
            //Declare variables
            Point nextTile = new Point(-1, -1);
            int tileID = 9;

            while (this.Fringe.Count > 0)
            {
                tileID = 0;
                int manahattanDistance = 0;
                Direction nextMove = Direction.NULL;
                Node curNode = null;
                Node nodeConsidering = null;
                for (int i = 0; i < this.Fringe.Count; i++)
                {
                    nextMove = this.PuzzleBoard.determineDirection(this.Fringe[i]);

                    if (nextMove == Direction.NULL)
                    {
                        return;
                    } //End  if (nextMove == Direction.NULL)

                    this.PuzzleBoard.setState(nextMove);
                    manahattanDistance = this.PuzzleBoard.getManhatanDistanceSum();
                    curNode = new Node(this.Fringe[i], this.PuzzleBoard.getTileID(this.PuzzleBoard.getPreviousPosition()), curDepth, this.PuzzleBoard.getPuzzleState(), manahattanDistance, this.Path, nextMove);

                    this.PuzzleBoard.setState(this.PuzzleBoard.getReverseDirection(curNode.DirUsedToReachTile));

                    if ((i == 0 && nodeConsidering == null) || (curNode.ManahattanDistance > nodeConsidering.ManahattanDistance || (curNode.TileID > nodeConsidering.TileID && curNode.ManahattanDistance == nodeConsidering.ManahattanDistance)))
                    {
                        nodeConsidering = curNode;
                        nextTile = Fringe[i];
                    } //End if ((i == 0 && nodeConsidering == null) || (curNode.ManahattanDistance > nodeConsidering.ManahattanDistance || (curNode.TileID > nodeConsidering.TileID && curNode.ManahattanDistance == nodeConsidering.ManahattanDistance)))

                    /**
                    //get the current point
                    Point currPoint = this.Fringe[i];
                    //get the manhatan distance for this point
                    int distance = this.PuzzleBoard.getSingleManahatanDistance(currPoint) + curDepth;
                    //if this distance is lower than the current lowest, then it is now the lowest
                    if (distance < lowestManhattanDistance)
                    {
                        tileID = this.PuzzleBoard.getTileID(currPoint);
                        nextTile = currPoint;
                        lowestManhattanDistance = distance;
                    }
                    //*/
                } //End for (int i = 0; i < this.Fringe.Count; i++)

                for (int i = 0; i < this.OrderedFringe.Count; i++)
                {
                    if (this.OrderedFringe[i].Cost > nodeConsidering.Cost || (this.OrderedFringe[i].TileID > nodeConsidering.TileID && this.OrderedFringe[i].Cost == nodeConsidering.Cost))
                    {
                        this.OrderedFringe.Insert(i, nodeConsidering);
                        break;
                    } //End if (this.OrderedFringe[i].Cost > nodeConsidering.Cost && this.OrderedFringe[i].TileID > nodeConsidering.TileID)

                    if (i == this.OrderedFringe.Count - 1)
                    {
                        this.OrderedFringe.Add(nodeConsidering);
                        break;
                    } //End if (i == this.OrderedFringe.Count - 1)
                } //End for (int i = 0; i < this.OrderedFringe.Count; i++)

                if (this.OrderedFringe.Count == 0)
                {
                    this.OrderedFringe.Add(nodeConsidering);
                } //End if (this.OrderedFringe.Count == 0)

                //this.OrderedFringe.Insert(0, nodeConsidering);
                //this.OrderedFringe.Insert(0, new Node(nextTile, this.PuzzleBoard.getTileID(nextTile), curDepth, this.PuzzleBoard.getPuzzleState()));

                this.Fringe.Remove(nextTile);
            } //End while (this.Fringe.Count > 0)
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

        private void resetPuzzleBoard()
        {
            int[,] puzzle = new int[3, 3];

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    puzzle[j, i] = this.originalBoardState[j, i];
                } //End for (int j = 0; j < 3; j++)
            } //End for (int i = 0; i < 3; i++)

            this.PuzzleBoard = new Puzzle(puzzle);
        } //End private void resetPuzzleBoard()

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

        private int[,] copyState(int[,] state, int[,] puzzle)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    state[j, i] = puzzle[j, i];
                } //End for (int j = 0; j < 3; j++)
            } //End for (int i = 0; i < 3; i++)

            return state;
        } //End private void copyState(int[,] puzzle)

        public string reportFirstFiveNodesExpanded()
        {
            //Declare variables
            string returnString = "";

            for (int i = 0; i < this.FirstFiveNodesExpanded.Count; i++)
            {
                returnString += this.FirstFiveNodesExpanded[i].ToString();

                if (i < this.FirstFiveNodesExpanded.Count - 1)
                {
                    returnString += ", ";
                } //End if  (i < this.FirstFiveNodesExpanded.Count - 1)
            } //End for (int i = 0; i < this.FirstFiveNodesExpanded.Count; i++)

            return returnString;
        } //End public string reportFirstFiveNodesExpanded()

        public string reportPathSolution()
        {
            //Declare variables
            string returnString = "";

            for (int i = 0; i < this.Path.Count; i++)
            {
                returnString += this.Path[i].DirUsedToReachTile.ToString() + ":" + this.Path[i].TileID.ToString();

                if (i < this.Path.Count - 1)
                {
                    returnString += " -> ";
                } //End if (i < this.Path.Count - 1)
            } //End for (int i = 0; i < this.Path.Count; i++)

            returnString += "\nNumber of moves: " + this.Path.Count;

            return returnString;
        } //End public string reportPathSolution()
    } //End class IDS
} //End namespace CS4750HW2
