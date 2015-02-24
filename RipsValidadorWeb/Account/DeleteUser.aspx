<%@ Page Title="Eliminar Usuario" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DeleteUser.aspx.cs" 
    Inherits="RipsValidadorWeb.Account.DeleteUser" MaintainScrollPositionOnPostback="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
    function confirmCallbackFn(arg) {
        $get("<%=HiddenField_ConfirmResponse.ClientID %>").value = arg;
        $find("<%=RadAjaxManager1.ClientID %>").ajaxRequest("confirmCallBack");
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h2>
   Eliminar Usuario
</h2>
<telerik:RadAjaxManager runat="server" ID="RadAjaxManager1" OnAjaxRequest="OnAjaxRequest">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="HiddenField_ConfirmResponse" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>
<asp:HiddenField runat="server" ID="HiddenField_ConfirmResponse" />
<div class="accountInfo">
    <fieldset class="register">
        <legend>Información de cuenta</legend>
            <p>
                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Nombre de usuario:</asp:Label>
                <telerik:RadTextBox ID="UserName" runat="server" Height="25px" Width="70%" InputType="Text" MaxLength="20"></telerik:RadTextBox>
                <asp:RequiredFieldValidator runat="server" ID="rfv_UserName" ControlToValidate="UserName" Display="None"
                    ErrorMessage="Ingrese el nombre de usuario" ForeColor="Red" ValidationGroup="G_btnEliminar">*</asp:RequiredFieldValidator>
                <asp:ValidatorCalloutExtender ID="vce_rfv_UserName" runat="server" TargetControlID="rfv_UserName"></asp:ValidatorCalloutExtender>
            </p>
            <p>             
                <telerik:RadButton runat="server" id="btnEliminar" Text="Eliminar" OnClick="btnEliminar_Click" ValidationGroup="G_btnEliminar"></telerik:RadButton>
            </p>
    </fieldset>
</div> 
<telerik:RadWindowManager runat="server" ID="MyRadWindowManager"></telerik:RadWindowManager>
</asp:Content>
