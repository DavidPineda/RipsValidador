using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ControlesAsp.DataTableControl;
using ControlesAsp.EstructuraDeDatos;
using System.Net.Mail;
using System.Net.Mime;
using RipsValidadorWeb.Enumeradores;
using System.IO;

namespace RipsValidadorWeb.Clases
{
    public static class Utilities
    {
        /// <summary>
        /// Retorna la ruta de la imagen mostrada por el alert
        /// </summary>
        /// <param name="tipoImagen">Enumerador que indica el tipo de imagen a mostrar</param>
        /// <returns>La ruta de la imagen que debe mostrar el message</returns>
        public static string pathImageMessage(Enumeradores.TypeMessage tipoImagen)
        {
            string imagen = string.Empty;
            switch (tipoImagen)
            {
                case Enumeradores.TypeMessage.error_message:
                    imagen = "/Images/error.png";
                    break;
                case Enumeradores.TypeMessage.warning_message:
                    imagen = "/Images/warning.png";
                    break;
                case Enumeradores.TypeMessage.information_message:
                    imagen = "/Images/info.png";
                    break;
                case Enumeradores.TypeMessage.question_message:
                    imagen = "/Images/question.png";
                    break;
            }
            return imagen;
        }

        /// <summary>
        /// Mensaje que se muestra cuando ocurre un error en la aplicacion
        /// </summary>
        /// <returns>El mensaje que se muestra en el mensaje</returns>
        public static string errorMessage()
        {
            return "Ocurrió un error, Favor Comunicarse con el administrador para más detalles";
        }

        /// <summary>
        /// Retorna el titulo que se mestra en la ventana de acuerdo al tipo de mensaje
        /// </summary>
        /// <param name="tipoError">Tipo de mensaje</param>
        /// <returns>Titulo de la ventana</returns>
        public static string windowTitle(Enumeradores.TypeMessage tipoError)
        {
            string titulo = string.Empty;
            switch (tipoError)
            {
                case Enumeradores.TypeMessage.error_message:
                    titulo = "Error";
                    break;
                case Enumeradores.TypeMessage.warning_message:
                    titulo = "Advertencia";
                    break;
                case Enumeradores.TypeMessage.information_message:
                    titulo = "Información";
                    break;
                case Enumeradores.TypeMessage.question_message:
                    titulo = "Pregunta";
                    break;
            }
            return titulo;
        }

