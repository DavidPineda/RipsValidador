using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxPro;
using Telerik.Web.UI;
using System.Data;
using ControlesAsp.DataTableControl;
using ControlesAsp.DropDownListControl;
using RipsValidadorWeb.Clases;
using System.Diagnostics;
using RipsValidadorWeb.Enumeradores;
using EncriptarClaves;
using RipsValidadorDao.ConnectionDB.Generales;
using MyAutenticationProvider.User;

namespace RipsValidadorWeb.Account
{
    //[AjaxNamespace("Register")] 
    public partial class Register : System.Web.UI.Page
    {
        #region "Eventos"
        protected void Page_Load(object sender, EventArgs e)
        {
            Utility.RegisterTypeForAjax(typeof(Register));
            if (!IsPostBack)
            {
                cargaCombosInicial();
            }
        }

        protected void btnCrear_Click(object sender, EventArgs e)
        {
            if (validaLongitudMinimaContrasena())
            {
                //if (validarContrasenas())
                //{
                    //string mensaje = validarCombos();
                    //if (!mensaje.Equals(string.Empty))
                    //{
                    //    this.myRadWindowManager.RadAlert(mensaje, 400, 200, Utilities.windowTitle(TypeMessage.warning_message), null,
                    //        Utilities.pathImageMessage(TypeMessage.warning_message));
                    //}
                    //else
                    //{
                        //actualizaCombos();
                guardarUsuario();
                    //}
                //}
                //else
                //{
                //    this.myRadWindowManager.RadAlert("Las contraseñas no coinciden", 400, 200, Utilities.windowTitle(TypeMessage.warning_message), null,
                //        Utilities.pathImageMessage(TypeMessage.warning_message));
                //}
            }
            else
            {
                this.myRadWindowManager.RadAlert("La longitud minima para la contraseña es de 6 caracteres", 400, 200, Utilities.windowTitle(TypeMessage.warning_message), null,
                    Utilities.pathImageMessage(TypeMessage.warning_message));
            }
        }

        protected void txtBuscarIPS_OnTextChanged(object sender, EventArgs e)
        {
            cargarIPS();
        }

        protected void ddlIps_SelectedIndexChanged(object sender, DropDownListEventArgs e)
        {
            cargarSedeIPS();
        }

        #endregion

        //[AjaxMethod]
        //public string consultarIPS(string ips)
        //{
        //    Consulta c = new Consulta();
        //    DataTable myDataTable = c.consultarIpsXnombre(ips);
        //    myDataTable.Merge(c.consultarIpsXnit(ips));
        //    return DataTableASP.convertDatatableToString(myDataTable);
        //}

        //[AjaxMethod]
        //public string consultarSedeIps(string codIPS)
        //{
        //    Consulta c = new Consulta();
        //    return DataTableASP.convertDatatableToString(c.consultarSedesIPSxCodIps(codIPS));
        //}

        private void cargaCombosInicial()
        {
            try
            {
                MyMembershipProvider m = new MyMembershipProvider();
                DropDownListASP.AddItemToDropDownList(ref this.ddlIps, "SELECCIONE>>", "-1", true);
                DropDownListASP.AddItemToDropDownList(ref this.ddlSedeIps, "SELECCIONE>>", "-1", true);
                Consulta c = new Consulta();
                DropDownListASP.llenarDropDownList(c.consultarRegionales(), "value", "text", ref this.ddlRegional);
                DropDownListASP.AddItemToDropDownList(ref this.ddlRegional, "SELECCIONE>>", "-1", true);
                //DropDownListASP.llenarDropDownList(Utilities.convertArrayRolesToDataTable(Roles.GetAllRoles()), "value", "text", ref this.ddlRol);
                DropDownListASP.llenarDropDownList(m.consultarRoles(), "value", "text", ref this.ddlRol);
            }
            catch (Exception ex)
            {
                Logger.generarLogError(ex.Message, new StackFrame(true), ex);
                this.myRadWindowManager.RadAlert(Utilities.errorMessage(), 400, 200, Utilities.windowTitle(TypeMessage.error_message), null, 
                    Utilities.pathImageMessage(TypeMessage.error_message));
            }
        }

        private void cargarIPS()
        {
            Consulta c = new Consulta();
            try
            {
                DataTable myDataTable = c.consultarIpsXnombre(this.txtBuscarIPS.Text.Trim());
                myDataTable.Merge(c.consultarIpsXnit(this.txtBuscarIPS.Text.Trim()));
                DropDownListASP.llenarDropDownList(myDataTable, "value", "text", ref this.ddlIps);
                DropDownListASP.AddItemToDropDownList(ref this.ddlIps, "SELECCIONE>>", "-1", true);
                this.ddlIps.Focus();
            }
            catch (Exception ex)
            {
                Logger.generarLogError(ex.Message, new StackFrame(true), ex);
                this.myRadWindowManager.RadAlert(Utilities.errorMessage(), 400, 200, Utilities.windowTitle(TypeMessage.error_message), null,
                    Utilities.pathImageMessage(TypeMessage.error_message));
            }
        }

