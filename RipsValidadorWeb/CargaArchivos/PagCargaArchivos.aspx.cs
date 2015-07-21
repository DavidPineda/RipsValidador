using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using RipsValidadorDao.ConnectionDB.Generales;
using ControlesAsp.DropDownListControl;
using System.Diagnostics;
using RipsValidadorWeb.Clases;
using RipsValidadorWeb.Enumeradores;
using System.Data;
using System.Collections;
using RipsValidadorDao.Model;

namespace RipsValidadorWeb.CargaArchivos
{
    public partial class PagCargaArchivos : System.Web.UI.Page
    {

        #region "Eventos"
        protected void Page_Load(object sender, EventArgs e)
        {          
            if (!IsPostBack)
            {
                cargarDatos();
            }
        }
        protected void ddlAno_SelectedIndexChanged(object sender, DropDownListEventArgs e)
        {
            cargarMeses();
        }

        protected void btnProgramar_Click(object sender, EventArgs e)
        {
            Validate("P_Programar");
            if (IsValid)
            {
                if (this.MyRadAsyncUpload.UploadedFiles.Count > 0)
                {
                    validarArchivosCargados();
                }
                else 
                {
                    this.RadWindowManager1.RadAlert("Debe cargar archivos para poder realizar la programación", 400, 200,
                        Utilities.windowTitle(TypeMessage.information_message), null, Utilities.pathImageMessage(TypeMessage.information_message));
                }
            }            
        }

        #endregion

        #region "Metodos"
        private void cargarDatos()
        {
            cargarRegional();
            cargarContratos();
            cargarAnos();
            cargarMeses();
            cargarExtensiones();
        }

        private void cargarExtensiones()
        {
            Consulta c = new Consulta();
            DataTable objDtDatos = c.consultarExtensiones();
            string[] extension = new string[objDtDatos.Rows.Count];
            int i = 0;
            foreach (DataRow row in objDtDatos.Rows)
            {
                extension[i++] = "." + row["text"].ToString();
            }
            this.MyRadAsyncUpload.AllowedFileExtensions = extension;
        }

        private void cargarRegional()
        {
            Consulta c = new Consulta();
            try
            {
                DropDownListASP.llenarDropDownList(c.consultarRegionales(), "value", "text", ref this.ddlRegional);
                DropDownListASP.AddItemToDropDownList(ref this.ddlRegional, "SELECCIONE>>", "-1", true);
            }
            catch (ArgumentNullException ex)
            {
                Logger.generarLogError(ex.Message, new StackFrame(true), ex);
                this.RadWindowManager1.RadAlert(Utilities.errorMessage(), 400, 200, Utilities.windowTitle(TypeMessage.error_message), null,
                    Utilities.pathImageMessage(TypeMessage.error_message));
            }
            catch (ArgumentException ex)
            {
                Logger.generarLogError(ex.Message, new StackFrame(true), ex);
                this.RadWindowManager1.RadAlert(Utilities.errorMessage(), 400, 200, Utilities.windowTitle(TypeMessage.error_message), null,
                    Utilities.pathImageMessage(TypeMessage.error_message));
            }
            catch (Exception ex)
            {
                Logger.generarLogError(ex.Message, new StackFrame(true), ex);
                this.RadWindowManager1.RadAlert(Utilities.errorMessage(), 400, 200, Utilities.windowTitle(TypeMessage.error_message), null,
                    Utilities.pathImageMessage(TypeMessage.error_message));
            }
        }

        private void cargarContratos()
        {
            Consulta c = new Consulta();
            try
            {
                DropDownListASP.llenarDropDownList(c.consultarTipoContrato(), "value", "text", ref this.ddlContrato);
                DropDownListASP.AddItemToDropDownList(ref this.ddlContrato, "SELECCIONE>>", "-1", true);
            }
            catch (ArgumentNullException ex)
            {
                Logger.generarLogError(ex.Message, new StackFrame(true), ex);
                this.RadWindowManager1.RadAlert(Utilities.errorMessage(), 400, 200, Utilities.windowTitle(TypeMessage.error_message), null,
                    Utilities.pathImageMessage(TypeMessage.error_message));
            }
            catch (ArgumentException ex)
            {
                Logger.generarLogError(ex.Message, new StackFrame(true), ex);
                this.RadWindowManager1.RadAlert(Utilities.errorMessage(), 400, 200, Utilities.windowTitle(TypeMessage.error_message), null,
                    Utilities.pathImageMessage(TypeMessage.error_message));
            }
            catch (Exception ex)
            {
                Logger.generarLogError(ex.Message, new StackFrame(true), ex);
                this.RadWindowManager1.RadAlert(Utilities.errorMessage(), 400, 200, Utilities.windowTitle(TypeMessage.error_message), null,
                    Utilities.pathImageMessage(TypeMessage.error_message));
            }
        }

