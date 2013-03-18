using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace WindowsFormsApplication1
{
    class TableForm : Form
    {
        Panel pa;
        Button[] bu;
        TextBox te;
        Label[] la;
        int b = 0;
        int c = -1;
        float zoom = 1;
        public TableForm()
        {
            this.Size = new Size(1000, 727);
            this.BackColor = Color.LightGreen;
            this.Text = "2D GRAPH";

            pa = new Panel();
            pa.Size = new Size(788, 674);
            pa.Location = new Point(193, 2);
            pa.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            pa.BackColor = Color.White;

            bu = new Button[4];
            bu[0] = new Button();
            bu[1] = new Button();
            bu[2] = new Button();
            bu[3] = new Button();
            bu[0].Location = new Point(100, 145);
            bu[0].Click += new EventHandler(button1_Click);
            bu[0].BackColor = Color.Yellow;
            bu[0].Text = "show graph";
            bu[1].Location = new Point(19, 145);
            bu[1].Click += new EventHandler(button2_Click);
            bu[1].BackColor = Color.Yellow;
            bu[1].Text = "show axis";
            bu[2].Location = new Point(19, 178);
            bu[2].Click += new EventHandler(button3_Click);
            bu[2].BackColor = Color.LightBlue;
            bu[2].Text = "zoom in";
            bu[3].Location = new Point(100, 178);
            bu[3].Click += new EventHandler(button4_Click);
            bu[3].BackColor = Color.LightBlue;
            bu[3].Text = "zoom out";

            la = new Label[3];
            la[0] = new Label();
            la[1] = new Label();
            la[2] = new Label();
            la[0].Location = new Point(49, 103);
            la[0].Size = new Size(95, 13);
            la[0].Text = "enter any function:";
            la[1].Location = new Point(0, 30);
            la[1].Size = new Size(1000, 50);
            la[1].ForeColor = Color.Red;
            la[1].Text = "2D GRAPH";
            la[1].Font = new Font("Segoe Print", 23.0F);
            la[2].Location = new Point(0, 210);
            la[2].Size = new Size(500, 30);
            la[2].ForeColor = Color.Blue;
            la[2].Text = "scale (distance between lines) = 10";
            la[2].Font = new Font("Segoe Print", 7.0F);

            te = new TextBox();
            te.Size = new Size(175, 20);
            te.Location = new Point(12, 119);

            this.Controls.Add(pa);
            this.Controls.Add(bu[0]);
            this.Controls.Add(bu[1]);
            this.Controls.Add(bu[2]);
            this.Controls.Add(bu[3]);
            this.Controls.Add(la[0]);
            this.Controls.Add(la[1]);
            this.Controls.Add(la[2]);
            this.Controls.Add(te);

            MessageBox.Show("Welcome to 2D Graph Drawer. Enter the Function You Want to See, Then Click Show Graph");
        }

        public byte Check(string pro)
        {
            int i, g = 0;
            if (pro == "")
            {
                return 0;
            }
            for (i = 0; i < pro.Length - 1; i++)
            {
                if ((pro[i] == '+' || pro[i] == '-' || pro[i] == '*' || pro[i] == '/' || pro[i] == '^' || pro[i] == '!') && (pro[i + 1] == '+' || pro[i + 1] == '*' || pro[i + 1] == '/' || pro[i + 1] == '^' || pro[i] == '!'))
                {
                    return 0;
                }
            }
            if (pro[0] == '/' || pro[0] == '*' || pro[0] == '^' || pro[0] == '!' || pro[pro.Length - 1] == '/' || pro[pro.Length - 1] == '*' || pro[pro.Length - 1] == '^' || pro[pro.Length - 1] == '!' || pro[pro.Length - 1] == '+')
            {
                return 0;
            }
            for (i = 0; i < pro.Length; i++)
            {
                if (pro[i] == '|')
                    g++;
            }
            if (g % 2 != 0)
            {
                return 0;
            }
            g = 0;
            for (i = 0; i < pro.Length; i++)
            {
                if (pro[i] == '(')
                {
                    g++;
                }
                if (pro[i] == ')')
                {
                    g--;
                }
                if (g < 0)
                    return 0;
            }
            if (g != 0)
                return 0;
            for (i = pro.Length - 1; i > -1; i--)
            {
                if ((int)pro[i] < 47 || (int)pro[i] > 58)
                {
                    if (pro[i] != '+' && pro[i] != '-' && pro[i] != '*' && pro[i] != '/' && pro[i] != '(' && pro[i] != ')' && pro[i] != '^' && pro[i] != '!' && pro[i] != '|' && pro[i] != 'x')
                    {
                        if (i >= 2)
                        {
                            if (pro[i] == 's' && pro[i - 1] == 'o' && pro[i - 2] == 'c')
                            {
                                i -= 3;
                            }
                            else if (pro[i] == 'n' && pro[i - 1] == 'i' && pro[i - 2] == 's')
                            {
                                i -= 3;
                            }
                            else if (pro[i] == 'n' && pro[i - 1] == 'a' && pro[i - 2] == 't')
                            {
                                i -= 3;
                            }
                            else
                            {
                                return 0;
                            }
                        }
                        else
                        {
                            return 0;
                        }
                    }
                }
            }
            return 1;
        }
        public float Solve(string pro, float x)
        {
            int i, g, sum = 1;
            for (i = 0; i < pro.Length; i++)
            {
                if (pro[i] == 'x')
                {
                    return Solve(pro.Substring(0, i) + '(' + x.ToString() + ')' + pro.Substring(i + 1), x);
                }
            }
            for (i = 1; i < pro.Length; i++)
            {
                if (pro[i] == '(')
                {
                    if ((int)pro[i - 1] < 58 && (int)pro[i - 1] > 47)
                    {
                        return Solve(pro.Substring(0, i) + '*' + pro.Substring(i), x);
                    }
                }
            }
            for (i = 0; i < pro.Length; i++)
            {
                if (pro[i] == ')')
                {
                    for (g = i; g > -1; g--)
                    {
                        if (pro[g] == '(')
                        {
                            return Solve(pro.Substring(0, g) + Solve(pro.Substring(g + 1, i - g - 1), x).ToString() + pro.Substring(i + 1), x);
                        }
                    }
                }
            }
            for (i = 1; i < pro.Length; i++)
            {
                if (pro[i] == '|' && pro[i - 1] != '+' && pro[i - 1] != '-' && pro[i - 1] != '/' && pro[i - 1] != '*' && pro[i - 1] != '^' && pro[i - 1] != '!')
                {
                    for (g = i - 1; g > -1; g--)
                    {
                        if (pro[g] == '|')
                        {
                            return Solve(pro.Substring(0, g) + Math.Abs(Solve(pro.Substring(g + 1, i - g - 1), x)).ToString() + pro.Substring(i + 1), x);
                        }
                    }
                }
            }
            for (i = pro.Length - 1; i > 0; i--)
            {
                if (pro[i] == '+')
                {
                    return Solve(pro.Substring(0, i), x) + Solve(pro.Substring(i + 1), x);
                }
                if (pro[i] == '-')
                {
                    if (pro[i - 1] == '*' || pro[i - 1] == '/' || pro[i - 1] == '^')
                    {
                        i--;
                        continue;
                    }
                    if (pro[i - 1] == '-')
                    {
                        return Solve(pro.Substring(0, i - 1), x) - Solve(pro.Substring(i), x);
                    }
                    if (pro[i - 1] == '+')
                    {
                        return Solve(pro.Substring(0, i - 1), x) + Solve(pro.Substring(i), x);
                    }
                    if (pro[i - 1] == 's')
                    {
                        i -= 3;
                        continue;
                    }
                    if (pro[i - 1] == 'n')
                    {
                        i -= 3;
                        continue;
                    }
                    return Solve(pro.Substring(0, i), x) - Solve(pro.Substring(i + 1), x);
                }
            }
            for (i = pro.Length - 1; i > -1; i--)
            {
                if (pro[i] == '*')
                {
                    return Solve(pro.Substring(0, i), x) * Solve(pro.Substring(i + 1), x);
                }
                if (pro[i] == '/')
                {
                    return Solve(pro.Substring(0, i), x) / Solve(pro.Substring(i + 1), x);
                }
            }
            for (i = pro.Length - 1; i > -1; i--)
            {
                if (pro[i] == '^')
                {
                    return (float)Math.Pow(Solve(pro.Substring(0, i), x), Solve(pro.Substring(i + 1), x));
                }
                if (pro[i] == '!')
                {
                    for (g = 2; g < Solve(pro.Substring(0, i), x) + 1; g++)
                    {
                        sum *= g;
                    }
                    return sum;
                }
            }
            for (i = pro.Length - 1; i > -1; i--)
            {
                if (pro[i] == 'c' && pro[i + 1] == 'o')
                {
                    return (float)Math.Cos(Solve(pro.Substring(i + 3), x));
                }
                if (pro[i] == 's' && pro[i + 1] == 'i')
                {
                    return (float)Math.Sin(Solve(pro.Substring(i + 3), x));
                }
                if (pro[i] == 't' && pro[i + 1] == 'a')
                {
                    return (float)Math.Tan(Math.PI * Solve(pro.Substring(i + 3), x) / 360);
                }
            }
            return float.Parse(pro);
        }
        public float Solve1(string pro, float x)
        {
            float ans = Solve(pro, x);
            if (ans > 10000)
            {
                ans = 10000;
            }
            if (ans < -10000)
            {
                ans = -10000;
            }
            return ans;
        }
        public void ShowAxis()
        {
            Graphics a = pa.CreateGraphics();
            a.DrawLine(Pens.Black, pa.Width / 2, 0, pa.Width / 2, pa.Height);
            a.DrawLine(Pens.Black, 0, pa.Height / 2, pa.Width, pa.Height / 2);
        }
        public void ShowScales(float zoom)
        {
            Graphics a = pa.CreateGraphics();
            for (float i = (float)pa.Width / 2; i < pa.Width; i += 10 / zoom)
            {
                a.DrawLine(Pens.Black, i, pa.Height / 2 - 5, i, pa.Height / 2 + 5);
                a.DrawLine(Pens.Black, pa.Width - i, pa.Height / 2 - 5, pa.Width - i, pa.Height / 2 + 5);
            }
            for (float i = (float)pa.Height / 2; i < pa.Height; i += 10 / zoom)
            {
                a.DrawLine(Pens.Black, pa.Width / 2 - 5, i, pa.Width / 2 + 5, i);
                a.DrawLine(Pens.Black, pa.Width / 2 - 5, pa.Height - i, pa.Width / 2 + 5, pa.Height - i);
            }
        }
        public void ShowGrid(float zoom)
        {
            Graphics a = pa.CreateGraphics();
            for (float i = (float)pa.Width / 2; i < pa.Width; i += 10 / zoom)
            {
                a.DrawLine(Pens.Aquamarine, i, 0, i, pa.Height);
                a.DrawLine(Pens.Aquamarine, pa.Width - i, 0, pa.Width - i, pa.Height);
            }
            for (float i = (float)pa.Height / 2; i < pa.Height; i += 10 / zoom)
            {
                a.DrawLine(Pens.Aquamarine, 0, i, pa.Width, i);
                a.DrawLine(Pens.Aquamarine, 0, pa.Height - i, pa.Width, pa.Height - i);
            }
        }
        public bool DrawGraph(float zoom)
        {
            Graphics a = pa.CreateGraphics();
            float ya, yb;
            string prob = te.Text;
            prob = prob.Replace(" ", "");
            if (prob[0] == '-')
            {
                prob = '0' + prob;
            }
            int i = Check(prob);
            if (i == 0)
            {
                MessageBox.Show("Error in function, try again");
                return false;
            }
            else
            {
                for (i = -(pa.Width) / 2; i < (pa.Width) / 2 - 1; i++)
                {
                    ya = Solve1(prob, (i) * zoom) * 1 / zoom;
                    yb = Solve1(prob, (i + 1) * zoom) * 1 / zoom;
                    if (float.IsNaN(ya) || float.IsNaN(yb))
                    {
                        continue;
                    }
                    a.DrawLine(Pens.Red, (i + (pa.Width) / 2), (-ya + (pa.Height) / 2), (i + 1 + (pa.Width) / 2), (-yb + (pa.Height) / 2));
                }
            }
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Graphics a = pa.CreateGraphics();
            c = -c;
            if (c > 0)
            {
                bool x = DrawGraph(zoom);
                if (x)
                {
                    bu[0].Text = ("hide graph");
                }
                else
                {
                    c = -c;
                }
            }
            if (c < 0)
            {
                a.Clear(pa.BackColor);
                if (b > 0)
                {
                    ShowGrid(zoom);
                    ShowAxis();
                    ShowScales(zoom);
                }
                if (b < 0)
                {
                    ShowAxis();
                    ShowScales(zoom);
                }
                bu[0].Text = ("show graph");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Graphics a = pa.CreateGraphics();
            if (b == 0)
            {
                b = 1;
            }
            b = -b;
            if (b > 0)
            {
                ShowGrid(zoom);
                ShowAxis();
                ShowScales(zoom);
                if (c > 0)
                {
                    DrawGraph(zoom);
                }
                bu[1].Text = ("hide grid");
            }
            if (b < 0)
            {
                a.Clear(pa.BackColor);
                ShowAxis();
                ShowScales(zoom);
                if (c > 0)
                {
                    DrawGraph(zoom);
                }
                bu[1].Text = ("show grid");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Graphics a = pa.CreateGraphics();
            if (zoom<0.0625)
            {
                MessageBox.Show("cannot zoom in further");
            }
            else
            {
                a.Clear(pa.BackColor);
                zoom = zoom / 2;
                if (b > 0)
                {
                    ShowGrid(zoom);
                    ShowAxis();
                    ShowScales(zoom);
                    if (c > 0)
                    {
                        DrawGraph(zoom);
                    }
                }
                if (b < 0)
                {
                    ShowAxis();
                    ShowScales(zoom);
                    if (c > 0)
                    {
                        DrawGraph(zoom);
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Graphics a = pa.CreateGraphics();
            if (10 / zoom - 5 < 1)
            {
                MessageBox.Show("cannot zoom out further");
            }
            else
            {
                zoom = zoom * 2;
                a.Clear(pa.BackColor);
                if (b > 0)
                {
                    ShowGrid(zoom);
                    ShowAxis();
                    ShowScales(zoom);
                    if (c > 0)
                    {
                        DrawGraph(zoom);
                    }
                }
                if (b < 0)
                {
                    ShowAxis();
                    ShowScales(zoom);
                    if (c > 0)
                    {
                        DrawGraph(zoom);
                    }
                }
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // TableForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "TableForm";
            this.Load += new System.EventHandler(this.TableForm_Load);
            this.ResumeLayout(false);

        }

        private void TableForm_Load(object sender, EventArgs e)
        {

        }
    }
}

