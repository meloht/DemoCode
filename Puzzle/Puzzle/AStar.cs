using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Puzzle
{
    public class AStar
    {
        //*Note: All datemembers and methods of this class are static*

        //Unlike the other two algorithms A Start uses the State class to hold all it's states

        static public bool AStarOn = false;     //If set the 2-d x/yTempPuzzle arrays are initialized
        static public bool solving = false;

        static private bool trackBack = false;  //Set when the simulation need to show going back an iteration

        private static ArrayList openStates = new ArrayList();  //holds all states that have been visited
        private static ArrayList closedStates = new ArrayList();    //holds all states that have been expanded

        public static Double[][] xTempPuzzle = new Double[3][]; //2-d arrays to temporarily hold the x&y position
        public static Double[][] yTempPuzzle = new Double[3][]; //of the puzzle


        //initializes the 2-d arrays xTempPuzzle and yTempPuzzle
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


        //resets all datamembers of the puzzle. gets called when algorithm is finished or reset button clicked.
        static public void resetAll()
        {
            openStates.Clear();
            closedStates.Clear();
            State.directions.Clear();
            State.currentDepth = 0;
            AStarOn = true;
            solving = false;
        }


        //Get's the depth of the final state from the start state
        static public int getLastDepth()
        {
            return ((State)closedStates[closedStates.Count - 1]).depth;
        }


        //Main entry point Method for this class. Performs a single iteration of the A* alorithm
        static public bool solvePuzzle()
        {
            if (AStarOn) { initializeArray(); AStarOn = false; }

            if (PuzzleData.isFinalState(AStar.xTempPuzzle, AStar.yTempPuzzle)) { return true; }

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
                if (AStar.xTempPuzzle[2][2] >= 100) { return true; }
            }
            else if (direction == 1) //check if sliding up is possible
            {
                if (AStar.yTempPuzzle[2][2] >= 100) { return true; }
            }
            else if (direction == 2)//check if sliding right is possible
            {
                if (AStar.xTempPuzzle[2][2] < 200) { return true; }
            }
            else if (direction == 3) //check if sliding down is possible
            {
                if (AStar.yTempPuzzle[2][2] < 200) { return true; }
            }

            return false;
        }


        //Checks if the current state is in the closeStates list or not. Like an expanded list.
        static private bool isVisitedState()
        {
           // return false;
            for (int cnt = closedStates.Count - 1; cnt > -1; cnt--)
            {
                int sameElementCnt = 0;

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if ((((State)closedStates[cnt]).xPuzzle[i][j] == xTempPuzzle[i][j]) &&
                            ((State)closedStates[cnt]).yPuzzle[i][j] == yTempPuzzle[i][j])
                        {
                            sameElementCnt++;
                        }
                    }
                }

                if (sameElementCnt == 9) { return true; }
            }
            return false;
        }


        //converts the paramete passed to a poitive value and passes it back
        static private double posNum(double x)
        {
            if (x >= 0) { return x; } else { return x * -1; }
        }


        //Calculates the Manhattan Distance Heuristic for the current state.
        static private int getManhattanDistance(int direction)
        {
            double heuristic = 0;

            heuristic += posNum(xTempPuzzle[0][0] - 0);
            heuristic += posNum(xTempPuzzle[0][1] - 100);
            heuristic += posNum(xTempPuzzle[0][2] - 200);
            heuristic += posNum(xTempPuzzle[1][0] - 0);
            heuristic += posNum(xTempPuzzle[1][1] - 100);
            heuristic += posNum(xTempPuzzle[1][2] - 200);
            heuristic += posNum(xTempPuzzle[2][0] - 0);
            heuristic += posNum(xTempPuzzle[2][1] - 100);
            heuristic += posNum(xTempPuzzle[2][2] - 200);

            heuristic += posNum(yTempPuzzle[0][0] - 0);
            heuristic += posNum(yTempPuzzle[0][1] - 0);
            heuristic += posNum(yTempPuzzle[0][2] - 0);
            heuristic += posNum(yTempPuzzle[1][0] - 100);
            heuristic += posNum(yTempPuzzle[1][1] - 100);
            heuristic += posNum(yTempPuzzle[1][2] - 100);
            heuristic += posNum(yTempPuzzle[2][0] - 200);
            heuristic += posNum(yTempPuzzle[2][1] - 200);
            heuristic += posNum(yTempPuzzle[2][2] - 200);

            return Convert.ToInt32(heuristic);
        }


        //Addes a move into the openStates list. Different from the other two algorithms, instead of adding a
        //direction it creates a new state instance which reflects the possible moves and add that to the openStates
        //list.
        static private void addNextMove()
        {
            ArrayList possibleMoves = new ArrayList(4);
            for (int i = 0; i < 4; i++) { possibleMoves.Add(-1); }

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
                    State st = new State(getManhattanDistance(0), xTempPuzzle, yTempPuzzle, 0);
                    openStates.Add(st);

                    PuzzleData.moveRight(xTempPuzzle, yTempPuzzle);
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
                    State st = new State(getManhattanDistance(1), xTempPuzzle, yTempPuzzle, 1);
                    openStates.Add(st);

                    PuzzleData.moveDown(xTempPuzzle, yTempPuzzle);
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
                    State st = new State(getManhattanDistance(2), xTempPuzzle, yTempPuzzle, 2);
                    openStates.Add(st);

                    PuzzleData.moveLeft(xTempPuzzle, yTempPuzzle);
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
                    State st = new State(getManhattanDistance(3), xTempPuzzle, yTempPuzzle, 3);
                    openStates.Add(st);

                    PuzzleData.moveUp(xTempPuzzle, yTempPuzzle);
                    moveAdded = true;
                }
            }

            //if there is no move possible then the algorithm backtracks.
            if (!moveAdded)
            {
                int lastMove = (int)State.directions.Pop();
                if (lastMove == 0) { lastMove = 2; }
                else if (lastMove == 1) { lastMove = 3; }
                else if (lastMove == 2) { lastMove = 0; }
                else if (lastMove == 3) { lastMove = 1; }

                State.directions.Push(lastMove);
                trackBack = true;
            }
        }


        //Find the state with the lowest heuristic and then updates the puzzle with that state
        //if trackBack flag set then it goes back one iteration for the purposes of the simulation.
        static private void movePuzzle()
        {
            if (!trackBack)
            {
                int direction = 0, Index = 0, lowestFValue = 100000;

                for (int i = 0; i < openStates.Count; i++)
                {
                    if (((State)openStates[i]).fValue <= lowestFValue)
                    {
                        lowestFValue = ((State)openStates[i]).fValue;
                        Index = i;
                    }
                }

                if (((State)openStates[Index]).depth < State.currentDepth) { trackBack = true; }

                if (!trackBack)
                {
                    direction = ((State)openStates[Index]).direction;

                    ((State)openStates[Index]).Copy(xTempPuzzle, yTempPuzzle);

                    closedStates.Add(((State)openStates[Index]));
                    openStates.RemoveAt(Index);

                    State.directions.Push(direction);
                    State.currentDepth++;
                }
            }

            if (trackBack)
            {
                int Index = 0, lowestFValue = 100000;


                Index = closedStates.Count - 1;
                if (Index > 0)
                {
                    ((State)openStates[Index - 1]).Copy(xTempPuzzle, yTempPuzzle);
                }
                //int direction = (int)State.directions.Pop();

                //if (direction == 0) //Move Left
                //{
                //    PuzzleData.moveLeft(xTempPuzzle, yTempPuzzle);
                //}
                //else if (direction == 1) //Move Up
                //{
                //    PuzzleData.moveUp(xTempPuzzle, yTempPuzzle);
                //}
                //else if (direction == 2) //Move Right
                //{
                //    PuzzleData.moveRight(xTempPuzzle, yTempPuzzle);
                //}
                //else if (direction == 3) //Move Down
                //{
                //    PuzzleData.moveDown(xTempPuzzle, yTempPuzzle);
                //}
                trackBack = false;
                State.currentDepth--;
            }
        }
    }

}
