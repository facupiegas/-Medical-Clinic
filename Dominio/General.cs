using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    [Serializable]
    public class General:Medico
    {
        #region Atributos y Properties
        public static double ValorHora { set; get; } = 110;
        public DateTime FechaRecibimiento { set; get; }
        #endregion

        #region Constructor
        public General(DateTime unaFechaRecibimiento, int unNumLicencia, string unNombreCompleto, string unaDireccion, string unTelefono, bool unEsExclusivo, Usuario unUsuario) : base(unNumLicencia, unNombreCompleto, unaDireccion, unTelefono, unEsExclusivo, unUsuario)
        {
            this.FechaRecibimiento = unaFechaRecibimiento;
        }
        #endregion

        #region Otros Metodos
        public override double CalcularSalario()
        {
            double retorno = General.ValorHora * this.HorasATrabajar;
            TimeSpan intervalo = DateTime.Now - this.FechaRecibimiento;
            int horasEnCincoAnios =43800;
            if (intervalo.Hours > horasEnCincoAnios)
            {
                retorno *= 1.10;
            }
            this.Sueldo = "$ " + Convert.ToString(retorno);
            return retorno;
        }
        #endregion
    }
}
