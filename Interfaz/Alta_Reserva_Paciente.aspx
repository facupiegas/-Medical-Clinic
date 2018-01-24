<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Alta_Reserva_Paciente.aspx.cs" Inherits="Interfaz.Alta_Reserva_Paciente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    Realizar reserva de consulta médica<br />
<br />
Tipo de Médico:
<br />
<br />
<asp:RadioButton ID="rdbAltaReservaGeneral" runat="server" Checked="True" GroupName="rdbAltaReserva" Text="General" AutoPostBack="True" />
&nbsp;
<asp:RadioButton ID="rdbAltaReservaEspecialista" runat="server" GroupName="rdbAltaReserva" Text="Especialista" AutoPostBack="True" />
<br />
<br />
Seleccione una Fecha:<br />
    <br />
<asp:Calendar ID="calAltaReserva" runat="server" BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" Width="350px" OnSelectionChanged="calAltaReserva_SelectionChanged">
    <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
    <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
    <OtherMonthDayStyle ForeColor="#999999" />
    <SelectedDayStyle BackColor="#333399" ForeColor="White" />
    <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" Font-Bold="True" Font-Size="12pt" ForeColor="#333399" />
    <TodayDayStyle BackColor="#CCCCCC" />
</asp:Calendar>
<br />
Seleccione la hora de la consulta:
<br />
<br />
<asp:DropDownList ID="ddlAltaReservaHorarios" runat="server" OnSelectedIndexChanged="ddlAltaReservaHorarios_SelectedIndexChanged" AutoPostBack="True">
    <asp:ListItem Value="8" Selected="True">08:00</asp:ListItem>
    <asp:ListItem Value="9">09:00</asp:ListItem>
    <asp:ListItem Value="10">10:00</asp:ListItem>
    <asp:ListItem Value="11">11:00</asp:ListItem>
    <asp:ListItem Value="12">12:00</asp:ListItem>
    <asp:ListItem Value="13">13:00</asp:ListItem>
    <asp:ListItem Value="14">14:00</asp:ListItem>
    <asp:ListItem Value="15">15:00</asp:ListItem>
    <asp:ListItem Value="16">16:00</asp:ListItem>
    <asp:ListItem Value="17">17:00</asp:ListItem>
    <asp:ListItem Value="18">18:00</asp:ListItem>
    <asp:ListItem Value="19">19:00</asp:ListItem>
    <asp:ListItem Value="20">20:00</asp:ListItem>
</asp:DropDownList>
<br />
<br />
<asp:Panel ID="pnlAltaReservaGeneral" runat="server" Visible="False">
    <br />
    Listado de Medicos Generales disponibles:
    <br />
    <br />
    <asp:GridView ID="grdAltaReservaGeneral" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" EmptyDataText="No existen Datos para mostrar" ShowHeaderWhenEmpty="True">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:CommandField ButtonType="Button" SelectText="Seleccionar" ShowSelectButton="True" />
            <asp:BoundField DataField="NumLicencia" HeaderText="Numero Licencia" />
            <asp:BoundField DataField="NombreCompleto" HeaderText="Nombre" />
            <asp:BoundField DataField="Telefono" HeaderText="Telefono" />
            <asp:BoundField DataField="Direccion" HeaderText="Direccion" />
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
    <br />
    <asp:Button ID="btnAltaReservaGeneral" runat="server" OnClick="btnAltaReservaGeneral_Click" Text="Alta Reserva" />
    <br />
    <br />
    <asp:Label ID="lblAltaReservaGeneral" runat="server"></asp:Label>
    <br />
    <br />
</asp:Panel>
<br />
<asp:Panel ID="pnlAltaReservaEspecialista" runat="server" Visible="False">
    Seleccione una Especialidad:
    <br />
    <br />
    <asp:DropDownList ID="ddlAltaReservaEspecialista" runat="server" AutoPostBack="True">
    </asp:DropDownList>
    <br />
    <br />
    Listado de Medicos Especialistas disponibles:
    <br />
    <br />
    <asp:GridView ID="grdAltaReservaEspecialista" runat="server" CellPadding="4" ForeColor="Red" GridLines="None" AutoGenerateColumns="False" EmptyDataText="No se encontraron médicos disponibles para la fecha seleccionada">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:CommandField ButtonType="Button" SelectText="Seleccionar" ShowSelectButton="True" />
            <asp:BoundField DataField="NumLicencia" HeaderText="Numero Licencia" />
            <asp:BoundField DataField="NombreCompleto" HeaderText="Nombre" />
            <asp:BoundField DataField="Telefono" HeaderText="Telefono" />
            <asp:BoundField DataField="Direccion" HeaderText="Direccion" />
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
    <br />
    <asp:Button ID="btnAltaReservaEspecialista" runat="server" OnClick="btnAltaReservaEspecialista_Click" Text="Alta Reserva" />
    <br />
    <br />
    <asp:Label ID="lblAltaReservaEspecialista" runat="server"></asp:Label>
    <br />
</asp:Panel>
<br />
<br />
&nbsp;
</asp:Content>
