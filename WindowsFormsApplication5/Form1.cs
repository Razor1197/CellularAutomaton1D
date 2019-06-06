using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication5
{
    public partial class Form1 : Form
    {
        int Vector_size, Number_of_iterations, rule, BC;
        Cell[] Vector;
        List<Cell[]> Iterations_list;
        private Graphics g1;
        bool[] rule_bin = new bool[8];

        public Form1()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try { 
                Vector_size = int.Parse(textBox1.Text);
                Number_of_iterations = int.Parse(textBox2.Text);
                rule = int.Parse(comboBox1.Text);
                if (comboBox2.Text == "Periodyczny")
                {
                    BC = 1;
                }         


            String tmp = Convert.ToString(rule, 2);
            //System.Windows.Forms.MessageBox.Show(tmp);
            for (int i = 0; i < (8 - tmp.Length); i++)
            {
                rule_bin[i] = false;
            }
            for(int i = 0; i<tmp.Length; i++)
            {
                rule_bin[i + (8 - tmp.Length)] = Convert.ToBoolean(tmp[i]-'0');
            }

            Vector = new Cell[Vector_size];
            for (int i = 0; i < Vector_size; i++)
                Vector[i] = new Cell();
            Vector[Vector_size / 2].SetState(true);


            Iterations_list = new List<Cell[]>();
            Iterations_list.Add(Vector);

            for (int i = 1; i < Number_of_iterations; i++)
            {
                Iterations_list.Add(new Cell[Vector_size]);
                    for (int j=0;j< Vector_size; j++)
                      {
                           Iterations_list[i][j] = new Cell();
                      }
            }

            UpdateVector(Iterations_list, rule_bin);
            DrawCells(Iterations_list);


            }
            catch (System.FormatException)
            {
                System.Windows.Forms.MessageBox.Show("Nieprawidłowe wartości - tylko liczby całkowite, lub brak reguły/warunku");
            }

        }
        private void DrawCells(List<Cell[]> Iterations_list)
        {
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g1 = Graphics.FromImage(this.pictureBox1.Image);
           // Pen pen = new Pen(Color.Black, 1);
            SolidBrush brush = new SolidBrush(Color.Red);
            float x = 0, y= 0;

            float RecWidth = (float)pictureBox1.Width / (float)Vector_size ;
            float RecHeight = (float)pictureBox1.Height / (float)Number_of_iterations ;
            // RecWidth < RecHeight ? RecWidth = RecHeight : RecHeight = RecWidth;
            if (RecWidth > RecHeight)
            {
                RecWidth = RecHeight;
            }
            else
            {
                RecHeight = RecWidth;
            }

            for (int i =0; i<Number_of_iterations; i++)
            {

                 for (int j = 0; j < Vector_size; j++)
                 {
                    if(Iterations_list[i][j].GetState()==true)
                    {
                        brush.Color = Color.Red;
                    }
                    else
                    {
                        brush.Color = Color.Gray;
                    }
                      g1.FillRectangle(brush, x, y, RecWidth, RecHeight);
                    //g1.DrawRectangle(pen, x, y, RecWidth, RecHeight);
                    x += RecWidth;
                }
               
                x = 0;
                y += RecHeight;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void UpdateVector(List<Cell[]> Iterations_list,  bool[] rule_bin)
        {
            bool s1, s2, s3;



           for (int i = 1; i<Number_of_iterations; i++)
            {
                for(int j=0; j<Vector_size; j++)
                {
                    if (j == 0)
                    {
                        s1 = Iterations_list[i-1][Vector_size-1].GetState();
                        s2 = Iterations_list[i-1][j].GetState();
                        s3 = Iterations_list[i-1][j+1].GetState();
                    }
                    else if (j==Vector_size-1)
                    {
                        s1 = Iterations_list[i-1][j-1].GetState();
                        s2 = Iterations_list[i-1][j].GetState();
                        s3 = Iterations_list[i-1][0].GetState();
                    }
                    else
                    {
                        s1 = Iterations_list[i - 1][j - 1].GetState();
                        s2 = Iterations_list[i - 1][j].GetState();
                        s3 = Iterations_list[i - 1][j + 1].GetState();
                    }

                    //////////////////////////////////////////////////////
                    
                    if(s1 == true && s2 == true && s3 == true)
                    {
                        Iterations_list[i][j].SetState(rule_bin[0]);
                    }

                    else if (s1 == true && s2 == true && s3 == false)
                    {
                        Iterations_list[i][j].SetState(rule_bin[1]);
                    }

                    else if (s1 == true && s2 == false && s3 == true)
                    {
                        Iterations_list[i][j].SetState(rule_bin[2]);
                    }

                    else if (s1 == true && s2 == false && s3 == false)
                    {
                        Iterations_list[i][j].SetState(rule_bin[3]);
                    }

                    else if (s1 == false && s2 == true && s3 == true)
                    {
                        Iterations_list[i][j].SetState(rule_bin[4]);
                    }

                    else if (s1 == false && s2 == true && s3 == false)
                    {
                        Iterations_list[i][j].SetState(rule_bin[5]);
                    }

                    else if (s1 == false && s2 == false && s3 == true)
                    {
                        Iterations_list[i][j].SetState(rule_bin[6]);
                    }

                    else if (s1 == false && s2 == false && s3 == false)
                    {
                        Iterations_list[i][j].SetState(rule_bin[7]);
                    }

                }
            }


        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
