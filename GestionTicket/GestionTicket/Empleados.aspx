<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Empleados.aspx.cs" Inherits="GestionTicket.Empleados" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 193px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
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
                <td class="auto-style2">
                    <asp:Label ID="Label1" runat="server" Text="DNI"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDni" runat="server" OnTextChanged="txtDni_TextChanged"></asp:TextBox>
                    <asp:Button ID="cmdBuscar" runat="server" OnClick="cmdBuscar_Click" Text="Buscar Empleado" />
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td class="auto-style2">
                    <asp:Label ID="Label2" runat="server" Text="NOMBRE"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TxtNombre" runat="server" Width="218px"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td class="auto-style2">
                    <asp:Label ID="Label3" runat="server" Text="CERTIFICADO"></asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="ChkCertificado" runat="server" />
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td class="auto-style2">
                    <asp:Label ID="Label4" runat="server" Text="EDAD"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TxtEdad" runat="server" Width="73px"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td class="auto-style2">
                    <asp:Label ID="Label5" runat="server" Text="ESTADO"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlEstado" runat="server">
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
                <td>
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
    </form>
</body>
</html>