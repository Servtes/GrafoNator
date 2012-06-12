using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

/* Autor: Jose Carlos Sosa Mejia 1190-10-13979
 * Fecha de Creacion: 04/06/2012
 * ********Bitacora de Cambios********
 * 01/06/12: Creacion.
 * 10/06/12: Comentarios añadidos a las funciones.
 *           Agregada funcion para la creacion de grafos
 *           Aleatorios.
 * 11/06/12: Agregada funcion para limpiar la matriz de representacion
 *           reestablciendo los valores de la variablea _cantidadNodos
 *           y eliminando todas las celdas del dataGridWiev hasta quedar en 1.
 *           Agregada barra de progreso y su respectiva funcion.
 */

namespace ProyectoGrafos
{
    /// <summary>
    /// Clase principal del programa, contiene las funciones de operacion y dibujo
    /// Y la implementacion de los controles de formulario.
    /// </summary>
    public partial class GrafoNator : Form

    {
        #region Declaracion de Variables

        private readonly Random _cantidadAleatoriaNodos = new Random((int) (DateTime.Now.Ticks));

        /*Vectores con la lista de coordenadas (x,y) para cada Nodo*/
        //private readonly int[] _ejeX = {99, 452, 99, 452, 208, 328, 208, 328};
        //private readonly int[] _ejeY = {113, 96, 195, 195, 266, 26, 52, 303};
        private readonly int[] _ejeX = {31, 170, 357, 507, 468, 335, 195, 75};
        private readonly int[] _ejeY = {132, 55, 34, 102, 256, 320, 320, 267};

        private readonly int[,] _matriz;
        private readonly Random _valorAleatorioCeldas = new Random(((int) DateTime.Now.Ticks));
        private int _cantidadNodos = 1;
        private int _origenX;
        private int _origenY;

        #endregion

        /// <summary>
        /// Formulario Principal de la Aplicacion
        /// </summary>
        public GrafoNator()
        {
            _matriz = new int[8,8];
            InitializeComponent();
            MouseMove += Form1MouseMove;
        }

        #region Funciones necesarias para mover la ventana sin barra de titulo [Jose Sosa]

        //Declaraciones del API de Windows (y constantes usadas para mover el form)
        private const int WM_SYSCOMMAND = 0x112;
        private const int MOUSE_MOVE = 0xF012;
        //
        // Declaraciones del API
        //
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private static extern void ReleaseCapture();


        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private static extern void SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        //
        // función privada usada para mover el formulario actual
        //
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

        #region Funciones del Programa

