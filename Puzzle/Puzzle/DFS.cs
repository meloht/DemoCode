using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Puzzle
{
    public class DFS
    {
        static public bool DFSOn = false;       //initial flag. if set initializes the array variables.
        static public bool solving = false;     //solving flag set whilst the algorithm is running.

        static private bool trackBack = false;  //trackBack flag set if the puzzle get stuck to track back moves

        private static Stack stackDFS = new Stack();    //FIFO structure required for DFS
        private static Stack stackMoves = new Stack();  //Stack stores everymove made. used for tracking back

        private static ArrayList xVisitedList = new ArrayList();    //list to store all the states that have been expanded
        private static ArrayList yVisitedList = new ArrayList();

        public static Double[][] xTempPuzzle = new Double[3][];     //actual x & y 2-d arrays strore the pos of squares
        public static Double[][] yTempPuzzle = new Double[3][];

        //gets the number of moves made towards the goal state.
        static public int getNumberOfMovesMade()
        {
            return stackMoves.Count;
        }


        //initialzes the 2-d arrays xTempPuzzle and yTempPuzzle and sets them to the current value of the
        //Puzzle displayed on the screen.
        static public void initializeArray()
        {
            for (int i = 0; i < 3; i++)
            {
                xTempPuzzle[i] = new Double[3];
                yTempPuzzle[i] = new Double[3];
            }

            xTempPuzzle[0][0] = PuzzleData.xPuzzle[0][0];
            xTempPuzzle[0][1] = PuzzleData.xPuzzle[0][1];
            xTempPuzzle[0][2] = PuzzleData.xPuzzle[0][2];
            xTempPuzzle[1][0] = PuzzleData.xPuzzle[1][0];
            xTempPuzzle[1][1] = PuzzleData.xPuzzle[1][1];
            xTempPuzzle[1][2] = PuzzleData.xPuzzle[1][2];
            xTempPuzzle[2][0] = PuzzleData.xPuzzle[2][0];
            xTempPuzzle[2][1] = PuzzleData.xPuzzle[2][1];
            xTempPuzzle[2][2] = PuzzleData.xPuzzle[2][2];

            yTempPuzzle[0][0] = PuzzleData.yPuzzle[0][0];
            yTempPuzzle[0][1] = PuzzleData.yPuzzle[0][1];
            yTempPuzzle[0][2] = PuzzleData.yPuzzle[0][2];
            yTempPuzzle[1][0] = PuzzleData.yPuzzle[1][0];
            yTempPuzzle[1][1] = PuzzleData.yPuzzle[1][1];
            yTempPuzzle[1][2] = PuzzleData.yPuzzle[1][2];
            yTempPuzzle[2][0] = PuzzleData.yPuzzle[2][0];
            yTempPuzzle[2][1] = PuzzleData.yPuzzle[2][1];
            yTempPuzzle[2][2] = PuzzleData.yPuzzle[2][2];
        }


        //resets all the variables. called when the reset button is clicked or the algorithm has finished
        static public void resetAll()
        {
            xVisitedList.Clear();
            yVisitedList.Clear();
            stackDFS.Clear();
            stackMoves.Clear();
            DFSOn = true;
            solving = false;
        }


        //the entry point method of this class. Get's called everytime a next move using DFS is required.
        static public bool solvePuzzle()
        {
            if (DFSOn) { initializeArray(); DFSOn = false; }

            if (PuzzleData.isFinalState(DFS.xTempPuzzle, DFS.yTempPuzzle)) { return true; }

            addNextMove();
            movePuzzle();

            return false;
        }


        //Each square of the shuffle can have 3 values each for x and y position. Those values
        //are 0, 100 & 200. This function checks if a left, up, down or right move is possible.
        //It does so by looking at the current x and y position of the blank square of the puzzle.
        static private bool isValidMove(int direction)
        {
            if (direction == 0) //check if sliding left is possible
            {
                if (DFS.xTempPuzzle[2][2] >= 100) { return true; }
            }
            else if (direction == 1) //check if sliding up is possible
            {
                if (DFS.yTempPuzzle[2][2] >= 100) { return true; }
            }
            else if (direction == 2)//check if sliding right is possible
            {
                if (DFS.xTempPuzzle[2][2] < 200) { return true; }
            }
            else if (direction == 3) //check if sliding down is possible
            {
                if (DFS.yTempPuzzle[2][2] < 200) { return true; }
            }

            return false;
        }


        //Checks if the current state has been expanded before or not contrary to the name
        //it doesn't check all the visited states but expanded states.
        static private bool isVisitedState()
        {
            for (int cnt = xVisitedList.Count - 1; cnt > -1; cnt--)
            {
                int sameElementCnt = 0;

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if ((((Double[][])xVisitedList[cnt])[i][j] == xTempPuzzle[i][j]) &&
                            ((Double[][])yVisitedList[cnt])[i][j] == yTempPuzzle[i][j])
                        {
                            sameElementCnt++;
                        }
                    }
                }

                if (sameElementCnt == 9) { return true; }
            }
            return false;
        }


        //The method below checks for a valid move and then checks if the state has been visited
        //if not the it adds that direction to the stackDFS variable.
        static private void addNextMove()
        {
            bool moveAdded = false;

            if (isValidMove(0))
            {
                PuzzleData.moveLeft(xTempPuzzle, yTempPuzzle);
                if (isVisitedState())
                {
                    PuzzleData.moveRight(xTempPuzzle, yTempPuzzle);
                }
                else
                {
                    PuzzleData.moveRight(xTempPuzzle, yTempPuzzle);
                    stackDFS.Push(0);
                    moveAdded = true;
                }
            }

            if (isValidMove(1))
            {
                PuzzleData.moveUp(xTempPuzzle, yTempPuzzle);
                if (isVisitedState())
                {
                    PuzzleData.moveDown(xTempPuzzle, yTempPuzzle);
                }
                else
                {
                    PuzzleData.moveDown(xTempPuzzle, yTempPuzzle);
                    stackDFS.Push(1);
                    moveAdded = true;
                }
            }

            if (isValidMove(2))
            {
                PuzzleData.moveRight(xTempPuzzle, yTempPuzzle);
                if (isVisitedState())
                {
                    PuzzleData.moveLeft(xTempPuzzle, yTempPuzzle);
                }
                else
                {
                    PuzzleData.moveLeft(xTempPuzzle, yTempPuzzle);
                    stackDFS.Push(2);
                    moveAdded = true;
                }
            }

            if (isValidMove(3))
            {
                PuzzleData.moveDown(xTempPuzzle, yTempPuzzle);
                if (isVisitedState())
                {
                    PuzzleData.moveUp(xTempPuzzle, yTempPuzzle);
                }
                else
                {
                    PuzzleData.moveUp(xTempPuzzle, yTempPuzzle);
                    stackDFS.Push(3);
                    moveAdded = true;
                }
            }


            //if no move could be found happens when the current state cannot get out because all the
            //neighbouring states have already been expanded. It adds the opposite of the last move made
            //which makes the puzzle go back one step.
            if (!moveAdded)
            {
                int lastMove = (int)stackMoves.Pop();
                if (lastMove == 0) { lastMove = 2; }
                else if (lastMove == 1) { lastMove = 3; }
                else if (lastMove == 2) { lastMove = 0; }
                else if (lastMove == 3) { lastMove = 1; }

                stackDFS.Push(lastMove);
                trackBack = true;   //trackBack flag is set for the movePuzzle method. see there for more explanation.
            }
        }


        //Method that updates the puzzle with the direction popped of the DFS stack. Then adds the new puzzle state
        //to the visited list.
        static private void movePuzzle()
        {
            int direction = (int)stackDFS.Pop();

            if (direction == 0) //Move Left
            {
                PuzzleData.moveLeft(xTempPuzzle, yTempPuzzle);
            }
            else if (direction == 1) //Move Up
            {
                PuzzleData.moveUp(xTempPuzzle, yTempPuzzle);
            }
            else if (direction == 2) //Move Right
            {
                PuzzleData.moveRight(xTempPuzzle, yTempPuzzle);
            }
            else if (direction == 3) //Move Down
            {
                PuzzleData.moveDown(xTempPuzzle, yTempPuzzle);
            }

            //if the trackBack flag is set as there were no moves, it doesn't add the move to the movesList stack.
            if (!trackBack) { addMove(direction); }
            addVisitedList();

            trackBack = false;
        }


        //adds the cuurent state the puzzle is in to the visitedList.
        static private void addVisitedList()
        {
            Double[][] xTemp = new Double[3][];
            xTemp[0] = new Double[3];
            xTemp[1] = new Double[3];
            xTemp[2] = new Double[3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    xTemp[i][j] = xTempPuzzle[i][j];
                }
            }
            xVisitedList.Add(xTemp);

            Double[][] yTemp = new Double[3][];
            yTemp[0] = new Double[3];
            yTemp[1] = new Double[3];
            yTemp[2] = new Double[3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    yTemp[i][j] = yTempPuzzle[i][j];
                }
            }
            yVisitedList.Add(yTemp);
        }


        //adds the previous move (while NOT trackingBack) to the visited list.
        static private void addMove(int direction)
        {
            stackMoves.Push(direction);
        }
    }
}
