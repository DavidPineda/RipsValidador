<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GroupField.ascx.cs" Inherits="RipsValidadorWeb.UserControls.GroupField" %>
<asp:Panel runat="server" ID="myPanel" Width="100%">
    <table style="width:80%; margin:10px auto;">
        <tbody>
            <tr>
                <td style="width:25%;">
                    <div style="text-align:center">
                        <asp:Label runat="server" ID="lblDescripcion" Font-Bold="true">Descripcion:</asp:Label>
                    </div>
                </td>
                <td style="width:25%;">
                    <telerik:RadTextBox runat="server" ID="txtDescripcion" MaxLength="250" Text='<%#DataBinder.Eval(Container, "DataItem.descripcion")%>'></telerik:RadTextBox>                    
                </td>
                <td style="width:25%;">
                    <div style="text-align:center">
                        <asp:Label runat="server" ID="lblEstado" Font-Bold="true">Estado:</asp:Label>
                    </div>
                </td>
                <td style="width:25%;">
                    <telerik:RadDropDownList runat="server" ID="ddlEstado" DropDownHeight="50px"></telerik:RadDropDownList>
                </td>
            </tr>
            <tr>
                <td style="width:50%;" colspan="2">
                </td>
                <td style="width:50%;" colspan="2">
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