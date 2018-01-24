using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    [Serializable]
    public class Usuario
    {
        #region Atributos y Properties

        public enum EnumRol { Administrador, Medico, Paciente }
        public string NombreUsuario { set; get; }
        public string contrasena { set; get; }
        public EnumRol Rol { set; get; }
        #endregion

        #region Constructor
        public Usuario(EnumRol unRol, string unNombreUsuario, string unaContrasena)
        {
            this.Rol = unRol;
            this.NombreUsuario = unNombreUsuario;
            this.contrasena = unaContrasena;
        }
        #endregion

        #region Otros Metodos

        #endregion
    }
}
