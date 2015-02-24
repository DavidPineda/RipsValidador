<%@ Page Title="Actualizar Usuario" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UpdateUser.aspx.cs" 
    Inherits="RipsValidadorWeb.Account.UpdateUser" MaintainScrollPositionOnPostback="true"%>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        td{
            height: 25px;
        }
        .RadInput{
            width:100% !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<telerik:RadAjaxManager runat="server" ID="RadAjaxManager1">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="btnConsultar">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="Mypanel" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="btnActualizar">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="Mypanel" />
                <telerik:AjaxUpdatedControl ControlID="UserName"/>
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>
<h2>
    Actualizar Datos de Usuario
</h2>
<div class="accountInfo">
    <fieldset class="register">
        <legend>Información de cuenta</legend>
            <p>
                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Nombre de usuario:</asp:Label>
                <telerik:RadTextBox ID="UserName" runat="server" Height="25px" Width="70%" InputType="Text" MaxLength="20"></telerik:RadTextBox>
                <asp:RequiredFieldValidator runat="server" ID="rfv_UserName" ControlToValidate="UserName" Display="None"
                    ErrorMessage="Ingrese el nombre de usuario" ForeColor="Red" ValidationGroup="G_Consultar">*</asp:RequiredFieldValidator>
                <asp:ValidatorCalloutExtender ID="vce_rfv_UserName" runat="server" TargetControlID="rfv_UserName"></asp:ValidatorCalloutExtender>
            </p>
            <p>             
                <telerik:RadButton runat="server" id="btnConsultar" Text="Consultar" OnClick="btnConsultar_Click" ValidationGroup="G_Consultar"></telerik:RadButton>
            </p>
    </fieldset>
</div>
<asp:Panel runat="server" ID="Mypanel" Visible="false" Width="95%"> 
<table id="tabla1" style="margin: 10px auto 10px 10px; width:80%; display:block;">
    <tbody>
        <tr>
            <td style="width:40%;">
                <asp:Label runat="server" ID="lblNomUsuario" Text="Nombre de Usuario:" Font-Bold="true"></asp:Label>       
            </td>
            <td align="left" style="width:60%;">
                <asp:Label runat="server" ID="lblNomUsuarioOculto" Text="" Font-Bold="false"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width:40%;">
                <asp:Label runat="server" ID="lblRol" Text="Rol de Usuario:" Font-Bold="true"></asp:Label>
            </td>
            <td align="left" style="width:60%;">
                <telerik:RadDropDownList runat="server" ID="ddlRol"></telerik:RadDropDownList>
            </td>
        </tr>
        <tr>
            <td style="width:40%;">
                <asp:Label runat="server" ID="lblEmail" Text="Correo Electronico" Font-Bold="true"></asp:Label>
            </td>
            <td align="left" style="width:60%;">
                <telerik:RadTextBox runat="server" ID="txtEmail"></telerik:RadTextBox>
                <asp:RequiredFieldValidator runat="server" ID="rfv_txtEmail" Display="None" ControlToValidate="txtEmail"
                    ErrorMessage="Ingrese el correo del afiliado" ForeColor="Red" ValidationGroup="G_Actualizar">*</asp:RequiredFieldValidator>
                <asp:ValidatorCalloutExtender runat="server" ID="vce_rfv_txtEmail" TargetControlID="rfv_txtEmail"></asp:ValidatorCalloutExtender>
                <asp:RegularExpressionValidator runat="server" ID="rev_txtEmail" Display="None" ControlToValidate="txtEmail"
                    ErrorMessage="Ingrese una dirección de correo electronico válida" ForeColor="Red" ValidationGroup="G_Actualizar" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                <asp:ValidatorCalloutExtender runat="server" TargetControlID="rev_txtEmail" ID="vce_rev_txtEmail"></asp:ValidatorCalloutExtender>
            </td>
        </tr>
        <tr>
            <td style="width:40%;">
                <asp:Label runat="server" ID="lblEstadoUsuario" Text="Estado de Usuario" Font-Bold="true"></asp:Label>
            </td>
            <td align="left" style="width:60%;">
                <asp:RadioButtonList runat="server" TextAlign="Left" RepeatDirection="Horizontal" ID="rblEstado">
                    <asp:ListItem Value="0" Text="Activo"></asp:ListItem>
                    <asp:ListItem Value="1" Text="Inactivo"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="width:100%;">
                <telerik:RadButton runat="server" ID="btnActualizar" OnClick="btnActualizar_Click" Text="Actualizar" ValidationGroup="G_Actualizar"></telerik:RadButton>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="width:100%;">
                <asp:LinkButton runat="server" Text="Cambiar Contraseña" ForeColor="Blue" id="lnkChangePassword" OnClick="lnkChangePassword_Click"></asp:LinkButton>
            </td>
        </tr>
    </tbody>
</table>
</asp:Panel>
<telerik:RadWindowManager runat="server" ID="MyRadWindowManager"></telerik:RadWindowManager>
</asp:Content>
