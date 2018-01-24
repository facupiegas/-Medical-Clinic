using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    [Serializable]
    public class Paciente
    {
        #region Atributos y Properties
        //Atributos de Clase
        // Autonumerado
        private static int _UltimoNro = 0;
        public static int UltimoNumero
        {
            get { return _UltimoNro; }
        }
        // Atributos de Instancia
        public int NumeroMatricula { set; get; }
        public string NombreCompleto { set; get; }
        public string Email { set; get; }
        public string Telefono { set; get; }
        public string Cedula { set; get; }
        public Usuario Usuario { set; get; }
        public List<string> HistoriaClinica { set; get; }
        public List<Reserva> ListaReservasPaciente { set; get; }
        #endregion

        #region Constructores
        public Paciente(string unNombreCompleto, string unEmail, string unTelefono, string unaCedula, Usuario unUsuario)
        {
            _UltimoNro++;
            this.NumeroMatricula = _UltimoNro;
            this.NombreCompleto = unNombreCompleto;
            this.Email = unEmail;
            this.Telefono = unTelefono;
            this.Cedula = unaCedula;
            this.Usuario = unUsuario;
            this.ListaReservasPaciente = new List<Reserva>();
            this.HistoriaClinica = new List<String>();
        }
        #endregion
     
    }
}
