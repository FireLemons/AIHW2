using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CS4750HW2
{
    public partial class Form1 : Form
    {
        /***************ATTRIBUTES***************/
        //Properties
        Stopwatch timer;
        private int[,] puzzle1 = {
            { 0, 4, 7 },
            { 1, 2, 8 },
            { 3, 5, 6 } };
        private int[,] puzzle2 = {
            { 0, 1, 4 },
            { 5, 8, 7 },
            { 2, 3, 6 } };
        private int[,] puzzle3 = {
            { 8, 2, 3 },
            { 6, 5, 0 },
            { 7, 4, 1 } };
        private int[,] puzzleGoal = {
            { 1, 4, 7 },
            { 2, 5, 8 },
            { 3, 6, 0 } };
        
        //Fields
        int[,] Puzzle1
        {
            get
            {
                return puzzle1;
            } //End get
        } //End int[,] Puzzle1
        int[,] Puzzle2
        {
            get
            {
                return puzzle2;
            } //End get
        } //End int[,] Puzzle2
        int[,] Puzzle3
        {
            get
            {
                return puzzle3;
            } //End get
        } //End int[,] Puzzle3
        int[,] PuzzleGoal
        {
            get
            {
                return puzzleGoal;
            } //End get
        } //End int[,] PuzzleGoal


        /***************CONSTRUCTOR***************/
        public Form1()
        {
            InitializeComponent();
        } //End public Form1()

        /***************METHODS***************/
        public void reset()
        {
            this.rtxtResults.Clear();
        } //End public void reset()
        public void displayData(string data)
        {
            this.rtxtResults.Text += data + "\n";
        } //End public void displayData(string data)
        public void displayMillisecondsElapsed()
        {
            timer.Stop();
            this.displayData("Time elapsed: " + this.timer.ElapsedMilliseconds.ToString());
        } //End public void displayMillisecondsElapsed()

        /***************EVENTS***************/
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnIDS_Click(object sender, EventArgs e)
        {
            //Puzzle puzzle = new Puzzle(Puzzle3);
            //puzzle.setState(Puzzle.Direction.Left);
            //displayData(puzzle.getMovePositions().Count.ToString());
            this.displayData("Iterative Depth Search\n");

            IDS ids1 = new IDS(Puzzle1);
            this.timer = Stopwatch.StartNew();
            var x = ids1.doTreeSearch();
            this.timer.Stop();
            displayData("Puzzle1:\n");
            displayData(ids1.reportFirstFiveNodesExpanded());

            if (x != null)
            {
                this.displayData("Success, a solution was found.");
                displayData(ids1.reportPathSolution());
            } //End if (x != null)
            else
            {
                this.displayData("Failure, a solution was not found.");
            } //End else

            displayData("Number of nodes expanded: " + ids1.TotalNumNodesExpanded.ToString());
            displayData("Time elapsed: " + this.timer.ElapsedMilliseconds.ToString() + " milliseconds");

            IDS ids2 = new IDS(Puzzle2);
            this.timer = Stopwatch.StartNew();
            var y = ids2.doTreeSearch();
            this.timer.Stop();
            displayData("\nPuzzle2:\n");
            displayData(ids2.reportFirstFiveNodesExpanded());

            if (y != null)
            {
                this.displayData("Success, a solution was found.");
                displayData(ids2.reportPathSolution());
            } //End if (y =! null)
            else
            {
                this.displayData("Failure, a solution was not found.");
            } //End else

            displayData("Number of nodes expanded: " + ids2.TotalNumNodesExpanded.ToString());
            displayData("Time elapsed: " + this.timer.ElapsedMilliseconds.ToString() + " milliseconds");

            IDS ids3 = new IDS(Puzzle3);
            this.timer = Stopwatch.StartNew();
            var z = ids3.doTreeSearch();
            this.timer.Stop();
            displayData("\nPuzzle3:\n");
            displayData(ids3.reportFirstFiveNodesExpanded());

            if (z != null)
            {
                this.displayData("Success, a solution was found.");
                displayData(ids3.reportPathSolution());
            } //End if (z != null)
            else
            {
                this.displayData("Failure, a solution was not found.");
            } //End else

            displayData("Number of nodes expanded: " + ids3.TotalNumNodesExpanded.ToString());
            displayData("Time elapsed: " + this.timer.ElapsedMilliseconds.ToString() + " milliseconds");

        } //End private void btnIDS_Click(object sender, EventArgs e)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDFS_Click(object sender, EventArgs e)
        {
            DFS DFS = new DFS(Puzzle3);
            DFS.performDepthFirstGraphSearch();
            this.rtxtResults.Text = DFS.getResult();
        } //End private void btnDFS_Click(object sender, EventArgs e)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAStar_Click(object sender, EventArgs e)
        {
            this.displayData("A* Search\n");

            List<int[,]> puzzles = new List<int[,]> { puzzle1, puzzle2, puzzle3 };

            for(int i = 0; i < puzzles.Count; i++)
            {
                int[,] puzzle = puzzles[i];
                AStar astar1 = new AStar(puzzle);
                this.timer = Stopwatch.StartNew();
                var x = AStar.doTreeSearch();
                this.timer.Stop();
                displayData("Puzzle" + (i+1) + ":\n");
                displayData(astar1.reportFirstFiveNodesExpanded());

                if (x != null)
                {
                    this.displayData("Success, a solution was found.");
                    displayData(astar1.reportPathSolution());
                } //End if (x != null)
                else
                {
                    this.displayData("Failure, a solution was not found.");
                } //End else

                displayData("Number of nodes expanded: " + astar1.TotalNumNodesExpanded.ToString());
                displayData("Time elapsed: " + this.timer.ElapsedMilliseconds.ToString() + " milliseconds");
            }
            
        } //End private void btnAStar_Click(object sender, EventArgs e)
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAll_Click(object sender, EventArgs e)
        {
            ///This is for when we finish the other three, makes it super easy.
            /*
            this.btnIDS.PerformClick();
            this.btnDFS.PerformClick();
            this.btnAStar.PerformClick();
            */
            this.rtxtResults.Text = "All: We don't do anything yet\n";
        } //End private void btnAll_Click(object sender, EventArgs e)
        /// <summary>
        /// Clears rtxtResults of all text
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearResults_Click(object sender, EventArgs e)
        {
            reset();
        } //End private void btnClearResults_Click(object sender, EventArgs e)
    } //End public partial class Form1 : Form
} //End namespace CS4750HW2
