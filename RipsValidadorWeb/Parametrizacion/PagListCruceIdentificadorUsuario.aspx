<%@ Page Title="Consultar Cruces" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PagListCruceIdentificadorUsuario.aspx.cs" Inherits="RipsValidadorWeb.Parametrizacion.PagListCruceIdentificadorUsuario" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <h2>
        CONSULTA DE CRUCES DE IDENTIFICACION DE AFILIADOS
    </h2>
     <br />
    <div style="width:90%; margin:10px auto; display:block;">        
        <telerik:RadGrid ID="radGridCruces" runat="server" AllowPaging="true" ShowFooter="true" ShowHeader="true" AllowMultiRowSelection="false"
            AutoGenerateColumns="false" AllowSorting="true" ShowStatusBar="true" OnNeedDataSource="radGridCruces_NeedDataSource" Culture="es-CO"
            OnUpdateCommand="radGridCruces_UpdateCommand" OnDeleteCommand="radGridCruces_DeleteCommand" 
            OnPreRender="radGridCruces_PreRender" OnItemCommand="radGridCruces_ItemCommand" OnInsertCommand="radGridCruces_InsertCommand">
                <MasterTableView CommandItemDisplay="Top" InsertItemPageIndexAction="ShowItemOnLastPage" DataKeyNames="id, estado">
                    <EditFormSettings>
                        <PopUpSettings Modal="true" />
                    </EditFormSettings>
                    <CommandItemSettings ShowAddNewRecordButton="true" ShowRefreshButton="true"/>
                    <Columns>
                        <telerik:GridEditCommandColumn  UniqueName="UpdateRegister" ButtonType="ImageButton" HeaderText="Modificar" Exportable="false" EditImageUrl="~/Images/editar.ico">
                            <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                            <ItemStyle HorizontalAlign="Center"/>
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn UniqueName="descripcion" DataField="descripcion" HeaderText="Descripcion">
                            <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                            <ItemStyle HorizontalAlign="Center"/>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="prioridad" DataField="prioridad" HeaderText="Prioridad">
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
                    <EditFormSettings UserControlName="../UserControls/CruceAfiliado.ascx" EditFormType="WebUserControl">
                        <PopUpSettings Modal="true"/>
                        <EditColumn UniqueName="EditCommandColumn1" ButtonType="ImageButton">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
        </telerik:RadGrid>    
    </div>
    <telerik:RadWindowManager runat="server" id="RadWindowManager1"></telerik:RadWindowManager>
</asp:Content>
