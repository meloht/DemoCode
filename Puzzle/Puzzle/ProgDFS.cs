using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Puzzle
{
    public class ProgDFS
    {
        static public bool ProgDFSOn = false;   //flag which initialises the 2-d arrays if set.
        static public bool solving = false;     //flag set when algorithm is solving the puzzle.

        static private bool trackBack = false;  //flag set while tracking back

        private static Stack stackProgDFS = new Stack();    //the FILO structure required for ProgDFS
        private static Stack stackMoves = new Stack();      //Move stack to store all moves made towards goal state

        private static int depth = 1, depthCounter = 0;     //depth initially set to 1 and depthCounter to track state depths

        private static ArrayList xVisitedList = new ArrayList();    //Stores expanded states  
        private static ArrayList yVisitedList = new ArrayList();

        public static Double[][] xTempPuzzle = new Double[3][]; //2-d arrays to store x and y positions of the squares
        public static Double[][] yTempPuzzle = new Double[3][];

        static public int getDepth() { return depth; }  //gets the current value of the depth variable


        //initializes the 2-d array and stores the puzzles current state into the xTempPuzzle and yTempPuzzle arrays
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


        //resets all variables of this static class. called when algorithm has been completed or reset button clicked
        static public void resetAll()
        {
            xVisitedList.Clear();
            yVisitedList.Clear();
            stackProgDFS.Clear();
            stackMoves.Clear();
            ProgDFSOn = true;
            solving = false;
            depth = 1;
            depthCounter = 0;
        }


        //main Entry method of this class. Get's called every time one iteration of the algorithm is required
        static public bool solvePuzzle()
        {
            if (ProgDFSOn) { initializeArray(); ProgDFSOn = false; }

            if (PuzzleData.isFinalState(xTempPuzzle, yTempPuzzle)) { return true; }

            addNextMove();
            movePuzzle();

            return false;
        }


        //checks if the direction passed to it is valid move from the puzzles current state
        static private bool isValidMove(int direction)
        {
            if (direction == 0) //check if sliding left is possible
            {
                if (ProgDFS.xTempPuzzle[2][2] >= 100) { return true; }
            }
            else if (direction == 1) //check if sliding up is possible
            {
                if (ProgDFS.yTempPuzzle[2][2] >= 100) { return true; }
            }
            else if (direction == 2)//check if sliding right is possible
            {
                if (ProgDFS.xTempPuzzle[2][2] < 200) { return true; }
            }
            else if (direction == 3) //check if sliding down is possible
            {
                if (ProgDFS.yTempPuzzle[2][2] < 200) { return true; }
            }

            return false;
        }


        //checks if the current state has been expanded contrary to the name it's an expanded list not a visited list.
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
        //if not the it adds that direction to the stackProgDFS variable.
        static private void addNextMove()
        {
            bool moveAdded = false;

            if (depthCounter >= depth) { previousState(moveAdded); return; }

            if ((isValidMove(0)) && (!moveAdded))
            {
                PuzzleData.moveLeft(xTempPuzzle, yTempPuzzle);
                if (isVisitedState())
                {
                    PuzzleData.moveRight(xTempPuzzle, yTempPuzzle);
                }
                else
                {
                    PuzzleData.moveRight(xTempPuzzle, yTempPuzzle);
                    stackProgDFS.Push(0);
                    moveAdded = true;
                }
            }

            if ((isValidMove(1)) && (!moveAdded))
            {
                PuzzleData.moveUp(xTempPuzzle, yTempPuzzle);
                if (isVisitedState())
                {
                    PuzzleData.moveDown(xTempPuzzle, yTempPuzzle);
                }
                else
                {
                    PuzzleData.moveDown(xTempPuzzle, yTempPuzzle);
                    stackProgDFS.Push(1);
                    moveAdded = true;
                }
            }

            if ((isValidMove(2)) && (!moveAdded))
            {
                PuzzleData.moveRight(xTempPuzzle, yTempPuzzle);
                if (isVisitedState())
                {
                    PuzzleData.moveLeft(xTempPuzzle, yTempPuzzle);
                }
                else
                {
                    PuzzleData.moveLeft(xTempPuzzle, yTempPuzzle);
                    stackProgDFS.Push(2);
                    moveAdded = true;
                }
            }

            if ((isValidMove(3)) && (!moveAdded))
            {
                PuzzleData.moveDown(xTempPuzzle, yTempPuzzle);
                if (isVisitedState())
                {
                    PuzzleData.moveUp(xTempPuzzle, yTempPuzzle);
                }
                else
                {
                    PuzzleData.moveUp(xTempPuzzle, yTempPuzzle);
                    stackProgDFS.Push(3);
                    moveAdded = true;
                }
            }

            if (!moveAdded) { previousState(moveAdded); } else { depthCounter++; }
        }


        //this method gets called if the puzzle has reached it's max depth for the iteration and there are no more states
        //it tracks back to the start state so the algorithm can begin again with the depth incremented
        static private void previousState(bool moveAdded)
        {
            if (!moveAdded)
            {
                if (stackMoves.Count == 0) { return; }

                int lastMove = (int)stackMoves.Pop();
                if (lastMove == 0) { lastMove = 2; }
                else if (lastMove == 1) { lastMove = 3; }
                else if (lastMove == 2) { lastMove = 0; }
                else if (lastMove == 3) { lastMove = 1; }

                stackProgDFS.Push(lastMove);

                depthCounter--;
                trackBack = true;
            }
        }


        //Method that updates the puzzle with the direction popped of the DFS stack. Then adds the new puzzle state
        //to the visited list.
        static private void movePuzzle()
        {
            if (stackProgDFS.Count == 0)
            {
                xVisitedList.Clear();
                yVisitedList.Clear();
                depth++;
                return;
            }

            int direction = (int)stackProgDFS.Pop();
            //while (!isValidMove(direction))
            //{
            //    if (stackProgDFS.Count > 0) { direction = (int)stackProgDFS.Pop(); } else { return; }
            //}

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

            //only add move to move stack if track back hasn't been set
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
