using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;
using ControlesAsp.EstructuraDeDatos;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Data;

namespace ControlesAsp.DataTableControl
{
    public static class DataTableASP
    {

        /// <summary>
        /// Permite construir un obejeto DataTable. 
        /// </summary>
        /// <param name="MyNodo">Nodo con key (Nombre columna) y item (Tipo de dato de columna)</param>
        /// <param name="nomTabla">Nombre de la tabla</param>
        /// <returns>Objeto datatable construido</returns>
        /// <remarks>
        /// <list>Creado: Noviembre 29 de 2013 - Ing. David Pineda</list>
        /// </remarks>
        public static DataTable crearTabla(Nodo<string, System.Type>[] MyNodo, string nomTabla = "")
        {

            DataTable myDatatable = new DataTable();
            DataColumn columna = default(DataColumn);


            foreach (Nodo<string, System.Type> nodo in MyNodo)
            {

                try
                {
                    columna = new DataColumn(nodo.key);
                    columna.DataType = nodo.item;
                    myDatatable.Columns.Add(columna);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            if (!string.IsNullOrEmpty(nomTabla))
            {
                myDatatable.TableName = nomTabla;
            }
            return myDatatable;
        }

        /// <summary>
        /// Permite adicionar un DataRow a un Datatable
        /// </summary>
        /// <param name="myDataTable">Datatable a la cual se adiciona el DataRow</param>
        /// <param name="myDataRow">DataRow a insertar en la DataTable</param>
        /// <param name="repetido">Opcional. Indica si permite valores repetidos asigne true si desea aceptar valores repetidos</param>
        /// <param name="norepeat">Opcional. Se puede enviar opcionalmente con los numeros de columnas en la datatable que no permite repetir</param>
        /// <returns>Valor Booleano que indica si pudo insertar (true) o si el elemento esta repetido (False)</returns>
        /// <remarks>
        /// <list>Creado: Noviembre 29 de 2013 - Ing. David Pineda</list>
        /// </remarks>
        public static bool addItemDataTable(ref DataTable myDataTable, DataRow myDataRow, bool repetido = false, int[] norepeat = null)
        {
            if (myDataTable == null)
            {
                throw new ArgumentNullException("El Argumento myDataTable se encuentra nulo");
            }
            if (myDataRow == null)
            {
                throw new ArgumentNullException("El Argumento myDataRow se encuentra nulo");
            }

            bool valorRetorno = false;
            try
            {
                if (repetido)
                {
                    myDataTable.Rows.Add(myDataRow);
                    valorRetorno = true;
                }
                else
                {
                    if (validarRepetido(myDataTable, myDataRow, norepeat))
                    {
                        myDataTable.Rows.Add(myDataRow);
                        valorRetorno = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return valorRetorno;

        }

        /// <summary>
        /// Valida si un objecto DataRow se encuentra repetido en un DataTable
        /// </summary>
        /// <param name="myDataTable">DataTable a validar</param>
        /// <param name="myDataRow">Datarow para validar si ya esta en al DataTable</param>
        /// <param name="norepeat">Arreglo con los indices de columnas de la tabla que no se pueden repetir</param>
        /// <returns>True si el valor no esta repetido, False de lo contrario</returns>
        /// <remarks>
        /// <list>Creado: Noviembre 29 de 2013 - Ing. David Pineda</list>
        /// </remarks>
        public static bool validarRepetido(DataTable myDataTable, DataRow myDataRow, int[] norepeat)
        {
            if (norepeat == null)
            {
                //Ningun campo de la DataTable se repite
                foreach (DataRow fila in myDataTable.Rows)
                {
                    if (comparar(fila, myDataRow))
                    {
                        return false;
                    }
                }
            }
            else
            {
                //Algnos campos de la tabla se repiten
                DataTable myDatatableCopia = new DataTable();
                foreach (DataRow fila in myDataTable.Rows)
                {
                    myDatatableCopia = myDataTable.Clone();
                    DataRow myDataRow_1 = quitarColumnas(myDatatableCopia, norepeat);
                    myDatatableCopia = myDataTable.Clone();
                    DataRow myDataRow_2 = quitarColumnas(myDatatableCopia, norepeat);
                    int index = 0;
                    foreach (int columna in norepeat)
                    {
                        myDataRow_1[index] = fila[columna];
                        myDataRow_2[index] = myDataRow[columna];
                        index += 1;
                    }
                    if (comparar(myDataRow_1, myDataRow_2))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Compara si dos DataRow son iguales
        /// </summary>
        /// <param name="myDataRow_1">DataRow 1 a comparar</param>
        /// <param name="myDataRow_2">DataRow 2 a comparar</param>
        /// <returns>True si los valores de columnas del DataRow son iguales</returns>
        /// <remarks>
        /// <list>Creado: Noviembre 29 de 2013 - Ing. David Pineda</list>
        /// </remarks>
        private static bool comparar(DataRow myDataRow_1, DataRow myDataRow_2)
        {

            object[] array_1 = myDataRow_1.ItemArray;
            object[] array_2 = myDataRow_2.ItemArray;

            if (array_1.Length != array_2.Length)
            {
                return true;
            }
            else
            {
                int cantIndex = array_1.Length;
                int indexIguales = 0;
                for (int index = 0; index <= array_1.Length - 1; index++)
                {
                    if (array_1[index].ToString().Equals(array_2[index].ToString()))
                    {
                        indexIguales += 1;
                    }
                }
                if (cantIndex == indexIguales)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Elimina columnas de un DataTable
        /// </summary>
        /// <param name="myDataTable">DataTable base para el DataRow</param>
        /// <param name="norepeat">Arreglo con los indices de columnas que no se eliminan</param>
        /// <returns>DataRow sin las columnas que se quitaron</returns>
        /// <remarks>
        /// <list>Creado: Noviembre 29 de 2013 - Ing. David Pineda</list>
        /// </remarks>
        private static DataRow quitarColumnas(DataTable myDataTable, int[] norepeat)
        {
            int indexColumna = 0;
            bool eliminarColumna = false;
            DataColumn[] columnasEliminar = {};
            int numColEli = 0;

            foreach (DataColumn columna in myDataTable.Columns)
            {
                eliminarColumna = true;
                for (int i = 0; i <= norepeat.Length - 1; i++)
                {
                    if (norepeat[i] == indexColumna)
                    {
                        eliminarColumna = false;
                        break;
                    }
                }
                if (eliminarColumna)
                {
                    Array.Resize<DataColumn>(ref columnasEliminar, numColEli + 1);
                    columnasEliminar[numColEli] = columna;
                    numColEli += 1;
                    
                }
                indexColumna += 1;
            }

            foreach (DataColumn columna in columnasEliminar)
            {
                myDataTable.Columns.Remove(columna);
            }

            return myDataTable.NewRow();
        }

        /// <summary>
        /// Remueve un DataRow de un DataTable
        /// </summary>
        /// <param name="MyDataTable">Objeto DataTable del cual desea remover los datos</param>
        /// <param name="myNodos">Arreglo con los nodes para remover (Key = Nombre columna / Item = Valor de busqueda para eliminar)</param>
        /// <returns>True si se modifico el objeto DataTable</returns>
        /// <remarks>
        /// <list>Creado: 29 de noviembre de 2013 - Ing. David Pineda</list>
        /// </remarks>
        public static bool removeItemDataTable(ref DataTable MyDataTable, Nodo<string, string>[] myNodos)
        {
            if ((MyDataTable != null) & (myNodos != null))
            {
                foreach (DataRow fila in MyDataTable.Rows)
                {
                    int iguales = myNodos.Length;
                    int coincidencia = 0;
                    foreach (Nodo<string, string> nodo in myNodos)
                    {
                        if (fila[nodo.key].ToString() == nodo.item)
                        {
                            coincidencia += 1;
                        }
                    }
                    if (coincidencia == iguales)
                    {
                        MyDataTable.Rows.Remove(fila);
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Permite retornar en un array los datos de una columna de un datatable
        /// </summary>
        /// <param name="objDtDatos">Objeto Datatable</param>
        /// <param name="nombre_columna">Nombre del campo de la tabla que se requiere pasar al array</param>
        /// <returns>Arreglo con los datos de la columna</returns>
        /// <remarks>
        /// <list>Creado: 20 de enero de 2014 - Ing. David Pineda</list>
        /// </remarks>
        /// <exception cref="ArgumentNullException">Si alguno de los parametros es null</exception>
        public static object[] inputTableToArray(DataTable objDtDatos, string nombre_columna)
        {

	        if ((objDtDatos == null)) {
		        throw new ArgumentNullException("El argumento objDtDatos es nulo");
	        }

	        if (nombre_columna == string.Empty) {
		        throw new ArgumentNullException("El argumento nom_input esta vacio");
	        }

	        int cantItems = objDtDatos.Rows.Count;
	        object[] datos = new object[cantItems - 1];
	        int cont = 0;

	        try 
            {
		        foreach (DataRow objDrDatos in objDtDatos.Rows) 
                {
			        datos[cont] = Convert.ToString(objDrDatos[nombre_columna]);
			        cont += 1;
		        }
	        }
            catch (Exception ex) {
                throw ex;
	        }
	        return datos;
        }

        /// <summary>
        /// Permite verificar si un datatable contiene filas
        /// </summary>
        /// <param name="myDatatable">Objeto Datatable a validar</param>
        /// <returns>Valor booleano que indica si tiene rows (True) o no (false)</returns>
        /// <remarks>
        /// <list>Creado Enero 24 de 2014 - David Pineda</list>
        /// </remarks>
        public static bool validarDatatableDatos(DataTable myDatatable)
        {
            if ((myDatatable == null))
            {
                return false;
            }
            else
            {
                if (!(myDatatable.Rows.Count > 0))
                {
                    return false;
                }
            }
            return true;
        }

        public static string convertDatatableToString(DataTable myDataTable)
        {
            if (myDataTable == null)
            {
                throw new ArgumentNullException("El argumento myDataTable no puede ser null");
            }
            return JsonConvert.SerializeObject(myDataTable, Formatting.None);
        }
    }
}
