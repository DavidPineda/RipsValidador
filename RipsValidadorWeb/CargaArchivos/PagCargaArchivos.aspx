<%@ Page Title="Carga De Archivos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PagCargaArchivos.aspx.cs" 
    Inherits="RipsValidadorWeb.CargaArchivos.PagCargaArchivos" MaintainScrollPositionOnPostback="true"%>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

<script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>

<script type="text/javascript">
    filesWithError = 0;

    function OnClientFileSelected(sender, args) {
        var RegExPattern = "^(AF|AP|AC|AM|AT|US|AN|AH|AU|CT)[0-9]{1,6}.TXT$";
        var currentFileName = args.get_fileName().toUpperCase();
        var row = args.get_row();
        var index = args.get_rowIndex();
        var upload = $find("<%=MyRadAsyncUpload.ClientID %>");
        if (!currentFileName.match(RegExPattern)) {
            row.className = "ruError";
            $("#" + row.id + " span span").addClass("ruUploadProgress ruUploadFailure");
            filesWithError = filesWithError + 1;
        }        
    }

    function OnClientFilesUploaded(sender) {
        if (filesWithError > 0) {
            var button2 = $find("<%=btnProgramar.ClientID%>");
            button2.set_enabled(false);
        }
    }

    function OnClientFileUploadRemoved(sender, args) {
        var RegExPattern = "^(AF|AP|AC|AM|AT|US|AN|AH|AU|CT)[0-9]{1,6}.TXT$";
        var currentFileName = args.get_fileName().toUpperCase();
        if (!currentFileName.match(RegExPattern)) {
            filesWithError = filesWithError - 1;
        }
        if (filesWithError <= 0) {
            var button2 = $find("<%=btnProgramar.ClientID%>");
            button2.set_enabled(true);
        }
    }

    function RadAsyncUpload1_FileUploadFailed(sender, args) {
        alert("Fallo el proceso de cargue del archivo.");
        var button2 = $find("<%=btnProgramar.ClientID%>");
            button2.set_enabled(false);
    }

    function RadAsyncUpload1_ValidationFailed(sender, args) {
        alert("Error, la extension del archivo es incorrecta, o se supero el maximo de MB");
    }
</script>

    <style type="text/css">
table.RadCalendarMonthView input
{
    background-image: none;
}
 
