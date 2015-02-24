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
using ControlesAsp.DropDownListControl;
using Telerik.Web.UI;

namespace RipsValidadorWeb.Parametrizacion
{
    public partial class dependsFile : System.Web.UI.Page
    {
        #region "Pripiedades"
        private DataTable _tablaDatos;
        private DataTable _tablaDatos2;
        public DataTable tablaDatos
        {
            get
            {
                if (this.Session["rgArchivosParametrizados"] != null)
                {
                    return (DataTable)this.Session["rgArchivosParametrizados"];
                }
                _tablaDatos = getDataTable();
                this.Session["rgArchivosParametrizados"] = _tablaDatos;
                return _tablaDatos;
            }
            set { _tablaDatos = value; }
        }

        public DataTable tablaDatos2
        {
            get
            {
                if (this.Session["rgArchivosDependientes"] != null)
                {
                    return (DataTable)this.Session["rgArchivosDependientes"];
                }
                _tablaDatos2 = getDataTable2();
                this.Session["rgArchivosDependientes"] = _tablaDatos2;
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
                this.Session["rgArchivosParametrizados"] = null;
                this.Session["rgArchivosDependientes"] = null;
                cargarComboTipoArchivo();
            }
        }

        protected void ddlTipoArchivo_SelectedIndexChanged(object sender, Telerik.Web.UI.DropDownListEventArgs e)
        {
            cargarTabla();
            cargarTabla2();
        }

        protected void rgArchivosDependientes_DeleteCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            eliminarArchivo((e.Item as GridDataItem));
        }

