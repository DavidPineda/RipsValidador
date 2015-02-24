<%@ Page Title="Crear Usuario" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" codeBehind="Register.aspx.cs" 
	Inherits="RipsValidadorWeb.Account.Register" MaintainScrollPositionOnPostback="true"%>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
	<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js" type="text/javascript"></script>
	<telerik:RadCodeBlock runat="server" ID="myRadCodeBlock">
		<script type="text/javascript">
<%--			function checkPasswordMatch() {
				var text1 = $find("<%=txtPassword.ClientID%>").get_textBoxValue();
				var text2 = $find("<%=txtRepeatPassword.ClientID %>").get_textBoxValue();

				if (text2 == "") {
					$get("PasswordRepeatedIndicator").innerHTML = "";
					$get("PasswordRepeatedIndicator").className = "Base L0";
				}
				else if (text1 == text2) {
					$get("PasswordRepeatedIndicator").innerHTML = "Coincide";
					$get("PasswordRepeatedIndicator").className = "Base L5";
				}
				else {
					$get("PasswordRepeatedIndicator").innerHTML = "No coincide";
					$get("PasswordRepeatedIndicator").className = "Base L1";
				}
			}--%>
		    <%--			function llenarIPS() {
				dropdownlist = $find("<%=ddlIps.ClientID %>");
				//items = dropdownlist.get_items();
				dropdownlist.get_items().clear();
				dropdownlist.trackChanges();
				ips = $find("<%=txtBuscarIPS.ClientID%>").get_textBoxValue();
			    jsonNET = Register.consultarIPS(ips);
				json = $.parseJSON(jsonNET.value);
				if (json.length > 0) {
					$.each(json, function (i, item) {
						MyDropDownListItem = new Telerik.Web.UI.RadComboBoxItem();
						MyDropDownListItem.set_text(item.text);
						MyDropDownListItem.set_value(item.value);
						dropdownlist.get_items().add(MyDropDownListItem);
					});
				} else {
					MyDropDownListItem = new Telerik.Web.UI.RadComboBoxItem();
					MyDropDownListItem.set_text("SELECCIONE>>");
					MyDropDownListItem.set_value("-1");
					dropdownlist.get_items().add(MyDropDownListItem);	
				}
				dropdownlist.commitChanges();
				dropdownlist.showDropDown();
			} --%>    
		</script>    
	</telerik:RadCodeBlock>
<%--	<script type="text/javascript" id="telerikClientEvents1">
//<![CDATA[

	function OnClientItemSelected(sender, eventArgs) {
		item = eventArgs.get_item();
		dropdownlist = $find("<%=ddlSedeIps.ClientID %>");
		dropdownlist.get_items().clear();
		dropdownlist.trackChanges();
		jsonNET = Register.consultarSedeIps(item.get_value());
		json = $.parseJSON(jsonNET.value);
		$.each(json, function (i, item) {
			MyDropDownListItem = new Telerik.Web.UI.RadComboBoxItem();
			MyDropDownListItem.set_text(item.text);
			MyDropDownListItem.set_value(item.value);
			dropdownlist.get_items().add(MyDropDownListItem);
		});
		dropdownlist.commitChanges();
		dropdownlist.showDropDown();
	}

