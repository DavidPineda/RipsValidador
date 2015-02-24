using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using RipsValidadorDao.ConnectionDB.Generales;
using RipsValidadorDao.Model;
using RipsValidadorWeb.Clases;
using RipsValidadorWeb.Enumeradores;

namespace RipsValidadorWeb.CargaArchivos
{
    public partial class ResultadoValidacionArchivosDetalle : System.Web.UI.Page
    {

        #region "Propiedades"
        private DataTable _tablaDatos;

        public DataTable tablaDatos
        {
            get 
            {
                if (this.Session["rgErroresEnArchivo"] != null)
                {
                    return (DataTable)this.Session["rgErroresEnArchivo"];
                }

                _tablaDatos = getDataTable();
                this.Session["rgErroresEnArchivo"] = _tablaDatos;
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
                this.Session["rgErroresEnArchivo"] = null;
                if (Request.QueryString["id_programacion"] != null)
                {
                    ViewState["idProgramacion"] = Convert.ToInt32(Request.QueryString["id_programacion"]);
                }
                if (Request.QueryString["consecutivo"] != null)
                {
                    ViewState["consecutivo"] = Convert.ToInt32(Request.QueryString["consecutivo"]);
                }
                cargarDatos();
            }
        }

        protected void rgErroresEnArchivo_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.rgErroresEnArchivo.MasterTableView.Rebind();
            }
        }

        protected void rgErroresEnArchivo_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable objDtDatos = null;
            objDtDatos = tablaDatos;
            if (objDtDatos != null)
            {
                this.rgErroresEnArchivo.DataSource = objDtDatos;
            }
            else
            {
                this.rgErroresEnArchivo.DataSource = new string[] { };
            }
        }

        protected void btnAtras_Click(object sender, EventArgs e)
        {
            retornar();
        }

        protected void btnDescargar_Click(object sender, EventArgs e)
        {
            descargarErrores();
        }

        #endregion

        #region "Metodos"
        private void cargarDatos()
        {
            Consulta c = new Consulta();
            try
            {
                ResultadoValidacionDetalle r = c.consultarResultadoValidacionDetalleOBJ(Convert.ToInt32(ViewState["idProgramacion"]), Convert.ToInt32(ViewState["consecutivo"]));
                if (r != null)
                {
                    this.lbl_desc_numCargue.Text = Convert.ToString(r.archivo.programacion.idProgramacion);
                    this.lbl_desc_nomArchivo.Text = r.archivo.nombreArchivo;
                    this.lbl_desc_estado.Text = r.archivo.estadoArchivo.descripcion;
                    this.lblDescCantRegTotales.Text = Convert.ToString(r.cantRegTotales);
                    this.lblDescRegValidos.Text = Convert.ToString(r.cantRegValidos);
                    this.lblDescRegError.Text = Convert.ToString(r.cantRegError);
                }
                else
                {
                    this.RadWindowManager1.RadAlert("No se pudo recuperar la información del archivo", 400, 200, Utilities.windowTitle(TypeMessage.information_message),
                        null, Utilities.pathImageMessage(TypeMessage.error_message));
                }
            }
            catch (InvalidCastException ex)
            {
                Logger.generarLogError(ex.Message, new System.Diagnostics.StackFrame(true), ex);
                this.RadWindowManager1.RadAlert("Ocurrio un error al consultar detalles del arror", 400, 200, Utilities.windowTitle(TypeMessage.information_message),
                    null, Utilities.pathImageMessage(TypeMessage.error_message));
            }
            catch (Exception ex)
            {
                Logger.generarLogError(ex.Message, new System.Diagnostics.StackFrame(true), ex);
                this.RadWindowManager1.RadAlert("Ocurrio un error al consultar detalles del arror", 400, 200, Utilities.windowTitle(TypeMessage.information_message),
                    null, Utilities.pathImageMessage(TypeMessage.error_message));
            }
        }

        private DataTable getDataTable()
        {
            DataTable objDtDatos = null;
            Consulta c = new Consulta();
            try
            {
                objDtDatos = c.consultarErroresProcesoValidacion(Convert.ToInt32(ViewState["idProgramacion"]), Convert.ToInt32(Request.QueryString["consecutivo"]));
                if (objDtDatos.Rows.Count > 0) this.btnDescargar.Visible = true;
            }
            catch (Exception ex)
            {
                Logger.generarLogError(ex.Message, new System.Diagnostics.StackFrame(true), ex);
                this.RadWindowManager1.RadAlert("Ocurrio un error al consultar los errores del archivo", 400, 200, Utilities.windowTitle(TypeMessage.error_message),
                    null, Utilities.pathImageMessage(TypeMessage.error_message));
            }
            return objDtDatos;
        }

        private void retornar()
        {
            Response.Redirect("ResultadoValidacionArchivos.aspx?id_programacion=" + this.lbl_desc_numCargue.Text);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }

        private void cargarGrilla()
        {
            this.Session["rgErroresEnArchivo"] = null;
            this.rgErroresEnArchivo.Rebind();
        }

        private void descargarErrores()
        {
            Consulta c = new Consulta();
            try
            {
                RipsValidadorDao.Model.ResultadoValidacionDetalle r = c.consultarResultadoValidacionDetalleOBJ(Convert.ToInt32(ViewState["idProgramacion"]), Convert.ToInt32(Request.QueryString["consecutivo"]));
                if (r != null)
                {
                    Utilities.downloadFile(this.Response, r.rutaArchivoError);
                }
            }
            catch (Exception ex)
            {
                Logger.generarLogError(ex.Message, new System.Diagnostics.StackFrame(true), ex);
                this.RadWindowManager1.RadAlert("Ocurrio un error al descargar los errores del archivo", 400, 200, Utilities.windowTitle(TypeMessage.error_message),
                    null, Utilities.pathImageMessage(TypeMessage.error_message));
            }            
        }

        #endregion

    }
}