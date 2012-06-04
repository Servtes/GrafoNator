using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ProyectoGrafos
{
    public partial class Form1 : Form

    {
        private readonly Random _ejeX = new Random((int) (DateTime.Now.Ticks));
        private readonly Random _ejeY = new Random(((int) DateTime.Now.Ticks));

        private readonly char[] _idNodo = {
                                              'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i',
                                              'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q',
                                              'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'
                                          };

        private readonly int[] ejeX = {99, 452, 99, 452, 208, 328, 208, 328};
        private readonly int[] ejeY = {113, 96, 195, 195, 266, 26, 52, 303};

        private int _cantidadNodos = 1;
        public int finX;
        public int finY;
        public int origenX;
        public int origenY;

        public Form1()
        {
            InitializeComponent();
        }

        private void Button1Click(object sender, EventArgs e)
        {
            int auxX = 6;
            int auxY = 14;

            for (int i = 0; i < _cantidadNodos; i++)
            {
                int x = _ejeX.Next(6, 472);
                int y = _ejeY.Next(14, 353);
                textBox1.Text = x.ToString();
                textBox2.Text = y.ToString();
                button1.SendToBack();
                var nodo = new Button
                               {
                                   Name = "nodo" + i,
                                   Size = new Size(25, 25),
                                   Location = new Point(x, y),
                                   Text = _idNodo[i].ToString(),
                                   //Z-Index.sendToBack();
                               };


                if (i == 0 || i%2 == 0)
                {
                    origenX = x;
                    origenY = y;
                }
                else
                {
                    finX = x;
                    finY = y;
                    DibujarArco(origenX, origenY, finX, finY);
                }
            
                if (nodo.Bounds.IntersectsWith(nodo.Bounds))
                {
                    //si pasa por aqui hay colision
                    textBox1.Text = "colision!!";
                }
                // var nodo = new Button {Name = "nodo" + i, Size = new Size(25, 25), Location = new Point(x, y), Text = _idNodo[i].ToString()};
                panel1.Controls.Add(nodo);
            }
        }

        private void NumericUpDown1ValueChanged(object sender, EventArgs e)
        {
            _cantidadNodos = (int) numericUpDown1.Value;
            dataGridView.RowCount = _cantidadNodos;
            dataGridView.ColumnCount = _cantidadNodos;
            for (int i = 0; i < _cantidadNodos; i++)
            {
                dataGridView.Columns[i].Width = 25; //Se le coloca un ancho de 35 a cada columna que se va creando
            }

            for (int i = 0; i < _cantidadNodos; i++)
            {
                for (int j = 0; j < _cantidadNodos; j++)
                {
                    dataGridView[i, j].Value = "";
                }
            }
        }


        private void DibujarArco(float origenX, float origenY, float finX, float finY)
        {
            var pen = new Pen(Color.FromArgb(255, 0, 0, 255), 2)
                          {StartCap = LineCap.ArrowAnchor, EndCap = LineCap.RoundAnchor};
            Graphics g = panel1.CreateGraphics();
            g.DrawLine(pen, origenX, origenY, finX, finY);
        }

        public void DibujarArcos(float X, float Y, float width,float height,float inicio,float curvatura)
        {
            var pen = new Pen(Color.FromArgb(255, 0, 0, 255), 2)
                          {StartCap = LineCap.ArrowAnchor, EndCap = LineCap.RoundAnchor};
            Graphics g = panel1.CreateGraphics();
           g.DrawArc(pen,  X, Y, width,height,inicio,curvatura);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form grafo = new Form2();
            //grafo.Show();
            for (int i = 0; i <= _cantidadNodos; i++)
            {
                panel1.Controls.Add(Graficar(i));
            }
        }


        public Control Graficar(int cantidadNodos)
        {
            Button nodo = null;

            switch (cantidadNodos)
            {
                case 1:
                    nodo = new Button
                               {
                                   Name = "nodo1",
                                   Size = new Size(45, 45),
                                   Location = new Point(ejeX[0], ejeY[0]),
                                   Text = "A",
                                   BackColor = Color.DeepPink,
                                   ForeColor = Color.White,
                               };
                    nodo.SendToBack();nodo.SendToBack();
                    break;


                case 2:
                    nodo = new Button
                               {
                                   Name = "nodo2",
                                   Size = new Size(45, 45),
                                   Location = new Point(ejeX[1], ejeY[1]),
                                   Text = "B",
                                   BackColor = Color.DeepPink,
                                   ForeColor = Color.White,
                               };
                    nodo.SendToBack();
                    break;

                case 3:
                    nodo = new Button
                               {
                                   Name = "nodo3",
                                   Size = new Size(45, 45),
                                   Location = new Point(ejeX[2], ejeY[2]),
                                   Text = "C",
                                   BackColor = Color.DeepPink,
                                   ForeColor = Color.White,
                               };
                    nodo.SendToBack();nodo.SendToBack();
                    break;

                case 4:
                    nodo = new Button
                               {
                                   Name = "nodo4",
                                   Size = new Size(45, 45),
                                   Location = new Point(ejeX[3], ejeY[3]),
                                   Text = "D",
                                   BackColor = Color.DeepPink,
                                   ForeColor = Color.White,
                               };
                    nodo.SendToBack();nodo.SendToBack();
                    break;

                case 5:
                    nodo = new Button
                               {
                                   Name = "nodo5",
                                   Size = new Size(45, 45),
                                   Location = new Point(ejeX[4], ejeY[4]),
                                   Text = "E",
                                   BackColor = Color.DeepPink,
                                   ForeColor = Color.White,
                               };
                    nodo.SendToBack();nodo.SendToBack();
                    break;

                case 6:
                    nodo = new Button
                               {
                                   Name = "nodo6",
                                   Size = new Size(45, 45),
                                   Location = new Point(ejeX[5], ejeY[5]),
                                   Text = "F",
                                   BackColor = Color.DeepPink,
                                   ForeColor = Color.White,
                               };
                    nodo.SendToBack();nodo.SendToBack();
                    break;

                case 7:
                    nodo = new Button
                               {
                                   Name = "nodo7",
                                   Size = new Size(45, 45),
                                   Location = new Point(ejeX[6], ejeY[6]),
                                   Text = "G",
                                   BackColor = Color.DeepPink,
                                   ForeColor = Color.White,
                               };
                    nodo.SendToBack();nodo.SendToBack();
                    break;

                case 8:
                    nodo = new Button
                               {
                                   Name = "nodo8",
                                   Size = new Size(45, 45),
                                   Location = new Point(ejeX[7], ejeY[7]),
                                   Text = "H",
                                   BackColor = Color.DeepPink,
                                   ForeColor = Color.White,
                               };
                    nodo.SendToBack();nodo.SendToBack();
                    break;
            }
            return nodo;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form grafo = new Form2();
            grafo.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= 7;i++ )
                DibujarArcos(ejeX[i] - 30, ejeY[i], 100, 40, ejeX[i], ejeY[i]);
        }

        private void DrawArcRectangle(/*PaintEventArgs e*/)
        {
            // Create pen.
            Pen blackPen = new Pen(Color.Red, 3);

            // Create rectangle to bound ellipse.
            Rectangle rect = new Rectangle(ejeX[0]-20, ejeY[0], 50, 30);

            // Create start and sweep angles on ellipse.
            float startAngle = 45.0F;
            float sweepAngle = 270.0F;
            Graphics g = panel1.CreateGraphics();
            // Draw arc to screen.
            g.DrawArc(blackPen, rect, ejeX[0], ejeX[0]+30);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DrawArcRectangle();
        }
    }
}