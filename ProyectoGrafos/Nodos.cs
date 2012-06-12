using System.Drawing;
using System.Threading;
using System.Windows.Forms;

/* Clase que contiene los metodos para la creacion de controles tipo Button
 * Los cuales son usados para representar los nodos del grafo deseado.
 * 
 * Autor: Jose Carlos Sosa Mejia 1190-10-13979
 * Fecha de Creacion: 04/06/2012
 * ********Bitacora de Cambios********
 * 04/06/12: Creacion.
 * 10/06/12: Agregados Comentarios al Codigo.
 */

namespace ProyectoGrafos
{
    ///<summary>
    ///Crea controles del tipo Button para la representacion de los Nodos.
    ///</summary>
    ///<remarks>
    ///.
    ///</remarks>
    public class Nodos
    {
        /// <summary>
        /// Crear los nodos.
        /// </summary>
        /// <param name="numeroDeNodo">El numero de nodo a Crear.</param>
        /// <param name="ejeX">Posicion en el eje X.</param>
        /// <param name="ejeY">Posicion en el eje Y.</param>
        /// <returns>El nodo en las posiciones x,y indicadas</returns>
        public Control CrearNodoN(int numeroDeNodo, int ejeX, int ejeY)
        {
            Button nodo = null;

            switch (numeroDeNodo)
            {
                case 0:
                    nodo = new Button
                               {
                                   //Descripcion de las propiedades
                                   Name = "nodo1",
                                   //Nombre que tendra el Control
                                   Size = new Size(45, 45),
                                   //Tamaño en pixeles
                                   Location = new Point(ejeX, ejeY),
                                   //Posicion en el Panel
                                   Text = "A",
                                   //Texto que contendra
                                   BackColor = Color.Crimson,
                                   //Color del control
                                   ForeColor = Color.White,
                                   //Color del Texto
                               };
                    nodo.SendToBack(); //Pasa al control al fondo de todos los elementos
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
            //Tiempo de espera entre cada llamada.
            Thread.Sleep(100);
            return nodo;
        }
    }
}