td.rcButtons input
{
    margin: 0 3px;
    padding: 2px 4px;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadAjaxManager runat="server" ID="RadAjaxManager">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ddlAno">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ddlMes"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <h2>
        CARGA Y PROGRAMACION DE ARCHIVOS RIPS - RESOLUCION 3374 DE 2000
    </h2>
    <asp:Panel runat="server" ID="myPanel1" Width="90%">
    <table width="90%" style="margin:5% auto;">
        <tbody>
            <tr>
                <td style="width:10%; text-align:left; height:25px">
                </td>
                <td style="width:15%; text-align:left; height:25px">
                    <div class="labelTitulo"><label>Regional de Cargue:</label></div>
                </td>
                <td style="width:75%; text-align:left; float:left; height:25px" colspan="2">
                    <telerik:RadDropDownList runat="server" ID="ddlRegional"></telerik:RadDropDownList>
                    <asp:RequiredFieldValidator runat="server" ID="rfv_ddlRegional" ControlToValidate="ddlRegional"
                        ErrorMessage="Seleccione una regional" ForeColor="Red" Display="None"
                        InitialValue="SELECCIONE>>" ValidationGroup="P_Programar">*</asp:RequiredFieldValidator>
                    <asp:ValidatorCalloutExtender runat="server" ID="vce_rfv_ddlRegional" TargetControlID="rfv_ddlRegional"></asp:ValidatorCalloutExtender>
                </td>
            </tr>
            <tr>
                <td style="width:10%; text-align:left; height:25px">
                </td>
                <td style="width:15%; text-align:left; height:25px">
                    <div class="labelTitulo"><label>Tipo de Contrato:</label></div>
                </td>
                <td style="width:75%; text-align:left; float:left; height:25px" colspan="2">
                    <telerik:RadDropDownList runat="server" ID="ddlContrato"></telerik:RadDropDownList>
                    <asp:RequiredFieldValidator runat="server" ID="rfv_ddlContrato" ControlToValidate="ddlContrato"
                        ErrorMessage="Seleccione un tipo de contrato" ForeColor="Red" Display="None"
                        InitialValue="SELECCIONE>>" ValidationGroup="P_Programar">*</asp:RequiredFieldValidator>
                    <asp:ValidatorCalloutExtender runat="server" ID="vce_rfv_ddlContrato" TargetControlID="rfv_ddlContrato"></asp:ValidatorCalloutExtender>
                </td>
            </tr>
            <tr>
                <td style="width:10%; text-align:left; height:25px">
                </td>
                <td style="width:15%; text-align:left; height:25px">
                    <div class="labelTitulo"><label>Periodo de Factura:</label></div>
                </td>
                <td style="width:75%; text-align:left; float:left; height:25px" colspan="2">
                    <table width="100%">
                        <tbody>
                            <tr>
                                <td style="width:10%;">
                                    <asp:Label runat="server" ID="lblAno" Font-Bold="true">Año:</asp:Label>
                                </td>
                                <td style="width:25%;">
                                    <telerik:RadDropDownList runat="server" ID="ddlAno" AutoPostBack="true" OnSelectedIndexChanged="ddlAno_SelectedIndexChanged"></telerik:RadDropDownList>
                                </td>
                                <td style="width:10%;">
                                    <asp:Label runat="server" ID="lblMes" Font-Bold="true">Mes:</asp:Label>
                                </td>
                                <td style="width:25%;">
                                    <telerik:RadDropDownList runat="server" ID="ddlMes"></telerik:RadDropDownList>
                                </td>
                                <td style="width:30%;"></td>
                            </tr>
                        </tbody>
                    </table>                                                          
                </td>
            </tr>

            <tr>
                <td style="width:100%; height:25px" colspan="4">
                </td>
            </tr>
            <tr>
                <td style="width:10%; text-align:left; height:25px">
                </td>
                <td style="width:15%; text-align:center; height:25px">
                    <div class="labelTitulo">
                        <asp:Label runat="server" ID="lblTamMaximo" ForeColor="Red"></asp:Label>
                    </div>
                </td>
                <td style="width:25%; text-align:left; float:left; height:25px">
                </td>
                <td style="width:50%; text-align:left; height:25px">
                </td>
            </tr>
            <tr>
                <td style="width:10%; text-align:left; height:25px">
                </td>
                <td style="width:90%; text-align:center; height:25px" colspan="3">
                    <div class="labelTitulo">
                        <telerik:RadAsyncUpload runat="server" ID="MyRadAsyncUpload"
                            onclientvalidationfailed="RadAsyncUpload1_ValidationFailed" 
                            onclientfileuploadfailed="RadAsyncUpload1_FileUploadFailed" 
                            OnClientFileSelected="OnClientFileSelected"
                            OnClientFilesUploaded="OnClientFilesUploaded"
                            OnClientFileUploadRemoved="OnClientFileUploadRemoved"
                            MultipleFileSelection="Automatic"
                            Width="100%" Culture="es-CO">
                        </telerik:RadAsyncUpload>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="width:10%; text-align:left; height:25px"></td>
                <td style="width:90%; text-align:center; height:25px" colspan="3">
                    <telerik:RadProgressManager runat="server" ID="RadProgressManager1" />
				    <telerik:RadProgressArea ID="RadProgressArea1" Runat="server" Width="410px" 
					    HeaderText="Porcentaje de carga" Language="Español" Culture="es-CO">
				    </telerik:RadProgressArea>
                </td>
            </tr>
            <tr>
                <td colspan="4" style="height:25px" align="right">
                    <telerik:RadButton runat="server" ID="btnProgramar" CausesValidation="false" Text="Programar" OnClick="btnProgramar_Click" ValidationGroup="P_Programar"></telerik:RadButton>
                </td>
            </tr>
        </tbody>
    </table>
    </asp:Panel>  
    <telerik:RadWindowManager runat="server" ID="RadWindowManager1"></telerik:RadWindowManager>
</asp:Content>

