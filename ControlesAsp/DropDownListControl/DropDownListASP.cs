using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;

#if Telerik
using Telerik.Web.UI;
#endif

namespace ControlesAsp.DropDownListControl
{
    public class DropDownListASP
    {
        public DropDownListASP()
        { 
        }

        #region ".NetControls"
        /// <summary>
        /// Permite llenar un DropDownList a partir de una Datatable con los datos
        /// </summary>
        /// <param name="dtDatos">Objecto DataTable con los datos a cargar en el DropDownList</param>
        /// <param name="dataValueFiled">Nombre del campo en la DataTable que sera el value del DropDownList</param>
        /// <param name="dataTextField">Nombre del campo en la DataTable que sera el texto del DropDownList</param>
        /// <param name="myDropDownList">Objeto DropDownList que se llena</param>
        /// <remarks>
        /// <list>Creado: 25 de Noviembre de 2013 - Ing. David Pineda</list>
        /// </remarks>
        /// <exception cref="ArgumentNullException">Si alguno de los argumentos dtDatos o el myDropDownList es nulo</exception>
        /// <exception cref="ArgumentException">SI alguno de los argumentos dataValueFiled o dataTextField es vacio</exception>
        public static void llenarDropDownList(DataTable dtDatos, string dataValueFiled, string dataTextField, ref DropDownList myDropDownList)
        {
            if (dtDatos == null)
            {
                throw new ArgumentNullException("El argumento dtDatos es nulo");
            }

            if (myDropDownList == null)
            {
                throw new ArgumentNullException("El argumento myDropDownList es nulo");
            }

            if (dataValueFiled == string.Empty)
            {
                throw new ArgumentException("El argumento dataValueFiled esta vacio");
            }

            if (dataTextField == string.Empty)
            {
                throw new ArgumentException("El argumento dataTextField esta vacio");
            }

            try
            {
                myDropDownList.Items.Clear();
                myDropDownList.DataValueField = dataValueFiled;
                myDropDownList.DataTextField = dataTextField;
                myDropDownList.DataSource = dtDatos;
                myDropDownList.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Permite llenar un DropDownList a partir de una Datatable con los datos
        /// </summary>
        /// <param name="dtDatos">Objecto DataTable con los datos a cargar en el DropDownList</param>
        /// <param name="myDropDownList">Objeto DropDownList que se llena</param>
        /// <remarks>
        /// <list>Creado: 25 de Noviembre de 2013 - Ing. David Pineda</list>
        /// </remarks>
        public static void llenarDropDownList(DataTable dtDatos, ref DropDownList myDropDownList)
        {

            if (dtDatos == null)
            {
                throw new ArgumentNullException("El argumento dtDatos es nulo");
            }

            if (myDropDownList == null)
            {
                throw new ArgumentNullException("El argumento myDropDownList es nulo");
            }

            try
            {
                myDropDownList.Items.Clear();
                myDropDownList.DataValueField = myDropDownList.DataValueField;
                myDropDownList.DataTextField = myDropDownList.DataTextField;
                myDropDownList.DataSource = dtDatos;
                myDropDownList.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Metodo que carga un registro a un DropDownList
        /// </summary>
        /// <param name="cboDropDownList">Control DropDownList al que se le van a cargar los datos</param>
        /// <param name="strText">Texto del registro</param>
        /// <param name="strValue">Valor del registro</param>
        /// <param name="blnAddFirstItem">True si desea adicionarlo al inicio o False de lo contrario</param>
        /// <remarks>
        /// <list>Creaciòn: Jul 12/2013 - Ing. Paulo Cesar Pacheco Tovar</list>
        /// </remarks>
        public static void AddItemToDropDownList(ref DropDownList cboDropDownList, string strText, string strValue, bool blnAddFirstItem = false)
        {
            if (cboDropDownList == null)
            {
                throw new ArgumentNullException("El argumento cboDropDownList no puede ser nulo");
            }

            if (strValue == string.Empty)
            {
                throw new ArgumentException("El argumento strValue no puede estar vacio");
            }

            ListItem objListItem = new ListItem();
            objListItem.Text = strText;
            objListItem.Value = strValue;

            try
            {
                if (blnAddFirstItem)
                {
                    cboDropDownList.Items.Insert(0, objListItem);
                }
                else
                {
                    cboDropDownList.Items.Add(objListItem);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Permite cargar un DropDownList con los meses de un año desde enero hasta el mes actual
        /// </summary>
        /// <param name="myDropDownList">El objecto DropDownList a llenar</param>
        /// <param name="abreviado">Indica si el nombre del mes se va a abreviar o no, si se omite el valor por defecto es False, es decir que no se omite</param>
        /// <remarks>
        /// <list>Creado: Noviembre 28 de 2013 - Ing. David Pineda</list>
        /// </remarks>
        public static void cargarMesesToDropDownList(ref DropDownList myDropDownList, bool abreviado = false)
        {
            try
            {
                myDropDownList.Items.Clear();
                for (int i = 1; i <= DateTime.Now.Month; i++)
                {
                    AddItemToDropDownList(ref myDropDownList, Microsoft.VisualBasic.DateAndTime.MonthName(i, abreviado), Convert.ToString(i), false);
                }

            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Permite cargar un DropDownList con los meses de un año desde enero hasta el mes actual o todos los meses del año
        /// </summary>
        /// <param name="myDropDownList">El objecto DropDownList a llenar</param>
        /// <param name="ano">Año del cual se quieren cargar los meses</param>
        /// <param name="abreviado">Indica si el nombre del mes se va a abreviar o no, si se omite el valor por defecto es False, es decir que no se omite</param>
        /// <remarks>
        /// <list>Creado: Enero 08 de 2014 - Ing. David Pineda</list>
        /// </remarks>
        public static void cargarMesesToDropDownList(ref DropDownList myDropDownList, System.DateTime ano, bool abreviado = false)
        {
            if (ano == null)
            {
                throw new ArgumentNullException("El argumento ano no puede ser nulo");
            }

            int toMonth = 0;
            if (System.DateTime.Now.Year > ano.Year)
            {
                toMonth = 12;
            }
            else
            {
                toMonth = ano.Month;
            }
            try
            {
                myDropDownList.Items.Clear();
                for (int i = 1; i <= toMonth; i++)
                {
                    AddItemToDropDownList(ref myDropDownList, Microsoft.VisualBasic.DateAndTime.MonthName(i, abreviado), Convert.ToString(i), false);
                }
            }
            catch (ArgumentNullException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Permite cargar años en un DropDownList desde el año inicial hasta el final
        /// </summary>
        /// <param name="myDropDownList">DropDownList en donde se cargan los años</param>
        /// <param name="anoInicial">Año desde el cual se inicia el cargue del DropDownList</param>
        /// <param name="anoFinal">Año en el cual termina el cargue del DropDownList</param>
        /// <remarks>
        /// <list>Creado Enero 08 de 2013 - Ing. David Pineda</list>
        /// </remarks>
        public static void cargarAnosToDropDownList(ref DropDownList myDropDownList, System.DateTime anoInicial, System.DateTime anoFinal)
        {
            if (myDropDownList == null)
            {
                throw new ArgumentNullException("El argumento myDropDownList no puede ser nulo");
            }
            else if (anoInicial == null)
            {
                throw new ArgumentNullException("El argumento anoInicial no puede ser nulo");
            }
            else if (anoFinal == null)
            {
                throw new ArgumentNullException("El argumento anoFinal no puede ser nulo");
            }

            int difAnos = (int)Microsoft.VisualBasic.DateAndTime.DateDiff(Microsoft.VisualBasic.DateInterval.Year, anoInicial, anoFinal);

            myDropDownList.Items.Clear();
            for (int i = 0; i <= difAnos; i++)
            {
                AddItemToDropDownList(ref myDropDownList, Convert.ToString(anoInicial.Date.AddYears(i).Year), Convert.ToString(anoInicial.Date.AddYears(i).Year), false);
            }
        }

        /// <summary>
        /// Permite pararse en un item del DropDownList deacuerdo a su valor
        /// </summary>
        /// <param name="myDropDownList">Objeto DropDownList el cual actualizar</param>
        /// <param name="item_value">Valor a buscar en el DropDownLis</param>
        /// <remarks>
        /// <list> Creado: Enero 17 de 2013 - Ing. David Pineda </list>
        /// </remarks>
        public static void selectIndexByValue(ref DropDownList myDropDownList, string item_value)
        {
            if (myDropDownList == null)
            {
                throw new ArgumentNullException("El argumento myDropDownList es nulo");
            }

            if (item_value == string.Empty)
            {
                throw new ArgumentException("El argumento item_value esta vacio");
            }

            if (myDropDownList.Items.Count > 0)
            {
                try
                {
                    myDropDownList.SelectedIndex = myDropDownList.Items.IndexOf(myDropDownList.Items.FindByValue(item_value));
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }

        /// <summary>
        /// Permite pararse en un item del DropDownList deacuerdo a su texto
        /// </summary>
        /// <param name="myDropDownList">Objeto DropDownList el cual actualizar</param>
        /// <param name="item_text">Texto a buscar en el DropDownLis</param>
        /// <remarks>
        /// <list> Creado: Enero 17 de 2013 - Ing. David Pineda </list>
        /// </remarks>
        public static void selectIndexByText(ref DropDownList myDropDownList, string item_text)
        {
            if ((myDropDownList == null))
            {
                throw new ArgumentNullException("El argumento myDropDownList es nulo");
            }

            if (item_text == string.Empty)
            {
                throw new ArgumentException("El argumento item_text esta vacio");
            }

            if (myDropDownList.Items.Count > 0)
            {
                try
                {
                    myDropDownList.SelectedIndex = myDropDownList.Items.IndexOf(myDropDownList.Items.FindByText(item_text));
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        #endregion

        #region "TelerikControls"
#if Telerik
        /// <summary>
        /// Permite llenar un DropDownList a partir de una Datatable con los datos
        /// </summary>
        /// <param name="dtDatos">Objecto DataTable con los datos a cargar en el DropDownList</param>
        /// <param name="dataValueFiled">Nombre del campo en la DataTable que sera el value del DropDownList</param>
        /// <param name="dataTextField">Nombre del campo en la DataTable que sera el texto del DropDownList</param>
        /// <param name="myDropDownList">Objeto DropDownList que se llena</param>
        /// <remarks>
        /// <list>Creado: 25 de Noviembre de 2013 - Ing. David Pineda</list>
        /// </remarks>
        /// <exception cref="ArgumentNullException">Si alguno de los argumentos dtDatos o el myDropDownList es nulo</exception>
        /// <exception cref="ArgumentException">SI alguno de los argumentos dataValueFiled o dataTextField es vacio</exception>
        public static void llenarDropDownList(DataTable dtDatos, string dataValueFiled, string dataTextField, ref RadDropDownList myDropDownList)
        {
            if (dtDatos == null)
            {
                throw new ArgumentNullException("El argumento dtDatos es nulo");
            }

            if (myDropDownList == null)
            {
                throw new ArgumentNullException("El argumento myDropDownList es nulo");
            }

            if (dataValueFiled == string.Empty)
            {
                throw new ArgumentException("El argumento dataValueFiled esta vacio");
            }

            if (dataTextField == string.Empty)
            {
                throw new ArgumentException("El argumento dataTextField esta vacio");
            }

            try
            {
                myDropDownList.Items.Clear();
                myDropDownList.DataValueField = dataValueFiled;
                myDropDownList.DataTextField = dataTextField;
                myDropDownList.DataSource = dtDatos;
                myDropDownList.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Permite llenar un DropDownList a partir de una Datatable con los datos
        /// </summary>
        /// <param name="dtDatos">Objecto DataTable con los datos a cargar en el DropDownList</param>
        /// <param name="myDropDownList">Objeto DropDownList que se llena</param>
        /// <remarks>
        /// <list>Creado: 25 de Noviembre de 2013 - Ing. David Pineda</list>
        /// </remarks>
        public static void llenarDropDownList(DataTable dtDatos, ref RadDropDownList myDropDownList)
        {

            if (dtDatos == null)
            {
                throw new ArgumentNullException("El argumento dtDatos es nulo");
            }

            if (myDropDownList == null)
            {
                throw new ArgumentNullException("El argumento myDropDownList es nulo");
            }

            try
            {
                myDropDownList.Items.Clear();
                myDropDownList.DataValueField = myDropDownList.DataValueField;
                myDropDownList.DataTextField = myDropDownList.DataTextField;
                myDropDownList.DataSource = dtDatos;
                myDropDownList.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Metodo que carga un registro a un DropDownList
        /// </summary>
        /// <param name="cboDropDownList">Control DropDownList al que se le van a cargar los datos</param>
        /// <param name="strText">Texto del registro</param>
        /// <param name="strValue">Valor del registro</param>
        /// <param name="blnAddFirstItem">True si desea adicionarlo al inicio o False de lo contrario</param>
        /// <remarks>
        /// <list>Creaciòn: Jul 12/2013 - Ing. Paulo Cesar Pacheco Tovar</list>
        /// </remarks>
        public static void AddItemToDropDownList(ref RadDropDownList cboDropDownList, string strText, string strValue, bool blnAddFirstItem = false)
        {
            if (cboDropDownList == null)
            {
                throw new ArgumentNullException("El argumento cboDropDownList no puede ser nulo");
            }

            if (strValue == string.Empty)
            {
                throw new ArgumentException("El argumento strValue no puede estar vacio");
            }

            DropDownListItem objListItem = new DropDownListItem(strText, strValue);

            try
            {
                if (blnAddFirstItem)
                {
                    cboDropDownList.Items.Insert(0, objListItem);
                }
                else
                {
                    cboDropDownList.Items.Add(objListItem);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Permite cargar un DropDownList con los meses de un año desde enero hasta el mes actual
        /// </summary>
        /// <param name="myDropDownList">El objecto DropDownList a llenar</param>
        /// <param name="abreviado">Indica si el nombre del mes se va a abreviar o no, si se omite el valor por defecto es False, es decir que no se omite</param>
        /// <remarks>
        /// <list>Creado: Noviembre 28 de 2013 - Ing. David Pineda</list>
        /// </remarks>
        public static void cargarMesesToDropDownList(ref RadDropDownList myDropDownList, bool abreviado = false)
        {
            try
            {
                myDropDownList.Items.Clear();
                for (int i = 1; i <= DateTime.Now.Month; i++)
                {
                    AddItemToDropDownList(ref myDropDownList, Microsoft.VisualBasic.DateAndTime.MonthName(i, abreviado), Convert.ToString(i), false);
                }

            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Permite cargar un DropDownList con los meses de un año desde enero hasta el mes actual o todos los meses del año
        /// </summary>
        /// <param name="myDropDownList">El objecto DropDownList a llenar</param>
        /// <param name="ano">Año del cual se quieren cargar los meses</param>
        /// <param name="abreviado">Indica si el nombre del mes se va a abreviar o no, si se omite el valor por defecto es False, es decir que no se omite</param>
        /// <remarks>
        /// <list>Creado: Enero 08 de 2014 - Ing. David Pineda</list>
        /// </remarks>
        public static void cargarMesesToDropDownList(ref RadDropDownList myDropDownList, System.DateTime ano, bool abreviado = false)
        {
            if (ano == null)
            {
                throw new ArgumentNullException("El argumento ano no puede ser nulo");
            }

            int toMonth = 0;
            if (System.DateTime.Now.Year > ano.Year)
            {
                toMonth = 12;
            }
            else
            {
                toMonth = ano.Month;
            }
            try
            {
                myDropDownList.Items.Clear();
                for (int i = 1; i <= toMonth; i++)
                {
                    AddItemToDropDownList(ref myDropDownList, Microsoft.VisualBasic.DateAndTime.MonthName(i, abreviado), Convert.ToString(i), false);
                }
            }
            catch (ArgumentNullException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Permite cargar años en un DropDownList desde el año inicial hasta el final
        /// </summary>
        /// <param name="myDropDownList">DropDownList en donde se cargan los años</param>
        /// <param name="anoInicial">Año desde el cual se inicia el cargue del DropDownList</param>
        /// <param name="anoFinal">Año en el cual termina el cargue del DropDownList</param>
        /// <remarks>
        /// <list>Creado Enero 08 de 2013 - Ing. David Pineda</list>
        /// </remarks>
        public static void cargarAnosToDropDownList(ref RadDropDownList myDropDownList, System.DateTime anoInicial, System.DateTime anoFinal)
        {
            if (myDropDownList == null)
            {
                throw new ArgumentNullException("El argumento myDropDownList no puede ser nulo");
            }
            else if (anoInicial == null)
            {
                throw new ArgumentNullException("El argumento anoInicial no puede ser nulo");
            }
            else if (anoFinal == null)
            {
                throw new ArgumentNullException("El argumento anoFinal no puede ser nulo");
            }

            int difAnos = (int)Microsoft.VisualBasic.DateAndTime.DateDiff(Microsoft.VisualBasic.DateInterval.Year, anoInicial, anoFinal);

            myDropDownList.Items.Clear();
            for (int i = 0; i <= difAnos; i++)
            {
                AddItemToDropDownList(ref myDropDownList, Convert.ToString(anoInicial.Date.AddYears(i).Year), Convert.ToString(anoInicial.Date.AddYears(i).Year), false);
            }
        }

        /// <summary>
        /// Permite pararse en un item del DropDownList deacuerdo a su valor
        /// </summary>
        /// <param name="myDropDownList">Objeto DropDownList el cual actualizar</param>
        /// <param name="item_value">Valor a buscar en el DropDownLis</param>
        /// <remarks>
        /// <list> Creado: Enero 17 de 2013 - Ing. David Pineda </list>
        /// </remarks>
        public static void selectIndexByValue(ref RadDropDownList myDropDownList, string item_value)
        {
            if (myDropDownList == null)
            {
                throw new ArgumentNullException("El argumento myDropDownList es nulo");
            }

            if (item_value == string.Empty)
            {
                throw new ArgumentException("El argumento item_value esta vacio");
            }

            if (myDropDownList.Items.Count > 0)
            {
                try
                {
                    myDropDownList.SelectedIndex = myDropDownList.FindItemByValue(item_value).Index;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }

        /// <summary>
        /// Permite pararse en un item del DropDownList deacuerdo a su texto
        /// </summary>
        /// <param name="myDropDownList">Objeto DropDownList el cual actualizar</param>
        /// <param name="item_text">Texto a buscar en el DropDownLis</param>
        /// <remarks>
        /// <list> Creado: Enero 17 de 2013 - Ing. David Pineda </list>
        /// </remarks>
        public static void selectIndexByText(ref RadDropDownList myDropDownList, string item_text)
        {
            if ((myDropDownList == null))
            {
                throw new ArgumentNullException("El argumento myDropDownList es nulo");
            }

            if (item_text == string.Empty)
            {
                throw new ArgumentException("El argumento item_text esta vacio");
            }

            if (myDropDownList.Items.Count > 0)
            {
                try
                {
                    myDropDownList.SelectedIndex = myDropDownList.FindItemByText(item_text).Index;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
#endif
        #endregion
    }
}
