<%@ Page Title="Resultado Validación Archivos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ResultadoValidacion.aspx.cs" Inherits="RipsValidadorWeb.CargaArchivos.ResultadoValidacion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<telerik:RadAjaxManager runat="server" ID="RadAjaxManager1">
	<AjaxSettings>
		<telerik:AjaxSetting AjaxControlID="ibtn_bus_nom">
			<UpdatedControls>
				<telerik:AjaxUpdatedControl ControlID="ddl_ips" />
			</UpdatedControls>
		</telerik:AjaxSetting>
	</AjaxSettings>
	<AjaxSettings>
		<telerik:AjaxSetting AjaxControlID="ibtn_bus_nit">
			<UpdatedControls>
				<telerik:AjaxUpdatedControl ControlID="ddl_ips"/>
			</UpdatedControls>
		</telerik:AjaxSetting>
	</AjaxSettings>
	<AjaxSettings>
		<telerik:AjaxSetting AjaxControlID="rgResultadosCarga">
			<UpdatedControls>
				<telerik:AjaxUpdatedControl ControlID="rgResultadosCarga" />
			</UpdatedControls>
		</telerik:AjaxSetting>
	</AjaxSettings>
	<AjaxSettings>
		<telerik:AjaxSetting AjaxControlID="btn_consultar">
			<UpdatedControls>
				<telerik:AjaxUpdatedControl ControlID="rgResultadosCarga" />
			</UpdatedControls>
		</telerik:AjaxSetting>
	</AjaxSettings> 
</telerik:RadAjaxManager>
<h2>
   ARCHIVOS PROGRAMADOS PARA VALIDACION
