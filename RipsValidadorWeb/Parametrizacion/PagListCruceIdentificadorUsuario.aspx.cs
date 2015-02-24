using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ControlesAsp.DataGridControl;
using RipsValidadorDao.ConnectionDB.Generales;
using RipsValidadorWeb.Clases;
using RipsValidadorWeb.Enumeradores;
using System.Data;
using Telerik.Web.UI;
using RipsValidadorDao.Model;


namespace RipsValidadorWeb.Parametrizacion
{
    public partial class PagListCruceIdentificadorUsuario : System.Web.UI.Page
    {

        #region "Pripiedades"
        
        private DataTable _tablaDatos;
        
        public DataTable tablaDatos
        {
            get
            {
                if (this.Session["radGridCruces"] != null)
                {
                    return (DataTable)this.Session["radGridCruces"];
                }
                _tablaDatos = getDataTable();
                this.Session["radGridCruces"] = _tablaDatos;
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
                this.Session["radGridCruces"] = null;
            }
        }

        protected void radGridCruces_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            DataTable objDtDatos = tablaDatos;
            if (objDtDatos != null)
            {
                this.radGridCruces.DataSource = tablaDatos;
            }
            else
            {
                this.radGridCruces.DataSource = new string[] { };
            }
        }

        protected void radGridCruces_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.radGridCruces.MasterTableView.Rebind();
            }
        }
        
        protected void radGridCruces_InsertCommand(object sender, GridCommandEventArgs e)
        {
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            guardarDatos(userControl);
        }

        protected void radGridCruces_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            GridEditableItem g = (e.Item as GridEditableItem);
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            guardarDatos(userControl, g);
        }

        protected void radGridCruces_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            eliminarRegisgtro((e.Item as GridEditableItem));
        }

        protected void radGridCruces_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName.Equals("Select"))
            {
                GridEditableItem g = (e.Item as GridEditableItem);
                int idCruce = Convert.ToInt32(g.GetDataKeyValue("id"));
                Response.Redirect("PagEditCruceIdentificadorUsuario.aspx?idCruce=" + idCruce);
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
        }
        
        #endregion

        #region "Metodos"
        private DataTable getDataTable()
        {
            Consulta c = new Consulta();
            DataTable objDtDatos = null;
            try
            {
                objDtDatos = c.consultarCruceAfiliado(0);
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
            this.Session["radGridCruces"] = null;
            this.radGridCruces.Rebind();
        }

        private void guardarDatos(UserControl userControl, GridEditableItem g = null)
        {
            Consulta c = new Consulta();
            RipsValidadorDao.ConnectionDB.AutenticationProvider.Consulta c1 = new RipsValidadorDao.ConnectionDB.AutenticationProvider.Consulta();
            InsertUpdateDelete i = new InsertUpdateDelete(c1.consultarUsuarioXnombre(User.Identity.Name));
            string mensaje = string.Empty;          
            try
            {
                CruceAfiliado c2 = new CruceAfiliado();
                c2.descripcion = (userControl.FindControl("txtDescripcion") as RadTextBox).Text.Trim();
                c2.prioridad = Convert.ToInt32((userControl.FindControl("txtPrioridad") as RadNumericTextBox).Text);
                c2.estado = Convert.ToInt16((userControl.FindControl("ddlEstado") as RadDropDownList).SelectedValue);
                if (validarPrioridad(c2.prioridad))
                { 
                    if (g == null)
                    {
                        c2.id = 0;
                        i.IUDcruceAfiliado(c2, 2);
                        mensaje = "Datos Guardados Correctamente";
                    }
                    else
                    {
                        c2.id = Convert.ToInt32(g.GetDataKeyValue("id"));
                        i.IUDcruceAfiliado(c2, 3);
                        mensaje = "Datos Actualizados Correctamente";
                    }
                    cargarGrilla();
                    this.RadWindowManager1.RadAlert(mensaje, 400, 200, Utilities.windowTitle(TypeMessage.information_message),
                        null, Utilities.pathImageMessage(TypeMessage.information_message));
                }
                else
                {
                    this.RadWindowManager1.RadAlert("La prioridad seleccionada para el cruce ya existe, por favor seleccione otro nivel de prioridad",
                        400, 200, Utilities.windowTitle(TypeMessage.information_message), null, Utilities.pathImageMessage(TypeMessage.information_message));
                }
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
            int idCruce = Convert.ToInt32(g.GetDataKeyValue("id"));
            if (c.consultarCruceAfiliadoColumna(idCruce, 0).Rows.Count > 0)
            {
                this.RadWindowManager1.RadAlert("No se puede eliminar el cruce, existen detalles asociados a esté, favor elimínelos primero "
                    + "antes de eliminar el cruce", 400, 200, Utilities.windowTitle(TypeMessage.information_message), null, Utilities.pathImageMessage(TypeMessage.information_message));
                return;
            }
            try
            {
                RipsValidadorDao.ConnectionDB.AutenticationProvider.Consulta c1 = new RipsValidadorDao.ConnectionDB.AutenticationProvider.Consulta();
                InsertUpdateDelete i = new InsertUpdateDelete(c1.consultarUsuarioXnombre(User.Identity.Name));
                CruceAfiliado c2 = c.consultarCruceAfiliadoOBJ(idCruce);
                i.IUDcruceAfiliado(c2, 4);
                this.RadWindowManager1.RadAlert("Cruce eliminado correctamente", 400, 200, Utilities.windowTitle(TypeMessage.information_message),
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

        private bool validarPrioridad(int prioridad)
        {
            Consulta c = new Consulta();
            try
            {
                DataTable objDtDatos = c.consultarCruceAfiliado(0);
                if (objDtDatos == null) return true;
                DataView d = new DataView(objDtDatos);
                d.RowFilter = "prioridad = " + prioridad;
                return d.ToTable().Rows.Count == 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

    }
     
}