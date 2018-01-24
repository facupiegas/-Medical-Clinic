using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    [Serializable]
    public abstract class Medico:IComparable<Medico>
    {
        #region Atributos y Properties
        public int NumLicencia { set; get; }
        public string NombreCompleto { set; get; }
        public string Direccion { set; get; }
        public string Telefono { set; get; }
        public bool EsExclusivo { set; get; }
        public Usuario Usuario { set; get; }
        public double HorasATrabajar { set; get; }
        private string sueldo;
        public string Sueldo
        {
            get
            {
                return "$ " + Convert.ToString(this.CalcularSalario());
            }
            set
            {
                sueldo = value;
            }
        }
        #endregion

        #region Constructor
        public Medico(int unNumLicencia,string unNombreCompleto,string unaDireccion,string unTelefono,bool unEsExclusivo,Usuario unUsuario)
        {
            this.NumLicencia = unNumLicencia;
            this.NombreCompleto = unNombreCompleto;
            this.Direccion = unaDireccion;
            this.Telefono = unTelefono;
            this.EsExclusivo = unEsExclusivo;
            this.Usuario = unUsuario;
            this.HorasATrabajar = 100;
        }
        #endregion

        #region Otros Metodos

        public int CompareTo(Medico unMedico)
        {
            return this.CalcularSalario().CompareTo(unMedico.CalcularSalario());
        } 

        public abstract double CalcularSalario();

        public void AgregarDetalleHistoriaClinica(Paciente unPaciente, string unDetalle)
        {
            unPaciente.HistoriaClinica.Add(unDetalle);
        }


        #endregion
    }
}
