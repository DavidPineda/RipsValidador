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
using Telerik.Web.UI;
using ControlesAsp.Panel;

namespace RipsValidadorWeb.Parametrizacion
{
    public partial class FileSettings : System.Web.UI.Page
    {

        #region "Propiedades"
        private DataTable _tablaDatos;
        public DataTable tablaDatos
        {
            get {
                if (this.Session["rgArchivos"] != null)
                {
                    return (DataTable)this.Session["rgArchivos"];
                }
                _tablaDatos = getDataTable();
                this.Session["rgArchivos"] = _tablaDatos;
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
                this.Session["rgArchivos"] = null;
            }
        }

        protected void rgArchivos_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            string codArchivo = string.Empty;
            switch((string)e.CommandName){
                case "Update":
                    GridEditableItem editItem = (GridEditableItem)(e.Item);
                    Consulta c = new Consulta();
                    codArchivo = Convert.ToString(editItem.OwnerTableView.DataKeyValues[editItem.ItemIndex]["COD_ARCHIVO"]);
                    cargarPanel(c.consultarArchivoParametrizado(codArchivo));
                    break;
                case "Delete":
                    codArchivo = (e.Item as GridDataItem).OwnerTableView.DataKeyValues[e.Item.ItemIndex]["COD_ARCHIVO"].ToString();
                    elimarRegistro(codArchivo);
                    break;
                case "Select":
                    codArchivo = (e.Item as GridDataItem).OwnerTableView.DataKeyValues[e.Item.ItemIndex]["COD_ARCHIVO"].ToString();
                    Response.Redirect("ExtensionForFile.aspx?CodArchivo=" + codArchivo);
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                    break;
            }
            
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            cargarPanel();
        }

