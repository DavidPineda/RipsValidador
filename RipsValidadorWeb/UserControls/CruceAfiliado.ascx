<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CruceAfiliado.ascx.cs" Inherits="RipsValidadorWeb.UserControls.CruceAfiliado" %>
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
                    <telerik:RadTextBox runat="server" ID="txtDescripcion" MaxLength="100" Width="95%" Text='<%#DataBinder.Eval(Container, "DataItem.descripcion")%>'></telerik:RadTextBox>                
                    <asp:RequiredFieldValidator runat="server" ID="rfv_txtDescripcion" ControlToValidate="txtDescripcion"
                        Display="None" ForeColor="Red" SetFocusOnError="true" ValidationGroup="G_Guardar"
                        ErrorMessage="Ingrese una descripcion">*</asp:RequiredFieldValidator>
                    <asp:ValidatorCalloutExtender runat="server" ID="vce_rfv_txtDescripcion" TargetControlID="rfv_txtDescripcion"></asp:ValidatorCalloutExtender>
                </td>
                <td style="width:25%;">
                    <div style="text-align:center">
                        <asp:Label runat="server" ID="lblPrioridad" Font-Bold="true">Prioridad:</asp:Label>
                    </div>
                </td>
                <td style="width:25%;">
                    <telerik:RadNumericTextBox runat="server" ID="txtPrioridad" MaxLength="3" Width="20%">
                        <NumberFormat ZeroPattern="n" DecimalDigits="0" GroupSeparator=""></NumberFormat>
                    </telerik:RadNumericTextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfv_txtPrioridad" ControlToValidate="txtPrioridad"
                        Display="None" ForeColor="Red" SetFocusOnError="true" ValidationGroup="G_Guardar"
                        ErrorMessage="Ingrese una prioridad">*</asp:RequiredFieldValidator>
                    <asp:ValidatorCalloutExtender runat="server" ID="vce_rfv_txtPrioridad" TargetControlID="rfv_txtPrioridad"></asp:ValidatorCalloutExtender>
                </td>
            </tr>
            <tr>
                <td style="width:25%;">
                    <div style="text-align:center">
                        <asp:Label runat="server" ID="lblEstado" Font-Bold="true">Estado Cruce:</asp:Label>
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
                <td colspan="2" style="width:50%;">
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