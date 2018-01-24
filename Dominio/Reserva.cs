using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    [Serializable]
    public class Reserva
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
        public int IdReserva { set; get; }
        public DateTime Fecha { set; get; }
        public Medico Medico { set; get; }
        public Paciente Paciente { set; get; }
        public Especialidad Especialidad { set; get; }
        public bool fueCancelada { set; get; }

        public string FechaString
        {
            get { return Fecha.ToString("dd/MM/yyyy - hh:mm")+" hs"; }
                
        }

        public string NombreMedicoString
        {
            get { return Medico.NombreCompleto; }

        }

        public string NombreEspecialidadString
        {
            get { return Especialidad.NombreEspecialidad; }

        }
        #endregion

        #region Constructores
        public Reserva(DateTime unaFecha, Medico unMedico, Paciente unPaciente, Especialidad unaEspecialidad)
        {
            _UltimoNro++;
            this.IdReserva = _UltimoNro;
            this.Fecha = unaFecha;
            this.Medico = unMedico;
            this.Paciente = unPaciente;
            this.Especialidad = unaEspecialidad;
            this.fueCancelada = false;
        }
        #endregion

        #region Otros Metodos
        #endregion
    }
}
