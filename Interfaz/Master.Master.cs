using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;

namespace Interfaz
{
    public partial class Master : System.Web.UI.MasterPage
    {
        Sistema dominio = Sistema.Instancia;
        protected void Page_Load(object sender, EventArgs e)
        {
            int tipo = (int)Session["tipo"];
            lblUsuario.Text = (string)Session["usuario"];
            lblUsuario.Visible = true;
            lnkSalir.Visible = false;
            if (tipo != -1)
            {
                lblBienvenida.Visible = true;
                lnkSalir.Visible = true;
                if (tipo == 0)
                {
                    menuAdmin.Visible = true;
                    menuMedico.Visible = false;
                    menuPaciente.Visible = false;
                }
                if (tipo == 1)
                {
                    menuAdmin.Visible = false;
                    menuMedico.Visible = true;
                    menuPaciente.Visible = false;
                }
                if (tipo == 2)
                {
                    menuAdmin.Visible = false;
                    menuMedico.Visible = false;
                    menuPaciente.Visible = true;
                }
            }
            else
            {
                menuAdmin.Visible = false;
                menuMedico.Visible = false;
                menuPaciente.Visible = false;
            }
        }
        protected void lnkSalir_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            Response.Redirect("Login.aspx");
        }
    }
}