<%@ Page Title="Encabezado Grupo Variables Dependientes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DependFieldGroup.aspx.cs" Inherits="RipsValidadorWeb.Parametrizacion.DependFieldGroup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .columna{
            height:25px;
            text-align:center;
            font-weight:bold;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadAjaxManager runat="server" ID="RadAjaxManager1">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ddlTipoArchivo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ddlNumColumna"/>
                    <telerik:AjaxUpdatedControl ControlID="myPanel1"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ddlNumColumna">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="myPanel1"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <h2>
        Agrupación de variables dependintes
    </h2>
    <table style="width: 50%; margin: 20px 0px 30px 30px; display:block;">
        <tbody>
            <tr>
                <td style="width:100%; text-align:left; color:blue; height:25px; margin-left:25px;" colspan="7">
                    <asp:Label runat="server" ID="lblTituloFiltro">Filtro de Consulta>></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width:40%;" class="columna">
                    <asp:Label runat="server" ID="lblTipoArchivo">Tipo De Archivo:</asp:Label>
                </td>
                <td style="width:60%;">
                    <telerik:RadDropDownList runat="server" ID="ddlTipoArchivo" DropDownWidth="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoArchivo_SelectedIndexChanged"></telerik:RadDropDownList>
                    <asp:RequiredFieldValidator runat="server" ID="rfv_ddlTipoArchivo" ControlToValidate="ddlTipoArchivo" Display="None"
                        ErrorMessage="Seleccione un tipo de archivo" ForeColor="Red"
                        ValidationGroup="G_Guardar" InitialValue="SELECCIONE>>">*</asp:RequiredFieldValidator>
                    <asp:ValidatorCalloutExtender runat="server" ID="vce_rfv_ddlTipoArchivo" TargetControlID="rfv_ddlTipoArchivo"></asp:ValidatorCalloutExtender>
                </td>
            </tr>
            <tr>
                <td style="width:40%;" class="columna">
                    <asp:Label runat="server" ID="lblColumna">Columna:</asp:Label>
                </td>
                <td style="width:60%;">
                    <telerik:RadDropDownList runat="server" ID="ddlNumColumna" DropDownWidth="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlNumColumna_SelectedIndexChanged"></telerik:RadDropDownList>
                    <asp:RequiredFieldValidator runat="server" ID="rfv_ddlNumColumna" ControlToValidate="ddlNumColumna" Display="None"
                        ErrorMessage="Seleccione una columna" ForeColor="Red"
                        ValidationGroup="G_Guardar" InitialValue="SELECCIONE>>">*</asp:RequiredFieldValidator>
                    <asp:ValidatorCalloutExtender runat="server" ID="vce_rfv_ddlNumColumna" TargetControlID="rfv_ddlNumColumna"></asp:ValidatorCalloutExtender>
                </td>
            </tr>
        </tbody>
    </table>
    <asp:Panel runat="server" ID="myPanel1" Width="100%" Visible="false">
        <div style="margin:10px auto; width:80%">
            <telerik:RadGrid runat="server" ID="rgEncabezadoGrupo" AllowPaging="true" ShowFooter="true" ShowHeader="true"
                AutoGenerateColumns="false" AllowSorting="true" ShowStatusBar="true" OnUpdateCommand="rgEncabezadoGrupo_UpdateCommand" OnInsertCommand="rgEncabezadoGrupo_InsertCommand"
                OnNeedDataSource="rgEncabezadoGrupo_NeedDataSource" OnPreRender="rgEncabezadoGrupo_PreRender" OnItemCommand="rgEncabezadoGrupo_ItemCommand" PageSize="5"
                OnDeleteCommand="rgEncabezadoGrupo_DeleteCommand" Culture="es-CO">
                <MasterTableView CommandItemDisplay="Top" InsertItemPageIndexAction="ShowItemOnLastPage" 
                    DataKeyNames="id_enc_grupo, cod_archivo, cod_estado" Name="Encabezado">
                    <EditFormSettings>
                        <PopUpSettings Modal="true" />
                    </EditFormSettings>
                    <CommandItemSettings ShowAddNewRecordButton="true" ShowRefreshButton="true"/>
                    <Columns>
                        <telerik:GridEditCommandColumn UniqueName="EditCommandColumn" ButtonType="ImageButton" HeaderText="Modificar" EditImageUrl="~/Images/editar.ico" Exportable="false">  
                            <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                            <ItemStyle HorizontalAlign="Center"/>
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn UniqueName="nom_archivo" DataField="nom_archivo" HeaderText="Archivo">
                            <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                            <ItemStyle HorizontalAlign="Center"/>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="nombre_columna" DataField="nombre_columna" HeaderText="Columna">
                            <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                            <ItemStyle HorizontalAlign="Center"/>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="descripcion" DataField="descripcion" HeaderText="Descripcion">
                            <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                            <ItemStyle HorizontalAlign="Center"/>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="desc_estado" DataField="desc_estado" HeaderText="Estado">
                            <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                            <ItemStyle HorizontalAlign="Center"/>
                        </telerik:GridBoundColumn>
                        <telerik:GridButtonColumn UniqueName="detalle" ButtonType="ImageButton" HeaderText="Detalles"
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
                    <EditFormSettings UserControlName="../UserControls/GroupField.ascx" EditFormType="WebUserControl">
                        <PopUpSettings Modal="true"/>
                        <EditColumn UniqueName="EditCommandColumn1" ButtonType="ImageButton">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
            </telerik:RadGrid>
        </div>
    </asp:Panel>
    <telerik:RadWindowManager runat="server" ID="RadWindowManager1"></telerik:RadWindowManager>
</asp:Content>
