<%@ Page Title="Archivos Programados" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ResultadoValidacionArchivos.aspx.cs" Inherits="RipsValidadorWeb.CargaArchivos.ResultadoValidacionArchivos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        ARCHIVOS PROGRAMADOS
    </h2>
    <br />
   <div class="CamposMarco">
	    <table width="100%">
	        <tr>
		        <td colspan="2" style="width:100%" align="center">
		            <asp:Label ID="titulo" runat="server" Text="Resumen Programación" Font-Bold="true" ForeColor="Black"></asp:Label>  
		        </td>
	        </tr>
	        <tr>
		        <td style="width:50%; height:25px" align="center">
		            <asp:Label id="lbl_numCargue" Runat="server" Font-Bold="True" ForeColor="Black">No. de Cargue:</asp:Label>
		        </td>
		        <td style="width:50%; height:25px" align="center">
		            <asp:Label id="lbl_desc_numCargue" Runat="server" Font-Bold="True" ForeColor="#0033CC"></asp:Label>&nbsp;
		        </td>
	        </tr>
	        <tr>
		        <td style="width:50%; height:25px" align="center">  
		            <asp:Label id="lblPerFactura" Runat="server" Font-Bold="True" ForeColor="Black">Periodo Facturado:</asp:Label>
		        </td>
		        <td style="width:50%; height:25px" align="center">
		            <asp:Label id="lbl_desc_factura" Runat="server" Font-Bold="True" ForeColor="#0033CC"></asp:Label>    
		        </td>
	        </tr>
	        <tr>
		        <td style="width:50%; height:25px" align="center">  
		            <asp:Label id="lbl_estado" Runat="server" Font-Bold="True" ForeColor="Black">Estado Programación:</asp:Label>
		        </td>
		        <td style="width:50%; height:25px" align="center">
		            <asp:Label id="lbl_desc_estado" Runat="server" Font-Bold="True" ForeColor="#0033CC"></asp:Label>    
		        </td>
	        </tr>
	        <tr>
		        <td style="width:50%; height:25px" align="center">  
		            <asp:Label id="lblCantArchProcesados" Runat="server" Font-Bold="True" ForeColor="Black">Total Archivos Procesados:</asp:Label>
		        </td>
		        <td style="width:50%; height:25px" align="center">
		            <asp:Label id="lblDescCantArchProcesados" Runat="server" Font-Bold="True" ForeColor="#0033CC"></asp:Label>    
		        </td>
	        </tr>
	        <tr>
		        <td style="width:50%; height:25px" align="center">  
		            <asp:Label id="lblCantArchError" Runat="server" Font-Bold="True" ForeColor="Black">Archivos con error:</asp:Label>
		        </td>
		        <td style="width:50%; height:25px" align="center">
		            <asp:Label id="lblDescCantArchError" Runat="server" Font-Bold="True" ForeColor="#0033CC"></asp:Label>    
		        </td>
	        </tr>
	        <tr>
		        <td style="width:50%; height:25px" align="center">  
		            <asp:Label id="lblArchValidos" Runat="server" Font-Bold="True" ForeColor="Black">Archivos Validos:</asp:Label>
		        </td>
		        <td style="width:50%; height:25px" align="center">
		            <asp:Label id="lblDescArchValidos" Runat="server" Font-Bold="True" ForeColor="#0033CC"></asp:Label>    
		        </td>
	        </tr>
	    </table>
    </div>
    <div style="margin:10px auto; width:90%;">
        <telerik:RadGrid runat="server" ID="rgArchivosProgramacion" AllowPaging="true" ShowFooter="true" ShowHeader="true"
            AutoGenerateColumns="false" AllowSorting="true" ShowStatusBar="true" Width="100%"
            OnPreRender="rgArchivosProgramacion_PreRender" OnNeedDataSource="rgArchivosProgramacion_NeedDataSource" OnItemCommand="rgArchivosProgramacion_ItemCommand" Culture="es-CO">
            <MasterTableView CommandItemDisplay="Top" Width="100%" InsertItemPageIndexAction="ShowItemOnLastPage"
                DataKeyNames="id_programacion, consecutivo, cod_archivo, cod_estado_archivo">
                <EditFormSettings>
                    <PopUpSettings Modal="true" />
                </EditFormSettings>
                <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="true"/>
                <Columns>
                    <telerik:GridBoundColumn UniqueName="nombre_archivo" DataField="nombre_archivo" HeaderText="Archivo">
                        <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                        <ItemStyle HorizontalAlign="Center"/>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="tipo_archivo" DataField="tipo_archivo" HeaderText="Tipo de archivo">
                        <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                        <ItemStyle HorizontalAlign="Center"/>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="estado_validacion" DataField="estado_validacion" HeaderText="Estado">
                        <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                        <ItemStyle HorizontalAlign="Center"/>
                    </telerik:GridBoundColumn>
                    <telerik:GridButtonColumn UniqueName="detalles"  HeaderText="Detalles" ButtonType="ImageButton" CommandName="Select" Exportable="false" ImageUrl="~/Images/detalles.ico">
                        <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                        <ItemStyle HorizontalAlign="Center"/>
                    </telerik:GridButtonColumn>                                                     
                </Columns>
            </MasterTableView>
        </telerik:RadGrid> 
    </div>
    <div style="margin:10px 50px 0 0; width:10%; text-align:right; float:right">
        <telerik:RadButton runat="server" ID="btnAtras" Text="Atras" OnClick="btnAtras_Click"></telerik:RadButton>
    </div>
    <telerik:RadWindowManager runat="server" ID="RadWindowManager1"></telerik:RadWindowManager>
</asp:Content>
