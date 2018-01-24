using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;

namespace Interfaz
{
    public partial class Listado_Medicos_Por_Sueldo : System.Web.UI.Page
    {
        Sistema dominio = Sistema.Instancia;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["tipo"].ToString() != "0")//Si el tipo de usuario loggeado es distinto a 0(admin) es redirigido al login
            {
                Response.Redirect("Login.aspx");
            }
            
            List<Medico> listaMedicos = dominio.MedicosOrdenadosPorSueldo();//obtengo la lista de medicos ordenada descendentemente por sueldo 
            if (listaMedicos.Count > 0)
            {
                lblMedicosOrdenadosPorSueldo.Visible = false;
                grdListadoMedicos.DataSource = dominio.MedicosOrdenadosPorSueldo();
                grdListadoMedicos.DataBind();
            }
            else
            {
                lblMedicosOrdenadosPorSueldo.ForeColor = System.Drawing.Color.Red;
                lblMedicosOrdenadosPorSueldo.Text = "No existen datos para mostrar";
                lblMedicosOrdenadosPorSueldo.Visible = true;
            }
        }
    }
}