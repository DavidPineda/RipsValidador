using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RipsValidadorDao.Model;
using RipsValidadorDao.ConnectionDB.Generales;
using RipsValidadorWeb.Enumeradores;
using RipsValidadorWeb.Clases;
using ControlesAsp.DropDownListControl;
using System.Data;
using Telerik.Web.UI;
using ControlesAsp.DataTableControl;

namespace RipsValidadorWeb.Parametrizacion
{
    public partial class DependsField : System.Web.UI.Page
    {

        #region "Pripiedades"
        private DataTable _tablaDatos;
        public DataTable tablaDatos
        {
            get
            {
                if (this.Session["rgDependencias"] != null)
                {
                    return (DataTable)this.Session["rgDependencias"];
                }
                _tablaDatos = getDataTable();
                this.Session["rgDependencias"] = _tablaDatos;
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
                cargarComboTipoArchivo();
                cargarColumnasDep(string.Empty);
                cargarColumnasCru(string.Empty);
                this.Session["rgDependencias"] = null;
            }
        }
        protected void ddlTipoArchivoDep_SelectedIndexChanged(object sender, Telerik.Web.UI.DropDownListEventArgs e)
        {
            cargarColumnasDep(this.ddlTipoArchivoDep.SelectedValue);
            ddlColumnaDep_SelectedIndexChanged(sender, e);
        }
        protected void ddlTipoArchivoCru_SelectedIndexChanged(object sender, Telerik.Web.UI.DropDownListEventArgs e)
        {
            cargarColumnasCru(this.ddlTipoArchivoCru.SelectedValue);
            ddlColumnaCru_SelectedIndexChanged(sender, e);
        }
        protected void ddlColumnaDep_SelectedIndexChanged(object sender, Telerik.Web.UI.DropDownListEventArgs e)
        {      
            cargarDatosDep(this.ddlTipoArchivoDep.SelectedValue, Convert.ToInt32(this.ddlColumnaDep.SelectedValue));
        }
        protected void ddlColumnaCru_SelectedIndexChanged(object sender, Telerik.Web.UI.DropDownListEventArgs e)
        {           
            cargarDatosCru(this.ddlTipoArchivoCru.SelectedValue, Convert.ToInt32(this.ddlColumnaCru.SelectedValue));
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Validate("Guardar");
            if(IsValid) guardarDatos();
        }
        protected void rgEstructura_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable objDtdDatos = tablaDatos;            
            if (tablaDatos != null) {
                this.rgDependencias.DataSource = objDtdDatos;
            }
            else
            {
                this.rgDependencias.DataSource = new string[] { };
            }
            //this.tablaDatos.PrimaryKey = new DataColumn[] { this.tablaDatos.Columns["id_var_dependiente, cod_archivo, numero_columna"] };
        }
        protected void rgEstructura_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.rgDependencias.MasterTableView.Rebind();
            }
        }
        protected void rgDependencias_UpdateCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridEditableItem g = (GridEditableItem)e.Item;
            UserControl uc = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            actualizarDatos(g, uc);
        }
        protected void rgDependencias_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            eliminarDatos((e.Item as GridDataItem));
        }
        protected void ddlValoresAceptadosDep_SelectedIndexChanged(object sender, DropDownListEventArgs e)
        {
            activarOtroValorDep(Convert.ToInt32(e.Value));
        }
        protected void ddlValoresAceptadosCru_SelectedIndexChanged(object sender, DropDownListEventArgs e)
        {
            activarOtroValorCru(Convert.ToInt32(e.Value));
        }

        #endregion

        #region "Metodos"
        private void cargarComboTipoArchivo()
        {
            Consulta c = new Consulta();
            try
            {
                DropDownListASP.llenarDropDownList(c.consultarArchivosParametrizados(), "cod_archivo", "descripcion", ref this.ddlTipoArchivoDep);
                DropDownListASP.AddItemToDropDownList(ref this.ddlTipoArchivoDep, "SELECCIONE>>", "-1", true);
                DropDownListASP.selectIndexByValue(ref this.ddlTipoArchivoDep, "-1");
                DropDownListASP.llenarDropDownList(c.consultarArchivosParametrizados(), "cod_archivo", "descripcion", ref this.ddlTipoArchivoCru);
                DropDownListASP.AddItemToDropDownList(ref this.ddlTipoArchivoCru, "SELECCIONE>>", "-1", true);
                DropDownListASP.selectIndexByValue(ref this.ddlTipoArchivoCru, "-1");
            }
            catch (Exception ex)
            {
                Logger.generarLogError(ex.Message, new System.Diagnostics.StackFrame(true), ex);
                this.RadWindowManager1.RadAlert(Utilities.errorMessage(), 400, 200, Utilities.windowTitle(TypeMessage.error_message),
                    null, Utilities.pathImageMessage(TypeMessage.error_message));
            }
        }
        private void cargarColumnasDep(string codArchivo)
        {
            Consulta c = new Consulta();
            try
            {
                DropDownListASP.llenarDropDownList(c.consultarEstructuraArchivo(codArchivo), "numero_columna", "nombre_columna", ref this.ddlColumnaDep);
                DropDownListASP.AddItemToDropDownList(ref this.ddlColumnaDep, "SELECCIONE>>", "-1", true);
                DropDownListASP.selectIndexByValue(ref this.ddlColumnaDep, "-1");
            }
            catch (Exception ex)
            {
                Logger.generarLogError(ex.Message, new System.Diagnostics.StackFrame(true), ex);
                this.RadWindowManager1.RadAlert(Utilities.errorMessage(), 400, 200, Utilities.windowTitle(TypeMessage.error_message),
                    null, Utilities.pathImageMessage(TypeMessage.error_message));
            }
        }
        private void cargarColumnasCru(string codArchivo)
        {
            Consulta c = new Consulta();
            try
            {
                DropDownListASP.llenarDropDownList(c.consultarEstructuraArchivo(codArchivo), "numero_columna", "nombre_columna", ref this.ddlColumnaCru);
                DropDownListASP.AddItemToDropDownList(ref this.ddlColumnaCru, "SELECCIONE>>", "-1", true);
                DropDownListASP.selectIndexByValue(ref this.ddlColumnaCru, "-1");
            }
            catch (Exception ex)
            {
                Logger.generarLogError(ex.Message, new System.Diagnostics.StackFrame(true), ex);
                this.RadWindowManager1.RadAlert(Utilities.errorMessage(), 400, 200, Utilities.windowTitle(TypeMessage.error_message),
                    null, Utilities.pathImageMessage(TypeMessage.error_message));
            }
        }
        private void cargarComboValoresAceptados(EstructuraArchivo e, ref Telerik.Web.UI.RadDropDownList r){
            Consulta c = new Consulta();
            try
            {
                DropDownListASP.llenarDropDownList(c.consultarValoresAceptados(e), "value", "text", ref r);
                DropDownListASP.AddItemToDropDownList(ref r, "SELECCIONE>>", "-1", true);
                DropDownListASP.selectIndexByValue(ref r, "-1");
                DropDownListASP.AddItemToDropDownList(ref r, "Otro Valor", "-2", false);
                DropDownListASP.AddItemToDropDownList(ref r, "Validar Contra el mismo", "-3", false);
            }
            catch (Exception ex)
            {
                Logger.generarLogError(ex.Message, new System.Diagnostics.StackFrame(true), ex);
                this.RadWindowManager1.RadAlert(Utilities.errorMessage(), 400, 200, Utilities.windowTitle(TypeMessage.error_message),
                    null, Utilities.pathImageMessage(TypeMessage.error_message));
            }
        }
        private void cargarComboTipoComparacion(ref Telerik.Web.UI.RadDropDownList r)
        {
            Consulta c = new Consulta();
            try
            {
                DropDownListASP.llenarDropDownList(c.consultarTipoComparacion(), "value", "text", ref r);
                DropDownListASP.AddItemToDropDownList(ref r, "SELECCIONE>>", "-1", true);
                DropDownListASP.selectIndexByValue(ref r, "-1");
            }
            catch (Exception ex)
            {
                Logger.generarLogError(ex.Message, new System.Diagnostics.StackFrame(true), ex);
                this.RadWindowManager1.RadAlert(Utilities.errorMessage(), 400, 200, Utilities.windowTitle(TypeMessage.error_message),
                    null, Utilities.pathImageMessage(TypeMessage.error_message));
            }
        }
        private void limpiarCamposCru()
        {
            this.lblTextDescripcionCru.Text = string.Empty;            
            this.lblTextFormatoFechaCru.Text = string.Empty;            
            this.lblTextLngMaximaCru.Text = string.Empty;            
            this.lblTextLngMinimaCru.Text = string.Empty;
            this.lblTextRangoIniCru.Text = string.Empty;
            this.lblTextRangoFinCru.Text = string.Empty;            
            this.lblTextTipoDatoCru.Text = string.Empty;
            this.txtOtroValorCru.Text = string.Empty;
        }
        private void limpiarCamposDep()
        {
            this.lblTextDescripcionDep.Text = string.Empty;
            this.lblTextFormatoFechaDep.Text = string.Empty;
            this.lblTextLngMaximaDep.Text = string.Empty;
            this.lblTextLngMinimaDep.Text = string.Empty;
            this.lblTextRangoIniDep.Text = string.Empty;
            this.lblTextRangoFinDep.Text = string.Empty;
            this.lblTextTipoDatoDep.Text = string.Empty;
            this.txtOtroValorDep.Text = string.Empty;
            this.txtMensaje.Text = string.Empty;
        }
        private void cargarDatosCru(string codArchivo, int numColumna)
        {
            this.myPanel2.Visible = false;
            this.btnGuardar.Visible = false;
            if (Convert.ToInt32(this.ddlColumnaCru.SelectedValue) != -1)
            {
                Consulta c = new Consulta();
                EstructuraArchivo e = c.consultarEstructuraArchivo(codArchivo, numColumna);
                limpiarCamposCru();
                this.lblTextDescripcionCru.Text = e.descripcion;
                this.lblTextLngMaximaCru.Text = Convert.ToString(e.longitudMaxima);
                this.lblTextLngMinimaCru.Text = Convert.ToString(e.longitud);
                this.lblTextRangoIniCru.Text = e.rangoIni != -1 ? Convert.ToString(e.rangoIni) : "N/A";
                this.lblTextRangoFinCru.Text = e.rangoFin != -1 ? Convert.ToString(e.rangoFin) : "N/A";
                if (e.formatoFecha != null)
                {
                    this.lblTextFormatoFechaCru.Text = e.formatoFecha.descripcion;
                }
                else
                {
                    this.lblTextFormatoFechaCru.Text = "N/A";
                }
                if (e.tipoDato != null)
                {
                    this.lblTextTipoDatoCru.Text = e.tipoDato.descripcion;
                }
                cargarComboValoresAceptados(e, ref this.ddlValoresAceptadosCru);
                cargarComboTipoComparacion(ref this.ddlTipoDenpendenciaCru);
                this.myPanel2.Visible = true;
            }
            if (this.myPanel1.Visible && this.myPanel2.Visible) this.btnGuardar.Visible = true;
        }
        private void cargarDatosDep(string codArchivo, int numColumna)
        {
            this.myPanel1.Visible = false;
            this.btnGuardar.Visible = false;
            this.myPanel4.Visible = false;
            if (Convert.ToInt32(this.ddlColumnaDep.SelectedValue) != -1)
            {
                Consulta c = new Consulta();
                EstructuraArchivo e = c.consultarEstructuraArchivo(codArchivo, numColumna);
                limpiarCamposDep();
                this.lblTextDescripcionDep.Text = e.descripcion;
                this.lblTextLngMaximaDep.Text = Convert.ToString(e.longitudMaxima);
                this.lblTextLngMinimaDep.Text = Convert.ToString(e.longitud);
                this.lblTextRangoIniDep.Text = e.rangoIni != -1 ? Convert.ToString(e.rangoIni) : "N/A";
                this.lblTextRangoFinDep.Text = e.rangoFin != -1 ? Convert.ToString(e.rangoFin) : "N/A";
                if (e.formatoFecha != null)
                {
                    this.lblTextFormatoFechaDep.Text = e.formatoFecha.descripcion;
                }
                else
                {
                    this.lblTextFormatoFechaDep.Text = "N/A";
                }
                if (e.tipoDato != null)
                {
                    this.lblTextTipoDatoDep.Text = e.tipoDato.descripcion;
                }
                cargarComboValoresAceptados(e, ref this.ddlValoresAceptadosDep);
                cargarComboTipoComparacion(ref this.ddlTipoDenpendenciaDep);
                this.myPanel1.Visible = true;
                this.myPanel4.Visible = true;
                cargarGrilla();
            }
            if (this.myPanel1.Visible && this.myPanel2.Visible) this.btnGuardar.Visible = true;
        }
        private void guardarDatos()
        {
            if (validarMismoValor())
            {
                Consulta c = new Consulta();
                RipsValidadorDao.ConnectionDB.AutenticationProvider.Consulta c1 = new RipsValidadorDao.ConnectionDB.AutenticationProvider.Consulta();
                InsertUpdateDelete i = new InsertUpdateDelete(c1.consultarUsuarioXnombre(User.Identity.Name));
                VariableDependiente v = new VariableDependiente(0, this.txtMensaje.Text.Trim(), 1);
                if (Convert.ToInt32(this.ddlValoresAceptadosDep.SelectedValue) == -2)
                {
                    v.otroValorDep = this.txtOtroValorDep.Text.Trim();
                }
                if (Convert.ToInt32(this.ddlValoresAceptadosCru.SelectedValue) == -2)
                {
                    v.otroValorCru = this.txtOtroValorCru.Text.Trim();
                }
                try
                {
                    int idDep = Convert.ToInt32(this.ddlValoresAceptadosDep.SelectedValue) > 0 ? Convert.ToInt32(this.ddlValoresAceptadosDep.SelectedValue) : 0;
                    int idCru = Convert.ToInt32(this.ddlValoresAceptadosCru.SelectedValue) > 0 ? Convert.ToInt32(this.ddlValoresAceptadosCru.SelectedValue) : 0;
                    DatosEstructuraArchivo d1 = new DatosEstructuraArchivo();
                    DatosEstructuraArchivo d2 = new DatosEstructuraArchivo();
                    if (idDep > 0)
                    {
                        v.estructuraDep = c.consultarDatosEstructuraArchivoOBJ(idDep, this.ddlTipoArchivoDep.SelectedValue, Convert.ToInt32(this.ddlColumnaDep.SelectedValue));
                    }
                    else
                    {
                        d1.estructuraArchivo = c.consultarEstructuraArchivo(this.ddlTipoArchivoDep.SelectedValue, Convert.ToInt32(this.ddlColumnaDep.SelectedValue));
                        d1.idValPermitido = Convert.ToInt32(this.ddlValoresAceptadosDep.SelectedValue);
                        v.estructuraDep = d1;
                    }
                    if (idCru > 0)
                    {
                        v.estructuraCru = c.consultarDatosEstructuraArchivoOBJ(idCru, this.ddlTipoArchivoCru.SelectedValue, Convert.ToInt32(this.ddlColumnaCru.SelectedValue));
                    }
                    else
                    {
                        d2.estructuraArchivo = c.consultarEstructuraArchivo(this.ddlTipoArchivoCru.SelectedValue, Convert.ToInt32(this.ddlColumnaCru.SelectedValue));
                        d2.idValPermitido = Convert.ToInt32(this.ddlValoresAceptadosCru.SelectedValue);
                        v.estructuraCru = d2;                    
                    }
                    v.estado = 1;
                    v.idVariableDependiente = 0;
                    v.mensajeError = this.txtMensaje.Text.Trim();
                    v.tipoComparacionDep = c.consultarTipoComparacionOBJ(Convert.ToInt16(this.ddlTipoDenpendenciaDep.SelectedValue));
                    v.tipoComparacionCru = c.consultarTipoComparacionOBJ(Convert.ToInt16(this.ddlTipoDenpendenciaCru.SelectedValue));
                    v.otroValorDep = this.txtOtroValorDep.Text.Trim();
                    v.otroValorCru = this.txtOtroValorCru.Text.Trim();
                    if (validarRepetido(v))
                    {
                        i.IUDvariablesDependientes(v, 2);
                        this.RadWindowManager1.RadAlert("Registro Guardado Correctamente", 400, 200, Utilities.windowTitle(TypeMessage.information_message),
                            null, Utilities.pathImageMessage(TypeMessage.information_message));
                        cargarGrilla();
                        activarOtroValorDep(1);
                        activarOtroValorCru(1);
                        cargarDatosDep(this.ddlTipoArchivoDep.SelectedValue, Convert.ToInt32(this.ddlColumnaDep.SelectedValue));
                        cargarDatosCru(this.ddlTipoArchivoCru.SelectedValue, Convert.ToInt32(this.ddlColumnaCru.SelectedValue));
                    }
                    else
                    {
                        this.RadWindowManager1.RadAlert("La validación que intenta parametrizar ya existe.", 400, 200, Utilities.windowTitle(TypeMessage.information_message),
                            null, Utilities.pathImageMessage(TypeMessage.information_message));
                    }
                }
                catch (Exception ex)
                {
                    Logger.generarLogError(ex.Message, new System.Diagnostics.StackFrame(true), ex);
                    this.RadWindowManager1.RadAlert(Utilities.errorMessage(), 400, 200, Utilities.windowTitle(TypeMessage.error_message),
                        null, Utilities.pathImageMessage(TypeMessage.error_message));
                }
            }
            else
            {
                this.RadWindowManager1.RadAlert("La validación contra sí mismo solo se puede aplicar cuando se utiliza en los dos lados de la expresión y archivos diferentes," 
                    + "Las demás validaciones solo se pueden aplicar sobre el mismo archivo",
                    400, 200, Utilities.windowTitle(TypeMessage.information_message), null, Utilities.pathImageMessage(TypeMessage.information_message));
            }
        }
        private bool validarMismoValor()
        {
            if (Convert.ToInt32(this.ddlValoresAceptadosDep.SelectedValue) == -3 || (Convert.ToInt32(this.ddlValoresAceptadosCru.SelectedValue) == -3))
            {
                if((Convert.ToInt32(this.ddlValoresAceptadosDep.SelectedValue) * Convert.ToInt32(this.ddlValoresAceptadosCru.SelectedValue)) == 9)
                    return !(this.ddlTipoArchivoDep.SelectedValue.Equals(this.ddlTipoArchivoCru.SelectedValue));
                    return false;
            }
            else
            {
                return (this.ddlTipoArchivoDep.SelectedValue.Equals(this.ddlTipoArchivoCru.SelectedValue));
            }
        }
        private void actualizarDatos(GridEditableItem g, UserControl uc)
        {
            Consulta c = new Consulta();
            RipsValidadorDao.ConnectionDB.AutenticationProvider.Consulta c1 = new RipsValidadorDao.ConnectionDB.AutenticationProvider.Consulta();
            InsertUpdateDelete i = new InsertUpdateDelete(c1.consultarUsuarioXnombre(User.Identity.Name));
            try
            {
                int id_var_dependiente = (int)g.OwnerTableView.DataKeyValues[g.ItemIndex]["id_var_dependiente"];
                VariableDependiente v = c.consultarVariablesDependientesOBJ(id_var_dependiente);
                v.mensajeError = ((RadTextBox)uc.FindControl("txtMensaje")).Text.Trim();
                v.estado = Convert.ToInt16(((RadDropDownList)uc.FindControl("ddlEstado")).SelectedValue);
                i.IUDvariablesDependientes(v, 3);
                this.RadWindowManager1.RadAlert("Registro Actualizado Correctamente", 400, 200, Utilities.windowTitle(TypeMessage.information_message),
                        null, Utilities.pathImageMessage(TypeMessage.information_message));
                cargarGrilla();
                activarOtroValorDep(1);
                activarOtroValorCru(1);
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
                objDtDatos = c.consultarVariablesDependientes(this.ddlTipoArchivoDep.SelectedValue, Convert.ToInt32(this.ddlColumnaDep.SelectedValue));
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
            this.Session["rgDependencias"] = null;
            this.rgDependencias.Rebind();
        }
        private bool validarRepetido(VariableDependiente v)
        {
            DataRow newRow = this.tablaDatos.NewRow();
            DataTable Copia = tablaDatos;
            Copia.PrimaryKey = null;
            newRow["id_var_dependiente"] = v.idVariableDependiente;
            newRow["cod_archivo_dep"] = v.estructuraDep.estructuraArchivo.parametrizacionArchivo.codArchivo;
            newRow["desc_archivo_dep"] = v.estructuraDep.estructuraArchivo.parametrizacionArchivo.descripcion;
            newRow["cod_archivo_cru"] = v.estructuraCru.estructuraArchivo.parametrizacionArchivo.codArchivo;
            newRow["desc_archivo_cru"] = v.estructuraCru.estructuraArchivo.parametrizacionArchivo.descripcion;
            newRow["num_columna_dep"] = v.estructuraDep.estructuraArchivo.numeroColumna;
            newRow["nom_columna_dep"] = v.estructuraDep.estructuraArchivo.nombreColumna;
            newRow["num_columna_cru"] = v.estructuraCru.estructuraArchivo.numeroColumna;
            newRow["nom_columna_cru"] = v.estructuraCru.estructuraArchivo.nombreColumna;
            newRow["id_val_permitido_dep"] = v.estructuraDep.idValPermitido;
            newRow["valor_columna_dep"] = v.estructuraDep.valor;
            newRow["id_val_permitido_cru"] = v.estructuraCru.idValPermitido;
            newRow["valor_columna_cru"] = v.estructuraCru.valor;
            newRow["tipo_comparacion_dep"] = v.tipoComparacionDep.codOperadorLogico;
            newRow["desc_tipo_comparacion_dep"] = v.tipoComparacionDep.descripcion;
            newRow["tipo_comparacion_cru"] = v.tipoComparacionCru.codOperadorLogico;
            newRow["desc_tipo_comparacion_cru"] = v.tipoComparacionCru.codOperadorLogico;
            newRow["mensaje"] = v.mensajeError;
            newRow["estado_parametrizado"] = v.estado;
            return DataTableASP.validarRepetido(Copia, newRow, new int[] { 1, 3, 5, 7, 9, 11, 13, 15 });
        }
        private void eliminarDatos(GridDataItem g)
        {
            int id_var_dependiente = int.Parse(g.GetDataKeyValue("id_var_dependiente").ToString());
            Consulta c = new Consulta();
            try
            {
                DataTable objDtDatos = c.consultarDetalleGrupoDependencia(0);
                DataRow[] rows = objDtDatos.Select("id_var_dependiente = " + id_var_dependiente);
                if (rows.Length > 0)
                {
                    this.RadWindowManager1.RadAlert("No se puede eliminar el registro, existen detalles de grupos que hacen referencia al registro, "
                        + "Primero debe eliminar los detalles de cruces de campos dependientes", 400, 200, Utilities.windowTitle(TypeMessage.information_message),
                        null, Utilities.pathImageMessage(TypeMessage.information_message));
                    return;
                }
                RipsValidadorDao.ConnectionDB.AutenticationProvider.Consulta c1 = new RipsValidadorDao.ConnectionDB.AutenticationProvider.Consulta();
                InsertUpdateDelete i = new InsertUpdateDelete(c1.consultarUsuarioXnombre(User.Identity.Name));
                VariableDependiente v = c.consultarVariablesDependientesOBJ(id_var_dependiente);
                i.IUDvariablesDependientes(v, 4);
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
            
        }
        private void activarOtroValorDep(int value)
        {
            bool valor = false;
            ControlesAsp.DropDownListControl.DropDownListASP.selectIndexByValue(ref this.ddlTipoDenpendenciaDep, "-1");
            this.ddlTipoDenpendenciaDep.Enabled = true;
            if (Convert.ToInt32(value) == -2)
            {
                valor = true;
            }
            else if (Convert.ToInt32(value) == -3)
            {
                this.ddlTipoDenpendenciaDep.Enabled = false;
                ControlesAsp.DropDownListControl.DropDownListASP.selectIndexByValue(ref this.ddlTipoDenpendenciaDep, "2");
            }
            this.lblOtroValorDep.Visible = valor;
            this.txtOtroValorDep.Visible = valor;
            this.rfv_txtOtroValorDep.Enabled = valor;     
        }
        private void activarOtroValorCru(int value)
        {
            bool valor = false;
            ControlesAsp.DropDownListControl.DropDownListASP.selectIndexByValue(ref this.ddlTipoDenpendenciaCru, "-1");
            this.ddlTipoDenpendenciaCru.Enabled = true;
            if (Convert.ToInt32(value) == -2)
            {
                valor = true;
            }
            else if (Convert.ToInt32(value) == -3)
            {
                this.ddlTipoDenpendenciaCru.Enabled = false;
                ControlesAsp.DropDownListControl.DropDownListASP.selectIndexByValue(ref this.ddlTipoDenpendenciaCru, "2");
            }
            this.lblOtroValorCru.Visible = valor;
            this.txtOtroValorCru.Visible = valor;
            this.rfv_txtOtroValorCru.Enabled = valor;
        }
        #endregion

    }
}