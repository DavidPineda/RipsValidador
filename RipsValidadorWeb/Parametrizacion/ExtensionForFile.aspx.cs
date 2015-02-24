using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using RipsValidadorDao.ConnectionDB.Generales;
using RipsValidadorWeb.Clases;
using RipsValidadorWeb.Enumeradores;
using RipsValidadorDao.Model;

namespace RipsValidadorWeb.Parametrizacion
{
    public partial class ExtensionForFile : System.Web.UI.Page
    {
        #region "Pripiedades"
        private DataTable _tablaDatos;
        private DataTable _tablaDatos2;
        public DataTable tablaDatos
        {
            get
            {
                if (this.Session["rgExtensiones"] != null)
                {
                    return (DataTable)this.Session["rgExtensiones"];
                }
                _tablaDatos = getDataTable();
                this.Session["rgExtensiones"] = _tablaDatos;
                return _tablaDatos;
            }
            set { _tablaDatos = value; }
        }

        public DataTable tablaDatos2
        {
            get
            {
                if (this.Session["rgExtArchivo"] != null)
                {
                    return (DataTable)this.Session["rgExtArchivo"];
                }
                _tablaDatos2 = getDataTable2();
                this.Session["rgExtArchivo"] = _tablaDatos2;
                return _tablaDatos2;
            }
            set { _tablaDatos = value; }
        }
        #endregion

