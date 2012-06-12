using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ProyectoGrafos
{
    /// <summary>
    /// 
    /// </summary>
    public partial class AcercaDe : Form
    {
        /// <summary>
        /// Informacion general sobre la aplicacion
        /// </summary>
        public AcercaDe()
        {
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

        #endregion

        private void AcercaDe_Load(object sender, EventArgs e)
        {
            linkLabel1.Links.Add(0, 23, "https://github.com/Servtes/ProyectoGrafos-1190-10-13979");
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData.ToString());
        }
    }
}