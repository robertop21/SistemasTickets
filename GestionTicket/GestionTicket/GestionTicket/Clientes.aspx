<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Clientes.aspx.cs" Inherits="GestionTicket.Clientes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div>
    
        <table class="auto-style1">
            <tr>
                <td>&nbsp;</td>
                <td class="auto-style2">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td class="auto-style2" style="text-align: left">
                    <asp:Label ID="Label1" runat="server" Text="RUC"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtRuc" runat="server" OnTextChanged="txtRuc_TextChanged" Width="217px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="cmdBuscar" runat="server" OnClick="cmdBuscar_Click" Text="Buscar Cliente" />
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td class="auto-style2" style="text-align: left">
                    <asp:Label ID="Label2" runat="server" Text="RAZON SOCIAL"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="TxtNombre" runat="server" Width="218px"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td class="auto-style2" style="text-align: left">
                    <asp:Label ID="Label5" runat="server" Text="ESTADO"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="ddlEstado" runat="server" Height="16px" Width="222px">
                        <asp:ListItem Selected="True">ACTIVO</asp:ListItem>
                        <asp:ListItem>INACTIVO</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td class="auto-style2">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td class="auto-style2">
                    <asp:Button ID="cmdNuevo" runat="server" Text="Nuevo" OnClick="cmdNuevo_Click" />
                    <asp:Button ID="cmdGrabar" runat="server" Text="Grabar" OnClick="cmdGrabar_Click" />
                    <asp:Button ID="cmdModificar" runat="server" Text="Modificar" OnClick="cmdModificar_Click" />
                </td>
                <td style="text-align: left">
                    <asp:Button ID="cmdEliminar" runat="server" Text="Eliminar" OnClick="cmdEliminar_Click" />
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td class="auto-style2">&nbsp;</td>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
    
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Footer" runat="server">
</asp:Content>
