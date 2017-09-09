using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS4750HW2
{
    class Puzzle {

        // Data model of puzzle
        private int[,] puzzle = new int[3, 3];


        public Puzzle(int[,] puzzle) {
            this.puzzle = puzzle;
        }


        public Point[] getChildren(Point parent, Point node) {
            return null;
        }

        private bool isValidNode(Point node) {
            return node.X > -1 && node.X < 3 && node.Y > -1 && node.Y < 3;
        }
    }
}