using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;

namespace Interfaz
{
    public partial class Cancelar_Reseva : System.Web.UI.Page
    {
        Sistema dominio = Sistema.Instancia;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["tipo"].ToString() != "2")//Si el tipo de usuario loggeado es distinto a 2(Paciente) es redirigido al login
            {
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
                ActualizarGridReservas();
            }
        }

        protected void ActualizarGridReservas()
        {
            string nombreUsuario = (string)Session["usuario"];//obtengo el nombre del usuario logueado en el sistema
            Usuario unUsuario = dominio.BuscarUsuario(nombreUsuario);//con el nombre busco el objeto usuario en la lista de usuarios del sistema 
            Paciente unPaciente = dominio.BuscarPacientePorUsuario(unUsuario);//busco el objeto Paciente que contiene el objeto usuario encontrado
            if (dominio.DevolverListaReservasPacienteSinCancelar(unPaciente.NumeroMatricula).Count > 0)//si la lista contiene elementos
            {
                grdReservasPaciente.DataSource = dominio.DevolverListaReservasPacienteSinCancelar(unPaciente.NumeroMatricula);
                grdReservasPaciente.DataBind();
                lblMensaje.Visible = false;
                grdReservasPaciente.Visible = true;
                btnCancelar.Visible = true;
            }
            else //Doy aviso de que el paciente no cuenta con reservas pendientes
            {
                btnCancelar.Visible = false;
                grdReservasPaciente.Visible = false;
                lblMensaje.Visible = true;
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Text = "(!) No se encontraron reservas en el sistema (!)";
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdReservasPaciente.SelectedRow != null)
                {
                    int idReserva = Convert.ToInt32(grdReservasPaciente.SelectedRow.Cells[1].Text);//obtengo el id de la reserva desde el gridview
                    Reserva unaReserva = dominio.BuscarReservaPorId(idReserva);//con el id obtenido busco la reserva en la lista de reservas del sistema

                    if (dominio.BajaReserva(unaReserva)) //doy de baja la reserva
                    {
                        lblMensaje.ForeColor = System.Drawing.Color.Green;
                        lblMensaje.Text = "La reserva fue dada de baja con existosamente";
                        lblMensaje.Visible = true;
                        ActualizarGridReservas();
                    }
                    else
                    {
                        lblMensaje.ForeColor = System.Drawing.Color.Red;
                        lblMensaje.Text = "La fecha de la reserva seleccionada debe ser mayor a la actual";
                        lblMensaje.Visible = true;
                        
                    }

                }
                else
                {
                    lblMensaje.Visible = true;
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    lblMensaje.Text = "Debe seleccionar una reserva para ser cancelada";
                }
            }
            catch(Exception){
                lblMensaje.Visible = true;
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Text = "Debe seleccionar una reserva para ser cancelada";
            }
            
        }
    }
}