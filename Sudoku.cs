using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sudoku
{
    public partial class Sudoku : Form
    {
        string numberSelected = "";
        bool EraserSelected = false;
        bool CheatSelected = false;
        public static List<SudokuProblem> SudokuProblems = new List<SudokuProblem>();

        public Sudoku()
        {
            InitializeComponent();
            CreateSudokuProblems();
            PopulateSudokuTable(false);
            //EulerSolution();
            
        }

        public void PopulateSudokuTable(bool restore)
        {
            int position = int.Parse((numericUpDown1.Value - 1).ToString());
            int i = 0;
            string[] myArr = new string[81];
            if (restore == true)
            {
                myArr = (string[])SudokuProblems[position].usersInputArr.Clone();
            }
            else
            {
                myArr = (string[])SudokuProblems[position].problemArr.Clone();
            }
            
            foreach (Button button in ReturnListOfButtons())
            {
                string squareValue = myArr[i];
                
                if (squareValue != "0")
                {
                    button.Text = squareValue;
                    if (SudokuProblems[position].problemArr[i] == myArr[i])
                    {
                        button.Font = new Font(button.Font, FontStyle.Bold);
                    }
                    else
                    {
                        button.Font = new Font(button.Font, FontStyle.Regular);
                    }   
                }
                else
                {
                    button.Text = "";
                    button.Font = new Font(button.Font, FontStyle.Regular);
                }
                i++;
            }
        }

        public static void CreateSudokuProblems()
        {
            List<string> cLines = System.IO.File.ReadAllLines
                (@"C:\Users\Ben\Desktop\C#\euler\euler96.txt").ToList();
            List<string> sudoku = new List<string>();
            int position = 0;
            foreach (var line in cLines)
            {
                if (Double.TryParse(line,out double temp) == false)
                {
                    position++;
                    sudoku.Clear();
                }
                else
                {
                    sudoku.Add(line);
                }
                if (sudoku.Count() == 9)
                {
                    SudokuProblems.Add(new SudokuProblem(sudoku, position));
                }
            }
        }

        private void NumberSelect_Click(object sender, EventArgs e)
        {
            ResetInputs();
            Button button = (Button)sender;
            numberSelected = button.Text;
        }

        private void Square_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (button.Font.Bold)
            {
                return;
            }
            var position = tableLayoutPanel.GetPositionFromControl(button);
            int positionLookup = position.Column + (position.Row * 9);

            if (EraserSelected)
            {
                button.Text = "";
            }
            else if (CheatSelected)
            {
                button.Text = SudokuProblems[int.Parse
                    ((numericUpDown1.Value - 1).ToString())].answersArr[positionLookup];
            }
            else if (numberSelected != "")
            {
                button.Text = numberSelected;
                numberSelected = "";
            }
            
            
            SudokuProblems[int.Parse((numericUpDown1.Value - 1).ToString())].usersInputArr
                [positionLookup] = button.Text;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown1.Value == 0)
            {
                numericUpDown1.Value = 1;
            }
            else if (numericUpDown1.Value == 51)
            {
                numericUpDown1.Value = 50;
            }
            //save state
            PopulateSudokuTable(true);
            ResetInputs();
        }

        private void erase_Click(object sender, EventArgs e)
        {
            if (CheatSelected)
            {
                cheat.Checked = false;
                CheatSelected = false;
            }
            if (erase.Checked && EraserSelected)
            {
                erase.Checked = false;
                EraserSelected = false;
            }
            else
            {
                erase.Checked = true;
                EraserSelected = true;
            }
        }

        private void Restart_Click(object sender, EventArgs e)
        {
            //add message box form
            EraserSelected = true;
            foreach (Button button in ReturnListOfButtons())
            {
                Square_Click(button, e);
            }
            ResetInputs();
        }

        private void cheat_Click(object sender, EventArgs e)
        {
            if (EraserSelected)
            {
                erase.Checked = false;
                EraserSelected = false;
            }
            if (cheat.Checked && CheatSelected)
            {
                cheat.Checked = false;
                CheatSelected = false;
            }
            else
            {
                cheat.Checked = true;
                CheatSelected = true;
            }
        }

        private void ResetInputs()
        {
            erase.Checked = false;
            cheat.Checked = false;
            numberSelected = "";
            EraserSelected = false;
            CheatSelected = false;
        }

        private void EulerSolution()
        {
            int indx = 0;
            long temp2 = 0;
            foreach (var puzzle in SudokuProblems)
            {
                string temp = null;
                for (int i = 0; i < 3; i++)
                {
                    temp += SudokuProblems[indx].answersArr[i];
                }
                temp2 += long.Parse(temp);
                indx++;
            }
            Console.WriteLine();
        }

        private void btn_check_Click(object sender, EventArgs e)
        {
            if (isCorrectSolution())
            {
                MessageBox.Show("Congratulations! That is the correct solution!");
            }
            else
            {
                MessageBox.Show("That is not the correct solution");
            }
        }
        private bool isCorrectSolution()
        {
            List<Button> buttons = ReturnListOfButtons();
            int i = 0;
            foreach (Button button in buttons)
            {
                if (button.Text != SudokuProblems[int.Parse
                    ((numericUpDown1.Value - 1).ToString())].answersArr[i])
                {
                    return false;
                }
                i++;
            }
            return true;
        }
        private List<Button> ReturnListOfButtons()
        {
            List<Button> buttons = new List<Button>();
            for (int i = 0; i < 81; i++)
            {
                var control = tableLayoutPanel.GetControlFromPosition(Square.ReturnColumn(i),
                    Square.ReturnRow(i));
                Button button = (Button)control;
                buttons.Add(button);
            }
            return buttons;
        }

        private void tableLayoutPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics gr = tableLayoutPanel.CreateGraphics();
            Pen myPen = new Pen(Brushes.DarkOliveGreen, 5);

            int lines = tableLayoutPanel.ColumnCount;
            float Xx = 0f;
            float Xy = 0f;
            float Xincrement = tableLayoutPanel.Width / lines;
            float Yx = 0f;
            float Yy = 0f;
            float Yincrement = tableLayoutPanel.Height / lines;

            for (int i = 0; i <= lines; i++)
            {
                if (i % 3 == 0)
                {
                    myPen = new Pen(Brushes.DarkOliveGreen, 5);
                }
                else
                {
                    myPen = new Pen(Brushes.DarkOliveGreen, 2);
                }
                gr.DrawLine(myPen, Xx, Xy, Xx, tableLayoutPanel.Height);
                gr.DrawLine(myPen, Yx, Yy, tableLayoutPanel.Width, Yy);
                Xx += Xincrement;
                Yy += Yincrement;
            }
        }
    }
}
