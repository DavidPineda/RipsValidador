using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Net.Mail;
using System.IO;
using MailManaged.Zip;

namespace MailManaged.Send
{

    using Microsoft.VisualBasic;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics;
    using System.Net.Mail;

    public class MailEnviar
    {

        #region "Variables constantes"
        /// <summary>
        /// Variable de tipo <see cref="String"></see> 
        /// </summary>
        /// <remarks>
        /// <list>Creaciòn: Jun 14/2013 - Ing. Anderson Paez </list>
        /// </remarks>

        public string _mailAddress;
        /// <summary>
        /// Variable de tipo <see cref="String"></see> 
        /// </summary>
        /// <remarks>
        /// <list>Creaciòn: Jun 14/2013 - Ing. Anderson Paez </list>
        /// </remarks>

        public string _mailSend;
        /// <summary>
        /// Variable de tipo <see cref="String"></see> 
        /// </summary>
        /// <remarks>
        /// <list>Creaciòn: Jun 14/2013 - Ing. Anderson Paez </list>
        /// </remarks>

        string _asunto;
        /// <summary>
        /// Variable de tipo <see cref="String"></see> 
        /// </summary>
        /// <remarks>
        /// <list>Creaciòn: Jun 14/2013 - Ing. Anderson Paez </list>
        /// </remarks>

        string _mailbody;
        /// <summary>
        /// Variable de tipo <see cref="String"></see> 
        /// </summary>
        /// <remarks>
        /// <list>Creaciòn: Jun 14/2013 - Ing. Anderson Paez </list>
        /// </remarks>

        string _nombreArchivo;
        /// <summary>
        /// Variable de tipo 
        /// </summary>
        /// <remarks>
        /// <list>Creaciòn: Jun 14/2013 - Ing. Anderson Paez </list>
        /// </remarks>

        DateTime _Fecha;
        /// <summary>
        /// Variable de tipo <see cref="MailMessage "></see> 
        /// </summary>
        /// <remarks>
        /// <list>Creaciòn: Jun 14/2013 - Ing. Anderson Paez </list>
        /// </remarks>

        MailMessage _mensaje = new MailMessage();
        /// <summary>
        /// Variable de tipo <see cref="HostName"></see> 
        /// </summary>
        /// <remarks>
        /// <list>Creaciòn: Jun 14/2013 - Ing. Anderson Paez </list>
        /// </remarks>

        string _hostName;
        /// <summary>
        /// Variable de tipo <see cref="SmtpClient"></see> 
        /// </summary>
        /// <remarks>
        /// <list>Creaciòn: Jun 14/2013 - Ing. Anderson Paez </list>
        /// </remarks>

        SmtpClient _SMTP;
        /// <summary>
        /// Contraseña de la cuenta desde la cual enviar correo
        /// </summary>
        /// <remarks>
        /// <list>Creación: Agosto 13 de 2013 - Ing. David Pineda </list>
        /// </remarks>

        string _passwod;
        /// <summary>
        /// Puerto de salida para el EMAIL (25 o 587)
        /// </summary>
        /// <remarks>
        /// <list>Creación: Septiembre 04 de 2013 - Ing David Pineda</list>
        /// </remarks>

        int _puerto;
        #endregion

        #region "property"

        public string mailBody
        {
            get { return _mailbody; }
            set { _mailbody = value; }
        }

        public string asunto
        {
            get { return _asunto; }
            set { _asunto = value; }
        }

        public string mailSend
        {
            get { return _mailSend; }
            set { _mailSend = value; }
        }

        public string mailAddress
        {
            get { return _mailAddress; }
            set { _mailAddress = value; }
        }

        public DateTime fecha
        {
            get { return _Fecha; }
            set { _Fecha = value; }
        }

        public MailMessage mensaje
        {
            get { return _mensaje; }
            set { _mensaje = value; }
        }

        public string hostName
        {
            get { return _hostName; }
            set { _hostName = value; }
        }

        public SmtpClient SMTP
        {
            get { return _SMTP; }
            set { _SMTP = value; }
        }

        public string password
        {
            get { return _passwod; }
            set { _passwod = value; }
        }

        public int puerto
        {
            get { return _puerto; }
            set { _puerto = value; }
        }

        public string nombreArchivo
        {
            get { return _nombreArchivo; }
            set { _nombreArchivo = value; }
        }

        #endregion

        #region "constructorres"

        /// <summary>
        /// Constructor vacio de la clase Mail
        /// </summary>
        /// <remarks>
        /// <list>Creado 13 Agosto de 2013 - Ing. David Pineda </list>
        /// </remarks>
        public MailEnviar()
        {
            this.mailAddress = "";
            this.mailSend = "";
            this.asunto = "";
            this.mailBody = "";
            this._puerto = 0;
            this.mensaje = new MailMessage();
            this.fecha = System.DateTime.Now;
            this.nombreArchivo = "";
        }

        /// <summary>
        /// Constructor de la clase Mail 
        /// </summary>
        /// <param name="mailAddress"> Dirección de correo electrónico desde donde se envia el mail </param>
        /// <param name="mailSend"> Destinatario del mail </param>
        /// <param name="asunto"> Asunto del correo electronico </param>
        /// <param name="mailBody"> Cuerpo del correo </param>
        /// <param name="password"> Contrsaseña de la cuenta de correo desde donde se envia el mail </param>
        /// <param name="puerto"> Puerto de salida del correo (25 0 587) </param>
        /// <remarks>
        /// <list> Creado Agosto 13 de 2013 - Ing. David Pineda </list>
        /// </remarks>
        public MailEnviar(string mailAddress, string mailSend, string asunto, string mailBody, string password, int puerto)
        {
            this.mailAddress = mailAddress;
            this.mailSend = mailSend;
            this.asunto = asunto;
            this.mailBody = mailBody;
            this.password = password;
            this.mensaje = new MailMessage();
            this.puerto = puerto;

            //Correo
            mensaje.From = new MailAddress(this.mailAddress);
            //Correo a quien se envia
            mensaje.To.Add(this.mailSend);
            //Asunto
            mensaje.Subject = this.asunto;
            //Cuerpo del mensaje
            mensaje.Body = this.mailBody;

        }

