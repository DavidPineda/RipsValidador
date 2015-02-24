<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CruceAfiliadoDet.ascx.cs" Inherits="RipsValidadorWeb.UserControls.CruceAfiliadoDet" %>
<asp:Panel runat="server" ID="myPanel" Width="100%">
    <table style="width:80%; margin:10px auto;">
        <tbody>
            <tr>
                <td style="width:25%;">
                    <div style="text-align:center">
                        <asp:Label runat="server" ID="lblColumna" Font-Bold="true">Columna:</asp:Label>
                    </div>
                </td>
                <td style="width:25%;">
                    <telerik:RadDropDownList runat="server" ID="ddlColumna"></telerik:RadDropDownList>                
                </td>
                <td style="width:25%;">
                    <div style="text-align:center">
                        <asp:Label runat="server" ID="lblEstado" Font-Bold="true">Estado:</asp:Label>
                    </div>
                </td>
                <td style="width:25%;">
                    <telerik:RadDropDownList runat="server" ID="ddlEstado">
                        <Items>
                            <telerik:DropDownListItem Value="1" Text="Activo" />
                            <telerik:DropDownListItem Value="0" Text="Inactivo" />
                        </Items>
                    </telerik:RadDropDownList>          
                </td>
            </tr>
            <tr>
                <td colspan="4" style="width:50%;">
                    <div style="float:right; height:auto;">
                        <telerik:RadButton runat="server" ID="btnCancelar" Text="Cancelar" CausesValidation="false" CommandName="Cancel"></telerik:RadButton>
                        <telerik:RadButton ID="btnModificar" Text="Modificar" ValidationGroup="G_Guardar" runat="server" CommandName="Update" Visible='<%# !(DataItem is Telerik.Web.UI.GridInsertionObject) %>'></telerik:RadButton>
                        <telerik:RadButton ID="btnInsertar" Text="Guardar" ValidationGroup="G_Guardar" runat="server" CommandName="PerformInsert" Visible='<%# DataItem is Telerik.Web.UI.GridInsertionObject %>'></telerik:RadButton>
                    </div>
                </td>
            </tr>
        </tbody>
    </table>
</asp:Panel>