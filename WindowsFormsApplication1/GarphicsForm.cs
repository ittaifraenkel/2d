using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace WindowsFormsApplication1
{
    class GarphicsForm: Form
    {
        Panel pa;
        Button[] bu;
        TextBox[] te;
        Label[] la;
        CheckBox che;
        Graphics g;
        int i = 0;
        float[][] jagarr;
        float[] spinc = new float[2];
        bool spin = false;
        public GarphicsForm()
        {
            bu = new Button[5];
            bu[0] = new Button();
            bu[1] = new Button();
            bu[2] = new Button();
            bu[3] = new Button();
            bu[4] = new Button();
            la = new Label[6];
            la[0] = new Label();
            la[1] = new Label();
            la[2] = new Label();
            la[3] = new Label();
            la[4] = new Label();
            la[5] = new Label();
            te = new TextBox[5];
            te[0] = new TextBox();
            te[1] = new TextBox();
            te[2] = new TextBox();
            te[3] = new TextBox();
            te[4] = new TextBox();
            pa = new Panel();

            this.Size = new Size(937, 547);
            this.BackColor = Color.LightBlue;
            this.Text = "2D SHAPE";

            bu[0].Location = new Point(228, 15);
            bu[0].Size = new Size(75, 23);
            bu[0].BackColor = Color.Blue;
            bu[0].ForeColor = Color.White;
            bu[0].Text = "move shape";
            bu[0].Enabled = false;
            bu[0].Click += new EventHandler(button1_Click);
            bu[1].Location = new Point(16, 482);
            bu[1].Size = new Size(883, 23);
            bu[1].BackColor = Color.Blue;
            bu[1].ForeColor = Color.White;
            bu[1].Text = "clear panel";
            bu[1].Click += new EventHandler(button2_Click);
            bu[2].Location = new Point(554, 15);
            bu[2].Size = new Size(75, 23);
            bu[2].Enabled = false;
            bu[2].BackColor = Color.Blue;
            bu[2].ForeColor = Color.White;
            bu[2].Text = "resize shape";
            bu[2].Click += new EventHandler(button3_Click);
            bu[3].Location = new Point(809, 15);
            bu[3].Size = new Size(75, 23);
            bu[3].Enabled = false;
            bu[3].BackColor = Color.Blue;
            bu[3].ForeColor = Color.White;
            bu[3].Text = "spin shape";
            bu[3].Click += new EventHandler(button4_Click);
            bu[4].Location = new Point(16, 65);
            bu[4].Size = new Size(883, 23);
            bu[4].BackColor = Color.Blue;
            bu[4].ForeColor = Color.White;
            bu[4].Text = "finish shape";
            bu[4].Click += new EventHandler(button5_Click);

            te[0].Size = new Size(100, 20);
            te[0].Location = new Point(16, 17);
            te[1].Size = new Size(100, 20);
            te[1].Location = new Point(122, 17);
            te[2].Size = new Size(100, 20);
            te[2].Location = new Point(342, 17);
            te[3].Size = new Size(100, 20);
            te[3].Location = new Point(448, 17);
            te[4].Size = new Size(100, 20);
            te[4].Location = new Point(703, 17);

            la[0].Location = new Point(361, 49);
            la[0].Text = "PRESS FINISH SHAPE TO MOVE SHAPE";
            la[0].Size = new Size(220, 13);
            la[1].Location = new Point(58, 1);
            la[1].Text = "X";
            la[1].Size = new Size(14, 13);
            la[2].Location = new Point(163, 1);
            la[2].Text = "Y";
            la[2].Size = new Size(14, 13);
            la[3].Location = new Point(385, 1);
            la[3].Text = "X";
            la[3].Size = new Size(14, 13);
            la[4].Location = new Point(490, 1);
            la[4].Text = "Y";
            la[4].Size = new Size(14, 13);
            la[5].Location = new Point(732, 1);
            la[5].Text = "degrees";
            la[5].Size = new Size(45, 13);


            che = new CheckBox();
            che.Location = new Point(0, 0);
            che.Text = "close shape";

            pa.Size = new Size(883, 382);
            pa.Location = new Point(16, 94);
            pa.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            pa.BackColor = Color.White;
            pa.MouseDown += pa_MouseDown;

            this.Controls.Add(pa);
            this.Controls.Add(bu[0]);
            this.Controls.Add(bu[1]);
            this.Controls.Add(bu[2]);
            this.Controls.Add(bu[3]);
            this.Controls.Add(bu[4]);
            this.Controls.Add(la[0]);
            this.Controls.Add(la[1]);
            this.Controls.Add(la[2]);
            this.Controls.Add(la[3]);
            this.Controls.Add(la[4]);
            this.Controls.Add(la[5]);
            this.Controls.Add(te[0]);
            this.Controls.Add(te[1]);
            this.Controls.Add(te[2]);
            this.Controls.Add(te[3]);
            this.Controls.Add(te[4]);
            pa.Controls.Add(che);

            jagarr = new float[2][];
            jagarr[0] = new float[2];
            jagarr[1] = new float[2];
            g = pa.CreateGraphics();

            MessageBox.Show("Welcome to 2D Drawing. Click on the Panel to Draw");
        }
        static void MatMul(float[] a, float[,] b, float[] ans)
        {
            for (int f = 0; f < b.GetLength(1); f++)
            {
                for (int c = 0; c < b.GetLength(0); c++)
                {
                    ans[f] += a[c] * b[c, f];
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            bool t = true;
            try
            {
                float.Parse(te[0].Text);
                float.Parse(te[1].Text);
            }
            catch
            {
                t = false;
            }
            int f;
            if (t)
            {
                g.Clear(pa.BackColor);
                float tx = float.Parse(te[0].Text);
                float ty = float.Parse(te[1].Text);
                float[] arr1 = new float[]
                {
                    jagarr[0][0], jagarr[0][1], 1   
                };
                float[,] arr2 = new float[,]
                {
                    {1,0,0},
                    {0,1,0},
                    {tx, ty, 1}
                };
                float[] ans = new float[arr1.Length];
                MatMul(arr1, arr2, ans);
                jagarr[0][0] = ans[0];
                jagarr[0][1] = ans[1];
                ans[0] = 0;
                ans[1] = 0;
                ans[2] = 0;
                for (f = 1; f < jagarr.Length - 1; f++)
                {
                    arr1 = new float[]
                {
                    jagarr[f][0], jagarr[f][1], 1   
                };
                    MatMul(arr1, arr2, ans);
                    jagarr[f][0] = ans[0];
                    jagarr[f][1] = ans[1];
                    ans[0] = 0;
                    ans[1] = 0;
                    ans[2] = 0;
                    g.DrawLine(Pens.Red, jagarr[f - 1][0], jagarr[f - 1][1], jagarr[f][0], jagarr[f][1]);
                }
                g.DrawLine(Pens.Red, jagarr[f - 1][0], jagarr[f - 1][1], jagarr[0][0], jagarr[0][1]);
            }
            else
            {
                MessageBox.Show("must enter values");
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            g.Clear(pa.BackColor);
            i = 0;
            spin = false;
            bu[4].Text = "finish shape";
            la[0].Text = "PRESS FINISH SHAPE TO MOVE SHAPE";
            bu[0].Enabled = false;
            bu[2].Enabled = false;
            bu[3].Enabled = false;
            jagarr = new float[2][];
            jagarr[0] = new float[2];
            jagarr[1] = new float[2];
        }
        private void button3_Click(object sender, EventArgs e)
        {
            bool t = true;
            try
            {
                float.Parse(te[2].Text);
                float.Parse(te[3].Text);
            }
            catch
            {
                t = false;
            }
            if (t)
            {
                g.Clear(pa.BackColor);
                float sx = float.Parse(te[2].Text);
                float sy = float.Parse(te[3].Text);
                int f;
                float[] center2 = new float[2];
                float[] center1 = new float[2];
                for (int x = 0; x < jagarr.Length; x++)
                {
                    center1[0] += jagarr[x][0];
                    center1[1] += jagarr[x][1];
                }
                center1[0] = center1[0] / (jagarr.Length - 1);
                center1[1] = center1[1] / (jagarr.Length - 1);
                float[] arr1 = new float[3];
                float[,] arr2 = new float[,]
                {
                    {sx,0,0},
                    {0,sy,0},
                    {0, 0, 1}
                };
                float[] ans = new float[arr1.Length];
                for (f = 0; f < jagarr.Length - 1; f++)
                {
                    arr1 = new float[]
                {
                    jagarr[f][0], jagarr[f][1], 1   
                };
                    MatMul(arr1, arr2, ans);
                    jagarr[f][0] = ans[0];
                    jagarr[f][1] = ans[1];
                    ans[0] = 0;
                    ans[1] = 0;
                    ans[2] = 0;
                }
                for (int x = 0; x < jagarr.Length; x++)
                {
                    center2[0] += jagarr[x][0];
                    center2[1] += jagarr[x][1];
                }
                center2[0] = center2[0] / (jagarr.Length - 1);
                center2[1] = center2[1] / (jagarr.Length - 1);
                jagarr[0][0] = jagarr[0][0] - (center2[0] - center1[0]);
                jagarr[0][1] = jagarr[0][1] - (center2[1] - center1[1]);
                for (f = 1; f < jagarr.Length - 1; f++)
                {
                    jagarr[f][0] = jagarr[f][0] - (center2[0] - center1[0]);
                    jagarr[f][1] = jagarr[f][1] - (center2[1] - center1[1]);
                    g.DrawLine(Pens.Red, jagarr[f - 1][0], jagarr[f - 1][1], jagarr[f][0], jagarr[f][1]);
                }
                g.DrawLine(Pens.Red, jagarr[f - 1][0], jagarr[f - 1][1], jagarr[0][0], jagarr[0][1]);
            }
            else
            {
                MessageBox.Show("must enter values");
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            bool t = true;
            try
            {
                float.Parse(te[4].Text);
            }
            catch
            {
                t = false;
            }
            if (t)
            {
                if (!(spinc[0] == 0 && spinc[1] == 0))
                {
                    g.Clear(pa.BackColor);
                    float deg = DegToRad(float.Parse(te[4].Text));
                    double b = Math.Cos(deg);
                    b = Math.Sin(deg);
                    for (int y = 0; y < jagarr.Length - 1; y++)
                    {
                        jagarr[y][0] -= spinc[0];
                        jagarr[y][1] -= spinc[1];
                        float tempx = jagarr[y][0];
                        float tempy = jagarr[y][1];
                        jagarr[y][0] = (float)(tempx * Math.Cos(deg) - tempy * Math.Sin(deg));
                        jagarr[y][1] = (float)(tempy * Math.Cos(deg) + tempx * Math.Sin(deg));
                        jagarr[y][0] += spinc[0];
                        jagarr[y][1] += spinc[1];
                    }
                    int f;
                    for (f = 1; f < jagarr.Length - 1; f++)
                    {
                        g.DrawLine(Pens.Red, jagarr[f - 1][0], jagarr[f - 1][1], jagarr[f][0], jagarr[f][1]);
                    }
                    g.DrawLine(Pens.Red, jagarr[f - 1][0], jagarr[f - 1][1], jagarr[0][0], jagarr[0][1]);
                }
                else
                {
                    MessageBox.Show("you must choose a point to turn around");
                }
            }
            else
            {
                MessageBox.Show("must enter values");
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            spin = !spin;
            if (spin == true)
            {
                bu[4].Text = "change shape";
                la[0].Location = new Point(300, 49);
                la[0].Text = "CLICK ON THE POINT YOU WANT TO SPIN THE SHAPE AROUND";
                bu[0].Enabled = true;
                bu[2].Enabled = true;
                bu[3].Enabled = true;
            }
            if (spin == false)
            {
                bu[4].Text = "finish shape";
                la[0].Location = new Point(361, 49);
                la[0].Text = "PRESS FINISH SHAPE TO MOVE SHAPE";
                bu[0].Enabled = false;
                bu[2].Enabled = false;
                bu[3].Enabled = false;
            }
        }
       static float DegToRad(float degrees)
        {
            return (float)(degrees * Math.PI / 180);
        }
        private void pa_MouseDown(object sender, MouseEventArgs e)
        {
            if (!spin)
            {
                if (i > 0)
                {
                    jagarr[i][0] = e.X;
                    jagarr[i][1] = e.Y;
                    g.DrawLine(Pens.Red, jagarr[i - 1][0], jagarr[i - 1][1], jagarr[i][0], jagarr[i][1]);
                    i++;
                    Array.Resize(ref jagarr, jagarr.Length + 1);
                    jagarr[i] = new float[2];
                }
                else
                {
                    jagarr[i][0] = e.X;
                    jagarr[i][1] = e.Y;
                    i++;
                }
                if (i > 2 && che.Checked == true)
                {
                    if (i > 3)
                    {
                        g.DrawLine(new Pen(pa.BackColor), jagarr[i - 2][0], jagarr[i - 2][1], jagarr[0][0], jagarr[0][1]);
                    }
                    g.DrawLine(Pens.Red, jagarr[i - 1][0], jagarr[i - 1][1], jagarr[0][0], jagarr[0][1]);
                }
            }
            else
            {
                spinc[0] = e.X;
                spinc[1] = e.Y;
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // GarphicsForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Name = "GarphicsForm";
            this.Load += new System.EventHandler(this.GarphicsForm_Load);
            this.ResumeLayout(false);

        }

        private void GarphicsForm_Load(object sender, EventArgs e)
        {

        }
    }
}