</h2>
<br />
<table id="datos" class="TABLA-DATOS1" width="95%" style="margin: 10px auto;">
	<tr>
		<td colspan="6" class="style1"></td>
	</tr>
	<tr>
		<td style="height:25px; font-weight:bold; text-align:center;">
			<asp:Label runat="server" ID="lblrazonSocial">Razón Social:</asp:Label>
		</td>
		<td style="height:25px;">
			<telerik:RadTextBox runat="server" ID="txtRazonSocial"></telerik:RadTextBox>                             
		</td>
		<td style="height:25px;">
			<asp:ImageButton runat="server" ID="ibtn_bus_nom" OnClick="ibtn_bus_nom_Click" ImageAlign="Middle" ImageUrl="~/Images/buscar.ico" />
		</td>
		<td style="height:25px; font-weight:bold; text-align:center;">
			<asp:Label runat="server" ID="lblNitIps">Nit:</asp:Label>
		</td>                
		<td style="height:25px;">
			<telerik:RadTextBox runat="server" ID="txtNitIps"></telerik:RadTextBox>
		</td>
		<td style="height:25px;">
			<asp:ImageButton runat="server" ID="ibtn_bus_nit" OnClick="ibtn_bus_nit_Click" ImageAlign="Middle" ImageUrl="~/Images/buscar.ico" />
		</td>
	</tr>
	<tr>
		<td class="style1" style="WIDTH: 10%" align="center">
		<asp:Label id="lbl_Ips" Runat="server" style="Z-INDEX: 0" Font-Bold="True" Text="IPS" ></asp:Label>
		</td>
		<td class="style1" style="WIDTH: 90%" colspan="5" align="left">
		<telerik:RadDropDownList ID="ddl_ips" runat="server" AutoPostBack="true" Width="90%" DropDownWidth="100%" DropDownHeight="100px">
		</telerik:RadDropDownList>
		</td>
	</tr>
	<tr>
		<td class="style1" style="WIDTH: 15%" align="center">
			<asp:Label ID="lbl_fecha_ini" runat="server" Text="Fecha Inicio" Font-Bold="true"></asp:Label>
		</td>
		<td class="style1" style="WIDTH: 15%" align="left">
			<telerik:RadDatePicker runat="server" ID="txt_fecha_ini"></telerik:RadDatePicker>
		</td>
		<td class="style1" style="WIDTH: 15%" align="center">					
			<asp:Label ID="lbl_fecha_fin" runat="server" Text="Fecha Fin" Font-Bold="true"></asp:Label>
		</td>
		<td class="style1" style="WIDTH: 15%" align="left">
            <telerik:RadDatePicker runat="server" ID="txt_fecha_fin"></telerik:RadDatePicker>
            <asp:CompareValidator runat="server" ID="cv_txt_fecha_fin" ControlToValidate="txt_fecha_fin" ControlToCompare="txt_fecha_ini"
                ErrorMessage="La fecha final debe ser mayor o igual a la inicial" ForeColor="Red" Display="None"
                Type="Date" Operator="GreaterThanEqual">*</asp:CompareValidator>
            <asp:ValidatorCalloutExtender runat="server" ID="rfv_cv_txt_fecha_fin" TargetControlID="cv_txt_fecha_fin"></asp:ValidatorCalloutExtender>
		</td>
		<td class="style1" style="WIDTH: 15%" align="center">
			<asp:Label ID="lbl_estado" runat="server" Text="Estado" Font-Bold="true"></asp:Label>
		</td>
		<td class="style1" style="WIDTH: 25%" align="left">
			<telerik:RadDropDownList ID="ddl_estados" runat="server" Width="65%" DropDownWidth="100%" 
				CssClass="styled_select"></telerik:RadDropDownList>
		</td>				  
	</tr>
	<tr>
		<td style="height:25px; text-align:center;">
			&nbsp;</td>
		<td style="height:25px; float:left;">
			&nbsp;</td>
		<td colspan="3" class="style1"></td>
		<td class="style1" align="center">
			<telerik:RadButton id="btn_consultar" runat="server" Text="Consultar" CssClass="botones" OnClick="btn_consultar_Click"/>
		</td>
	</tr>
	<tr>
		<td colspan="6" class="style1" align="center"></td>
	</tr>
	<tr>
		<td colspan="6" class="style1" align="center">
            <telerik:RadGrid runat="server" ID="rgResultadosCarga" AllowPaging="true" ShowFooter="true" ShowHeader="true"
                AutoGenerateColumns="false" AllowSorting="true" ShowStatusBar="true" Width="95%" PageSize="5"
                OnPreRender="rgResultadosCarga_PreRender" OnNeedDataSource="rgResultadosCarga_NeedDataSource" 
                OnItemCommand="rgResultadosCarga_ItemCommand" Culture="es-CO">
                <MasterTableView CommandItemDisplay="Top" Width="100%" InsertItemPageIndexAction="ShowItemOnLastPage" DataKeyNames="id_programacion, estado_programacion">
                    <EditFormSettings>
                        <PopUpSettings Modal="true" />
                    </EditFormSettings>
                    <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="true"/>
                    <Columns>
                        <telerik:GridBoundColumn UniqueName="id_programacion" DataField="id_programacion" HeaderText="N° Programación">
                            <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                            <ItemStyle HorizontalAlign="Center"/>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="periodo_cobro" DataField="periodo_cobro" HeaderText="Periodo">
                            <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                            <ItemStyle HorizontalAlign="Center"/>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="fecha_programacion" DataField="fecha_programacion" HeaderText="Fecha Carga">
                            <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                            <ItemStyle HorizontalAlign="Center"/>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="descripcion" DataField="descripcion" HeaderText="Estado">
                            <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                            <ItemStyle HorizontalAlign="Center"/>
                        </telerik:GridBoundColumn>
                        <telerik:GridButtonColumn UniqueName="detalles"  HeaderText="Detalles" ButtonType="ImageButton" CommandName="Select" Exportable="false" ImageUrl="~/Images/detalles.ico">
                            <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                            <ItemStyle HorizontalAlign="Center"/>
                        </telerik:GridButtonColumn>                                                     
                        <telerik:GridButtonColumn UniqueName="deleteRegister" ConfirmText="Borrar este registro" ConfirmDialogType="RadWindow" HeaderText="Eliminar"
                            ConfirmTitle="Eliminar" ButtonType="ImageButton" CommandName="Delete" Exportable="false" ImageUrl="~/Images/eliminar.ico">
                            <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                            <ItemStyle HorizontalAlign="Center"/>
                        </telerik:GridButtonColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>    			            
		</td>
	</tr>
</table>
<telerik:RadWindowManager runat="server" ID="RadWindowManager1"></telerik:RadWindowManager>
</asp:Content>
