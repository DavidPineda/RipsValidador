<%@ Page Title="Ingresar" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" 
    Inherits="RipsValidadorWeb.Account.Login" MaintainScrollPositionOnPostback="true"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Ingresar Al Sistema
    </h2>
    <asp:Label ID="errorLabel" runat="server" ForeColor="Red"></asp:Label>
    <div class="accountInfo">
        <fieldset class="login">
            <legend>Información de la cuenta</legend>
            <p>
                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName" Font-Bold="true">Nombre de Usuario:</asp:Label>
                <telerik:RadTextBox ID="UserName" runat="server" Height="25px" Width="70%" InputType="Text" MaxLength="20"></telerik:RadTextBox>
                <asp:RequiredFieldValidator runat="server" ID="rfv_UserName" ControlToValidate="UserName" Display="None"
                    ErrorMessage="Ingrese el nombre de usuario" ForeColor="Red" ValidationGroup="G_btnIngresar"></asp:RequiredFieldValidator>
                <asp:ValidatorCalloutExtender ID="vce_rfv_UserName" runat="server" TargetControlID="rfv_UserName"></asp:ValidatorCalloutExtender>
            </p>
            <p>
                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password" Font-Bold="true">Contraseña:</asp:Label>
                <telerik:RadTextBox ID="Password" runat="server" Height="25px" Width="70%" TextMode="Password" InputType="Text" MaxLength="20"></telerik:RadTextBox>
                <asp:RequiredFieldValidator runat="server" ID="rfv_Password" ControlToValidate="Password" Display="None"
                    ErrorMessage="Ingrese la contraseña del usuario" ForeColor="Red" ValidationGroup="G_btnIngresar"></asp:RequiredFieldValidator>
                <asp:ValidatorCalloutExtender runat="server" ID="vce_rfv_Password" TargetControlID="rfv_Password"></asp:ValidatorCalloutExtender>
            </p>
            <p>
                <asp:HyperLink id="hyperlink_1" 
                                NavigateUrl="RecoverPassword.aspx" 
                                Text="¿Olvido su contraseña?" 
                                runat="server"
                                Target="_top" 
                                Visible="true">
                </asp:HyperLink>
            </p>
            <p>
                <telerik:RadAjaxPanel runat="server" ID="RadAjaxPanel1">
                    <telerik:RadButton runat="server" id="btnIngresar" Text="Iniciar Sesion" OnClick="btnIngresar_Click" ValidationGroup="G_btnIngresar"></telerik:RadButton>
                </telerik:RadAjaxPanel>
            </p>
        </fieldset>
    </div>
    <telerik:RadWindowManager runat="server" ID="MyRadWindowManager"></telerik:RadWindowManager>
</asp:Content>
