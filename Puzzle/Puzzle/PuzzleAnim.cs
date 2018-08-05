using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Puzzle
{
    public partial class FrmMain
    {
        int AnmSpeed = 1;      //speed of the animation in milliseconds
        int speed = 5;


        Point xy1 = new Point(0, 0);
        Point xy2 = new Point(0, 0);
        int numx = 0;
        int numy = 0;
        int lx = 0;
        int ly = 0;
        private void SetLocation(System.Windows.Forms.Label lable, int x, int y)
        {
            if (lable.Location.X != x || lable.Location.Y != y)
            {

                numx = lable.Location.X - x;
                numy = lable.Location.Y - y;
                if (numx != 0)
                {
                    lx = lable.Location.X;
                    for (int i = 1; i <= Math.Abs(numx); i++)
                    {
                        if (numx < 0)
                        {
                            if (i % speed == 0)
                            {
                                xy1.X = lx + i;
                                xy1.Y = lable.Location.Y;
                                lable.Location = xy1;
                            }
                        }
                        else
                        {
                            if (i % speed == 0)
                            {
                                xy1.X = lx - i;
                                xy1.Y = lable.Location.Y;
                                lable.Location = xy1;
                            }
                        }
                        if (i % speed == 0)
                        {
                            Thread.Sleep(AnmSpeed);
                            System.Windows.Forms.Application.DoEvents();
                        }
                    }
                }
                if (numy != 0)
                {
                    ly = lable.Location.Y;
                    for (int i = 1; i <= Math.Abs(numy); i++)
                    {
                        if (numy < 0)
                        {
                            if (i % speed == 0)
                            {
                                xy2.X = lable.Location.X;
                                xy2.Y = ly + i;
                                lable.Location = xy2;
                            }
                        }
                        else
                        {
                            if (i % speed == 0)
                            {
                                xy2.X = lable.Location.X;
                                xy2.Y = ly - i;
                                lable.Location = xy2;
                            }
                        }

                        if (i % speed == 0)
                        {
                            Thread.Sleep(AnmSpeed);
                            System.Windows.Forms.Application.DoEvents();
                        }
                    }
                }
            }
        }
             


        protected void UpdatePuzzle()
        {
            this.BeginInvoke(new Action<string>(UpdatePuzzled), "");
        }
        //Animate the puzzle
        protected void UpdatePuzzled(string s)
        {

            SetLocation(this.label1, Convert.ToInt32(PuzzleData.xPuzzle[0][0]), Convert.ToInt32(PuzzleData.yPuzzle[0][0]));
            SetLocation(this.label2, Convert.ToInt32(PuzzleData.xPuzzle[0][1]), Convert.ToInt32(PuzzleData.yPuzzle[0][1]));
            SetLocation(this.label3, Convert.ToInt32(PuzzleData.xPuzzle[0][2]), Convert.ToInt32(PuzzleData.yPuzzle[0][2]));
            SetLocation(this.label4, Convert.ToInt32(PuzzleData.xPuzzle[1][0]), Convert.ToInt32(PuzzleData.yPuzzle[1][0]));
            SetLocation(this.label5, Convert.ToInt32(PuzzleData.xPuzzle[1][1]), Convert.ToInt32(PuzzleData.yPuzzle[1][1]));
            SetLocation(this.label6, Convert.ToInt32(PuzzleData.xPuzzle[1][2]), Convert.ToInt32(PuzzleData.yPuzzle[1][2]));
            SetLocation(this.label7, Convert.ToInt32(PuzzleData.xPuzzle[2][0]), Convert.ToInt32(PuzzleData.yPuzzle[2][0]));
            SetLocation(this.label8, Convert.ToInt32(PuzzleData.xPuzzle[2][1]), Convert.ToInt32(PuzzleData.yPuzzle[2][1]));
            SetLocation(this.label0, Convert.ToInt32(PuzzleData.xPuzzle[2][2]), Convert.ToInt32(PuzzleData.yPuzzle[2][2]));
            Thread.Sleep(10);

            //The statement below is basically an eventhandler for when the blank square's animation stops. The
            //Event handler is called the method Shuffle_Complete is ShuffleDonw flag is not set and shuffleOn flag
            //is set. This is how it loops however many number of times defined in the Shuffle class. Look at Shuffle
            //Class for more info.

            if ((!Shuffle.shuffleDone) && (Shuffle.shuffleOn))
            {
                //this.label0.LocationChanged += new EventHandler(Shuffle_Completed);
                Shuffle_Completed();
            }
        }

        //This is method is called when the blank square a.k.a. Box9 animation comes to a stop. This method 
        //calls the shufflePuzzle() method again and then calls the UpdatePuzzle() again to update the puzzle.
        //This is not like a recursive method or a loop. It depends on the Box9's animation being finished as
        //the blank square box has to move everytime. Each time it's move is finished the puzzle goes onto it's
        //next iteration.
        void Shuffle_Completed()
        {
            Shuffle.shufflePuzzle();
            UpdatePuzzle();

            Shuffle.shuffleDone = false;

            if (!Shuffle.shuffleOn)
            {
                lblStatus.Text = "Shuffling Done!";

            } //if shuffleOn flag is set shuffling has stopped
        }
        protected void UpdatePuzzle(Double[][] xTempPuzzle, Double[][] yTempPuzzle)
        {
            this.BeginInvoke(new Action<Double[][], Double[][]>(UpdatePuzzledd), xTempPuzzle, yTempPuzzle);
        }

        //This UpdatePuzzle method is an overloaded method of the UpdatePuzzle() above. It can receive two parameters
        //The parameters being two 2-d arrays of size 3 by 3. One stores the x(left) positions of all the squares
        //and the other stores the y(top) position of all the squares. This method obviously changes the puzzle according
        //to the parameters received.
        protected void UpdatePuzzledd(Double[][] xTempPuzzle, Double[][] yTempPuzzle)
        {
            SetLocation(this.label1, Convert.ToInt32(xTempPuzzle[0][0]), Convert.ToInt32(yTempPuzzle[0][0]));
            SetLocation(this.label2, Convert.ToInt32(xTempPuzzle[0][1]), Convert.ToInt32(yTempPuzzle[0][1]));
            SetLocation(this.label3, Convert.ToInt32(xTempPuzzle[0][2]), Convert.ToInt32(yTempPuzzle[0][2]));
            SetLocation(this.label4, Convert.ToInt32(xTempPuzzle[1][0]), Convert.ToInt32(yTempPuzzle[1][0]));
            SetLocation(this.label5, Convert.ToInt32(xTempPuzzle[1][1]), Convert.ToInt32(yTempPuzzle[1][1]));
            SetLocation(this.label6, Convert.ToInt32(xTempPuzzle[1][2]), Convert.ToInt32(yTempPuzzle[1][2]));
            SetLocation(this.label7, Convert.ToInt32(xTempPuzzle[2][0]), Convert.ToInt32(yTempPuzzle[2][0]));
            SetLocation(this.label8, Convert.ToInt32(xTempPuzzle[2][1]), Convert.ToInt32(yTempPuzzle[2][1]));
            SetLocation(this.label0, Convert.ToInt32(xTempPuzzle[2][2]), Convert.ToInt32(yTempPuzzle[2][2]));

            Thread.Sleep(10);
            //The below statements all relate to control variables for the search algorithms. If the control flag
            //is set or not set, the algorithm stops. This happens when the algorithm has finished and it sets it's
            //flag or the reset button is clicked.
            if (DFS.solving)
            {
                DFS_Completed();
            }
            if (ProgDFS.solving)
            {
                ProgDFS_Completed();
            }
            if (AStar.solving)
            {
                AStar_Completed();
            }

        }

        //The below three methods are all what the event handlers for when the box9 animation finishes. They just
        //iterate call the same functions similar to a recursive method but the function does exit each time.
        //If the algorithm complete flag has been set it updates the status message and resets all the variables
        //that the algorithm uses by calling it's own resetAll() method.
        void DFS_Completed()
        {
            if (!DFS.solvePuzzle())
            {
                UpdatePuzzle(DFS.xTempPuzzle, DFS.yTempPuzzle);
            }
            else
            {
                lblStatus.Text = "Puzzle Solved using Depth-First Search. No of Moves made: ";
                lblStatus.Text += DFS.getNumberOfMovesMade().ToString();
                DFS.resetAll();
            }
        }

        void ProgDFS_Completed()
        {
            if (!ProgDFS.solvePuzzle())
            {
                UpdatePuzzle(ProgDFS.xTempPuzzle, ProgDFS.yTempPuzzle);
            }
            else
            {
                lblStatus.Text = "Puzzle Solved using Progressive DFS. Solution found at Depth: ";
                lblStatus.Text += ProgDFS.getDepth().ToString();
                ProgDFS.resetAll();
            }
        }

        void AStar_Completed()
        {
            if (!AStar.solvePuzzle())
            {
                UpdatePuzzle(AStar.xTempPuzzle, AStar.yTempPuzzle);
            }
            else
            {
                lblStatus.Text = "Puzzle Solved using A*. Number of Moves: ";
                lblStatus.Text += State.currentDepth.ToString();
                AStar.resetAll();
            }
        }


        private void SetBegin()
        {
            if (this.txtBegin.Text.Trim() == string.Empty || IsNum(this.txtBegin.Text.Trim()) == false)
            {
                MessageBox.Show("初始状态输入有误！");
                this.txtBegin.Focus();
                return;
            }
            if (this.txtEnd.Text.Trim() == string.Empty || IsNum(this.txtEnd.Text.Trim()) == false)
            {
                MessageBox.Show("目标状态输入有误！");
                this.txtEnd.Focus();
                return;
            }

            Dictionary<int, int> lsBegin = GetBeginEnd(this.txtBegin.Text.Trim());

            //Setting all the values of the xPuzzle array to represent the final state

            GetLableLocation(1, lsBegin[1]);
            GetLableLocation(2, lsBegin[2]);
            GetLableLocation(3, lsBegin[3]);
            GetLableLocation(4, lsBegin[4]);
            GetLableLocation(5, lsBegin[5]);
            GetLableLocation(6, lsBegin[6]);
            GetLableLocation(7, lsBegin[7]);
            GetLableLocation(8, lsBegin[8]);
            GetLableLocation(0, lsBegin[0]);

            //Setting all the values of the yPuzzle array to represent the final state
            PuzzleData.resetPuzzle(PuzzleData.xBeginPuzzle, PuzzleData.yBeginPuzzle);

            SetEnd();

        }
        private Dictionary<int, int> GetBeginEnd(string s)
        {
            Dictionary<int, int> ls = new Dictionary<int, int>();

            for (int i = 0; i < s.Length; i++)
            {
                ls[i] = GetByIndex(i, s);
            }
            return ls;
        }
        private int GetByIndex(int num, string s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i].ToString() == num.ToString())
                {
                    return i;
                }
            }
            return -1;
        }


        private void SetEnd()
        {
            Dictionary<int, int> lsEnd = GetBeginEnd(this.txtEnd.Text.Trim());
            //Setting all the values of the xPuzzle array to represent the final state
            PuzzleData.FinalPuzzleX[0][0] = GetLableXY(lsEnd[1]).X;
            PuzzleData.FinalPuzzleX[0][1] = GetLableXY(lsEnd[2]).X;
            PuzzleData.FinalPuzzleX[0][2] = GetLableXY(lsEnd[3]).X;

            PuzzleData.FinalPuzzleX[1][0] = GetLableXY(lsEnd[4]).X;
            PuzzleData.FinalPuzzleX[1][1] = GetLableXY(lsEnd[5]).X;
            PuzzleData.FinalPuzzleX[1][2] = GetLableXY(lsEnd[6]).X;

            PuzzleData.FinalPuzzleX[2][0] = GetLableXY(lsEnd[7]).X;
            PuzzleData.FinalPuzzleX[2][1] = GetLableXY(lsEnd[8]).X;
            PuzzleData.FinalPuzzleX[2][2] = GetLableXY(lsEnd[0]).X;

            //Setting all the values of the yPuzzle array to represent the final state
            PuzzleData.FinalPuzzleY[0][0] = GetLableXY(lsEnd[1]).Y;
            PuzzleData.FinalPuzzleY[0][1] = GetLableXY(lsEnd[2]).Y;
            PuzzleData.FinalPuzzleY[0][2] = GetLableXY(lsEnd[3]).Y;

            PuzzleData.FinalPuzzleY[1][0] = GetLableXY(lsEnd[4]).Y;
            PuzzleData.FinalPuzzleY[1][1] = GetLableXY(lsEnd[5]).Y;
            PuzzleData.FinalPuzzleY[1][2] = GetLableXY(lsEnd[6]).Y;

            PuzzleData.FinalPuzzleY[2][0] = GetLableXY(lsEnd[7]).Y;
            PuzzleData.FinalPuzzleY[2][1] = GetLableXY(lsEnd[8]).Y;
            PuzzleData.FinalPuzzleY[2][2] = GetLableXY(lsEnd[0]).Y;

        }
        private Point GetLableLocation(int num, int index)
        {
            Point pp = GetSetPoint(index);

            switch (num)
            {
                case 0:
                    PuzzleData.xBeginPuzzle[2][2] = pp.X;
                    PuzzleData.yBeginPuzzle[2][2] = pp.Y;
                    SetLocation(this.label0, pp.X, pp.Y);
                    return pp;
                case 1:
                    PuzzleData.xBeginPuzzle[0][0] = pp.X;
                    PuzzleData.yBeginPuzzle[0][0] = pp.Y;
                    SetLocation(this.label1, pp.X, pp.Y);
                    return pp;
                case 2:
                    PuzzleData.xBeginPuzzle[0][1] = pp.X;
                    PuzzleData.yBeginPuzzle[0][1] = pp.Y;
                    SetLocation(this.label2, pp.X, pp.Y);
                    return pp;
                case 3:
                    PuzzleData.xBeginPuzzle[0][2] = pp.X;
                    PuzzleData.yBeginPuzzle[0][2] = pp.Y;
                    SetLocation(this.label3, pp.X, pp.Y);
                    return pp;
                case 4:
                    PuzzleData.xBeginPuzzle[1][0] = pp.X;
                    PuzzleData.yBeginPuzzle[1][0] = pp.Y;
                    SetLocation(this.label4, pp.X, pp.Y);
                    return pp;
                case 5:
                    PuzzleData.xBeginPuzzle[1][1] = pp.X;
                    PuzzleData.yBeginPuzzle[1][1] = pp.Y;
                    SetLocation(this.label5, pp.X, pp.Y);
                    return pp;
                case 6:
                    PuzzleData.xBeginPuzzle[1][2] = pp.X;
                    PuzzleData.yBeginPuzzle[1][2] = pp.Y;
                    SetLocation(this.label6, pp.X, pp.Y);
                    return pp;
                case 7:
                    PuzzleData.xBeginPuzzle[2][0] = pp.X;
                    PuzzleData.yBeginPuzzle[2][0] = pp.Y;
                    SetLocation(this.label7, pp.X, pp.Y);
                    return pp;
                case 8:
                    PuzzleData.xBeginPuzzle[2][1] = pp.X;
                    PuzzleData.yBeginPuzzle[2][1] = pp.Y;
                    SetLocation(this.label8, pp.X, pp.Y);
                    return pp;
                default:
                    return new Point(0, 0);
            }
        }
        private Point GetLableXY(int num)
        {
            switch (num)
            {
                case 0:
                    return new Point(0, 0);
                case 1:
                    return new Point(100, 0);
                case 2:
                    return new Point(200, 0);
                case 3:
                    return new Point(0, 100);
                case 4:
                    return new Point(100, 100);
                case 5:
                    return new Point(200, 100);
                case 6:
                    return new Point(0, 200);
                case 7:
                    return new Point(100, 200);
                case 8:
                    return new Point(200, 200);
                default:
                    return new Point(0, 0);
            }
        }
        private Point GetSetPoint(int index)
        {
            switch (index)
            {
                case 0:
                    return new Point(0, 0);
                case 1:
                    return new Point(100, 0);
                case 2:
                    return new Point(200, 0);
                case 3:
                    return new Point(0, 100);
                case 4:
                    return new Point(100, 100);
                case 5:
                    return new Point(200, 100);
                case 6:
                    return new Point(0, 200);
                case 7:
                    return new Point(100, 200);
                case 8:
                    return new Point(200, 200);
                default:
                    return new Point(0, 0);
            }
        }
        private bool IsNum(string s)
        {
            List<char> ls = new List<char>();
            foreach (char item in s)
            {
                if (char.IsNumber(item) == false || item.ToString().Trim() == "9")
                {
                    return false;
                }
                else
                {
                    if (ls.Contains(item) == false)
                    {
                        ls.Add(item);
                    }
                }
            }
            if (ls.Count == s.Length && s.Length == 9)
            {
                return true;
            }
            return false;
        }
    }
}
