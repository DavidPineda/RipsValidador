<%@ Page Title="Detalle validacion Archivos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ResultadoValidacionArchivosDetalle.aspx.cs" Inherits="RipsValidadorWeb.CargaArchivos.ResultadoValidacionArchivosDetalle" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        td
        {
            font-size:14px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        RESULTADO DETALLE ARCHIVO
    </h2>
    <br />
    <br />
    <div class="CamposMarco">
	    <table width="100%">
	        <tr>
		        <td colspan="2" style="width:100%" align="center">
		            <asp:Label ID="titulo" runat="server" Text="Resumen Cargue" Font-Bold="true" ForeColor="Black"></asp:Label>  
		        </td>
	        </tr>
	        <tr>
		        <td style="width:50%; height:25px" align="center">
		            <asp:Label id="lbl_numCargue" Runat="server" Font-Bold="True" ForeColor="Black">No. de Cargue:</asp:Label>
		        </td>
		        <td style="width:50%; height:25px" align="center">
		            <asp:Label id="lbl_desc_numCargue" Runat="server" Font-Bold="True" ForeColor="#0033CC"></asp:Label>
		        </td>
	        </tr>
	        <tr>
		        <td style="width:50%; height:25px" align="center">  
		            <asp:Label id="lbl_nomArchivo" Runat="server" Font-Bold="True" ForeColor="Black">Nom. Archivo:</asp:Label>
		        </td>
		        <td style="width:50%; height:25px" align="center">
		            <asp:Label id="lbl_desc_nomArchivo" Runat="server" Font-Bold="True" ForeColor="#0033CC"></asp:Label>    
		        </td>
	        </tr>
	        <tr>
		        <td style="width:50%; height:25px" align="center">  
		            <asp:Label id="lbl_estado" Runat="server" Font-Bold="True" ForeColor="Black">Estado Archivo:</asp:Label>
		        </td>
		        <td style="width:50%; height:25px" align="center">
		            <asp:Label id="lbl_desc_estado" Runat="server" Font-Bold="True" ForeColor="#0033CC"></asp:Label>    
		        </td>
	        </tr>
	        <tr>
		        <td style="width:50%; height:25px" align="center">  
		            <asp:Label id="lblCantRegTotales" Runat="server" Font-Bold="True" ForeColor="Black">Registros Totales:</asp:Label>
		        </td>
		        <td style="width:50%; height:25px" align="center">
		            <asp:Label id="lblDescCantRegTotales" Runat="server" Font-Bold="True" ForeColor="#0033CC"></asp:Label>    
		        </td>
	        </tr>
	        <tr>
		        <td style="width:50%; height:25px" align="center">  
		            <asp:Label id="lblRegValidos" Runat="server" Font-Bold="True" ForeColor="Black">Registros Validos:</asp:Label>
		        </td>
		        <td style="width:50%; height:25px" align="center">
		            <asp:Label id="lblDescRegValidos" Runat="server" Font-Bold="True" ForeColor="#0033CC"></asp:Label>    
		        </td>
	        </tr>
	        <tr>
		        <td style="width:50%; height:25px" align="center">  
		            <asp:Label id="lblRegError" Runat="server" Font-Bold="True" ForeColor="Black">Registros Con Errores:</asp:Label>
		        </td>
		        <td style="width:50%; height:25px" align="center">
		            <asp:Label id="lblDescRegError" Runat="server" Font-Bold="True" ForeColor="#0033CC"></asp:Label>    
		        </td>
	        </tr>
	    </table>
    </div>
    <div style="margin:10px auto; width:90%;">
        <telerik:RadGrid runat="server" ID="rgErroresEnArchivo" AllowPaging="true" ShowFooter="true" ShowHeader="true"
            AutoGenerateColumns="false" AllowSorting="true" ShowStatusBar="true" Width="100%"
            OnPreRender="rgErroresEnArchivo_PreRender" OnNeedDataSource="rgErroresEnArchivo_NeedDataSource" Culture="es-CO">
            <MasterTableView CommandItemDisplay="Top" Width="100%" InsertItemPageIndexAction="ShowItemOnLastPage">
                <EditFormSettings>
                    <PopUpSettings Modal="true" />
                </EditFormSettings>
                <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="true"/>
                <Columns>
                    <telerik:GridBoundColumn UniqueName="columna" DataField="columna" HeaderText="N° Columna">
                        <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                        <ItemStyle HorizontalAlign="Center"/>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="desc_error" DataField="desc_error" HeaderText="Error">
                        <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                        <ItemStyle HorizontalAlign="Center"/>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="tipo_error" DataField="tipo_error" HeaderText="Tipo de error">
                        <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                        <ItemStyle HorizontalAlign="Center"/>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="cantidad" DataField="cantidad" HeaderText="Cantidad Errores">
                        <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                        <ItemStyle HorizontalAlign="Center"/>
                    </telerik:GridBoundColumn>                                                             
                </Columns>
            </MasterTableView>
        </telerik:RadGrid> 
    </div>
    <div style="margin:10px 50px 0 0; width:18%; text-align:right; float:right">
        <telerik:RadButton runat="server" ID="btnDescargar" Text="Descargar" Visible="false" OnClick="btnDescargar_Click"></telerik:RadButton>
        <telerik:RadButton runat="server" ID="btnAtras" Text="Atras" OnClick="btnAtras_Click"></telerik:RadButton>
    </div>
    <telerik:RadWindowManager runat="server" ID="RadWindowManager1"></telerik:RadWindowManager>
</asp:Content>
