using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Puzzle
{
    public class Shuffle
    {
        //Static variables delcration
        static public int numberOfMoves = 10;   //holds the number of random moves to be performed for shuffling
        static public bool shuffleDone = false; //flag to show whether shuffling the puzzle is done or not
        static public bool shuffleOn = false;   //flag to show whther in shuffle state or not

        static private int temp = -1;    //temp value to hold the number of moves


        static public void shufflePuzzle()
        {
            //if shuffleOn flag hasn't been set then shouldn't shuffle the puzzle
            //if (!shuffleOn) { return; }

            //if temp == 0 then it mean the shuffling has started for the first time.
            if (temp == -1)
            {
                temp = numberOfMoves;
                shuffleDone = false;
            }

            //numberOfMoves intger variable keeps track of how many shuffling iterations
            //have to took place also. Each time a shuffle takes place numberOfMoves decrements
            //till it's 0. Once it's 0 shuffleDone flash is set to true and numberOfMoves and 
            //temp variables and shuffleOn flags are set to default.
            if (numberOfMoves > 0)
            {
                movePuzzle();
                numberOfMoves--;
                return;
            }
            else
            {
                shuffleOn = false;
                shuffleDone = true;

                numberOfMoves = temp;
                temp = -1;

                return;
            }
        }


        //Each square of the shuffle can have 3 values each for x and y position. Those values
        //are 0, 100 & 200. This function checks if a left, up, down or right move is possible.
        //It does so by looking at the current x and y position of the blank square of the puzzle.
        static private bool isValidMove(int direction)
        {
            if (direction == 0) //check if sliding left is possible
            {
                if (PuzzleData.xPuzzle[2][2] >= 100) { return true; }
            }
            else if (direction == 1) //check if sliding up is possible
            {
                if (PuzzleData.yPuzzle[2][2] >= 100) { return true; }
            }
            else if (direction == 2)//check if sliding right is possible
            {
                if (PuzzleData.xPuzzle[2][2] < 200) { return true; }
            }
            else if (direction == 3) //check if sliding down is possible
            {
                if (PuzzleData.yPuzzle[2][2] < 200) { return true; }
            }

            return false;
        }


        //The procedure movePuzzle gets a valid random direction that the blank square of the
        //puzzle can move in. Once a direction is found the blank square is updated and the 
        //square that is in the place where the blank square was moved to moves to the original
        //location of the blank square simulataneously. So x goes to where y was and y goes to
        //where x was. This is counted as a single move.
        static private void movePuzzle()
        {
            Random ran = new Random();

            //while loop calls the isValidMove static function of this class till a true a value
            //is return from the function to ensure a valid direction is chosen.
            int direction = ran.Next(4);
            while (!isValidMove(direction)) { direction = ran.Next(4); }

            if (direction == 0) //Move Left
            {
                PuzzleData.moveLeft(PuzzleData.xPuzzle, PuzzleData.yPuzzle);
            }
            else if (direction == 1) //Move Up
            {
                PuzzleData.moveUp(PuzzleData.xPuzzle, PuzzleData.yPuzzle);
            }
            else if (direction == 2) //Move Right
            {
                PuzzleData.moveRight(PuzzleData.xPuzzle, PuzzleData.yPuzzle);
            }
            else if (direction == 3) //Move Down
            {
                PuzzleData.moveDown(PuzzleData.xPuzzle, PuzzleData.yPuzzle);
            }
        }
    }
}
