using System.Drawing;
using System.Threading;
using System.Windows.Forms;

/* Clase que contiene los metodos para la creacion de controles tipo Button
 * Los cuales son usados para representar los nodos del grafo deseado.
 * 
 * Autor: Jose Carlos Sosa Mejia 1190-10-13979
 * Fecha de Creacion: 04/06/2012
 * ********Bitacora de Cambios********
 * 04/06/12: Creacion
 */

namespace ProyectoGrafos
{
    public class Nodos
    {
        public Control CrearNodoN(int cantidadNodos, int ejeX, int ejeY)
        {
            Button nodo = null;

            switch (cantidadNodos)
            {
                case 0:
                    nodo = new Button
                               {                                    //Descripcion de las propiedades
                                   Name = "nodo1",                   //Nombre que tendra el Control
                                   Size = new Size(45, 45),           //Tamaño en pixeles
                                   Location = new Point(ejeX, ejeY),    //Posicion en el Panel
                                   Text = "A",                        //Texto que contendra
                                   BackColor = Color.Crimson,           //Color del control
                                   ForeColor = Color.White,             //Color del Texto
                               };
                    nodo.SendToBack();//Pasa al control al fondo de todos los elementos
                    nodo.SendToBack();
                    break;


                case 1:
                    nodo = new Button
                               {
                                   Name = "nodo2",
                                   Size = new Size(45, 45),
                                   Location = new Point(ejeX, ejeY),
                                   Text = "B",
                                   BackColor = Color.Crimson,
                                   ForeColor = Color.White,
                               };
                    nodo.SendToBack();
                    break;

                case 2:
                    nodo = new Button
                               {
                                   Name = "nodo3",
                                   Size = new Size(45, 45),
                                   Location = new Point(ejeX, ejeY),
                                   Text = "C",
                                   BackColor = Color.Crimson,
                                   ForeColor = Color.White,
                               };
                    nodo.SendToBack();
                    nodo.SendToBack();
                    break;

                case 3:
                    nodo = new Button
                               {
                                   Name = "nodo4",
                                   Size = new Size(45, 45),
                                   Location = new Point(ejeX, ejeY),
                                   Text = "D",
                                   BackColor = Color.Crimson,
                                   ForeColor = Color.White,
                               };
                    nodo.SendToBack();
                    nodo.SendToBack();
                    break;

                case 4:
                    nodo = new Button
                               {
                                   Name = "nodo5",
                                   Size = new Size(45, 45),
                                   Location = new Point(ejeX, ejeY),
                                   Text = "E",
                                   BackColor = Color.Crimson,
                                   ForeColor = Color.White,
                               };
                    nodo.SendToBack();
                    nodo.SendToBack();
                    break;

                case 5:
                    nodo = new Button
                               {
                                   Name = "nodo6",
                                   Size = new Size(45, 45),
                                   Location = new Point(ejeX, ejeY),
                                   Text = "F",
                                   BackColor = Color.Crimson,
                                   ForeColor = Color.White,
                               };
                    nodo.SendToBack();
                    nodo.SendToBack();
                    break;

                case 6:
                    nodo = new Button
                               {
                                   Name = "nodo7",
                                   Size = new Size(45, 45),
                                   Location = new Point(ejeX, ejeY),
                                   Text = "G",
                                   BackColor = Color.Crimson,
                                   ForeColor = Color.White,
                               };
                    nodo.SendToBack();
                    nodo.SendToBack();
                    break;

                case 7:
                    nodo = new Button
                               {
                                   Name = "nodo8",
                                   Size = new Size(45, 45),
                                   Location = new Point(ejeX, ejeY),
                                   Text = "H",
                                   BackColor = Color.Crimson,
                                   ForeColor = Color.White,
                               };
                    nodo.SendToBack();
                    nodo.SendToBack();
                    break;
            }
            Thread.Sleep(100);
            return nodo;
        }
    }
}