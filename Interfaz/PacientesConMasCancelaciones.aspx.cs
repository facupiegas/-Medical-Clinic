using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;

namespace Interfaz
{
    public partial class PacientesConMasCancelaciones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["tipo"].ToString() != "0")//Si el tipo de usuario loggeado es distinto a 0(admin) es redirigido al login
            {
                Response.Redirect("Login.aspx");
            }

            List<Paciente> listaPacientes = Sistema.Instancia.PacientesConMasCancelaciones();
            if (listaPacientes.Count > 0)
            {
                lblPacientesMasCancelaciones.Visible = false;
                grdPacientesMasCancelaciones.DataSource = listaPacientes;
                grdPacientesMasCancelaciones.DataBind();
            }
            else
            {
                lblPacientesMasCancelaciones.ForeColor = System.Drawing.Color.Red;
                lblPacientesMasCancelaciones.Text = "No existen datos para mostrar";
                lblPacientesMasCancelaciones.Visible = true;
            }
        }
    }
}