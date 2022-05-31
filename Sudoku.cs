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
            PopulateSudokuTable();
            //EulerSolution();
        }

        public void PopulateSudokuTable()
        {
            foreach (Button button in panel1.Controls)
            {
                button.Text = "";
                button.Font = new Font(button.Font, FontStyle.Regular);
            }
            int position = int.Parse((numericUpDown1.Value - 1).ToString());
            int indx = 80;
            foreach (Button button in panel1.Controls)
            {
                string squareValue = SudokuProblems[position].problemArr[indx];
                if (squareValue != "0")
                {
                    button.Text = squareValue;
                    button.Font = new Font(button.Font, FontStyle.Bold);
                }
                else
                {
                    button.Text = "";
                }
                
                indx--;
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
            if (EraserSelected)
            {
                button.Text = "";
            }
            else if (CheatSelected)
            {
                string name = button.Name;
                name = name.Remove(0, 2);
                int position = int.Parse(name);
                button.Text = SudokuProblems[int.Parse
                    ((numericUpDown1.Value - 1).ToString())].answersArr[position];
            }
            if (numberSelected == "")
            {
                return;
            }
            
            button.Text = numberSelected;
            numberSelected = "";
            foreach (Button item in panel1.Controls)
            {
                string name = item.Name;
                string temp = item.Text;
            }
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
            PopulateSudokuTable();
            ResetInputs();
        }

        private void erase_Click(object sender, EventArgs e)
        {
            if (CheatSelected)
            {
                return;
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
            PopulateSudokuTable();
            ResetInputs();
        }

        private void cheat_Click(object sender, EventArgs e)
        {
            if (EraserSelected)
            {
                return;
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
    }
}
