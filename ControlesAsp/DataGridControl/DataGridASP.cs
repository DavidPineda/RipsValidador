using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;

namespace ControlesAsp.DataGridControl
{
    public class DataGridASP
    {
        public DataGridASP()
        { 
        }

        /// <summary>
        /// Permite actualizar un objecto datagrid despues de haber realizado modificaciones sobre el mismo
        /// </summary>
        /// <param name="MyDataGrid">Objeto DataGrid a actualizar</param>
        /// <param name="myDataTable">Objeto DataTable con los datos</param>
        /// <remarks>
        /// <list> Creado: Diciembre 10 de 2013 - Ing. David Alejandro Pineda Diaz </list>
        /// </remarks>
        public static void afterUpdateDataTable(ref DataGrid MyDataGrid, DataTable myDataTable)
        {
            try
            {
                try
                {
                    MyDataGrid.DataSource = myDataTable;
                    MyDataGrid.DataBind();
                }
                catch (HttpException ex)
                {
                    if (MyDataGrid.PageCount == MyDataGrid.CurrentPageIndex)
                    {
                        MyDataGrid.CurrentPageIndex -= 1;
                        MyDataGrid.DataBind();
                    }
                }
                MyDataGrid.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Permite actualizar un objeto DataGrid
        /// </summary>
        /// <param name="myDataGrid">Objeto DataGrid a actualizar</param>
        /// <param name="myDataTable">Objeto DataTable con los datos</param>
        /// <param name="pagindex">Opcional: si desea aumentar la páginación</param>
        /// <remarks>
        /// <list> Creado: Diciembre 10 de 2013 - Ing. David Alejandro Pineda Diaz </list>
        /// </remarks>
        /// <exception cref="ArgumentNullException">Si alguno de los parametros myDataGrid o myDataTable es null</exception>
        /// <exception cref="Exception">Si se presenta algun error en el llenado del datagtrid</exception>
        public static void updateGrid(ref DataGrid myDataGrid, DataTable myDataTable, int pagindex = -1)
        {
            if ((myDataGrid == null))
            {
                throw new ArgumentNullException("El argumento myDataGrid es nulo");
            }

            if ((myDataTable == null))
            {
                throw new ArgumentNullException("El argumento myDataTable es nulo");
            }

            try
            {
                if (pagindex != -1)
                {
                    myDataGrid.CurrentPageIndex = pagindex;
                }
                myDataGrid.DataSource = myDataTable;
                myDataGrid.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Agrega una nueva columna de plantilla al objeto DataGrid
        /// </summary>
        /// <param name="MyDataGrid">Objeto DataGrid a modificar</param>
        /// <param name="myDatatable">Objeto DataTable con los datos</param>
        /// <param name="nombreColumna">Nombre de la columna a incluir</param>
        /// <param name="control">Control a insertar en la nueva columna</param>
        /// <param name="tipo_item">Tipo de control a insertar en la columna</param>
        /// <remarks>
        /// <list> Creado: Diciembre 10 de 2013 - Ing. David Alejandro Pineda Diaz </list>
        /// </remarks>
        public static void addItemColumnToGrid(ref DataGrid MyDataGrid, DataTable myDatatable, string nombreColumna, System.Web.UI.Control control, ListItemType tipo_item)
        {

            try
            {
                TemplateColumn columna = new TemplateColumn();
                columna.HeaderTemplate = new DataGridTemplate(ListItemType.Header, nombreColumna);
                columna.ItemTemplate = new DataGridTemplate(tipo_item, control);
                MyDataGrid.Columns.Add(columna);
                updateGrid(ref MyDataGrid, myDatatable);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Agrega una nueva columna al DataGrid
        /// </summary>
        /// <param name="MyDataGrid">Objeto DataGrid a actualizar</param>
        /// <param name="myDatatable">Objeto Datatable con los datos</param>
        /// <param name="objBound_column">Objeto BoundColumn a agregar</param>
        /// <remarks>
        /// <list> Creado: Diciembre 10 de 2013 - Ing. David Alejandro Pineda Diaz </list>
        /// </remarks>
        public static void addBoundColumnToGrid(ref DataGrid MyDataGrid, DataTable myDatatable, BoundColumn objBound_column)
        {
            try
            {
                MyDataGrid.Columns.Add(objBound_column);
                updateGrid(ref MyDataGrid, myDatatable);
            }
            catch (Exception ex)
            {
                throw ex;
           }
        }
    }
}
