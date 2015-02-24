using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using RipsValidadorWeb.Clases;
using RipsValidadorWeb.Enumeradores;
using RipsValidadorDao.Model;
using RipsValidadorDao.ConnectionDB.Generales;
using Telerik.Web.UI;

namespace RipsValidadorWeb.Parametrizacion
{
    public partial class DependFieldDetails : System.Web.UI.Page
    {
        #region "Pripiedades"
        private DataTable _tablaDatos;
        private DataTable _tablaDatos2;
        public DataTable tablaDatos
        {
            get
            {
                if (this.Session["rgDetalles"] != null)
                {
                    return (DataTable)this.Session["rgDetalles"];
                }
                _tablaDatos = getDataTable();
                this.Session["rgDetalles"] = _tablaDatos;
                return _tablaDatos;
            }
            set { _tablaDatos = value; }
        }
        public DataTable tablaDatos2
        {
            get
            {
                if (this.Session["rgDependientes"] != null)
                {
                    return (DataTable)this.Session["rgDependientes"];
                }
                _tablaDatos2 = getDataTable2();
                this.Session["rgDependientes"] = _tablaDatos2;
                return _tablaDatos2;
            }
            set { _tablaDatos2 = value; }
        }
        #endregion

        #region "Eventos"
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Session["rgDetalles"] = null;
                this.Session["rgDependientes"] = null;
            }
        }

        protected void rgDetalles_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            this.rgDetalles.DataSource = tablaDatos;
        }

        protected void rgDetalles_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.rgDetalles.MasterTableView.Rebind();
            }
        }

        protected void RadGrid1_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.rgDependientes.MasterTableView.Rebind();
            }
        }

        protected void rgDetalles_RowDrop(object sender, Telerik.Web.UI.GridDragDropEventArgs e)
        {
            if (string.IsNullOrEmpty(e.HtmlElement))
            {
                if (e.DraggedItems[0].OwnerGridID == rgDetalles.ClientID)
                {
                    if ((e.DestDataItem == null && tablaDatos2.Rows.Count == 0) ||
                        e.DestDataItem != null && e.DestDataItem.OwnerGridID == this.rgDependientes.ClientID)
                    {
                        foreach (GridDataItem g in e.DraggedItems)
                        {
                            guardarDatos(g);
                        }
                    }
                }
            }
        }

        protected void RadGrid1_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            this.rgDependientes.DataSource = tablaDatos2;
        }

        protected void rgDependientes_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            GridEditableItem g = (e.Item as GridEditableItem);
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            actualizarDatos(userControl, g);
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("DependFieldGroup.aspx");
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }

        protected void rgDependientes_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            eliminarRegistro((e.Item as GridEditableItem));
        }
        #endregion

        #region "Metodos"
        private DataTable getDataTable()
        {
            Consulta c = new Consulta();
            DataTable objDtDatos = null;
            int idEncabezado = Request.QueryString["idEncabezado"] != null ? Convert.ToInt32(Request.QueryString["idEncabezado"]) : 0;
            EncabezadoGrupoVarDependiente e = c.consultarEncabezadoGruposDependenciasOBJ(idEncabezado); 
            try
            {
                objDtDatos = c.consultarVariablesDependientes(e.datosArchivo.parametrizacionArchivo.codArchivo, e.datosArchivo.numeroColumna);
            }
            catch (Exception ex)
            {
                Logger.generarLogError(ex.Message, new System.Diagnostics.StackFrame(true), ex);
                this.RadWindowManager1.RadAlert(Utilities.errorMessage(), 400, 200, Utilities.windowTitle(TypeMessage.error_message),
                    null, Utilities.pathImageMessage(TypeMessage.error_message));
            }
            return objDtDatos;
        }

        private DataTable getDataTable2()
        {
            Consulta c = new Consulta();
            DataTable objDtDatos = null;
            int idEncabezado = Request.QueryString["idEncabezado"] != null ? Convert.ToInt32(Request.QueryString["idEncabezado"]) : 0;
            try
            {
                objDtDatos = c.consultarDetalleGrupoDependencia(idEncabezado);
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
            this.Session["rgDetalles"] = null;
            this.rgDetalles.Rebind();
        }

        private void cargarGrilla2()
        {
            this.Session["rgDependientes"] = null;
            this.rgDependientes.Rebind();
        }

        private void guardarDatos(GridDataItem g)
        {
            DataRow[] d = this.tablaDatos2.Select("id_var_dependiente = " + g.OwnerTableView.DataKeyValues[g.ItemIndex]["id_var_dependiente"]);
            if (d.Length == 0)
            {
                Consulta c = new Consulta();
                RipsValidadorDao.ConnectionDB.AutenticationProvider.Consulta c1 = new RipsValidadorDao.ConnectionDB.AutenticationProvider.Consulta();
                InsertUpdateDelete i = new InsertUpdateDelete(c1.consultarUsuarioXnombre(User.Identity.Name));
                DetalleGrupoDependiente g1 = new DetalleGrupoDependiente(0);
                try
                {
                    g1.varDependiente = c.consultarVariablesDependientesOBJ((int)g.OwnerTableView.DataKeyValues[g.ItemIndex]["id_var_dependiente"]);
                    g1.estado = c.consultarEstadoParametrizacionOBJ(Convert.ToInt32(g.GetDataKeyValue("estado_parametrizado")));
                    int idEncabezado = Request.QueryString["idEncabezado"] != null ? Convert.ToInt32(Request.QueryString["idEncabezado"]) : 0;
                    g1.encabezadoGrupo = c.consultarEncabezadoGruposDependenciasOBJ(idEncabezado);
                    g1.descripcion = "Cruce Automatico";
                    i.IUDdetGrupoVariableDependiente(g1, 2);
                    this.RadWindowManager1.RadAlert("Datos agregados correctamente", 400, 200, Utilities.windowTitle(TypeMessage.information_message),
                        null, Utilities.pathImageMessage(TypeMessage.information_message));
                    cargarGrilla2();
                }
                catch(Exception ex)
                {
                    Logger.generarLogError(ex.Message, new System.Diagnostics.StackFrame(true), ex);
                    this.RadWindowManager1.RadAlert(Utilities.errorMessage(),400,200,Utilities.windowTitle(TypeMessage.error_message),
                        null, Utilities.pathImageMessage(TypeMessage.error_message));
                }
            }
            else
            {
                this.RadWindowManager1.RadAlert("La validación que intenta agrupar ya se encuentra en el grupo.", 400, 200,
                    Utilities.windowTitle(TypeMessage.information_message), null, Utilities.pathImageMessage(TypeMessage.information_message));
            }
        }

        private void actualizarDatos(UserControl userControl, GridEditableItem g)
        {
            Consulta c = new Consulta();
            RipsValidadorDao.ConnectionDB.AutenticationProvider.Consulta c1 = new RipsValidadorDao.ConnectionDB.AutenticationProvider.Consulta();
            DetalleGrupoDependiente d = null;
            InsertUpdateDelete i = new InsertUpdateDelete(c1.consultarUsuarioXnombre(User.Identity.Name));
            try
            {
                d = c.consultarDetalleGrupoDependenciaOBJ(Convert.ToInt32(g.GetDataKeyValue("id_grupo")));
                d.estado = c.consultarEstadoParametrizacionOBJ(Convert.ToInt16((userControl.FindControl("ddlEstado") as RadDropDownList).SelectedValue));
                d.descripcion = (userControl.FindControl("txtDescripcion") as RadTextBox).Text.Trim();
                i.IUDdetGrupoVariableDependiente(d, 3);
                this.RadWindowManager1.RadAlert("Datos actualizados correctamente", 400, 200, Utilities.windowTitle(TypeMessage.information_message),
                    null, Utilities.pathImageMessage(TypeMessage.information_message));
                cargarGrilla2();
            }
            catch (Exception ex)
            {
                Logger.generarLogError(ex.Message, new System.Diagnostics.StackFrame(true), ex);
                this.RadWindowManager1.RadAlert(Utilities.errorMessage(), 400, 200, Utilities.windowTitle(TypeMessage.error_message),
                    null, Utilities.pathImageMessage(TypeMessage.error_message));
            }
        }

        private void eliminarRegistro(GridEditableItem g)
        {
            int idEncGrupo = Convert.ToInt32(g.GetDataKeyValue("id_enc_grupo"));
            int idGrupo = Convert.ToInt32(g.GetDataKeyValue("id_grupo"));
            try
            {
                Consulta c = new Consulta();
                RipsValidadorDao.ConnectionDB.AutenticationProvider.Consulta c1 = new RipsValidadorDao.ConnectionDB.AutenticationProvider.Consulta();
                InsertUpdateDelete i = new InsertUpdateDelete(c1.consultarUsuarioXnombre(User.Identity.Name));
                DetalleGrupoDependiente d = c.consultarDetalleGrupoDependenciaOBJ(idGrupo);
                i.IUDdetGrupoVariableDependiente(d, 4);
                this.RadWindowManager1.RadAlert("Registro Eliminado Correctamente", 400, 200, Utilities.windowTitle(TypeMessage.information_message),
                    null, Utilities.pathImageMessage(TypeMessage.information_message));
                cargarGrilla2();
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