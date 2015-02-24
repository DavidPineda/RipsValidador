using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using RipsValidadorWeb.Enumeradores;
using RipsValidadorWeb.Clases;
using RipsValidadorDao.Model;
using MyAutenticationProvider.User;
using ControlesAsp.DropDownListControl;
using System.Data;

namespace RipsValidadorWeb.Account
{
    public partial class UpdateUser : System.Web.UI.Page
    {
        #region "Eventos"
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["nomUsuario"] == null)
                {
                    this.MyRadWindowManager.RadAlert("Ingrese el nombre del usuario que desea actualizar, Se cargaran los datos para ser actualizados", 400, 200,
                        Utilities.windowTitle(TypeMessage.information_message), null, Utilities.pathImageMessage(TypeMessage.information_message));
                }
                else
                {
                    this.UserName.Text = Request.QueryString["nomUsuario"].ToString();
                    consultarUsuario();
                }
            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            actualizarUsuario();
        }
        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            consultarUsuario();
        }

        protected void lnkChangePassword_Click(object sender, EventArgs e)
        {
            Response.Redirect("ChangePassword.aspx?nomUsuario=" + UserName.Text.Trim());
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
        #endregion

        #region "Metodos"
        private void consultarUsuario()
        {
            MyMembershipProvider m = new MyMembershipProvider();
            DataTable d = m.consultarRoles();
            Usuario u = m.consultarUsuarioXnombre(this.UserName.Text.Trim());
            if(u.idUsuario != -1){
                cargarCampos(true, u, d);
            }else{
                cargarCampos(false, null, null);
                this.MyRadWindowManager.RadAlert("El usuario " + this.UserName.Text.Trim() + " No se encontro en el sistema", 400, 200,
                    Utilities.windowTitle(TypeMessage.information_message), null, Utilities.pathImageMessage(TypeMessage.information_message));
            }
        }
        private void cargarCampos(bool value, Usuario u, DataTable d)
        {
            if (value)
            {
                this.txtEmail.Text = u.email;
                this.lblNomUsuarioOculto.Text = u.nomUsuario;
                DropDownListASP.llenarDropDownList(d, "value", "text", ref this.ddlRol);
                DropDownListASP.selectIndexByText(ref this.ddlRol, Roles.GetRolesForUser(u.nomUsuario)[0].ToString());
                if (u.estado)
                {
                    this.rblEstado.Items[0].Selected = true;
                }
                else
                {
                    this.rblEstado.Items[1].Selected = true;
                }
                this.Mypanel.Visible = true;
            }
            else
            {
                this.Mypanel.Visible = false;
            }
        }
        private void actualizarUsuario()
        {
            MyMembershipProvider m = new MyMembershipProvider();         
            Usuario u = m.consultarUsuarioXnombre(this.UserName.Text.Trim());
            Usuario u1 = m.consultarUsuarioXnombre(User.Identity.Name);
            u.email = this.txtEmail.Text.Trim();
            u.estado = this.rblEstado.Items[0].Selected ? true : false;
            u.numIntentosLogeo = this.rblEstado.Items[0].Selected ? (short)0 : u.numIntentosLogeo;
            try
            {
                m.actualizarUsuario(u, u1.idUsuario, int.Parse(this.ddlRol.SelectedItem.Value));
                this.MyRadWindowManager.RadAlert("Actualización Ejecutada Correctamente", 400,200,
                    Utilities.windowTitle(TypeMessage.information_message),null,Utilities.pathImageMessage(TypeMessage.information_message));
            }
            catch (Exception ex)
            {
                Logger.generarLogError(ex.Message, new System.Diagnostics.StackFrame(true), ex);
                this.MyRadWindowManager.RadAlert(Utilities.errorMessage(), 400, 200,
                    Utilities.windowTitle(TypeMessage.error_message), null, Utilities.pathImageMessage(TypeMessage.error_message));
            }            
            this.Mypanel.Visible = false;
            this.UserName.Text = string.Empty;
        }
        #endregion

    }
}