        private void cargarMeses()
        {
            try
            {
                DropDownListASP.cargarMesesToDropDownList(ref this.ddlMes, new DateTime(int.Parse((this.ddlAno.SelectedValue)), DateTime.Now.Month, DateTime.Now.Day));
            }
            catch (Exception ex)
            {
                Logger.generarLogError(ex.Message, new StackFrame(true), ex);
            }
            
        }

        private void cargarAnos()
        {
            try
            {
                DropDownListASP.cargarAnosToDropDownList(ref this.ddlAno, new DateTime(DateTime.Now.Year - 2, 1, 1), new DateTime(DateTime.Now.Year, 1, 1));
            }
            catch(Exception ex)
            {
                Logger.generarLogError(ex.Message, new StackFrame(true), ex);
            }
        }

        private void validarArchivosCargados()
        {
            string msgRetorno;
            UploadedFile aControl = retornarArchivo(this.MyRadAsyncUpload.UploadedFiles, Utilities.retornarSiglaArchivo(TipoDeArchivo.aControl), out msgRetorno);
            if (aControl != null)
            {
                try
                {
                    bool val1 = validarArchivosCargueObligatorio(validarArchivoControl(aControl), "el archivo de control");
                    bool val2 = validarArchivosCargueObligatorio(retornarArchivosCargados(), "los archivos cargados");
                    if (val1 && val2)
                    {
                        if (validarArchivosCoincidentes(validarArchivoControl(aControl), retornarArchivosCargados()))
                        {
                            if (validarDependencias())
                            {
                                if(validarDetalle()){
                                    guardarProgramacion();
                                }
                            }                            
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.RadWindowManager1.RadAlert(Utilities.errorMessage(), 400, 200, Utilities.windowTitle(TypeMessage.error_message),
                        null, Utilities.pathImageMessage(TypeMessage.error_message));
                    Logger.generarLogError(ex.Message, new StackFrame(true), ex);
                }
            }
            else
            {
                if (!msgRetorno.Equals(string.Empty))
                {
                    this.RadWindowManager1.RadAlert(msgRetorno, 400, 200, Utilities.windowTitle(TypeMessage.information_message),
                        null, Utilities.pathImageMessage(TypeMessage.error_message));
                }
                else
                {
                    this.RadWindowManager1.RadAlert("Debe incluir un archivo de control en el cargue.", 400, 200, Utilities.windowTitle(TypeMessage.information_message),
                        null, Utilities.pathImageMessage(TypeMessage.error_message));
                }
            }
        }

        private void guardarProgramacion()
        {
            int ano = Convert.ToInt32(this.ddlAno.SelectedValue);
            int mes = Convert.ToInt32(this.ddlMes.SelectedValue);
            int dia = DateTime.DaysInMonth(ano, mes);
            DateTime periodoCobro = new DateTime(ano, mes, dia);
            Consulta c = new Consulta();
            RipsValidadorDao.ConnectionDB.AutenticationProvider.Consulta c1 = new RipsValidadorDao.ConnectionDB.AutenticationProvider.Consulta();     
            try
            {
                Usuario u = c1.consultarUsuarioXnombre(User.Identity.Name);
                ProgramacionArchivo p = new ProgramacionArchivo(0, DateTime.Now, periodoCobro, "N");
                p.regional = c.consultarRegionalesOBJ(this.ddlRegional.SelectedValue);
                p.contrato = c.consultarTipoContratoOBJ(this.ddlContrato.SelectedValue);
                p.estado = c.consultarEstadoProgramacionOBJ(1);
                p.usuario = u;
                InsertUpdateDelete i = new InsertUpdateDelete(u);
                int idProgramacion = i.IUDprogramacionArchivo(p, 2);
                guardarProgramacionDetalle(idProgramacion);
                this.RadWindowManager1.RadAlert("Programación Guardada de manera exitosa", 400, 200, Utilities.windowTitle(TypeMessage.information_message),
                    null, Utilities.pathImageMessage(TypeMessage.information_message));
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void guardarProgramacionDetalle(int idProgramacion)
        {
            ParametrizacionArchivo p = null;
            ArchivoCargado a = new ArchivoCargado();
            Consulta c = new Consulta();
            RipsValidadorDao.ConnectionDB.AutenticationProvider.Consulta c1 = new RipsValidadorDao.ConnectionDB.AutenticationProvider.Consulta();
            InsertUpdateDelete i = new InsertUpdateDelete(c1.consultarUsuarioXnombre(User.Identity.Name));
            int cont = 1;
            foreach (UploadedFile file in MyRadAsyncUpload.UploadedFiles)
            {
                try
                {
                    string codArchivo = Utilities.retornarSiglaArchivo(Utilities.retornarTipoArchivo(file.FileName));
                    p = c.consultarArchivoParametrizado(codArchivo);
                    a = new ArchivoCargado(cont++, file.FileName, System.IO.Path.Combine(p.rutaCargueArchivo, User.Identity.Name, file.FileName), "N");
                    a.programacion = c.consultarProgramacionArchivoOBJ(idProgramacion);
                    a.archivo = c.consultarArchivoParametrizado(codArchivo);
                    a.estadoArchivo = c.consultarEstadoArchivoOBJ(1);
                    Utilities.validarCrearFolder(System.IO.Path.Combine(p.rutaCargueArchivo, User.Identity.Name));
                    file.SaveAs(a.rutaArchivo, true);
                    i.IUDcargarArchivo(a, 2);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private bool validarArchivosCoincidentes(ArrayList archivosControl, ArrayList archivosCargados)
        {
            string[] arr1 = (string[])archivosCargados.ToArray(typeof(string));
            string[] arr2 = (string[])archivosControl.ToArray(typeof(string));
            IEnumerable<string> coincidente1 = arr1.Except(arr2, new Comparer());
            IEnumerable<string> coincidente2 = arr2.Except(arr1, new Comparer());
            string mensaje = "Inconsistencia entre archivos cargados y los descritos en el archivo de control: <br/><br/>";
            mensaje += "Archivos incluidos en archivos de control y no cargados: <br/>";
            foreach (string archivo in coincidente2) mensaje += "Archivo -> " + archivo + "<br/>";
            mensaje += "<br/><br/>";
            mensaje += "Archivos cargados y no incluidos en archivo de control: <br/>";
            foreach (string archivo in coincidente1) mensaje += "Archivo -> " + archivo + "<br/>";
            if (coincidente1.Count() > 0 || coincidente2.Count() > 0)
            {
                this.RadWindowManager1.RadAlert(mensaje, 600, 400, Utilities.windowTitle(TypeMessage.information_message),
                    null, Utilities.pathImageMessage(TypeMessage.error_message));
                return false;
            }
            return true;
        }

        private bool validarDetalle()
        {
            ParametrizacionArchivo p = null;
            Consulta c = new Consulta();
            string mensaje = string.Empty;
            ArrayList lista = new ArrayList();
            string codArchivo = string.Empty;
            foreach (string fileName in retornarArchivosCargados())
            {                
                p = c.consultarArchivoParametrizado(Utilities.retornarSiglaArchivo(Utilities.retornarTipoArchivo(fileName)));
                mensaje = validarErrorDetalle(p, fileName);
                if (!mensaje.Equals(string.Empty)) lista.Add(mensaje);
            }
            mensaje = string.Empty;
            if (lista.Count > 0)
            {
                foreach (string error in lista) mensaje += error;
                this.RadWindowManager1.RadAlert(mensaje, 800, 600, Utilities.windowTitle(TypeMessage.information_message),
                    null, Utilities.pathImageMessage(TypeMessage.error_message));
                return false;
            }
            return true;
        }

        private string validarErrorDetalle(ParametrizacionArchivo p, string nomArchivo)
        {
            string retorno = string.Empty;
            if (nomArchivo.Length > p.lngMaximaNombre)
            {
                retorno = "Archivo: " + nomArchivo + " no cumple con la longitud maxima permitida <br/>";               
            }
            else if (nomArchivo.Length < p.lngMinimaNombre)
            {
                retorno = "Archivo: " + nomArchivo + " no cumple con la longitud minima permitida <br/>";
            }
            else
            {
                Consulta c = new Consulta();
                DataTable objDtDatos = c.consultarExtensionXarchivo(p.codArchivo, 0);
                bool extensionValida = false;
                foreach (DataRow row in objDtDatos.Rows)
                {
                    if (("." + row["extension"]).ToString().ToUpper().Equals(System.IO.Path.GetExtension(nomArchivo).ToUpper()))
                    {
                        extensionValida = true;
                        break;
                    }
                }
                if (!extensionValida)
                {
                    retorno = "Archivo: " + nomArchivo + " no cumple con ninguna de la extension permitida para el archivo <br/>";
                }
            }
            return retorno;
        }

        private bool validarArchivosCargueObligatorio(ArrayList archivosACargar, string titulo)
        {
            Consulta c = new Consulta();
            DataTable objDtDatos = c.consultarArchivosParametrizados();
            objDtDatos.PrimaryKey = new DataColumn[] { objDtDatos.Columns["cod_archivo"] }; 
            DataView d = new DataView(objDtDatos);
            d.RowFilter = "cargue_obligatorio = 1";
            DataTable dt = d.ToTable();    
            int index = 0;
            foreach (DataRow row in dt.Rows)
            {
                foreach (string archivo in archivosACargar)
                {
                    if (archivo.Contains((string)row["cod_archivo"]))
                    {
                        try
                        {
                            d.Delete(index);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        index--;
                        break;
                    }
                }
                index++;
            }
            objDtDatos.AcceptChanges();
            d.RowFilter = "cargue_obligatorio = 1 and cod_archivo <> '" + Utilities.retornarSiglaArchivo(TipoDeArchivo.aControl) + "'";
            if (d.ToTable().Rows.Count > 0)
            {
                mostrarArchivosFaltantesEnControl(d, titulo);
                return false;
            }
            return true;
        }

        private void mostrarArchivosFaltantesEnControl(DataView objDtDatos, string titulo)
        {
            string mensaje = "Faltan los siguientes archivos obligatorios en " + titulo + ": <br/><br/>";
            int numError = 1;
            foreach (DataRow row in objDtDatos.ToTable().Rows)
            {
                mensaje += numError + ") " + row["descripcion"].ToString() + "<br/>";
                numError++;
            }
            this.RadWindowManager1.RadAlert(mensaje, 600, 400, Utilities.windowTitle(TypeMessage.information_message),
                null, Utilities.pathImageMessage(TypeMessage.error_message));
        }

        private UploadedFile retornarArchivo(UploadedFileCollection archivos, string patronBusqueda, out string msgRetorno)
        {
            int cont = 0;
            UploadedFile retorno = null;
            msgRetorno = string.Empty;
            foreach (UploadedFile u in archivos)
            {
                if (u.FileName.Contains(patronBusqueda))
                {
                    cont++;
                    retorno = u;
                }
            }
            if (cont > 1)
            {
                retorno = null;
                msgRetorno = "No se pueden cargar dos archivos del tipo " + patronBusqueda + " en el mismo cargue";
            }
            return retorno;
        }

        private System.Collections.ArrayList validarArchivoControl(UploadedFile file)
        {            
            ArrayList archivos = new ArrayList();
            System.IO.StreamReader lector = null;
            try
            {
                lector = new System.IO.StreamReader(file.InputStream);
                while(!lector.EndOfStream){
                    archivos.Add(lector.ReadLine().Split(',')[2].ToString() + ".txt");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally{
                lector.Close();
            }
            return archivos;
        }

        private ArrayList retornarArchivosCargados()
        {
            ArrayList archivos = new ArrayList();
            try
            {
                foreach (UploadedFile file in this.MyRadAsyncUpload.UploadedFiles)
                {
                    if (!file.FileName.ToUpper().Contains(Utilities.retornarSiglaArchivo(TipoDeArchivo.aControl)))
                        archivos.Add(file.FileName);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return archivos;
        }

        private bool validarDependencias()
        {
            Consulta c = new Consulta();
            DataTable objDtDatos;
            ArrayList lista = new ArrayList();
            string archivosLine = Utilities.arrayToLine(retornarArchivosCargados());
            foreach (string archivo in retornarArchivosCargados())
            {
                try
                {
                    string cod_archivo = Utilities.retornarSiglaArchivo(Utilities.retornarTipoArchivo(archivo));
                    objDtDatos = c.consultarArchivosDependientes(cod_archivo);
                    foreach (DataRow row in objDtDatos.Rows)
                    {
                        if (!archivosLine.Contains(row["cod_archivo_dep"].ToString()))
                        {
                            lista.Add((c.consultarArchivoParametrizado(cod_archivo) as ParametrizacionArchivo).descripcion
                                + " requiere de " + (c.consultarArchivoParametrizado(row["cod_archivo_dep"].ToString()) as ParametrizacionArchivo).descripcion + " no encontrado <br/>");
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            if (lista.Count > 0)
            {
                string mensaje = string.Empty;
                foreach (string error in lista) mensaje += error;
                this.RadWindowManager1.RadAlert(mensaje, 600, 400, Utilities.windowTitle(TypeMessage.information_message    ),
                    null, Utilities.pathImageMessage(TypeMessage.error_message));
                return false;
            }
            return true;
        }
        #endregion

        #region "Clases"        
        class Comparer : IEqualityComparer<string>
        {
            bool IEqualityComparer<string>.Equals(string x, string y)
            {                
                return x.Equals(y);
            }

            int IEqualityComparer<string>.GetHashCode(string obj)
            {
                return obj.GetHashCode();
            }
        }
        #endregion

        protected void MyRadAsyncUpload_FileUploaded(object sender, FileUploadedEventArgs e)
        {
            System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex("^(AF|AP|AC|AM|AT|US|AN|AH|AU|CT)[0-9]{1,6}.TXT$");
            if (r.IsMatch(e.File.FileName.ToUpper()))
            {
                string s = string.Empty;
            }
            else
            {
                string s = string.Empty;
            }
        }
    }
}