<%@ Page Title="Detalle de Grupos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DependFieldDetails.aspx.cs" 
    Inherits="RipsValidadorWeb.Parametrizacion.DependFieldDetails" MaintainScrollPositionOnPostback="true"%>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
        <telerik:RadScriptBlock runat="server" ID="scriptBlock">
            <script type="text/javascript">
                //<![CDATA[
                function onRowDropping(sender, args) {
                    if (sender.get_id() == "<%=rgDetalles.ClientID%>") {
                        var node = args.get_destinationHtmlElement();
                        if (!isChildOf('<%=rgDependientes.ClientID %>', node) && !isChildOf('<%=rgDetalles.ClientID%>', node)) {
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
    <h2>
        Detalle agrupación variables dependientes
    </h2>
    <div style="width:90%; margin:10px auto; display:block;">
       <telerik:RadAjaxPanel runat="server" id="RadAjaxPanel1" Width="100%">
           <asp:Panel runat="server" ID="myPanel4" Visible="true" Width="100%">
                <h3>
                    Variables Depedientes
                </h3>
                <telerik:RadGrid runat="server" ID="rgDetalles" AllowPaging="true" ShowFooter="true" ShowHeader="true" AllowMultiRowSelection="true"
                    AutoGenerateColumns="false" AllowSorting="true" ShowStatusBar="true" PageSize="5"
                    OnNeedDataSource="rgDetalles_NeedDataSource" OnPreRender="rgDetalles_PreRender" OnRowDrop="rgDetalles_RowDrop" Culture="es-CO">
                    <MasterTableView CommandItemDisplay="None" InsertItemPageIndexAction="ShowItemOnLastPage" 
                        DataKeyNames="id_var_dependiente, cod_archivo_dep, cod_archivo_cru, num_columna_dep, num_columna_cru, id_val_permitido_dep, id_val_permitido_cru, tipo_comparacion_dep, tipo_comparacion_cru, estado_parametrizado">
                        <EditFormSettings>
                            <PopUpSettings Modal="true" />
                        </EditFormSettings>
                        <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="true"/>
                        <Columns>
                            <telerik:GridBoundColumn UniqueName="id_var_dependiente" DataField="id_var_dependiente" HeaderText="Num Variable">
                                <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                                <ItemStyle HorizontalAlign="Center"/>
                            </telerik:GridBoundColumn>
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
                        </Columns>
                    </MasterTableView>
                    <ClientSettings AllowRowsDragDrop="True" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="false"></Selecting>
                        <ClientEvents OnRowDropping="onRowDropping"></ClientEvents>
                    </ClientSettings>
                </telerik:RadGrid>
               <br />
                <h3>
                    Datos del grupo
                </h3>
                <telerik:RadGrid runat="server" ID="rgDependientes" AllowPaging="true" ShowFooter="true" ShowHeader="true" AllowMultiRowSelection="true"
                    AutoGenerateColumns="false" AllowSorting="true" ShowStatusBar="true" PageSize="5" OnUpdateCommand="rgDependientes_UpdateCommand"
                    OnPreRender="RadGrid1_PreRender" OnNeedDataSource="RadGrid1_NeedDataSource" OnDeleteCommand="rgDependientes_DeleteCommand" Culture="es-CO">
                    <MasterTableView CommandItemDisplay="None" InsertItemPageIndexAction="ShowItemOnLastPage" 
                        DataKeyNames="id_enc_grupo, id_grupo, id_var_dependiente">
                        <EditFormSettings>
                            <PopUpSettings Modal="true" />
                        </EditFormSettings>
                        <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="true"/>
                        <Columns>
                            <telerik:GridEditCommandColumn UniqueName="EditCommandColumn" ButtonType="ImageButton" HeaderText="Modificar" EditImageUrl="~/Images/editar.ico" Exportable="false">  
                                <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                                <ItemStyle HorizontalAlign="Center"/>
                            </telerik:GridEditCommandColumn>
                            <telerik:GridBoundColumn UniqueName="id_var_dependiente" DataField="id_var_dependiente" HeaderText="Num Variable">
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
            </asp:Panel>
        </telerik:RadAjaxPanel>
    </div>
    <div style="width:90%; margin:10px auto; display:block; text-align:right;">
        <telerik:RadButton runat="server" ID="btnCancelar" CausesValidation="false" Text="Atras" OnClick="btnCancelar_Click"></telerik:RadButton>
    </div>
    <telerik:RadWindowManager runat="server" ID="RadWindowManager1"></telerik:RadWindowManager>
</asp:Content>
