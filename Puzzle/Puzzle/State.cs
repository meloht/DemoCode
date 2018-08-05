using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Puzzle
{
    //This class is only for AStar. It's a data structure that is required for the AStar algorithm as I
    //had to store more properties than the other two algorithms. Main property being the heuristic 
    //associated with each state, and also being the depth of the current state and each child's parent
    //state.
    public class State
    {
        public Double[][] xPuzzle = new Double[3][];    //2-d array to store the x and y position of the puzzle
        public Double[][] yPuzzle = new Double[3][];

        public Int32 fValue, depth = 0, direction = -1; //fValue is the Heuristic value, depth is the depth of the state
        //from the start state, direction the direction the blank square
        //moved in order to get to it's current state.

        public static int currentDepth;                 //currentDepth. *Note it's a Static variable
        public static Stack directions = new Stack();   //directions Stack also a static variable to store all the 
        //directions the A* algorithm moved the blank square

        //Overloaded Constructor with one parameter as the heuristic for the State class
        public State(int heuristic)
        {
            fValue = heuristic + depth;

            for (int i = 0; i < 3; i++)
            {
                xPuzzle[i] = new Double[3];
                yPuzzle[i] = new Double[3];
            }

            xPuzzle[0][0] = AStar.xTempPuzzle[0][0];
            xPuzzle[0][1] = AStar.xTempPuzzle[0][1];
            xPuzzle[0][2] = AStar.xTempPuzzle[0][2];
            xPuzzle[1][0] = AStar.xTempPuzzle[1][0];
            xPuzzle[1][1] = AStar.xTempPuzzle[1][1];
            xPuzzle[1][2] = AStar.xTempPuzzle[1][2];
            xPuzzle[2][0] = AStar.xTempPuzzle[2][0];
            xPuzzle[2][1] = AStar.xTempPuzzle[2][1];
            xPuzzle[2][2] = AStar.xTempPuzzle[2][2];

            yPuzzle[0][0] = AStar.yTempPuzzle[0][0];
            yPuzzle[0][1] = AStar.yTempPuzzle[0][1];
            yPuzzle[0][2] = AStar.yTempPuzzle[0][2];
            yPuzzle[1][0] = AStar.yTempPuzzle[1][0];
            yPuzzle[1][1] = AStar.yTempPuzzle[1][1];
            yPuzzle[1][2] = AStar.yTempPuzzle[1][2];
            yPuzzle[2][0] = AStar.yTempPuzzle[2][0];
            yPuzzle[2][1] = AStar.yTempPuzzle[2][1];
            yPuzzle[2][2] = AStar.yTempPuzzle[2][2];
        }

        //Overloaded Constructor with 4 parameters as the heuristic for the State class that set the
        //appropriate data members of the class.
        public State(int heuristic, Double[][] pXPuzzle, Double[][] pYPuzzle, Int32 pDirection)
        {
            fValue = heuristic + currentDepth;
            direction = pDirection;
            depth = currentDepth;

            for (int i = 0; i < 3; i++)
            {
                xPuzzle[i] = new Double[3];
                yPuzzle[i] = new Double[3];
            }

            xPuzzle[0][0] = pXPuzzle[0][0];
            xPuzzle[0][1] = pXPuzzle[0][1];
            xPuzzle[0][2] = pXPuzzle[0][2];
            xPuzzle[1][0] = pXPuzzle[1][0];
            xPuzzle[1][1] = pXPuzzle[1][1];
            xPuzzle[1][2] = pXPuzzle[1][2];
            xPuzzle[2][0] = pXPuzzle[2][0];
            xPuzzle[2][1] = pXPuzzle[2][1];
            xPuzzle[2][2] = pXPuzzle[2][2];

            yPuzzle[0][0] = pYPuzzle[0][0];
            yPuzzle[0][1] = pYPuzzle[0][1];
            yPuzzle[0][2] = pYPuzzle[0][2];
            yPuzzle[1][0] = pYPuzzle[1][0];
            yPuzzle[1][1] = pYPuzzle[1][1];
            yPuzzle[1][2] = pYPuzzle[1][2];
            yPuzzle[2][0] = pYPuzzle[2][0];
            yPuzzle[2][1] = pYPuzzle[2][1];
            yPuzzle[2][2] = pYPuzzle[2][2];
        }


        //Copy class which copies the instances curreny xPuzzle and yPuzzle arrays into the parameters
        //that have been passed (i.e. pXPuzzle and pYPuzzle)
        public void Copy(Double[][] pXPuzzle, Double[][] pYPuzzle)
        {

            pXPuzzle[0][0] = xPuzzle[0][0];
            pXPuzzle[0][1] = xPuzzle[0][1];
            pXPuzzle[0][2] = xPuzzle[0][2];
            pXPuzzle[1][0] = xPuzzle[1][0];
            pXPuzzle[1][1] = xPuzzle[1][1];
            pXPuzzle[1][2] = xPuzzle[1][2];
            pXPuzzle[2][0] = xPuzzle[2][0];
            pXPuzzle[2][1] = xPuzzle[2][1];
            pXPuzzle[2][2] = xPuzzle[2][2];

            pYPuzzle[0][0] = yPuzzle[0][0];
            pYPuzzle[0][1] = yPuzzle[0][1];
            pYPuzzle[0][2] = yPuzzle[0][2];
            pYPuzzle[1][0] = yPuzzle[1][0];
            pYPuzzle[1][1] = yPuzzle[1][1];
            pYPuzzle[1][2] = yPuzzle[1][2];
            pYPuzzle[2][0] = yPuzzle[2][0];
            pYPuzzle[2][1] = yPuzzle[2][1];
            pYPuzzle[2][2] = yPuzzle[2][2];
        }
    }
}
