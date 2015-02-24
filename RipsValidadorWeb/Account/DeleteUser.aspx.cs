using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using RipsValidadorWeb.Clases;
using RipsValidadorWeb.Enumeradores;
using System.Web.Services;
using MyAutenticationProvider.User;
using System.Diagnostics;
using RipsValidadorDao.Model;

namespace RipsValidadorWeb.Account
{
    public partial class DeleteUser : System.Web.UI.Page
    {
        #region "Eventos"
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.MyRadWindowManager.RadAlert("Por favor digite el nombre de usuario en el recuadro indicado y de clic en el botón Eliminar, se eliminaran las credenciales del usuario en el sistema, junto con todos los datos adjuntos al mismo",
                           400, 200, Utilities.windowTitle(TypeMessage.information_message), null, Utilities.pathImageMessage(TypeMessage.information_message));
            }
        }
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            validaEliminacion();
        }
        protected void OnAjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {
            if (e.Argument == "confirmCallBack")
            {
                if (HiddenField_ConfirmResponse.Value.Equals("true"))
                {
                    MyMembershipProvider m = new MyMembershipProvider();
                    try
                    {
                        m.eliminarUsuario(m.consultarUsuarioXnombre(this.UserName.Text.Trim()), m.consultarUsuarioXnombre(User.Identity.Name));
                        MyRadWindowManager.RadAlert("Usuario eliminado correctamente", 400, 200,
                            Utilities.windowTitle(TypeMessage.information_message), null, Utilities.pathImageMessage(TypeMessage.information_message));
                    }
                    catch (Exception ex)
                    {
                        Logger.generarLogError(ex.Message, new StackFrame(true), ex);
                        MyRadWindowManager.RadAlert(Utilities.errorMessage(), 400, 200,
                            Utilities.windowTitle(TypeMessage.error_message), null, Utilities.pathImageMessage(TypeMessage.error_message));
                    }
                }
            }
        }

        #endregion

        #region "Metodos"
        private void validaEliminacion()
        {
            if (!User.Identity.Name.Equals(this.UserName.Text.Trim()))
            {
                MyMembershipProvider m = new MyMembershipProvider();
                Usuario u = m.consultarUsuarioXnombre(this.UserName.Text.Trim());
                if (u != null && u.idUsuario != -1)
                {
                    MyRadWindowManager.RadConfirm("Esta seguro de eliminar todos los datos del usuario " + this.UserName.Text.Trim(),
                        "confirmCallbackFn", 400, 200, "null", Utilities.windowTitle(TypeMessage.question_message));
                }
                else
                {
                    MyRadWindowManager.RadAlert("EL usuario ingresado no existe en el sistema", 400, 200,
                        Utilities.windowTitle(TypeMessage.information_message), null, Utilities.pathImageMessage(TypeMessage.information_message));
                }
            }
            else
            {
                MyRadWindowManager.RadAlert("No es posible eliminar el usuario con el cual esta logeado actualmente", 400, 200,
                    Utilities.windowTitle(TypeMessage.information_message), null, Utilities.pathImageMessage(TypeMessage.information_message));
            }
        }
        #endregion
    }
}