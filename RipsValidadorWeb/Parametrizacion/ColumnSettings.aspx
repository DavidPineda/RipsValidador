<%@ Page Title="Ajustes de Columnas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ColumnSettings.aspx.cs" 
    Inherits="RipsValidadorWeb.Parametrizacion.ColumnSettings" MaintainScrollPositionOnPostback="true"%>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        function onPopUpShowing(sender, args) {
            args.get_popUp().className += " popUpEditForm";
        }
    </script>
</telerik:RadCodeBlock>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <telerik:RadAjaxManager runat="server" ID="RadAjaxManager1">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ddlTipoArchivo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ddlNumColumna" />
                    <telerik:AjaxUpdatedControl ControlID="rgValPermitido" />
                    <telerik:AjaxUpdatedControl ControlID="myPanel3" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ddlNumColumna">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="myPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="myPanel3" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <h2>
        Ajuste de Variables(Columnas)
    </h2>
    <br />
    <asp:Panel runat="server" ID="myPanel1" Width="100%"> 
        <table width="60%" style="margin-left:25px;">
            <tbody>
                <tr>
                    <td style="width:20%;" align="center">
                        <asp:Label runat="server" ID="lblTipoArchivo" Font-Bold="true" Text="Tipo De Archivo:"></asp:Label>
                    </td>
                    <td style="width:30%;">
                        <telerik:RadDropDownList runat="server" ID="ddlTipoArchivo" AutoPostBack="true" DropDownHeight="100px" OnSelectedIndexChanged="ddlTipoArchivo_SelectedIndexChanged"></telerik:RadDropDownList>
                    </td>
                    <td style="width:20%;" align="center">
                        <asp:Label runat="server" ID="lblNumColumna" Font-Bold="true" Text="Número de Columna"></asp:Label>
                    </td>
                    <td style="width:30%;">
                        <telerik:RadDropDownList runat="server" ID="ddlNumColumna" AutoPostBack="true" DropDownHeight="100px" OnSelectedIndexChanged="ddlNumColumna_SelectedIndexChanged"></telerik:RadDropDownList>
                    </td>
                </tr>
            </tbody>
        </table>
    </asp:Panel>
    <br />
    <asp:Panel runat="server" ID="myPanel3" Visible="false" Width="100%">
        <table width="60%" style="margin:10px auto;">
            <tbody>
                <tr>
                    <td style="width:17%;">
                        <asp:Label runat="server" ID="lblNomColumna" Font-Bold="true">Nombre Columna:</asp:Label>
                    </td>
                    <td style="width:17%;">
                        <asp:Label runat="server" ID="lblTextNomColumna" Text=""></asp:Label>
                    </td>
                    <td style="width:17%;">
                        <asp:Label runat="server" ID="lblLngMinima" Font-Bold="true">Longitud Minima:</asp:Label>
                    </td>
                    <td style="width:17%;">
                        <asp:Label runat="server" ID="lblTextLngMinima" Text=""></asp:Label>
                    </td>
                    <td style="width:17%;">
                        <asp:Label runat="server" ID="lblLngMaxima" Font-Bold="true">Longitud Maxima:</asp:Label>
                    </td>
                    <td style="width:15%;">
                        <asp:Label runat="server" ID="lblTextLngMaxima" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width:17%;">
                        <asp:Label runat="server" ID="lblTipoDato" Font-Bold="true">Tipo de Dato:</asp:Label>
                    </td>
                    <td style="width:17%;">
                        <asp:Label runat="server" ID="lblTextTipoDato" Text=""></asp:Label>
                    </td>
                    <td style="width:17%;">
                        <asp:Label runat="server" ID="lblEstado" Font-Bold="true">Estado:</asp:Label>
                    </td>
                    <td style="width:17%;">
                        <asp:Label runat="server" ID="lblTextEstado" Text=""></asp:Label>
                    </td>
                    <td style="width:17%;">
                        <asp:Label runat="server" ID="lblFormatoFecha" Font-Bold="true">Formato Fecha:</asp:Label>
                    </td>
                    <td style="width:15%;">
                        <asp:Label runat="server" ID="lblTextFormatoFecha" Text=""></asp:Label>
                    </td>
                </tr>
            </tbody>
        </table>
    </asp:Panel>
    <br />
    <asp:Panel runat="server" id="myPanel2" Width="100%" Visible="true">
        <table width="60%" style="margin:10px auto">
            <tbody>
                <tr>
                    <td align="center">
                        <asp:Label runat="server" ID="lblValPermitidos" Font-Bold="true" ForeColor="Blue" Text="Valores Permitidos"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <telerik:RadGrid runat="server" ID="rgValPermitido" AllowPaging="true" ShowFooter="true" ShowHeader="true"
                            AutoGenerateColumns="false" AllowSorting="true" ShowStatusBar="true" Width="95%" PageSize="5"
                            OnPreRender="rgValPermitido_PreRender" OnNeedDataSource="rgValPermitido_NeedDataSource" 
                            OnUpdateCommand="rgValPermitido_UpdateCommand" OnInsertCommand="rgValPermitido_InsertCommand" OnDeleteCommand="rgValPermitido_DeleteCommand" Culture="es-CO">
                            <MasterTableView CommandItemDisplay="Top" Width="100%" InsertItemPageIndexAction="ShowItemOnLastPage" DataKeyNames="ID_VAL_PERMITIDO, COD_ARCHIVO, NUMERO_COLUMNA">
                                <EditFormSettings>
                                    <PopUpSettings Modal="true" />
                                </EditFormSettings>
                                <CommandItemSettings ShowAddNewRecordButton="true" ShowRefreshButton="true"/>
                                <Columns>
                                    <telerik:GridEditCommandColumn UniqueName="EditCommandColumn" ButtonType="ImageButton" EditImageUrl="~/Images/editar.ico" Exportable="false">  
                                    </telerik:GridEditCommandColumn>
                                    <telerik:GridBoundColumn UniqueName="valor" DataField="valor" HeaderText="valor permitido">
                                        <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="Center"/>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="descripcion" DataField="descripcion" HeaderText="Descripcion">
                                        <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="Center"/>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="desc_tipo_valor" DataField="desc_tipo_valor" HeaderText="Tipo Valor">
                                        <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="Center"/>
                                    </telerik:GridBoundColumn>                                      
                                    <telerik:GridButtonColumn UniqueName="deleteRegister" ConfirmText="Borrar este registro" ConfirmDialogType="RadWindow" HeaderText="Eliminar"
                                        ConfirmTitle="Eliminar" ButtonType="ImageButton" CommandName="Delete" Exportable="false" ImageUrl="~/Images/eliminar.ico">
                                        <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="Center"/>
                                    </telerik:GridButtonColumn>
                                </Columns>
                                <EditFormSettings UserControlName="../UserControls/DataStructFile.ascx" EditFormType="WebUserControl">
                                    <PopUpSettings Modal="true"/>
                                    <EditColumn UniqueName="EditCommandColumn1" ButtonType="ImageButton">
                                    </EditColumn>
                                </EditFormSettings>
                            </MasterTableView>
                        </telerik:RadGrid>    
                    </td>
                </tr>
            </tbody>
        </table>
    </asp:Panel>
    <telerik:RadWindowManager runat="server" id="RadWindowManager1"></telerik:RadWindowManager>
</asp:Content>
