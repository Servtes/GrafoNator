using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace ProyectoGrafos
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            MouseMove += panel1MouseMove;
        }

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


        private void panel1MouseMove(object sender, MouseEventArgs e)
        {
            MoverForm();
        }
        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
