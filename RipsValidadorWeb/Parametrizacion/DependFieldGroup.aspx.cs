using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using RipsValidadorDao.ConnectionDB.Generales;
using RipsValidadorWeb.Clases;
using RipsValidadorWeb.Enumeradores;
using ControlesAsp.DropDownListControl;
using Telerik.Web.UI;
using RipsValidadorDao.Model;

namespace RipsValidadorWeb.Parametrizacion
{
    public partial class DependFieldGroup : System.Web.UI.Page
    {

        private DataTable _tablaDatos;

        public DataTable tablaDatos
        {
            get
            {
                if (this.Session["rgEncabezadoGrupo"] != null)
                {
                    return (DataTable)this.Session["rgEncabezadoGrupo"];
                }
                _tablaDatos = getDataTable();
                this.Session["rgEncabezadoGrupo"] = _tablaDatos;
                return _tablaDatos;
            }
            set { _tablaDatos = value; }
        }

        #region "Eventos"

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarComboTipoArchivo();
                cargarColumnas(this.ddlTipoArchivo.SelectedValue);
                this.Session["rgEncabezadoGrupo"] = null;
            }
        }

        protected void rgEncabezadoGrupo_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable objDtDatos = tablaDatos;
            if (objDtDatos != null)
            {
                this.rgEncabezadoGrupo.DataSource = tablaDatos;
            }
            else
            {
                this.rgEncabezadoGrupo.DataSource = new string[]{};
            }            
        }

        protected void rgEncabezadoGrupo_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.rgEncabezadoGrupo.MasterTableView.Rebind();
            }
        }

        protected void ddlTipoArchivo_SelectedIndexChanged(object sender, Telerik.Web.UI.DropDownListEventArgs e)
        {
            cargarColumnas(this.ddlTipoArchivo.SelectedValue);
            myPanel1.Visible = false;
        }

        protected void ddlNumColumna_SelectedIndexChanged(object sender, Telerik.Web.UI.DropDownListEventArgs e)
        {
            cargarGrilla();
            myPanel1.Visible = true;
        }
       
        protected void rgEncabezadoGrupo_UpdateCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridEditableItem g = (e.Item as GridEditableItem);
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            guardarDatos(userControl, g);
        }

        protected void rgEncabezadoGrupo_InsertCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            guardarDatos(userControl);
        }

        protected void rgEncabezadoGrupo_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName.Equals("Select"))
            {
                GridEditableItem g = (e.Item as GridEditableItem);
                int idEncabezado = Convert.ToInt32(g.GetDataKeyValue("id_enc_grupo"));
                Response.Redirect("DependFieldDetails.aspx?idEncabezado=" + idEncabezado);
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
        }

        protected void rgEncabezadoGrupo_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            eliminarRegisgtro((e.Item as GridEditableItem));
        }
        #endregion

        #region "Metodos"
        private DataTable getDataTable()
        {
            Consulta c = new Consulta();
            DataTable objDtDatos = null;
            try
            {
                objDtDatos = c.consultarEncabezadoGruposDependencias(this.ddlTipoArchivo.SelectedValue, int.Parse(this.ddlNumColumna.SelectedValue));
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
            this.Session["rgEncabezadoGrupo"] = null;
            this.rgEncabezadoGrupo.Rebind();
        }
        private void eliminarRegisgtro(GridEditableItem g)
        {
            Consulta c = new Consulta();
            int idEncGrupo = Convert.ToInt32(g.GetDataKeyValue("id_enc_grupo"));
            if (c.consultarDetalleGrupoDependencia(idEncGrupo).Rows.Count > 0)
            {
                this.RadWindowManager1.RadAlert("No se puede eliminar el grupo, existen detalles asociados a este, favor elimínelos primero "
                    + "antes de eliminar el grupo", 400, 200, Utilities.windowTitle(TypeMessage.information_message), null, Utilities.pathImageMessage(TypeMessage.information_message));
                return;
            }
            try
            {
                RipsValidadorDao.ConnectionDB.AutenticationProvider.Consulta c1 = new RipsValidadorDao.ConnectionDB.AutenticationProvider.Consulta();
                InsertUpdateDelete i = new InsertUpdateDelete(c1.consultarUsuarioXnombre(User.Identity.Name));
                EncabezadoGrupoVarDependiente e = c.consultarEncabezadoGruposDependenciasOBJ(idEncGrupo);
                i.IUDencGrupoVariableDependiente(e, 4);
                this.RadWindowManager1.RadAlert("Grupo eliminado correctamente", 400, 200, Utilities.windowTitle(TypeMessage.information_message),
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

        private void guardarDatos(UserControl userControl, GridEditableItem g = null)
        {
            string codArchivo = this.ddlTipoArchivo.SelectedValue;
            int numColumna = Convert.ToInt32(this.ddlNumColumna.SelectedValue);
            Consulta c = new Consulta();
            RipsValidadorDao.ConnectionDB.AutenticationProvider.Consulta c1 = new RipsValidadorDao.ConnectionDB.AutenticationProvider.Consulta();
            InsertUpdateDelete i = new InsertUpdateDelete(c1.consultarUsuarioXnombre(User.Identity.Name));
            try
            {
                EncabezadoGrupoVarDependiente e = new EncabezadoGrupoVarDependiente();
                e.datosArchivo = c.consultarEstructuraArchivo(codArchivo, numColumna);
                e.estado = c.consultarEstadoParametrizacionOBJ(Convert.ToInt16((userControl.FindControl("ddlEstado") as RadDropDownList).SelectedValue));
                e.descripcion = (userControl.FindControl("txtDescripcion") as RadTextBox).Text.Trim();
                if (g == null)
                {
                    e.idEncabezadoGrupo = 0;
                    i.IUDencGrupoVariableDependiente(e, 2);
                }
                else
                {
                    e.idEncabezadoGrupo = Convert.ToInt32(g.GetDataKeyValue("id_enc_grupo"));
                    i.IUDencGrupoVariableDependiente(e, 3);
                }
                cargarGrilla();
            }
            catch (Exception ex)
            {
                Logger.generarLogError(ex.Message, new System.Diagnostics.StackFrame(true), ex);
                this.RadWindowManager1.RadAlert(Utilities.errorMessage(), 400, 200, Utilities.windowTitle(TypeMessage.error_message),
                    null, Utilities.pathImageMessage(TypeMessage.error_message));
            }
        }
        #endregion

    }
}