//]]>
</script>--%>
    <style type="text/css">
        .RadInput{
            width:50% !important;
        }
        .RadDropDownList{
            width:50% !important;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
<h2>
	Crear una nueva Cuenta
</h2>
    <telerik:RadAjaxManager runat="server" ID="RadAjaxManager1">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="txtBuscarIPS">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ddlIps"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ddlIps">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ddlSedeIps" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnCrear">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtNombre"/>
                    <telerik:AjaxUpdatedControl ControlID="txtBuscarIPS"/>
                    <telerik:AjaxUpdatedControl ControlID="txtEmail"/>
                    <telerik:AjaxUpdatedControl ControlID="txtPassword"/>
                    <telerik:AjaxUpdatedControl ControlID="txtRepeatPassword"/>
                    <telerik:AjaxUpdatedControl ControlID="ddlIps"/>
                    <telerik:AjaxUpdatedControl ControlID="ddlSedeIps"/>
                    <telerik:AjaxUpdatedControl ControlID="ddlRegional"/>
                    <telerik:AjaxUpdatedControl ControlID="ddlRol" />
                    <telerik:AjaxUpdatedControl ControlID="myRadWindowManager" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
<div style="width: 90%; margin:5px 10px; float:left; display:block;">
    <p>
        Todos los campos marcados con * son obligatorios
    </p>
	<fieldset class="register">
		<legend>Información de la Cuenta</legend>
		<p>
			<asp:Label runat="server" ID="lblNomUsuario" AssociatedControlID="txtNombre" Font-Bold="true">Nombre de Usuario *</asp:Label>
			<telerik:RadTextBox runat="server" ID="txtNombre" Height="25px" InputType="Text" MaxLength="20"></telerik:RadTextBox>
            <asp:RequiredFieldValidator runat="server" ID="frv_txtNombre" ControlToValidate="txtNombre" Display="None"
                ErrorMessage="Ingrese el nombre de usuario" ForeColor="Red" ValidationGroup="G_btnCrear">*</asp:RequiredFieldValidator>
            <asp:ValidatorCalloutExtender ID="vce_frv_txtNombre" runat="server" TargetControlID="frv_txtNombre"></asp:ValidatorCalloutExtender>
		</p>
		<p>
			<asp:Label runat="server" ID="lblConsultaIPS" AssociatedControlID="txtBuscarIPS" Font-Bold="true">Consultar IPS(Razon Social o Nit)</asp:Label>
			<telerik:RadTextBox runat="server" ID="txtBuscarIPS" InputType="Text" MaxLength="500" Height="25px" AutoPostBack="true" 
                OnTextChanged="txtBuscarIPS_OnTextChanged"></telerik:RadTextBox>
			<asp:Label runat="server" ID="lblIPS" Font-Bold="true" AssociatedControlID="ddlIps">Seleccione la IPS *</asp:Label>
			<telerik:RadDropDownList runat="server" ID="ddlIps" AutoPostBack="true" Width="55%" DropDownHeight="50px" DropDownWidth="100%" 
                OnSelectedIndexChanged="ddlIps_SelectedIndexChanged"></telerik:RadDropDownList>
            <asp:RequiredFieldValidator runat="server" ID="rfv_ddlIps" ControlToValidate="ddlIps" Display="None"
                InitialValue="SELECCIONE>>" ErrorMessage="Seleccione una IPS" ValidationGroup="G_btnCrear" ForeColor="Red">*</asp:RequiredFieldValidator>
            <asp:ValidatorCalloutExtender ID="vce_rfv_ddlIps" runat="server" TargetControlID="rfv_ddlIps"></asp:ValidatorCalloutExtender>
		</p>
		<p>
			<asp:Label runat="server" ID="lblSedeIps" Font-Bold="true" AssociatedControlID="ddlSedeIps">Sede de la IPS *</asp:Label>
			<telerik:RadDropDownList runat="server" ID="ddlSedeIps" DropDownHeight="50px" Width="55%" DropDownWidth="100%"></telerik:RadDropDownList>
            <asp:RequiredFieldValidator runat="server" ID="rfv_ddlSedeIps" ControlToValidate="ddlSedeIps" Display="None"
                ErrorMessage="Seleccione una sede de la IPS" InitialValue="SELECCIONE>>" ForeColor="Red" ValidationGroup="G_btnCrear">*</asp:RequiredFieldValidator>
            <asp:ValidatorCalloutExtender runat="server" ID="vce_ddlSedeIps" TargetControlID="rfv_ddlSedeIps"></asp:ValidatorCalloutExtender>
		</p>
		<p>
		   <asp:Label runat="server" ID="lblRegional" Font-Bold="true" AssociatedControlID="ddlRegional">Regional *</asp:Label>
		   <telerik:RadDropDownList runat="server" ID="ddlRegional" DropDownHeight="50px" ValidationGroup="G_btnCrear"></telerik:RadDropDownList>
           <asp:RequiredFieldValidator runat="server" ID="rfv_ddlRegional" ControlToValidate="ddlRegional" Display="None"
               ErrorMessage="Seleccione la regional" InitialValue="SELECCIONE>>" ForeColor="Red" ValidationGroup="G_btnCrear">*</asp:RequiredFieldValidator>
            <asp:ValidatorCalloutExtender runat="server" ID="vce_rfv_ddlRegional" TargetControlID="rfv_ddlRegional"></asp:ValidatorCalloutExtender>
		</p>
		<p>
			<asp:Label runat="server" ID="lblEmail" AssociatedControlID="txtEmail" Font-Bold="true">Correo Electronico *</asp:Label>
			<telerik:RadTextBox runat="server" ID="txtEmail" Height="25px" MaxLength="100"></telerik:RadTextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfv_txtEmail" ControlToValidate="txtEmail" Display="None"
                ErrorMessage="Ingrese el correo electronico del usuario" ForeColor="Red" ValidationGroup="G_btnCrear">*</asp:RequiredFieldValidator>
            <asp:ValidatorCalloutExtender runat="server" ID="vce_rfv_txtEmail" TargetControlID="rfv_txtEmail"></asp:ValidatorCalloutExtender>
            <asp:RegularExpressionValidator runat="server" ID="rev_txtEmail" Display="None" ControlToValidate="txtEmail"
                ErrorMessage="Ingrese una direccion de correo válida" ForeColor="Red" ValidationGroup="G_btnCrear" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" >*</asp:RegularExpressionValidator>
            <asp:ValidatorCalloutExtender runat="server" ID="vce_rev_txtEmail" TargetControlID="rev_txtEmail"></asp:ValidatorCalloutExtender>
		</p>
		<p>
			<asp:Label runat="server" ID="lblPassword" AssociatedControlID="txtPassword" Font-Bold="true">Contrasena (Longitud Minima 6 Caracteres) *</asp:Label>
			<telerik:RadTextBox runat="server" InputType="Text" TextMode="Password" ID="txtPassword" Height="25px" 
                MaxLength="20"></telerik:RadTextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfv_txtPassword" Display="None" ControlToValidate="txtPassword"
                ErrorMessage="Ingrese la contraseña del usuario" ForeColor="Red" ValidationGroup="G_btnCrear">*</asp:RequiredFieldValidator>
            <asp:ValidatorCalloutExtender runat="server" ID="vce_rfv_txtPassword" TargetControlID="rfv_txtPassword"></asp:ValidatorCalloutExtender>
		</p>
		<p>
			<asp:Label runat="server" ID="lblRepeatPassword" AssociatedControlID="txtRepeatPassword" Font-Bold="true">Confirmar Contrasena (Longitud Minima 6 Caracteres) *</asp:Label>
			<telerik:RadTextBox runat="server" InputType="Text" TextMode="Password" ID="txtRepeatPassword" Height="25px" MaxLength="20" Width="50%" onkeyup="checkPasswordMatch()">
			<ClientEvents></ClientEvents>
			</telerik:RadTextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfv_txtRepeatPassword" Display="None" ControlToValidate="txtRepeatPassword"
                ErrorMessage="Ingrese la contraseña del usuario" ForeColor="Red" ValidationGroup="G_btnCrear">*</asp:RequiredFieldValidator>
            <asp:ValidatorCalloutExtender runat="server" ID="vce_rfv_txtRepeatPassword" TargetControlID="rfv_txtRepeatPassword"></asp:ValidatorCalloutExtender>
            <asp:CompareValidator runat="server" ID="cv_txtRepeatPassword" Display="None" ControlToValidate="txtRepeatPassword"
                ControlToCompare="txtPassword" ForeColor="Red" ValidationGroup="G_btnCrear" ErrorMessage="Las contraseñas no coinciden">*</asp:CompareValidator>
            <asp:ValidatorCalloutExtender runat="server" ID="vce_cv_txtRepeatPassword" TargetControlID="cv_txtRepeatPassword"></asp:ValidatorCalloutExtender>
		   <%--<span id="PasswordRepeatedIndicator" class="Base L0">&nbsp;</span>--%>
		</p>
		<p>
			<asp:Label runat="server" ID="lblRol" AssociatedControlID="ddlRol" Font-Bold="true">Rol de Usuario *</asp:Label>
			<telerik:RadDropDownList runat="server" ID="ddlRol"></telerik:RadDropDownList>
		</p>
        <p>
	        <telerik:RadButton runat="server" Text="Crear" ID="btnCrear" onclick="btnCrear_Click" ValidationGroup="G_btnCrear"></telerik:RadButton>
        </p>
	</fieldset>
</div>
	<telerik:RadWindowManager runat="server" ID="myRadWindowManager"></telerik:RadWindowManager>
<br />
<br />
</asp:Content>
