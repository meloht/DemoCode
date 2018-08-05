using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Puzzle
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void btnAStar_Click(object sender, EventArgs e)
        {
            String message = "Attempting to Solve Puzzle using A*";
            lblStatus.Text=message;            

            AStar.AStarOn = true;
            AStar.solving = true;

            if (AStar.solvePuzzle() == true)
            {
                lblStatus.Text = "Puzzle Solved";
            }
            else
            {
                lblStatus.Text = message;
            }

            UpdatePuzzle(AStar.xTempPuzzle, AStar.yTempPuzzle);
        }
        
        private void btnDfs_Click(object sender, EventArgs e)
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
            String message = "Attempting to Solve Puzzle using Progressive DFS...";
            lblStatus.Text = message;

            ProgDFS.ProgDFSOn = true;
            ProgDFS.solving = true;

            if (ProgDFS.solvePuzzle() == true)
            {
                lblStatus.Text = "Puzzle Solved";
            }
            else
            {
                lblStatus.Text = message;
            }

            UpdatePuzzle(ProgDFS.xTempPuzzle, ProgDFS.yTempPuzzle);     
        }

        private void btnPDfs_Click(object sender, EventArgs e)
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
            String message = "Attempting to Solve Puzzle using Progressive DFS...";
            lblStatus.Text = message;

            ProgDFS.ProgDFSOn = true;
            ProgDFS.solving = true;

            if (ProgDFS.solvePuzzle() == true)
            {
                lblStatus.Text = "Puzzle Solved";
            }
            else
            {
                lblStatus.Text = message;
            }

            UpdatePuzzle(ProgDFS.xTempPuzzle, ProgDFS.yTempPuzzle);   
        }

        private void btnWfs_Click(object sender, EventArgs e)
        {

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            PuzzleData.resetPuzzle();
           // SetBegin();
            UpdatePuzzle();
            
            DFS.resetAll();
            ProgDFS.resetAll();
            AStar.resetAll();

            lblStatus.Text = "Puzzle Reset!";
        }

        private void btnStart_Click(object sender, EventArgs e)
        {

            this.BeginInvoke(new Action(SetBegin));
           // SetBegin();
           
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnShuffle_Click(object sender, EventArgs e)
        {
            Shuffle.shuffleOn = true;
            lblStatus.Text = "Shuffling Puzzle, please wait...";

            UpdatePuzzle();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            PuzzleData.loadPuzzleData();
        }
    }
}
