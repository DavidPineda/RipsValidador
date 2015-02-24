using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RipsValidadorDao.ConnectionDB.Generales;
using ControlesAsp.DropDownListControl;
using RipsValidadorWeb.Clases;
using RipsValidadorWeb.Enumeradores;
using System.Data;
using RipsValidadorDao.Model;
using ControlesAsp.Panel;
using Telerik.Web.UI;

namespace RipsValidadorWeb.Parametrizacion
{
    public partial class FileStruct : System.Web.UI.Page
    {

        #region "Pripiedades"
        private DataTable _tablaDatos;
        public DataTable tablaDatos
        {
            get
            {
                if (this.Session["rgEstructura"] != null)
                {
                    return (DataTable)this.Session["rgEstructura"];
                }
                _tablaDatos = getDataTable();
                this.Session["rgEstructura"] = _tablaDatos;
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
                this.Session["rgEstructura"] = null;
                cargarComboTipoArchivo();
            }
        }
        protected void ddlTipoArchivo_SelectedIndexChanged(object sender, Telerik.Web.UI.DropDownListEventArgs e)
        {
            cargarPanelGrilla();
            this.myPanel3.Visible = false;
        }
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                cargarPanelDatos();
            }
            catch (Exception ex)
            {
                Logger.generarLogError(ex.Message, new System.Diagnostics.StackFrame(true), ex);
                this.RadWindowManager1.RadAlert(Utilities.errorMessage(), 400, 200, Utilities.windowTitle(TypeMessage.error_message), 
                    null, Utilities.pathImageMessage(TypeMessage.error_message));
            }            
        }
        protected void rgEstructura_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridEditableItem editItem = null;
            string codArchivo = string.Empty;
            int num_columna = 0;
            switch ((string)e.CommandName)
            {
                case "Update":
                    Consulta c = new Consulta();
                    editItem = (GridEditableItem)e.Item;
                    codArchivo = Convert.ToString(editItem.OwnerTableView.DataKeyValues[editItem.ItemIndex]["COD_ARCHIVO"]);
                    num_columna = Convert.ToInt32(editItem.OwnerTableView.DataKeyValues[editItem.ItemIndex]["NUMERO_COLUMNA"]);
                    try
                    {
                        cargarPanelDatos(c.consultarEstructuraArchivo(codArchivo, num_columna));
                    }
                    catch (Exception ex)
                    {
                        Logger.generarLogError(ex.Message, new System.Diagnostics.StackFrame(true), ex);
                        this.RadWindowManager1.RadAlert(Utilities.errorMessage(), 400, 200, Utilities.windowTitle(TypeMessage.error_message),
                            null, Utilities.pathImageMessage(TypeMessage.error_message));
                    }  
                    break;
                case "Delete":
                    editItem = (GridEditableItem)e.Item;
                    codArchivo = Convert.ToString(editItem.OwnerTableView.DataKeyValues[editItem.ItemIndex]["COD_ARCHIVO"]);
                    num_columna = Convert.ToInt32(editItem.OwnerTableView.DataKeyValues[editItem.ItemIndex]["NUMERO_COLUMNA"]);
                    eliminarEstructura(codArchivo, num_columna);
                    break;
            }
        }
        protected void rgEstructura_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            this.rgEstructura.DataSource = tablaDatos;
        }
        protected void ddlTipoDato_SelectedIndexChanged(object sender, Telerik.Web.UI.DropDownListEventArgs e)
        {
            validarCampos(int.Parse(this.ddlTipoDato.SelectedValue));
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            this.Page.Validate();
            if(IsValid)
            {
                if (validarArchivoColumna())
                {
                    guardarDatos();
                }               
            }            
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            PanelASP.limpiarPanel(ref this.myPanel3);
            this.myPanel3.Visible = false;
        }
        protected void cv2_txtRangoFin_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if ((this.txtRangoIni.Text.Equals(string.Empty) && !this.txtRangoFin.Text.Equals(string.Empty)) || (!this.txtRangoIni.Text.Equals(string.Empty) && this.txtRangoFin.Text.Equals(string.Empty)))
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }            
        }
        protected void cv1_txtRangoIni_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if ((this.txtRangoIni.Text.Equals(string.Empty) && !this.txtRangoFin.Text.Equals(string.Empty)) || (!this.txtRangoIni.Text.Equals(string.Empty) && this.txtRangoFin.Text.Equals(string.Empty)))
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }            
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
        private void cargarComboTipoDato()
        {
            Consulta c = new Consulta();
            try
            {
                DropDownListASP.llenarDropDownList(c.consultarTipoDato(), "value", "text", ref this.ddlTipoDato);
                DropDownListASP.AddItemToDropDownList(ref this.ddlTipoDato, "SELECCIONE>>", "-1", true);
                DropDownListASP.selectIndexByValue(ref this.ddlTipoDato, "-1");
            }
            catch (Exception ex)
            {
                Logger.generarLogError(ex.Message, new System.Diagnostics.StackFrame(true), ex);
                this.RadWindowManager1.RadAlert(Utilities.errorMessage(), 400, 200, Utilities.windowTitle(TypeMessage.error_message),
                    null, Utilities.pathImageMessage(TypeMessage.error_message));
            }
        }
        private void cargarComboEstado()
        {
            Consulta c = new Consulta();
            try
            {
                DropDownListASP.llenarDropDownList(c.consultarEstadoParametrizacion(), "value", "text", ref this.ddlEstado);
                DropDownListASP.AddItemToDropDownList(ref this.ddlEstado, "SELECCIONE>>", "-1", true);
                DropDownListASP.selectIndexByValue(ref this.ddlEstado, "-1");
            }
            catch (Exception ex)
            {
                Logger.generarLogError(ex.Message, new System.Diagnostics.StackFrame(true), ex);
                this.RadWindowManager1.RadAlert(Utilities.errorMessage(), 400, 200, Utilities.windowTitle(TypeMessage.error_message),
                    null, Utilities.pathImageMessage(TypeMessage.error_message));
            }
        }
        private void cargarComboFormatoFecha()
        {
            Consulta c = new Consulta();
            try
            {
                DropDownListASP.llenarDropDownList(c.consultarFormatoFecha(), "value", "text", ref this.ddlFormatoFecha);
                DropDownListASP.AddItemToDropDownList(ref this.ddlFormatoFecha, "SELECCIONE>>", "-1", true);
                DropDownListASP.selectIndexByValue(ref this.ddlFormatoFecha, "-1");
            }
            catch (Exception ex)
            {
                Logger.generarLogError(ex.Message, new System.Diagnostics.StackFrame(true), ex);
                this.RadWindowManager1.RadAlert(Utilities.errorMessage(), 400, 200, Utilities.windowTitle(TypeMessage.error_message),
                    null, Utilities.pathImageMessage(TypeMessage.error_message));
            }
        }
        private DataTable getDataTable()
        {
            Consulta c = new Consulta();
            DataTable objDtDatos = null;
            try
            {
                objDtDatos = c.consultarEstructuraArchivo(this.ddlTipoArchivo.SelectedValue);
            }
            catch (Exception ex)
            {
                Logger.generarLogError(ex.Message, new System.Diagnostics.StackFrame(true), ex);
                this.RadWindowManager1.RadAlert(Utilities.errorMessage(), 400, 200, Utilities.windowTitle(TypeMessage.error_message), 
                    null, Utilities.pathImageMessage(TypeMessage.error_message));
            }
            return objDtDatos;
        }
        private void cargarGrilla()
        {
            this.Session["rgEstructura"] = null;
            this.rgEstructura.Rebind();
        }
        private void cargarPanelGrilla()
        {
            this.myPanel2.Visible = true;
            cargarGrilla();
        }
        private void cargarPanelDatos(EstructuraArchivo e = null)
        {
            if (e == null)
            {
                try
                {
                    PanelASP.limpiarPanel(ref this.myPanel3);
                }
                catch (Exception ex)
                {
                    throw ex;                   
                }                
                cargarComboTipoDato();
                cargarComboEstado();
                cargarComboFormatoFecha();
                this.txtCodArchivo.Text = this.ddlTipoArchivo.SelectedText;
                this.txtNumColumna.Enabled = true;
            }
            else
            {
                cargarComboTipoDato();
                cargarComboEstado();
                cargarComboFormatoFecha();
                this.txtNumColumna.Enabled = false;
                cargarDatos(e);
                validarCampos(int.Parse(this.ddlTipoDato.SelectedValue));
            }
            this.myPanel3.Visible = true;
        }
        private void validarCampos(int tipoDato)
        {
            switch (tipoDato)
            {
                case -1:
                    habilitarTodos(true);
                    break;
                case 1:
                    habilitarTodos(false);
                    break;                    
                case 2:
                    habilitarTodos(false);
                    break;
                case 3:
                    habilitarRango(true);
                    habilitarFecha(false);
                    break;
                case 4:
                    habilitarRango(true);
                    habilitarFecha(false);
                    break;
                case 5:
                    habilitarRango(true);
                    habilitarFecha(false);
                    break;
                case 6:
                    habilitarRango(false);
                    habilitarFecha(true);
                    break;
            }
        }
        private void habilitarTodos(bool value)
        {
            this.txtRangoIni.Enabled = value;
            this.txtRangoFin.Enabled = value;
            this.ddlFormatoFecha.Enabled = value;
            this.rfv_ddlFormatoFecha.Enabled = value;
            if (!value)
            {
                this.txtRangoIni.Text = string.Empty;
                this.txtRangoFin.Text = string.Empty;
                DropDownListASP.selectIndexByValue(ref this.ddlFormatoFecha, "-1");
            }
        }
        private void habilitarRango(bool value)
        {
            this.txtRangoIni.Enabled = value;
            this.txtRangoFin.Enabled = value;
            if (!value)
            {
                this.txtRangoIni.Text = string.Empty;
                this.txtRangoFin.Text = string.Empty;
            }
        }
        private void habilitarFecha(bool value)
        {
            this.ddlFormatoFecha.Enabled = value;
            this.rfv_ddlFormatoFecha.Enabled = value;
            if (!value)
            {
                DropDownListASP.selectIndexByValue(ref this.ddlFormatoFecha, "-1");
            }            
        }
        private void cargarDatos(EstructuraArchivo e)
        {
            try
            {
                this.txtCodArchivo.Text = e.parametrizacionArchivo.codArchivo;
                this.txtNumColumna.Text = Convert.ToString(e.numeroColumna);
                this.txtNombreColumna.Text = e.nombreColumna;
                this.txtDescripcion.Text = e.descripcion;
                this.txtLongitudMin.Text = Convert.ToString(e.longitud);
                this.txtLongitudMax.Text = Convert.ToString(e.longitudMaxima);
                this.chValorRequerido.Checked = e.valorRequerido;
                this.chValidar.Checked = e.validar;
                DropDownListASP.selectIndexByValue(ref this.ddlTipoDato, Convert.ToString(e.tipoDato.codTipoDato));
                DropDownListASP.selectIndexByValue(ref this.ddlEstado, Convert.ToString(e.estadoParametrizacion.codEstado));
                if (e.rangoIni != -1)
                {
                    this.txtRangoIni.Text = Convert.ToString(e.rangoIni);
                }
                if (e.rangoFin != -1)
                {
                    this.txtRangoFin.Text = Convert.ToString(e.rangoFin);
                }
                if (e.formatoFecha != null)
                {
                    DropDownListASP.selectIndexByValue(ref this.ddlFormatoFecha, Convert.ToString(e.formatoFecha.codFormatoFecha));
                }                
            }
            catch (Exception ex)
            {
                Logger.generarLogError(ex.Message, new System.Diagnostics.StackFrame(true), ex);
                this.RadWindowManager1.RadAlert(Utilities.errorMessage(), 400, 200, Utilities.windowTitle(TypeMessage.error_message),
                    null, Utilities.pathImageMessage(TypeMessage.error_message));
            }
        }
        private void guardarDatos()
        {
            EstructuraArchivo e = new EstructuraArchivo();
            RipsValidadorDao.ConnectionDB.AutenticationProvider.Consulta c = new RipsValidadorDao.ConnectionDB.AutenticationProvider.Consulta();
            InsertUpdateDelete i = new InsertUpdateDelete(c.consultarUsuarioXnombre(User.Identity.Name));
            Consulta c1 = new Consulta();
            try{
                e.numeroColumna = int.Parse(this.txtNumColumna.Text);
                e.nombreColumna = this.txtNombreColumna.Text;
                e.longitud = int.Parse(this.txtLongitudMin.Text);
                e.descripcion = this.txtDescripcion.Text;
                e.longitudMaxima = int.Parse(this.txtLongitudMax.Text);
                e.validar = this.chValidar.Checked;
                e.valorRequerido = this.chValorRequerido.Checked;
                e.estadoParametrizacion = new EstadoParametrizacion(int.Parse(this.ddlEstado.SelectedValue), this.ddlEstado.SelectedText);
                e.tipoDato = new RipsValidadorDao.Model.TipoDato(int.Parse(this.ddlTipoDato.SelectedValue), this.ddlTipoDato.SelectedText);            
                e.formatoFecha = new FormatoFecha(int.Parse(this.ddlFormatoFecha.SelectedValue), this.ddlFormatoFecha.SelectedText);
                e.rangoIni = this.txtRangoIni.Text == string.Empty ? -1 : Single.Parse(this.txtRangoIni.Text, System.Globalization.CultureInfo.CreateSpecificCulture("es-CO"));
                e.rangoFin = this.txtRangoFin.Text == string.Empty ? -1 : Single.Parse(this.txtRangoFin.Text, System.Globalization.CultureInfo.CreateSpecificCulture("es-CO"));
                e.parametrizacionArchivo = c1.consultarArchivoParametrizado(this.ddlTipoArchivo.SelectedValue);
                Int16 codOperacion = 3;
                string mensaje = "Estructura Actualizada Correctamente";
                if (this.txtNumColumna.Enabled)
                {
                    codOperacion = 2;
                    mensaje = "Estructura Guardada Correctamente";
                }
                i.IUDestructuraArchivo(e, codOperacion);
                cargarGrilla();
                this.myPanel3.Visible = false;
                this.RadWindowManager1.RadAlert(mensaje, 400, 200, Utilities.windowTitle(TypeMessage.information_message),
                    null, Utilities.pathImageMessage(TypeMessage.information_message));
            }catch(InvalidCastException ex)
            {
                Logger.generarLogError(ex.Message, new System.Diagnostics.StackFrame(true), ex);
                this.RadWindowManager1.RadAlert(Utilities.errorMessage(), 400, 200, Utilities.windowTitle(TypeMessage.error_message), 
                    null, Utilities.pathImageMessage(TypeMessage.error_message));
            }
            catch (Exception ex)
            {
            Logger.generarLogError(ex.Message, new System.Diagnostics.StackFrame(true), ex);
            this.RadWindowManager1.RadAlert(Utilities.errorMessage(), 400, 200, Utilities.windowTitle(TypeMessage.error_message), 
                null, Utilities.pathImageMessage(TypeMessage.error_message));
            }
        }
        private bool validarArchivoColumna()
        {
            Consulta c = new Consulta();
            try
            {
                string codArchivo = this.ddlTipoArchivo.SelectedValue;
                int columna = Convert.ToInt32(this.txtNumColumna.Text);
                EstructuraArchivo e = c.consultarEstructuraArchivo(codArchivo, columna);
                if (e.numeroColumna == columna && this.txtNumColumna.Enabled)
                {
                    this.RadWindowManager1.RadAlert("Ya existe un parametrización para la columna seleccionada", 400, 200, Utilities.windowTitle(TypeMessage.information_message),
                        null, Utilities.pathImageMessage(TypeMessage.information_message));
                    return false;
                }
                else
                {
                    if (int.Parse(this.txtNumColumna.Text.ToString()) > e.parametrizacionArchivo.cantColumnas)
                    {
                        this.RadWindowManager1.RadAlert("Número de columna seleccionado supera el maximo de columnas del archivo", 400, 200, Utilities.windowTitle(TypeMessage.information_message),
                            null, Utilities.pathImageMessage(TypeMessage.information_message));
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.generarLogError(ex.Message, new System.Diagnostics.StackFrame(true), ex);
                this.RadWindowManager1.RadAlert(Utilities.errorMessage(), 400, 200, Utilities.windowTitle(TypeMessage.error_message),
                    null, Utilities.pathImageMessage(TypeMessage.error_message));
                return false;
            }
            return true;
        }
        private void eliminarEstructura(string codArchivo, int num_columna)
        {
            Consulta c = new Consulta();
            
            if (c.consultarDatosEstructuraArchivo(codArchivo, num_columna).Rows.Count > 0)
            {
                //Se valida contra la tabla RIPS_DATOS_ESTRUCTURA_ARCHIVO
                this.RadWindowManager1.RadAlert("No se puede eliminar la estructura, existen datos de variables asociados a la estructura, "
                    + "elimine estos datos antes de eliminar la columna", 400, 200, Utilities.windowTitle(TypeMessage.information_message), null, Utilities.pathImageMessage(TypeMessage.information_message));
                return;
            }
            if (c.consultarEncabezadoGruposDependencias(codArchivo, num_columna).Rows.Count > 0)
            {
                // Se valida contra la tabla RIPS_ENC_GRUPO_VARIABLE_DEPENDIENTE
                this.RadWindowManager1.RadAlert("No se puede eliminar la estructura, existen datos de encabezados de grupos de campos dependientes asociados a la estructura, "
                    + "elimine estos datos antes de eliminar la columna", 400, 200, Utilities.windowTitle(TypeMessage.information_message), null, Utilities.pathImageMessage(TypeMessage.information_message));
                return;
            }
            try
            {
                RipsValidadorDao.ConnectionDB.AutenticationProvider.Consulta c1 = new RipsValidadorDao.ConnectionDB.AutenticationProvider.Consulta();
                InsertUpdateDelete i = new InsertUpdateDelete(c1.consultarUsuarioXnombre(User.Identity.Name));
                EstructuraArchivo e = c.consultarEstructuraArchivo(codArchivo, num_columna);
                i.IUDestructuraArchivo(e, 4);
                this.RadWindowManager1.RadAlert("Registro eliminado correctamente", 400, 200, Utilities.windowTitle(TypeMessage.information_message),
                    null, Utilities.pathImageMessage(TypeMessage.information_message));
                cargarGrilla();
            }
            catch (Exception ex)
            {
                Logger.generarLogError(ex.Message, new System.Diagnostics.StackFrame(true), ex);
                this.RadWindowManager1.RadAlert(Utilities.errorMessage(), 400, 200, Utilities.windowTitle(TypeMessage.error_message),
                    null, Utilities.pathImageMessage(TypeMessage.error_message));
            }
            /*
                select * from RIPS_ERROR_VALIDACION_ARCHIVO
             */
        }
        #endregion

    }
}