        private void cargarSedeIPS()
        {
            Consulta c = new Consulta();
            try
            {
                DropDownListASP.llenarDropDownList(c.consultarSedesIPSxCodIps(this.ddlIps.SelectedValue), "value", "text", ref this.ddlSedeIps);
                DropDownListASP.AddItemToDropDownList(ref this.ddlSedeIps, "SELECCIONE>>", "-1", true);
                this.ddlSedeIps.Focus();
            }catch(Exception ex)
            {
                Logger.generarLogError(ex.Message, new StackFrame(true), ex);
                this.myRadWindowManager.RadAlert(Utilities.errorMessage(), 400, 200, Utilities.windowTitle(TypeMessage.error_message), null,
                    Utilities.pathImageMessage(TypeMessage.error_message));
            }
        }

        private string validarCombos()
        {
            if (this.ddlIps.SelectedValue == "-1")
                return "Seleccione una IPS";
            if (this.ddlRegional.SelectedValue == "-1")
                return "Seleccione una Regional";
            if (this.ddlRol.SelectedValue == "-1")
                return "Seleccione un Rol";
            if (this.ddlSedeIps.SelectedValue == "-1")
                return "Selecione una Sede";
            return string.Empty;
        }

        private void guardarUsuario()
        {
            MyMembershipProvider i = new MyMembershipProvider();
            RipsValidadorDao.Model.Usuario u = new RipsValidadorDao.Model.Usuario();
            u.codIps = this.ddlIps.SelectedItem.Value;
            u.codRegional = this.ddlRegional.SelectedItem.Value;
            u.codSedeIps = this.ddlSedeIps.SelectedItem.Value;
            u.contrasena = clsEncriptarClases.Encrypt(this.txtPassword.Text.Trim());
            u.nomUsuario = this.txtNombre.Text.Trim();
            u.numIntentosLogeo = 0;
            u.email = this.txtEmail.Text.Trim();
            u.estado = true;
            try
            {
                if (i.consultarUsuarioXnombre(u.nomUsuario).idUsuario == -1)
                {
                    i.crearUsuario(u, i.consultarUsuarioXnombre(User.Identity.Name).idUsuario);
                    Roles.AddUserToRole(u.nomUsuario, this.ddlRol.SelectedItem.Text.Trim());
                    this.myRadWindowManager.RadAlert("Usuario Creado Correctamente", 400, 200, 
                        Utilities.windowTitle(TypeMessage.information_message), null, Utilities.pathImageMessage(TypeMessage.information_message));
                    limpiarCampos();
                }
                else
                {
                    this.myRadWindowManager.RadAlert("Ya existe un usuario con el mismo nombre en el sistema, Favor elegir otro", 400, 200, 
                        Utilities.windowTitle(TypeMessage.error_message), null, Utilities.pathImageMessage(TypeMessage.error_message));
                }
            }
            catch (Exception ex)
            {
                Logger.generarLogError(ex.Message, new StackFrame(true), ex);
                this.myRadWindowManager.RadAlert(Utilities.errorMessage(), 400, 200, Utilities.windowTitle(TypeMessage.error_message), null, 
                    Utilities.pathImageMessage(TypeMessage.error_message));
            }            
        }

        private bool validarContrasenas()
        {
            return this.txtPassword.Text.Trim().Equals(this.txtRepeatPassword.Text.Trim());
        }

        private bool validaLongitudMinimaContrasena()
        {
            return this.txtPassword.Text.Length > 5;
        }

        //private void actualizaCombos(){
        //    if(this.ddlIps.ClientChanges.Count > 0)
        //        this.ddlIps.Items.Clear();
        //    foreach (ClientOperation<RadComboBoxItem> operation in this.ddlIps.ClientChanges)
        //    {
        //        switch (operation.Type)
        //        {
        //            case ClientOperationType.Insert:
        //                RadComboBoxItem r = operation.Item;
        //                this.ddlIps.Items.Add(r);
        //                break;
        //        }
        //    }
        //    if(this.ddlSedeIps.ClientChanges.Count > 0)
        //        this.ddlSedeIps.Items.Clear();
        //    foreach (ClientOperation<RadComboBoxItem> operation in this.ddlSedeIps.ClientChanges)
        //    {
        //        switch (operation.Type)
        //        {
        //            case ClientOperationType.Insert:
        //                RadComboBoxItem r = operation.Item;
        //                this.ddlSedeIps.Items.Add(r);
        //                break;
        //        }
        //    }
        //}

        private void limpiarCampos()
        {
            this.txtNombre.Text = string.Empty;
            this.txtBuscarIPS.Text = string.Empty;
            this.txtEmail.Text = String.Empty;
            this.txtPassword.Text = string.Empty;
            this.txtRepeatPassword.Text = string.Empty;
            DropDownListASP.selectIndexByValue(ref this.ddlIps, "-1");
            DropDownListASP.selectIndexByValue(ref this.ddlSedeIps, "-1");
            DropDownListASP.selectIndexByValue(ref this.ddlRegional, "-1");
            DropDownListASP.selectIndexByValue(ref this.ddlRol, "1");
        }
    }
}
