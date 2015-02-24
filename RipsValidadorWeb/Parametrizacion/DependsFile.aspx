<%@ Page Title="Archivos Dependientes En Cargue" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DependsFile.aspx.cs" 
    Inherits="RipsValidadorWeb.Parametrizacion.dependsFile" MaintainScrollPositionOnPostback="true"%>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<telerik:RadScriptBlock runat="server" ID="scriptBlock">
    <script type="text/javascript">
        //<![CDATA[
        function onRowDropping(sender, args) {
            if (sender.get_id() == "<%=rgArchivosParametrizados.ClientID%>") {
                var node = args.get_destinationHtmlElement();
                if (!isChildOf('<%=rgArchivosDependientes.ClientID %>', node) && !isChildOf('<%=rgArchivosParametrizados.ClientID%>', node)) {
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
        Archivos Dependientes en Carga de Archivos
    </h1>
    <telerik:RadAjaxPanel runat="server" ID="RadAjaxPanel1" Width="98%">
    <asp:Panel runat="server" ID="myPanel4" Width="100%">
        <div style="width:45%; float:left; display:inline; margin:10px 0px 10px 20px;">
            <h4>
                Archivos Parametrizados
            </h4>
            <div style="margin:10px auto;">
                <telerik:RadGrid runat="server" ID="rgArchivosParametrizados" AllowPaging="true" ShowFooter="false" ShowHeader="true"
                    AutoGenerateColumns="false" AllowSorting="true" ShowStatusBar="true" OnRowDrop="rgArchivosParametrizados_RowDrop"
                    OnNeedDataSource="rgArchivosParametrizados_NeedDataSource" OnPreRender="rgArchivosParametrizados_PreRender" Width="95%" Culture="es-CO">
                    <MasterTableView CommandItemDisplay="Top" Width="100%" InsertItemPageIndexAction="ShowItemOnLastPage" DataKeyNames="cod_archivo">
                        <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="true"/>
                        <Columns>                
                            <telerik:GridBoundColumn HeaderText="Archivo" DataField="cod_archivo" UniqueName="cod_archivo">
                                <HeaderStyle HorizontalAlign="Center" Font-Bold="true"/>
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Nombre" DataField="descripcion" UniqueName="descripcion">
                                <HeaderStyle HorizontalAlign="Center" Font-Bold="true"/>
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                        </Columns>
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
                Archivos dependientes 
            </h4>
            <br />
            <asp:Label runat="server" ID="lblTipoArchivo" Font-Bold="true">Tipo De Archivo: </asp:Label>
            <telerik:RadDropDownList runat="server" ID="ddlTipoArchivo" DropDownHeight="100px" DropDownWidth="100%" Width="70%" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoArchivo_SelectedIndexChanged"></telerik:RadDropDownList>
            <div style="margin:10px auto;">
                <telerik:RadGrid runat="server" ID="rgArchivosDependientes" AllowPaging="true" ShowFooter="false" ShowHeader="true"
                    AutoGenerateColumns="false" AllowSorting="true" ShowStatusBar="true" OnDeleteCommand="rgArchivosDependientes_DeleteCommand"
                    OnNeedDataSource="rgArchivosDependientes_NeedDataSource" OnPreRender="rgArchivosDependientes_PreRender" Width="95%" Culture="es-CO">
                    <MasterTableView CommandItemDisplay="Top" Width="100%" InsertItemPageIndexAction="ShowItemOnLastPage" DataKeyNames="cod_archivo, cod_archivo_dep">
                        <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="true"/>
                        <Columns>                       
                            <telerik:GridBoundColumn HeaderText="Archivo Dependiente" DataField="descripcion_archivo_dep" UniqueName="descripcion_archivo_dep">
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
                <telerik:RadButton runat="server" id="btnRetornar" Text="Retornar" OnClick="btnRetornar_Click"></telerik:RadButton>
            </div>
        </div>
    </asp:Panel>
    <telerik:RadWindowManager runat="server" ID="RadWindowManager1"></telerik:RadWindowManager>
    </telerik:RadAjaxPanel>
</asp:Content>
