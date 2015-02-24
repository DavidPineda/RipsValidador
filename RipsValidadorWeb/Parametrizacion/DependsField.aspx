    <%@ Page Title="Ajuste Variables Dependientes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DependsField.aspx.cs" Inherits="RipsValidadorWeb.Parametrizacion.DependsField" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .RadButton {
            float: right !important;
            margin-right: 12px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadScriptBlock runat="server" ID="radScriptBlock1">
        <telerik:RadAjaxManager runat="server" ID="radAjaxManager1">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="ddlTipoArchivoDep">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="ddlColumnaDep"/>
                        <telerik:AjaxUpdatedControl ControlID="myPanel1"/>
                        <telerik:AjaxUpdatedControl ControlID="myPanel4"/>
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="ddlColumnaDep">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="myPanel1"/>
                        <telerik:AjaxUpdatedControl ControlID="btnGuardar"/>
                        <telerik:AjaxUpdatedControl ControlID="myPanel4"/>
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="ddlTipoArchivoCru">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="ddlColumnaCru"/>
                        <telerik:AjaxUpdatedControl ControlID="myPanel2"/>
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="ddlColumnaCru">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="myPanel2"/>
                        <telerik:AjaxUpdatedControl ControlID="btnGuardar"/>
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnGuardar">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="myPanel1"/>
                        <telerik:AjaxUpdatedControl ControlID="myPanel2"/>
                        <telerik:AjaxUpdatedControl ControlID="myPanel4"/>
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
    </telerik:RadScriptBlock>
    <h2>
        Ajuste Variables Dependientes
    </h2>
    <br />
    <div style="width:90%; margin:10px auto; display:block;">
        <div style="width:48%; margin-left:10px; display:inline; float:left;">
            <table width="95%" style="margin:10px auto;">
                <tbody>
                    <tr>
                        <td colspan="2" style="width:100%; text-align:center; height:25px;">
                            <asp:Label runat="server" ID="lblSecDependiente" Font-Bold="true" ForeColor="Blue">Variable Dependiente:</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:40%; text-align:center; height:25px;">
                            <asp:Label runat="server" ID="lblTipArchivoDep" Font-Bold="true">Tipo De Archivo:</asp:Label>
                        </td>
                        <td style="width:60%; height:25px;">
                            <telerik:RadDropDownList runat="server" ID="ddlTipoArchivoDep" DropDownHeight="200px" Width="100%" DropDownWidth="100%"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlTipoArchivoDep_SelectedIndexChanged"></telerik:RadDropDownList>
                            <asp:RequiredFieldValidator runat="server" ID="rfv_ddlTipoArchivoDep" ControlToValidate="ddlTipoArchivoDep" Display="None" ForeColor="Red"
                                ErrorMessage="Seleccione un tipo de archivo" InitialValue="SELECCIONE>>" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                            <asp:ValidatorCalloutExtender runat="server" ID="vce_rfv_ddlTipoArchivoDep" TargetControlID="rfv_ddlTipoArchivoDep"></asp:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:40%; text-align:center; height:25px;">
                            <asp:Label runat="server" ID="lblColumnaDep" Font-Bold="true">Variable:</asp:Label>
                        </td>
                        <td style="width:60%; height:25px;">
                            <telerik:RadDropDownList runat="server" ID="ddlColumnaDep" DropDownHeight="100px"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlColumnaDep_SelectedIndexChanged"></telerik:RadDropDownList>
                            <asp:RequiredFieldValidator runat="server" ID="rfv_ddlColumnaDep" ControlToValidate="ddlColumnaDep" Display="None" ForeColor="Red"
                                ErrorMessage="Seleccione una variable del archivo" InitialValue="SELECCIONE>>" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                            <asp:ValidatorCalloutExtender runat="server" ID="vce_rfv_ddlColumnaDep" TargetControlID="rfv_ddlColumnaDep"></asp:ValidatorCalloutExtender>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div style="width:48%; margin-right:10px; display:inline; float:right;">
            <table width="95%" style="margin:10px auto;">
                <tbody>
                    <tr>
                        <td colspan="2" style="width:100%; text-align:center; height:25px;">
                            <asp:Label runat="server" ID="lblSecCruce" Font-Bold="true" ForeColor="Blue">Variable Cruce:</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:40%; text-align:center; height:25px;">
                            <asp:Label runat="server" ID="lblTipArchivoCru" Font-Bold="true">Tipo De Archivo:</asp:Label>
                        </td>
                        <td style="width:60%; height:25px;">
                            <telerik:RadDropDownList runat="server" ID="ddlTipoArchivoCru" DropDownHeight="200px" Width="100%" DropDownWidth="100%"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlTipoArchivoCru_SelectedIndexChanged"></telerik:RadDropDownList>
                            <asp:RequiredFieldValidator runat="server" ID="rfv_ddlTipoArchivoCru" ControlToValidate="ddlTipoArchivoCru" Display="None" ForeColor="Red"
                                ErrorMessage="Seleccione un tipo de archivo" InitialValue="SELECCIONE>>" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                            <asp:ValidatorCalloutExtender runat="server" ID="vce_rfv_ddlTipoArchivoCru" TargetControlID="rfv_ddlTipoArchivoCru"></asp:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:40%; text-align:center; height:25px;">
                            <asp:Label runat="server" ID="lblColumnaCru" Font-Bold="true">Variable:</asp:Label>
                        </td>
                        <td style="width:60%; height:25px;">
                            <telerik:RadDropDownList runat="server" ID="ddlColumnaCru" DropDownHeight="100px"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlColumnaCru_SelectedIndexChanged"></telerik:RadDropDownList>
                            <asp:RequiredFieldValidator runat="server" ID="rfv_ddlColumnaCru" ControlToValidate="ddlColumnaCru" Display="None" ForeColor="Red"
                                ErrorMessage="Seleccione una variable del archivo" InitialValue="SELECCIONE>>" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                            <asp:ValidatorCalloutExtender runat="server" ID="vce_rfv_ddlColumnaCru" TargetControlID="rfv_ddlColumnaCru"></asp:ValidatorCalloutExtender>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
    <br />
    <div style="width:90%; margin:10px auto; display:block;">
        <div style="width:48%; margin-left:10px; display:inline; float:left;">
            <asp:Panel runat="server" ID="myPanel1" Width="100%" Visible="false">
                <table width="95%" style="margin:10px auto;">
                    <tbody>
                        <tr>
                            <td style="width:40%; text-align:center; height:25px;">
                                <asp:Label ID="lblDescripcionDep" runat="server" Font-Bold="true">Descripcion:</asp:Label>
                            </td>
                            <td style="width:60%; height:25px;">
                                <asp:Label ID="lblTextDescripcionDep" runat="server" Font-Bold="true" ForeColor="Blue" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:40%; text-align:center; height:25px;">
                                <asp:Label ID="lblLngMinimaDep" runat="server" Font-Bold="true">Longitud Minima:</asp:Label>
                            </td>
                            <td style="width:60%; height:25px;">
                                <asp:Label ID="lblTextLngMinimaDep" runat="server" Font-Bold="true" ForeColor="Blue" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:40%; text-align:center; height:25px;">
                                <asp:Label ID="lblLngMaximaDep" runat="server" Font-Bold="true">Longitud Maxima:</asp:Label>
                            </td>
                            <td style="width:60%; height:25px;">
                                <asp:Label ID="lblTextLngMaximaDep" runat="server" Font-Bold="true" ForeColor="Blue" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:40%; text-align:center; height:25px;">
                                <asp:Label ID="lblTipoDatoDep" runat="server" Font-Bold="true">Tipo de Dato:</asp:Label>
                            </td>
                            <td style="width:60%; height:25px;">
                                <asp:Label ID="lblTextTipoDatoDep" runat="server" Font-Bold="true" ForeColor="Blue" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:40%; text-align:center; height:25px;">
                                <asp:Label ID="lblRangoIniDep" runat="server" Font-Bold="true">Rango Inicial:</asp:Label>
                            </td>
                            <td style="width:60%; height:25px;">
                                <asp:Label ID="lblTextRangoIniDep" runat="server" Font-Bold="true" ForeColor="Blue" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:40%; text-align:center; height:25px;">
                                <asp:Label ID="lblRangoFinDep" runat="server" Font-Bold="true">Rango Final:</asp:Label>
                                &nbsp;</td>
                            <td style="width:60%; height:25px;">
                                <asp:Label ID="lblTextRangoFinDep" runat="server" Font-Bold="true" ForeColor="Blue" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:40%; text-align:center; height:25px;">
                                <asp:Label ID="lblFormatoFechaDep" runat="server" Font-Bold="true">Formato Fecha:</asp:Label>
                            </td>
                            <td style="width:60%; height:25px;">
                                <asp:Label ID="lblTextFormatoFechaDep" runat="server" Font-Bold="true" ForeColor="Blue" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:40%; text-align:center; height:25px;">
                                <asp:Label ID="lblValoresAceptadosDep" runat="server" Font-Bold="true">Valores Aceptados:</asp:Label>
                            </td>
                            <td style="width:60%; height:25px;">
                                <telerik:RadDropDownList runat="server" ID="ddlValoresAceptadosDep" DropDownHeight="100px" DropDownWidth="100%" Width="100%"
                                    OnSelectedIndexChanged="ddlValoresAceptadosDep_SelectedIndexChanged" AutoPostBack="true"></telerik:RadDropDownList>
                                <asp:RequiredFieldValidator runat="server" ID="rfv_ddlValoresAceptadosDep" ControlToValidate="ddlValoresAceptadosDep"
                                    ErrorMessage="Seleccione un valor" Display="None" ForeColor="Red" InitialValue="SELECCIONE>>" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>  
                                <asp:ValidatorCalloutExtender runat="server" ID="vce_rfv_ddlValoresAceptadosDep" TargetControlID="rfv_ddlValoresAceptadosDep"></asp:ValidatorCalloutExtender>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:40%; text-align:center; height:25px;">
                                <asp:Label ID="lblOtroValorDep" runat="server" Font-Bold="true" Visible="false">Otro Valor</asp:Label>
                            </td>
                            <td style="width:60%; height:25px;">
                                <telerik:RadTextBox runat="server" ID="txtOtroValorDep" MaxLength="10" Visible="false"></telerik:RadTextBox>
                                <asp:RequiredFieldValidator runat="server" ID="rfv_txtOtroValorDep" ControlToValidate="txtOtroValorDep"
                                    ErrorMessage="Ingrese el valor a validar" Display="None" ForeColor="Red" Enabled="false" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                <asp:ValidatorCalloutExtender runat="server" ID="vce_rfv_txtOtroValorDep" TargetControlID="rfv_txtOtroValorDep"></asp:ValidatorCalloutExtender>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:40%; text-align:center; height:25px;">
                                <asp:Label ID="lblTipoDenpendenciaDep" runat="server" Font-Bold="true">Tipo de Dependencia:</asp:Label>
                            </td>
                            <td style="width:60%; height:25px;">
                                <telerik:RadDropDownList ID="ddlTipoDenpendenciaDep" runat="server" DropDownHeight="100px"></telerik:RadDropDownList>
                                <asp:RequiredFieldValidator runat="server" ID="rfv_ddlTipoDenpendenciaDep" ControlToValidate="ddlTipoDenpendenciaDep"
                                    ErrorMessage="Seleccione un tipo de dependencia" Display="None" ForeColor="Red" InitialValue="SELECCIONE>>" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>                           
                                <asp:ValidatorCalloutExtender runat="server" ID="vce_rfv_ddlTipoDenpendenciaDep" TargetControlID="rfv_ddlTipoDenpendenciaDep"></asp:ValidatorCalloutExtender>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:40%; text-align:center; height:25px;">
                                <asp:Label ID="lblMensaje" runat="server" Font-Bold="true">Mensaje de Error:</asp:Label>
                            </td>
                            <td style="width:60%; height:25px;">
                                <telerik:RadTextBox runat="server" ID="txtMensaje" 
                                    ToolTip="Ingrese un mensaje que se mostrara en caso de no cumplir la validación" MaxLength="250"></telerik:RadTextBox>
                                <asp:RequiredFieldValidator runat="server" ID="rfv_txtMensaje" ControlToValidate="txtMensaje" Display="None"
                                    ErrorMessage="Ingrese el Mensaje de Error" ForeColor="Red" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                <asp:ValidatorCalloutExtender runat="server" ID="vce_rfv_txtMensaje" TargetControlID="rfv_txtMensaje"></asp:ValidatorCalloutExtender>
                            </td>
                        </tr>
                    </tbody>
                </table>                
            </asp:Panel>
        </div>
        <div style="width:48%; margin-right:10px; display:inline; float:right;">
            <asp:Panel runat="server" ID="myPanel2" Width="100%" Visible="false">
                <table width="95%" style="margin:10px auto;">
                    <tbody>
                        <tr>
                            <td style="width:40%; text-align:center; height:25px;">
                                <asp:Label ID="lblDescripcionCru" runat="server" Font-Bold="true">Descripcion:</asp:Label>
                            </td>
                            <td style="width:60%; height:25px;">
                                <asp:Label ID="lblTextDescripcionCru" runat="server" Font-Bold="true" ForeColor="Blue" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:40%; text-align:center; height:25px;">
                                <asp:Label ID="lblLngMinimaCru" runat="server" Font-Bold="true">Longitud Minima:</asp:Label>
                            </td>
                            <td style="width:60%; height:25px;">
                                <asp:Label ID="lblTextLngMinimaCru" runat="server" Font-Bold="true" ForeColor="Blue" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:40%; text-align:center; height:25px;">
                                <asp:Label ID="lblLngMaximaCru" runat="server" Font-Bold="true">Longitud Maxima:</asp:Label>
                            </td>
                            <td style="width:60%; height:25px;">
                                <asp:Label ID="lblTextLngMaximaCru" runat="server" Font-Bold="true" ForeColor="Blue" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:40%; text-align:center; height:25px;">
                                <asp:Label ID="lblTipoDatoCru" runat="server" Font-Bold="true">Tipo de Dato:</asp:Label>
                            </td>
                            <td style="width:60%; height:25px;">
                                <asp:Label ID="lblTextTipoDatoCru" runat="server" Font-Bold="true" ForeColor="Blue" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:40%; text-align:center; height:25px;">
                                <asp:Label ID="lblRangoIniCru" runat="server" Font-Bold="true">Rango Inicial:</asp:Label>
                            </td>
                            <td style="width:60%; height:25px;">
                                <asp:Label ID="lblTextRangoIniCru" runat="server" Font-Bold="true" ForeColor="Blue" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:40%; text-align:center; height:25px;">
                                <asp:Label ID="lblRangoFinCru" runat="server" Font-Bold="true">Rango Final:</asp:Label>
                            </td>
                            <td style="width:60%; height:25px;">
                                <asp:Label ID="lblTextRangoFinCru" runat="server" Font-Bold="true" ForeColor="Blue" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:40%; text-align:center; height:25px;">
                                <asp:Label ID="lblFormatoFechaCru" runat="server" Font-Bold="true">Formato Fecha:</asp:Label>
                            </td>
                            <td style="width:60%; height:25px;">
                                <asp:Label ID="lblTextFormatoFechaCru" runat="server" Font-Bold="true" ForeColor="Blue" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:40%; text-align:center; height:25px;">
                                <asp:Label ID="lblValoresAceptadosCru" runat="server" Font-Bold="true">Valores Aceptados:</asp:Label>
                            </td>
                            <td style="width:60%; height:25px;">
                                <telerik:RadDropDownList ID="ddlValoresAceptadosCru" runat="server" DropDownHeight="100px" DropDownWidth="100%" Width="100%"
                                    OnSelectedIndexChanged="ddlValoresAceptadosCru_SelectedIndexChanged" AutoPostBack="true"></telerik:RadDropDownList>
                                <asp:RequiredFieldValidator runat="server" ID="rfv_ddlValoresAceptadosCru" ControlToValidate="ddlValoresAceptadosCru"
                                    ErrorMessage="Seleccione un valor" Display="None" ForeColor="Red" InitialValue="SELECCIONE>>" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                <asp:ValidatorCalloutExtender runat="server" ID="vce_rfv_ddlValoresAceptadosCru" TargetControlID="rfv_ddlValoresAceptadosCru"></asp:ValidatorCalloutExtender>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:40%; text-align:center; height:25px;">
                                <asp:Label ID="lblOtroValorCru" runat="server" Font-Bold="true" Visible="false">Otro Valor:</asp:Label>
                            </td>
                            <td style="width:60%; height:25px;">
                                <telerik:RadTextBox ID="txtOtroValorCru" runat="server" MaxLength="10" Visible="false"></telerik:RadTextBox>
                                <asp:RequiredFieldValidator runat="server" ID="rfv_txtOtroValorCru" ControlToValidate="txtOtroValorCru"
                                    ErrorMessage="Ingrese el valor a validar" Display="None" ForeColor="Red" Enabled="false" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                <asp:ValidatorCalloutExtender runat="server" ID="vce_rfv_txtOtroValorCru" TargetControlID="rfv_txtOtroValorCru"></asp:ValidatorCalloutExtender>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:40%; text-align:center; height:25px;">
                                <asp:Label ID="lblTipoDenpendenciaCru" runat="server" Font-Bold="true">Tipo de Dependencia:</asp:Label>
                            </td>
                            <td style="width:60%; height:25px;">
                                <telerik:RadDropDownList ID="ddlTipoDenpendenciaCru" runat="server" DropDownHeight="100px"></telerik:RadDropDownList>
                                <asp:RequiredFieldValidator runat="server" ID="rfv_ddlTipoDenpendenciaCru" ControlToValidate="ddlTipoDenpendenciaCru"
                                    ErrorMessage="Seleccione un tipo de dependencia" Display="None" ForeColor="Red" InitialValue="SELECCIONE>>" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                <asp:ValidatorCalloutExtender runat="server" ID="vce_rfv_ddlTipoDenpendenciaCru" TargetControlID="rfv_ddlTipoDenpendenciaCru"></asp:ValidatorCalloutExtender>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:40%; text-align:center; height:25px;"></td>
                            <td style="width:60%; height:25px;"></td>
                        </tr>
                    </tbody>
                </table>
            </asp:Panel>
        </div>
        <div style="float:right; width:30%; height:25px;">
            <telerik:RadButton runat="server" ID="btnGuardar" Text="Agregar" CausesValidation="false" OnClick="btnGuardar_Click" Visible="false" ValidationGroup="Guardar"></telerik:RadButton>
        </div>
    </div>
    <div style="width:90%; margin:32% auto 10px; display:block;">
       <asp:Panel runat="server" ID="myPanel4" Visible="false" Width="100%">
            <h3>
                Variables Depedientes
            </h3>
            <telerik:RadGrid runat="server" ID="rgDependencias" AllowPaging="true" ShowFooter="true" ShowHeader="true" AllowMultiRowSelection="true"
                AutoGenerateColumns="false" AllowSorting="true" ShowStatusBar="true" OnUpdateCommand="rgDependencias_UpdateCommand" OnDeleteCommand="rgDependencias_DeleteCommand"
                OnNeedDataSource="rgEstructura_NeedDataSource" OnPreRender="rgEstructura_PreRender" PageSize="5" Culture="es-CO">
                <MasterTableView CommandItemDisplay="Top" InsertItemPageIndexAction="ShowItemOnLastPage" 
                    DataKeyNames="id_var_dependiente, cod_archivo_dep, cod_archivo_cru, num_columna_dep, num_columna_cru, id_val_permitido_dep, id_val_permitido_cru, tipo_comparacion_dep, tipo_comparacion_cru, estado_parametrizado">
                    <EditFormSettings>
                        <PopUpSettings Modal="true" />
                    </EditFormSettings>
                    <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="true"/>
                    <Columns>
                        <telerik:GridEditCommandColumn UniqueName="EditCommandColumn" ButtonType="ImageButton" HeaderText="Modificar" EditImageUrl="~/Images/editar.ico" Exportable="false">  
                            <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                            <ItemStyle HorizontalAlign="Center"/>
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn UniqueName="desc_archivo_dep" DataField="desc_archivo_dep" HeaderText="Arch. Dependiente">
                            <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                            <ItemStyle HorizontalAlign="Center"/>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="nom_columna_dep" DataField="nom_columna_dep" HeaderText="Col. Dependiente">
                            <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                            <ItemStyle HorizontalAlign="Center"/>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="valor_columna_dep" DataField="valor_columna_dep" HeaderText="Valor Dependiente">
                            <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                            <ItemStyle HorizontalAlign="Center"/>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="desc_tipo_comparacion_dep" DataField="desc_tipo_comparacion_dep" HeaderText="Op. Dependiente">
                            <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                            <ItemStyle HorizontalAlign="Center"/>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="desc_archivo_cru" DataField="desc_archivo_cru" HeaderText="Arch. Cruce">
                            <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                            <ItemStyle HorizontalAlign="Center"/>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="nom_columna_cru" DataField="nom_columna_cru" HeaderText="Col. Cruce">
                            <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                            <ItemStyle HorizontalAlign="Center"/>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="valor_columna_cru" DataField="valor_columna_cru" HeaderText="Valor Cruce">
                            <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                            <ItemStyle HorizontalAlign="Center"/>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="desc_tipo_comparacion_cru" DataField="desc_tipo_comparacion_cru" HeaderText="Op. Cruces">
                            <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                            <ItemStyle HorizontalAlign="Center"/>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="mensaje" DataField="mensaje" HeaderText="Mensaje de Error" Display="false">
                            <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                            <ItemStyle HorizontalAlign="Center"/>
                        </telerik:GridBoundColumn>
                        <telerik:GridButtonColumn UniqueName="deleteRegister" ConfirmText="Borrar este registro" ConfirmDialogType="RadWindow" HeaderText="Eliminar"
                            ConfirmTitle="Eliminar" ButtonType="ImageButton" CommandName="Delete" Exportable="false" ImageUrl="~/Images/eliminar.ico">
                            <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                            <ItemStyle HorizontalAlign="Center"/>
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="../UserControls/ErrorMessage.ascx" EditFormType="WebUserControl">
                        <PopUpSettings Modal="true"/>
                        <EditColumn UniqueName="EditCommandColumn1" ButtonType="ImageButton">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
            </telerik:RadGrid>
           <br />
        </asp:Panel>
    </div>
    <telerik:RadWindowManager runat="server" ID="RadWindowManager1"></telerik:RadWindowManager>
</asp:Content>
