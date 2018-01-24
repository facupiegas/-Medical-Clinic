using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;

namespace Interfaz
{
    public partial class Visualizar_Lista_Pacientes_Por_Fecha : System.Web.UI.Page
    {
        Sistema dominio = Sistema.Instancia;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["tipo"].ToString() != "1")//Si el tipo de usuario loggeado es distinto a 1(Médico) es redirigido al login
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void calFecha_SelectionChanged(object sender, EventArgs e) //al cambiar la fecha seleccionada se despliega el grid con los pacientes que hayan realizado una reserva ese dia
        {
            DateTime fecha = calFecha.SelectedDate.Date;
            string nombreUsuario = (string)Session["usuario"]; //obtengo el nombre del usuario logueado en el sistema
            Usuario unUsuario = dominio.BuscarUsuario(nombreUsuario); //busco el objeto usuario con ese nombre en la lista de usuarios del sistema
            Medico unMedico = dominio.BuscarMedicoPorUsuario(unUsuario); //busco el objeto medico que contiene el objeto usuario encontrado
            List<Paciente> listaPacientes = dominio.DevolverPacientesMedicoPorFecha(unMedico, fecha);//obtengo la lista filtrada por medico y fecha
            if (listaPacientes.Count != 0)
            {
                grdDatosPacientes.Visible = true;
                grdDatosPacientes.DataSource = listaPacientes;
                grdDatosPacientes.DataBind();
                lblMensaje.Visible = false;
            }
            else
            {
                grdDatosPacientes.Visible = false;
                grdHistoriaClinica.Visible = false;
                lblMensaje.Visible = true;
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Text = "No se encontraron pacientes en el sistema (!)";
            }

        }

        protected void grdDatosPaciente_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Paciente unPaciente = dominio.BuscarPaciente(grdDatosPacientes.SelectedRow.Cells[5].Text);//con los datos obtenidos del gridview busco el objeto paciente en la lista de pacientes del sistema
                if (unPaciente.HistoriaClinica.Count != 0)
                {
                    grdHistoriaClinica.Visible = true;
                    grdHistoriaClinica.DataSource = unPaciente.HistoriaClinica;
                    grdHistoriaClinica.DataBind();
                    grdHistoriaClinica.HeaderRow.Cells[0].Text = "Historia Clinica"; //cambio el nombre del header de la columna(por defecto es "Item")
                }
                else
                {
                    grdHistoriaClinica.Visible = false;
                    lblMensaje.Visible = true;
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    lblMensaje.Text = "No se encontró historia clínica para el paciente seleccionado en el sistema (!)";
                }
            }
            catch (Exception)
            {
                lblMensaje.Visible = true;
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Text = "Debe seleccionar un paciente para visualizar su historia clínica";
            }

        }
    }
}