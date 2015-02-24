<%@ Page Title="Cambiar Contraseña" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="ChangePassword.aspx.cs" Inherits="RipsValidadorWeb.Account.ChangePassword" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <telerik:RadAjaxManager runat="server" id="RadAjaxManager1">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnActualizar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtPasswordOld" />
                    <telerik:AjaxUpdatedControl ControlID="txtPasswordNew" />
                    <telerik:AjaxUpdatedControl ControlID="txtConfirmPasswordNew" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <h2>
        Cambiar Contraseña
    </h2>
    <p>
        Use el siguiente formulario para cambiar de contraseña
    </p>
        <div class="accountInfo">
            <fieldset class="changePassword">
                <legend>Información de la Cuenta</legend>
                <p>
                    <asp:Label ID="lblPasswordOld" runat="server" AssociatedControlID="txtPasswordOld" Font-Bold="true">Contraseña Actual:</asp:Label>
                    <telerik:RadTextBox ID="txtPasswordOld" runat="server" TextMode="Password" MaxLength="20"></telerik:RadTextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfv_txtPasswordOld" ControlToValidate="txtPasswordOld"
                        ErrorMessage="Ingrese la contraseña actual" ForeColor="Red" ValidationGroup="G_btnGuardar" Display="None">*</asp:RequiredFieldValidator>
                    <asp:ValidatorCalloutExtender ID="vce_txtPasswordOld" runat="server" TargetControlID="rfv_txtPasswordOld"></asp:ValidatorCalloutExtender>
                </p>
                
                <p>
                    <asp:Label ID="lblPasswordNew" runat="server" AssociatedControlID="txtPasswordNew" Font-Bold="true">Nueva Contraseña:</asp:Label>
                    <telerik:RadTextBox ID="txtPasswordNew" runat="server" TextMode="Password" MaxLength="20"></telerik:RadTextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfv_txtPasswordNew" ControlToValidate="txtPasswordNew"
                        ErrorMessage="Ingrese la nueva contraseña" ForeColor="Red" ValidationGroup="G_btnGuardar" Display="None">*</asp:RequiredFieldValidator>
                    <asp:ValidatorCalloutExtender ID="vce_txtPasswordNew" runat="server" TargetControlID="rfv_txtPasswordNew"></asp:ValidatorCalloutExtender>
                </p>
                <p>
                    <asp:Label ID="lblConfirmPasswordNew" runat="server" AssociatedControlID="txtConfirmPasswordNew" Font-Bold="true">Confirme la nueva Contraseña:</asp:Label>
                    <telerik:RadTextBox ID="txtConfirmPasswordNew" runat="server" TextMode="Password" MaxLength="20"></telerik:RadTextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfv_txtConfirmPasswordNew" ControlToValidate="txtConfirmPasswordNew"
                        ErrorMessage="Ingrese la nueva contraseña" ForeColor="Red" ValidationGroup="G_btnGuardar" Display="None">*</asp:RequiredFieldValidator>
                    <asp:ValidatorCalloutExtender runat="server" ID="vce_txtConfirmPasswordNew" TargetControlID="rfv_txtConfirmPasswordNew"></asp:ValidatorCalloutExtender>
                    <asp:CompareValidator runat="server" id="cv_txtConfirmPasswordNew" ControlToCompare="txtPasswordNew" ControlToValidate="txtConfirmPasswordNew"
                        ErrorMessage="Las contraseñas no coinciden" ForeColor="Red" ValidationGroup="G_btnGuardar" Display="None">*</asp:CompareValidator>
                    <asp:ValidatorCalloutExtender runat="server" ID="vce2_txtConfirmPasswordNew" TargetControlID="cv_txtConfirmPasswordNew"></asp:ValidatorCalloutExtender>
                </p>
                <p>
                    <telerik:RadButton ID="btnCancelar" runat="server" CausesValidation="false" OnClick="btnCancelar_Click" Text="Cancelar"></telerik:RadButton>
                    <telerik:RadButton ID="btnActualizar" runat="server" Text="Cambiar Contraseña" OnClick="btnActualizar_Click" ValidationGroup="G_btnGuardar"/>
                </p>
            </fieldset>
        </div>
        <telerik:RadWindowManager runat="server" ID="RadWindowManager1"></telerik:RadWindowManager>
</asp:Content>