        /// <summary>
        /// Vaciar el panel de elementos
        /// </summary>
        private void LimpiarPanel()
        {
            for (int i = 0; i < 8; i++)
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

        /// <summary>
        /// Funcion utilizada para dibjar las lineas de conexion entre nodos.
        /// </summary>
        private void LimpiarMatriz()
        {
            _cantidadNodos = 1;
            numericUpDown1.Value = 1;
            OperacionesConDataGridView();
        }

        /// <summary>
        /// Funcion utilizada para dibjar las lineas de conexion entre nodos.
        /// </summary>
        private void DibujarLinea(float origenX, float origenY, float finX, float finY)
        {
            var pen = new Pen(Color.FromArgb(255, 158, 30, 57), 4)
                          {/*StartCap = LineCap.ArrowAnchor,*/EndCap = LineCap.DiamondAnchor};
            Graphics g = panel1.CreateGraphics();
            g.DrawLine(pen, origenX, origenY, finX, finY);
        }

        /*Funcion utilizada para dibujar los bucles*/

        private void DibujarArco(float x, float y, float width, float height, float inicio, float curvatura)
        {
            var pen = new Pen(Color.FromArgb(255, 30, 129, 158), 5)
                          {StartCap = LineCap.DiamondAnchor, /* EndCap = LineCap.Triangle/*ArrowAnchor*/};
            Graphics g = panel1.CreateGraphics();
            g.DrawArc(pen, x, y, width, height, inicio, curvatura);
        }

        /// <summary>
        /// Toma un screenshot de la ventana del programa, la guarda en el portapapeles
        /// con la imagen el el portapapeles se puede decidir si guardarla en cualquier
        /// posicion mediante un dialogo de guardado.
        /// </summary>
        private void GuardarGrafo()
        {
            /*Manda al sistema operativo la misma señal enviada al presionar las teclas ATL + PRINT SCR*/
            SendKeys.SendWait("%{PRTSC}");

            /* Se crea un objeto que puede contener cualquier tipo de datos y se le asigna como valor
             * el dato que se tenga en el portapapeles.*/
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
                            /*Si el objeto d es nulo o no contiene una imagen, se interrumpe el proceso*/
                            if (d == null || !d.GetDataPresent(DataFormats.Bitmap)) return;
                            var b = (Bitmap) d.GetData(DataFormats.Bitmap);
                            b.Save(ruta);
                            MessageBox.Show(
                                @"Archivo Guardado Exitosamente\r\n" + @"En: " + ruta + @" " +
                                @"Se limpiara el panel para crear un nuevo grafo",
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
            progressBar1.Visible = false;
            AnimarProgressBar();
            for (int i = 0; i < _cantidadNodos; i++)
            {
                for (int j = 0; j < _cantidadNodos; j++)
                {
                    if ((string) dataGridView[i, j].Value != "0")
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
                    _matriz[i, j] = asignarValoresMatriz;
                }
            }
            /*Lineas que parten desde el Nodo 
             *  a|b|c|d|e|f|g|h Se tomara una columna a columna 
             *a|0|0|0|0|0|0|0|0 Y de cada una de ellas todas sus filas
             *b|0|0|0|0|0|0|0|0 De esta manera se podra determinar
             *c|0|0|0|0|0|0|0|0 cual sera el nodo de destino, comparando
             *d|0|0|0|0|0|0|0|0 aquellas posiciones de la matriz que
             *e|0|0|0|0|0|0|0|0 tengan como valor 1.
             *f|0|0|0|0|0|0|0|0
             *g|0|0|0|0|0|0|0|0
             *h|0|0|0|0|0|0|0|0                      
             */

            _origenX = _ejeX[columna];
            _origenY = _ejeY[columna];

            for (int i = 0; i < _cantidadNodos; i++)
            {
                var colocarNodos = new Nodos();
                panel1.Controls.Add(colocarNodos.CrearNodoN(i, _ejeX[i], _ejeY[i]));
                //Crear las lineas curvas que apuntan al mismo nodo de origen
                if (columna == i && _matriz[columna, i] == 1)
                {
                    DibujarArco(_origenX - 50, _origenY, 100, 40, 120, 180);
                }
                else
                {
                    //Crea las lineas desde el nodo origen hasta el nodo destino
                    if (_matriz[columna, i] == 1)
                    {
                        DibujarLinea(_origenX + 30, _origenY + 30, _ejeX[i], _ejeY[i] - 5);
                    }
                }
            }
        }

        private void CrearGrafoAleatorio()
        {
            progressBar1.Visible = false;
            AnimarProgressBar();
            LimpiarPanel();
            /*Da un valor aleatorio a la variable entre 1 y 8*/
            _cantidadNodos = _cantidadAleatoriaNodos.Next(1, 9);
            numericUpDown1.Value = _cantidadNodos;
            OperacionesConDataGridView();
            /*Asigna un valor entre "0" y "1" aleatoriamente a cada celda en el datagrid*/
            for (int i = 0; i < _cantidadNodos; i++)
            {
                for (int j = 0; j < _cantidadNodos; j++)
                {
                    dataGridView[i, j].Value = Convert.ToString(_valorAleatorioCeldas.Next(0, 2));
                }
            }
            for (int i = 0; i < _cantidadNodos; i++)
            {
                CrearGrafo(i);
            }
        }

        /// <summary>
        /// Establece la cantidad de filas y columnas en el datagrid igual al valor en la
        /// variable _cantidadNodos.
        /// Asigna un valor "0" a cada celda del datagrid
        /// </summary>
        private void OperacionesConDataGridView()
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

        /// <summary>
        /// Funcion para la animacion de la barra de progreso,  de acuerdo a cada accion
        /// realizada durante la creacion de grafos
        /// </summary>
        private void AnimarProgressBar()
        {
            progressBar1.Visible = true;
            progressBar1.Maximum = 100000;
            progressBar1.Minimum = 0;
            progressBar1.Value = 0;
            progressBar1.Step = 1;
            for (int i = progressBar1.Minimum; i < progressBar1.Maximum; i = i + progressBar1.Step)
            {
                progressBar1.PerformStep();
            }
        }

        #endregion

        #region Controles del Formulario

        private void NumericUpDown1ValueChanged(object sender, EventArgs e)
        {
            OperacionesConDataGridView();
        }

        private void Button2Click(object sender, EventArgs e)
        {
            for (int i = 0; i < _cantidadNodos; i++)
            {
                CrearGrafo(i);
            }
        }

        private void BtnNuevoGrafoClick(object sender, EventArgs e)
        {
            LimpiarMatriz();
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
            dataGridView.RowCount = _cantidadNodos;
            dataGridView.ColumnCount = _cantidadNodos;
            dataGridView.Columns[0].Width = 25;
            dataGridView[0, 0].Value = "0";
        }

        private void BtnGrafoAleatorioClick(object sender, EventArgs e)
        {
            CrearGrafoAleatorio();
        }

        private void AcercaDe_Click(object sender, EventArgs e)
        {
            var acercaDe = new AcercaDe {StartPosition = FormStartPosition.Manual};
            acercaDe.Show();
        }

        #endregion
    }
}