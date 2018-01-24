using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;

namespace Interfaz
{
    public partial class Alta_Reserva_Paciente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["tipo"].ToString() != "2")//Si el tipo de usuario loggeado es distinto a 2(Paciente) es redirigido al login
            {
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
                ddlAltaReservaEspecialista.DataSource = Sistema.Instancia.ListaEspecialidades;
                ddlAltaReservaEspecialista.DataTextField = "NombreEspecialidad";
                ddlAltaReservaEspecialista.DataValueField = "IdEspecialidad";
                ddlAltaReservaEspecialista.DataBind();
                calAltaReserva.SelectedDate = DateTime.Now.Date;
            }
            ActualizarGridMedicoGeneral();
            ActualizarGridMedicoEspecialista();
            
            
            if (rdbAltaReservaGeneral.Checked)
            {
                pnlAltaReservaGeneral.Visible = true;
                pnlAltaReservaEspecialista.Visible = false;
            }
            if (rdbAltaReservaEspecialista.Checked)
            {
                pnlAltaReservaGeneral.Visible = false;
                pnlAltaReservaEspecialista.Visible = true;
            }
            lblAltaReservaGeneral.Text = "";
            lblAltaReservaEspecialista.Text = "";
        }

        protected DateTime ObtenerFechaConsulta()
        {
            int hora = Convert.ToInt32(ddlAltaReservaHorarios.SelectedValue); //tomo el valor de la hora seleccionada del dropdownlist
            DateTime fechaCalendario = calAltaReserva.SelectedDate; //tomo la fecha seleccionada del calendario
            DateTime fecha = new DateTime(fechaCalendario.Year, fechaCalendario.Month, fechaCalendario.Day, hora, 0, 0); //formo un DateTime con los datos
            return fecha;
        }
        protected void ActualizarGridMedicoGeneral()
        {
            grdAltaReservaGeneral.DataSource = Sistema.Instancia.DevolverListaMedicosGeneralesDisponibles(ObtenerFechaConsulta());
            grdAltaReservaGeneral.DataBind();
        }
        protected void ActualizarGridMedicoEspecialista()
        {

            int idEspecialidad = Convert.ToInt32(ddlAltaReservaEspecialista.SelectedValue);
            Especialidad tmpEspecialidad = Sistema.Instancia.BuscarEspecialidad(idEspecialidad);
            grdAltaReservaEspecialista.DataSource = Sistema.Instancia.DevolverListMedicosEspecialistaDisponibles(ObtenerFechaConsulta(), tmpEspecialidad);
            grdAltaReservaEspecialista.DataBind();

        }

        protected void ddlAltaReservaHorarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarGridMedicoGeneral();
            ActualizarGridMedicoEspecialista();
        }

        protected void calAltaReserva_SelectionChanged(object sender, EventArgs e)
        {
            ActualizarGridMedicoGeneral();
            ActualizarGridMedicoEspecialista();
        }
        protected void btnAltaReservaGeneral_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdAltaReservaGeneral.SelectedRow != null)
                {

                    string nombreUsuario = (string)Session["usuario"]; //obtengo el nombre del usuario logueado en el sistema
                    Usuario unUsuario = Sistema.Instancia.BuscarUsuario(nombreUsuario);//con el nombre busco el objeto usuario en la lista de usuarios del sistema
                    Paciente tmpPaciente = Sistema.Instancia.BuscarPacientePorUsuario(unUsuario);//con el objeto usuario busco el Paciente que contiene ese usuario
                    Medico tmpMedico = Sistema.Instancia.BuscarMedico(Convert.ToInt32(grdAltaReservaGeneral.SelectedRow.Cells[1].Text));//busco el Medico con el id tomado desde el gridview
                    if (Sistema.Instancia.AltaReservaGeneral(ObtenerFechaConsulta(), tmpMedico, tmpPaciente))//doy de alta la reserva
                    {
                        lblAltaReservaGeneral.ForeColor = System.Drawing.Color.Green;
                        lblAltaReservaGeneral.Text = "La reserva ha sido efectuada con exito";
                    }
                    else
                    {
                        lblAltaReservaGeneral.ForeColor = System.Drawing.Color.Red;
                        lblAltaReservaGeneral.Text = "La reserva no ha podido ser efectuada. La fecha elegida debe ser mayor a la actual";
                    }
                    ActualizarGridMedicoGeneral();
                }
                else
                {
                    lblAltaReservaGeneral.ForeColor = System.Drawing.Color.Red;
                    lblAltaReservaGeneral.Text = "Debe seleccionar un medico para realizar una reserva";
                }
            }
            catch (Exception)
            {

                lblAltaReservaGeneral.ForeColor = System.Drawing.Color.Red;
                lblAltaReservaGeneral.Text = "Debe seleccionar un medico para realizar una reserva";
            }
        }

        protected void btnAltaReservaEspecialista_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdAltaReservaEspecialista.SelectedRow != null)
                {
                    string nombreUsuario = (string)Session["usuario"]; //obtengo el nombre del usuario logueado en el sistema
                    Usuario unUsuario = Sistema.Instancia.BuscarUsuario(nombreUsuario);//con el nombre busco el objeto usuario en la lista de usuarios del sistema
                    Paciente tmpPaciente = Sistema.Instancia.BuscarPacientePorUsuario(unUsuario);//con el objeto usuario busco el Paciente que contiene ese usuario
                    Medico tmpMedico = Sistema.Instancia.BuscarMedico(Convert.ToInt32(grdAltaReservaEspecialista.SelectedRow.Cells[1].Text));//busco el Medico con el id tomado desde el gridview
                    Especialidad tmpEspecialidad = Sistema.Instancia.BuscarEspecialidad(Convert.ToInt32(ddlAltaReservaEspecialista.SelectedValue));//busco la especialidad con el valor seleccionado en el dropdownlist
                    if (Sistema.Instancia.AltaReservaEspecialista(ObtenerFechaConsulta(), tmpMedico, tmpPaciente, tmpEspecialidad)) //doy de alta la reserva
                    {
                        lblAltaReservaEspecialista.ForeColor = System.Drawing.Color.Green;
                        lblAltaReservaEspecialista.Text = "La reserva ha sido efectuada con exito";
                    }
                    else
                    {
                        lblAltaReservaEspecialista.ForeColor = System.Drawing.Color.Red;
                        lblAltaReservaEspecialista.Text = "La reserva no ha podido ser efectuada. La fecha elegida debe ser mayor a la actual";
                    }
                    ActualizarGridMedicoEspecialista();
                }
                else {
                    lblAltaReservaEspecialista.ForeColor = System.Drawing.Color.Red;
                    lblAltaReservaEspecialista.Text = "Debe seleccionar un medico para realizar una reserva";
                }
            }
            catch (Exception)
            {

                lblAltaReservaEspecialista.ForeColor = System.Drawing.Color.Red;
                lblAltaReservaEspecialista.Text = "Debe seleccionar un medico para realizar una reserva";
            }
        }
    }
}