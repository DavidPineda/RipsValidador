using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RipsValidadorDao.ConnectionDB.Generales;
using ControlesAsp.DataTableControl;
using RipsValidadorDao.Model;
using ControlesAsp.DropDownListControl;
using RipsValidadorWeb.Clases;
using RipsValidadorWeb.Enumeradores;
using System.Data;
using Telerik.Web.UI;

namespace RipsValidadorWeb.Parametrizacion
{
    public partial class ColumnSettings : System.Web.UI.Page
    {

        #region "Pripiedades"
        private DataTable _tablaDatos;
        public DataTable tablaDatos
        {
            get
            {
                if (this.Session["rgValPermitido"] != null)
                {
                    return (DataTable)this.Session["rgValPermitido"];
                }
                _tablaDatos = getDataTable();
                this.Session["rgValPermitido"] = _tablaDatos;
                return _tablaDatos;
            }
            set { _tablaDatos = value; }
        }
        #endregion

        #region "Eventos"
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Session["rgValPermitido"] = null;
                cargarComboTipoArchivo();
                cargarColumnas(this.ddlTipoArchivo.SelectedValue);
            }
        }

        protected void ddlTipoArchivo_SelectedIndexChanged(object sender, Telerik.Web.UI.DropDownListEventArgs e)
        {
            cargarColumnas(this.ddlTipoArchivo.SelectedValue);
            cargarGrilla();
            limpiarLabels();
            this.myPanel3.Visible = false;
        }

        protected void ddlNumColumna_SelectedIndexChanged(object sender, Telerik.Web.UI.DropDownListEventArgs e)
        {
            cargarGrilla();
            cargarDetalleEstructura();
        }

        protected void rgValPermitido_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.rgValPermitido.MasterTableView.Rebind();
            }
        }

        protected void rgValPermitido_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            this.rgValPermitido.DataSource = tablaDatos;
            this.tablaDatos.PrimaryKey = new DataColumn[] { this.tablaDatos.Columns["ID_VAL_PERMITIDO, COD_ARCHIVO, NUMERO_COLUMNA"] };
        }

        protected void rgValPermitido_UpdateCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            UserControl uc = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            GridEditableItem g = (GridEditableItem)e.Item;
            int tipoValor = int.Parse(((RadDropDownList)uc.FindControl("ddlTipoValor")).SelectedValue.ToString());
            string valor = ((RadTextBox)uc.FindControl("txtValor")).Text;
            string descripcion = ((RadTextBox)uc.FindControl("txtDescripcion")).Text;
            int id = (int)g.OwnerTableView.DataKeyValues[g.ItemIndex]["ID_VAL_PERMITIDO"];
            if (validarControl(tipoValor, valor, descripcion))
            {
                try
                {
                    Consulta c = new Consulta();
                    DatosEstructuraArchivo d = new DatosEstructuraArchivo(id, valor, descripcion);
                    d.tipoValor = c.consultarTipoValorOBJ(tipoValor);
                    d.estructuraArchivo = c.consultarEstructuraArchivo(this.ddlTipoArchivo.SelectedValue, int.Parse(this.ddlNumColumna.SelectedValue.ToString()));
                    modificarDatos(d);
                }
                catch (Exception ex)
                {
                    Logger.generarLogError(ex.Message, new System.Diagnostics.StackFrame(true), ex);
                    this.RadWindowManager1.RadAlert(Utilities.errorMessage(), 400, 200, Utilities.windowTitle(TypeMessage.error_message),
                        null, Utilities.pathImageMessage(TypeMessage.error_message));
                    e.Canceled = true;
                }
            }
            else
            {
                e.Canceled = true;
            }


        }

        protected void rgValPermitido_InsertCommand(object sender, GridCommandEventArgs e)
        {
            UserControl uc = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            int tipoValor = int.Parse(((RadDropDownList)uc.FindControl("ddlTipoValor")).SelectedValue.ToString());
            string valor = ((RadTextBox)uc.FindControl("txtValor")).Text;
            string descripcion = ((RadTextBox)uc.FindControl("txtDescripcion")).Text;
            if (validarControl(tipoValor, valor, descripcion))
            {
                Consulta c = new Consulta();
                try
                {
                    DatosEstructuraArchivo d = new DatosEstructuraArchivo(0, valor, descripcion);
                    d.tipoValor = c.consultarTipoValorOBJ(tipoValor);
                    d.estructuraArchivo = c.consultarEstructuraArchivo(this.ddlTipoArchivo.SelectedValue, int.Parse(this.ddlNumColumna.SelectedValue.ToString()));
                    guardarDatos(d);
                }
                catch(Exception ex){
                    Logger.generarLogError(ex.Message, new System.Diagnostics.StackFrame(true), ex);
                    this.RadWindowManager1.RadAlert(Utilities.errorMessage(), 400, 200, Utilities.windowTitle(TypeMessage.error_message),
                        null, Utilities.pathImageMessage(TypeMessage.error_message));
                    e.Canceled = true;
                }                
            }
            else
            {
                e.Canceled = true;
            }
        }

        protected void rgValPermitido_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            GridEditableItem g = (e.Item as GridEditableItem);
            eliminarDatos(g);
        }

        #endregion

        #region "Metodos"
        private void cargarComboTipoArchivo()
        {
            Consulta c = new Consulta();
            try
            {
                DropDownListASP.llenarDropDownList(c.consultarArchivosParametrizados(), "cod_archivo", "descripcion", ref this.ddlTipoArchivo);
                DropDownListASP.AddItemToDropDownList(ref this.ddlTipoArchivo, "SELECCIONE>>", "-1", true);
                DropDownListASP.selectIndexByValue(ref this.ddlTipoArchivo, "-1");
            }
            catch (Exception ex)
            {
                Logger.generarLogError(ex.Message, new System.Diagnostics.StackFrame(true), ex);
                this.RadWindowManager1.RadAlert(Utilities.errorMessage(), 400, 200, Utilities.windowTitle(TypeMessage.error_message),
                    null, Utilities.pathImageMessage(TypeMessage.error_message));
            }
        }
        private void cargarColumnas(string codArchivo)
        {
            Consulta c = new Consulta();
            try
            {
                DropDownListASP.llenarDropDownList(c.consultarEstructuraArchivo(codArchivo), "numero_columna", "nombre_columna", ref this.ddlNumColumna);
                DropDownListASP.AddItemToDropDownList(ref this.ddlNumColumna, "SELECCIONE>>", "-1", true);
                DropDownListASP.selectIndexByValue(ref this.ddlNumColumna, "-1");
            }
            catch (Exception ex)
            {
                Logger.generarLogError(ex.Message, new System.Diagnostics.StackFrame(true), ex);
                this.RadWindowManager1.RadAlert(Utilities.errorMessage(), 400, 200, Utilities.windowTitle(TypeMessage.error_message),
                    null, Utilities.pathImageMessage(TypeMessage.error_message));
            }
        }
        private void cargarDetalleEstructura()
        {
            Consulta c = new Consulta();
            EstructuraArchivo e = c.consultarEstructuraArchivo(this.ddlTipoArchivo.SelectedValue, int.Parse(this.ddlNumColumna.SelectedValue.ToString()));
            limpiarLabels();
            this.lblTextNomColumna.Text = e.nombreColumna;
            this.lblTextLngMinima.Text = Convert.ToString(e.longitud);
            this.lblTextLngMaxima.Text = Convert.ToString(e.longitudMaxima);
            if (e.tipoDato != null)
            {
                this.lblTextTipoDato.Text = e.tipoDato.descripcion;
            }
            if (e.estadoParametrizacion != null)
            {
                this.lblTextEstado.Text = e.estadoParametrizacion.descripcion;
            }
            if (e.formatoFecha != null)
            {
                this.lblTextFormatoFecha.Text = e.formatoFecha.descripcion;
            }            
            this.myPanel3.Visible = true;
        }
        private DataTable getDataTable()
        {
            DataTable myData = null;
            Consulta c = new Consulta();
            try
            {
                int columna = 0;
                bool validacion = int.TryParse(this.ddlNumColumna.SelectedValue, out columna);
                myData = c.consultarDatosEstructuraArchivo((string)this.ddlTipoArchivo.SelectedValue, validacion ? columna : 0);
            }
            catch (Exception ex)
            {
                Logger.generarLogError(ex.Message, new System.Diagnostics.StackFrame(true), ex);
                this.RadWindowManager1.RadAlert(Utilities.errorMessage(), 400, 200, Utilities.windowTitle(TypeMessage.error_message), 
                    null, Utilities.pathImageMessage(TypeMessage.error_message));
            }
            return myData;
        }
        private void cargarGrilla()
        {
            this.Session["rgValPermitido"] = null;
            this.rgValPermitido.MasterTableView.Rebind();            
        }
        private bool validarControl(int tipoValor, string valor, string descripcion)
        {
            if(this.ddlTipoArchivo.SelectedValue.Equals("-1")){
                this.RadWindowManager1.RadAlert("Seleccione un tipo de archivo", 400,200,Utilities.windowTitle(TypeMessage.information_message),
                    null,Utilities.pathImageMessage(TypeMessage.information_message));
                return false;
            }

            if (int.Parse(this.ddlNumColumna.SelectedValue.ToString()) == -1)
            {
                this.RadWindowManager1.RadAlert("Seleccione una columna", 400, 200, Utilities.windowTitle(TypeMessage.information_message),
                    null, Utilities.pathImageMessage(TypeMessage.information_message));
                return false;
            }

            if (valor.Equals(string.Empty))
            {
                this.RadWindowManager1.RadAlert("Ingrese un valor", 400, 200, Utilities.windowTitle(TypeMessage.information_message), 
                    null, Utilities.pathImageMessage(TypeMessage.information_message));
                return false;
            }

            Consulta c = new Consulta();
            EstructuraArchivo e = c.consultarEstructuraArchivo(this.ddlTipoArchivo.SelectedValue, int.Parse(this.ddlNumColumna.SelectedValue.ToString()));
            if (tipoValor == 1)// Si es valor Permitido y no por defecto
            {
                bool validacion = true;
                string mensaje = "El valor ingresado no es acepatado para una columna con tipo de dato ";
                switch (e.tipoDato.codTipoDato)
                {
                    case (int)Enumeradores.TipoDato.texto:
                        mensaje += "texto";
                        validacion = Validaciones.validarTextoNET(valor);
                        break;
                    case (int)Enumeradores.TipoDato.alfanumerico:
                        mensaje += "alfanumerico";
                        validacion = Validaciones.validarAlfanumericoNET(valor);
                        break;
                    case (int)Enumeradores.TipoDato.entero:
                        mensaje += "entero";
                        validacion = Validaciones.validarEnteroNET(valor);
                        break;
                    case (int)Enumeradores.TipoDato._decimal:
                        mensaje += "decimal";
                        validacion = Validaciones.validarDecimalNET(valor);
                        break;
                    case (int)Enumeradores.TipoDato.numerico:
                        mensaje += "numerico";
                        validacion = Validaciones.validarNumericoNET(valor);
                        break;
                    case (int)Enumeradores.TipoDato.fecha:                        
                        string[] array = Utilities.retornarFormatoFecha(e.formatoFecha.codFormatoFecha);
                        if (array[1].Equals(string.Empty))
                        {
                            validacion = Validaciones.validaFechaFormato(valor, array[0]);
                        }
                        else
                        {
                            validacion = Validaciones.validaFechaFormato(valor, array[0], array[1]);
                        }
                        mensaje += "Fecha con formato " + array[0];
                        break;
                }
                if (!validacion)
                {
                    this.RadWindowManager1.RadAlert(mensaje, 400, 200, Utilities.windowTitle(TypeMessage.information_message), 
                        null, Utilities.pathImageMessage(TypeMessage.information_message));
                    return false;
                }
            }

            return true;
        }
        private void guardarDatos(DatosEstructuraArchivo e)
        {
            _tablaDatos = tablaDatos;
            DataRow newRow = this.tablaDatos.NewRow();
            try
            {
                newRow["ID_VAL_PERMITIDO"] = e.idValPermitido;
                newRow["COD_ARCHIVO"] = e.estructuraArchivo.parametrizacionArchivo.codArchivo;
                newRow["NUMERO_COLUMNA"] = e.estructuraArchivo.numeroColumna;
                newRow["COD_TIPO_VALOR"] = e.tipoValor.codTipoValor;
                newRow["DESCRIPCION"] = e.descripcion;
                newRow["VALOR"] = e.valor;
                newRow["DESC_TIPO_VALOR"] = e.tipoValor.descripcion;
                bool res = DataTableASP.addItemDataTable(ref _tablaDatos, newRow, false, new int[] { 1, 2, 3, 5 });
                if (res)
                {
                    RipsValidadorDao.ConnectionDB.AutenticationProvider.Consulta c = new RipsValidadorDao.ConnectionDB.AutenticationProvider.Consulta();
                    InsertUpdateDelete i = new InsertUpdateDelete(c.consultarUsuarioXnombre(User.Identity.Name));
                    i.IUDdatosEstructuraAcrchivo(e, 2);
                    cargarGrilla();
                    this.RadWindowManager1.RadAlert("Valor Guardado Correctamente", 400, 200, Utilities.windowTitle(TypeMessage.information_message),
                        null, Utilities.pathImageMessage(TypeMessage.information_message));
                }
                else
                {
                    this.RadWindowManager1.RadAlert("El valor ingresado ya se encuentra parametrizado para la columna", 400, 200, 
                        Utilities.windowTitle(TypeMessage.information_message),null, Utilities.pathImageMessage(TypeMessage.information_message));
                }
            }
            catch (ArgumentNullException ex)
            {
                Logger.generarLogError(ex.Message, new System.Diagnostics.StackFrame(true), ex);
                throw ex;
            }
            catch (InvalidCastException ex)
            {
                Logger.generarLogError(ex.Message, new System.Diagnostics.StackFrame(true), ex);
                throw ex;
            }
            catch (Exception ex)
            {
                Logger.generarLogError(ex.Message, new System.Diagnostics.StackFrame(true), ex);
                throw ex;
            }
        }
        private void modificarDatos(DatosEstructuraArchivo e)
        {
            try
            {
                DataRow newRow = this.tablaDatos.NewRow();
                newRow["ID_VAL_PERMITIDO"] = e.idValPermitido;
                newRow["COD_ARCHIVO"] = e.estructuraArchivo.parametrizacionArchivo.codArchivo;
                newRow["NUMERO_COLUMNA"] = e.estructuraArchivo.numeroColumna;
                newRow["COD_TIPO_VALOR"] = e.tipoValor.codTipoValor;
                newRow["DESCRIPCION"] = e.descripcion;
                newRow["VALOR"] = e.valor;
                newRow["DESC_TIPO_VALOR"] = e.tipoValor.descripcion;
                DataRow[] changeRows = this.tablaDatos.Select("ID_VAL_PERMITIDO = " + e.idValPermitido);
                if (changeRows.Length != 1)
                {
                    return;
                }
                else
                {
                    if (DataTableASP.validarRepetido(tablaDatos, newRow, new int[] { 1, 2, 3, 5 }))
                    {
                        RipsValidadorDao.ConnectionDB.AutenticationProvider.Consulta c = new RipsValidadorDao.ConnectionDB.AutenticationProvider.Consulta();
                        InsertUpdateDelete i = new InsertUpdateDelete(c.consultarUsuarioXnombre(User.Identity.Name));
                        i.IUDdatosEstructuraAcrchivo(e, 3);
                        cargarGrilla();
                        this.RadWindowManager1.RadAlert("Valor Guardado Correctamente", 400, 200, Utilities.windowTitle(TypeMessage.information_message),
                            null, Utilities.pathImageMessage(TypeMessage.information_message));
                    }
                    else { 
                        this.RadWindowManager1.RadAlert("El valor ingresado ya se encuentra parametrizado para la columna", 400, 200,
                            Utilities.windowTitle(TypeMessage.information_message), null, Utilities.pathImageMessage(TypeMessage.information_message));
                    }
                }
            }
            catch (ArgumentNullException ex)
            {
                Logger.generarLogError(ex.Message, new System.Diagnostics.StackFrame(true), ex);
                throw ex;
            }
            catch (InvalidCastException ex)
            {
                Logger.generarLogError(ex.Message, new System.Diagnostics.StackFrame(true), ex);
                throw ex;
            }
            catch (Exception ex)
            {
                Logger.generarLogError(ex.Message, new System.Diagnostics.StackFrame(true), ex);
                throw ex;
            }
        }
        private void limpiarLabels()
        {
            this.lblTextNomColumna.Text = string.Empty;
            this.lblTextLngMinima.Text = string.Empty;
            this.lblTextLngMaxima.Text = string.Empty;
            this.lblTextTipoDato.Text = string.Empty;
            this.lblTextEstado.Text = string.Empty;
            this.lblTextFormatoFecha.Text = string.Empty;
        }

        private void eliminarDatos(GridEditableItem g)
        {
            string codArchivo = g.GetDataKeyValue("COD_ARCHIVO").ToString();
            int numColumna = int.Parse(g.GetDataKeyValue("NUMERO_COLUMNA").ToString());
            int idValPermitido = int.Parse(g.GetDataKeyValue("ID_VAL_PERMITIDO").ToString());
            Consulta c = new Consulta();
            try
            {
                DataTable objDtDatos = c.consultarVariablesDependientes(string.Empty, 0);
                DataRow[] rows1 = objDtDatos.Select("COD_ARCHIVO_DEP = '" + codArchivo + "' and NUM_COLUMNA_DEP = " + numColumna + " and ID_VAL_PERMITIDO_DEP = " + idValPermitido);
                DataRow[] rows2 = objDtDatos.Select("COD_ARCHIVO_CRU = '" + codArchivo + "' and NUM_COLUMNA_CRU = " + numColumna + " and ID_VAL_PERMITIDO_CRU = " + idValPermitido);
                if (rows1.Length > 0)
                {
                    this.RadWindowManager1.RadAlert("No se puede eliminar el registro, este esta asociado en variables dependientes, primero "
                        + "debe eliminarlo de variables dependientes para poder realizar esta operación", 400, 200, Utilities.windowTitle(TypeMessage.information_message),
                        null, Utilities.pathImageMessage(TypeMessage.information_message));
                    return;
                }
                if (rows2.Length > 0)
                {
                    this.RadWindowManager1.RadAlert("No se puede eliminar el registro, este esta asociado en variables dependientes, primero "
                        + "debe eliminarlo de variables dependientes para poder realizar esta operación", 400, 200, Utilities.windowTitle(TypeMessage.information_message),
                        null, Utilities.pathImageMessage(TypeMessage.information_message));
                    return;
                }
                DatosEstructuraArchivo d = c.consultarDatosEstructuraArchivoOBJ(idValPermitido, codArchivo, numColumna);
                RipsValidadorDao.ConnectionDB.AutenticationProvider.Consulta c1 = new RipsValidadorDao.ConnectionDB.AutenticationProvider.Consulta();
                InsertUpdateDelete i = new InsertUpdateDelete(c1.consultarUsuarioXnombre(User.Identity.Name));
                i.IUDdatosEstructuraAcrchivo(d, 4);
                this.RadWindowManager1.RadAlert("Registro eliminado con exito", 400, 200, Utilities.windowTitle(TypeMessage.information_message),
                    null, Utilities.pathImageMessage(TypeMessage.information_message));
                cargarGrilla();
            }
            catch(Exception ex){
                Logger.generarLogError(ex.Message, new System.Diagnostics.StackFrame(true), ex);
                this.RadWindowManager1.RadAlert(Utilities.errorMessage(), 400, 200, Utilities.windowTitle(TypeMessage.error_message),
                    null, Utilities.pathImageMessage(TypeMessage.error_message));
            }
        }
        #endregion

    }
}