        /// <summary>
        /// Constructor de la clase Mail 
        /// </summary>
        /// <param name="mailAddress"> Dirección de correo electrónico desde donde se envia el mail </param>
        /// <param name="mailSend"> Destinatarios del mail </param>
        /// <param name="asunto"> Asunto del correo electronico </param>
        /// <param name="mailBody"> Cuerpo del correo </param>
        /// <param name="password"> Contrsaseña de la cuenta de correo desde donde se envia el mail </param>
        /// <remarks></remarks>
        public MailEnviar(string mailAddress, string[] mailSend, string asunto, string mailBody, string password, int puerto)
        {
            this.mailAddress = mailAddress;
            this.asunto = asunto;
            this.mailBody = mailBody;
            this.mensaje = new MailMessage();
            this.password = password;
            this.puerto = puerto;

            if ((mailSend != null))
            {
                if (mailSend.Length > 0)
                {
                    for (int i = 0; i <= mailSend.Length - 1; i++)
                    {
                        this.mailSend += mailSend[i].Trim() + ",";
                    }
                }
            }

            //Se quita la coma final
            this.mailSend = this.mailSend.Substring(0, this.mailSend.Length - 1);

            //Correo
            mensaje.From = new MailAddress(this.mailAddress);
            //Correo a quien se envia
            mensaje.To.Add(this.mailSend);
            //Asunto
            mensaje.Subject = this.asunto;
            //Cuerpo del mensaje
            mensaje.Body = this.mailBody;

        }

        #endregion

        #region "Procedimientos o Funciones"

        /// <summary>
        /// Envia un correo electronico con archivo adjunto
        /// </summary>
        /// <param name="cuenta"> Tipo de cuenta desde la cual se envia el correo </param>
        /// <param name="html"> Vista en html del correo </param>
        /// <param name="texto_plano"> Vista en texto plano del correo </param>
        /// <param name="rutaArchivo"> Ruta donde se encuentra el archivo a adjuntar </param>
        /// <param name="rutaCarpeta"> Ruta donde se encuentra la carpeta comprimida a adjuntar </param>
        /// <param name="rutasArchivos"> Rutas de varios archivos si se quieren adjuntar </param>
        /// <param name="rutasCarpetas"> Rutas de varias carpetas si se quieren adjuntar </param>
        /// <returns> Array con errores o vacio si logro enviar el correo de manera correcta </returns>
        /// <remarks>
        /// <list> Creación Agosto 13 de 2013 - Ing. David Pineda </list>
        /// </remarks>
        public void enviar(string cuenta, AlternateView html, AlternateView texto_plano, string rutaArchivo = "", string rutaCarpeta = "", string[] rutasArchivos = null, string[] rutasCarpetas = null)
        {

            ArrayList array = new ArrayList();

            if (!string.IsNullOrEmpty(rutaCarpeta))
            {
                //Comprimir y enviar la carpeta
                try
                {
                    ManagedZip.generarZip(rutaCarpeta, false);
                    mensaje.Attachments.Add(new Attachment(rutaCarpeta + ".zip"));
                }
                catch (Exception ex)
                {
                    throw ex;
                }          
            }
            if (!string.IsNullOrEmpty(rutaArchivo))
            {
                //Enviar el archivo
                try
                {
                    //Archivo que se Adjuntara
                    ManagedZip.generarZip(rutaArchivo, true);
                    mensaje.Attachments.Add(new Attachment(rutaArchivo.Replace(Path.GetFileName(rutaArchivo), Path.GetFileNameWithoutExtension(rutaArchivo) + ".zip")));
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            if ((rutasArchivos != null))
            {
                //Adjuntar los archivos
                for (int i = 0; i <= rutasArchivos.Length - 1; i++)
                {
                    try
                    {
                        ManagedZip.generarZip(rutasArchivos[i], true);
                        mensaje.Attachments.Add(new Attachment(rutaArchivo.Replace(Path.GetFileName(rutasArchivos[i]), Path.GetFileNameWithoutExtension(rutasArchivos[i]) + ".zip")));
                        //Archivo que se Adjuntara
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }

            if ((rutasCarpetas != null))
            {
                //Comprimir y adjuntar las carpetas
                for (int i = 0; i <= rutasCarpetas.Length - 1; i++)
                {
                    try
                    {
                        ManagedZip.generarZip(rutasCarpetas[i], false);
                        mensaje.Attachments.Add(new Attachment(rutasCarpetas[i] + ".zip"));
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }


            try
            {
                SmtpClient SMTP = new SmtpClient(cuenta);
                //Cuenta smtp desde donde se envia el correo

                mensaje.IsBodyHtml = true;
                mensaje.AlternateViews.Add(html);

                SMTP.Port = puerto;
                SMTP.EnableSsl = true;
                SMTP.Credentials = new System.Net.NetworkCredential(this.mailAddress, this.password);
                //ServicePointManager.ServerCertificateValidationCallback = Function(s As Object, certificate As X509Certificate, chain As X509Chain, sslPolicyErrors As SslPolicyErrors) True

                SMTP.Send(mensaje);
                //eliminarZips(rutaArchivo, rutaCarpeta, rutasArchivos, rutasCarpetas)

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                mensaje.Dispose();
            }

        }

        #endregion

    }
}
