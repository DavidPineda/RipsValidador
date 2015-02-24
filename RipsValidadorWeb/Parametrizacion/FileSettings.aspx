<%@ Page Title="Ajustar Archivos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FileSettings.aspx.cs" 
    Inherits="RipsValidadorWeb.Parametrizacion.FileSettings" MaintainScrollPositionOnPostback="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        td{
            height:25px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadAjaxManager runat="server" ID="RadAjaxManager1">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnNuevo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="myPanel"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
                <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnCancelar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="myPanel"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="myPanle1" />
                    <telerik:AjaxUpdatedControl ControlID="myPanel"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rgArchivos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="myPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
<h2>
    Ajuste de Archivos
</h2>
<asp:Panel runat="server" ID="myPanle1" Width="100%">
<table width="90%" style="margin:10px auto;">
    <tbody>
        <tr>
            <td align="center">
                <asp:Label runat="server" ID="lblSubTitulo" Text="Archivos Parametrizados" ForeColor="Blue" Font-Bold="true"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center">
                <telerik:RadGrid runat="server" ID="rgArchivos" AllowPaging="true" ShowFooter="false" ShowHeader="true"
                    AutoGenerateColumns="false" AllowSorting="true" ShowStatusBar="true" OnItemCommand="rgArchivos_ItemCommand"
                    OnNeedDataSource="rgArchivos_NeedDataSource" OnPreRender="rgArchivos_PreRender" Width="95%" Culture="es-CO">
                    <MasterTableView CommandItemDisplay="None" Width="100%" InsertItemPageIndexAction="ShowItemOnLastPage" DataKeyNames="COD_ARCHIVO">
                        <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="true"/>
                        <Columns>
                            <telerik:GridButtonColumn UniqueName="updateRegister" HeaderText="Editar" ButtonType="ImageButton"
                                CommandName="Update" Exportable="false" ImageUrl="~/Images/editar.ico">
                                <HeaderStyle HorizontalAlign="Center" Font-Bold="true"/>
                                <ItemStyle HorizontalAlign="Center"/>
                            </telerik:GridButtonColumn>
                            <telerik:GridBoundColumn HeaderText="Código Archivo" DataField="COD_ARCHIVO" UniqueName="COD_ARCHIVO">
                                <HeaderStyle HorizontalAlign="Center" Font-Bold="true"/>
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Descripcion" DataField="DESCRIPCION" UniqueName="DESCRIPCION">
                                <HeaderStyle HorizontalAlign="Center" Font-Bold="true"/>
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridButtonColumn UniqueName="detalle" ButtonType="ImageButton" HeaderText="Extensiones"
                                Exportable="false" CommandName="Select" ImageUrl="~/Images/detalles.ico">
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
        <tr>
            <td align="right">
                <telerik:RadButton runat="server" ID="btnNuevo" Text="Nuevo" OnClick="btnNuevo_Click"></telerik:RadButton>
            </td>
        </tr>
    </tbody>
