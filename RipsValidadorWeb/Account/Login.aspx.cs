using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyAutenticationProvider.User;
using RipsValidadorDao.Model;
using EncriptarClaves;
using RipsValidadorWeb.Clases;
using RipsValidadorWeb.Enumeradores;
using System.Web.Security;
using System.Drawing;

namespace RipsValidadorWeb.Account
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack){
              validarVersion();
            }                
        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            validarUsuario();
        }

        private void validarUsuario()
        {
            MyMembershipProvider m = new MyMembershipProvider();
            string nombreUSuario = this.UserName.Text.Trim();
            string password = clsEncriptarClases.Encrypt(this.Password.Text.Trim());
            Usuario u = null;
            try
            {
                u = m.consultarUsarioAutenticacion(nombreUSuario, password);
            }catch(Exception ex)
            {
                this.MyRadWindowManager.RadAlert("Ocurrio un error, al intentar autenticar el usuario", 400, 200,
                    Utilities.windowTitle(TypeMessage.error_message), null, Utilities.pathImageMessage(TypeMessage.error_message));
                Logger.generarLogError(ex.Message, new System.Diagnostics.StackFrame(true), ex);
                return;
            }            
            if (u != null)
            {
                if (u.idUsuario != -1)
                {
                    if (!u.estado)
                    {
                        if (u.numIntentosLogeo > 2)
                        {
                            this.MyRadWindowManager.RadAlert("Se ha superado el número de intentos posibles de logeo, comunicarse con el administrador para activar el usuario", 400, 200,
                                                            Utilities.windowTitle(TypeMessage.warning_message), null, Utilities.pathImageMessage(TypeMessage.warning_message));
                        }
                        else 
                        {
                            this.MyRadWindowManager.RadAlert("Usuario Inactivo", 400, 200,
                                Utilities.windowTitle(TypeMessage.warning_message), null, Utilities.pathImageMessage(TypeMessage.warning_message));
                        }
                    }
                    else
                    {
                        FormsAuthentication.RedirectFromLoginPage(nombreUSuario, false);
                    }
                }
                else {
                    this.MyRadWindowManager.RadAlert("No se encontró un usuario coincidente con las credenciales ingresadas", 400, 200,
                                Utilities.windowTitle(TypeMessage.warning_message), null, Utilities.pathImageMessage(TypeMessage.warning_message));
                }
            }
            else
            {
                this.MyRadWindowManager.RadAlert("No se encontró un usuario coincidente con las credenciales ingresadas", 400, 200,
                                                Utilities.windowTitle(TypeMessage.warning_message), null, Utilities.pathImageMessage(TypeMessage.warning_message));
            }
        }

        public void validarVersion()
        {
            string strMessage = "";
            string strPath = Server.MapPath("~\\bin\\");
            string fecha = "";
            int diasVersion;
            try
            {
                diasVersion = Utilities.validarVersion(strPath, ref fecha);
            }
            catch (Exception ex)
            {
                Logger.generarLogError(ex.Message, new System.Diagnostics.StackFrame(true), ex);
                diasVersion = -1;
            }

            if (diasVersion != 100)
            {
                switch (diasVersion)
                {
                    case -1:
                        this.errorLabel.ForeColor = Color.Red;
                        strMessage = "La licencia del aplicativo esta vencida, por favor comunicarse con el administrador.";
                        this.btnIngresar.Enabled = false;
                        this.hyperlink_1.Enabled = false;
                        break;
                    case 0:
                        this.errorLabel.ForeColor = Color.Blue;
                        strMessage = fecha;
                        this.MyRadWindowManager.RadAlert("Recuerde que hoy vence su licencia, no olvide renovarla.", 400, 200,
                            Utilities.windowTitle(TypeMessage.information_message), null, Utilities.pathImageMessage(TypeMessage.information_message));
                        break;
                    case 1:
                        this.errorLabel.ForeColor = Color.Blue;
                        strMessage = fecha;
                        this.MyRadWindowManager.RadAlert("Su licencia pronto vencerá, queda 1 día para renovar la licencia", 400, 200,
                            Utilities.windowTitle(TypeMessage.information_message), null, Utilities.pathImageMessage(TypeMessage.information_message));
                        break;
                    case 2:
                        this.errorLabel.ForeColor = Color.Blue;
                        strMessage = fecha;
                        this.MyRadWindowManager.RadAlert("Su licencia pronto vencerá, quedan 2 días para renovar la licencia", 400, 200,
                            Utilities.windowTitle(TypeMessage.information_message), null, Utilities.pathImageMessage(TypeMessage.information_message));
                        break;
                    case 3:
                        this.errorLabel.ForeColor = Color.Blue;
                        strMessage = fecha;
                        this.MyRadWindowManager.RadAlert("Su licencia pronto vencerá, quedan 3 días para renovar la licencia", 400, 200,
                            Utilities.windowTitle(TypeMessage.information_message), null, Utilities.pathImageMessage(TypeMessage.information_message));
                        break;
                    case 4:
                        this.errorLabel.ForeColor = Color.Blue;
                        strMessage = fecha;
                        this.MyRadWindowManager.RadAlert("Su licencia pronto vencerá, quedan 4 días para renovar la licencia", 400, 200,
                            Utilities.windowTitle(TypeMessage.information_message), null, Utilities.pathImageMessage(TypeMessage.information_message));
                        break;
                    case 5:
                        this.errorLabel.ForeColor = Color.Blue;
                        strMessage = fecha;
                        this.MyRadWindowManager.RadAlert("Su licencia pronto vencerá, quedan 5 días para renovar la licencia", 400, 200,
                            Utilities.windowTitle(TypeMessage.information_message), null, Utilities.pathImageMessage(TypeMessage.information_message));
                        break;
                }
                this.errorLabel.Text = strMessage;
            }
            else
            {
                this.errorLabel.ForeColor = Color.Black;
                this.errorLabel.Text = fecha;
                this.btnIngresar.Enabled = true;
                this.hyperlink_1.Enabled = true;
            }
        }

    }

}
