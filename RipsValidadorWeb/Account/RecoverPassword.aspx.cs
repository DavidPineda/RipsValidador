using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RipsValidadorWeb.Clases;
using RipsValidadorWeb.Enumeradores;
using RipsValidadorDao.Model;
using MyAutenticationProvider.User;
using System.Configuration;
using MailManaged.Send;
using System.Diagnostics;

namespace RipsValidadorWeb.Account
{
	public partial class recoverPassword : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				this.MyRadWindowManager.RadAlert("Por favor digite su nombre de usuario en el recuadro indicado y de clic en el botón recuperar, se enviara su nueva contraseña al correo que tenga registrado en el aplicativo.",
						   400, 200, Utilities.windowTitle(TypeMessage.information_message), null, Utilities.pathImageMessage(TypeMessage.information_message));
			}
		}

		protected void btnRecuperar_Click(object sender, EventArgs e)
		{
			consultarDatosUsuario();
		}

		private void consultarDatosUsuario()
		{
			string nombreUsuario = this.UserName.Text.Trim();
			MyMembershipProvider m = new MyMembershipProvider();
			Usuario u = m.consultarUsuarioXnombre(nombreUsuario);
			if (u != null)
			{
				if (!u.email.Equals(string.Empty))
				{
					try
					{
						string newPassword = Utilities.generarContrasena(10);
						u.contrasena = EncriptarClaves.clsEncriptarClases.Encrypt(newPassword);
						m.actualizarUsuario(u, u.idUsuario);
						enviarCorreo(newPassword, u.email, u.nomUsuario);
						MyRadWindowManager.RadAlert("La nueva contraseña a sido enviada al email registrado en el sistema", 400, 200,
							Utilities.windowTitle(TypeMessage.information_message), null, Utilities.pathImageMessage(TypeMessage.information_message));
					}
					catch (Exception ex)
					{
						Logger.generarLogError(ex.Message, new StackFrame(true), ex);
						MyRadWindowManager.RadAlert(Utilities.errorMessage(), 400, 200,
							Utilities.windowTitle(TypeMessage.error_message), null, Utilities.pathImageMessage(TypeMessage.error_message));
					}
				}
				else 
				{
					MyRadWindowManager.RadAlert("El Usuario " + nombreUsuario + " no cuenta con un email registrado en el sistema, no es posible enviar la contraseña nueva.", 400, 200,
						Utilities.windowTitle(TypeMessage.information_message), null, Utilities.pathImageMessage(TypeMessage.information_message));
				}
			}
			else
			{
				MyRadWindowManager.RadAlert("El nombre de usuario ingresado, no se encuentra en el sistema",400,200,
					Utilities.windowTitle(TypeMessage.information_message), null, Utilities.pathImageMessage(TypeMessage.information_message));
			}
		}

		private void enviarCorreo(string password, string destinatario, string usuario)
		{
			try 
			{
				string contrasena_corero = EncriptarClaves.clsEncriptarClases.Decrypt(ConfigurationManager.AppSettings["CONTRASENA_CORREO"]);
				string tipo_cuenta = ConfigurationManager.AppSettings["TIPO_CUENTA"];
				string cuenta_envio = ConfigurationManager.AppSettings["CUENTA_CORREO"];
				string ruta_imagen_correo = Server.MapPath(ConfigurationManager.AppSettings["IMGEN_CORREO"]);
				string nombre_eps_correo = ConfigurationManager.AppSettings["NOM_EMPRESA"];
				int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["PUERTO_MAIL"]);
				string asunto = "Información de reemplazo de inicio de sesión para " + usuario + " en INTEGR@ RIPS‏";
				string cuerpo_mensaje = "Recibimos en INTEGR@ RIPS una solicitud para restablecer la contraseña de su cuenta, " + "\n" + " Su nueva contraseña es: " + password + " Recuerde que puede cambiar esta contraseña despues de ingresar al aplicativo de nuevo.";		
				MailEnviar email = new MailEnviar(cuenta_envio, destinatario, asunto, cuerpo_mensaje, contrasena_corero, puerto);
				email.enviar(tipo_cuenta, Utilities.formatoCorreoHtml(ruta_imagen_correo, nombre_eps_correo, cuerpo_mensaje), Utilities.formatoCorreoTextoPlano(cuerpo_mensaje));
			} catch (Exception ex) {
				throw ex;
			}
		}
	}
}