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
        static List<SudokuProblem> SudokuProblems = new List<SudokuProblem>();

        public Sudoku()
        {
            InitializeComponent();
            CreateSudokuProblems();
            PopulateSudokuTable();
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

        public void CreateSudokuProblems()
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
            Button button = (Button)sender;
            numberSelected = button.Text;
            if (radioButton1.Checked)
            {
                radioButton1_Click(sender, e);
            }   
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
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked && EraserSelected)
            {
                radioButton1.Checked = false;
                EraserSelected = false;
            }
            else
            {
                radioButton1.Checked = true;
                EraserSelected = true;
            }
        }

        private void Restart_Click(object sender, EventArgs e)
        {
            PopulateSudokuTable();
        }
    }
}
