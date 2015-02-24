<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ErrorMessage.ascx.cs" Inherits="RipsValidadorWeb.UserControls.ErrorMessage" %>
<style type="text/css">
    .auto-style1 {
        width: 50%;
        height: 26px;
    }
</style>
<asp:Panel runat="server" ID="myPanel" Width="100%">
    <table style="width:80%; margin:10px auto;">
        <tbody>
            <tr>
                <td style="width:25%;">
                    <div style="text-align:center">
                        <asp:Label runat="server" ID="lblMensajeError" Font-Bold="true">Mensaje de Error:</asp:Label>
                    </div>
                </td>
                <td style="width:25%;">
                    <telerik:RadTextBox runat="server" ID="txtMensaje" MaxLength="25" Text='<%#DataBinder.Eval(Container, "DataItem.mensaje")%>'></telerik:RadTextBox>                
                </td>
            </tr>
            <tr>
                <td style="width:25%;">
                    <div style="text-align:center">
                        <asp:Label runat="server" ID="lblEstado" Font-Bold="true">Estado Validación:</asp:Label>
                    </div>
                </td>
                <td style="width:75%;">
                    <telerik:RadDropDownList runat="server" ID="ddlEstado"></telerik:RadDropDownList>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">
                </td>
                <td class="auto-style1">
                    <div style="float:right; height:auto;">
                        <telerik:RadButton runat="server" ID="btnCancelar" Text="Cancelar" CausesValidation="false" CommandName="Cancel"></telerik:RadButton>
                        <telerik:RadButton ID="btnModificar" Text="Modificar" ValidationGroup="M_Modificar" runat="server" CommandName="Update" Visible='<%# !(DataItem is Telerik.Web.UI.GridInsertionObject) %>'></telerik:RadButton>
<%--                        <telerik:RadButton ID="btnInsertar" Text="Guardar" ValidationGroup="G_Guardar" runat="server" CommandName="PerformInsert" Visible='<%# DataItem is Telerik.Web.UI.GridInsertionObject %>'></telerik:RadButton>--%>
                    </div>
                </td>
            </tr>
        </tbody>
    </table>
</asp:Panel>
