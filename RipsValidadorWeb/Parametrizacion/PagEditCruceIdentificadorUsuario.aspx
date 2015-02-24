<%@ Page Title="Detalles Cruce" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PagEditCruceIdentificadorUsuario.aspx.cs" Inherits="RipsValidadorWeb.Parametrizacion.PagEditCruceIdentificadorUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <h2>
        DETALLE CRUCE IDENTIFICACION AFILIADO
    </h2>
     <br />
    <div style="width:90%; margin:10px auto; display:block;">        
        <telerik:RadGrid ID="rgEditCruces" runat="server" AllowPaging="true" ShowFooter="true" ShowHeader="true" AllowMultiRowSelection="false"
            AutoGenerateColumns="false" AllowSorting="true" ShowStatusBar="true" OnNeedDataSource="radGridCruces_NeedDataSource" Culture="es-CO"
            OnUpdateCommand="radGridCruces_UpdateCommand" OnDeleteCommand="radGridCruces_DeleteCommand" 
            OnPreRender="radGridCruces_PreRender" OnInsertCommand="radGridCruces_InsertCommand">
                <MasterTableView CommandItemDisplay="Top" InsertItemPageIndexAction="ShowItemOnLastPage" DataKeyNames="id_cruce_afiliado, id_cruce_columna">
                    <EditFormSettings>
                        <PopUpSettings Modal="true" />
                    </EditFormSettings>
                    <CommandItemSettings ShowAddNewRecordButton="true" ShowRefreshButton="true"/>
                    <Columns>
                        <telerik:GridEditCommandColumn UniqueName="UpdateRegister" ButtonType="ImageButton" HeaderText="Modificar" Exportable="false" EditImageUrl="~/Images/editar.ico">
                            <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                            <ItemStyle HorizontalAlign="Center"/>
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn UniqueName="desc_columna" DataField="desc_columna" HeaderText="Columna">
                            <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                            <ItemStyle HorizontalAlign="Center"/>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="desc_estado" DataField="desc_estado" HeaderText="Estado">
                            <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                            <ItemStyle HorizontalAlign="Center"/>
                        </telerik:GridBoundColumn>
                        <telerik:GridButtonColumn UniqueName="deleteRegister" ConfirmText="Borrar este registro" ConfirmDialogType="RadWindow" HeaderText="Eliminar"
                            ConfirmTitle="Eliminar" ButtonType="ImageButton" CommandName="Delete" Exportable="false" ImageUrl="~/Images/eliminar.ico">
                            <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                            <ItemStyle HorizontalAlign="Center"/>
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="../UserControls/CruceAfiliadoDet.ascx" EditFormType="WebUserControl">
                        <PopUpSettings Modal="true"/>
                        <EditColumn UniqueName="EditCommandColumn1" ButtonType="ImageButton">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
        </telerik:RadGrid>    
    </div>
    <div style="width:90%; margin:10px auto; display:block; text-align:right;">
        <telerik:RadButton runat="server" ID="btnCancelar" CausesValidation="false" Text="Atras" OnClick="btnCancelar_Click"></telerik:RadButton>
    </div>
    <telerik:RadWindowManager runat="server" id="RadWindowManager1"></telerik:RadWindowManager>
</asp:Content>
