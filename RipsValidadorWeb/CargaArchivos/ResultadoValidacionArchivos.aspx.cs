using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using RipsValidadorWeb.Clases;
using RipsValidadorWeb.Enumeradores;
using RipsValidadorDao.ConnectionDB.Generales;
using RipsValidadorDao.Model;
using Telerik.Web.UI;

namespace RipsValidadorWeb.CargaArchivos
{
    public partial class ResultadoValidacionArchivos : System.Web.UI.Page
    {

        #region "Propiedades"
        private DataTable _tablaDatos = null;

        public DataTable TablaDatos
        {
            get 
            {
                if (this.Session["rgArchivosProgramacion"] != null)
                {
                    return (DataTable)this.Session["rgArchivosProgramacion"]; 
                }

                _tablaDatos = getDataTable();
                this.Session["rgArchivosProgramacion"] = _tablaDatos;
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
                this.Session["rgArchivosProgramacion"] = null;
                if (this.Request.QueryString["id_programacion"] != null)
                {
                    ViewState["id_programacion"] = this.Request.QueryString["id_programacion"];
                    cargarDatos(Convert.ToInt32(ViewState["id_programacion"]));
                }
            }
        }

        protected void rgArchivosProgramacion_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.rgArchivosProgramacion.MasterTableView.Rebind();
            }
        }

        protected void rgArchivosProgramacion_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            if (TablaDatos != null)
            {
                this.rgArchivosProgramacion.DataSource = TablaDatos;
            }
            else
            {
                this.rgArchivosProgramacion.DataSource = new String[] { };
            }
        }

        protected void rgArchivosProgramacion_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                GridEditableItem g = (GridEditableItem)e.Item;
                Response.Redirect("ResultadoValidacionArchivosDetalle.aspx?id_programacion=" + g.GetDataKeyValue("id_programacion") + "&consecutivo=" + g.GetDataKeyValue("consecutivo"));
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
        }
        #endregion

        #region "Metodos"

        private DataTable getDataTable()
        {
            DataTable objDtDatos = null;
            Consulta c = new Consulta();
            try
            {
                objDtDatos = c.consultarArchivoCargado(Convert.ToInt32(ViewState["id_programacion"]));
            }
            catch (Exception ex)
            {
                Logger.generarLogError(ex.Message, new System.Diagnostics.StackFrame(true), ex);
                this.RadWindowManager1.RadAlert("Ocurrio un error al cargar los datos", 400, 200,
                    Utilities.windowTitle(TypeMessage.error_message), null, Utilities.pathImageMessage(TypeMessage.error_message));
            }
            return objDtDatos;
        }
        private void cargarDatos(int idProgramacion)
        {
            Consulta c = new Consulta();
            try
            {
                RipsValidadorDao.Model.ResultadoValidacion r = c.consultarResultadoValidacionOBJ(idProgramacion);
                if (r != null)
                {
                    this.lbl_desc_numCargue.Text = Convert.ToString(r.programacion.idProgramacion);
                    this.lblDescCantArchProcesados.Text = Convert.ToString(r.cantArchivosProcesados);
                    this.lblDescCantArchError.Text = Convert.ToString(r.cantArchivosError);
                    this.lblDescArchValidos.Text = Convert.ToString(r.cantArchivosValidos);
                    this.lbl_desc_factura.Text = r.programacion.periodoCobro.ToString("yyyy/MM/dd");
                    this.lbl_desc_estado.Text = r.programacion.estado.descripcion;
                }
            }
            catch (InvalidCastException ex)
            {
                Logger.generarLogError(ex.Message, new System.Diagnostics.StackFrame(true), ex);
                this.RadWindowManager1.RadAlert("Ocurrio un error al cargar los datos del resultado de validación de la programación",
                    400, 200, Utilities.windowTitle(TypeMessage.error_message), null, Utilities.pathImageMessage(TypeMessage.error_message));
            }
            catch (Exception ex)
            {
                Logger.generarLogError(ex.Message, new System.Diagnostics.StackFrame(true), ex);
                this.RadWindowManager1.RadAlert("Ocurrio un error al cargar los datos del resultado de validación de la programación",
                    400, 200, Utilities.windowTitle(TypeMessage.error_message), null, Utilities.pathImageMessage(TypeMessage.error_message));
            }
        }

        #endregion

    }
}