        protected void rgArchivosDependientes_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            this.rgArchivosDependientes.DataSource = tablaDatos2;
        }

        protected void rgArchivosDependientes_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack) this.rgArchivosDependientes.MasterTableView.Rebind();
        }

        protected void rgArchivosParametrizados_RowDrop(object sender, Telerik.Web.UI.GridDragDropEventArgs e)
        {
            if (string.IsNullOrEmpty(e.HtmlElement))
            {
                if (e.DraggedItems[0].OwnerGridID == rgArchivosParametrizados.ClientID)
                {
                    if ((e.DestDataItem == null && tablaDatos2.Rows.Count == 0) ||
                        e.DestDataItem != null && e.DestDataItem.OwnerGridID == this.rgArchivosDependientes.ClientID)
                    {
                        foreach (GridDataItem g in e.DraggedItems)
                        {
                            adicionarArchivo(g);
                        }
                    }
                }
            }
        }

        protected void rgArchivosParametrizados_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            this.rgArchivosParametrizados.DataSource = tablaDatos;
        }

        protected void rgArchivosParametrizados_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack) this.rgArchivosParametrizados.MasterTableView.Rebind();
        }

        protected void btnRetornar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ParametrizacionIndex.aspx");
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
                objDtDatos = c.consultarArchivosParametrizados();
                DataView d = new DataView(objDtDatos);
                d.RowFilter = "cod_archivo <> '" + ddlTipoArchivo.SelectedValue + "' and cod_archivo <> '" + Utilities.retornarSiglaArchivo(TipoDeArchivo.aControl) + "' ";
                return d.ToTable();
            }
            catch (Exception ex)
            {
                Logger.generarLogError(ex.Message, new System.Diagnostics.StackFrame(true), ex);
                this.RadWindowManager1.RadAlert(Utilities.errorMessage(), 400, 200, Utilities.windowTitle(TypeMessage.error_message),
                    null, Utilities.pathImageMessage(TypeMessage.error_message));
                return null;
            }
        }

        private DataTable getDataTable2()
        {
            Consulta c = new Consulta();
            DataTable objDtDatos = null;
            try
            {
                objDtDatos = c.consultarArchivosDependientes(this.ddlTipoArchivo.SelectedValue);
            }
            catch (Exception ex)
            {
                Logger.generarLogError(ex.Message, new System.Diagnostics.StackFrame(true), ex);
                this.RadWindowManager1.RadAlert(Utilities.errorMessage(), 400, 200, Utilities.windowTitle(TypeMessage.error_message),
                    null, Utilities.pathImageMessage(TypeMessage.error_message));
            }
            return objDtDatos;
        }

        private void cargarComboTipoArchivo()
        {
            Consulta c = new Consulta();
            try
            {
                DropDownListASP.llenarDropDownList(c.consultarArchivosParametrizados(), "cod_archivo", "descripcion", ref this.ddlTipoArchivo);
            }
            catch (Exception ex)
            {
                Logger.generarLogError(ex.Message, new System.Diagnostics.StackFrame(true), ex);
                this.RadWindowManager1.RadAlert(Utilities.errorMessage(), 400, 200, Utilities.windowTitle(TypeMessage.error_message),
                    null, Utilities.pathImageMessage(TypeMessage.error_message));
            }
        }

        private void cargarTabla()
        {
            this.Session["rgArchivosParametrizados"] = null;
            this.rgArchivosParametrizados.Rebind();
        }

        private void cargarTabla2(){
            this.Session["rgArchivosDependientes"] = null;
            this.rgArchivosDependientes.Rebind();
        }

        private void adicionarArchivo(GridDataItem g)
        {
            Consulta c = new Consulta();
            ArchivoDependiente a = c.consultarArchivosDependientesOBJ(this.ddlTipoArchivo.SelectedValue, g.GetDataKeyValue("cod_archivo").ToString());
            if (a == null)
            {
                try
                {
                    RipsValidadorDao.ConnectionDB.AutenticationProvider.Consulta c1 = new RipsValidadorDao.ConnectionDB.AutenticationProvider.Consulta();
                    InsertUpdateDelete i = new InsertUpdateDelete(c1.consultarUsuarioXnombre(User.Identity.Name));
                    a = new ArchivoDependiente();
                    a.archivo = c.consultarArchivoParametrizado(this.ddlTipoArchivo.SelectedValue);
                    a.archivoDep = c.consultarArchivoParametrizado(g.GetDataKeyValue("cod_archivo").ToString());
                    i.IUDarchivoDependiente(a, 2);
                    cargarTabla2();
                    this.RadWindowManager1.RadAlert("Archivo asociado correctamente", 400, 200, Utilities.windowTitle(TypeMessage.information_message),
                        null, Utilities.pathImageMessage(TypeMessage.information_message));
                }
                catch (Exception ex)
                {
                    Logger.generarLogError(ex.Message, new System.Diagnostics.StackFrame(true), ex);
                    this.RadWindowManager1.RadAlert(Utilities.errorMessage(), 400, 200, Utilities.windowTitle(TypeMessage.information_message),
                        null, Utilities.pathImageMessage(TypeMessage.error_message));
                }
            }
            else
            {
                this.RadWindowManager1.RadAlert("El archivo seleccionado ya se encuentra asociado", 400, 200, Utilities.windowTitle(TypeMessage.information_message),
                    null, Utilities.pathImageMessage(TypeMessage.information_message));
            }
        }

        private void eliminarArchivo(GridDataItem g)
        {
            Consulta c = new Consulta();
            try
            {
                ArchivoDependiente a = c.consultarArchivosDependientesOBJ(g.GetDataKeyValue("cod_archivo").ToString(), g.GetDataKeyValue("cod_archivo_dep").ToString());
                if (a != null)
                {
                    RipsValidadorDao.ConnectionDB.AutenticationProvider.Consulta c1 = new RipsValidadorDao.ConnectionDB.AutenticationProvider.Consulta();
                    InsertUpdateDelete i = new InsertUpdateDelete(c1.consultarUsuarioXnombre(User.Identity.Name));
                    i.IUDarchivoDependiente(a, 4);
                    cargarTabla2();
                    this.RadWindowManager1.RadAlert("Archivo eliminado correctamente", 400, 200, Utilities.windowTitle(TypeMessage.information_message),
                        null, Utilities.pathImageMessage(TypeMessage.information_message));
                }
            }
            catch(Exception ex)
            {
                Logger.generarLogError(ex.Message, new System.Diagnostics.StackFrame(true), ex);
                this.RadWindowManager1.RadAlert(Utilities.errorMessage(), 400, 200, Utilities.windowTitle(TypeMessage.information_message),
                    null, Utilities.pathImageMessage(TypeMessage.error_message));
            }            
        }
        #endregion
    }
}