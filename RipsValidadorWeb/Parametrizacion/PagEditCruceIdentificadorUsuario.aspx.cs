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
using Telerik.Web.UI;
using RipsValidadorDao.Model;

namespace RipsValidadorWeb.Parametrizacion
{
    public partial class PagEditCruceIdentificadorUsuario : System.Web.UI.Page
    {

        #region "Propiedades"
        private DataTable _tablaDatos;

        public DataTable tablaDatos
        {
            get
            {
                if (this.Session["rgEditCruces"] != null)
                {
                    return (DataTable)this.Session["rgEditCruces"];
                }
                _tablaDatos = getDataTable();
                this.Session["rgEditCruces"] = _tablaDatos;
                return _tablaDatos;
            }
            set { _tablaDatos = value; }
        }
        #endregion

        #region "Evetos"
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Session["rgEditCruces"] = null;
                ViewState["idCruce"] = Request.QueryString["idCruce"];
            }
        }

        protected void radGridCruces_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable objDtDatos = tablaDatos;
            if (objDtDatos != null)
            {
                this.rgEditCruces.DataSource = objDtDatos;
            }
            else
            {
                this.rgEditCruces.DataSource = new string[] { };
            }
        }

        protected void radGridCruces_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.rgEditCruces.MasterTableView.Rebind();
            }
        }

        protected void radGridCruces_UpdateCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridEditableItem g = (e.Item as GridEditableItem);
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            guardarDatos(userControl, g);
        }

        protected void radGridCruces_DeleteCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            eliminarRegisgtro((e.Item as GridEditableItem));
        }

        protected void radGridCruces_InsertCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            guardarDatos(userControl);
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("PagListCruceIdentificadorUsuario.aspx");
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
        #endregion

        #region "Metodos"
        private DataTable getDataTable()
        {
            Consulta c = new Consulta();
            DataTable objDtDatos = null;
            try
            {
                objDtDatos = c.consultarCruceAfiliadoColumna(Convert.ToInt32(ViewState["idCruce"]), 0);
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
            this.Session["rgEditCruces"] = null;
            this.rgEditCruces.Rebind();
        }

        private void guardarDatos(UserControl userControl, GridEditableItem g = null)
        {
            Consulta c = new Consulta();
            RipsValidadorDao.ConnectionDB.AutenticationProvider.Consulta c1 = new RipsValidadorDao.ConnectionDB.AutenticationProvider.Consulta();
            InsertUpdateDelete i = new InsertUpdateDelete(c1.consultarUsuarioXnombre(User.Identity.Name));
            string mensaje = string.Empty;
            try
            {
                CruceAfiliadoColumna c2 = new CruceAfiliadoColumna();
                c2.estado = Convert.ToInt16((userControl.FindControl("ddlEstado") as RadDropDownList).SelectedValue);
                c2.columnaCruce = c.consultarColumnaCruceOBJ(Convert.ToInt32((userControl.FindControl("ddlColumna") as RadDropDownList).SelectedValue));
                c2.cruceAfiliado = c.consultarCruceAfiliadoOBJ(Convert.ToInt32(ViewState["idCruce"]));
                if (g == null)
                {
                    if (c.consultarCruceAfiliadoColumnaOBJ(c2.cruceAfiliado.id, c2.columnaCruce.id) == null)
                    {
                        i.IUDcruceAfiliadoColumna(c2, 2);
                        mensaje = "Datos Guardados Correctamente";
                    }
                    else
                    {
                        this.RadWindowManager1.RadAlert("La columna que intenta asignar al cruce, ya hace parte de esté y no se puede duplicar, Favor seleccionar otra columna",
                            400, 200, Utilities.windowTitle(TypeMessage.information_message), null, Utilities.pathImageMessage(TypeMessage.information_message));
                        return;
                    }
                }
                else
                {
                    i.IUDcruceAfiliadoColumna(c2, 3);
                    mensaje = "Datos Actualizados Correctamente";
                }
                cargarGrilla();
                this.RadWindowManager1.RadAlert(mensaje, 400, 200, Utilities.windowTitle(TypeMessage.information_message),
                    null, Utilities.pathImageMessage(TypeMessage.information_message));
            }
            catch (Exception ex)
            {
                Logger.generarLogError(ex.Message, new System.Diagnostics.StackFrame(true), ex);
                this.RadWindowManager1.RadAlert(Utilities.errorMessage(), 400, 200, Utilities.windowTitle(TypeMessage.error_message),
                    null, Utilities.pathImageMessage(TypeMessage.error_message));    
            }
        }
        private void eliminarRegisgtro(GridEditableItem g)
        {
            Consulta c = new Consulta();
            int idCruce = Convert.ToInt32(g.GetDataKeyValue("id_cruce_afiliado"));
            int idColumna = Convert.ToInt32(g.GetDataKeyValue("id_cruce_columna"));
            try
            {
                RipsValidadorDao.ConnectionDB.AutenticationProvider.Consulta c1 = new RipsValidadorDao.ConnectionDB.AutenticationProvider.Consulta();
                InsertUpdateDelete i = new InsertUpdateDelete(c1.consultarUsuarioXnombre(User.Identity.Name));
                CruceAfiliadoColumna c2 = c.consultarCruceAfiliadoColumnaOBJ(idCruce, idColumna);
                i.IUDcruceAfiliadoColumna(c2, 4);
                this.RadWindowManager1.RadAlert("Detalle eliminado correctamente", 400, 200, Utilities.windowTitle(TypeMessage.information_message),
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
        #endregion

    }
}