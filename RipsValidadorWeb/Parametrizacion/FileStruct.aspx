<%@ Page Title="Estructura de Archivo" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FileStruct.aspx.cs" 
    Inherits="RipsValidadorWeb.Parametrizacion.FileStruct" MaintainScrollPositionOnPostback="true"%>
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
            <telerik:AjaxSetting AjaxControlID="ddlTipoArchivo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="myPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="myPanel3" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnAgregar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="myPanel3" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnCancelar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="myPanel3" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ddlTipoDato">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtRangoIni" />
                    <telerik:AjaxUpdatedControl ControlID="rfv_txtRangoIni" />
                    <telerik:AjaxUpdatedControl ControlID="txtRangoFin" />
                    <telerik:AjaxUpdatedControl ControlID="rfv_txtRangoIni" />
                    <telerik:AjaxUpdatedControl ControlID="ddlFormatoFecha" />
                    <telerik:AjaxUpdatedControl ControlID="rfv_ddlFormatoFecha" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="myPanel2"/>
                    <telerik:AjaxUpdatedControl ControlID="myPanel3"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rgEstructura">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="myPanel3" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <h2>
        Estructura de Archivo
    </h2>
    <br />
    <asp:Panel runat="server" ID="myPanel1" Width="100%"> 
        <table width="50%" style="margin-left:25px;">
            <tbody>
                <tr>
                    <td style="width:40%;" align="center">
                        <asp:Label runat="server" ID="lblTipoArchivo" Font-Bold="true" Text="Tipo De Archivo:"></asp:Label>
                    </td>
                    <td style="width:60%;">
                        <telerik:RadDropDownList runat="server" ID="ddlTipoArchivo" Width="100%" DropDownWidth="100%" DropDownHeight="200px" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoArchivo_SelectedIndexChanged"></telerik:RadDropDownList>
                    </td>
                </tr>
            </tbody>
        </table>
    </asp:Panel>
    <br />
    <asp:Panel runat="server" ID="myPanel2" Width="100%" Visible="false">
        <table width="90%" style="margin:10px auto;">
            <tbody>
                <tr>
                    <td align="center">
                        <asp:Label runat="server" ID="lblSubTitulo1" ForeColor="Blue" Font-Bold="true" Text="Archivos Parametrizados"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width:95%" align="center">
                        <telerik:RadGrid runat="server" ID="rgEstructura" AllowPaging="true" ShowFooter="true" ShowHeader="true"
                            AutoGenerateColumns="false" AllowSorting="true" ShowStatusBar="true" OnItemCommand="rgEstructura_ItemCommand"
                            OnNeedDataSource="rgEstructura_NeedDataSource" Width="95%" PageSize="5" Culture="es-CO">
                            <MasterTableView CommandItemDisplay="None" Width="100%" InsertItemPageIndexAction="ShowItemOnLastPage" DataKeyNames="COD_ARCHIVO, NUMERO_COLUMNA">
                                <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="true"/>
                                <Columns>
                                    <telerik:GridButtonColumn UniqueName="updateRegister" HeaderText="Editar" ButtonType="ImageButton"
                                        CommandName="Update" Exportable="false" ImageUrl="~/Images/editar.ico">
                                        <HeaderStyle HorizontalAlign="Center" Font-Bold="true"/>
                                        <ItemStyle HorizontalAlign="Center"/>
                                    </telerik:GridButtonColumn>
                                    <telerik:GridBoundColumn UniqueName="numero_columna" DataField="numero_columna" HeaderText="Número de Columna">
                                        <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="Center"/>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="nombre_columna" DataField="nombre_columna" HeaderText="Nombre de Columna">
                                        <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="Center"/>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="descripcion" DataField="descripcion" HeaderText="Descripcion">
                                        <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="Center"/>
                                    </telerik:GridBoundColumn>
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
                    <td style="width:100%;" align="right">
                        <telerik:RadButton runat="server" ID="btnAgregar" Text="Agregar" OnClick="btnAgregar_Click"></telerik:RadButton>
                    </td>
                </tr>
            </tbody>
        </table>
    </asp:Panel>
    <br />
    <asp:Panel runat="server" ID="myPanel3" Width="100%" Visible="false">
        <table width="90%" style="margin:10px auto;">
            <tbody>
                <tr>
                    <td align="center" colspan="4">
                        <asp:Label runat="server" ID="lblSubTitulo2" ForeColor="Blue" Font-Bold="true" Text="Estructura de Archivo"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width:25%" align="center">
                        <asp:Label runat="server" ID="lblCodArchivo" Text="Archivo" Font-Bold="true"></asp:Label>
                    </td>
                    <td style="width:25%" align="center">
                        <telerik:RadTextBox runat="server" ID="txtCodArchivo" Enabled="false" MaxLength="2"></telerik:RadTextBox>
                    </td>
                    <td style="width:25%" align="center">
                        <asp:Label runat="server" ID="lblNumColumna" Text="Número de Columna" Font-Bold="true"></asp:Label>
                    </td>
                    <td style="width:25%" align="center">
                        <telerik:RadNumericTextBox runat="server" ID="txtNumColumna" MaxLength="3">
                            <NumberFormat ZeroPattern="n" DecimalDigits="0" GroupSeparator=""></NumberFormat>
                        </telerik:RadNumericTextBox>
                        <asp:RequiredFieldValidator runat="server" ID="rfv_txtNumColumna" ControlToValidate="txtNumColumna" Display="None"
                            ErrorMessage="Ingrese el numero de la columna" ForeColor="Red" ValidationGroup="G_Guardar">*</asp:RequiredFieldValidator>
                        <asp:ValidatorCalloutExtender runat="server" ID="vce_rfv_txtNumColumna" TargetControlID="rfv_txtNumColumna"></asp:ValidatorCalloutExtender>
                        <asp:CompareValidator runat="server" ID="cv_txtNumColumna" ControlToValidate="txtNumColumna" Display="None"
                            ErrorMessage="Ingrese un valor mayor a cero" ForeColor="Red" ValidationGroup="G_Guardar"
                            ValueToCompare="0" Operator="GreaterThan">*</asp:CompareValidator>
                        <asp:ValidatorCalloutExtender runat="server" ID="vce_cv_txtNumColumna" TargetControlID="cv_txtNumColumna"></asp:ValidatorCalloutExtender>
                    </td>
                </tr>
                <tr>
                    <td style="width:25%" align="center">
                        <asp:Label runat="server" ID="lblNomColumna" Text="Nombre de Columna" Font-Bold="true"></asp:Label>
                    </td>
                    <td style="width:25%" align="center">
                        <telerik:RadTextBox runat="server" ID="txtNombreColumna" MaxLength="50"></telerik:RadTextBox>
                        <asp:RequiredFieldValidator runat="server" ID="rfv_txtNombreColumna" ControlToValidate="txtNombreColumna" Display="None"
                            ErrorMessage="Ingrese el nombre de la columna" ForeColor="Red" ValidationGroup="G_Guardar">*</asp:RequiredFieldValidator>
                        <asp:ValidatorCalloutExtender runat="server" ID="vce_rfv_txtNombreColumna" TargetControlID="rfv_txtNombreColumna"></asp:ValidatorCalloutExtender>
                    </td>
                    <td style="width:25%" align="center">
                        <asp:Label runat="server" ID="lblDescripcion" Text="Descripción" Font-Bold="true"></asp:Label>
                    </td>
                    <td style="width:25%" align="center">
                        <telerik:RadTextBox runat="server" ID="txtDescripcion" MaxLength="250"></telerik:RadTextBox>
                        <asp:RequiredFieldValidator runat="server" ID="rfv_txtDescripcion" ControlToValidate="txtDescripcion" Display="None"
                            ErrorMessage="Ingrese la descripcion de la columna" ForeColor="Red" ValidationGroup="G_Guardar">*</asp:RequiredFieldValidator>
                        <asp:ValidatorCalloutExtender runat="server" ID="vce_rfv_txtDescripcion" TargetControlID="rfv_txtDescripcion"></asp:ValidatorCalloutExtender>
                    </td>
                </tr>
                <tr>
                    <td style="width:25%" align="center">
                        <asp:Label runat="server" ID="lblLongitudMin" Text="Longitud Minima" Font-Bold="true"></asp:Label>
                    </td>
                    <td style="width:25%" align="center">
                        <telerik:RadNumericTextBox runat="server" ID="txtLongitudMin" MaxLength="3">
                            <NumberFormat ZeroPattern="n" DecimalDigits="0" GroupSeparator=""></NumberFormat>
                        </telerik:RadNumericTextBox>
                        <asp:RequiredFieldValidator runat="server" ID="rfv_txtLongitudMin" ControlToValidate="txtLongitudMin" Display="None"
                            ErrorMessage="Ingrese la longitud minima" ForeColor="Red" ValidationGroup="G_Guardar">*</asp:RequiredFieldValidator>
                        <asp:ValidatorCalloutExtender runat="server" ID="vce_rfv_txtLongitudMin" TargetControlID="rfv_txtLongitudMin"></asp:ValidatorCalloutExtender>
                        <asp:CompareValidator runat="server" ID="cv_txtLongitudMin" ControlToValidate="txtLongitudMin" Display="None"
                            ErrorMessage="Ingrese un valor mayor a cero" ForeColor="Red" ValidationGroup="G_Guardar"
                            ValueToCompare="0" Operator="GreaterThan" Type="Integer">*</asp:CompareValidator>
                        <asp:ValidatorCalloutExtender runat="server" ID="vce_cv_txtLongitudMin" TargetControlID="cv_txtLongitudMin"></asp:ValidatorCalloutExtender>
                    </td>
                    <td style="width:25%" align="center">
                        <asp:Label runat="server" ID="lblLongitudMax" Text="Longitud Maxima" Font-Bold="true"></asp:Label>
                    </td>
                    <td style="width:25%" align="center">
                        <telerik:RadNumericTextBox runat="server" ID="txtLongitudMax" MaxLength="3">
                            <NumberFormat ZeroPattern="n" DecimalDigits="0" GroupSeparator=""></NumberFormat>
                        </telerik:RadNumericTextBox>
                        <asp:RequiredFieldValidator runat="server" ID="rfv_txtLongitudMax" ControlToValidate="txtLongitudMax" Display="None"
                            ErrorMessage="Ingrese la longitud maxima" ForeColor="Red" ValidationGroup="G_Guardar">*</asp:RequiredFieldValidator>
                        <asp:ValidatorCalloutExtender runat="server" ID="vce_rfv_txtLongitudMax" TargetControlID="rfv_txtLongitudMax"></asp:ValidatorCalloutExtender>
                        <asp:CompareValidator runat="server" ID="cv_txtLongitudMax" Display="None" ControlToValidate="txtLongitudMax"
                            ControlToCompare="txtLongitudMin" ValidationGroup="G_Guardar" ForeColor="Red" Operator="GreaterThanEqual"
                            ErrorMessage="Longitud Maxima debe ser mayor o igual que longitud Minima" Type="Integer">*</asp:CompareValidator>
                        <asp:ValidatorCalloutExtender runat="server" ID="vce_cv_txtLongitudMax" TargetControlID="cv_txtLongitudMax"></asp:ValidatorCalloutExtender>
                        <asp:CompareValidator runat="server" ID="cv1_txtLongitudMax" ControlToValidate="txtLongitudMax" Display="None"
                            ErrorMessage="Ingrese un valor mayor a cero" ForeColor="Red" ValidationGroup="G_Guardar" Type="Integer"
                            ValueToCompare="0" Operator="GreaterThan">*</asp:CompareValidator>
                        <asp:ValidatorCalloutExtender runat="server" ID="cv_cv1_txtLongitudMax" TargetControlID="cv1_txtLongitudMax"></asp:ValidatorCalloutExtender>
                    </td>
                </tr>
                <tr>
                    <td style="width:25%" align="center">
                        <asp:Label runat="server" ID="lblValorRequerido" Text="Valor Requerido" Font-Bold="true"></asp:Label>
                    </td>
                    <td style="width:25%" align="center">
                        <asp:CheckBox runat="server" ID="chValorRequerido" Text="" ToolTip="Indica si el campo debe contener algun dato o puede estar vacio" Checked="true"/>
                    </td>
                    <td style="width:25%" align="center">
                        <asp:Label runat="server" ID="lblValidar" Text="Validar" Font-Bold="true"></asp:Label>
                    </td>
                    <td style="width:25%" align="center">
                        <asp:CheckBox runat="server" ID="chValidar" Text="" ToolTip="Indica si el campo debe ser validado o no" Checked="true"/>
                    </td>
                </tr>
                <tr>
                    <td style="width:25%" align="center">
                        <asp:Label runat="server" ID="lblTipoDato" Text="Tipo de dato" Font-Bold="true"></asp:Label>
                    </td>
                    <td style="width:25%" align="center">
                        <telerik:RadDropDownList runat="server" ID="ddlTipoDato" DropDownHeight="50px" OnSelectedIndexChanged="ddlTipoDato_SelectedIndexChanged" AutoPostBack="true"></telerik:RadDropDownList>
                        <asp:RequiredFieldValidator runat="server" ID="rfv_ddlTipoDato" ControlToValidate="ddlTipoDato" Display="None"
                            ErrorMessage="Seleccione el tipo de dato" ForeColor="Red" ValidationGroup="G_Guardar" InitialValue="SELECCIONE>>">*</asp:RequiredFieldValidator>
                        <asp:ValidatorCalloutExtender runat="server" ID="vce_rfv_ddlTipoDato" TargetControlID="rfv_ddlTipoDato"></asp:ValidatorCalloutExtender>
                    </td>
                    <td style="width:25%" align="center">
                        <asp:Label runat="server" ID="lblCodEstado" Text="Estado" Font-Bold="true"></asp:Label>
                    </td>
                    <td style="width:25%" align="center">
                        <telerik:RadDropDownList runat="server" ID="ddlEstado" DropDownHeight="50px"></telerik:RadDropDownList>
                        <asp:RequiredFieldValidator runat="server" ID="rfv_ddlEstado" ControlToValidate="ddlEstado" Display="None"
                            ErrorMessage="Seleccione el estado" ForeColor="Red" ValidationGroup="G_Guardar" InitialValue="SELECCIONE>>">*</asp:RequiredFieldValidator>
                        <asp:ValidatorCalloutExtender runat="server" ID="vce_rfv_ddlEstado" TargetControlID="rfv_ddlEstado"></asp:ValidatorCalloutExtender>
                    </td>
                </tr>
                <tr>
                    <td style="width:25%" align="center">
                        <asp:Label runat="server" ID="lblRangoIni" Text="Rango Inicial (Utilice (,) como separador decimal)" Font-Bold="true"></asp:Label>
                    </td>
                    <td style="width:25%" align="center">
                        <telerik:RadTextBox runat="server" ID="txtRangoIni" MaxLength="6"></telerik:RadTextBox>
                        <asp:RegularExpressionValidator ID="rev_txtRangoIni" runat="server" 
                            ControlToValidate="txtRangoIni" ErrorMessage="Digite un valor númerico" 
                            ForeColor="Red" ValidationExpression="(\d{1,3}\,\d{1,2})|(\d{1,6})" Display="None" 
                            ValidationGroup="G_Guardar">*</asp:RegularExpressionValidator>
                        <asp:ValidatorCalloutExtender ID="vce_rev_txtRangoIni" runat="server" TargetControlID="rev_txtRangoIni"></asp:ValidatorCalloutExtender>
                        <asp:CompareValidator runat="server" ID="cv_txtRangoIni" ControlToValidate="txtRangoIni" Display="None"
                            ErrorMessage="Ingrese un valor mayor a cero" ForeColor="Red" ValidationGroup="G_Guardar"
                            ValueToCompare="0" Operator="GreaterThan">*</asp:CompareValidator>
                        <asp:ValidatorCalloutExtender runat="server" ID="vce_cv_txtRangoIni" TargetControlID="cv_txtRangoIni"></asp:ValidatorCalloutExtender>
                        <asp:CustomValidator runat="server" id="cv1_txtRangoIni" controltovalidate="txtRangoIni" Display="None" ValidationGroup="G_Guardar" 
                            onservervalidate="cv1_txtRangoIni_ServerValidate" errormessage="Ingrese un rango final" ForeColor="Red">*</asp:CustomValidator>
                        <asp:ValidatorCalloutExtender runat="server" ID="vce_cv1_txtRangoIni" TargetControlID="cv1_txtRangoIni"></asp:ValidatorCalloutExtender>
                    </td>
                    <td style="width:25%" align="center">
                        <asp:Label runat="server" ID="lblRangoFin" Text="Rango Final (Utilice (,) como separador decimal)" Font-Bold="true"></asp:Label>
                    </td>
                    <td style="width:25%" align="center">
                        <telerik:RadTextBox runat="server" ID="txtRangoFin" MaxLength="6"></telerik:RadTextBox>
                        <asp:RegularExpressionValidator ID="rev_txtRangoFin" runat="server" 
                            ControlToValidate="txtRangoFin" ErrorMessage="Digite un valor númerico" 
                            ForeColor="Red" ValidationExpression="(\d{1,3}\,\d{1,2})|(\d{1,6})" Display="None" 
                            ValidationGroup="G_Guardar">*</asp:RegularExpressionValidator>
                        <asp:ValidatorCalloutExtender ID="vce_rev_txtRangoFin" runat="server" TargetControlID="rev_txtRangoFin"></asp:ValidatorCalloutExtender>
                        <asp:CompareValidator runat="server" ID="cv_txtRangoFin" ControlToValidate="txtRangoFin" Display="None"
                            ErrorMessage="Ingrese un valor mayor a cero" ForeColor="Red" ValidationGroup="G_Guardar"
                            ValueToCompare="0" Operator="GreaterThan">*</asp:CompareValidator>
                        <asp:ValidatorCalloutExtender runat="server" ID="vce_cv_txtRangoFin" TargetControlID="cv_txtRangoFin"></asp:ValidatorCalloutExtender>
                        <asp:CompareValidator runat="server" ID="cv1_txtRangoFin" ControlToValidate="txtRangoFin" Display="None"
                            ErrorMessage="Ingrese un valor mayor rango inicial" ForeColor="Red" ValidationGroup="G_Guardar"
                            ControlToCompare="txtRangoIni" Operator="GreaterThan">*</asp:CompareValidator>
                        <asp:ValidatorCalloutExtender runat="server" ID="vce_cv1_txtRangoFin" TargetControlID="cv1_txtRangoFin"></asp:ValidatorCalloutExtender>
                        <asp:CustomValidator runat="server" id="cv2_txtRangoFin" controltovalidate="txtRangoFin" Display="None" ValidationGroup="G_Guardar" 
                            onservervalidate="cv2_txtRangoFin_ServerValidate" errormessage="Ingrese un rango Inicial" ForeColor="Red">*</asp:CustomValidator>
                        <asp:ValidatorCalloutExtender runat="server" ID="vce_cv2_txtRangoFin" TargetControlID="cv2_txtRangoFin"></asp:ValidatorCalloutExtender>
                    </td>
                </tr>
                <tr>
                    <td style="width:25%" align="center">
                        <asp:Label runat="server" ID="lblFormatoFecha" Text="Formato Fecha" Font-Bold="true"></asp:Label>
                    </td>
                    <td style="width:25%" align="center">
                        <telerik:RadDropDownList runat="server" ID="ddlFormatoFecha" DropDownHeight="50px"></telerik:RadDropDownList>
                        <asp:RequiredFieldValidator runat="server" ID="rfv_ddlFormatoFecha" ControlToValidate="ddlFormatoFecha" Display="None"
                            ErrorMessage="Seleccione el formato de fecha" ForeColor="Red" ValidationGroup="G_Guardar" InitialValue="SELECCIONE>>">*</asp:RequiredFieldValidator>
                        <asp:ValidatorCalloutExtender runat="server" ID="vce_rfv_ddlFormatoFecha" TargetControlID="rfv_ddlFormatoFecha"></asp:ValidatorCalloutExtender>
                    </td>
                    <td style="width:25%" align="right" colspan="2">
                        <telerik:RadButton runat="server" ID="btnCancelar" Text="Cancelar" OnClick="btnCancelar_Click" CausesValidation="false"></telerik:RadButton>
                        <telerik:RadButton runat="server" ID="btnGuardar" Text="Guardar" OnClick="btnGuardar_Click" ValidationGroup="G_Guardar" CausesValidation="false"></telerik:RadButton>
                    </td>
                </tr>
            </tbody>
        </table>
    </asp:Panel>
    <telerik:RadWindowManager runat="server" ID="RadWindowManager1"></telerik:RadWindowManager>
    <asp:Panel runat="server" ID="myPanel4" Width="100%" Height="50px"></asp:Panel>
</asp:Content>
