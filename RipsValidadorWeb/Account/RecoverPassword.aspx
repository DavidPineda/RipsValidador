<%@ Page Title="Recuperar Contraseña" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RecoverPassword.aspx.cs"
     Inherits="RipsValidadorWeb.Account.recoverPassword" MaintainScrollPositionOnPostback="true"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h2>
	Recuperar Contraseña
</h2>
<div class="accountInfo">
	<fieldset class="register">
		<legend>Información de cuenta</legend>
			<p>
				<asp:Label id="lbl_usuario" Runat="server" AssociatedControlID="UserName">Nombre de usuario</asp:Label>
				<telerik:RadTextBox ID="UserName" runat="server" Height="25px" Width="70%" InputType="Text" MaxLength="20"></telerik:RadTextBox>
                <asp:RequiredFieldValidator runat="server" ID="rfv_UserName" ControlToValidate="UserName" Display="Dynamic"
                    ErrorMessage="Ingrese el nombre de usuario" ForeColor="Red" ValidationGroup="G_btnRecuperar"></asp:RequiredFieldValidator>
                <asp:ValidatorCalloutExtender ID="vce_rfv_UserName" runat="server" TargetControlID="rfv_UserName"></asp:ValidatorCalloutExtender>
			</p>
			<p>
                <telerik:RadAjaxPanel runat="server" ID="RadAjaxPanel1">
				    <telerik:RadButton runat="server" id="btnRecuperar" Text="Recuperar" OnClick="btnRecuperar_Click" ValidationGroup="G_btnRecuperar"></telerik:RadButton>
                </telerik:RadAjaxPanel>
			</p>
	</fieldset>
	</div>
	<telerik:RadWindowManager runat="server" ID="MyRadWindowManager"></telerik:RadWindowManager>
</asp:Content>
