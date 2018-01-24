using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;

namespace Interfaz
{
    public partial class Consulta_Reservas_Paciente : System.Web.UI.Page
    {
        Sistema dominio = Sistema.Instancia;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["tipo"].ToString() != "2")//Si el tipo de usuario loggeado es distinto a 2(Paciente) es redirigido al login
            {
                Response.Redirect("Login.aspx");
            }
        }
        protected void radGeneral_CheckedChanged(object sender, EventArgs e)
        {
            grdConsultas.Visible = false;
            calConsulta.Visible = false;
            comboEspecialidades.Visible = false;
            btnConsulta.Visible = true;
        }

        protected void radFecha_CheckedChanged(object sender, EventArgs e)
        {
            grdConsultas.Visible = false;
            calConsulta.Visible = true;
            comboEspecialidades.Visible = false;
            btnConsulta.Visible = true;
        }

        protected void radEspecialidad_CheckedChanged(object sender, EventArgs e)
        {
            grdConsultas.Visible = false;
            calConsulta.Visible = false;
            comboEspecialidades.Visible = true;
            btnConsulta.Visible = true;
            comboEspecialidades.DataSource = dominio.ListaEspecialidades;
            comboEspecialidades.DataTextField = "NombreEspecialidad";
            comboEspecialidades.DataValueField = "IdEspecialidad";
            comboEspecialidades.DataBind();
        }

        protected void btnConsulta_Click(object sender, EventArgs e)
        {
            string nombreUsuario = (string)Session["usuario"];//obtengo el nombre del usuario logueado en el sistema
            Usuario unUsuario = dominio.BuscarUsuario(nombreUsuario);//busco el objeto usuario que contiene ese nombre en la lista de usuarios del sistema
            Paciente unPaciente = dominio.BuscarPacientePorUsuario(unUsuario);//busco el paciente que contiene el objeto usuario encontrado
            if (radGeneral.Checked)
            {
                List<Reserva> listaReservasGeneral = dominio.ListaReservasPacienteGeneral(unPaciente);
                if (listaReservasGeneral.Count != 0)
                {
                    lblMensaje.Visible = false;
                    grdConsultas.DataSource = listaReservasGeneral;
                    grdConsultas.DataBind();
                }
                else
                {
                    grdConsultas.Visible = false;
                    lblMensaje.Visible = true;
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    lblMensaje.Text = "No se encontraron reservas para la especialidad seleccionada";
                }
            }
            else if (radEspecialidad.Checked)
            {
                int idEspecialidad = Convert.ToInt32(comboEspecialidades.SelectedValue);//obtengo el id de la especialidad seleccionada en el dropdownlist
                Especialidad unaEspecialidad = dominio.BuscarEspecialidad(idEspecialidad);//busco el objeto especialidad con el id guardado
                List<Reserva> listaReservasPorEspecialidad = dominio.ListaReservasPacientePorEspecialidad(unPaciente, unaEspecialidad);//cargo la lista de reservas filtrada por paciente y especialidad
                if(listaReservasPorEspecialidad.Count != 0)
                {
                    grdConsultas.DataSource = listaReservasPorEspecialidad;
                    grdConsultas.DataBind();
                    grdConsultas.Visible = true;
                    lblMensaje.Visible = false;
                }
                else
                {
                    grdConsultas.Visible = false;
                    lblMensaje.Visible = true;
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    lblMensaje.Text = "No se encontraron reservas para la especialidad seleccionada";
                }
                
            }
            else if (radFecha.Checked)
            {
                DateTime unaFecha = calConsulta.SelectedDate; //obtengo la fecha seleccionada en el calendario
                List<Reserva> listaReservasPorFecha = dominio.ListaReservasPacientePorFecha(unPaciente, unaFecha);//obtengo la lista de reservas filtrada por paciente y fecha
                if (listaReservasPorFecha.Count > 0)
                {
                    grdConsultas.DataSource = listaReservasPorFecha;
                    grdConsultas.DataBind();
                    grdConsultas.Visible = true;
                    lblMensaje.Visible = false; 
                }
                else
                {
                    grdConsultas.Visible = false;
                    lblMensaje.Visible = true;
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    lblMensaje.Text = "No se encontraron reservas para la especialidad seleccionada";
                }
            }
        }

        
    }
}