</table>
</asp:Panel>
<asp:Panel runat="server" ID="myPanel" Visible="false" Width="100%">
    <table width="90%" style="margin:10px auto;">
        <tbody>
            <tr>
                <td colspan="4" align="center">
                    <asp:Label runat="server" ID="lblNuemvoArchivo" Text="Variables de Archivo" Font-Bold="true" ForeColor="Blue"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width:25%" align="center">
                    <asp:Label runat="server" Font-Bold="true" ID="lblCodArchivo" Text="Código Archivo *"></asp:Label>
                </td>
                <td style="width:25%" align="center">
                    <telerik:RadTextBox runat="server" ID="txtCodArchivo" MaxLength="2"></telerik:RadTextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfv_txtCodArchivo" ControlToValidate="txtCodArchivo" Display="None"
                        ErrorMessage="Ingrese el código de archivo" ForeColor="Red" ValidationGroup="G_Guardar">*</asp:RequiredFieldValidator>
                    <asp:ValidatorCalloutExtender runat="server" ID="vce_rfv_txtCodArchivo" TargetControlID="rfv_txtCodArchivo"></asp:ValidatorCalloutExtender>
                </td>
                <td style="width:25%" align="center">
                    <asp:Label runat="server" ID="lblDescripcion" Font-Bold="true" Text="Descripción *"></asp:Label>
                </td>
                <td style="width:25%" align="center">
                    <telerik:RadTextBox runat="server" id="txtDescripcion" MaxLength="255"></telerik:RadTextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfv_txtDescripcion" ControlToValidate="txtDescripcion" Display="None"
                        ErrorMessage="Ingrese una descripción" ForeColor="Red" ValidationGroup="G_Guardar">*</asp:RequiredFieldValidator>
                    <asp:ValidatorCalloutExtender runat="server" ID="vce_rfv_txtDescripcion" TargetControlID="rfv_txtDescripcion"></asp:ValidatorCalloutExtender>                    
                </td>
            </tr>
            <tr>
                <td style="width:25%" align="center">
                    <asp:Label runat="server" ID="lblCantColumnas" Font-Bold="true" Text="Cantidad columnas *"></asp:Label>
                 </td>
                <td style="width:25%" align="center" >
                    <telerik:RadNumericTextBox runat="server" ID="txtNumColumnas" MaxLength="3">
                        <NumberFormat ZeroPattern="n" DecimalDigits="0" GroupSeparator=""></NumberFormat>
                    </telerik:RadNumericTextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfv_txtNumColumnas" ControlToValidate="txtNumColumnas" Display="None"
                        ErrorMessage="Ingrese la cantidad de columnas" ForeColor="Red" ValidationGroup="G_Guardar">*</asp:RequiredFieldValidator>
                    <asp:ValidatorCalloutExtender runat="server" ID="vce_rfv_txtNumColumnas" TargetControlID="rfv_txtNumColumnas"></asp:ValidatorCalloutExtender>
                    <asp:CompareValidator runat="server" ID="cv_txtNumColumnas" ControlToValidate="txtNumColumnas" Display="None"
                        ErrorMessage="La cantidad de columna no puede ser menor a 1" ForeColor="Red" ValidationGroup="G_Guardar"
                        ValueToCompare="0" Operator="GreaterThan">*</asp:CompareValidator>
                    <asp:ValidatorCalloutExtender runat="server" ID="vce_cv_txtNumColumnas" TargetControlID="cv_txtNumColumnas"></asp:ValidatorCalloutExtender>
                </td>
                <td style="width:25%" align="center">
                    <asp:Label runat="server" ID="lblSeparador" Font-Bold="true" Text="Delimitador de columna *"></asp:Label>
                </td>
                <td style="width:25%" align="center">
                    <telerik:RadTextBox runat="server" ID="txtSeparador" MaxLength="1"></telerik:RadTextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfv_txtSeparador" ControlToValidate="txtSeparador" Display="None"
                        ErrorMessage="Ingrese el delimitador" ForeColor="Red" ValidationGroup="G_Guardar">*</asp:RequiredFieldValidator>
                    <asp:ValidatorCalloutExtender runat="server" ID="vce_rfv_txtSeparador" TargetControlID="rfv_txtSeparador"></asp:ValidatorCalloutExtender> 
                </td>
            </tr>
            <tr>
                <td style="width:25%" align="center">
                    <asp:Label runat="server" ID="lblTamCargue" Font-Bold="true" Text="Tamaño Maximo Archivo (MB) *"></asp:Label>
                </td>
                <td style="width:25%" align="center">
                    <telerik:RadNumericTextBox runat="server" ID="txtTamCargue" MaxLength="3">
                        <NumberFormat ZeroPattern="n" DecimalDigits="0" GroupSeparator=""></NumberFormat>
                    </telerik:RadNumericTextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfv_txtTamCargue" ControlToValidate="txtTamCargue" Display="None"
                        ErrorMessage="Ingrese el tamaño maximo de archivo" ForeColor="Red" ValidationGroup="G_Guardar">*</asp:RequiredFieldValidator>
                    <asp:ValidatorCalloutExtender runat="server" ID="vce_rfv_txtTamCargue" TargetControlID="rfv_txtTamCargue"></asp:ValidatorCalloutExtender>
                    <asp:CompareValidator runat="server" ID="cv_txtTamCargue" ControlToValidate="txtTamCargue" Display="None"
                        ErrorMessage="tamaño del archivo no puede ser menor a 1" ForeColor="Red" ValidationGroup="G_Guardar"
                        ValueToCompare="0" Operator="GreaterThan">*</asp:CompareValidator>
                    <asp:ValidatorCalloutExtender runat="server" ID="vce_cv_txtTamCargue" TargetControlID="cv_txtTamCargue"></asp:ValidatorCalloutExtender>
                </td>
                <td style="width:25%" align="center">
                    <asp:Label runat="server" ID="lblRutaArchivo" Font-Bold="true" Text="Ruta Cargue Archivo *"></asp:Label>
                </td>
                <td style="width:25%" align="center">
                    <telerik:RadTextBox runat="server" ID="txtRutaArchivo" MaxLength="255"></telerik:RadTextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfv_txtRutaArchivo" ControlToValidate="txtRutaArchivo" Display="None"
                        ErrorMessage="Ingrese la ruta de cargue de los archivos" ForeColor="Red" ValidationGroup="G_Guardar">*</asp:RequiredFieldValidator>
                    <asp:ValidatorCalloutExtender runat="server" ID="vce_rfv_txtRutaArchivo" TargetControlID="rfv_txtRutaArchivo"></asp:ValidatorCalloutExtender> 
                </td>
            </tr>
            <tr>
                <td style="width:25%" align="center">                    
                    <asp:Label runat="server" ID="lblLngMinimaNom" Font-Bold="true" Text="Longitud Minima Nombre"></asp:Label>
                </td>
                <td style="width:25%" align="center">
                    <telerik:RadNumericTextBox runat="server" ID="txtLngMininaNom" MaxLength="3">
                        <NumberFormat ZeroPattern="n" DecimalDigits="0" GroupSeparator=""></NumberFormat>
                    </telerik:RadNumericTextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfv_txtLngMininaNom" ControlToValidate="txtLngMininaNom" Display="None"
                        ErrorMessage="Ingrese el tamaño minimo del nombre de archivo" ForeColor="Red" ValidationGroup="G_Guardar">*</asp:RequiredFieldValidator>
                    <asp:ValidatorCalloutExtender runat="server" ID="vce_rfv_txtLngMininaNom" TargetControlID="rfv_txtLngMininaNom"></asp:ValidatorCalloutExtender>
                    <asp:CompareValidator runat="server" ID="cv_txtLngMininaNom" ControlToValidate="txtLngMininaNom" Display="None"
                        ErrorMessage="longitud minima del nombre de archivo no puede ser menor a 1" ForeColor="Red" ValidationGroup="G_Guardar"
                        ValueToCompare="0" Operator="GreaterThan">*</asp:CompareValidator>
                    <asp:ValidatorCalloutExtender runat="server" ID="vce_cv_txtLngMininaNom" TargetControlID="cv_txtLngMininaNom"></asp:ValidatorCalloutExtender>
                </td>
                <td style="width:25%" align="center">
                    <asp:Label runat="server" ID="lblLngMaximaNom" Font-Bold="true" Text="Longitud Maxima Nombre"></asp:Label>
                </td>
                <td style="width:25%" align="center">
                    <telerik:RadNumericTextBox runat="server" ID="txtLngMaximaNom" MaxLength="3">
                        <NumberFormat ZeroPattern="n" DecimalDigits="0" GroupSeparator=""></NumberFormat>
                    </telerik:RadNumericTextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfv_txtLngMaximaNom" ControlToValidate="txtLngMaximaNom" Display="None"
                        ErrorMessage="Ingrese el tamaño maximo del nombre de archivo" ForeColor="Red" ValidationGroup="G_Guardar">*</asp:RequiredFieldValidator>
                    <asp:ValidatorCalloutExtender runat="server" ID="vce_rfv_txtLngMaximaNom" TargetControlID="rfv_txtLngMaximaNom"></asp:ValidatorCalloutExtender>
                    <asp:CompareValidator runat="server" ID="cv_txtLngMaximaNom" ControlToValidate="txtLngMaximaNom" Display="None" 
                        ErrorMessage="Longitud maxima del nombre no puede ser menor que la minima" ForeColor="Red" ControlToCompare="txtLngMininaNom"
                        ValidationGroup="G_Guardar" Operator="GreaterThanEqual">*</asp:CompareValidator>
                    <asp:ValidatorCalloutExtender runat="server" ID="vce_cv_txtLngMaximaNom" TargetControlID="cv_txtLngMaximaNom"></asp:ValidatorCalloutExtender>
                    <asp:CompareValidator runat="server" ID="vc1_txtLngMaximaNom" ControlToValidate="txtLngMaximaNom" Display="None"
                        ErrorMessage="longitud maxima del nombre de archivo no puede ser menor a 1" ForeColor="Red" ValidationGroup="G_Guardar"
                        ValueToCompare="0" Operator="GreaterThan">*</asp:CompareValidator>
                    <asp:ValidatorCalloutExtender runat="server" ID="vce_vc1_txtLngMaximaNom" TargetControlID="vc1_txtLngMaximaNom"></asp:ValidatorCalloutExtender>
                </td>
            </tr>
            <tr>
                <td style="width:25%" align="center">
                    <asp:Label runat="server" ID="lblCargueObligatorio" Font-Bold="true" Text="Cargue Obligatorio"></asp:Label>
                </td>
                <td style="width:25%" align="left">
                    <asp:CheckBox runat="server" ID="chObligatorio" Checked="true" Text=""/>
                </td>
                <td style="width:50%" align="right" colspan="2"> 
                    <telerik:RadButton runat="server" ID="btnCancelar" Text="Cancelar" CausesValidation="false" OnClick="btnCancelar_Click"></telerik:RadButton>
                    <telerik:RadButton runat="server" Text="Guardar" ID="btnGuardar" ValidationGroup="G_Guardar" OnClick="btnGuardar_Click"></telerik:RadButton>
                </td>
            </tr>
        </tbody>
    </table>
    <br />
</asp:Panel>
<telerik:RadWindowManager runat="server" ID="RadWindowManager1"></telerik:RadWindowManager>
</asp:Content>
