using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RipsValidadorWeb.Enumeradores;
using RipsValidadorWeb.Clases;
using RipsValidadorDao.ConnectionDB.Generales;
using ControlesAsp.DropDownListControl;
using System.Data;
using Telerik.Web.UI;

namespace RipsValidadorWeb.CargaArchivos
{
    public partial class ResultadoValidacion : System.Web.UI.Page
    {

        #region "Propiedades"
        private DataTable _tablaDatos;

        public DataTable TablaDatos
        {
            get 
            {
                if (this.Session["rgResultadosCarga"] != null)
                {
                    return (DataTable)this.Session["rgResultadosCarga"];
                }

                _tablaDatos = getData();
                this.Session["rgResultadosCarga"] = _tablaDatos;
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
                this.Session["rgResultadosCarga"] = null;
                cargarFechas();
                cargarEstados();
                DropDownListASP.AddItemToDropDownList(ref this.ddl_ips, "Seleccione>>", "-1", true);
            }
        }

        protected void ibtn_bus_nom_Click(object sender, ImageClickEventArgs e)
        {
            this.buscarXnombreIPS();
        }

        protected void ibtn_bus_nit_Click(object sender, ImageClickEventArgs e)
        {
            this.buscarXCodigoIPS();
        }

        protected void rgResultadosCarga_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Select":
                    Response.Redirect("ResultadoValidacionArchivos.aspx?id_programacion=" + (e.Item as GridEditableItem).GetDataKeyValue("id_programacion"));
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                    break;
                case "Delete":
                    eliminarProgramacion(Convert.ToInt32((e.Item as GridEditableItem).GetDataKeyValue("id_programacion")));
                    break;
            }
        }

        protected void rgResultadosCarga_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.rgResultadosCarga.MasterTableView.Rebind();
            }
        }

        protected void rgResultadosCarga_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            if (TablaDatos != null)
            {
                this.rgResultadosCarga.DataSource = TablaDatos;
            }
            else
            {
                this.rgResultadosCarga.DataSource = new String[] { };
            }
        }

        protected void btn_consultar_Click(object sender, EventArgs e)
        {
            cargarGrilla();
        }

        #endregion

        #region "Metodos"
        private void buscarXCodigoIPS()
        {
            if(!this.txtNitIps.Text.Equals(string.Empty )){
                Consulta c = new Consulta();
                try
                {
                    DropDownListASP.llenarDropDownList(c.consultarRazonSocialIpsXNit(this.txtNitIps.Text), "cod_ips", "razon_social", ref this.ddl_ips);
                    DropDownListASP.AddItemToDropDownList(ref this.ddl_ips, "Seleccione>>", "-1", true);
                    DropDownListASP.selectIndexByValue(ref this.ddl_ips, "-1");
                }
                catch(ArgumentNullException ex)
                {
                    this.RadWindowManager1.RadAlert("Ocurrio un error al cargar las IPS", 400, 200, 
                        Utilities.windowTitle(TypeMessage.error_message),null, Utilities.pathImageMessage(TypeMessage.error_message));
                    Logger.generarLogError(ex.Message, new System.Diagnostics.StackFrame(true), ex);
                }
                catch(ArgumentException ex)
                {
                    this.RadWindowManager1.RadAlert("Ocurrio un error al cargar las IPS", 400, 200, 
                        Utilities.windowTitle(TypeMessage.error_message),null, Utilities.pathImageMessage(TypeMessage.error_message));
                    Logger.generarLogError(ex.Message, new System.Diagnostics.StackFrame(true), ex);
                }
                catch(Exception ex)
                {
                    this.RadWindowManager1.RadAlert("Ocurrio un error al cargar las IPS", 400, 200, 
                        Utilities.windowTitle(TypeMessage.error_message),null, Utilities.pathImageMessage(TypeMessage.error_message));
                    Logger.generarLogError(ex.Message, new System.Diagnostics.StackFrame(true), ex);
                }
                this.txtNitIps.Text = string.Empty;
            }
            else
            {
                this.RadWindowManager1.RadAlert("Favor ingrese un nit de IPS en el campo de búsqueda por Nit", 400, 200, 
                    Utilities.windowTitle(TypeMessage.information_message),null, Utilities.pathImageMessage(TypeMessage.information_message));
            }
        }

        private void buscarXnombreIPS()
        {
            if (!this.txtRazonSocial.Text.Equals(string.Empty))
            {
                Consulta c = new Consulta();
                try
                {
                    DropDownListASP.llenarDropDownList(c.consultarRazonSocialIpsXNombre(this.txtRazonSocial.Text), "cod_ips", "razon_social", ref this.ddl_ips);
                    DropDownListASP.AddItemToDropDownList(ref this.ddl_ips, "Seleccione>>", "-1", true);
                    DropDownListASP.selectIndexByValue(ref this.ddl_ips, "-1");
                }
                catch(ArgumentNullException ex)
                {
                    this.RadWindowManager1.RadAlert("Ocurrio un error al cargar las IPS", 400, 200, 
                        Utilities.windowTitle(TypeMessage.error_message),null, Utilities.pathImageMessage(TypeMessage.error_message));
                    Logger.generarLogError(ex.Message, new System.Diagnostics.StackFrame(true), ex);
                }
                catch(ArgumentException ex)
                {
                    this.RadWindowManager1.RadAlert("Ocurrio un error al cargar las IPS", 400, 200, 
                        Utilities.windowTitle(TypeMessage.error_message),null, Utilities.pathImageMessage(TypeMessage.error_message));
                    Logger.generarLogError(ex.Message, new System.Diagnostics.StackFrame(true), ex);
                }
                catch(Exception ex)
                {
                    this.RadWindowManager1.RadAlert("Ocurrio un error al cargar las IPS", 400, 200, 
                        Utilities.windowTitle(TypeMessage.error_message),null, Utilities.pathImageMessage(TypeMessage.error_message));
                    Logger.generarLogError(ex.Message, new System.Diagnostics.StackFrame(true), ex);
                }
                this.txtRazonSocial.Text = string.Empty;
            }
            else
            {
                this.RadWindowManager1.RadAlert("Favor ingrese un nombre de IPS en el campo de búsqueda por razón social", 400, 200, 
                    Utilities.windowTitle(TypeMessage.information_message),null, Utilities.pathImageMessage(TypeMessage.information_message));
            }
        }

        private void cargarFechas()
        {
            this.txt_fecha_ini.SelectedDate = System.DateTime.Today;
            this.txt_fecha_fin.SelectedDate = System.DateTime.Today;
        }

        private DataTable getData()
        {
            Consulta c = new Consulta();
            DataTable objDtDatos = null;
            try
            {
                string codIps = !this.ddl_ips.SelectedValue.Equals("-1") ? this.ddl_ips.SelectedValue : string.Empty;
                Int16 estado = !this.ddl_estados.SelectedValue.Equals("-1") ? Int16.Parse(this.ddl_estados.SelectedValue.ToString()) : (Int16)0;
                string fecIni = String.Format("{0:yyyyMMdd}", txt_fecha_ini.SelectedDate);
                string fecFin = String.Format("{0:yyyyMMdd}", txt_fecha_fin.SelectedDate);
                objDtDatos = c.consultarProgramacionArchivo(codIps, estado, fecIni, fecFin);
            }
            catch (Exception ex)
            {
                Logger.generarLogError(ex.Message, new System.Diagnostics.StackFrame(true), ex);
                this.RadWindowManager1.RadAlert("Ocurrio un error al consultar las programaciones", 400, 200,
                    Utilities.windowTitle(TypeMessage.error_message), null, Utilities.pathImageMessage(TypeMessage.error_message));                
            }
            return objDtDatos;
        }

        private void cargarEstados()
        {
            Consulta c = new Consulta();
            try
            {
                DropDownListASP.llenarDropDownList(c.consultarEstadoProgramacion(), "value", "text", ref this.ddl_estados);
                DropDownListASP.AddItemToDropDownList(ref this.ddl_estados, "Seleccione>>", "0", true);
            }
            catch (ArgumentNullException ex)
            {
                Logger.generarLogError(ex.Message, new System.Diagnostics.StackFrame(true), ex);
                this.RadWindowManager1.RadAlert("Ocurrio un error al cargar los estados de programación", 400, 200,
                    Utilities.windowTitle(TypeMessage.error_message), null, Utilities.pathImageMessage(TypeMessage.error_message));
            }
            catch (ArgumentException ex)
            {
                Logger.generarLogError(ex.Message, new System.Diagnostics.StackFrame(true), ex);
                this.RadWindowManager1.RadAlert("Ocurrio un error al cargar los estados de programación", 400, 200,
                    Utilities.windowTitle(TypeMessage.error_message), null, Utilities.pathImageMessage(TypeMessage.error_message));                
            }
            catch (Exception ex)
            {
                Logger.generarLogError(ex.Message, new System.Diagnostics.StackFrame(true), ex);
                this.RadWindowManager1.RadAlert("Ocurrio un error al cargar los estados de programación", 400, 200,
                    Utilities.windowTitle(TypeMessage.error_message), null, Utilities.pathImageMessage(TypeMessage.error_message));                
            }
        }

        private void cargarGrilla()
        {
            this.Session["rgResultadosCarga"] = null;
            this.rgResultadosCarga.Rebind();
        }

        private void eliminarProgramacion(int idProgramacion)
        {
            RipsValidadorDao.ConnectionDB.AutenticationProvider.Consulta c1 = new RipsValidadorDao.ConnectionDB.AutenticationProvider.Consulta();
            Consulta c = new Consulta();
            RipsValidadorDao.Model.Usuario u = null;            
            RipsValidadorDao.Model.ProgramacionArchivo p = null;
            try
            {
                u = c1.consultarUsuarioXnombre(User.Identity.Name);
                InsertUpdateDelete i = new InsertUpdateDelete(u);
                p = c.consultarProgramacionArchivoOBJ(idProgramacion);
                if (validarEstado(p.estado.codEstadoCargue))
                {
                    i.borrarDatosProgramacion(idProgramacion, u.idUsuario);
                    cargarGrilla();
                    this.RadWindowManager1.RadAlert("Programación eliminada correctamente", 400, 200, Utilities.windowTitle(TypeMessage.information_message),
                        null, Utilities.pathImageMessage(TypeMessage.information_message));
                }
                else
                {
                    this.RadWindowManager1.RadAlert("El estado actual del archivo no permite que los datos sean borrados", 400, 200,
                        Utilities.windowTitle(TypeMessage.information_message), null, Utilities.pathImageMessage(TypeMessage.information_message));
                }
            }
            catch (Exception ex)
            {
                Logger.generarLogError(ex.Message, new System.Diagnostics.StackFrame(true), ex);
                this.RadWindowManager1.RadAlert("Ocurrio un error al eliminar la programacion", 400, 200,
                    Utilities.windowTitle(TypeMessage.error_message), null, Utilities.pathImageMessage(TypeMessage.error_message));   
            }
        }

        public bool validarEstado(Int16 estado)
        {
            switch (estado)
            {
                case 1:
                    return true;
                case 2:
                    return false;
                case 3:
                    return false;
                case 4:
                    return true;
                case 5:
                    return true;
                case 6:
                    return true;
                default:
                    return false;
            }
        }

        #endregion

    }
}