        /// <summary>
        /// Convierte el array un array de roles en una DataTable
        /// </summary>
        /// <param name="roles">Array con los roles del sistema</param>
        /// <returns>Datatable con  los roles del sistema</returns>
        public static DataTable convertArrayRolesToDataTable(string[] roles)
        {
            Nodo<string, System.Type>[] myArray = new Nodo<string, Type>[2];
            myArray[0] = new Nodo<string, Type>("value", System.Type.GetType("System.String"));
            myArray[1] = new Nodo<string, Type>("text", System.Type.GetType("System.String"));
            try
            {
                DataTable myDataTable = DataTableASP.crearTabla(myArray, "Roles");
                foreach (string rol in roles)
                {
                    DataRow newRow = myDataTable.NewRow();
                    newRow["value"] = rol.Split(':')[0].ToString();
                    newRow["text"] = rol.Split(':')[1].ToString();
                    DataTableASP.addItemDataTable(ref myDataTable, newRow, true);
                }
                return myDataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Valida si la licencia del aplicativo se encunetra activa
        /// </summary>
        /// <param name="rutaKey">Ruta del archivo de la licencia</param>
        /// <param name="fecha">Fecha que regresa de expiracion de la licencia</param>
        /// <returns>Indica cuantos dias de validez tiene la licencia</returns>
        public static int validarVersion(string rutaKey, ref string fecha)
        {
            string strPath = System.IO.Path.Combine(rutaKey, "Key.txt");
            string strFileText = string.Empty, texto = string.Empty;
            int intDay = 0, intMonth = 0, intYear = 0, intIndex = 0, versionActiva = 100;
            System.IO.StreamReader sr;
            try
            {
                sr = new System.IO.StreamReader(strPath, System.Text.Encoding.Default);
                texto = sr.ReadLine();
                sr.Close();
                strFileText = EncriptarClaves.clsEncriptarClases.Decrypt(texto);
                if (strFileText.Equals(string.Empty))
                {
                    versionActiva = 0;
                }
                else
                {
                    foreach (string strWork in strFileText.Split('~'))
                    {
                        switch (intIndex)
                        {
                            case 0:
                                intDay = int.Parse(strWork);
                                break;
                            case 1:
                                intMonth = int.Parse(strWork);
                                break;
                            case 2:
                                intYear = int.Parse(strWork);
                                break;
                        }
                        intIndex++;
                    }
                    DateTime fechaVersion = new DateTime(intYear, intMonth, intDay);
                    if (fechaVersion < System.DateTime.Now.Date)
                    {
                        versionActiva = -1;
                    }
                    else if (fechaVersion == System.DateTime.Now.Date)
                    {
                        versionActiva = 0;
                    }
                    else if (fechaVersion == System.DateTime.Now.AddDays(1))
                    {
                        versionActiva = 1;
                    }
                    else if (fechaVersion == System.DateTime.Now.AddDays(2))
                    {
                        versionActiva = 2;
                    }
                    else if (fechaVersion == System.DateTime.Now.AddDays(3))
                    {
                        versionActiva = 3;
                    }
                    else if (fechaVersion == System.DateTime.Now.AddDays(4))
                    {
                        versionActiva = 4;
                    }
                    else if (fechaVersion == System.DateTime.Now.AddDays(5))
                    {
                        versionActiva = 5;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            fecha = strFileText.Replace("~", "/");
            return versionActiva;
        }

        /// <summary>
        /// Genera una contraseña aleatoria para recuperar la contraseña
        /// </summary>
        /// <param name="numeroCaracteres"> Número de caracteres de la nueva contraseña </param>
        /// <returns> La cadena de caracteres aleatoria </returns>
        /// <remarks>
        /// <list> Creado: Agosto 15 de 2013 </list>
        /// </remarks>
        public static string generarContrasena(int numeroCaracteres)
        {
            string cadenaAleatoria = string.Empty;
            try
            {
                string[] letras = new string[62];

                // Se Rellena el array con las letras 
                int n = 0;
                for (int item = 65; item <= 90; item++)
                {
                    letras[n] = Convert.ToString(Convert.ToChar(item));
                    letras[n + 1] = letras[n].ToLower();
                    n += 2;
                }

                // Se Rellena el array con los números
                for (Int16 i = 48; i <= 57; i++)
                {
                    letras[n] = Convert.ToString(Convert.ToChar(i));
                    n += 1;
                }

                // Iniciando el generador de números aleatorios 
                Random rnd = new Random(DateTime.Now.Millisecond);
                for (n = 0; n <= numeroCaracteres; n++)
                {
                    int numero = rnd.Next(0, 60);
                    cadenaAleatoria += letras[numero];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return cadenaAleatoria.Trim();
        }

        /// <summary>
        /// Retorna el formato fecha a validar
        /// </summary>
        /// <param name="codFormato">Código del formato fecha</param>
        /// <returns>Array con el formato mas el separador</returns>
        public static string[] retornarFormatoFecha(int codFormato)
        {
            string[] array = new String[2];
            array[0] = string.Empty;
            array[1] = string.Empty;
            switch (codFormato)
            {
                case 1:
                    array[0] = "AAAAMMDD";
                    break;
                case 2:
                    array[0] = "AAAADDMM";
                    break;
                case 3:
                    array[0] = "MMDDAAAA";
                    break;
                case 4:
                    array[0] = "MMAAAADD";
                    break;
                case 5:
                    array[0] = "DDAAAAMM";
                    break;
                case 6:
                    array[0] = "DDMMAAAA";
                    break;
                case 7:
                    array[0] = "AAAA-MM-DD";
                    array[1] = "-";
                    break;
                case 8:
                    array[0] = "AAAA-DD-MM";
                    array[1] = "-";
                    break;
                case 9:
                    array[0] = "MM-DD-AAAA";
                    array[1] = "-";
                    break;
                case 10:
                    array[0] = "MM-AAAA-DD";
                    array[1] = "-";
                    break;
                case 11:
                    array[0] = "DD-AAAA-MM";
                    array[1] = "-";
                    break;
                case 12:
                    array[0] = "DD-MM-AAAA";
                    array[1] = "-";
                    break;
                case 13:
                    array[0] = "AAAA/MM/DD";
                    array[1] = "/";
                    break;
                case 14:
                    array[0] = "AAAA/DD/MM";
                    array[1] = "/";
                    break;
                case 15:
                    array[0] = "MM/DD/AAAA";
                    array[1] = "/";
                    break;
                case 16:
                    array[0] = "MM/AAAA/DD";
                    array[1] = "/";
                    break;
                case 17:
                    array[0] = "DD/AAAA/MM";
                    array[1] = "/";
                    break;
                case 18:
                    array[0] = "DD/MM/AAAA";
                    array[1] = "/";
                    break;
            }
            return array;
        }

        /// <summary>
        /// Permite Retornar el código de tipo de archivo
        /// </summary>
        /// <param name="tipoArchivo">TIpo de Archivo a buscar</param>
        /// <returns>Tipo de archivo que se encuentra</returns>
        public static string retornarSiglaArchivo(TipoDeArchivo tipoArchivo)
        {
            switch (tipoArchivo)
            {
                case TipoDeArchivo.aControl:
                    return "CT";
                case TipoDeArchivo.aTransacciones:
                    return "AF";
                case TipoDeArchivo.aUsuarios:
                    return "US";
                case TipoDeArchivo.aDescripcion:
                    return "AD";
                case TipoDeArchivo.aConsultas:
                    return "AC";
                case TipoDeArchivo.aProcedimientos:
                    return "AP";
                case TipoDeArchivo.aHospitalizacion:
                    return "AH";
                case TipoDeArchivo.aUrgencias:
                    return "AU";
                case TipoDeArchivo.aNacidos:
                    return "AN";
                case TipoDeArchivo.aMedicamentos:
                    return "AM";
                case TipoDeArchivo.aOtros:
                    return "AT";
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// Retorna el tipo de archivo segun el nombre de archivo
        /// </summary>
        /// <param name="nombreArchivo">Nombre de archivo a validar</param>
        /// <returns>Tipo de archivo encontrado</returns>
        public static TipoDeArchivo retornarTipoArchivo(string nombreArchivo)
        {
            if (nombreArchivo.Contains(retornarSiglaArchivo(TipoDeArchivo.aControl)))
            {
                return TipoDeArchivo.aControl;
            }
            else if (nombreArchivo.Contains(retornarSiglaArchivo(TipoDeArchivo.aTransacciones)))
            {
                return TipoDeArchivo.aTransacciones;
            }
            else if (nombreArchivo.Contains(retornarSiglaArchivo(TipoDeArchivo.aUsuarios)))
            {
                return TipoDeArchivo.aUsuarios;
            }
            else if (nombreArchivo.Contains(retornarSiglaArchivo(TipoDeArchivo.aDescripcion)))
            {
                return TipoDeArchivo.aDescripcion;
            }
            else if (nombreArchivo.Contains(retornarSiglaArchivo(TipoDeArchivo.aConsultas)))
            {
                return TipoDeArchivo.aConsultas;
            }
            else if (nombreArchivo.Contains(retornarSiglaArchivo(TipoDeArchivo.aProcedimientos)))
            {
                return TipoDeArchivo.aProcedimientos;
            }
            else if (nombreArchivo.Contains(retornarSiglaArchivo(TipoDeArchivo.aHospitalizacion)))
            {
                return TipoDeArchivo.aHospitalizacion;
            }
            else if (nombreArchivo.Contains(retornarSiglaArchivo(TipoDeArchivo.aUrgencias)))
            {
                return TipoDeArchivo.aUrgencias;
            }
            else if (nombreArchivo.Contains(retornarSiglaArchivo(TipoDeArchivo.aNacidos)))
            {
                return TipoDeArchivo.aNacidos;
            }
            else if (nombreArchivo.Contains(retornarSiglaArchivo(TipoDeArchivo.aMedicamentos)))
            {
                return TipoDeArchivo.aMedicamentos;
            }
            else if (nombreArchivo.Contains(retornarSiglaArchivo(TipoDeArchivo.aOtros)))
            {
                return TipoDeArchivo.aOtros;
            }
            else
            {
                return 0;
            }
        }

        public static string arrayToLine(System.Collections.ArrayList archivos)
        {
            string linea = string.Empty;
            foreach (string archivo in archivos) linea += System.IO.Path.GetFileNameWithoutExtension(archivo);
            return linea;
        }

        public static void validarCrearFolder(string path)
        {
            if (!System.IO.Directory.Exists(path)) System.IO.Directory.CreateDirectory(path);
        }

        #region "Formato de correo electronico"

        /// <summary>
        /// Permite crear el formato de vista html para el envio de correo
        /// </summary>
        /// <param name="ruta_imagen"> Ruta de la imagen que se muestra </param>
        /// <param name="nombre_eps"> Nombre de la EPS que firma el correo </param>
        /// <param name="mailBody"> Cuerpo del mensaje </param>
        /// <returns> Vista html del correo </returns>
        /// <remarks>
        /// <list> Creado: Agosto 15 de 2013 - Ing. David Pineda </list>
        /// </remarks>
        public static AlternateView formatoCorreoHtml(string ruta_imagen, string nombre_eps, string mailBody)
        {
            AlternateView htmlView = null;
            try
            {
                StringBuilder objString = new StringBuilder();
                objString.Append("<!DOCTYPE html>");
                objString.Append("<html lang='Es'>");
                objString.Append("<head>");
                objString.Append("<meta charset=\"utf-8\" />");
                objString.Append("<style type='text/css'>");
                objString.Append("body");
                objString.Append("{");
                objString.Append("font-size :1em;");
                objString.Append("font-family: Helvetica, Verdana;");
                objString.Append("color: #08088A;");
                objString.Append("}");
                objString.Append("img, video");
                objString.Append("{");
                objString.Append("max-width: 100%;");
                objString.Append("}");
                objString.Append("header, footer");
                objString.Append("{");
                objString.Append("background: #FFF;");
                objString.Append("margin: 0 auto;");
                objString.Append("max-width: 90%;");
                objString.Append("text-align: center;");
                objString.Append("}");
                objString.Append("header h1");
                objString.Append("{");
                objString.Append("display: inline-block;");
                objString.Append("max-width: 100%;");
                objString.Append("vertical-align: middle;");
                objString.Append("}");
                objString.Append("footer");
                objString.Append("{");
                objString.Append("color: #000;");
                objString.Append("font-size: 0.85em;");
                objString.Append("padding: 0.75em 0;");
                objString.Append("}");
                objString.Append("</style>");
                objString.Append("</head>");
                objString.Append("<body>");
                objString.Append("<header>");
                objString.Append("<h1>");
                objString.Append("<img src='cid:imagen'");
                objString.Append("</h1>");
                objString.Append("</header>");
                objString.Append("<section>");
                objString.Append("<article>");
                objString.Append("<p>");
                objString.Append("<h2>");
                objString.Append(mailBody);
                objString.Append("</h2>");
                objString.Append("</p>");
                objString.Append("</article>");
                objString.Append("</section>");
                objString.Append("<footer>");
                objString.Append("<p>");
                objString.Append("<h2>");
                objString.Append("Reciba un cordial saludo");
                objString.Append("<br />");
                objString.Append(nombre_eps);
                objString.Append("</h2>");
                objString.Append("</p>");
                objString.Append("<p>");
                objString.Append("Por favor no responder este correo");
                objString.Append("</p>");
                objString.Append("</footer>");
                objString.Append("</body>");
                objString.Append("</html>");

                htmlView = AlternateView.CreateAlternateViewFromString(objString.ToString(), Encoding.Default, MediaTypeNames.Text.Html);
                LinkedResource img = new LinkedResource(ruta_imagen, MediaTypeNames.Image.Jpeg);
                img.ContentId = "imagen";
                htmlView.LinkedResources.Add(img);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return htmlView;
        }

        /// <summary>
        /// Permite crear el texto plano del envio de mensaje por correo
        /// </summary>
        /// <param name="mailBody"> Cuerpo del mensaje </param>
        /// <returns> Vista en texto plano del mensaje </returns>
        /// <remarks>
        /// <list> Creado: Agosto 15 de 2013 - Ing. David Pineda </list>
        /// </remarks>
        public static AlternateView formatoCorreoTextoPlano(string mailBody)
        {
            AlternateView plainView = null;
            try
            {
                plainView = AlternateView.CreateAlternateViewFromString(mailBody, Encoding.Default, MediaTypeNames.Text.Plain);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return plainView;
        }

        #endregion

        /// <summary>
        /// Permite descargar un archivo al PC del cliente
        /// </summary>
        /// <param name="objResponse">Objeto response de la pagina desde donde se descarga el archivo</param>
        /// <param name="path_download">Ruta fisica del archivo</param>
        /// <param name="nomArchivoDescarga">Nombre con el que se decarga el archivo</param>
        public static void downloadFile(System.Web.HttpResponse objResponse, string pathDownload)
        {
            FileStream stream = null;
            BinaryReader objBinaryReader = null;
            Byte[] objByte = { };
            if (File.Exists(pathDownload))
            {
                try
                {
                    stream = new FileStream(pathDownload, FileMode.Open, FileAccess.Read);
                    objBinaryReader = new BinaryReader(stream);
                    objByte = objBinaryReader.ReadBytes((int)stream.Length);
                    objResponse.ClearHeaders();
                    objResponse.ClearContent();
                    switch (Path.GetExtension(pathDownload).ToUpper())
                    {
                        case ".ZIP":
                            objResponse.ContentType = "application/zip";
                            break;
                        case ".PDF":
                            objResponse.ContentType = "application/pdf";
                            break;
                        default:
                            objResponse.ContentType = "application/txt";
                            break;
                    }
                    objResponse.AddHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(pathDownload));
                    objResponse.BinaryWrite(objByte);
                    //objResponse.WriteFile(pathDownload);
                    objResponse.Flush();
                    objResponse.Close();
                }
                catch (Exception ex) 
                {
                    throw ex;
                }
            }
        }

    }
}