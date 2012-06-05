using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace ProyectoGrafos
{
    public partial class Form1 : Form

    {
        #region Declaracion de Variables

        //private readonly int[] _ejeX = {99, 452, 99, 452, 208, 328, 208, 328};
        //private readonly int[] _ejeY = {113, 96, 195, 195, 266, 26, 52, 303};
        private readonly int[] _ejeX = {31, 170, 357, 507, 468, 335, 195, 75};
        private readonly int[] _ejeY = {132, 55, 34, 102, 256, 320, 320, 267};

        private readonly char[] _idNodo = {
                                              'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i',
                                              'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q',
                                              'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'
                                          };

        public int FinX;
        public int FinY;
        public int[,] Matriz;
        public int OrigenX;
        public int OrigenY;
        private int _cantidadNodos = 1;

        #endregion

        public Form1()
        {
            Matriz = new int[8,8];
            InitializeComponent();
            MouseMove += Form1MouseMove;
        }

        #region Funciones necesarias para mover la ventana sin barra de titulo [Jose Sosa]

        //
        // Declaraciones del API de Windows (y constantes usadas para mover el form)
        //
        private const int WM_SYSCOMMAND = 0x112;
        private const int MOUSE_MOVE = 0xF012;
        //
        // Declaraciones del API
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private static extern void ReleaseCapture();


        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private static extern void SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        // función privada usada para mover el formulario actual
        private void MoverForm()
        {
            ReleaseCapture();
            SendMessage(Handle, WM_SYSCOMMAND, MOUSE_MOVE, 0);
        }


        private void Form1MouseMove(object sender, MouseEventArgs e)
        {
            MoverForm();
        }

        private void BtnCerrarClick1(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        private void LimpiarPanel()
        {
            for (int i = 0; i < 6; i++)
                foreach (object control in panel1.Controls)
                {
                    var botones = control as Button;
                    if (botones != null)
                        if (botones.Name.Contains("nodo"))
                        {
                            panel1.Controls.Remove(botones); //Elimina todos los contrles tipo boton en el Panel
                        }
                }
            panel1.Refresh();
        }

        private void DibujarLinea(float origenX, float origenY, float finX, float finY)
        {
            var pen = new Pen(Color.FromArgb(255, 158, 30, 57), 4)
                          {/*StartCap = LineCap.ArrowAnchor,*/EndCap = LineCap.DiamondAnchor};
            Graphics g = panel1.CreateGraphics();
            g.DrawLine(pen, origenX, origenY, finX, finY);
        }

        public void DibujarArco(float x, float y, float width, float height, float inicio, float curvatura)
        {
            var pen = new Pen(Color.FromArgb(255, 30, 129, 158), 5)
                          {StartCap = LineCap.DiamondAnchor, /* EndCap = LineCap.Triangle/*ArrowAnchor*/};
            Graphics g = panel1.CreateGraphics();
            g.DrawArc(pen, x, y, width, height, inicio, curvatura);
        }

        private void GuardarGrafo()
        {
            SendKeys.SendWait("%{PRTSC}");
            IDataObject d = Clipboard.GetDataObject();
            var guardar = new SaveFileDialog
                              {
                                  Filter = @"Imagenes JPG|*.jpg|Imagen de Mapa de Bits|*.bmp|Imagenes Gif|*.gif",
                                  Title = @"Guardar la captura del Grafo",
                                  RestoreDirectory = true
                              };
            switch (guardar.ShowDialog())
            {
                case DialogResult.OK:
                    {
                        string ruta = guardar.FileName;

                        try
                        {
                            if (d == null || !d.GetDataPresent(DataFormats.Bitmap)) return;
                            var b = (Bitmap) d.GetData(DataFormats.Bitmap);
                            b.Save(ruta);
                            MessageBox.Show(
                                @"Archivo Guardado Exitosamente\r\n" + @"En: "+ruta+@" " + @"Se limpiara el panel para crear un nuevo grafo",
                                @"Grafonator - Guardar Grafo",
                                MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            LimpiarPanel();
                        }
                        catch (Exception mensaje)
                        {
                            LimpiarPanel();
                            MessageBox.Show(@"Error" + mensaje, @"Grafonator - Guardar Grafo",
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    break;
            }
        }

        private void CrearGrafo(int columna)
        {
            for (int i = 0; i < _cantidadNodos; i++)
            {
                for (int j = 0; j < _cantidadNodos; j++)
                {
                    if ((string) dataGridView[i, j].Value!="0")
                    {
                        dataGridView[i, j].Value = "1"; 
                    }
                    
                }
            }

            for (int i = 0; i < _cantidadNodos; i++)
            {
                for (int j = 0; j < _cantidadNodos; j++)
                {
                    int asignarValoresMatriz = Convert.ToInt16(dataGridView[i, j].Value);
                    Matriz[i, j] = asignarValoresMatriz;
                }
            }
            /*Lineas que parten desde el Nodo 
             * a|b|c|d|e|f|g|h Se tomara una columna a columna 
             *a0|0|0|0|0|0|0|0 Y de cada una de ellas todas sus filas
             *b0|0|0|0|0|0|0|0 De esta manera se podra determinar
             *c0|0|0|0|0|0|0|0 cual sera el nodo de destino, comparando
             *d0|0|0|0|0|0|0|0 aquellas posiciones de la matriz que
             *e0|0|0|0|0|0|0|0 tengan como valor 1.
             *f0|0|0|0|0|0|0|0
             *g0|0|0|0|0|0|0|0
             *h0|0|0|0|0|0|0|0                      
             */
           
            OrigenX = _ejeX[columna];
            OrigenY = _ejeY[columna];

            for (int i = 0; i < _cantidadNodos; i++)
            {
                var colocarNodos = new Nodos();
                panel1.Controls.Add(colocarNodos.CrearNodoN(i, _ejeX[i], _ejeY[i]));
                //Crear las lineas curvas que apuntan al mismo nodo de origen
                if (columna == i && Matriz[columna,i]==1)
                {
                    DibujarArco(OrigenX - 50, OrigenY, 100, 40, 120, 180);
                }
                else
                //Crea las lineas desde el nodo origen hasta el nodo destino
                if (Matriz[columna,i]==1)
                {
                    DibujarLinea(OrigenX+30, OrigenY+30, _ejeX[i], _ejeY[i]-5);
                }
            }
        }
        
        private void NumericUpDown1ValueChanged(object sender, EventArgs e)
        {
            _cantidadNodos = (int) numericUpDown1.Value;
            dataGridView.RowCount = _cantidadNodos;
            dataGridView.ColumnCount = _cantidadNodos;
            for (int i = 0; i < _cantidadNodos; i++)
            {
                dataGridView.Columns[i].Width = 25; //Se le coloca un ancho de 25 a cada columna que se va creando
            }

            for (int i = 0; i < _cantidadNodos; i++)
            {
                for (int j = 0; j < _cantidadNodos; j++)
                {
                    dataGridView[i, j].Value = "0";
                }
            }
           
        }

        private void Button2Click(object sender, EventArgs e)
        {
            var graficarNodo = new Nodos();
            for (int i = 0; i < _cantidadNodos; i++)
            {
                panel1.Controls.Add(graficarNodo.CrearNodoN(i, _ejeX[i], _ejeY[i]));
                for (int j = 0; j < _cantidadNodos; j++)
                {
                    DibujarArco(_ejeX[i] - 50, _ejeY[i], 100, 40, 120, 180);

                    for (int k = 0; k <= 7; k++)
                    {
                        DibujarLinea(_ejeX[i], _ejeY[i] + 10, _ejeX[i + 1], _ejeY[i + 1]);
                    }
                }
            }
        }

        private void BtnNuevoGrafoClick(object sender, EventArgs e)
        {
            LimpiarPanel();
        }

        private void BtnMinimizarClick(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void BtnCerrarClick(object sender, EventArgs e)
        {
            Close();
        }

        private void Button1Click(object sender, EventArgs e)
        {
           GuardarGrafo();
        }

        private void LinkLabel1LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           
            Process.Start(e.Link.LinkData.ToString());
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            linkLabel1.Links.Add(0, 23, "https://github.com/Servtes/ProyectoGrafos-1190-10-13979");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < _cantidadNodos; i++)
            {
                CrearGrafo(i);
            }
            
        }
    }
}