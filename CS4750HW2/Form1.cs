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
            this.rtxtResults.Text += data + "\n\n";
        } //End public void displayData(string data)
        public void displayMillisecondsElapsed()
        {
            timer.Stop();
            this.displayData("Time elapsed: " + this.timer.ElapsedMilliseconds.ToString());
        } //End 
        

        /***************EVENTS***************/
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnIDS_Click(object sender, EventArgs e)
        {
            Puzzle puzzle = new Puzzle(Puzzle1);
        } //End private void btnIDS_Click(object sender, EventArgs e)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDFS_Click(object sender, EventArgs e)
        {
            this.rtxtResults.Text = "DFS: We don't do anything yet\n";
        } //End private void btnDFS_Click(object sender, EventArgs e)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAStar_Click(object sender, EventArgs e)
        {
            this.rtxtResults.Text = "A*: We don't do anything yet\n";
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
