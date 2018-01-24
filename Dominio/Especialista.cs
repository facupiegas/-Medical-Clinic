using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    [Serializable]
    public class Especialista:Medico
    {
        #region Atributos y Properties
        public static double FictoEspecialista { set; get; } = 0;
        public List<EspecialidadMedico> ListaEspecialidades { set; get; }
        #endregion

        #region Constructor
        public Especialista(int unNumLicencia, string unNombreCompleto, string unaDireccion, string unTelefono, bool unEsExclusivo, Usuario unUsuario) : base(unNumLicencia, unNombreCompleto, unaDireccion, unTelefono, unEsExclusivo, unUsuario)
        {
            this.ListaEspecialidades = new List<EspecialidadMedico>();
        }
        #endregion

        #region Otros Metodos
        public override double CalcularSalario()
        {
            double retorno = 0;
            double max = 0;
            foreach (EspecialidadMedico tmpEspecialidadMedico in this.ListaEspecialidades)
            {
                if (tmpEspecialidadMedico.Especialidad.ValorHora > max) {
                    max = tmpEspecialidadMedico.Especialidad.ValorHora;
                    retorno = tmpEspecialidadMedico.Especialidad.ValorHora;
                }
            }

            retorno *= this.HorasATrabajar;

            if (this.ListaEspecialidades.Count > 3)
            {
                retorno += Especialista.FictoEspecialista;
            }
            this.Sueldo = "$ " + Convert.ToString(retorno); 
            return retorno;
        }
        #endregion
    }

}
