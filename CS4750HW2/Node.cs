using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS4750HW2
{
    class Node
    {
        /***************ATTRIBUTES***************/
        //Properties

        //Fields
        public Point TileLocation { get; private set; }
        public int TileID { get; private set; }
        public int DepthWhenFound { get; private set; }
        public List<Node> PossibleMoves { get; private set; }
        public Direction DirUsedToReachTile { get; private set; }
        public int[,] state { get; private set; }
        public int ManahattanDistance { get; private set; }
        public int Cost
        {
            get
            {
                return this.DepthWhenFound + 1 + ManahattanDistance;
            } //End get
        } //End public int Cost
        public List<Node> Path { get; private set; }

        /***************CONSTRUCTOR***************/
        public Node(Point location, int id, int depth, int[,] puzzle, Direction dir = Direction.NULL)
        {
            this.TileLocation = location;
            this.TileID = id;
            this.DepthWhenFound = depth;
            this.DirUsedToReachTile = dir;
            this.state = new int[3, 3];
            copyBoardState(puzzle);
        } //End public IDS(puzzle puzzle)

        public Node(Point location, int id, int depth, int[,] puzzle, int manahattanDist, List<Node> pathTaken, Direction dir = Direction.NULL)
        {
            this.TileLocation = location;
            this.TileID = id;
            this.DepthWhenFound = depth;
            this.DirUsedToReachTile = dir;
            this.state = new int[3, 3];
            copyBoardState(puzzle);
            this.ManahattanDistance = manahattanDist;
            this.Path = new List<Node>(pathTaken);
        } //End public IDS(puzzle puzzle)

        /***************METHODS***************/
        public void setPossibleMoves(List<Node> moves)
        {
            this.PossibleMoves = moves;
        } //End 

        private void copyBoardState(int[,] puzzle)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    this.state[j, i] = puzzle[j, i];
                } //End for (int j = 0; j < 3; j++)
            } //End for (int i = 0; i < 3; i++)
        } //End private void copyBoardState(int[,] puzzle)

    } //End class Node
} //End namespace CS4750
