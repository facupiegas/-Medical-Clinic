<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Consulta_Reservas_Paciente.aspx.cs" Inherits="Interfaz.Consulta_Reservas_Paciente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>
        SELECCIONE UN FILTRO
    </p>
    <p>
        <asp:RadioButton ID="radGeneral" runat="server" AutoPostBack="True" GroupName="filtroConsulta" OnCheckedChanged="radGeneral_CheckedChanged" Text="General" />
&nbsp;
        <asp:RadioButton ID="radEspecialidad" runat="server" AutoPostBack="True" GroupName="filtroConsulta" OnCheckedChanged="radEspecialidad_CheckedChanged" Text="Especialidad" />
&nbsp;
        <asp:RadioButton ID="radFecha" runat="server" AutoPostBack="True" GroupName="filtroConsulta" OnCheckedChanged="radFecha_CheckedChanged" Text="Fecha" />
    &nbsp;</p>
    <p>
        <asp:DropDownList ID="comboEspecialidades" runat="server" Visible="False">
        </asp:DropDownList>
    </p>
    <p>
        <asp:Calendar ID="calConsulta" runat="server" BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" Visible="False" Width="350px">
            <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
            <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
            <OtherMonthDayStyle ForeColor="#999999" />
            <SelectedDayStyle BackColor="#333399" ForeColor="White" />
            <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" Font-Bold="True" Font-Size="12pt" ForeColor="#333399" />
            <TodayDayStyle BackColor="#CCCCCC" />
        </asp:Calendar>
    </p>
    <p>
        <asp:Button ID="btnConsulta" runat="server" OnClick="btnConsulta_Click" Text="Consultar" Visible="False" />
    </p>
    <p>
    <asp:GridView ID="grdConsultas" runat="server" AutoGenerateColumns="False" style="text-align: center" Visible="False" CellPadding="4" EmptyDataText="No hay datos para mostrar" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
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
    </p>
    <p>
        <asp:Label ID="lblMensaje" runat="server"></asp:Label>
    </p>
</asp:Content>
