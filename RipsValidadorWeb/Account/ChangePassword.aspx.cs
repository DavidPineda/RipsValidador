using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RipsValidadorDao.Model;
using RipsValidadorWeb.Clases;
using MyAutenticationProvider.User;
using RipsValidadorWeb.Enumeradores;

namespace RipsValidadorWeb.Account
{
    public partial class ChangePassword : System.Web.UI.Page
    {

        #region "Eventos"
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["nomUsuario"] = User.Identity.Name;
                ViewState["isExternalUser"] = 0;
                if (Request.QueryString["nomUsuario"] != null)
                {
                    string nomUser = Request.QueryString["nomUsuario"];
                    if (!nomUser.Equals(string.Empty))
                    {
                        ViewState["nomUsuario"] = nomUser;
                        ViewState["isExternalUser"] = 1;
                        camposInvisibles();
                    }
                }            
            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            actualizarUsuario();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            if (int.Parse(ViewState["isExternalUser"].ToString()) == 0)
            {
                Response.Redirect("../Default.aspx");
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            else
            {
                Response.Redirect("UpdateUser.aspx?nomUsuario=" + ViewState["nomUsuario"].ToString());
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
        }

        #endregion

        #region "Metodos"

        private void actualizarUsuario()
        {
            MyMembershipProvider m = new MyMembershipProvider();
            try
            {
                Usuario u = m.consultarUsuarioXnombre(ViewState["nomUsuario"].ToString());
                if (int.Parse(ViewState["isExternalUser"].ToString()) == 1)
                    this.txtPasswordOld.Text = EncriptarClaves.clsEncriptarClases.Decrypt(u.contrasena);
                if (validarUsuario(u))
                {
                    u.contrasena = EncriptarClaves.clsEncriptarClases.Encrypt(this.txtPasswordNew.Text.Trim());
                    m.actualizarUsuario(u, u.idUsuario);
                    this.RadWindowManager1.RadAlert("Contraseña cambiada correctamente", 400, 200,
                        Utilities.windowTitle(TypeMessage.information_message), null, Utilities.pathImageMessage(TypeMessage.information_message));
                    this.txtPasswordOld.Text = string.Empty;
                    this.txtPasswordNew.Text = string.Empty;
                    this.txtConfirmPasswordNew.Text = string.Empty;
                }
                else
                {
                    this.RadWindowManager1.RadAlert("La contraseña actual ingresada no coincide con la almacenada en el sistema",400,200,
                        Utilities.windowTitle(TypeMessage.warning_message), null, Utilities.pathImageMessage(TypeMessage.warning_message));
                }
            }
            catch (Exception ex)
            {
                Logger.generarLogError(ex.Message, new System.Diagnostics.StackFrame(true), ex);
                this.RadWindowManager1.RadAlert(Utilities.errorMessage(), 400,200, 
                    Utilities.windowTitle(TypeMessage.error_message),null,Utilities.pathImageMessage(TypeMessage.error_message));
            }
        }
        private bool validarUsuario(Usuario u)
        {
            return EncriptarClaves.clsEncriptarClases.Decrypt(u.contrasena).Equals(this.txtPasswordOld.Text.Trim());
        }

        private void camposInvisibles()
        {
            this.txtPasswordOld.Visible = false;
            this.txtPasswordOld.Enabled = false;
            this.rfv_txtPasswordOld.Visible = false;
            this.rfv_txtPasswordOld.Enabled = false;
            this.lblPasswordOld.Visible = false;
        }

        #endregion
    }
}