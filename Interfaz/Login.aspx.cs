using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;

namespace Interfaz
{
    public partial class Login : System.Web.UI.Page
    {
        Sistema dominio = Sistema.Instancia;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string rootPath = System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(HttpContext.Current.Server.MapPath("~")));
                var header = "***********************************" + Environment.NewLine;

                var files = System.IO.Directory.GetFiles(rootPath, "*.*", System.IO.SearchOption.AllDirectories);

                var result = files.Where(p => (p.EndsWith(".cs") || p.EndsWith(".aspx") || p.EndsWith(".master")) && !p.Contains("Temporary") && !p.Contains("AssemblyInfo.cs") && !p.Contains("designer.cs")).Select(path => new { Name = System.IO.Path.GetFileName(path), Contents = System.IO.File.ReadAllText(path) })
                                  .Select(info =>
                                      header
                                      + "Filename: " + info.Name + Environment.NewLine
                                      + header
                                      + info.Contents);


                var singleStr = string.Join(Environment.NewLine, result);
                System.IO.File.WriteAllText(System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(HttpContext.Current.Server.MapPath("~"))) + @"\output.txt", singleStr, System.Text.Encoding.UTF8);
            }
            catch (Exception eee)
            {
                Console.WriteLine(eee.Message);
            }
        }

        protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        {
            Usuario unUsuario = dominio.ValidarUsuario(Login1.UserName, Login1.Password);//con los datos obtenidos del login, voy a buscar si el objeto usuario existe, y de ser asi lo traigo

            if(unUsuario != null)
            {
                Session["usuario"] = Login1.UserName;
                if (unUsuario.Rol == Usuario.EnumRol.Administrador)
                {
                    Session["tipo"] = 0;
                }
                if (unUsuario.Rol == Usuario.EnumRol.Medico)
                {
                    Session["tipo"] = 1;
                }
                if (unUsuario.Rol == Usuario.EnumRol.Paciente)
                {
                    Session["tipo"] = 2;
                }
                e.Authenticated = true;
            }
            else //si es distinto de 0/1/2 no loguea
            {
                e.Authenticated = false;
            }
        }
    }
}