        protected void rgArchivos_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            this.rgArchivos.DataSource = tablaDatos;
        }

        protected void rgArchivos_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.rgArchivos.MasterTableView.Rebind();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validarCodArchivo())
            {
                guardarDatos();
            }
            else
            {
                this.RadWindowManager1.RadAlert("Ya existe un archivo con el código ingresado", 400, 200, Utilities.windowTitle(TypeMessage.information_message), 
                    null, Utilities.pathImageMessage(TypeMessage.information_message));
            }            
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                PanelASP.limpiarPanel(ref this.myPanel);
                this.myPanel.Visible = false;
            }
            catch (Exception ex)
            {
                Logger.generarLogError(ex.Message, new System.Diagnostics.StackFrame(true), ex);
                this.RadWindowManager1.RadAlert(Utilities.errorMessage(), 400, 200, Utilities.windowTitle(TypeMessage.error_message), 
                    null, Utilities.pathImageMessage(TypeMessage.error_message));
            }
        }
        #endregion

        #region "Metodos"
        private DataTable getDataTable()
        {
            DataTable objDatos = null;
            Consulta c = new Consulta();
            try
            {
                objDatos = c.consultarArchivosParametrizados();
            }
            catch (Exception ex)
            {
                Logger.generarLogError(ex.Message, new System.Diagnostics.StackFrame(true), ex);
                this.RadWindowManager1.RadAlert(Utilities.errorMessage(), 400, 200, Utilities.windowTitle(TypeMessage.error_message),
                    null, Utilities.pathImageMessage(TypeMessage.error_message));                
            }
            return objDatos;            
        }
        private void cargarPanel(ParametrizacionArchivo p = null)
        {
            if (p == null)
            {
                limpiarCampos();
            }
            else
            {
                cargarCampos(p);
            }
            this.myPanel.Visible = true;            
        }
        private void limpiarCampos()
        {
            this.txtCodArchivo.Text = string.Empty;
            this.txtDescripcion.Text = string.Empty;
            this.txtLngMaximaNom.Text = string.Empty;
            this.txtLngMininaNom.Text = string.Empty;
            this.txtNumColumnas.Text = string.Empty;
            this.txtRutaArchivo.Text = string.Empty;
            this.txtSeparador.Text = string.Empty;
            this.txtTamCargue.Text = string.Empty;
            this.chObligatorio.Checked = true;
            this.lblCodArchivo.Enabled = true;
        }
        private void cargarCampos(ParametrizacionArchivo p)
        {
            try
            {
                this.txtCodArchivo.Text = p.codArchivo;
                this.txtDescripcion.Text = p.descripcion;
                this.txtLngMaximaNom.Text = Convert.ToString(p.lngMaximaNombre);
                this.txtLngMininaNom.Text = Convert.ToString(p.lngMinimaNombre);
                this.txtNumColumnas.Text = Convert.ToString(p.cantColumnas);
                this.txtRutaArchivo.Text = p.rutaCargueArchivo;
                this.txtSeparador.Text = p.separador;
                this.txtTamCargue.Text = Convert.ToString(p.tamMaximoCargue);
                this.chObligatorio.Checked = p.isCargueObligatorio;
                this.txtCodArchivo.Enabled = false;
            }
            catch (InvalidCastException ex)
            {
                Logger.generarLogError(ex.Message, new System.Diagnostics.StackFrame(true), ex);
                this.RadWindowManager1.RadAlert(Utilities.errorMessage(), 400, 200, Utilities.windowTitle(TypeMessage.error_message), 
                    null, Utilities.pathImageMessage(TypeMessage.error_message));
            }
        }
        private void guardarDatos()
        {
            ParametrizacionArchivo p = new ParametrizacionArchivo();
            RipsValidadorDao.ConnectionDB.AutenticationProvider.Consulta c = new RipsValidadorDao.ConnectionDB.AutenticationProvider.Consulta();
            InsertUpdateDelete i = new InsertUpdateDelete(c.consultarUsuarioXnombre(User.Identity.Name));
            try
            {
                p.codArchivo = this.txtCodArchivo.Text;
                p.descripcion = this.txtDescripcion.Text;
                p.lngMaximaNombre = int.Parse(this.txtLngMaximaNom.Text);
                p.lngMinimaNombre = int.Parse(this.txtLngMininaNom.Text);
                p.cantColumnas = int.Parse(this.txtNumColumnas.Text);
                p.rutaCargueArchivo = this.txtRutaArchivo.Text;
                p.separador = this.txtSeparador.Text;
                p.tamMaximoCargue = int.Parse(this.txtTamCargue.Text);
                p.isCargueObligatorio = this.chObligatorio.Checked;
                string mensaje = string.Empty;
                Int16 codOperacion = 0;
                if (this.txtCodArchivo.Enabled)
                {
                    codOperacion = 2; // Creacion
                    mensaje = "Archivo Guardado Correctamente";
                }
                else {
                    codOperacion = 3; // Actualizacion
                    mensaje = "Archivo Actualizado Correctamente";
                }
                i.IUDarchivoParametrizado(p, codOperacion);
                limpiarCampos();
                this.Session["rgArchivos"] = null;
                this.rgArchivos.Rebind();
                this.RadWindowManager1.RadAlert(mensaje, 400, 200, Utilities.windowTitle(TypeMessage.information_message),
                    null, Utilities.pathImageMessage(TypeMessage.information_message));
                this.myPanel.Visible = false;
            }
            catch (InvalidCastException ex)
            {
                Logger.generarLogError(ex.Message, new System.Diagnostics.StackFrame(true), ex);
                this.RadWindowManager1.RadAlert(Utilities.errorMessage(), 400, 200, Utilities.windowTitle(TypeMessage.error_message),
                    null, Utilities.pathImageMessage(TypeMessage.error_message));
            }
            catch (Exception ex)
            {
                Logger.generarLogError(ex.Message, new System.Diagnostics.StackFrame(true), ex);
                this.RadWindowManager1.RadAlert(Utilities.errorMessage(), 400, 200, Utilities.windowTitle(TypeMessage.error_message), 
                    null, Utilities.pathImageMessage(TypeMessage.error_message));
            }
        }
        private bool validarCodArchivo()
        {
            Consulta c = new Consulta();
            try
            {
                ParametrizacionArchivo p = c.consultarArchivoParametrizado(this.txtCodArchivo.Text);
                if (p.codArchivo.Equals(this.txtCodArchivo.Text) && this.txtCodArchivo.Enabled)
                {
                    return false;
                }
            }catch(Exception ex){
                Logger.generarLogError(ex.Message, new System.Diagnostics.StackFrame(true), ex);
                this.RadWindowManager1.RadAlert(Utilities.errorMessage(), 400, 200, Utilities.windowTitle(TypeMessage.error_message), 
                    null, Utilities.pathImageMessage(TypeMessage.error_message));
                return false;
            }
            return true;
        }
        private void elimarRegistro(string codArchivo){
            Consulta c = new Consulta();
            if (c.consultarEstructuraArchivo(codArchivo).Rows.Count > 0)
            {
                // Se valida contra la tabla RIPS_ESTRUCTURA_ARCHIVO
                this.RadWindowManager1.RadAlert("No es posible eliminar el archivo, existen estructuras de archivos que dependen del archivo que intenta eliminar, "
                    + "elimine dichas estructuras para eliminar el archivo", 400,200,Utilities.windowTitle(TypeMessage.information_message),null,Utilities.pathImageMessage(TypeMessage.information_message));
                return;
            }
            if (c.consultarExtensionXarchivo(codArchivo, 0).Rows.Count > 0)
            {
                // Se valida contra la tabla RIPS_EXTENSION_X_ARCHIVO
                this.RadWindowManager1.RadAlert("No es posible eliminar el archivo, existen extensiones asociadas al archivo que desea eliminar", 400, 200, Utilities.windowTitle(TypeMessage.information_message), 
                    null, Utilities.pathImageMessage(TypeMessage.information_message));
                return;
            }
            ParametrizacionArchivo p = c.consultarArchivoParametrizado(codArchivo);
            try
            {
                RipsValidadorDao.ConnectionDB.AutenticationProvider.Consulta c1 = new RipsValidadorDao.ConnectionDB.AutenticationProvider.Consulta();
                InsertUpdateDelete i = new InsertUpdateDelete(c1.consultarUsuarioXnombre(User.Identity.Name));
                i.IUDarchivoParametrizado(p, 4);
                this.RadWindowManager1.RadAlert("Archivo eliminado con exito", 400, 200, Utilities.windowTitle(TypeMessage.information_message),
                    null, Utilities.pathImageMessage(TypeMessage.information_message));
                cargarGrilla();
            }
            catch (Exception ex) 
            {
                Logger.generarLogError(ex.Message, new System.Diagnostics.StackFrame(true), ex);
                this.RadWindowManager1.RadAlert(Utilities.errorMessage(), 400, 200, Utilities.windowTitle(TypeMessage.error_message),
                    null, Utilities.pathImageMessage(TypeMessage.error_message));
            }
            /*
                select * from RIPS_CRUCE_AFILIADO_X_ARCHIVO;
                select * from RIPS_ARCHIVO_CARGADO;;
             */
        }
        private void cargarGrilla()
        {
            this.Session["rgArchivos"] = null;
            this.rgArchivos.Rebind();
        }
        #endregion

    }
}