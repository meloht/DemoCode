using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Puzzle
{
    public class PuzzleData
    {
        //Two Double arrays to hold and store the positions of each square of the puzzle.
        //The xPuzzle array stores the distance of each square from the Left of it's container 
        //while the yPuzzle array stores the distance of each sqaure from the top of the container.
        public volatile static Double[][] xPuzzle = new Double[3][];
        public volatile static Double[][] yPuzzle = new Double[3][];

        public volatile static Double[][] xBeginPuzzle = new Double[3][];
        public volatile static Double[][] yBeginPuzzle = new Double[3][];

        public volatile static Double[][] FinalPuzzleX = new Double[3][];
        public volatile static Double[][] FinalPuzzleY = new Double[3][];

        //Default Constructor for the PuzzleData Class.
        //The constructor initializes arrays: xPuzzle and yPuzzle.
        public static void loadPuzzleData()
        {
            initializeArray();
        }


        //Initialising the xPuzzle and yPuzzle two dimensional array.
        private static void initializeArray()
        {
            //Initialising the 2 dimensional arrays
            xPuzzle[0] = new Double[3];
            xPuzzle[1] = new Double[3];
            xPuzzle[2] = new Double[3];

            yPuzzle[0] = new Double[3];
            yPuzzle[1] = new Double[3];
            yPuzzle[2] = new Double[3];

            FinalPuzzleX[0] = new Double[3];
            FinalPuzzleX[1] = new Double[3];
            FinalPuzzleX[2] = new Double[3];

            FinalPuzzleY[0] = new Double[3];
            FinalPuzzleY[1] = new Double[3];
            FinalPuzzleY[2] = new Double[3];

            xBeginPuzzle[0] = new Double[3];
            xBeginPuzzle[1] = new Double[3];
            xBeginPuzzle[2] = new Double[3];

            yBeginPuzzle[0] = new Double[3];
            yBeginPuzzle[1] = new Double[3];
            yBeginPuzzle[2] = new Double[3];


            //Assigned default values to the arrays.
            resetPuzzle();
        }


        //Sets the puzzles position to it's final/finished state by assigning the appropriate
        //values to the xPuzzle and yPuzzle arrays.
        public static void resetPuzzle()
        {
            //Setting all the values of the xPuzzle array to represent the final state
            xPuzzle[0][0] = xBeginPuzzle[0][0];
            xPuzzle[0][1] = xBeginPuzzle[0][1];
            xPuzzle[0][2] = xBeginPuzzle[0][2];
            xPuzzle[1][0] = xBeginPuzzle[1][0];
            xPuzzle[1][1] = xBeginPuzzle[1][1];
            xPuzzle[1][2] = xBeginPuzzle[1][2];
            xPuzzle[2][0] = xBeginPuzzle[2][0];
            xPuzzle[2][1] = xBeginPuzzle[2][1];
            xPuzzle[2][2] = xBeginPuzzle[2][2];

            //Setting all the values of the yPuzzle array to represent the final state
            yPuzzle[0][0] = yBeginPuzzle[0][0];
            yPuzzle[0][1] = yBeginPuzzle[0][1];
            yPuzzle[0][2] = yBeginPuzzle[0][2];
            yPuzzle[1][0] = yBeginPuzzle[1][0];
            yPuzzle[1][1] = yBeginPuzzle[1][1];
            yPuzzle[1][2] = yBeginPuzzle[1][2];
            yPuzzle[2][0] = yBeginPuzzle[2][0];
            yPuzzle[2][1] = yBeginPuzzle[2][1];
            yPuzzle[2][2] = yBeginPuzzle[2][2];
        }

        //Sets the puzzles position to it's final/finished state by assigning the appropriate
        //values to the xPuzzle and yPuzzle arrays.
        public static void resetPuzzle(Double[][] xP, Double[][] yP)
        {
            //Setting all the values of the xPuzzle array to represent the final state
            xPuzzle[0][0] = xP[0][0];
            xPuzzle[0][1] = xP[0][1];
            xPuzzle[0][2] = xP[0][2];
            
            xPuzzle[1][0] = xP[1][0];
            xPuzzle[1][1] = xP[1][1];
            xPuzzle[1][2] = xP[1][2];

            xPuzzle[2][0] = xP[2][0];
            xPuzzle[2][1] = xP[2][1];
            xPuzzle[2][2] = xP[2][2];

            //Setting all the values of the yPuzzle array to represent the final state
            yPuzzle[0][0] = yP[0][0];
            yPuzzle[0][1] = yP[0][1];
            yPuzzle[0][2] = yP[0][2];
           
            yPuzzle[1][0] = yP[1][0];
            yPuzzle[1][1] = yP[1][1];
            yPuzzle[1][2] = yP[1][2];
            
            yPuzzle[2][0] = yP[2][0];
            yPuzzle[2][1] = yP[2][1];
            yPuzzle[2][2] = yP[2][2];
        }

        //method 
        public static bool isFinalState(Double[][] xPuzzle, Double[][] yPuzzle)
        {
            if ((xPuzzle[0][0] == FinalPuzzleX[0][0]) &&
                (xPuzzle[0][1] == FinalPuzzleX[0][1]) &&
                (xPuzzle[0][2] == FinalPuzzleX[0][2]) &&
                (xPuzzle[1][0] == FinalPuzzleX[1][0]) &&
                (xPuzzle[1][1] == FinalPuzzleX[1][1]) &&
                (xPuzzle[1][2] == FinalPuzzleX[1][2]) &&
                (xPuzzle[2][0] == FinalPuzzleX[2][0]) &&
                (xPuzzle[2][1] == FinalPuzzleX[2][1]) &&
                (xPuzzle[2][2] == FinalPuzzleX[2][2]) &&

                (yPuzzle[0][0] == FinalPuzzleY[0][0]) &&
                (yPuzzle[0][1] == FinalPuzzleY[0][1]) &&
                (yPuzzle[0][2] == FinalPuzzleY[0][2]) &&
                (yPuzzle[1][0] == FinalPuzzleY[1][0]) &&
                (yPuzzle[1][1] == FinalPuzzleY[1][1]) &&
                (yPuzzle[1][2] == FinalPuzzleY[1][2]) &&
                (yPuzzle[2][0] == FinalPuzzleY[2][0]) &&
                (yPuzzle[2][1] == FinalPuzzleY[2][1]) &&
                (yPuzzle[2][2] == FinalPuzzleY[2][2]))
            {
                return true;
            }
            else
            {
                return false;

            }
        }


        //The below four methods update an array to show a movement of the blank square in a particular
        //direction. moveLeft updates the parameters passed to it to update the arrays to show a left move
        //and so on.
        public static void moveLeft(Double[][] xTempPuzzle, Double[][] yTempPuzzle)
        {
            if (xTempPuzzle[2][2] < 99) { return; }

            xTempPuzzle[2][2] -= 100;
            for (int i = 0; i < 3; i++)
            {
                bool moveMade = false;
                for (int j = 0; j < 3; j++)
                {
                    if ((xTempPuzzle[2][2] == xTempPuzzle[i][j]) && (yTempPuzzle[2][2] == yTempPuzzle[i][j]))
                    {
                        if ((i == 2) && (j == 2)) { continue; } //this is the position of the blank square
                        xTempPuzzle[i][j] += 100;
                        moveMade = true;
                        break;
                    }
                }
                if (moveMade) { break; }
            }
        }

        public static void moveRight(Double[][] xTempPuzzle, Double[][] yTempPuzzle)
        {
            if (xTempPuzzle[2][2] > 200) { return; }

            xTempPuzzle[2][2] += 100;
            for (int i = 0; i < 3; i++)
            {
                bool moveMade = false;
                for (int j = 0; j < 3; j++)
                {
                    if ((xTempPuzzle[2][2] == xTempPuzzle[i][j]) && (yTempPuzzle[2][2] == yTempPuzzle[i][j]))
                    {
                        if ((i == 2) && (j == 2)) { continue; }
                        xTempPuzzle[i][j] -= 100;
                        moveMade = true;
                        break;
                    }
                }
                if (moveMade) { break; }
            }
        }

        public static void moveUp(Double[][] xTempPuzzle, Double[][] yTempPuzzle)
        {
            if (yTempPuzzle[2][2] < 99) { return; }

            yTempPuzzle[2][2] -= 100;
            for (int i = 0; i < 3; i++)
            {
                bool moveMade = false;
                for (int j = 0; j < 3; j++)
                {
                    if ((xTempPuzzle[2][2] == xTempPuzzle[i][j]) && (yTempPuzzle[2][2] == yTempPuzzle[i][j]))
                    {
                        if ((i == 2) && (j == 2)) { continue; }
                        yTempPuzzle[i][j] += 100;
                        moveMade = true;
                        break;
                    }
                }
                if (moveMade) { break; }
            }

        }

        public static void moveDown(Double[][] xTempPuzzle, Double[][] yTempPuzzle)
        {
            if (yTempPuzzle[2][2] > 200) { return; }

            yTempPuzzle[2][2] += 100;
            for (int i = 0; i < 3; i++)
            {
                bool moveMade = false;
                for (int j = 0; j < 3; j++)
                {
                    if ((xTempPuzzle[2][2] == xTempPuzzle[i][j]) && (yTempPuzzle[2][2] == yTempPuzzle[i][j]))
                    {
                        if ((i == 2) && (j == 2)) { continue; }
                        yTempPuzzle[i][j] -= 100;
                        moveMade = true;
                        break;
                    }
                }
                if (moveMade) { break; }
            }
        }
    }
}