        #region "Eventos"
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Session["rgExtensiones"] = null;
                this.Session["rgExtArchivo"] = null;
                if (Request.QueryString["CodArchivo"] != null)
                {
                    ViewState["CodArchivo"] = Convert.ToString(Request.QueryString["CodArchivo"]);
                    cargarDatosArchivo();
                }
            }
        }

        protected void rgExtensiones_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            this.rgExtensiones.DataSource = tablaDatos;
        }

        protected void rgExtensiones_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.rgExtensiones.MasterTableView.Rebind();
            }
        }

        protected void rgExtArchivo_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            this.rgExtArchivo.DataSource = tablaDatos2;
        }

        protected void rgExtArchivo_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.rgExtArchivo.MasterTableView.Rebind();
            }
        }

        protected void rgExtensiones_InsertCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            UserControl uc = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            guardarDatos(uc);
        }

        protected void rgExtensiones_UpdateCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            UserControl uc = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            GridEditableItem g = (e.Item as GridEditableItem);
            guardarDatos(uc, g);
        }

        protected void rgExtensiones_RowDrop(object sender, Telerik.Web.UI.GridDragDropEventArgs e)
        {
            if (string.IsNullOrEmpty(e.HtmlElement))
            {
                if (e.DraggedItems[0].OwnerGridID == rgExtensiones.ClientID)
                {
                    if ((e.DestDataItem == null && tablaDatos2.Rows.Count == 0) ||
                        e.DestDataItem != null && e.DestDataItem.OwnerGridID == this.rgExtArchivo.ClientID)
                    {
                        foreach (GridDataItem g in e.DraggedItems)
                        {
                            adicionarExtension(g);
                        }
                    }
                }
            }
        }

        protected void rgExtensiones_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            GridEditableItem g = (e.Item as GridEditableItem);
            eliminarExtension(g);
        }

        protected void rgExtArchivo_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            GridEditableItem g = (e.Item as GridEditableItem);
            eliminarExtensionXarchivo(g);
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("FileSettings.aspx");
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
                objDtDatos = c.consultarExtensiones();
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
            string codArchivo = Convert.ToString(Request.QueryString["CodArchivo"]);
            DataTable objDtDatos = null;
            try
            {
                objDtDatos = c.consultarExtensionXarchivo(codArchivo);
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
            this.Session["rgExtensiones"] = null;
            this.rgExtensiones.Rebind();
        }

        private void cargarGrilla2()
        {
            this.Session["rgExtArchivo"] = null;
            this.rgExtArchivo.Rebind();
        }

        private void guardarDatos(UserControl uc, GridEditableItem g = null)
        {
            string extension = (uc.FindControl("txtExtension") as RadTextBox).Text.Trim().ToUpper();
            string descripcion = (uc.FindControl("txtDescipcion") as RadTextBox).Text.Trim();
            RipsValidadorDao.ConnectionDB.AutenticationProvider.Consulta c = new RipsValidadorDao.ConnectionDB.AutenticationProvider.Consulta();
            InsertUpdateDelete i = new InsertUpdateDelete(c.consultarUsuarioXnombre(User.Identity.Name));
            if (g == null)
            {
                DataRow[] changeRow = tablaDatos.Select("text = '" + extension + "'");
                if (changeRow.Length <= 0)
                {
                    try
                    {
                        ExtensionArchivo e = new ExtensionArchivo(0, extension.ToUpper(), descripcion);
                        i.IUDextensionArchivo(e, 2);
                        this.RadWindowManager1.RadAlert("Valor agregado correctamente", 400, 200, Utilities.windowTitle(TypeMessage.information_message),
                            null, Utilities.pathImageMessage(TypeMessage.information_message));
                        cargarGrilla();
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
                    this.RadWindowManager1.RadAlert("La extension que inteta agregar, ya existe",400,200,Utilities.windowTitle(TypeMessage.information_message),
                        null,Utilities.pathImageMessage(TypeMessage.information_message));
                }
            }
            else
            {
                Int16 idExtension = Convert.ToInt16(g.GetDataKeyValue("value"));
                try
                {
                    ExtensionArchivo e = new ExtensionArchivo(idExtension, extension.ToUpper(), descripcion);
                    i.IUDextensionArchivo(e, 3);
                        this.RadWindowManager1.RadAlert("Valor actualizado correctamente", 400, 200, Utilities.windowTitle(TypeMessage.information_message),
                            null, Utilities.pathImageMessage(TypeMessage.information_message));
                    cargarGrilla();
                }
                catch(Exception ex)
                {
                    Logger.generarLogError(ex.Message, new System.Diagnostics.StackFrame(true), ex);
                    this.RadWindowManager1.RadAlert(Utilities.errorMessage(),400,200,Utilities.windowTitle(TypeMessage.error_message),
                        null, Utilities.pathImageMessage(TypeMessage.error_message));
                }
            }
        }

        private void eliminarExtension(GridEditableItem g)
        {
            Int16 idExtension = Convert.ToInt16(g.GetDataKeyValue("value"));
            Consulta c = new Consulta();
            RipsValidadorDao.ConnectionDB.AutenticationProvider.Consulta c1 = new RipsValidadorDao.ConnectionDB.AutenticationProvider.Consulta();
            InsertUpdateDelete i = new InsertUpdateDelete(c1.consultarUsuarioXnombre(User.Identity.Name));
            ExtensionArchivo e = c.consultarExtensionesOBJ(idExtension);
            if (!(c.consultarExtensionXarchivo("",idExtension).Rows.Count > 0))
            {
                if (e != null)
                {
                    try
                    {
                        i.IUDextensionArchivo(e, 4);
                        this.RadWindowManager1.RadAlert("Valor eliminado correctamente", 400, 200, Utilities.windowTitle(TypeMessage.information_message),
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
            }
            else
            {
                this.RadWindowManager1.RadAlert("la extensión que intenta eliminar está asociada a uno o más archivo elimine la extensión del archivo antes de eliminarla de la plataforma.", 400, 200, 
                    Utilities.windowTitle(TypeMessage.information_message), null, Utilities.pathImageMessage(TypeMessage.information_message));
            }
        }

        private void cargarDatosArchivo()
        {
            Consulta c = new Consulta();
            ParametrizacionArchivo p = c.consultarArchivoParametrizado(ViewState["CodArchivo"].ToString());
            this.lblTipoArchivo.Text = p.descripcion;
        }

        private void adicionarExtension(GridDataItem g)
        {
            Consulta c = new Consulta();
            Int16 idExtension = Convert.ToInt16(g.GetDataKeyValue("value"));
            string codArchivo = ViewState["CodArchivo"].ToString();
            if (c.consultarExtensionXarchivoOBJ(codArchivo, idExtension) == null)
            {
                ExtensionXarchivo e = new ExtensionXarchivo();
                RipsValidadorDao.ConnectionDB.AutenticationProvider.Consulta c1 = new RipsValidadorDao.ConnectionDB.AutenticationProvider.Consulta();
                InsertUpdateDelete i = new InsertUpdateDelete(c1.consultarUsuarioXnombre(User.Identity.Name));
                try
                {
                    e.extension = c.consultarExtensionesOBJ(idExtension);
                    e.archivo = c.consultarArchivoParametrizado(codArchivo);               
                    i.IUDextensionXarchivo(e, 2);
                    this.RadWindowManager1.RadAlert("Extension agregada correctamente", 400, 200, Utilities.windowTitle(TypeMessage.information_message),
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
            else
            {
                this.RadWindowManager1.RadAlert("La extension ya se encuentra asociada al archivo seleccionado", 400, 200, Utilities.windowTitle(TypeMessage.information_message),
                    null, Utilities.pathImageMessage(TypeMessage.information_message));
            }
        }

        private void eliminarExtensionXarchivo(GridEditableItem g)
        {
            Int16 idExtension = Convert.ToInt16(g.GetDataKeyValue("id_extension"));
            string codArchivo = g.GetDataKeyValue("cod_archivo").ToString();
            RipsValidadorDao.ConnectionDB.AutenticationProvider.Consulta c = new RipsValidadorDao.ConnectionDB.AutenticationProvider.Consulta();
            Consulta c1 = new Consulta();
            InsertUpdateDelete i = new InsertUpdateDelete(c.consultarUsuarioXnombre(User.Identity.Name));
            try
            {
                ExtensionXarchivo e = new ExtensionXarchivo(c1.consultarArchivoParametrizado(codArchivo),c1.consultarExtensionesOBJ(idExtension));
                i.IUDextensionXarchivo(e, 4);
                this.RadWindowManager1.RadAlert("Valor eliminado correctamente", 400, 200, Utilities.windowTitle(TypeMessage.information_message),
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