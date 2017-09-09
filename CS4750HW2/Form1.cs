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

        //Fields
        Stopwatch timer;

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
            timer = Stopwatch.StartNew();
            this.rtxtResults.Text = "IDS: We don't do anything yet\n";
            displayMillisecondsElapsed();
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
