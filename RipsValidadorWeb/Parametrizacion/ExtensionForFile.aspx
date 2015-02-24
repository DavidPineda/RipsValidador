<%@ Page Title="Extensiones Por Archivo" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ExtensionForFile.aspx.cs" 
    Inherits="RipsValidadorWeb.Parametrizacion.ExtensionForFile" MaintainScrollPositionOnPostback="true"%>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
            <telerik:RadScriptBlock runat="server" ID="scriptBlock">
            <script type="text/javascript">
                //<![CDATA[
                function onRowDropping(sender, args) {
                    if (sender.get_id() == "<%=rgExtensiones.ClientID%>") {
                        var node = args.get_destinationHtmlElement();
                        if (!isChildOf('<%=rgExtArchivo.ClientID %>', node) && !isChildOf('<%=rgExtensiones.ClientID%>', node)) {
                            args.set_cancel(true);
                        }
                    }
                    else {
                        var node = args.get_destinationHtmlElement();
                        if (!isChildOf('trashCan', node)) {
                            args.set_cancel(true);
                        }
                        else {
                            if (confirm("¿Esta seguro que desea eliminar el submenú.?"))
                                args.set_destinationHtmlElement($get('trashCan'));
                            else
                                args.set_cancel(true);
                        }
                    }
                }

                function isChildOf(parentId, element) {
                    while (element) {
                        if (element.id && element.id.indexOf(parentId) > -1) {
                            return true;
                        }
                        element = element.parentNode;
                    }
                    return false;
                }
                //]]>
            </script>
        </telerik:RadScriptBlock>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>
        Extensiones Validas Por Archivo
    </h1>
    <telerik:RadAjaxPanel runat="server" ID="RadAjaxPanel1" Width="98%">
    <asp:Panel runat="server" ID="myPanel4" Width="100%">
        <div style="width:45%; float:left; display:inline; margin:10px 0px 10px 20px;">
            <h4>
                Extensiones Disponibles
            </h4>
            <div style="margin:10px auto;">
                <telerik:RadGrid runat="server" ID="rgExtensiones" AllowPaging="true" ShowFooter="false" ShowHeader="true"
                    AutoGenerateColumns="false" AllowSorting="true" ShowStatusBar="true" OnDeleteCommand="rgExtensiones_DeleteCommand"
                    OnInsertCommand="rgExtensiones_InsertCommand" OnUpdateCommand="rgExtensiones_UpdateCommand" OnRowDrop="rgExtensiones_RowDrop"
                    OnNeedDataSource="rgExtensiones_NeedDataSource" OnPreRender="rgExtensiones_PreRender" Width="95%" Culture="es-CO">
                    <MasterTableView CommandItemDisplay="Top" Width="100%" InsertItemPageIndexAction="ShowItemOnLastPage" DataKeyNames="value">
                        <CommandItemSettings ShowAddNewRecordButton="true" ShowRefreshButton="true"/>
                        <Columns>
                            <telerik:GridEditCommandColumn UniqueName="EditCommandColumn" ButtonType="ImageButton" HeaderText="Modificar" EditImageUrl="~/Images/editar.ico" Exportable="false">  
                                <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                                <ItemStyle HorizontalAlign="Center"/>
                            </telerik:GridEditCommandColumn>                            
                            <telerik:GridBoundColumn HeaderText="Extension" DataField="text" UniqueName="text">
                                <HeaderStyle HorizontalAlign="Center" Font-Bold="true"/>
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Descripcion" DataField="descripcion" UniqueName="descripcion">
                                <HeaderStyle HorizontalAlign="Center" Font-Bold="true"/>
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridButtonColumn UniqueName="deleteRegister" ConfirmText="Borrar este registro" ConfirmDialogType="RadWindow" HeaderText="Eliminar"
                                ConfirmTitle="Eliminar" ButtonType="ImageButton" CommandName="Delete" Exportable="false" ImageUrl="~/Images/eliminar.ico">
                                <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                                <ItemStyle HorizontalAlign="Center"/>
                            </telerik:GridButtonColumn>
                        </Columns>
                        <EditFormSettings UserControlName="../UserControls/ExtensionAdd.ascx" EditFormType="WebUserControl">
                            <PopUpSettings Modal="true"/>
                            <EditColumn UniqueName="EditCommandColumn" ButtonType="ImageButton">
                            </EditColumn>
                        </EditFormSettings>
                    </MasterTableView>
                    <ClientSettings AllowRowsDragDrop="True" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="false"></Selecting>
                        <ClientEvents OnRowDropping="onRowDropping"></ClientEvents>
                    </ClientSettings>
                </telerik:RadGrid>
            </div>
        </div>
        <div style="width:45%; float:right; display:inline; margin:10px 20px 10px 0px;">
            <h4>
                Extensiones para archivo: 
                <asp:Label runat="server" ID="lblTipoArchivo" Text=""></asp:Label>
            </h4>
            <div style="margin:10px auto;">
                <telerik:RadGrid runat="server" ID="rgExtArchivo" AllowPaging="true" ShowFooter="false" ShowHeader="true"
                    AutoGenerateColumns="false" AllowSorting="true" ShowStatusBar="true" OnDeleteCommand="rgExtArchivo_DeleteCommand"
                    OnNeedDataSource="rgExtArchivo_NeedDataSource" OnPreRender="rgExtArchivo_PreRender" Width="95%" Culture="es-CO">
                    <MasterTableView CommandItemDisplay="Top" Width="100%" InsertItemPageIndexAction="ShowItemOnLastPage" DataKeyNames="id_extension, cod_archivo">
                        <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="true"/>
                        <Columns>                       
                            <telerik:GridBoundColumn HeaderText="extension" DataField="extension" UniqueName="extension">
                                <HeaderStyle HorizontalAlign="Center" Font-Bold="true"/>
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Descripcion" DataField="Descripcion" UniqueName="Descripcion">
                                <HeaderStyle HorizontalAlign="Center" Font-Bold="true"/>
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridButtonColumn UniqueName="deleteRegister" ConfirmText="Borrar este registro" ConfirmDialogType="RadWindow" HeaderText="Eliminar"
                                ConfirmTitle="Eliminar" ButtonType="ImageButton" CommandName="Delete" Exportable="false" ImageUrl="~/Images/eliminar.ico">
                                <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                                <ItemStyle HorizontalAlign="Center"/>
                            </telerik:GridButtonColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </div>
            <div style="float:right;">
                <telerik:RadButton runat="server" id="btnCancelar" CausesValidation="false" Text="Retornar" OnClick="btnCancelar_Click"></telerik:RadButton>
            </div>
        </div>
    </asp:Panel>
    <telerik:RadWindowManager runat="server" ID="RadWindowManager1"></telerik:RadWindowManager>
    </telerik:RadAjaxPanel>
</asp:Content>
