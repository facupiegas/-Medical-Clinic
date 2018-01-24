<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Cancelar_Reseva.aspx.cs" Inherits="Interfaz.Cancelar_Reseva" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <asp:GridView ID="grdReservasPaciente" runat="server" AutoGenerateColumns="False" style="text-align: center" Visible="False" CellPadding="4" EmptyDataText="No hay datos para mostrar" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:CommandField ButtonType="Button" SelectText="Seleccionar" ShowSelectButton="True" />
            <asp:BoundField DataField="IdReserva" HeaderText="ID Reserva" />
            <asp:BoundField DataField="FechaString" HeaderText="Fecha" />
            <asp:BoundField DataField="NombreMedicoString" HeaderText="Medico" />
            <asp:BoundField DataField="NombreEspecialidadString" HeaderText="Especialidad" />
        </Columns>
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#E9E7E2" />
        <SortedAscendingHeaderStyle BackColor="#506C8C" />
        <SortedDescendingCellStyle BackColor="#FFFDF8" />
        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
    </asp:GridView>
    <br />
    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar Reserva" Visible="False" OnClick="btnCancelar_Click" />
    <br />
    <br />
    <asp:Label ID="lblMensaje" runat="server" Visible="False"></asp:Label>
    <br />
</asp:Content>
